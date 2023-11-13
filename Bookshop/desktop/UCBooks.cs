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
        PaperBook selected;
        List<PaperBook> books;
        private IBookRepository bookRepo;

        public UCBooks(IBookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
            books = new List<PaperBook>();
            InitializeComponent();
        }

        private void tbnShowAll_Click(object sender, EventArgs e)
        {
            RefreshCollection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddFormControl();
            btnDelete.Enabled = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            PaperBook book = new PaperBook(0, tbTitle.Text, Convert.ToDouble(tbPrice.Text), "", tbAuthor.Text, tbLanguage.Text, tbPublisher.Text, Convert.ToInt32(tbPages.Text), tbISBN.Text, Convert.ToInt32(tbYear.Text), 1,0);
            switch (action)
            {
                case "add":

                    bool success = bookRepo.AddBook(book);
                    ReadOnlyTrue();
                    HideButtons();
                    if (success) { MessageBox.Show("Book succesfully added.", "Update"); RefreshCollection(); }
                    else
                    {
                        MessageBox.Show("Problem with adding the book occured.", "Update");
                    }
                    break;
                case "update":
                    
                    success = bookRepo.UpdateBook(book);
                    ReadOnlyTrue();
                    HideButtons();
                    if (success) { MessageBox.Show("Book succesfully updated.", "Update"); RefreshCollection(); }
                    else
                    {
                        MessageBox.Show("Problem with updating the book occured.", "Update");
                    }
                    break;
                default:
                    break;
            }

        }


        private void lbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (action != "add" || action != "update")
            {
                ReadOnlyTrue();
                HideButtons();
            }
            selected = lbBooks.SelectedItem as PaperBook;
            LoadBookInfo(selected);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReadOnlyTrue();
            HideButtons();
            EnableButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            PaperBook book = lbBooks.SelectedItem as PaperBook;
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Do you really want to delete this book?", "Delete", buttons);
            if (result == DialogResult.Yes)
            {
                bookRepo.DeleteBook(book);
                RefreshCollection();
            }
            else
            {

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateFormControl();
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
        private void LoadBookInfo(PaperBook book)
        {
            tbTitle.Text = book.Title;
            tbAuthor.Text = book.Author;
            tbLanguage.Text = book.Language;
            tbPublisher.Text = book.GetPublisher();


            tbPages.Text = Convert.ToString(book.GetPages());
            tbISBN.Text = book.GetISBN();
            tbYear.Text = Convert.ToString(book.GetPublicationYear());
        }
        private void RefreshCollection()
        {
            lbBooks.Items.Clear();
            books = bookRepo.GetAllBooks();
            foreach (PaperBook book in books)
            {
                lbBooks.Items.Add(book);
            }
        }
        private void UpdateFormControl()
        {
            tbTitle.ReadOnly = false;

            tbAuthor.ReadOnly = false;

            tbLanguage.ReadOnly = false;

            tbPublisher.ReadOnly = false;


            tbPages.ReadOnly = false;

            tbISBN.ReadOnly = false;

            tbPrice.ReadOnly = false;

            tbYear.ReadOnly = false;


            btnConfirm.Visible = true;
            btnCancel.Visible = true;
            action = "update";
        }
        private void HideButtons()
        {
            btnConfirm.Visible = false;
            btnCancel.Visible = false;
        }
        private void EnableButtons()
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }
}
