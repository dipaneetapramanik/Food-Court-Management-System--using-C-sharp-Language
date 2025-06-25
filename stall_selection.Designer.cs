namespace Food_Court_Management_System
{
    partial class stall_selection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(stall_selection));
            this.bfc = new System.Windows.Forms.Button();
            this.tasty_treat = new System.Windows.Forms.Button();
            this.takeout = new System.Windows.Forms.Button();
            this.sultan_dine = new System.Windows.Forms.Button();
            this.cha_time = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bfc
            // 
            this.bfc.BackColor = System.Drawing.Color.Transparent;
            this.bfc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bfc.Location = new System.Drawing.Point(80, 177);
            this.bfc.Name = "bfc";
            this.bfc.Size = new System.Drawing.Size(263, 259);
            this.bfc.TabIndex = 0;
            this.bfc.UseVisualStyleBackColor = false;
            this.bfc.Click += new System.EventHandler(this.button1_Click);
            // 
            // tasty_treat
            // 
            this.tasty_treat.BackColor = System.Drawing.Color.Transparent;
            this.tasty_treat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tasty_treat.Location = new System.Drawing.Point(467, 177);
            this.tasty_treat.Name = "tasty_treat";
            this.tasty_treat.Size = new System.Drawing.Size(265, 267);
            this.tasty_treat.TabIndex = 1;
            this.tasty_treat.UseVisualStyleBackColor = false;
            this.tasty_treat.Click += new System.EventHandler(this.tasty_treat_Click);
            // 
            // takeout
            // 
            this.takeout.BackColor = System.Drawing.Color.Transparent;
            this.takeout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.takeout.Location = new System.Drawing.Point(857, 173);
            this.takeout.Name = "takeout";
            this.takeout.Size = new System.Drawing.Size(265, 267);
            this.takeout.TabIndex = 2;
            this.takeout.UseVisualStyleBackColor = false;
            this.takeout.Click += new System.EventHandler(this.takeout_Click);
            // 
            // sultan_dine
            // 
            this.sultan_dine.BackColor = System.Drawing.Color.Transparent;
            this.sultan_dine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sultan_dine.Location = new System.Drawing.Point(284, 492);
            this.sultan_dine.Name = "sultan_dine";
            this.sultan_dine.Size = new System.Drawing.Size(265, 267);
            this.sultan_dine.TabIndex = 3;
            this.sultan_dine.UseVisualStyleBackColor = false;
            this.sultan_dine.Click += new System.EventHandler(this.sultan_dine_Click);
            // 
            // cha_time
            // 
            this.cha_time.BackColor = System.Drawing.Color.Transparent;
            this.cha_time.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cha_time.Location = new System.Drawing.Point(637, 492);
            this.cha_time.Name = "cha_time";
            this.cha_time.Size = new System.Drawing.Size(265, 267);
            this.cha_time.TabIndex = 4;
            this.cha_time.UseVisualStyleBackColor = false;
            this.cha_time.Click += new System.EventHandler(this.cha_time_Click);
            // 
            // stall_selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.cha_time);
            this.Controls.Add(this.sultan_dine);
            this.Controls.Add(this.takeout);
            this.Controls.Add(this.tasty_treat);
            this.Controls.Add(this.bfc);
            this.Name = "stall_selection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "কুড়াতলী ভোজনশালা";
            this.Load += new System.EventHandler(this.stall_selection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bfc;
        private System.Windows.Forms.Button tasty_treat;
        private System.Windows.Forms.Button takeout;
        private System.Windows.Forms.Button sultan_dine;
        private System.Windows.Forms.Button cha_time;
    }
}