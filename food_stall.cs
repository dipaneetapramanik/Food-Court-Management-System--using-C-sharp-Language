using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class food_stall : Form
    {
        private string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
        private string currentAdmin = "admin1"; 
        private DataTable stallTable;

        public food_stall()
        {
            InitializeComponent();
            this.Load += food_stall_Load;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void food_stall_Load(object sender, EventArgs e)
        {
            LoadAndSetupStalls();
        }

        private void LoadAndSetupStalls()
        {
            stallTable = GetStallData();
            SetupGrid(stallTable);
        }

        private DataTable GetStallData()
        {
            DataTable dt = new DataTable();
            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM food_stall ORDER BY stall_id", con);
                    oda.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stalls: " + ex.Message);
            }
            return dt;
        }

        private void SetupGrid(DataTable dt)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dt;
            if (dt.Columns.Contains("STALL_ID"))
                dataGridView1.Columns["STALL_ID"].HeaderText = "Stall ID";
            if (dt.Columns.Contains("STALL_LOCATION"))
                dataGridView1.Columns["STALL_LOCATION"].HeaderText = "Location";
            if (dt.Columns.Contains("STALL_NAME"))
                dataGridView1.Columns["STALL_NAME"].HeaderText = "Name";
            if (dt.Columns.Contains("OWNER_NAME"))
                dataGridView1.Columns["OWNER_NAME"].HeaderText = "Owner";
            if (dt.Columns.Contains("LICENSE_NUMBER"))
                dataGridView1.Columns["LICENSE_NUMBER"].HeaderText = "License";
            if (dt.Columns.Contains("CONTACT_NUMBER"))
                dataGridView1.Columns["CONTACT_NUMBER"].HeaderText = "Contact";
            if (dt.Columns.Contains("ADMIN_USERNAME"))
                dataGridView1.Columns["ADMIN_USERNAME"].HeaderText = "Admin";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["STALL_ID"].Value?.ToString();
                textBox3.Text = row.Cells["STALL_LOCATION"].Value?.ToString();
                textBox2.Text = row.Cells["STALL_NAME"].Value?.ToString();
                textBox4.Text = row.Cells["OWNER_NAME"].Value?.ToString();
                textBox5.Text = row.Cells["LICENSE_NUMBER"].Value?.ToString();
                textBox6.Text = row.Cells["CONTACT_NUMBER"].Value?.ToString();
            }
        }

        // ADD
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Please fill in all fields except Stall ID.");
                return;
            }

            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = @"INSERT INTO food_stall 
                        (STALL_ID, STALL_LOCATION, STALL_NAME, OWNER_NAME, LICENSE_NUMBER, CONTACT_NUMBER, ADMIN_USERNAME) 
                        VALUES (food_stall_seq.NEXTVAL, :location, :name, :owner, :license, :contact, :admin)
                        RETURNING STALL_ID INTO :newId";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add("location", textBox3.Text.Trim());
                        cmd.Parameters.Add("name", textBox2.Text.Trim());
                        cmd.Parameters.Add("owner", textBox4.Text.Trim());
                        cmd.Parameters.Add("license", textBox5.Text.Trim());
                        cmd.Parameters.Add("contact", textBox6.Text.Trim());
                        cmd.Parameters.Add("admin", currentAdmin);

                        var outId = new OracleParameter("newId", OracleDbType.Int32, ParameterDirection.Output);
                        cmd.Parameters.Add(outId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Food stall inserted successfully.");

                            int newStallId = Convert.ToInt32(outId.Value.ToString());
                            DataRow newRow = stallTable.NewRow();
                            newRow["STALL_ID"] = newStallId;
                            newRow["STALL_LOCATION"] = textBox3.Text.Trim();
                            newRow["STALL_NAME"] = textBox2.Text.Trim();
                            newRow["OWNER_NAME"] = textBox4.Text.Trim();
                            newRow["LICENSE_NUMBER"] = textBox5.Text.Trim();
                            newRow["CONTACT_NUMBER"] = textBox6.Text.Trim();
                            newRow["ADMIN_USERNAME"] = currentAdmin;
                            stallTable.Rows.Add(newRow);

                            ClearFields();
                            textBox2.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Insert failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting food stall: " + ex.Message);
            }
        }

        // UPDATE
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please select a stall to update.");
                return;
            }
            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = @"UPDATE food_stall SET
                        STALL_LOCATION = :location,
                        STALL_NAME = :name,
                        OWNER_NAME = :owner,
                        LICENSE_NUMBER = :license,
                        CONTACT_NUMBER = :contact
                        WHERE STALL_ID = :stallId";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add("location", textBox3.Text.Trim());
                        cmd.Parameters.Add("name", textBox2.Text.Trim());
                        cmd.Parameters.Add("owner", textBox4.Text.Trim());
                        cmd.Parameters.Add("license", textBox5.Text.Trim());
                        cmd.Parameters.Add("contact", textBox6.Text.Trim());
                        cmd.Parameters.Add("stallId", textBox1.Text.Trim());

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Food stall updated successfully.");
                            // Reload datatable to reflect latest data
                            LoadAndSetupStalls();
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("No such stall_id found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating food stall: " + ex.Message);
            }
        }

        // DELETE (delete child rows before parent)
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one row to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete the selected stalls and all their child records?",
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
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (row.Cells["STALL_ID"].Value != null)
                        {
                            string stallId = row.Cells["STALL_ID"].Value.ToString();

                            // 1. Delete payments for orders of this stall
                            string deletePayments = @"DELETE FROM payment 
                        WHERE order_id IN (SELECT order_id FROM orders WHERE stall_id = :stallId)";
                            using (OracleCommand cmdPay = new OracleCommand(deletePayments, con))
                            {
                                cmdPay.Parameters.Add("stallId", stallId);
                                cmdPay.ExecuteNonQuery();
                            }

                           

                            // 4. Delete menu items for this stall
                            string deleteMenu = "DELETE FROM menu_item WHERE stall_id = :stallId";
                            using (OracleCommand cmdMenu = new OracleCommand(deleteMenu, con))
                            {
                                cmdMenu.Parameters.Add("stallId", stallId);
                                cmdMenu.ExecuteNonQuery();
                            }

                            // 5. Delete employees for this stall
                            string deleteEmp = "DELETE FROM employee WHERE stall_id = :stallId";
                            using (OracleCommand cmdEmp = new OracleCommand(deleteEmp, con))
                            {
                                cmdEmp.Parameters.Add("stallId", stallId);
                                cmdEmp.ExecuteNonQuery();
                            }

                            // 6. Delete orders for this stall
                            string deleteOrders = "DELETE FROM orders WHERE stall_id = :stallId";
                            using (OracleCommand cmdOrder = new OracleCommand(deleteOrders, con))
                            {
                                cmdOrder.Parameters.Add("stallId", stallId);
                                cmdOrder.ExecuteNonQuery();
                            }

                            // 7. Delete the stall itself
                            string deleteStall = "DELETE FROM food_stall WHERE stall_id = :stallId";
                            using (OracleCommand cmdStall = new OracleCommand(deleteStall, con))
                            {
                                cmdStall.Parameters.Add("stallId", stallId);
                                cmdStall.ExecuteNonQuery();
                            }
                        }
                    }
                }
                MessageBox.Show("Selected stalls and all their child records deleted.");
                LoadAndSetupStalls();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting food stall: " + ex.Message);
            }
        }
        // SEARCH
        private void button5_Click(object sender, EventArgs e)
        {
            // Reload datatable with latest data from DB (removes all filters and shows updated records)
            LoadAndSetupStalls();
            ClearFields();
        }

        // CLEAR/RESET FILTER ONLY
        private void button6_Click(object sender, EventArgs e)
        {
            ClearFields();
            if (stallTable != null)
                stallTable.DefaultView.RowFilter = "";
        }

        // BACK to admin
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin adminForm = new admin();
            adminForm.Show();
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        // Designer event handler stubs
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
    }
}