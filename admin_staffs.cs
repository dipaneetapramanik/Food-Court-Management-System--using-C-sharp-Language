using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class admin_staffs : Form
    {
        private string connStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
        private int selectedStaffId = -1;

        public admin_staffs()
        {
            InitializeComponent();
        }

        private void admin_staffs_Load(object sender, EventArgs e)
        {
            LoadStaffs();
            StyleDataGridView();
        }

        private void LoadStaffs()
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT STAFF_ID, STAFF_NAME, STAFF_ROLE, STAFF_SALARY, STAFF_MOBILE_NUMBER, STAFF_ADDRESS, ADMIN_USERNAME FROM STAFFS";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading staffs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void StyleDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(195, 214, 172);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkGreen;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void ClearInputs()
        {
            staff_id_textbox.Text = "";
            staff_name_textbox.Text = "";
            staff_role_combobox.SelectedIndex = -1;
            staff_salary_textbox.Text = "";
            staff_mobile_textbox.Text = "";
            staff_address_textbox.Text = "";
            selectedStaffId = -1;
        }

        private void add_buttton_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(staff_name_textbox.Text) ||
                staff_role_combobox.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(staff_salary_textbox.Text) ||
                string.IsNullOrWhiteSpace(staff_mobile_textbox.Text) ||
                string.IsNullOrWhiteSpace(staff_address_textbox.Text)
            )
            {
                MessageBox.Show("All fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal salary;
            if (!decimal.TryParse(staff_salary_textbox.Text.Trim(), out salary))
            {
                MessageBox.Show("Staff salary must be a valid number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int newStaffId;
                    using (OracleCommand seqCmd = new OracleCommand("SELECT staffs_seq.NEXTVAL FROM dual", conn))
                        newStaffId = Convert.ToInt32(seqCmd.ExecuteScalar());

                    using (OracleCommand cmd = new OracleCommand(
                        @"INSERT INTO STAFFS (STAFF_ID, STAFF_NAME, STAFF_ROLE, STAFF_SALARY, STAFF_MOBILE_NUMBER, STAFF_ADDRESS, ADMIN_USERNAME)
                          VALUES (:id, :name, :role, :salary, :mobile, :address, :admin)", conn))
                    {
                        cmd.Parameters.Add(":id", newStaffId);
                        cmd.Parameters.Add(":name", staff_name_textbox.Text.Trim());
                        cmd.Parameters.Add(":role", staff_role_combobox.SelectedItem.ToString());
                        cmd.Parameters.Add(":salary", salary);
                        cmd.Parameters.Add(":mobile", staff_mobile_textbox.Text.Trim());
                        cmd.Parameters.Add(":address", staff_address_textbox.Text.Trim());
                        cmd.Parameters.Add(":admin", "admin6");
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show($"Staff added successfully! Generated Staff ID: {newStaffId}");
                    LoadStaffs();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding staff: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            if (selectedStaffId == -1)
            {
                MessageBox.Show("Please select a staff to update.", "Select Staff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (
                string.IsNullOrWhiteSpace(staff_name_textbox.Text) ||
                staff_role_combobox.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(staff_salary_textbox.Text) ||
                string.IsNullOrWhiteSpace(staff_mobile_textbox.Text) ||
                string.IsNullOrWhiteSpace(staff_address_textbox.Text)
            )
            {
                MessageBox.Show("All fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal salary;
            if (!decimal.TryParse(staff_salary_textbox.Text.Trim(), out salary))
            {
                MessageBox.Show("Staff salary must be a valid number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(
                        @"UPDATE STAFFS
                          SET STAFF_NAME=:name,
                              STAFF_ROLE=:role,
                              STAFF_SALARY=:salary,
                              STAFF_MOBILE_NUMBER=:mobile,
                              STAFF_ADDRESS=:address,
                              ADMIN_USERNAME=:admin
                          WHERE STAFF_ID=:id", conn))
                    {
                        cmd.Parameters.Add(":name", staff_name_textbox.Text.Trim());
                        cmd.Parameters.Add(":role", staff_role_combobox.SelectedItem.ToString());
                        cmd.Parameters.Add(":salary", salary);
                        cmd.Parameters.Add(":mobile", staff_mobile_textbox.Text.Trim());
                        cmd.Parameters.Add(":address", staff_address_textbox.Text.Trim());
                        cmd.Parameters.Add(":admin", "admin6");
                        cmd.Parameters.Add(":id", selectedStaffId);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Staff updated successfully!");
                    LoadStaffs();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating staff: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            if (selectedStaffId == -1)
            {
                MessageBox.Show("Please select a staff to delete.", "Select Staff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this staff?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM STAFFS WHERE STAFF_ID=:id", conn))
                        {
                            cmd.Parameters.Add(":id", selectedStaffId);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Staff deleted successfully!");
                        LoadStaffs();
                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting staff: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            var whereClauses = new List<string>();
            var parameters = new List<OracleParameter>();

            int parsedStaffId;
            if (!string.IsNullOrWhiteSpace(staff_id_textbox.Text) && int.TryParse(staff_id_textbox.Text.Trim(), out parsedStaffId))
            {
                whereClauses.Add("STAFF_ID = :staffid");
                parameters.Add(new OracleParameter(":staffid", parsedStaffId));
            }
            if (!string.IsNullOrWhiteSpace(staff_name_textbox.Text))
            {
                whereClauses.Add("LOWER(STAFF_NAME) LIKE :name");
                parameters.Add(new OracleParameter(":name", "%" + staff_name_textbox.Text.Trim().ToLower() + "%"));
            }
            if (staff_role_combobox.SelectedIndex != -1)
            {
                whereClauses.Add("LOWER(STAFF_ROLE) = :role");
                parameters.Add(new OracleParameter(":role", staff_role_combobox.SelectedItem.ToString().Trim().ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(staff_salary_textbox.Text))
            {
                decimal salary;
                if (decimal.TryParse(staff_salary_textbox.Text.Trim(), out salary))
                {
                    whereClauses.Add("STAFF_SALARY = :salary");
                    parameters.Add(new OracleParameter(":salary", salary));
                }
            }
            if (!string.IsNullOrWhiteSpace(staff_mobile_textbox.Text))
            {
                whereClauses.Add("STAFF_MOBILE_NUMBER LIKE :mobile");
                parameters.Add(new OracleParameter(":mobile", "%" + staff_mobile_textbox.Text.Trim() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(staff_address_textbox.Text))
            {
                whereClauses.Add("LOWER(STAFF_ADDRESS) LIKE :address");
                parameters.Add(new OracleParameter(":address", "%" + staff_address_textbox.Text.Trim().ToLower() + "%"));
            }

            string baseQuery = @"SELECT STAFF_ID, STAFF_NAME, STAFF_ROLE, STAFF_SALARY, STAFF_MOBILE_NUMBER, STAFF_ADDRESS, ADMIN_USERNAME FROM STAFFS";
            string finalQuery = baseQuery;

            if (whereClauses.Count > 0)
            {
                finalQuery += " WHERE " + string.Join(" AND ", whereClauses);
            }
            else
            {
                MessageBox.Show("Please enter at least one field to search.", "Input Needed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(finalQuery, conn))
                    {
                        foreach (var param in parameters)
                            cmd.Parameters.Add(param);

                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching staff: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            LoadStaffs();
            ClearInputs();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedStaffId = Convert.ToInt32(row.Cells["STAFF_ID"].Value);
                staff_id_textbox.Text = row.Cells["STAFF_ID"].Value.ToString();
                staff_name_textbox.Text = row.Cells["STAFF_NAME"].Value.ToString();
                staff_role_combobox.Text = row.Cells["STAFF_ROLE"].Value.ToString();
                staff_salary_textbox.Text = row.Cells["STAFF_SALARY"].Value.ToString();
                staff_mobile_textbox.Text = row.Cells["STAFF_MOBILE_NUMBER"].Value.ToString();
                staff_address_textbox.Text = row.Cells["STAFF_ADDRESS"].Value.ToString();
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Show previous form if needed
        }
    }
}