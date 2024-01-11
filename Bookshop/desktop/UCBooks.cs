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
        BookManager _bookManager;
        ValidationContext validationContext;
        public UCBooks(IBookRepository bookRepo, IReviewRepository revRepo)
        {
            _bookManager = new BookManager(bookRepo, revRepo);
            InitializeComponent();
        }

        private void tbnShowAll_Click(object sender, EventArgs e)
        {
            RefreshCollection();
            HideFormSubmitButtons();
            EnableButtons();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //title
            tbTitle.ReadOnly = false;
            tbTitle.Clear();
            //author
            cbAuthors.Enabled = true;
            cbAuthors.SelectedItem = null;
            //language
            tbLanguage.ReadOnly = false;
            tbLanguage.Clear();
            //publisher
            tbPublisher.ReadOnly = false;
            tbPublisher.Clear();
            //date
            pubDatePicker.Enabled = true;
            //format
            cbFormat.Enabled = true;
            cbFormat.SelectedItem = null;
            //description
            tbDescription.ReadOnly = false;
            tbDescription.Clear();
            //paperboook
            tbPages.Clear();
            tbISBN.Clear();
            tbISBN10.Clear();
            //ebook
            tbFileSize.Clear();
            tbLink.Clear();
            // button disablings
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnConfirm.Visible = true;
            btnCancel.Visible = true;
            //action
            action = "add";
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            Book book = null;
            List<string> errorMessages = new List<string>();
            if (cbAuthors.SelectedItem == null)
            {
                errorMessages.Add("Select an author");
            }
            if (cbFormat.SelectedItem == null)
            {
                MessageBox.Show("Select a format");
                return;
            }
            switch (action)
            {
                case "add":

                    if ((Format)cbFormat.SelectedItem == Format.PAPERBOOK || (Format)cbFormat.SelectedItem == Format.PAPERBACK || (Format)cbFormat.SelectedItem == Format.HARDCOVER)
                    {
                        //for paperbook
                        try
                        {
                            List<Author> authorList = new List<Author>() { cbAuthors.SelectedItem as Author };
                            book = new PaperBook()
                            {
                                Title = tbTitle.Text,
                                Description = tbDescription.Text,
                                Publisher = tbPublisher.Text,
                                Format = (Format)cbFormat.SelectedItem,
                                PublicationDate = pubDatePicker.Value,
                                Language = tbLanguage.Text,
                                Authors = authorList,
                                Pages = Convert.ToInt32(int.TryParse(tbPages.Text, out int result)),
                                ISBN = tbISBN.Text,
                                ISBN10 = tbISBN10.Text,
                            };
                        }
                        catch (Exception arg)
                        {
                            errorMessages.Add(arg.Message);
                        }

                    }
                    else
                    {
                        //ebook
                        try
                        {
                            List<Author> authorList = new List<Author>() { cbAuthors.SelectedItem as Author };
                            book = new EBook()
                            {
                                Title = tbTitle.Text,
                                Description = tbDescription.Text,
                                Publisher = tbPublisher.Text,
                                Format = (Format)cbFormat.SelectedItem,
                                PublicationDate = pubDatePicker.Value,
                                Language = tbLanguage.Text,
                                Authors = authorList,
                                FileSize = Convert.ToDouble(double.TryParse(tbFileSize.Text,out double value)),
                                DownloadLink = tbLink.Text,
                            };
                        }
                        catch (Exception arg)
                        {
                            errorMessages.Add(arg.Message);
                        }

                    }

                    //input validation
                    if (errorMessages.Count > 0)
                    {
                        string errorMessage = string.Join(Environment.NewLine, errorMessages);
                        MessageBox.Show(errorMessage, "Input Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        bool success = _bookManager.AddNewBook(book);
                        ReadOnlyTrue();
                        HideFormSubmitButtons();
                        if (success)
                        {
                            MessageBox.Show("book succesfully added.", "Book additions");
                            RefreshCollection(); action = "";
                            EnableButtons();
                        }
                        else
                        {
                            MessageBox.Show("problem with adding the book occured.", "Book additions");
                        }
                    }

                    break;
                case "update":

                    if (currentlySelected.Format == Format.EBOOK)
                    {
                        try
                        {
                            List<Author> authorList = new List<Author>() { cbAuthors.SelectedItem as Author };
                            book = new EBook(currentlySelected.Id, tbTitle.Text, tbDescription.Text, tbPublisher.Text, tbLanguage.Text, pubDatePicker.Value, (Format)cbFormat.SelectedItem, authorList, Convert.ToDouble(tbFileSize.Text), tbLink.Text);
                        }
                        catch (Exception arg)
                        {
                            errorMessages.Add(arg.Message);
                        }
                    }
                    else if (currentlySelected.Format == Format.PAPERBOOK || currentlySelected.Format == Format.HARDCOVER || currentlySelected.Format == Format.PAPERBACK)
                    {
                        try
                        {
                            List<Author> authorList = new List<Author>() { cbAuthors.SelectedItem as Author };
                            book = new PaperBook(currentlySelected.Id, tbTitle.Text, tbDescription.Text, tbPublisher.Text, tbLanguage.Text, pubDatePicker.Value, (Format)cbFormat.SelectedItem, authorList, Convert.ToInt32(tbPages.Text), tbISBN.Text, tbISBN10.Text);
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
                    else
                    {
                        bool success = _bookManager.UpdateBook(book);
                        ReadOnlyTrue();
                        HideFormSubmitButtons();
                        if (success)
                        {
                            MessageBox.Show("book succesfully updated.", "update");
                            RefreshCollection(); action = "";
                            EnableButtons();
                        }
                        else
                        {
                            MessageBox.Show("problem with updating the book occured.", "update");
                        }

                    }

                    break;

            }
        }

        private void lbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (action != "add" || action != "update")
            {
                ReadOnlyTrue();
                EnableButtons();
                HideFormSubmitButtons();
            }
            try
            {
                currentlySelected = lbBooks.SelectedItem as Book;
                LoadBookInfo(currentlySelected);
            }
            catch (Exception)
            {

            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReadOnlyTrue();
            HideFormSubmitButtons();
            EnableButtons();
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
                var ebook = (EBook)book;
                tbFileSize.Text = Convert.ToString(ebook.FileSize);
                tbLink.Text = ebook.DownloadLink;
            }

        }
        private void RefreshCollection()
        {
            lbBooks.Items.Clear();
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
            tbPages.ReadOnly = false;

            tbDescription.ReadOnly = false;
            cbFormat.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnConfirm.Visible = true;
            btnCancel.Visible = true;
            action = "update";
        }
        private void HideFormSubmitButtons()
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
            HideFormSubmitButtons();
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

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (currentlySelected == null)
            {
                MessageBox.Show("Select an item first");
            }
            else
            {
                UpdateFormControl();
            }
        }
        private void lbDanger_Click(object sender, EventArgs e)
        {
            lbDanger.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Book book;
            if (lbBooks.SelectedItem != null)
            {
                try
                {
                    book = lbBooks.SelectedItem as Book;
                    DialogResult result = MessageBox.Show("Do you really want to delete this book?", "Delete", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        _bookManager.DeleteBook(book);
                        RefreshCollection();
                    }
                    else { }
                }
                catch (Exception)
                {
                    lbDanger.Visible = true;
                    lbDanger.Text = "Select an item from the list.";
                }


            }
        }

        private void cbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cbFormat.SelectedItem == null))
            {
                if ((Format)cbFormat.SelectedItem == Format.EBOOK)
                {
                    tbPages.ReadOnly = true;
                    tbPages.Clear();
                    tbISBN.ReadOnly = true;
                    tbISBN.Clear();
                    tbISBN10.ReadOnly = true;
                    tbISBN10.Clear();
                    tbFileSize.ReadOnly = false;
                    tbFileSize.Clear();
                    tbLink.ReadOnly = false;
                    tbLink.Clear();
                }
                else if ((Format)cbFormat.SelectedItem == Format.PAPERBOOK || (Format)cbFormat.SelectedItem == Format.PAPERBACK || (Format)cbFormat.SelectedItem == Format.HARDCOVER)
                {
                    tbFileSize.ReadOnly = true;
                    tbFileSize.Clear();
                    tbLink.ReadOnly = true;
                    tbLink.Clear();
                    tbPages.ReadOnly = false;
                    tbPages.Clear();
                    tbISBN.ReadOnly = false;
                    tbISBN.Clear();
                    tbISBN10.ReadOnly = false;
                    tbISBN10.Clear();
                }
            }
        }
    }
}
