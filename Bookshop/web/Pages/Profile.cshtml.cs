using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace web.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        UserController _userCon;
        private readonly IWebHostEnvironment _hostingEnvironment;


        [BindProperty]
        public BookManagement _bookManager { get; set; }
        public List<Review> MyReviews { get; set; }
        public User MyUser { get; set; }

        public ProfileModel(IReviewRepository reviewRepository, IUserRepository userRepository, IBookRepository bookRepo, IWebHostEnvironment hostingEnvironment)
        {
            _userCon = new UserController(userRepository);
            _bookManager = new BookManagement(bookRepo, reviewRepository);
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult OnGet()
        {
            if (User.Identity == null && User.Identity.IsAuthenticated == false)
            {
                TempData["Message"] = "You are already logged in! Log out if you want to login a new profile.";
                RedirectToPage("/");
            }

            MyReviews = _bookManager.GetAllReviewsByUser(Convert.ToInt32(User.FindFirstValue("id")));
            MyUser = _userCon.GetUserById(Convert.ToInt32(User.FindFirstValue("id")));
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile profilePicture)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                var fileExtension = Path.GetExtension(profilePicture.FileName).ToLower();

                if (allowedExtensions.Contains(fileExtension))
                {
                    var uploadsPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadsPath))
                    {
                        Directory.CreateDirectory(uploadsPath);
                    }

                    MyUser = _userCon.GetUserById(Convert.ToInt32(User.FindFirstValue("id")));
                    string oldFilePath = Path.Combine(uploadsPath, MyUser.PicturePath);

                    if (MyUser.PicturePath != "noPic")
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + profilePicture.FileName;
                    var filePath = Path.Combine(uploadsPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await profilePicture.CopyToAsync(fileStream);
                    }

                    _userCon.InsertProfilePicture(Convert.ToInt32(User.FindFirstValue("id")), uniqueFileName);

                    return RedirectToPage("/Profile");
                }
                else
                {
                    // Invalid file type, handle accordingly (e.g., return an error message)
                    ModelState.AddModelError(string.Empty, "Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.");
                }
            }

            return RedirectToPage("/Profile");
        }

    }
}
