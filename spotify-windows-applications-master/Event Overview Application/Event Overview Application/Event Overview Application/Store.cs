using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Overview_Application
{
    class Store
    {
        //fields
        private int id;
        private string name;

        //constructor
        public Store(int id, string name) {
            this.id = id;
            this.name = name;
        }
        //properties
        public int Id { set { id = value; }get { return id; } }
        public string Name { set { name = value; }get { return name; } }


        //methods
        public override string ToString()
        {
              return this.id.ToString() + "-" + this.name;
        }
    }
}
