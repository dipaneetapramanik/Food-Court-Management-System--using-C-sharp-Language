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
            // Mask the password by default, and make sure checkbox is unchecked
            textBox2.UseSystemPasswordChar = false;
            checkBox1.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // If no staff type is selected, check for admin login
            if (comboBox1.SelectedItem == null)
            {
                string inputAdminUsername = textBox1.Text.Trim();
                string inputAdminPassword = textBox2.Text.Trim();

                if (string.IsNullOrEmpty(inputAdminUsername))
                {
                    MessageBox.Show("Admin username is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(inputAdminPassword))
                {
                    MessageBox.Show("Password is empty.");
                    return;
                }

                string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
                using (OracleConnection con = new OracleConnection(conString))
                {
                    try
                    {
                        con.Open();
                        string query = "SELECT PASSWORD FROM admin_panel WHERE ADMIN_USERNAME = :adminUser";
                        using (OracleCommand cmd = new OracleCommand(query, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("adminUser", inputAdminUsername));
                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                string dbPassword = result.ToString();
                                if (dbPassword == inputAdminPassword)
                                {
                                    MessageBox.Show("Admin login successful!");
                                    admin adminForm = new admin();
                                    adminForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid admin password.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Admin username not found.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                return; // Skip the rest of the login logic
            }

            // The rest of your original code for staff types...
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

                string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
                using (OracleConnection con = new OracleConnection(conString))
                {
                    try
                    {
                        con.Open();
                        string query = "SELECT CONTACT_NUMBER, STALL_ID FROM food_stall WHERE OWNER_NAME = :ownerName";
                        using (OracleCommand cmd = new OracleCommand(query, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("ownerName", inputOwnerName));
                            using (OracleDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string dbContact = reader["CONTACT_NUMBER"].ToString();
                                    int stallId = Convert.ToInt32(reader["STALL_ID"]);

                                    if (dbContact == inputContact)
                                    {
                                        MessageBox.Show("Owner login successful!");
                                        owner ownerForm = new owner(stallId);
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
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
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

                if (!int.TryParse(inputStallId, out int stallIdInt))
                {
                    MessageBox.Show("Stall ID must be a number.");
                    return;
                }

                string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
                using (OracleConnection con = new OracleConnection(conString))
                {
                    try
                    {
                        con.Open();

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
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
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
                    try
                    {
                        con.Open();
                        string query = "SELECT EMPLOYEE_ID FROM employee WHERE EMPLOYEE_NAME = :employeeName AND E_MOBILE_NUMBER = :mobile";
                        using (OracleCommand cmd = new OracleCommand(query, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("employeeName", inputEmployeeName));
                            cmd.Parameters.Add(new OracleParameter("mobile", inputMobile));
                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                int employeeId = Convert.ToInt32(result);
                                MessageBox.Show("Login successful!");
                                employee empForm = new employee(employeeId);
                                empForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid employee name or mobile number.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
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
                    try
                    {
                        con.Open();
                        string query = "SELECT STAFF_ID FROM STAFFS WHERE STAFF_NAME = :staffName AND STAFF_MOBILE_NUMBER = :mobile";
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
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selected staff type is not supported yet.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            welcome_page get = new welcome_page();
            get.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optionally, you can clear the textboxes or set the labels here based on selection
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // No need to put anything here for password masking logic
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Show/hide password
            textBox2.UseSystemPasswordChar = checkBox1.Checked;
        }
    }
}