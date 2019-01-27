using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Overview_Application
{
    class Camping
    {
        //fields
        private int camping_id;
        private string location;

        //constructor
        public Camping(int id,string location) {
            this.camping_id = id;
            this.location = location;
        }

        //properties
        public int Camping_id { set { camping_id = value; }get { return camping_id; } }
        public string Location { set { location = value; } get { return location; } }
        //methods
        public override string ToString()
        {
            return string.Format("CS: {0}- {1} ",camping_id,location);
        }
    }
}
