using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
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
        Book currentlySelected;
        BookManagement _bookManager;

        public UCBooks(IBookRepository bookRepo, IReviewRepository revRepo)
        {
            _bookManager = new BookManagement(bookRepo,revRepo);
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

        private void btnConfirm_click(object sender, EventArgs e)
        {
            Book book;
            if (cbPaper.Checked == true)
            {
                List<Author> authorList = new List<Author>
                {
                    cbAuthors.SelectedItem as Author
                };
                book = new PaperBook(
                    0,
                    tbTitle.Text,
                    tbDescription.Text,
                    tbPublisher.Text,
                    tbLanguage.Text,
                    pubDatePicker.Value,
                    (Format)cbFormat.SelectedValue,
                    authorList,
                    Convert.ToInt32(tbPages.Text),
                    tbISBN.Text,
                    tbISBN10.Text);
            }
            else if (cbEbook.Checked == true)
            {
                List<Author> authorList = new List<Author>
                {
                    cbAuthors.SelectedItem as Author
                };
                book = new EBook(0,
                    tbTitle.Text,
                    tbDescription.Text,
                    tbPublisher.Text,
                    tbLanguage.Text,
                    pubDatePicker.Value,
                    (Format)cbFormat.SelectedValue,
                    authorList,
                    Convert.ToDouble(tbFileSize.Text), tbLink.Text
                    );
            }
            else
            {
                throw new Exception();
            }

            switch (action)
            {
                case "add":

                    bool success = _bookManager.AddNewBook(book);
                    ReadOnlyTrue();
                    HideButtons();
                    if (success) { MessageBox.Show("book succesfully added.", "update"); RefreshCollection(); }
                    else
                    {
                        MessageBox.Show("problem with adding the book occured.", "update");
                    }
                    break;
                case "update":

                    success = _bookManager.AddNewBook(book);
                    ReadOnlyTrue();
                    HideButtons();
                    if (success) { MessageBox.Show("book succesfully updated.", "update"); RefreshCollection(); }
                    else
                    {
                        MessageBox.Show("problem with updating the book occured.", "update");
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
            tbLanguage.ReadOnly = false;
            tbLanguage.Clear();
            tbPublisher.ReadOnly = false;
            tbPublisher.Clear();

            tbPages.ReadOnly = false;
            tbPages.Clear();
            tbDescription.ReadOnly = false;
            tbDescription.Clear();

            btnConfirm.Visible = true;
            btnCancel.Visible = true;
            action = "add";
        }

        private void ReadOnlyTrue()
        {
            tbTitle.ReadOnly = true;

            tbLanguage.ReadOnly = true;
            tbPublisher.ReadOnly = true;


            tbPages.ReadOnly = true;

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
                var paperBook = (PaperBook)book;
                tbISBN.Text = paperBook.ISBN;
                tbISBN10.Text = paperBook.ISBN10;
                tbPages.Text = Convert.ToString(paperBook.Pages);
            }
            else if (book.GetType() == typeof(EBook))
            {
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



            tbLanguage.ReadOnly = false;

            tbPublisher.ReadOnly = false;


            tbPages.ReadOnly = false;

            tbDescription.ReadOnly = false;

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

        private void UCBooks_Load(object sender, EventArgs e)
        {
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
    }
}
