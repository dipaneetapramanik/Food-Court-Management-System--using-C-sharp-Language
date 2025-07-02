using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class admin : Form
    {
        private int lastPressedButton = 0;
        // Change this connection string to match your Oracle configuration
        private string conString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=food_court;Password=leader;";

        public admin()
        {
            InitializeComponent();
        }

        // Button1: Load all food_stall table
        private void button1_Click(object sender, EventArgs e)
        {
            LoadTableToGrid("SELECT * FROM food_stall");
            lastPressedButton = 1;
        }

        // Button2: Load all employee info
        private void button2_Click(object sender, EventArgs e)
        {
            LoadTableToGrid("SELECT * FROM employee");
            lastPressedButton = 2;
        }

        // Button3: Load all orders
        private void button3_Click(object sender, EventArgs e)
        {
            LoadTableToGrid("SELECT * FROM orders");
            lastPressedButton = 3;
        }

        // Button4: Load all staffs
        private void button4_Click(object sender, EventArgs e)
        {
            LoadTableToGrid("SELECT * FROM staffs");
            lastPressedButton = 4;
        }

        // Button5: Load all menu_item
        private void button5_Click(object sender, EventArgs e)
        {
            LoadTableToGrid("SELECT * FROM menu_item");
            lastPressedButton = 5;
        }

        // Button6: Load all food_stall (same as button1)
        private void button6_Click(object sender, EventArgs e)
        {
            LoadTableToGrid("SELECT * FROM food_stall");
            lastPressedButton = 6;
        }

        // Button7: Open the new admin_owner form for managing stalls/owners
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            staff_login adm = new staff_login();

            adm.Show();
        }

        // Helper method to load any table to the DataGridView
        private void LoadTableToGrid(string query)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(conString))
                {
                    OracleDataAdapter oda = new OracleDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    oda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Add logic if you want actions on cell click
        }

        // Button8: Open related forms depending on last pressed button
        private void button8_Click(object sender, EventArgs e)
        {
            switch (lastPressedButton)
            {
                case 1:
                    // Open food_stall form
                    food_stall fsForm = new food_stall();
                    fsForm.Show();
                    break;
                    /*
                case 2:
                    // Open admin_employee form
                    admin_employee empForm = new admin_employee();
                    empForm.Show();
                    break;
                case 3:
                    // Open admin_orders form
                    admin_orders ordersForm = new admin_orders();
                    ordersForm.Show();
                    break;*/
                case 4:
                    // Open admin_staffs form
                    admin_staffs staffsForm = new admin_staffs();
                    staffsForm.Show();
                    break;
                    /*
                case 5:
                    // Open admin_ment form
                    admin_menu mentForm = new admin_menu();
                    mentForm.Show();
                    break;
                    */
                case 6:
                    // Open admin_owner form
                    admin_owner adm = new admin_owner();
                    adm.Show();
                    break;
                   

                default:
                    MessageBox.Show("Not AVAILABLE to modify");
                    break;
            }
        }

        private void admin_Load(object sender, EventArgs e)
        {

        }
    }
}