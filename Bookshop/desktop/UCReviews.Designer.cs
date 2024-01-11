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
            SuspendLayout();
            // 
            // lbReviews
            // 
            lbReviews.FormattingEnabled = true;
            lbReviews.ItemHeight = 20;
            lbReviews.Location = new Point(24, 29);
            lbReviews.Name = "lbReviews";
            lbReviews.Size = new Size(351, 344);
            lbReviews.TabIndex = 0;
            lbReviews.SelectedIndexChanged += lbReviews_SelectedIndexChanged;
            // 
            // btnSusreviews
            // 
            btnSusreviews.Location = new Point(24, 379);
            btnSusreviews.Name = "btnSusreviews";
            btnSusreviews.Size = new Size(178, 44);
            btnSusreviews.TabIndex = 2;
            btnSusreviews.Text = "Get reported reviews";
            btnSusreviews.UseVisualStyleBackColor = true;
            btnSusreviews.Click += btnSusreviews_Click;
            // 
            // btnGetRated
            // 
            btnGetRated.Location = new Point(197, 379);
            btnGetRated.Name = "btnGetRated";
            btnGetRated.Size = new Size(178, 44);
            btnGetRated.TabIndex = 3;
            btnGetRated.Text = "Get highly rated";
            btnGetRated.UseVisualStyleBackColor = true;
            btnGetRated.Click += btnGetRated_Click;
            // 
            // tbComment
            // 
            tbComment.Location = new Point(423, 137);
            tbComment.Name = "tbComment";
            tbComment.Size = new Size(465, 245);
            tbComment.TabIndex = 4;
            tbComment.Text = "";
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(692, 388);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 36);
            btnUpdate.TabIndex = 5;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(792, 388);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 36);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnPromote
            // 
            btnPromote.Location = new Point(748, 55);
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
            label2.Location = new Point(423, 114);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 8;
            label2.Text = "Comment:";
            // 
            // lbLikes
            // 
            lbLikes.AutoSize = true;
            lbLikes.Location = new Point(423, 88);
            lbLikes.Name = "lbLikes";
            lbLikes.Size = new Size(44, 20);
            lbLikes.TabIndex = 9;
            lbLikes.Text = "Likes:";
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.Location = new Point(423, 55);
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
            tbLikeThreshold.Location = new Point(197, 429);
            tbLikeThreshold.Name = "tbLikeThreshold";
            tbLikeThreshold.Size = new Size(81, 27);
            tbLikeThreshold.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 432);
            label1.Name = "label1";
            label1.Size = new Size(173, 20);
            label1.TabIndex = 13;
            label1.Text = "Highly rated starts from :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(284, 432);
            label3.Name = "label3";
            label3.Size = new Size(41, 20);
            label3.TabIndex = 14;
            label3.Text = "likes.";
            // 
            // UCReviews
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(tbLikeThreshold);
            Controls.Add(label4);
            Controls.Add(lbUser);
            Controls.Add(lbLikes);
            Controls.Add(label2);
            Controls.Add(btnPromote);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(tbComment);
            Controls.Add(btnGetRated);
            Controls.Add(btnSusreviews);
            Controls.Add(lbReviews);
            Name = "UCReviews";
            Size = new Size(1505, 463);
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
    }
}
