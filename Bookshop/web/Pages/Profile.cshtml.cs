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
        private BookManager _bookManager;
        private BLL.ReviewManager _reviewManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public List<Review> MyReviews { get; set; }
        public User MyUser { get; set; }

        public ProfileModel(IWebHostEnvironment hostingEnvironment, BookManager bookManager, BLL.ReviewManager reviewManager, UserController userController)
        {
            _userCon = userController;
            _bookManager = bookManager;
            _reviewManager = reviewManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult OnGet()
        {
            MyUser = _userCon.GetUserById(Convert.ToInt32(User.FindFirstValue("id")));
            MyReviews = _reviewManager.GetAllReviewsByUser(MyUser);

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
