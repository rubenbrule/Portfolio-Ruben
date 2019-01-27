using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrance_Application
{
    class Store
    {
        //fields
        private int id;
        private string name;
        private List<Item> listItem;

        //constructor
        public Store(int id, string name) {
            this.id = id;
            this.name = name;
            listItem = new List<Item>();
        }
        //methods
        public int getTotalItem() {

            //Todo
            return 1;
        }
        
        public Store getStore(int id) {//Get Store by Store_id
            if (this.id == id)
            {
                return this;
            }
            else return null; 
        }
        public double getTotalProfit() {
            //Todo
            return 1;
        }
        public List<Item> getAllItem() {
            return listItem;
        }
    }
}
