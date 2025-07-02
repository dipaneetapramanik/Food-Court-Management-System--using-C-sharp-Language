namespace Food_Court_Management_System
{
    partial class admin_staffs
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.staff_id_textbox = new System.Windows.Forms.TextBox();
            this.staff_name_textbox = new System.Windows.Forms.TextBox();
            this.staff_salary_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.staff_mobile_textbox = new System.Windows.Forms.TextBox();
            this.staff_address_textbox = new System.Windows.Forms.TextBox();
            this.staff_role_combobox = new System.Windows.Forms.ComboBox();
            this.add_buttton = new System.Windows.Forms.Button();
            this.update_button = new System.Windows.Forms.Button();
            this.search_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.load_button = new System.Windows.Forms.Button();
            this.back_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-1, 366);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(986, 197);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(346, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 55);
            this.label1.TabIndex = 2;
            this.label1.Text = "Staff\'s List";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "Staff ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 32);
            this.label4.TabIndex = 5;
            this.label4.Text = "Staff Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 32);
            this.label5.TabIndex = 6;
            this.label5.Text = "Staff Salary:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 32);
            this.label6.TabIndex = 8;
            this.label6.Text = "Staff Address:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 32);
            this.label7.TabIndex = 9;
            this.label7.Text = "Mobile Number:";
            // 
            // staff_id_textbox
            // 
            this.staff_id_textbox.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staff_id_textbox.Location = new System.Drawing.Point(177, 72);
            this.staff_id_textbox.Multiline = true;
            this.staff_id_textbox.Name = "staff_id_textbox";
            this.staff_id_textbox.Size = new System.Drawing.Size(452, 40);
            this.staff_id_textbox.TabIndex = 10;
          //  this.staff_id_textbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // staff_name_textbox
            // 
            this.staff_name_textbox.Font = new System.Drawing.Font("Times New Roman", 21.75F);
            this.staff_name_textbox.Location = new System.Drawing.Point(177, 122);
            this.staff_name_textbox.Multiline = true;
            this.staff_name_textbox.Name = "staff_name_textbox";
            this.staff_name_textbox.Size = new System.Drawing.Size(452, 40);
            this.staff_name_textbox.TabIndex = 11;
         //   this.staff_name_textbox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // staff_salary_textbox
            // 
            this.staff_salary_textbox.Font = new System.Drawing.Font("Times New Roman", 21.75F);
            this.staff_salary_textbox.Location = new System.Drawing.Point(177, 173);
            this.staff_salary_textbox.Multiline = true;
            this.staff_salary_textbox.Name = "staff_salary_textbox";
            this.staff_salary_textbox.Size = new System.Drawing.Size(452, 40);
            this.staff_salary_textbox.TabIndex = 12;
         //   this.staff_salary_textbox.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 318);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 32);
            this.label2.TabIndex = 13;
            this.label2.Text = "Staff Role:";
            // 
            // staff_mobile_textbox
            // 
            this.staff_mobile_textbox.Font = new System.Drawing.Font("Times New Roman", 21.75F);
            this.staff_mobile_textbox.Location = new System.Drawing.Point(228, 222);
            this.staff_mobile_textbox.Multiline = true;
            this.staff_mobile_textbox.Name = "staff_mobile_textbox";
            this.staff_mobile_textbox.Size = new System.Drawing.Size(401, 40);
            this.staff_mobile_textbox.TabIndex = 14;
         //   this.staff_mobile_textbox.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // staff_address_textbox
            // 
            this.staff_address_textbox.Font = new System.Drawing.Font("Times New Roman", 21.75F);
            this.staff_address_textbox.Location = new System.Drawing.Point(186, 268);
            this.staff_address_textbox.Multiline = true;
            this.staff_address_textbox.Name = "staff_address_textbox";
            this.staff_address_textbox.Size = new System.Drawing.Size(443, 40);
            this.staff_address_textbox.TabIndex = 15;
          //  this.staff_address_textbox.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // staff_role_combobox
            // 
            this.staff_role_combobox.Font = new System.Drawing.Font("Times New Roman", 21.75F);
            this.staff_role_combobox.FormattingEnabled = true;
            this.staff_role_combobox.Items.AddRange(new object[] {
            "Manager",
            "Cashier",
            "Cleaner",
            "Maintainance Worker"});
            this.staff_role_combobox.Location = new System.Drawing.Point(155, 318);
            this.staff_role_combobox.Name = "staff_role_combobox";
            this.staff_role_combobox.Size = new System.Drawing.Size(152, 41);
            this.staff_role_combobox.TabIndex = 16;
         //g   this.staff_role_combobox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // add_buttton
            // 
            this.add_buttton.Location = new System.Drawing.Point(689, 72);
            this.add_buttton.Name = "add_buttton";
            this.add_buttton.Size = new System.Drawing.Size(105, 40);
            this.add_buttton.TabIndex = 17;
            this.add_buttton.Text = "Add";
            this.add_buttton.UseVisualStyleBackColor = true;
            this.add_buttton.Click += new System.EventHandler(this.add_buttton_Click);
            // 
            // update_button
            // 
            this.update_button.Location = new System.Drawing.Point(689, 122);
            this.update_button.Name = "update_button";
            this.update_button.Size = new System.Drawing.Size(105, 40);
            this.update_button.TabIndex = 18;
            this.update_button.Text = "Update";
            this.update_button.UseVisualStyleBackColor = true;
            this.update_button.Click += new System.EventHandler(this.update_button_Click);
            // 
            // search_button
            // 
            this.search_button.Location = new System.Drawing.Point(689, 173);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(105, 40);
            this.search_button.TabIndex = 19;
            this.search_button.Text = "Search";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Location = new System.Drawing.Point(689, 226);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(105, 40);
            this.delete_button.TabIndex = 20;
            this.delete_button.Text = "Delete";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // load_button
            // 
            this.load_button.Location = new System.Drawing.Point(689, 283);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(105, 40);
            this.load_button.TabIndex = 21;
            this.load_button.Text = "Load";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click);
            // 
            // back_button
            // 
            this.back_button.Location = new System.Drawing.Point(848, 173);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(105, 40);
            this.back_button.TabIndex = 22;
            this.back_button.Text = "Back";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // admin_staffs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Purple;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.back_button);
            this.Controls.Add(this.load_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.update_button);
            this.Controls.Add(this.add_buttton);
            this.Controls.Add(this.staff_role_combobox);
            this.Controls.Add(this.staff_address_textbox);
            this.Controls.Add(this.staff_mobile_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.staff_salary_textbox);
            this.Controls.Add(this.staff_name_textbox);
            this.Controls.Add(this.staff_id_textbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "admin_staffs";
            this.Text = "Staff Info";
            this.Load += new System.EventHandler(this.admin_staffs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox staff_id_textbox;
        private System.Windows.Forms.TextBox staff_name_textbox;
        private System.Windows.Forms.TextBox staff_salary_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox staff_mobile_textbox;
        private System.Windows.Forms.TextBox staff_address_textbox;
        private System.Windows.Forms.ComboBox staff_role_combobox;
        private System.Windows.Forms.Button add_buttton;
        private System.Windows.Forms.Button update_button;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.Button back_button;
    }
}