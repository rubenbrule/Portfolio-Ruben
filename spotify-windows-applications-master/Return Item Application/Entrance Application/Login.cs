using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrance_Application
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// User can just only login this application as a staff at the entrance with 
        /// Password: entrance
        /// </summary>
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (cbx_rights.Text == "Entrance Staff")
            {
                if (txt_password.Text == "entrance")
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

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
