using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrance_Application
{
    class Item
    {
        //fields
        private int item_id;
        private string item_name;
        private string category;
        private double item_price;
        private int item_quantity;

        //constructor
        public Item(int id, string name, string category, double price, int quantity)
        {
            this.item_id = id;
            this.item_name = name;
            this.category = category;
            this.item_price = price;
            this.item_quantity = quantity;
        }
        //methods
        public override string ToString()
        {
            return string.Format("Item: {0} - Price: {1}", item_name,item_price);
        }

    }
}
