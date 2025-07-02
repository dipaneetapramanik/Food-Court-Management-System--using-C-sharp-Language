using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Food_Court_Management_System
{
    public partial class option : Form
    {
        private DataTable selectedItems;
        private int stallId;
        private decimal toPay;
        private int discountId;
        private int discountPercent;
        private string discountName;
        private string paymentMethod = "Cash"; // Default
        private string connStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";

        public option(DataTable selectedItems, int stallId, decimal toPay, int discountId, int discountPercent, string discountName)
        {
            InitializeComponent();
            this.selectedItems = selectedItems;
            this.stallId = stallId;
            this.toPay = toPay;
            this.discountId = discountId;
            this.discountPercent = discountPercent;
            this.discountName = discountName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Payment Successful. Redirecting to Service.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
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

                        // Step 5: Insert Payment (one row per order)
                        using (OracleCommand cmd = new OracleCommand(
                            @"INSERT INTO Payment (Payment_ID, Order_ID, Payment_Method) 
                              VALUES (payment_seq.NEXTVAL, :orderId, :paymentMethod)", conn))
                        {
                            cmd.Parameters.Add(":orderId", orderId);
                            cmd.Parameters.Add(":paymentMethod", paymentMethod);
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
        }

        private void option_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button2.BackColor = Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bkash bkash = new bkash(selectedItems, stallId, toPay, discountId, discountPercent, discountName);
            bkash.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Reserved for future use or exit
        }
    }
}