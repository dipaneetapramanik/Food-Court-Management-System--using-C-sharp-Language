using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Food_Court_Management_System
{
    public partial class thanks : Form
    {
  
        public thanks(DataTable selectedItems, int stallId, decimal toPay)
        {
            InitializeComponent();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
           welcome_page wel= new welcome_page();
            wel.Show();
            this.Hide();

        }

        private void thanks_Load(object sender, EventArgs e)
        {

        }
    }
}
