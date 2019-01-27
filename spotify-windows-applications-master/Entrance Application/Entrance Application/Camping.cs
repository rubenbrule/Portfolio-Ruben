using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrance_Application
{
    class Camping
    {
        //fields
        private int camping_id;
        private string status;

        //constructor
        public Camping(int id, string s) {
            this.camping_id = id;
            this.status = s;
        }
        //methods
        public override string ToString()
        {
            return string.Format("Camping Spot: {0} - status: {1}",camping_id,status);
        }
    }
}
