namespace Food_Court_Management_System
{
    partial class admin_owner
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.stall_id_textbox = new System.Windows.Forms.TextBox();
            this.stall_name_textbox = new System.Windows.Forms.TextBox();
            this.owner_name_textbox = new System.Windows.Forms.TextBox();
            this.stall_location_tectbox = new System.Windows.Forms.TextBox();
            this.licenese_number_textbox = new System.Windows.Forms.TextBox();
            this.contact_number_textbox = new System.Windows.Forms.TextBox();
            this.add_button = new System.Windows.Forms.Button();
            this.update_button = new System.Windows.Forms.Button();
            this.search_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.load_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 564);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(986, 197);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(342, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "Owner\'s List";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 383);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Licence Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "Stall ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 32);
            this.label4.TabIndex = 4;
            this.label4.Text = "Stall Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(206, 32);
            this.label5.TabIndex = 5;
            this.label5.Text = "Owner\'s Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(195, 32);
            this.label6.TabIndex = 6;
            this.label6.Text = "Stall Location:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(26, 447);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(231, 32);
            this.label7.TabIndex = 7;
            this.label7.Text = "Contact Number:";
            // 
            // stall_id_textbox
            // 
            this.stall_id_textbox.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stall_id_textbox.Location = new System.Drawing.Point(238, 107);
            this.stall_id_textbox.Multiline = true;
            this.stall_id_textbox.Name = "stall_id_textbox";
            this.stall_id_textbox.Size = new System.Drawing.Size(452, 40);
            this.stall_id_textbox.TabIndex = 8;
            this.stall_id_textbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // stall_name_textbox
            // 
            this.stall_name_textbox.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.stall_name_textbox.Location = new System.Drawing.Point(238, 170);
            this.stall_name_textbox.Multiline = true;
            this.stall_name_textbox.Name = "stall_name_textbox";
            this.stall_name_textbox.Size = new System.Drawing.Size(452, 40);
            this.stall_name_textbox.TabIndex = 9;
            this.stall_name_textbox.TextChanged += new System.EventHandler(this.stall_name_textbox_TextChanged);
            // 
            // owner_name_textbox
            // 
            this.owner_name_textbox.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.owner_name_textbox.Location = new System.Drawing.Point(238, 240);
            this.owner_name_textbox.Multiline = true;
            this.owner_name_textbox.Name = "owner_name_textbox";
            this.owner_name_textbox.Size = new System.Drawing.Size(452, 40);
            this.owner_name_textbox.TabIndex = 10;
            this.owner_name_textbox.TextChanged += new System.EventHandler(this.owner_name_textbox_TextChanged);
            // 
            // stall_location_tectbox
            // 
            this.stall_location_tectbox.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.stall_location_tectbox.Location = new System.Drawing.Point(238, 308);
            this.stall_location_tectbox.Multiline = true;
            this.stall_location_tectbox.Name = "stall_location_tectbox";
            this.stall_location_tectbox.Size = new System.Drawing.Size(452, 40);
            this.stall_location_tectbox.TabIndex = 11;
            this.stall_location_tectbox.TextChanged += new System.EventHandler(this.stall_location_tectbox_TextChanged);
            // 
            // licenese_number_textbox
            // 
            this.licenese_number_textbox.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.licenese_number_textbox.Location = new System.Drawing.Point(260, 383);
            this.licenese_number_textbox.Multiline = true;
            this.licenese_number_textbox.Name = "licenese_number_textbox";
            this.licenese_number_textbox.Size = new System.Drawing.Size(438, 40);
            this.licenese_number_textbox.TabIndex = 12;
            this.licenese_number_textbox.TextChanged += new System.EventHandler(this.licenese_number_textbox_TextChanged);
            // 
            // contact_number_textbox
            // 
            this.contact_number_textbox.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.contact_number_textbox.Location = new System.Drawing.Point(260, 447);
            this.contact_number_textbox.Multiline = true;
            this.contact_number_textbox.Name = "contact_number_textbox";
            this.contact_number_textbox.Size = new System.Drawing.Size(438, 40);
            this.contact_number_textbox.TabIndex = 13;
            this.contact_number_textbox.TextChanged += new System.EventHandler(this.contact_number_textbox_TextChanged);
            // 
            // add_button
            // 
            this.add_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_button.Location = new System.Drawing.Point(761, 105);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(113, 42);
            this.add_button.TabIndex = 14;
            this.add_button.Text = "Add";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // update_button
            // 
            this.update_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.update_button.Location = new System.Drawing.Point(761, 170);
            this.update_button.Name = "update_button";
            this.update_button.Size = new System.Drawing.Size(113, 42);
            this.update_button.TabIndex = 15;
            this.update_button.Text = "Update";
            this.update_button.UseVisualStyleBackColor = true;
            this.update_button.Click += new System.EventHandler(this.update_button_Click);
            // 
            // search_button
            // 
            this.search_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.search_button.Location = new System.Drawing.Point(761, 240);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(113, 42);
            this.search_button.TabIndex = 16;
            this.search_button.Text = "Search";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.delete_button.Location = new System.Drawing.Point(761, 308);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(113, 42);
            this.delete_button.TabIndex = 17;
            this.delete_button.Text = "Delete";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // load_button
            // 
            this.load_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.load_button.Location = new System.Drawing.Point(761, 381);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(113, 42);
            this.load_button.TabIndex = 18;
            this.load_button.Text = "Load";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.button1.Location = new System.Drawing.Point(761, 445);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 42);
            this.button1.TabIndex = 19;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(835, 134);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // admin_owner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.load_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.update_button);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.contact_number_textbox);
            this.Controls.Add(this.licenese_number_textbox);
            this.Controls.Add(this.stall_location_tectbox);
            this.Controls.Add(this.owner_name_textbox);
            this.Controls.Add(this.stall_name_textbox);
            this.Controls.Add(this.stall_id_textbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "admin_owner";
            this.Text = "Owners List";
            this.Load += new System.EventHandler(this.admin_owner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox stall_id_textbox;
        private System.Windows.Forms.TextBox stall_name_textbox;
        private System.Windows.Forms.TextBox owner_name_textbox;
        private System.Windows.Forms.TextBox stall_location_tectbox;
        private System.Windows.Forms.TextBox licenese_number_textbox;
        private System.Windows.Forms.TextBox contact_number_textbox;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Button update_button;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}