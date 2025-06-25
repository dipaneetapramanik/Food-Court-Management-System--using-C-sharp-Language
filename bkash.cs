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
        private string connStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";

        public bkash(DataTable selectedItems, int stallId, decimal toPay)
        {
            InitializeComponent();
            this.selectedItems = selectedItems;
            this.stallId = stallId;
            this.toPay = toPay;
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
           option option = new option(selectedItems, stallId, toPay);
            option.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string bkashNumber = textBoxBkashNumber.Text.Trim();

            // Validation: Not blank, 11 chars, all digits
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

                    // Get new Customer_ID ONCE
                    int customerId;
                    using (OracleCommand cmd = new OracleCommand("SELECT customer_seq.NEXTVAL FROM dual", conn))
                    {
                        customerId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Get new Order_ID ONCE
                    int orderId;
                    using (OracleCommand cmd = new OracleCommand("SELECT order_seq.NEXTVAL FROM dual", conn))
                    {
                        orderId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Get new Payment_ID ONCE for the whole order
                    int paymentId;
                    using (OracleCommand cmd = new OracleCommand("SELECT payment_seq.NEXTVAL FROM dual", conn))
                    {
                        paymentId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Insert into Customer table ONCE
                    string customerInsertSql = @"INSERT INTO Customer(Customer_ID, Order_ID) values (:customerId, :orderId)";
                    using (OracleCommand cmd = new OracleCommand(customerInsertSql, conn))
                    {
                        cmd.Parameters.Add(":customerId", customerId);
                        cmd.Parameters.Add(":orderId", orderId);
                        int rows = cmd.ExecuteNonQuery();
                    }

                    // Insert order and payment for each item row except the last summary row
                    for (int i = 0; i < selectedItems.Rows.Count - 1; i++)
                    {
                        DataRow row = selectedItems.Rows[i];
                        string itemName = row["item_name"].ToString();
                        int numberOfPlates = Convert.ToInt32(row["NUMBER OF PLATES"]);
                        decimal totalPrice = Convert.ToDecimal(row["TOTAL PRICE"]);
                        decimal discountPrice = 0;
                        if (toPay < totalPrice)
                        {
                            discountPrice = totalPrice - toPay;
                        }

                        // Insert into Orders
                        InsertOrderForItem(conn, itemName, numberOfPlates, totalPrice, discountPrice, customerId, orderId, stallId);

                        // Get new Payment_ID for each payment/food
                    
                        using (OracleCommand cmd = new OracleCommand("SELECT payment_seq.NEXTVAL FROM dual", conn))
                        {
                            paymentId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insert into Payment: FORCE bKash as payment method
                        InsertPaymentForItem(conn, paymentId, orderId, itemName, "bKash", bkashNumber);
                    }

                    MessageBox.Show(
                        $"Your new Customer ID is: {customerId}\nYour new Order ID is: {orderId}",
                        "Order Placed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                
                    thanks thanks = new thanks(selectedItems, stallId, toPay);
                    thanks.Show();
                    this.Hide();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting order/payment: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertOrderForItem(OracleConnection conn, string itemName, int numberOfPlates, decimal totalPrice, decimal discountPrice, int customerId, int orderId, int stallId)
        {
            string insertSql = @"INSERT INTO Orders
                (Order_ID, Number_of_Plates, Total_Price, Customer_ID, Discount_Price, Food, Stall_ID)
                VALUES (:orderId, :numberOfPlates, :totalPrice, :customerId, :discountPrice, :food, :stallId)";

            using (OracleCommand cmd = new OracleCommand(insertSql, conn))
            {
                cmd.Parameters.Add(":orderId", orderId);
                cmd.Parameters.Add(":numberOfPlates", numberOfPlates);
                cmd.Parameters.Add(":totalPrice", totalPrice);
                cmd.Parameters.Add(":customerId", customerId);
                cmd.Parameters.Add(":discountPrice", discountPrice);
                cmd.Parameters.Add(":food", itemName);
                cmd.Parameters.Add(":stallId", stallId);

                cmd.ExecuteNonQuery();
            }
        }

        // Only use these columns if they exist in your table
        private void InsertPaymentForItem(OracleConnection conn, int paymentId, int orderId, string food, string paymentMethod, string bkashNumber)
        {
            string insertSql = @"INSERT INTO Payment
                (Payment_ID, Order_ID, Food, PAYMENT_METHOD)
                VALUES (:paymentId, :orderId, :food, :paymentMethod)";

            using (OracleCommand cmd = new OracleCommand(insertSql, conn))
            {
                cmd.Parameters.Add(":paymentId", paymentId);
                cmd.Parameters.Add(":orderId", orderId);
                cmd.Parameters.Add(":food", food);
                cmd.Parameters.Add(":paymentMethod", paymentMethod); // This is always "bKash" here

                // DEBUG: Show value before insert
                MessageBox.Show("Inserting Payment: " + paymentMethod, "Debug");

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    MessageBox.Show("Warning: Payment row was not inserted.", "Insert Warning");
            }
        }

        private void textBoxBkashNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}