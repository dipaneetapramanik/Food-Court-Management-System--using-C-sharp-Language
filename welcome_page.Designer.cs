namespace Food_Court_Management_System
{
    partial class welcome_page
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(welcome_page));
            this.get_started_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // get_started_button
            // 
            this.get_started_button.BackColor = System.Drawing.Color.White;
            this.get_started_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.get_started_button.Font = new System.Drawing.Font("Times New Roman", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.get_started_button.Location = new System.Drawing.Point(440, 395);
            this.get_started_button.Name = "get_started_button";
            this.get_started_button.Size = new System.Drawing.Size(337, 117);
            this.get_started_button.TabIndex = 0;
            this.get_started_button.Text = "Get Started";
            this.get_started_button.UseVisualStyleBackColor = false;
            this.get_started_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // welcome_page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.get_started_button);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "welcome_page";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "কুড়াতলী ভোজনশালা";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button get_started_button;
    }
}

