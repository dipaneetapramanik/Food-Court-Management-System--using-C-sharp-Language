using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class order_history : Form
    {
        private string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";
        private int stallId;

        public order_history(int stallId)
        {
            InitializeComponent();
            this.stallId = stallId;
            this.Load += order_history_Load;
        }

        private void order_history_Load(object sender, EventArgs e)
        {
            LoadOrderData();
        }

        private void LoadOrderData()
        {
            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    con.Open();
                    string query = "SELECT * FROM orders WHERE STALL_ID = :stallId";
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
                MessageBox.Show("Error loading order history: " + ex.Message);
            }
        }
    }
}