using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class confirmation : Form
    {
        private DataTable selectedItems;
        private int stallId;
        private Form previousForm;

        public confirmation(DataTable selectedItems, int stallId, Form previousForm)
        {
            InitializeComponent();
            this.selectedItems = selectedItems;
            this.stallId = stallId;
            this.previousForm = previousForm; 
        }
       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void confirmation_Load(object sender, EventArgs e)
        {
            button2.Text = "Close";
            label2.Font = new Font(label2.Font, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(255, 176, 0);

            if (!selectedItems.Columns.Contains("NUMBER OF PLATES"))
                selectedItems.Columns.Add("NUMBER OF PLATES", typeof(int));


            foreach (DataRow row in selectedItems.Rows)
                row["NUMBER OF PLATES"] = 1;

            dataGridView1.DataSource = selectedItems;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns["NUMBER OF PLATES"].ReadOnly = false;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name != "NUMBER OF PLATES")
                    col.ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            payment optionForm = new payment(selectedItems, stallId);
            optionForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Are you sure you want to cancel?",
       "",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {

                this.Close();
            }
            else if (result == DialogResult.No)
            {
            }
           
      
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}