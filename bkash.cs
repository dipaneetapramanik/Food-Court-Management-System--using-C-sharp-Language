using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Food_Court_Management_System
{
    public partial class bkash : Form
    {
        private DataTable selectedItems;
        private int stallId;
        private decimal toPay;
        private int discountId;
        private int discountPercent;
        private string discountName;
        private string connStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";

        public bkash(DataTable selectedItems, int stallId, decimal toPay, int discountId, int discountPercent, string discountName)
        {
            InitializeComponent();
            this.selectedItems = selectedItems;
            this.stallId = stallId;
            this.toPay = toPay;
            this.discountId = discountId;
            this.discountPercent = discountPercent;
            this.discountName = discountName;
        }

        private void bkash_Load(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button1.ForeColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;

            label2.Text = $"To Pay: {toPay:C}";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Text = $"{toPay:C}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            option option = new option(selectedItems, stallId, toPay, discountId, discountPercent, discountName);
            option.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string bkashNumber = textBoxBkashNumber.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(bkashNumber))
            {
                MessageBox.Show("bKash number cannot be blank.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxBkashNumber.Focus();
                return;
            }
            if (bkashNumber.Length != 11)
            {
                MessageBox.Show("bKash number must be exactly 11 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxBkashNumber.Focus();
                return;
            }
            if (!Regex.IsMatch(bkashNumber, @"^\d{11}$"))
            {
                MessageBox.Show("bKash number must contain only digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxBkashNumber.Focus();
                return;
            }

            if (selectedItems.Rows.Count <= 1)
            {
                MessageBox.Show("No items selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Step 1: Get new Customer_ID
                    int customerId;
                    using (OracleCommand cmd = new OracleCommand("SELECT customer_seq.NEXTVAL FROM dual", conn))
                        customerId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Step 2: Insert Customer
                    using (OracleCommand cmd = new OracleCommand("INSERT INTO Customer(Customer_ID) VALUES (:customerId)", conn))
                    {
                        cmd.Parameters.Add(":customerId", customerId);
                        cmd.ExecuteNonQuery();
                    }

                    // Step 3: Get new Order_ID
                    int orderId;
                    using (OracleCommand cmd = new OracleCommand("SELECT order_seq.NEXTVAL FROM dual", conn))
                        orderId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Step 4: Insert into Orders (one row per item, same Order_ID)
                    for (int i = 0; i < selectedItems.Rows.Count - 1; i++)
                    {
                        DataRow row = selectedItems.Rows[i];
                        string food = row["item_name"].ToString();
                        int plates = Convert.ToInt32(row["NUMBER OF PLATES"]);
                        decimal totalPrice = Convert.ToDecimal(row["TOTAL PRICE"]);
                        decimal discountedPrice = row.Table.Columns.Contains("DISCOUNTED PRICE") && !Convert.IsDBNull(row["DISCOUNTED PRICE"])
                            ? Convert.ToDecimal(row["DISCOUNTED PRICE"])
                            : totalPrice;

                        using (OracleCommand cmd = new OracleCommand(
                            @"INSERT INTO Orders (Order_ID, Number_of_Plates, Total_Price, Customer_ID, Discounted_Price, Food, Stall_ID, Discount_Id)
                              VALUES (:orderId, :numberOfPlates, :totalPrice, :customerId, :discountedPrice, :food, :stallId, :discountId)", conn))
                        {
                            cmd.Parameters.Add(":orderId", orderId);
                            cmd.Parameters.Add(":numberOfPlates", plates);
                            cmd.Parameters.Add(":totalPrice", totalPrice);
                            cmd.Parameters.Add(":customerId", customerId);
                            cmd.Parameters.Add(":discountedPrice", discountedPrice);
                            cmd.Parameters.Add(":food", food);
                            cmd.Parameters.Add(":stallId", stallId);
                            cmd.Parameters.Add(":discountId", discountId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Step 5: Insert Payment (one row per order, payment method is bKash)
                    using (OracleCommand cmd = new OracleCommand(
                        @"INSERT INTO Payment (Payment_ID, Order_ID, Payment_Method) 
                          VALUES (payment_seq.NEXTVAL, :orderId, :paymentMethod)", conn))
                    {
                        cmd.Parameters.Add(":orderId", orderId);
                        cmd.Parameters.Add(":paymentMethod", "bKash");
                        
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        $"Your new Customer ID is: {customerId}\nYour new Order ID is: {orderId}\nDiscount: {discountName} ({discountPercent}%)",
                        "Order Placed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    thanks thanks = new thanks(selectedItems, stallId, toPay);
                    thanks.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxBkashNumber_TextChanged(object sender, EventArgs e)
        {
            // Optionally handle text changed event
        }
    }
}