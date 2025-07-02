using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class menu_inspection : Form
    {
        private int stallId;
        private string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";

        public menu_inspection(int stallId)
        {
            InitializeComponent();
            this.stallId = stallId;
            // Ensure event is wired
            this.Load += menu_inspection_Load;
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void menu_inspection_Load(object sender, EventArgs e)
        {
            LoadMenuData();
        }

        private void LoadMenuData()
        {
            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = "SELECT * FROM menu_item WHERE STALL_ID = :stallId";
                    using (OracleDataAdapter da = new OracleDataAdapter(query, con))
                    {
                        da.SelectCommand.Parameters.Add("stallId", stallId);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading menu items: " + ex.Message);
            }
        }

        // Use CellClick for more reliable cell selection
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMenuId.Text = row.Cells["MENU_ID"].Value?.ToString() ?? "";
                txtItemName.Text = row.Cells["ITEM_NAME"].Value?.ToString() ?? "";
                txtCategory.Text = row.Cells["CATEGORY"].Value?.ToString() ?? "";
                txtPrice.Text = row.Cells["PRICE"].Value?.ToString() ?? "";
                txtDescription.Text = row.Cells["DESCRIPTION"].Value?.ToString() ?? "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // If this is not used, you may remove it.
        }

        private void ClearFields()
        {
            txtMenuId.Clear();
            txtItemName.Clear();
            txtCategory.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text) ||
                string.IsNullOrWhiteSpace(txtCategory.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Please fill in Item Name, Category, and Price.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price))
            {
                MessageBox.Show("Invalid price. Please enter a valid number.");
                return;
            }

            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = @"INSERT INTO menu_item 
                        (MENU_ID, ITEM_NAME, CATEGORY, PRICE, DESCRIPTION, STALL_ID) 
                        VALUES (menu_item_seq.NEXTVAL, :itemName, :category, :price, :description, :stallId)";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add("itemName", txtItemName.Text.Trim());
                        cmd.Parameters.Add("category", txtCategory.Text.Trim());
                        cmd.Parameters.Add("price", price);
                        cmd.Parameters.Add("description", txtDescription.Text.Trim());
                        cmd.Parameters.Add("stallId", stallId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Menu item added successfully!");
                            LoadMenuData();
                            ClearFields();
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

        private void btnClr_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one row to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete the selected items?",
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
                        if (row.Cells["MENU_ID"].Value != null)
                        {
                            int menuId = Convert.ToInt32(row.Cells["MENU_ID"].Value);
                            string query = "DELETE FROM menu_item WHERE MENU_ID = :menuId";
                            using (OracleCommand cmd = new OracleCommand(query, con))
                            {
                                cmd.Parameters.Add("menuId", menuId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Selected rows deleted.");
                LoadMenuData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting rows: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMenuId.Text))
            {
                MessageBox.Show("Please select a menu item to update.");
                return;
            }

            if (!int.TryParse(txtMenuId.Text.Trim(), out int menuId))
            {
                MessageBox.Show("Invalid Menu ID.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price))
            {
                MessageBox.Show("Invalid price. Please enter a valid number.");
                return;
            }

            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = @"UPDATE menu_item 
                                 SET ITEM_NAME = :itemName,
                                     CATEGORY = :category,
                                     PRICE = :price,
                                     DESCRIPTION = :description
                                 WHERE MENU_ID = :menuId";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        // Order of Add matters for Oracle!
                        cmd.Parameters.Add("itemName", txtItemName.Text.Trim());
                        cmd.Parameters.Add("category", txtCategory.Text.Trim());
                        cmd.Parameters.Add("price", price);
                        cmd.Parameters.Add("description", txtDescription.Text.Trim());
                        cmd.Parameters.Add("menuId", menuId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Menu item updated successfully!");
                            LoadMenuData();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = @"SELECT * FROM menu_item WHERE STALL_ID = :stallId";

                    if (!string.IsNullOrWhiteSpace(txtMenuId.Text))
                        query += " AND MENU_ID = :menuId";
                    if (!string.IsNullOrWhiteSpace(txtItemName.Text))
                        query += " AND ITEM_NAME LIKE :itemName";
                    if (!string.IsNullOrWhiteSpace(txtCategory.Text))
                        query += " AND CATEGORY LIKE :category";
                    if (!string.IsNullOrWhiteSpace(txtPrice.Text))
                        query += " AND PRICE LIKE :price";
                    if (!string.IsNullOrWhiteSpace(txtDescription.Text))
                        query += " AND DESCRIPTION LIKE :description";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add("stallId", stallId);

                        if (!string.IsNullOrWhiteSpace(txtMenuId.Text))
                            cmd.Parameters.Add("menuId", txtMenuId.Text.Trim());
                        if (!string.IsNullOrWhiteSpace(txtItemName.Text))
                            cmd.Parameters.Add("itemName", "%" + txtItemName.Text.Trim() + "%");
                        if (!string.IsNullOrWhiteSpace(txtCategory.Text))
                            cmd.Parameters.Add("category", "%" + txtCategory.Text.Trim() + "%");
                        if (!string.IsNullOrWhiteSpace(txtPrice.Text))
                            cmd.Parameters.Add("price", "%" + txtPrice.Text.Trim() + "%");
                        if (!string.IsNullOrWhiteSpace(txtDescription.Text))
                            cmd.Parameters.Add("description", "%" + txtDescription.Text.Trim() + "%");

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching menu items: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            owner own = new owner(stallId);
            own.Show();
            this.Hide();
        }
    }
}