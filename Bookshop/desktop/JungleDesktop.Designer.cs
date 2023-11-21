namespace desktop
{
    partial class JungleDesktop
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            addBooksToolStripMenuItem = new ToolStripMenuItem();
            reviewsToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ActiveCaption;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { addBooksToolStripMenuItem, reviewsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1504, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // addBooksToolStripMenuItem
            // 
            addBooksToolStripMenuItem.Name = "addBooksToolStripMenuItem";
            addBooksToolStripMenuItem.Size = new Size(63, 24);
            addBooksToolStripMenuItem.Text = "Books";
            addBooksToolStripMenuItem.Click += addBooksToolStripMenuItem_Click;
            // 
            // reviewsToolStripMenuItem
            // 
            reviewsToolStripMenuItem.Name = "reviewsToolStripMenuItem";
            reviewsToolStripMenuItem.Size = new Size(76, 24);
            reviewsToolStripMenuItem.Text = "Reviews";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.InactiveCaption;
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(1505, 463);
            panel1.TabIndex = 2;
            // 
            // JungleDesktop
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1504, 490);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "JungleDesktop";
            Text = "JungleWebsite";
            Load += JungleDesktop_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem addBooksToolStripMenuItem;
        private Panel panel1;
        private ToolStripMenuItem reviewsToolStripMenuItem;
    }
}