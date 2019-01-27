using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Application
{
    class PurchaseItem : Item
    {
        //fields
        private int purchase_quantity;
        private DateTime purchase_time;

        //constructor
        public PurchaseItem(int id, string name, string category, decimal price, int quantity,decimal deposit, int purchase_quantity, DateTime time) : base(id, name, category, price, quantity,deposit) {
            this.purchase_quantity = purchase_quantity;
            this.purchase_time = time;
        }
        //methods
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
