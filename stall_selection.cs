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
    public partial class stall_selection : Form
    {
        public stall_selection()
        {
            InitializeComponent();
            bfc.FlatStyle = FlatStyle.Flat;
            bfc.BackColor = Color.Transparent;
            bfc.FlatAppearance.BorderSize = 0;
            bfc.FlatAppearance.MouseOverBackColor = Color.Transparent;
            bfc.FlatAppearance.MouseDownBackColor = Color.Transparent;

            tasty_treat.FlatStyle = FlatStyle.Flat; 
            tasty_treat.BackColor = Color.Transparent;
            tasty_treat.FlatAppearance.BorderSize = 0;
            tasty_treat.FlatAppearance.MouseOverBackColor = Color.Transparent;
            tasty_treat.FlatAppearance.MouseDownBackColor = Color.Transparent;

            takeout.FlatStyle = FlatStyle.Flat;
            takeout.BackColor = Color.Transparent;
            takeout.FlatAppearance.BorderSize = 0;
            takeout.FlatAppearance.MouseOverBackColor = Color.Transparent;
            takeout.FlatAppearance.MouseDownBackColor = Color.Transparent;

            sultan_dine.FlatStyle = FlatStyle.Flat;
            sultan_dine.BackColor = Color.Transparent;
            sultan_dine.FlatAppearance.BorderSize = 0;
            sultan_dine.FlatAppearance.MouseOverBackColor = Color.Transparent;
            sultan_dine.FlatAppearance.MouseDownBackColor = Color.Transparent;

            cha_time.FlatStyle = FlatStyle.Flat;
            cha_time.BackColor = Color.Transparent;
            cha_time.FlatAppearance.BorderSize = 0;
            cha_time.FlatAppearance.MouseOverBackColor = Color.Transparent;
            cha_time.FlatAppearance.MouseDownBackColor = Color.Transparent;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            bfc_stall bfcStall = new bfc_stall(); 
            bfcStall.Show();
            this.Hide(); 
        }

        private void stall_selection_Load(object sender, EventArgs e)
        {

        }

        private void takeout_Click(object sender, EventArgs e)
        {
            takeout_stall takeoutStall = new takeout_stall();
            takeoutStall.Show(); 
                this.Hide(); 
        }

        private void cha_time_Click(object sender, EventArgs e)
        {
            cha_time_stall chaTimeStall = new cha_time_stall(); 
            chaTimeStall.Show();
            this.Hide(); 
        }

        private void tasty_treat_Click(object sender, EventArgs e)
        {
            tasty_treat_stall tastyTreatStall = new tasty_treat_stall();
            tastyTreatStall.Show();
            this.Hide();
        }

        private void sultan_dine_Click(object sender, EventArgs e)
        {
            sultan_dine_stall sultanDineStall = new sultan_dine_stall();
            sultanDineStall.Show();
            this.Hide();
        }
    }
}
