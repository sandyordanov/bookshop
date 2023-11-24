using DAL;
using DAL.Interfaces;

namespace desktop
{
    public partial class JungleDesktop : Form
    {
        private IBookRepository bookRepo;
        private IReviewRepository reviewRepo;

        public JungleDesktop(IBookRepository bookRepo, IReviewRepository reviewRepo)
        {
            this.bookRepo = bookRepo;
            this.reviewRepo = reviewRepo;
            InitializeComponent();
        }

        private void addBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            var uc = new UCBooks(bookRepo,reviewRepo) { Dock = DockStyle.Fill };
            panel1.Controls.Add(uc);
        }

        private void JungleDesktop_Load(object sender, EventArgs e)
        {

        }
    }
}