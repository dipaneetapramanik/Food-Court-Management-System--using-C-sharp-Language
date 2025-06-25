using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class Shop : Form
    {
        private int stallId;
        // Store indices of green rows
        private HashSet<int> greenRows = new HashSet<int>();

        public Shop(int stallId)
        {
            InitializeComponent();
            this.stallId = stallId;
        }

        private void Shop_Load(object sender, EventArgs e)
        {
            // Display Stall Name
            string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                string query = "SELECT STALL_NAME FROM food_stall WHERE STALL_ID = :stallId";
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    cmd.Parameters.Add(new OracleParameter("stallId", stallId));
                    object result = cmd.ExecuteScalar();
                    label2.Text = result != null ? result.ToString() : "Stall Not Found";
                }
            }
            LoadOrdersForStall();
        }

        private void LoadOrdersForStall()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                string query = "SELECT * FROM orders WHERE STALL_ID = :stallId";
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    cmd.Parameters.Add(new OracleParameter("stallId", stallId));
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        ApplyGreenRows();
                    }
                }
            }
        }

        // Color all green rows
        private void ApplyGreenRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                if (greenRows.Contains(row.Index))
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                else
                    row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Optional: Show more info
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Default behavior or custom if you want
        }

        // Mark selected rows as green
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                    greenRows.Add(row.Index);
            }
            ApplyGreenRows();
        }

        // Remove green color from ONLY the selected rows
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow && greenRows.Contains(row.Index))
                    greenRows.Remove(row.Index);
            }
            ApplyGreenRows();
        }

        private void button2_Click(object sender, EventArgs e)
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
    }
}