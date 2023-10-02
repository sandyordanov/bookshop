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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace desktop
{
    public partial class UCBooks : UserControl
    {
        string action;
        List<Book> books;
        private IBookRepository bookRepo;

        public UCBooks(IBookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
            books = new List<Book>();
            InitializeComponent();
        }

        private void tbnShowAll_Click(object sender, EventArgs e)
        {
            RefreshCollection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddFormControl();

        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            switch (action)
            {
                case "add":
                    Book book = new Book(tbTitle.Text, tbAuthor.Text, tbLanguage.Text, tbPublisher.Text, int.Parse(tbPages.Text), tbISBN.Text, double.Parse(tbPrice.Text), int.Parse(tbYear.Text), new List<Review>());
                    bool success = bookRepo.AddBook(book);
                    ReadOnlyTrue();
                    if (success) { MessageBox.Show("Book succesfully added."); RefreshCollection(); }
                    else
                    {
                        MessageBox.Show("Problem with adding the book occured.");
                    }
                    break;
                case "update":

                    break;
                default:
                    break;
            }

        }

        private void AddFormControl()
        {
            tbTitle.ReadOnly = false;
            tbTitle.Clear();
            tbAuthor.ReadOnly = false;
            tbAuthor.Clear();
            tbLanguage.ReadOnly = false;
            tbLanguage.Clear();
            tbPublisher.ReadOnly = false;
            tbPublisher.Clear();

            tbPages.ReadOnly = false;
            tbPages.Clear();
            tbISBN.ReadOnly = false;
            tbISBN.Clear();
            tbPrice.ReadOnly = false;
            tbPrice.Clear();
            tbYear.ReadOnly = false;
            tbYear.Clear();

            btnConfirm.Visible = true;
            btnCancel.Visible = true;
            action = "add";
        }

        private void ReadOnlyTrue()
        {
            tbTitle.ReadOnly = true;
            tbAuthor.ReadOnly = true;
            tbLanguage.ReadOnly = true;
            tbPublisher.ReadOnly = true;


            tbPages.ReadOnly = true;
            tbISBN.ReadOnly = true;
            tbPrice.ReadOnly = true;
            tbYear.ReadOnly = true;
        }
        private void LoadBookInfo(Book book)
        {
            tbTitle.Text = book.GetTitle();
            tbAuthor.Text = book.GetAuthor();
            tbLanguage.Text = book.GetLanguage();
            tbPublisher.Text = book.GetPublisher();


            tbPages.Text = Convert.ToString(book.GetPages());
            tbISBN.Text = book.GetISBN();
            tbPrice.Text = Convert.ToString(book.GetPrice());
            tbYear.Text = Convert.ToString(book.GetPublicationYear());
        }
        private void RefreshCollection()
        {
            lbBooks.Items.Clear();
            books = bookRepo.GetAllBooks();
            foreach (Book book in books)
            {
                lbBooks.Items.Add(book);
            }
        }
        private void lbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            Book selected = lbBooks.SelectedItem as Book;
            LoadBookInfo(selected);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReadOnlyTrue();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Book book = lbBooks.SelectedItem as Book;
            MessageBoxButtons.YesNo.HasFlag(MessageBoxButtons.YesNo);
        }
    }
}
