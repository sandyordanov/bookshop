namespace desktop
{
    partial class UCBooks
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox2 = new GroupBox();
            rdbPaperBook = new RadioButton();
            label12 = new Label();
            tbISBN10 = new TextBox();
            label11 = new Label();
            tbISBN = new TextBox();
            label6 = new Label();
            tbPages = new TextBox();
            label7 = new Label();
            tbDescription = new TextBox();
            label1 = new Label();
            label5 = new Label();
            label4 = new Label();
            tbLanguage = new TextBox();
            tbPublisher = new TextBox();
            groupBox1 = new GroupBox();
            cbFormat = new ComboBox();
            pubDatePicker = new DateTimePicker();
            cbAuthors = new ComboBox();
            label9 = new Label();
            label3 = new Label();
            label2 = new Label();
            tbTitle = new TextBox();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            tbnShowAll = new Button();
            lbBooks = new ListBox();
            btnConfirm = new Button();
            btnCancel = new Button();
            pictureBox1 = new PictureBox();
            groupBox3 = new GroupBox();
            rdbEBook = new RadioButton();
            label8 = new Label();
            tbLink = new TextBox();
            label10 = new Label();
            tbFileSize = new TextBox();
            d = new GroupBox();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox3.SuspendLayout();
            d.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rdbPaperBook);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(tbISBN10);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(tbISBN);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(tbPages);
            groupBox2.Location = new Point(6, 16);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(332, 175);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = "Paperbook specific";
            // 
            // rdbPaperBook
            // 
            rdbPaperBook.AutoSize = true;
            rdbPaperBook.Location = new Point(309, 16);
            rdbPaperBook.Name = "rdbPaperBook";
            rdbPaperBook.Size = new Size(17, 16);
            rdbPaperBook.TabIndex = 22;
            rdbPaperBook.TabStop = true;
            rdbPaperBook.UseVisualStyleBackColor = true;
            rdbPaperBook.CheckedChanged += rdbPaperBook_CheckedChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(10, 114);
            label12.Name = "label12";
            label12.Size = new Size(72, 20);
            label12.TabIndex = 12;
            label12.Text = "ISBN10:";
            // 
            // tbISBN10
            // 
            tbISBN10.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            tbISBN10.Location = new Point(117, 111);
            tbISBN10.Name = "tbISBN10";
            tbISBN10.ReadOnly = true;
            tbISBN10.Size = new Size(194, 28);
            tbISBN10.TabIndex = 13;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(10, 80);
            label11.Name = "label11";
            label11.Size = new Size(54, 20);
            label11.TabIndex = 10;
            label11.Text = "ISBN:";
            // 
            // tbISBN
            // 
            tbISBN.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            tbISBN.Location = new Point(117, 77);
            tbISBN.Name = "tbISBN";
            tbISBN.ReadOnly = true;
            tbISBN.Size = new Size(194, 28);
            tbISBN.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(10, 46);
            label6.Name = "label6";
            label6.Size = new Size(101, 20);
            label6.TabIndex = 3;
            label6.Text = "Nr. of pages:";
            // 
            // tbPages
            // 
            tbPages.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            tbPages.Location = new Point(117, 43);
            tbPages.Name = "tbPages";
            tbPages.ReadOnly = true;
            tbPages.Size = new Size(194, 28);
            tbPages.TabIndex = 9;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(6, 162);
            label7.Name = "label7";
            label7.Size = new Size(128, 20);
            label7.TabIndex = 20;
            label7.Text = "Publication date:";
            // 
            // tbDescription
            // 
            tbDescription.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            tbDescription.Location = new Point(580, 269);
            tbDescription.Multiline = true;
            tbDescription.Name = "tbDescription";
            tbDescription.ReadOnly = true;
            tbDescription.Size = new Size(568, 179);
            tbDescription.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(580, 246);
            label1.Name = "label1";
            label1.Size = new Size(95, 20);
            label1.TabIndex = 18;
            label1.Text = "Description:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(6, 128);
            label5.Name = "label5";
            label5.Size = new Size(80, 20);
            label5.TabIndex = 8;
            label5.Text = "Publisher:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(6, 94);
            label4.Name = "label4";
            label4.Size = new Size(82, 20);
            label4.TabIndex = 7;
            label4.Text = "Language:";
            // 
            // tbLanguage
            // 
            tbLanguage.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            tbLanguage.Location = new Point(96, 91);
            tbLanguage.Name = "tbLanguage";
            tbLanguage.ReadOnly = true;
            tbLanguage.Size = new Size(211, 28);
            tbLanguage.TabIndex = 5;
            // 
            // tbPublisher
            // 
            tbPublisher.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            tbPublisher.Location = new Point(96, 125);
            tbPublisher.Name = "tbPublisher";
            tbPublisher.ReadOnly = true;
            tbPublisher.Size = new Size(211, 28);
            tbPublisher.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cbFormat);
            groupBox1.Controls.Add(pubDatePicker);
            groupBox1.Controls.Add(cbAuthors);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tbTitle);
            groupBox1.Controls.Add(tbPublisher);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(tbLanguage);
            groupBox1.Location = new Point(772, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(376, 240);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Book Information";
            // 
            // cbFormat
            // 
            cbFormat.BackColor = SystemColors.Menu;
            cbFormat.FormattingEnabled = true;
            cbFormat.Location = new Point(96, 193);
            cbFormat.Name = "cbFormat";
            cbFormat.Size = new Size(151, 28);
            cbFormat.TabIndex = 16;
            // 
            // pubDatePicker
            // 
            pubDatePicker.CalendarMonthBackground = SystemColors.Menu;
            pubDatePicker.Format = DateTimePickerFormat.Short;
            pubDatePicker.Location = new Point(140, 159);
            pubDatePicker.Name = "pubDatePicker";
            pubDatePicker.Size = new Size(230, 27);
            pubDatePicker.TabIndex = 23;
            pubDatePicker.Value = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // cbAuthors
            // 
            cbAuthors.BackColor = SystemColors.Menu;
            cbAuthors.ForeColor = SystemColors.WindowText;
            cbAuthors.FormattingEnabled = true;
            cbAuthors.Location = new Point(96, 57);
            cbAuthors.Name = "cbAuthors";
            cbAuthors.RightToLeft = RightToLeft.No;
            cbAuthors.Size = new Size(151, 28);
            cbAuthors.TabIndex = 22;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(6, 196);
            label9.Name = "label9";
            label9.Size = new Size(65, 20);
            label9.TabIndex = 21;
            label9.Text = "Format:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(6, 60);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 6;
            label3.Text = "Author:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(6, 23);
            label2.Name = "label2";
            label2.Size = new Size(45, 20);
            label2.TabIndex = 5;
            label2.Text = "Title:";
            // 
            // tbTitle
            // 
            tbTitle.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            tbTitle.Location = new Point(96, 23);
            tbTitle.Name = "tbTitle";
            tbTitle.ReadOnly = true;
            tbTitle.Size = new Size(274, 28);
            tbTitle.TabIndex = 3;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(441, 410);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(133, 50);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(302, 410);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(133, 50);
            btnUpdate.TabIndex = 14;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click_1;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(142, 410);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(133, 50);
            btnAdd.TabIndex = 13;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // tbnShowAll
            // 
            tbnShowAll.Location = new Point(3, 410);
            tbnShowAll.Name = "tbnShowAll";
            tbnShowAll.Size = new Size(133, 50);
            tbnShowAll.TabIndex = 12;
            tbnShowAll.Text = "Show all";
            tbnShowAll.UseVisualStyleBackColor = true;
            tbnShowAll.Click += tbnShowAll_Click;
            // 
            // lbBooks
            // 
            lbBooks.BackColor = SystemColors.MenuBar;
            lbBooks.FormattingEnabled = true;
            lbBooks.ItemHeight = 20;
            lbBooks.Location = new Point(3, 3);
            lbBooks.Name = "lbBooks";
            lbBooks.Size = new Size(571, 404);
            lbBooks.TabIndex = 11;
            lbBooks.SelectedIndexChanged += lbBooks_SelectedIndexChanged;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(1174, 398);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(154, 50);
            btnConfirm.TabIndex = 18;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Visible = false;
            btnConfirm.Click += btnConfirm_Click_1;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(1334, 398);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(154, 50);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Visible = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(580, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(186, 240);
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(rdbEBook);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(tbLink);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(tbFileSize);
            groupBox3.Location = new Point(6, 197);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(332, 167);
            groupBox3.TabIndex = 21;
            groupBox3.TabStop = false;
            groupBox3.Text = "E-Book specific";
            // 
            // rdbEBook
            // 
            rdbEBook.AutoSize = true;
            rdbEBook.Location = new Point(309, 21);
            rdbEBook.Name = "rdbEBook";
            rdbEBook.Size = new Size(17, 16);
            rdbEBook.TabIndex = 23;
            rdbEBook.TabStop = true;
            rdbEBook.UseVisualStyleBackColor = true;
            rdbEBook.CheckedChanged += rdbEBook_CheckedChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(11, 81);
            label8.Name = "label8";
            label8.Size = new Size(118, 20);
            label8.TabIndex = 10;
            label8.Text = "Download link:";
            // 
            // tbLink
            // 
            tbLink.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            tbLink.Location = new Point(135, 77);
            tbLink.Name = "tbLink";
            tbLink.ReadOnly = true;
            tbLink.Size = new Size(176, 28);
            tbLink.TabIndex = 11;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(10, 46);
            label10.Name = "label10";
            label10.Size = new Size(71, 20);
            label10.TabIndex = 3;
            label10.Text = "File size:";
            // 
            // tbFileSize
            // 
            tbFileSize.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            tbFileSize.Location = new Point(87, 43);
            tbFileSize.Name = "tbFileSize";
            tbFileSize.ReadOnly = true;
            tbFileSize.Size = new Size(194, 28);
            tbFileSize.TabIndex = 9;
            // 
            // d
            // 
            d.Controls.Add(groupBox3);
            d.Controls.Add(groupBox2);
            d.FlatStyle = FlatStyle.Flat;
            d.Location = new Point(1154, 26);
            d.Name = "d";
            d.Size = new Size(344, 366);
            d.TabIndex = 22;
            d.TabStop = false;
            // 
            // UCBooks
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(d);
            Controls.Add(pictureBox1);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(tbDescription);
            Controls.Add(groupBox1);
            Controls.Add(btnDelete);
            Controls.Add(label1);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(tbnShowAll);
            Controls.Add(lbBooks);
            Name = "UCBooks";
            Size = new Size(1505, 463);
            Load += UCBooks_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            d.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox2;
        private TextBox tbPages;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox tbLanguage;
        private TextBox tbPublisher;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private TextBox tbTitle;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private Button tbnShowAll;
        private ListBox lbBooks;
        private TextBox tbDescription;
        private Label label1;
        private Label label7;
        private Button btnConfirm;
        private Button btnCancel;
        private PictureBox pictureBox1;
        private Label label9;
        private Label label12;
        private TextBox tbISBN10;
        private Label label11;
        private TextBox tbISBN;
        private GroupBox groupBox3;
        private Label label10;
        private TextBox tbFileSize;
        private Label label8;
        private TextBox tbLink;
        private ComboBox cbAuthors;
        private DateTimePicker pubDatePicker;
        private ComboBox cbFormat;
        private RadioButton rdbPaperBook;
        private GroupBox d;
        private RadioButton rdbEBook;
    }
}
