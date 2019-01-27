using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrance_Application
{
    class RentalItem:Item
    {
        //fields
        private double deposit_price;
        private DateTime rentTime;
        private DateTime returnTime;

        //constructor
        public RentalItem(int id, string name, string category, double price, int quantity, double deposit, DateTime rent, DateTime returnn) : base(id, name, category, price, quantity) {
            this.deposit_price = deposit;
            this.rentTime = rent;
            this.returnTime = returnn;
        }

        //methods

    }
}
