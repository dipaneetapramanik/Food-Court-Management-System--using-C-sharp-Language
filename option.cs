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
    public partial class option : Form
    {
        private DataTable selectedItems;
        private int stallId;
        private decimal toPay;
        private string paymentMethod = "Cash"; // Default, can be set as needed

        // Oracle connection string
        private string connStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";

        public option(DataTable selectedItems, int stallId, decimal toPay)
        {
            InitializeComponent();
            this.selectedItems = selectedItems;
            this.stallId = stallId;
            this.toPay = toPay;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Payment Successful. Redirecting to Service.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                decimal toPay = 0;
                if (selectedItems.Rows.Count > 0)
                {
                    DataRow lastRow = selectedItems.Rows[selectedItems.Rows.Count - 1];
                    if (!Convert.IsDBNull(lastRow["TOTAL PRICE"]) && lastRow[0].ToString() == "TO PAY")
                    {
                        toPay = Convert.ToDecimal(lastRow["TOTAL PRICE"]);
                    }
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

                        // Insert into Customer table ONCE
                        string insertsql = @"INSERT INTO Customer(Customer_ID, Order_ID) values (:customerId, :orderId)";
                        using (OracleCommand cmd = new OracleCommand(insertsql, conn))
                        {
                            cmd.Parameters.Add(":customerId", customerId);
                            cmd.Parameters.Add(":orderId", orderId);
                            cmd.ExecuteNonQuery();
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

                            // Insert into Payment (use this.paymentMethod, or pass from UI)
                            InsertPaymentForItem(conn, orderId, itemName, paymentMethod);
                        }

                        MessageBox.Show(
                            $"Your new Customer ID is: {customerId}\nYour new Order ID is: {orderId}",
                            "Order Placed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                       thanks thanks= new thanks(selectedItems, stallId, toPay);
                        thanks.Show();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting order/payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void InsertPaymentForItem(OracleConnection conn, int orderId, string food, string paymentMethod)
        {
            // Get new Payment_ID from sequence each time
            int paymentId;
            using (OracleCommand cmd = new OracleCommand("SELECT payment_seq.NEXTVAL FROM dual", conn))
            {
                paymentId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            string insertSql = @"INSERT INTO Payment
                (Payment_ID, Order_ID, Food, Payment_Method)
                VALUES (:paymentId, :orderId, :food, :paymentMethod)";

            using (OracleCommand cmd = new OracleCommand(insertSql, conn))
            {
                cmd.Parameters.Add(":paymentId", paymentId);
                cmd.Parameters.Add(":orderId", orderId);
                cmd.Parameters.Add(":food", food);
                cmd.Parameters.Add(":paymentMethod", paymentMethod);

                cmd.ExecuteNonQuery();
            }
        }

        private void option_Load(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            button2.BackColor = System.Drawing.Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bkash bkash = new bkash(selectedItems, stallId, toPay);
            bkash .Show();
            this.Hide();
             }
    }
}