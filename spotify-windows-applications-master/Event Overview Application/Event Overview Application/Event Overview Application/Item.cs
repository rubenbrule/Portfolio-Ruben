using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Overview_Application
{
    class Item
    {
        //fields
        private int item_id;
        private string item_name;
        private int store_id;
        private string category;
        private double item_price;
        private int item_quantity;

        //properties
        public int Item_id { set { item_id = value; }get { return item_id; } }
        public string Item_name { set { item_name = value; } get { return item_name; } }
        public string Category { set { category = value; } get { return category; } }
        public double Item_price { set { item_price = value; } get { return item_price; } }
        public int Item_quantity { set { item_quantity = value; } get { return item_quantity; } }
        public int Store_id { set { store_id = value; } get { return store_id; } }
        //constructor
        public Item(int id, string name, string category, double price, int quantity,int store_id)
        {
            this.item_id = id;
            this.item_name = name;
            this.category = category;
            this.item_price = price;
            this.item_quantity = quantity;
            this.store_id = store_id;
        }
        //methods
        public override string ToString()
        {
            return string.Format("Item: {0} - Price: {1}", item_name,item_price);
        }

    }
}
