using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        Book currentlySelected;
        BookManagement _bookManager;
        ValidationContext validationContext;
        public UCBooks(IBookRepository bookRepo, IReviewRepository revRepo)
        {
            _bookManager = new BookManagement(bookRepo, revRepo);
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

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            Book book = null;
            List<Author> authorList = new List<Author>
                {
                    cbAuthors.SelectedItem as Author
                };
            List<string> errorMessages = new List<string>();
            if (rdbPaperBook.Checked)
            {
                try
                {
                    book = new PaperBook()
                    {
                        Title = tbTitle.Text,
                        Description = tbDescription.Text,
                        Publisher = tbPublisher.Text,
                        Format = (Format)cbFormat.SelectedItem,
                        PublicationDate = pubDatePicker.Value,
                        Language = tbLanguage.Text,
                        Authors = authorList,
                        Pages = Convert.ToInt32(tbPages.Text),
                        ISBN = tbISBN.Text,
                        ISBN10 = tbISBN10.Text,
                    };
                }
                catch (Exception arg)
                {
                    errorMessages.Add(arg.Message);
                }

            }
            else if (rdbEBook.Checked)
            {
                try
                {
                    book = new EBook()
                    {
                        Title = tbTitle.Text,
                        Description = tbDescription.Text,
                        Publisher = tbPublisher.Text,
                        Format = (Format)cbFormat.SelectedItem,
                        PublicationDate = pubDatePicker.Value,
                        Language = tbLanguage.Text,
                        Authors = authorList,
                        FileSize = Convert.ToDouble(tbFileSize.Text),
                        DownloadLink = tbLink.Text,
                    };
                }
                catch (Exception arg)
                {

                    errorMessages.Add(arg.Message);
                }

            }
            if (errorMessages.Count > 0)
            {
                string errorMessage = string.Join(Environment.NewLine, errorMessages);
                MessageBox.Show(errorMessage, "Input Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (book != null)
            {
                switch (action)
                {
                    case "add":

                        bool success = _bookManager.AddNewBook(book);
                        ReadOnlyTrue();
                        HideButtons();
                        if (success)
                        {
                            MessageBox.Show("book succesfully added.", "update");
                            RefreshCollection();
                            EnableButtons();
                            action = "";
                        }
                        else
                        {
                            MessageBox.Show("problem with adding the book occured.", "update");
                        }
                        break;
                    case "update":

                        success = _bookManager.UpdateBook(book);
                        ReadOnlyTrue();
                        HideButtons();
                        if (success) { MessageBox.Show("book succesfully updated.", "update"); RefreshCollection(); action = ""; RefreshCollection();
                            EnableButtons();
                        }
                        else
                        {
                            MessageBox.Show("problem with updating the book occured.", "update");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void lbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (action != "add" || action != "update")
            {
                ReadOnlyTrue();
                HideButtons();
            }
            currentlySelected = lbBooks.SelectedItem as Book;
            LoadBookInfo(currentlySelected);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReadOnlyTrue();
            HideButtons();
            EnableButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Book book = lbBooks.SelectedItem as Book;
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Do you really want to delete this book?", "Delete", buttons);
            if (result == DialogResult.Yes)
            {
                _bookManager.DeleteBook(book);
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
            cbAuthors.Enabled = true;
            cbAuthors.SelectedItem = null;
            tbLanguage.ReadOnly = false;
            tbLanguage.Clear();
            tbPublisher.ReadOnly = false;
            tbPublisher.Clear();
            pubDatePicker.Enabled = true;
            cbFormat.Enabled = true;
            tbPages.ReadOnly = false;
            tbPages.Clear();
            tbDescription.ReadOnly = false;
            tbDescription.Clear();
            tbPages.Clear();
            tbISBN.Clear();
            tbISBN10.Clear();
            tbLink.Clear();
            tbFileSize.Clear();
            groupBox2.BackColor = default;
            groupBox3.BackColor = default;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnConfirm.Visible = true;
            btnCancel.Visible = true;
            rdbPaperBook.Visible = true;
            rdbEBook.Visible = true;
            action = "add";
        }

        private void ReadOnlyTrue()
        {
            tbTitle.ReadOnly = true;
            cbAuthors.Enabled = false;
            tbLanguage.ReadOnly = true;
            tbPublisher.ReadOnly = true;

            pubDatePicker.Enabled = false;
            tbPages.ReadOnly = true;
            cbFormat.Enabled = false;
            tbDescription.ReadOnly = true;
        }
        private void LoadBookInfo(Book book)
        {
            tbTitle.Text = book.Title;
            tbDescription.Text = book.Description;
            cbAuthors.Text = string.Join(',', book.Authors);
            tbLanguage.Text = book.Language;
            tbPublisher.Text = book.Publisher;
            pubDatePicker.Value = Convert.ToDateTime(book.PublicationDate);
            cbFormat.SelectedItem = book.Format;
            if (book.GetType() == typeof(PaperBook))
            {
                tbFileSize.Clear();
                tbLink.Clear();
                groupBox3.BackColor = default;
                var paperBook = (PaperBook)book;
                tbISBN.Text = paperBook.ISBN;
                tbISBN10.Text = paperBook.ISBN10;
                tbPages.Text = Convert.ToString(paperBook.Pages);
            }
            else if (book.GetType() == typeof(EBook))
            {
                tbISBN.Clear();
                tbISBN10.Clear();
                tbPages.Clear();
                groupBox2.BackColor = default;
                var ebook = (EBook)book;
                tbFileSize.Text = Convert.ToString(ebook.FileSize);
                tbLink.Text = ebook.DownloadLink;
            }

        }
        private void RefreshCollection()
        {
            lbBooks.Items.Clear();
            _bookManager.RefreshCollection();
            foreach (Book book in _bookManager.GetAllBooks())
            {
                lbBooks.Items.Add(book);
            }
        }
        private void UpdateFormControl()
        {
            tbTitle.ReadOnly = false;

            cbAuthors.Enabled = true;
            tbLanguage.ReadOnly = false;
            tbPublisher.ReadOnly = false;
            pubDatePicker.Enabled = true;
            cbFormat.Enabled = true;
            tbPages.ReadOnly = false;

            tbDescription.ReadOnly = false;
            groupBox2.BackColor = default;
            groupBox3.BackColor = default;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnConfirm.Visible = true;
            btnCancel.Visible = true;
            rdbPaperBook.Visible = true;
            rdbEBook.Visible = true;
            action = "update";
        }
        private void HideButtons()
        {
            btnConfirm.Visible = false;
            btnCancel.Visible = false;
            rdbEBook.Visible = false;
            rdbPaperBook.Visible = false;
        }
        private void EnableButtons()
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void UCBooks_Load(object sender, EventArgs e)
        {
            HideButtons();
            List<Author> authorList = _bookManager.GetAllAuthors();
            foreach (var author in authorList)
            {
                cbAuthors.Items.Add(author);
            }
            List<Format> formats = _bookManager.GetAllFormats();
            foreach (var format in formats)
            {
                cbFormat.Items.Add(format);
            }
        }

        private void rdbPaperBook_CheckedChanged(object sender, EventArgs e)
        {
            rdbEBook.Checked = false;
            tbPages.ReadOnly = false;
            tbPages.Clear();
            tbISBN.ReadOnly = false;
            tbISBN.Clear();
            tbISBN10.ReadOnly = false;
            tbISBN10.Clear();

            tbFileSize.ReadOnly = true;
            tbLink.ReadOnly = true;
        }

        private void rdbEBook_CheckedChanged(object sender, EventArgs e)
        {
            rdbPaperBook.Checked = false;
            tbFileSize.ReadOnly = false;
            tbFileSize.Clear();
            tbLink.ReadOnly = false;
            tbLink.Clear();

            tbPages.ReadOnly = true;
            tbISBN.ReadOnly = true;
            tbISBN10.ReadOnly = true;

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            UpdateFormControl();
        }
    }
}
