using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class admin_owner : Form
    {
        private string connStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
        private int selectedStallId = -1;

        public admin_owner()
        {
            InitializeComponent();
        }

        private void admin_owner_Load(object sender, EventArgs e)
        {
            LoadFoodStalls();
            StyleDataGridView();
        }

        private void LoadFoodStalls()
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT STALL_ID, STALL_LOCATION, STALL_NAME, OWNER_NAME, LICENSE_NUMBER, CONTACT_NUMBER, ADMIN_USERNAME FROM food_stall";
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
                    MessageBox.Show("Error loading food stalls: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            stall_id_textbox.Text = "";
            stall_name_textbox.Text = "";
            owner_name_textbox.Text = "";
            stall_location_tectbox.Text = "";
            licenese_number_textbox.Text = "";
            contact_number_textbox.Text = "";
            selectedStallId = -1;
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(stall_name_textbox.Text) ||
                string.IsNullOrWhiteSpace(owner_name_textbox.Text) ||
                string.IsNullOrWhiteSpace(stall_location_tectbox.Text) ||
                string.IsNullOrWhiteSpace(licenese_number_textbox.Text) ||
                string.IsNullOrWhiteSpace(contact_number_textbox.Text)
            )
            {
                MessageBox.Show("All fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int newStallId;
                    using (OracleCommand seqCmd = new OracleCommand("SELECT food_stall_seq.NEXTVAL FROM dual", conn))
                        newStallId = Convert.ToInt32(seqCmd.ExecuteScalar());

                    using (OracleCommand cmd = new OracleCommand(
                        @"INSERT INTO food_stall (STALL_ID, STALL_LOCATION, STALL_NAME, OWNER_NAME, LICENSE_NUMBER, CONTACT_NUMBER, ADMIN_USERNAME)
                  VALUES (:id, :location, :name, :owner, :license, :contact, :admin)", conn))
                    {
                        cmd.Parameters.Add(":id", newStallId);
                        cmd.Parameters.Add(":location", stall_location_tectbox.Text.Trim());
                        cmd.Parameters.Add(":name", stall_name_textbox.Text.Trim());
                        cmd.Parameters.Add(":owner", owner_name_textbox.Text.Trim());
                        cmd.Parameters.Add(":license", licenese_number_textbox.Text.Trim());
                        cmd.Parameters.Add(":contact", contact_number_textbox.Text.Trim());
                        cmd.Parameters.Add(":admin", "admin6"); // If ADMIN_USERNAME has a dedicated textbox, use it here.
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show($"Stall added successfully! Generated Stall ID: {newStallId}");
                    LoadFoodStalls();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding stall: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            if (selectedStallId == -1)
            {
                MessageBox.Show("Please select a stall to update.", "Select Stall", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (
                string.IsNullOrWhiteSpace(stall_name_textbox.Text) ||
                string.IsNullOrWhiteSpace(owner_name_textbox.Text) ||
                string.IsNullOrWhiteSpace(stall_location_tectbox.Text) ||
                string.IsNullOrWhiteSpace(licenese_number_textbox.Text) ||
                string.IsNullOrWhiteSpace(contact_number_textbox.Text)
            )
            {
                MessageBox.Show("All fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(
                        @"UPDATE food_stall
                          SET STALL_LOCATION=:location,
                              STALL_NAME=:name,
                              OWNER_NAME=:owner,
                              LICENSE_NUMBER=:license,
                              CONTACT_NUMBER=:contact,
                              ADMIN_USERNAME=:admin
                          WHERE STALL_ID=:id", conn))
                    {
                        cmd.Parameters.Add(":location", stall_location_tectbox.Text.Trim());
                        cmd.Parameters.Add(":name", stall_name_textbox.Text.Trim());
                        cmd.Parameters.Add(":owner", owner_name_textbox.Text.Trim());
                        cmd.Parameters.Add(":license", licenese_number_textbox.Text.Trim());
                        cmd.Parameters.Add(":contact", contact_number_textbox.Text.Trim());
                        cmd.Parameters.Add(":admin", "admin6");
                        cmd.Parameters.Add(":id", selectedStallId);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Stall updated successfully!");
                    LoadFoodStalls();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating stall: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            if (selectedStallId == -1)
            {
                MessageBox.Show("Please select a stall to delete.", "Select Stall", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this stall?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        using (OracleCommand cmd = new OracleCommand("DELETE FROM food_stall WHERE STALL_ID=:id", conn))
                        {
                            cmd.Parameters.Add(":id", selectedStallId);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Stall deleted successfully!");
                        LoadFoodStalls();
                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting stall: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            LoadFoodStalls();
            ClearInputs();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedStallId = Convert.ToInt32(row.Cells["STALL_ID"].Value);
                stall_id_textbox.Text = row.Cells["STALL_ID"].Value.ToString();
                stall_location_tectbox.Text = row.Cells["STALL_LOCATION"].Value.ToString();
                stall_name_textbox.Text = row.Cells["STALL_NAME"].Value.ToString();
                owner_name_textbox.Text = row.Cells["OWNER_NAME"].Value.ToString();
                licenese_number_textbox.Text = row.Cells["LICENSE_NUMBER"].Value.ToString();
                contact_number_textbox.Text = row.Cells["CONTACT_NUMBER"].Value.ToString();
                // If you have an ADMIN_USERNAME textbox, set it here.
                // admin_username_textbox.Text = row.Cells["ADMIN_USERNAME"].Value.ToString();
            }
        }

        private void stall_id_textbox_TextChanged(object sender, EventArgs e) { }
        private void stall_name_textbox_TextChanged(object sender, EventArgs e) { }
        private void owner_name_textbox_TextChanged(object sender, EventArgs e) { }
        private void stall_location_tectbox_TextChanged(object sender, EventArgs e) { }
        private void licenese_number_textbox_TextChanged(object sender, EventArgs e) { }
        private void contact_number_textbox_TextChanged(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e)
        {
            // Collect all non-empty search fields
            var whereClauses = new List<string>();
            var parameters = new List<OracleParameter>();

            // Search by STALL_ID (exact match if integer entered)
            int parsedStallId;
            if (!string.IsNullOrWhiteSpace(stall_id_textbox.Text) && int.TryParse(stall_id_textbox.Text.Trim(), out parsedStallId))
            {
                whereClauses.Add("STALL_ID = :stallid");
                parameters.Add(new OracleParameter(":stallid", parsedStallId));
            }

            if (!string.IsNullOrWhiteSpace(stall_name_textbox.Text))
            {
                whereClauses.Add("LOWER(STALL_NAME) LIKE :stallname");
                parameters.Add(new OracleParameter(":stallname", "%" + stall_name_textbox.Text.Trim().ToLower() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(owner_name_textbox.Text))
            {
                whereClauses.Add("LOWER(OWNER_NAME) LIKE :ownername");
                parameters.Add(new OracleParameter(":ownername", "%" + owner_name_textbox.Text.Trim().ToLower() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(stall_location_tectbox.Text))
            {
                whereClauses.Add("LOWER(STALL_LOCATION) LIKE :location");
                parameters.Add(new OracleParameter(":location", "%" + stall_location_tectbox.Text.Trim().ToLower() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(licenese_number_textbox.Text))
            {
                whereClauses.Add("LOWER(LICENSE_NUMBER) LIKE :license");
                parameters.Add(new OracleParameter(":license", "%" + licenese_number_textbox.Text.Trim().ToLower() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(contact_number_textbox.Text))
            {
                whereClauses.Add("LOWER(CONTACT_NUMBER) LIKE :contact");
                parameters.Add(new OracleParameter(":contact", "%" + contact_number_textbox.Text.Trim().ToLower() + "%"));
            }
            // Add more fields as needed, e.g. for admin_username_textbox if you have one

            string baseQuery = @"SELECT STALL_ID, STALL_LOCATION, STALL_NAME, OWNER_NAME, LICENSE_NUMBER, CONTACT_NUMBER, ADMIN_USERNAME FROM food_stall";
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
                    MessageBox.Show("Error searching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin a = new admin();
            a.Show();
            this.Hide();
        }

        private void load_button_Click_1(object sender, EventArgs e)
        {
            LoadFoodStalls();
            ClearInputs();
        }
    }
}