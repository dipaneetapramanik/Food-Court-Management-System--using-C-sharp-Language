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
    public partial class customer_dashboard : Form
    {
        private string customerName;

        // Modified constructor to accept customer name
        public customer_dashboard(string name)
        {
            InitializeComponent();
            customerName = name;
            this.Load += customer_dashboard_Load;
        }

        private void customer_dashboard_Load(object sender, EventArgs e)
        {
            // Set the customer's name and color in label2
            label2.Text = $"Welcome, {customerName}!";
            label2.TextAlign = ContentAlignment.TopLeft;
            label2.ForeColor = Color.FromArgb(255, 176, 0); // Custom orange color
        }

    

        private void label2_Click(object sender, EventArgs e)
        {
        
        }
    }
}