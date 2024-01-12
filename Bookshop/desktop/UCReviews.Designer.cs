namespace desktop
{
    partial class UCReviews
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
            lbReviews = new ListBox();
            btnSusreviews = new Button();
            btnGetRated = new Button();
            tbComment = new RichTextBox();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnPromote = new Button();
            label2 = new Label();
            lbLikes = new Label();
            lbUser = new Label();
            label4 = new Label();
            tbLikeThreshold = new TextBox();
            label1 = new Label();
            label3 = new Label();
            btnGetAll = new Button();
            groupBox1 = new GroupBox();
            btnDeleteUser = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lbReviews
            // 
            lbReviews.FormattingEnabled = true;
            lbReviews.ItemHeight = 20;
            lbReviews.Location = new Point(24, 29);
            lbReviews.Name = "lbReviews";
            lbReviews.Size = new Size(351, 384);
            lbReviews.TabIndex = 0;
            lbReviews.SelectedIndexChanged += lbReviews_SelectedIndexChanged;
            // 
            // btnSusreviews
            // 
            btnSusreviews.Location = new Point(381, 134);
            btnSusreviews.Name = "btnSusreviews";
            btnSusreviews.Size = new Size(178, 44);
            btnSusreviews.TabIndex = 2;
            btnSusreviews.Text = "Get reported reviews";
            btnSusreviews.UseVisualStyleBackColor = true;
            btnSusreviews.Click += btnSusreviews_Click;
            // 
            // btnGetRated
            // 
            btnGetRated.Location = new Point(381, 84);
            btnGetRated.Name = "btnGetRated";
            btnGetRated.Size = new Size(178, 44);
            btnGetRated.TabIndex = 3;
            btnGetRated.Text = "Get highly rated";
            btnGetRated.UseVisualStyleBackColor = true;
            btnGetRated.Click += btnGetRated_Click;
            // 
            // tbComment
            // 
            tbComment.Location = new Point(6, 126);
            tbComment.Name = "tbComment";
            tbComment.Size = new Size(465, 245);
            tbComment.TabIndex = 4;
            tbComment.Text = "";
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(267, 377);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 36);
            btnUpdate.TabIndex = 5;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(367, 377);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 36);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnPromote
            // 
            btnPromote.Location = new Point(192, 63);
            btnPromote.Name = "btnPromote";
            btnPromote.Size = new Size(137, 53);
            btnPromote.TabIndex = 7;
            btnPromote.Text = "Promote to a power user";
            btnPromote.UseVisualStyleBackColor = true;
            btnPromote.Click += btnPromote_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 103);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 8;
            label2.Text = "Comment:";
            // 
            // lbLikes
            // 
            lbLikes.AutoSize = true;
            lbLikes.Location = new Point(6, 63);
            lbLikes.Name = "lbLikes";
            lbLikes.Size = new Size(44, 20);
            lbLikes.TabIndex = 9;
            lbLikes.Text = "Likes:";
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.Location = new Point(6, 29);
            lbUser.Name = "lbUser";
            lbUser.Size = new Size(41, 20);
            lbUser.TabIndex = 10;
            lbUser.Text = "User:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 6);
            label4.Name = "label4";
            label4.Size = new Size(65, 20);
            label4.TabIndex = 11;
            label4.Text = "Reviews:";
            // 
            // tbLikeThreshold
            // 
            tbLikeThreshold.Location = new Point(205, 419);
            tbLikeThreshold.Name = "tbLikeThreshold";
            tbLikeThreshold.Size = new Size(81, 27);
            tbLikeThreshold.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 422);
            label1.Name = "label1";
            label1.Size = new Size(173, 20);
            label1.TabIndex = 13;
            label1.Text = "Highly rated starts from :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(292, 422);
            label3.Name = "label3";
            label3.Size = new Size(41, 20);
            label3.TabIndex = 14;
            label3.Text = "likes.";
            // 
            // btnGetAll
            // 
            btnGetAll.Location = new Point(381, 34);
            btnGetAll.Name = "btnGetAll";
            btnGetAll.Size = new Size(178, 44);
            btnGetAll.TabIndex = 15;
            btnGetAll.Text = "Get all";
            btnGetAll.UseVisualStyleBackColor = true;
            btnGetAll.Click += btnGetAll_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnDeleteUser);
            groupBox1.Controls.Add(tbComment);
            groupBox1.Controls.Add(btnUpdate);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(btnPromote);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(lbLikes);
            groupBox1.Controls.Add(lbUser);
            groupBox1.Location = new Point(579, 17);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(668, 429);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Selected review";
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Location = new Point(334, 63);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(137, 53);
            btnDeleteUser.TabIndex = 11;
            btnDeleteUser.Text = "Delete user account";
            btnDeleteUser.UseVisualStyleBackColor = true;
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // UCReviews
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Controls.Add(btnGetAll);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(tbLikeThreshold);
            Controls.Add(label4);
            Controls.Add(btnGetRated);
            Controls.Add(btnSusreviews);
            Controls.Add(lbReviews);
            Name = "UCReviews";
            Size = new Size(1505, 463);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lbReviews;
        private Button btnSusreviews;
        private Button btnGetRated;
        private RichTextBox tbComment;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnPromote;
        private Label label2;
        private Label lbLikes;
        private Label lbUser;
        private Label label4;
        private TextBox tbLikeThreshold;
        private Label label1;
        private Label label3;
        private Button btnGetAll;
        private GroupBox groupBox1;
        private Button btnDeleteUser;
    }
}
