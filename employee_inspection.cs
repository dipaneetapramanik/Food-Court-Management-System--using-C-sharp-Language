using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Food_Court_Management_System
{
    public partial class employee_inspection : Form
    {
        private int stallId;
        private string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
        private DataTable employeeTable;

        public employee_inspection(int stallId)
        {
            InitializeComponent();
            this.stallId = stallId;

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Chef", "Waiter", "Cashier", "Baker", "Cleaner" });

            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Load += employee_inspection_Load;
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void employee_inspection_Load(object sender, EventArgs e)
        {
            employeeTable = GetEmployeeData();
            SetupGrid(employeeTable);
        }

        private DataTable GetEmployeeData()
        {
            DataTable dt = new DataTable();
            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = "SELECT * FROM employee WHERE STALL_ID = :stallId";
                    using (OracleDataAdapter da = new OracleDataAdapter(query, con))
                    {
                        da.SelectCommand.Parameters.Add("stallId", stallId);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message);
            }
            return dt;
        }

        private void SetupGrid(DataTable dt)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dt;
            // Optional: Set column headers for better look
            if (dt.Columns.Contains("EMPLOYEE_ID"))
                dataGridView1.Columns["EMPLOYEE_ID"].HeaderText = "ID";
            if (dt.Columns.Contains("EMPLOYEE_NAME"))
                dataGridView1.Columns["EMPLOYEE_NAME"].HeaderText = "Name";
            if (dt.Columns.Contains("EMPLOYEE_ROLE"))
                dataGridView1.Columns["EMPLOYEE_ROLE"].HeaderText = "Role";
            if (dt.Columns.Contains("EMPLOYEE_ADDRESS"))
                dataGridView1.Columns["EMPLOYEE_ADDRESS"].HeaderText = "Address";
            if (dt.Columns.Contains("EMPLOYEE_SALARY"))
                dataGridView1.Columns["EMPLOYEE_SALARY"].HeaderText = "Salary";
            if (dt.Columns.Contains("E_MOBILE_NUMBER"))
                dataGridView1.Columns["E_MOBILE_NUMBER"].HeaderText = "Mobile";
            if (dt.Columns.Contains("STALL_ID"))
                dataGridView1.Columns["STALL_ID"].HeaderText = "Stall ID";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["EMPLOYEE_ID"].Value?.ToString();
                textBox2.Text = row.Cells["EMPLOYEE_NAME"].Value?.ToString();
                comboBox1.SelectedItem = row.Cells["EMPLOYEE_ROLE"].Value?.ToString();
                textBox4.Text = row.Cells["EMPLOYEE_ADDRESS"].Value?.ToString();
                textBox5.Text = row.Cells["EMPLOYEE_SALARY"].Value?.ToString();
                textBox6.Text = row.Cells["E_MOBILE_NUMBER"].Value?.ToString();
            }
        }

        // ADD
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedItem == null ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Please fill in all fields except Employee ID.");
                return;
            }

            // Validate salary as decimal
            decimal salary;
            if (!decimal.TryParse(textBox5.Text.Trim(), out salary))
            {
                MessageBox.Show("Salary must be a valid number.");
                return;
            }

            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = @"INSERT INTO employee 
                        (EMPLOYEE_ID, EMPLOYEE_NAME, EMPLOYEE_ROLE, EMPLOYEE_ADDRESS, EMPLOYEE_SALARY, E_MOBILE_NUMBER, STALL_ID)
                        VALUES (employee_seq.NEXTVAL, :name, :role, :address, :salary, :mobile, :stallId)
                        RETURNING EMPLOYEE_ID INTO :newId";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add("name", textBox2.Text.Trim());
                        cmd.Parameters.Add("role", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.Add("address", textBox4.Text.Trim());
                        cmd.Parameters.Add("salary", salary); // use decimal here
                        cmd.Parameters.Add("mobile", textBox6.Text.Trim());
                        cmd.Parameters.Add("stallId", stallId);

                        var outId = new OracleParameter("newId", OracleDbType.Int32, ParameterDirection.Output);
                        cmd.Parameters.Add(outId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Employee added successfully!");

                            int newEmpId = Convert.ToInt32(outId.Value.ToString());
                            DataRow newRow = employeeTable.NewRow();
                            newRow["EMPLOYEE_ID"] = newEmpId;
                            newRow["EMPLOYEE_NAME"] = textBox2.Text.Trim();
                            newRow["EMPLOYEE_ROLE"] = comboBox1.SelectedItem.ToString();
                            newRow["EMPLOYEE_ADDRESS"] = textBox4.Text.Trim();
                            newRow["EMPLOYEE_SALARY"] = salary;
                            newRow["E_MOBILE_NUMBER"] = textBox6.Text.Trim();
                            newRow["STALL_ID"] = stallId;
                            employeeTable.Rows.Add(newRow);

                            ClearFields();
                            textBox2.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Add failed. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // DELETE
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one row to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete the selected employees?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No)
                return;

            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (row.Cells["EMPLOYEE_ID"].Value != null)
                        {
                            string empId = row.Cells["EMPLOYEE_ID"].Value.ToString();
                            string query = "DELETE FROM employee WHERE EMPLOYEE_ID = :empId AND STALL_ID = :stallId";
                            using (OracleCommand cmd = new OracleCommand(query, con))
                            {
                                cmd.Parameters.Add("empId", empId);
                                cmd.Parameters.Add("stallId", stallId);
                                cmd.ExecuteNonQuery();
                            }
                            rowsToRemove.Add(row);
                        }
                    }
                    // Remove from DataTable (which updates DataGridView)
                    foreach (var row in rowsToRemove)
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                }

                MessageBox.Show("Selected employees deleted.");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting employees: " + ex.Message);
            }
        }

        // SEARCH (multi-field, matches all filled fields)
        private void button3_Click(object sender, EventArgs e)
        {
            if (employeeTable == null) return;

            var filters = new List<string>();

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
                filters.Add($"CONVERT(EMPLOYEE_ID, 'System.String') LIKE '%{textBox1.Text.Trim()}%'");
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
                filters.Add($"EMPLOYEE_NAME LIKE '%{textBox2.Text.Trim()}%'");
            if (!string.IsNullOrWhiteSpace(textBox4.Text))
                filters.Add($"EMPLOYEE_ADDRESS LIKE '%{textBox4.Text.Trim()}%'");
            if (!string.IsNullOrWhiteSpace(textBox5.Text))
                filters.Add($"CONVERT(EMPLOYEE_SALARY, 'System.String') LIKE '%{textBox5.Text.Trim()}%'");
            if (!string.IsNullOrWhiteSpace(textBox6.Text))
                filters.Add($"E_MOBILE_NUMBER LIKE '%{textBox6.Text.Trim()}%'");
            if (comboBox1.SelectedItem != null && !string.IsNullOrWhiteSpace(comboBox1.Text))
                filters.Add($"EMPLOYEE_ROLE LIKE '%{comboBox1.Text.Trim()}%'");

            string filterString = string.Join(" AND ", filters);

            DataView dv = employeeTable.DefaultView;
            dv.RowFilter = filterString;
        }

        // UPDATE
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please select an employee to update.");
                return;
            }

            // Validate salary as decimal
            decimal salary;
            if (!decimal.TryParse(textBox5.Text.Trim(), out salary))
            {
                MessageBox.Show("Salary must be a valid number.");
                return;
            }

            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = @"UPDATE employee SET 
                        EMPLOYEE_NAME = :name,
                        EMPLOYEE_ROLE = :role,
                        EMPLOYEE_ADDRESS = :address,
                        EMPLOYEE_SALARY = :salary,
                        E_MOBILE_NUMBER = :mobile
                        WHERE EMPLOYEE_ID = :empId AND STALL_ID = :stallId";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add("name", textBox2.Text.Trim());
                        cmd.Parameters.Add("role", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.Add("address", textBox4.Text.Trim());
                        cmd.Parameters.Add("salary", salary); // use decimal here
                        cmd.Parameters.Add("mobile", textBox6.Text.Trim());
                        cmd.Parameters.Add("empId", textBox1.Text.Trim());
                        cmd.Parameters.Add("stallId", stallId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Employee updated successfully!");

                            // Update the selected row in DataGridView
                            if (dataGridView1.SelectedRows.Count > 0)
                            {
                                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                                selectedRow.Cells["EMPLOYEE_NAME"].Value = textBox2.Text.Trim();
                                selectedRow.Cells["EMPLOYEE_ROLE"].Value = comboBox1.SelectedItem.ToString();
                                selectedRow.Cells["EMPLOYEE_ADDRESS"].Value = textBox4.Text.Trim();
                                selectedRow.Cells["EMPLOYEE_SALARY"].Value = salary;
                                selectedRow.Cells["E_MOBILE_NUMBER"].Value = textBox6.Text.Trim();
                            }
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // CLEAR / BACK
        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
            if (employeeTable != null)
            {
                employeeTable.DefaultView.RowFilter = "";
            }
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        // Event handler stubs for designer
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void employee_inspection_Load_1(object sender, EventArgs e) { }

        private void button6_Click(object sender, EventArgs e)
        {
            if (employeeTable != null)
            {
                employeeTable.DefaultView.RowFilter = ""; // Show all data again
            }
        }
    }
}