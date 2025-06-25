using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Food_Court_Management_System
{
    public partial class takeout_stall : Form
    {
        string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
        private int currentStallId; 

        public takeout_stall()
        {
            InitializeComponent();
        }

        private void takeout_stall_Load(object sender, EventArgs e)
        {
            currentStallId = 20;
            LoadMenuByStallId(currentStallId);
        }
        private void LoadMenuByStallId(int stallId)
        {
            currentStallId = stallId; 
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                string query = "SELECT ITEM_NAME, PRICE, DESCRIPTION, CATEGORY FROM menu_item WHERE STALL_ID = :stallId";
                using (OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(query, con))
                {
                    oracleDataAdapter.SelectCommand.Parameters.Add(new OracleParameter("stallId", stallId));

                    DataTable dataTable = new DataTable();
                    oracleDataAdapter.Fill(dataTable);
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.MultiSelect = true;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one item to proceed.");
                return;
            }
            DataTable dt = (DataTable)dataGridView1.DataSource;
            DataTable selectedTable = dt.Clone();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.DataBoundItem != null)
                {
                    DataRow dataRow = ((DataRowView)row.DataBoundItem).Row;
                    selectedTable.ImportRow(dataRow);
                }
            }
            confirmation confirmationForm = new confirmation(selectedTable, currentStallId, this);
            confirmationForm.Show();
       
        }
    }
}