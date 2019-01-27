using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Overview_Application
{
    class RentalItem:Item
    {
        //fields
        private double deposit_price;
        private DateTime rentTime;
        private DateTime returnTime;

        //constructor
        public RentalItem(int id, string name, string category, double price, int quantity,int store_id, double deposit, DateTime rent, DateTime returnn) : base(id, name, category, price, quantity, store_id) {
            this.deposit_price = deposit;
            this.rentTime = rent;
            this.returnTime = returnn;
        }

        //methods

    }
}
