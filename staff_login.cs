using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class staff_login : Form
    {
        public staff_login()
        {
            InitializeComponent();
        }

        private void staff_login_Load(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a staff type.");
                return;
            }

            string staffType = comboBox1.SelectedItem.ToString();

            if (staffType == "Owner")
            {
                string inputOwnerName = textBox1.Text.Trim();
                string inputContact = textBox2.Text.Trim();

                if (string.IsNullOrEmpty(inputOwnerName))
                {
                    MessageBox.Show("Owner name is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(inputContact))
                {
                    MessageBox.Show("Contact number is empty.");
                    return;
                }

                string ownerNameStr = inputOwnerName.ToString();
                string contactStr = inputContact.ToString();

                string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    // First, check OWNER_NAME
                    string query = "SELECT CONTACT_NUMBER, STALL_ID FROM food_stall WHERE OWNER_NAME = :ownerName";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("ownerName", ownerNameStr));
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbContact = reader["CONTACT_NUMBER"].ToString();
                                int stallId = Convert.ToInt32(reader["STALL_ID"]);

                                if (dbContact == contactStr)
                                {
                                    MessageBox.Show("Owner login successful!");
                                    owner ownerForm = new owner(stallId); // Passes STALL_ID to owner form
                                    ownerForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Contact number does not match for this owner.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Owner name not found.");
                            }
                        }
                    }
                }
            }
            else if (staffType == "Shop")
            {
                string inputStallId = textBox1.Text.Trim();
                string inputLicense = textBox2.Text.Trim();

                if (string.IsNullOrEmpty(inputStallId))
                {
                    MessageBox.Show("Stall ID is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(inputLicense))
                {
                    MessageBox.Show("License Number is empty.");
                    return;
                }

                // Validate inputStallId is an integer
                if (!int.TryParse(inputStallId, out int stallIdInt))
                {
                    MessageBox.Show("Stall ID must be a number.");
                    return;
                }

                string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";

                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();

                    // 1. Check if STALL_ID exists in orders
                    string query = "SELECT COUNT(*) FROM orders WHERE STALL_ID = :stallId";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("stallId", stallIdInt));
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 0)
                        {
                            MessageBox.Show("Stall ID not found in orders table.");
                            return;
                        }
                    }

                    // 2. Check LICENSE_NUMBER from food_stall
                    string licenseQuery = "SELECT LICENSE_NUMBER FROM food_stall WHERE STALL_ID = :stallId";
                    using (OracleCommand cmd2 = new OracleCommand(licenseQuery, con))
                    {
                        cmd2.Parameters.Add(new OracleParameter("stallId", stallIdInt));
                        object result = cmd2.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Stall ID not found in food_stall table.");
                            return;
                        }

                        string licenseNumber = result.ToString();

                        if (licenseNumber == inputLicense)
                        {
                            MessageBox.Show("Login successful!");
                            Shop shopForm = new Shop(stallIdInt);
                            shopForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid license number.");
                        }
                    }
                }
            }
            else if (staffType == "Employee")
            {
                string inputEmployeeName = textBox1.Text.Trim();
                string inputMobile = textBox2.Text.Trim();

                if (string.IsNullOrEmpty(inputEmployeeName))
                {
                    MessageBox.Show("Employee name is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(inputMobile))
                {
                    MessageBox.Show("Mobile number is empty.");
                    return;
                }

                string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = "SELECT STALL_ID FROM employee WHERE EMPLOYEE_NAME = :employeeName AND E_MOBILE_NUMBER = :mobile";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("employeeName", inputEmployeeName));
                        cmd.Parameters.Add(new OracleParameter("mobile", inputMobile));
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int stallId = Convert.ToInt32(result);
                            MessageBox.Show("Login successful!");
                            // Pass stallId to the employee form
                            employee empForm = new employee(stallId); // Make sure your employee form has a constructor that takes stallId
                            empForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid employee name or mobile number.");
                        }
                    }
                }

            }
            else if (staffType == "Staff")
            {
                string inputStaffName = textBox1.Text.Trim();
                string inputMobile = textBox2.Text.Trim();

                if (string.IsNullOrEmpty(inputStaffName))
                {
                    MessageBox.Show("Name is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(inputMobile))
                {
                    MessageBox.Show("Mobile number is empty.");
                    return;
                }

                string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = "SELECT STAFF_ID FROM staffs WHERE STAFF_NAME = :staffName AND STAFF_MOBILE_NUMBER = :mobile";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("staffName", inputStaffName));
                        cmd.Parameters.Add(new OracleParameter("mobile", inputMobile));
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int staffId = Convert.ToInt32(result);
                            MessageBox.Show("Login successful!");

                            staff staffForm = new staff(staffId);
                            staffForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid staff name or mobile number.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Selected staff type is not supported yet.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void staff_login_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            welcome_page get= new welcome_page();
            get.Show();
            this.Hide();
        }
    }
}