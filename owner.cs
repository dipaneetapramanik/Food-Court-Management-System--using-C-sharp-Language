using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class owner : Form
    {
        private int stallId;
        public owner(int stallId)
        {
            InitializeComponent();
            this.stallId = stallId;
        }

        private void owner_Load(object sender, EventArgs e)
        {
            // Fetch stall and owner info from database
            string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                string query = "SELECT STALL_LOCATION, STALL_NAME, OWNER_NAME, LICENSE_NUMBER, CONTACT_NUMBER, ADMIN_USERNAME FROM food_stall WHERE STALL_ID = :stallId";
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    cmd.Parameters.Add(new OracleParameter("stallId", stallId));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Update these label names to match your form's design
                            label6.Text = reader["STALL_LOCATION"].ToString();
                            label2.Text = reader["STALL_NAME"].ToString();
                            label1.Text = reader["OWNER_NAME"].ToString();
                            label5.Text = reader["LICENSE_NUMBER"].ToString();
                            label4.Text = reader["CONTACT_NUMBER"].ToString();
                            label3.Text = stallId.ToString();
                        } 
                        else
                        {
                            MessageBox.Show("No stall found for this owner!");
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                staff_login loginForm = new staff_login();
                loginForm.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            employee_inspection emp= new employee_inspection(stallId);
            emp.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu_inspection menu_Ispection = new menu_inspection(stallId);
            menu_Ispection.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            order_history og= new order_history(stallId);
            og.Show();

        }
    }
}