using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Application
{
    class Visitors
    {
        //fields
        private int id;
        private string firstName;
        private string lastName;
        private DateTime birthday;
        private string email;
        private string phone;
        private int ticket_id;
        private int spot_id;
        private string rfid;

        public Visitors(string fname, string lname, DateTime bd, string phone, string email, int ticketId, int campingId, string rfid) {
          //  this.id = id;
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
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime Birthday { set; get; }
        public string Email { set; get; }
        public string Phone { get; set; }
        public int Ticket_id { set; get; }
        public int Spot_id { set; get; }
        public string Rfid { set; get; }
        //methods
        public override string ToString() {
            return lastName + "," + firstName + " - " + birthday;
        }
        
    }
}
