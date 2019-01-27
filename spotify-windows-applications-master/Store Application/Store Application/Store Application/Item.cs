using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Application
{
    class Item
    {
        //fields
        private int item_id;
        private string item_name;
        private string category;
        private decimal item_price;
        private int item_quantity;
        private decimal deposit;

        //constructor
        public Item(int id, string name, string category, decimal price, int quantity, decimal deposit)
        {
            this.item_id = id;
            this.item_name = name;
            this.category = category;
            this.item_price = price;
            this.item_quantity = quantity;
            this.deposit = deposit;
        }
        //properties
        public int Item_id { set { item_id = value; }get { return item_id; } }
        public string Item_name { set { item_name = value; } get { return item_name; } }
        public string Category { set { category = value; } get { return category; } }
        public decimal Item_price { set { item_price = value; } get { return item_price; } }
        public int Item_quantity { set { item_quantity = value; } get { return item_quantity; } }
        public decimal Deposit { set { deposit = value; } get { return deposit; } }
        //methods
        public override string ToString()
        {
            return string.Format("{0}-Item: {1}-Price: {2}", item_name,item_price);
        }




        

    }
}
