namespace Food_Court_Management_System
{
    partial class role_choice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(role_choice));
            this.staff_button = new System.Windows.Forms.Button();
            this.customer_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // staff_button
            // 
            this.staff_button.BackColor = System.Drawing.Color.Transparent;
            this.staff_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.staff_button.Location = new System.Drawing.Point(782, 233);
            this.staff_button.Name = "staff_button";
            this.staff_button.Size = new System.Drawing.Size(260, 386);
            this.staff_button.TabIndex = 1;
            this.staff_button.UseVisualStyleBackColor = false;
            this.staff_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // customer_button
            // 
            this.customer_button.BackColor = System.Drawing.Color.Transparent;
            this.customer_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customer_button.Location = new System.Drawing.Point(74, 212);
            this.customer_button.Name = "customer_button";
            this.customer_button.Size = new System.Drawing.Size(417, 386);
            this.customer_button.TabIndex = 2;
            this.customer_button.UseVisualStyleBackColor = false;
            this.customer_button.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // role_choice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.customer_button);
            this.Controls.Add(this.staff_button);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "role_choice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "কুড়াতলী ভোজনশালা";
            this.Load += new System.EventHandler(this.role_choice_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button staff_button;
        private System.Windows.Forms.Button customer_button;
    }
}