using BLL;
using Classes;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desktop
{
    public partial class UCReviews : UserControl
    {
        Review selectedReview;
        private readonly ReviewManager _reviewManager;
        private readonly UserController _userController;
        public UCReviews(IReviewRepository revrepo, IUserRepository userRepo)
        {
            InitializeComponent();
            _reviewManager = new ReviewManager(revrepo);
            _userController = new UserController(userRepo);
        }

        private void btnSusreviews_Click(object sender, EventArgs e)
        {
            var reviews = _reviewManager.GetAllReviews().Where(x => x.Likes < 0);
            lbReviews.DataSource = reviews.ToList();
        }
        private void btnGetRated_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(tbLikeThreshold.Text, out int value))
            {
                MessageBox.Show("Please enter a valid number. From how much likes should reviews be considered highly rated.");
                return;
            }
            var treshHold = int.Parse(tbLikeThreshold.Text);
            var reviews = _reviewManager.GetAllReviews().Where(x => x.Likes >= treshHold);
            lbReviews.DataSource = reviews.ToList();
        }
        private void lbReviews_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedReview = lbReviews.SelectedItem as Review;
                tbComment.Text = selectedReview.Comment;
                lbLikes.Text = "Likes: " + selectedReview.Likes.ToString();
                lbUser.Text = "User: " + selectedReview.User.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnPromote_Click(object sender, EventArgs e)
        {
            bool isAlreadyPromoted = _userController.CheckIfUserIsPowerUser(selectedReview.User.Id);
            if (isAlreadyPromoted)
            {
                MessageBox.Show("User is already a power user");
                return;
            }
            else
            {
                _userController.AddPowerUser(selectedReview.User.Id);
                MessageBox.Show("User promoted to power user");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var newComment = tbComment.Text;
            selectedReview.Comment = newComment;
            bool success = _reviewManager.UpdateReview(selectedReview);
            if (success)
            {
                MessageBox.Show("Review updated");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _reviewManager.DeleteReview(selectedReview);
            MessageBox.Show("Review deleted");
            var reviews = _reviewManager.GetAllReviews();
            lbReviews.DataSource = reviews.ToList();
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            var reviews = _reviewManager.GetAllReviews();
            lbReviews.DataSource = reviews.ToList();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
           bool success = _userController.DeleteUserProfile(selectedReview.User);
            if (success)
            {
                MessageBox.Show("User deleted");
            }
            var reviews = _reviewManager.GetAllReviews();
            lbReviews.DataSource = reviews.ToList();
        }
    }
}
