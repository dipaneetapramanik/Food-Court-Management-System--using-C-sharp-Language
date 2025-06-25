using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Food_Court_Management_System
{
    public partial class payment : Form
    {
        private DataTable selectedItems;
        private int stallId;
        
        private string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";

        public payment(DataTable selectedItems, int stallId)
        {
            InitializeComponent();
            this.selectedItems = selectedItems;
            this.stallId = stallId;
        }

        private void payment_Load(object sender, EventArgs e)
        {
     
            if (!selectedItems.Columns.Contains("UNIT PRICE"))
            {
                selectedItems.Columns.Add("UNIT PRICE", typeof(decimal));
                foreach (DataRow row in selectedItems.Rows)
                {
                    row["UNIT PRICE"] = Convert.ToDecimal(row["price"]);
                }
            }

            if (selectedItems.Columns.Contains("price"))
            {
                selectedItems.Columns["price"].ColumnName = "TOTAL PRICE";
            }
          
            foreach (DataRow row in selectedItems.Rows)
            {
                decimal unitPrice = Convert.ToDecimal(row["UNIT PRICE"]);
                int plates = Convert.ToInt32(row["NUMBER OF PLATES"]);
                row["TOTAL PRICE"] = unitPrice * plates;
            }

            AddToPayRow();

            dataGridView1.DataSource = selectedItems;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        private void AddToPayRow()
        {
   
            foreach (DataRow row in selectedItems.Rows.Cast<DataRow>().ToList())
            {
                if (row[0].ToString() == "TO PAY")
                {
                    selectedItems.Rows.Remove(row);
                    break;
                }
            }

            decimal sum = 0;
            foreach (DataRow row in selectedItems.Rows)
            {
                if (row[0].ToString() == "TO PAY") continue;
                if (decimal.TryParse(row["TOTAL PRICE"].ToString(), out decimal value))
                {
                    sum += value;
                }
            }

            DataRow totalRow = selectedItems.NewRow();
            totalRow[0] = "TO PAY";
            totalRow["TOTAL PRICE"] = sum;
            for (int i = 1; i < selectedItems.Columns.Count; i++)
            {
                if (selectedItems.Columns[i].ColumnName != "TOTAL PRICE")
                {
                    totalRow[i] = DBNull.Value;
                }
            }
            selectedItems.Rows.Add(totalRow);
        }

 
        private void button1_Click(object sender, EventArgs e)
        {
            string inputDiscount = textBox1.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(inputDiscount))
            {
                MessageBox.Show("Please enter a discount name.");
                return;
            }

            DataRow toPayRow = selectedItems.Rows
                .Cast<DataRow>()
                .FirstOrDefault(r => r[0].ToString() == "TO PAY");

            if (toPayRow == null)
            {
                MessageBox.Show("TO PAY row not found.");
                return;
            }

            decimal totalToPay = Convert.ToDecimal(toPayRow["TOTAL PRICE"]);

       
            using (var con = new OracleConnection(conString))
            {
                con.Open();
                string query = "SELECT DISCOUNT_PERCENTAGES FROM discount WHERE LOWER(DISCOUNT_NAME) = :name";
                using (var cmd = new OracleCommand(query, con))
                {
                    cmd.Parameters.Add(new OracleParameter("name", inputDiscount));
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int discountPercent = Convert.ToInt32(result);
                        decimal discountAmount = totalToPay * discountPercent / 100m;
                        decimal newTotal = totalToPay - discountAmount;

                        toPayRow["TOTAL PRICE"] = newTotal;

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = selectedItems;

                        MessageBox.Show($"Discount applied");

                        button1.Enabled = false;
                        discount.Show();
                        label3.Show();
                        label3.Text = $"{newTotal} Taka";

                    }
                    else
                    {
                        MessageBox.Show("Invalid discount name.");
                    }
                }
            }
        }
        private decimal GetToPayAmount()
        {
            DataRow toPayRow = selectedItems.Rows
                .Cast<DataRow>()
                .FirstOrDefault(r => r[0].ToString() == "TO PAY");

            if (toPayRow != null && toPayRow["TOTAL PRICE"] != DBNull.Value)
                return Convert.ToDecimal(toPayRow["TOTAL PRICE"]);
            else
                return 0;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            confirmation confirmationForm = new confirmation(selectedItems, stallId, this);
            confirmationForm.Show();
            this.Hide();
        }


        private void discount_Click(object sender, EventArgs e)
        {
        }

       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

   
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }


        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e)
        {
            decimal toPay = GetToPayAmount();
            option optionForm = new option(selectedItems, stallId, toPay);
            optionForm.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}