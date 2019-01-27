using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrance_Application
{
    class PurchaseItem : Item
    {
        //fields
        private int purchase_quantity;
        private DateTime purchase_time;

        //constructor
        public PurchaseItem(int id, string name, string category, double price, int quantity, int purchase_quantity, DateTime time) : base(id, name, category, price, quantity) {
            this.purchase_quantity = purchase_quantity;
            this.purchase_time = time;
        }
        

        //methods
    }
}
