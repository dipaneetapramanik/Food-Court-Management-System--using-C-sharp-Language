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
    public partial class role_choice : Form
    {
        public role_choice()
        {
            InitializeComponent();
            staff_button.FlatStyle = FlatStyle.Flat;
            staff_button.BackColor = Color.Transparent;
            staff_button.FlatAppearance.BorderSize = 0;
            staff_button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            staff_button.FlatAppearance.MouseDownBackColor = Color.Transparent;

            customer_button.FlatStyle = FlatStyle.Flat;
            customer_button.BackColor = Color.Transparent;
            customer_button.FlatAppearance.BorderSize = 0;
            customer_button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            customer_button.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void role_choice_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            staff_login customer_Login = new staff_login(); 
            customer_Login.Show(); 
            this.Hide(); 
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            stall_selection stall= new stall_selection(); 
            stall.Show(); 
            this.Hide();
        }
    }
}
