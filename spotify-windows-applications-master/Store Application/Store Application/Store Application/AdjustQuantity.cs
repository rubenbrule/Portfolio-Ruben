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
    public partial class AdjustQuantity : Form
    {
        public bool delete;
        public AdjustQuantity()
        {
            InitializeComponent();
            delete = false;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            GetQuantity();
            this.Close();
        }
        public int GetQuantity() {
            try
            {
                return (int) numUpDown_quantity.Value;
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
            return -1;
        }

        private void AdjustQuantity_Load(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_deleteItem_Click(object sender, EventArgs e)
        {
            delete = true;
        }
    }
}
