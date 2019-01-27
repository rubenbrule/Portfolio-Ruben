using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Overview_Application
{
    class Visitors
    {
        //fields
        private int id;
        private string firstName;
        private string lastName;
        private string birthday;
        private string email;
        private string phone;
        private string ticket_id;
        private int spot_id;
        private string rfid;

        public Visitors() { }
        public Visitors(int id,string fname, string lname, string bd, string phone, string email, string ticketId, int campingId, string rfid) {
           this.id = id;
            this.firstName = fname;
            this.lastName = lname;
            this.birthday = bd;
            this.phone = phone;
            this.email = email;
            this.ticket_id = ticketId;
            this.spot_id = campingId;
            this.rfid = rfid;
        }
        //properties
        public int Id { set { id = value; } get { return id; } }
        public string FirstName { set { firstName = value; } get { return firstName; } }
        public string LastName { set { lastName = value; } get { return lastName; } }
        //  public DateTime Birthday { set; get; }
        // public string Birthday { set { birthday = value; } get { return birthday; } }
        public string Email { set { email = value; } get { return email; } }
        public string Phone { set { phone = value; } get { return phone; } }
        public string Ticket_id { set { ticket_id = value; } get { return ticket_id; } }
        public int Spot_id { set { spot_id = value; } get { return spot_id; } }
        public string Rfid { set { rfid = value; } get { return rfid; } }
        public string Birthday { set { birthday = value; } get { return birthday; } }
        //methods
        public override string ToString()
        {
            return lastName + "," + firstName;
        }

    }
}
