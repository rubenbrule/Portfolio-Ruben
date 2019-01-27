using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store_Application
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// User can just only login this application as a shop seller with 
        /// Password: shop
        /// </summary>
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (cbx_rights.Text == "Shop Seller")
            {
                if (txt_password.Text == "shop")
                {
                    this.Hide();
                    Form1 f = new Form1(); 
                    f.Show();
                }
                else {
                    MessageBox.Show("Wrong password", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                MessageBox.Show("You dont have right to Login", "Store Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
    }
}
