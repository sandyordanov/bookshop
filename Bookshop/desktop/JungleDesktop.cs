using DAL.Interfaces;

namespace desktop
{
    public partial class JungleDesktop : Form
    {
        private IBookRepository bookRepo;

        public JungleDesktop(IBookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
            InitializeComponent();
        }

        private void addBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            var uc = new UCBooks(bookRepo) { Dock = DockStyle.Fill };
            panel1.Controls.Add(uc);
        }

        private void JungleDesktop_Load(object sender, EventArgs e)
        {

        }
    }
}