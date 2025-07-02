using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class employee : Form
    {
        private int employeeId;

        public employee(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
        }

        private void employee_Load(object sender, EventArgs e)
        {
            string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                string query = "SELECT EMPLOYEE_NAME, EMPLOYEE_ROLE, EMPLOYEE_ADDRESS, EMPLOYEE_SALARY, E_MOBILE_NUMBER, STALL_ID FROM employee WHERE EMPLOYEE_ID = :empId";
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    cmd.Parameters.Add(new OracleParameter("empId", employeeId));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            label1.Text = reader["EMPLOYEE_NAME"].ToString();
                            label2.Text = reader["EMPLOYEE_ROLE"].ToString();
                            label3.Text = reader["EMPLOYEE_ADDRESS"].ToString();
                            label4.Text = reader["EMPLOYEE_SALARY"].ToString();
                            label5.Text = reader["E_MOBILE_NUMBER"].ToString();
                            label6.Text = reader["STALL_ID"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No employee found for this ID.");
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

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
    }
}