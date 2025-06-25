using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class staff : Form
    {
        private int staffId;
        public staff(int staffId)
        {
            InitializeComponent();
            this.staffId = staffId;
        }

        private void staff_Load(object sender, EventArgs e)
        {
            // Fetch staff info from database
            string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                string query = "SELECT STAFF_ID, STAFF_NAME, STAFF_ROLE, STAFF_SALARY, STAFF_MOBILE_NUMBER, STAFF_ADDRESS, ADMIN_USERNAME FROM staffs WHERE STAFF_ID = :staffId";
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    cmd.Parameters.Add(new OracleParameter("staffId", staffId));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            label1.Text = reader["STAFF_NAME"].ToString();
                            label2.Text = reader["STAFF_ROLE"].ToString();
                            label3.Text = reader["STAFF_SALARY"].ToString();
                            label4.Text = reader["STAFF_MOBILE_NUMBER"].ToString();
                            label5.Text = reader["STAFF_ADDRESS"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Staff not found.");
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}