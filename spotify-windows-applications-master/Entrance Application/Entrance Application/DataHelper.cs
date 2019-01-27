using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using MySql;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Entrance_Application 
{
     class DataHelper : IData
    {
        //fields
        private string connectionInfo;
        private MySqlConnection connection;
        Visitors v;
        //print
       


        //constructor
        public DataHelper()
        {
            connectionInfo = "server=studmysql01.fhict.local;" +
                               "database=dbi361552;" +
                               "user id=dbi361552;" +
                               "password=p@55w0rdn0tf0und;" +
                               "connect timeout=3;";
            connection = new MySqlConnection(connectionInfo);
        }
        //****************************************methods*************************************//
        /// <summary>
        /// Methods for Entrance app
        /// </summary>
        /// //Get Visistor by RFID 
        public Visitors getVisitorByRfid(string rfid) {
            string sql = string.Format("SELECT visitor_id, ticket_id,ifnull(rfid,'null'),ifnull(spot_id,''),first_name,last_name,birthday,ifnull(email,''),ifnull(password,''),ifnull(phone,''),balance FROM visitor  where rfid='{0}'",rfid);
          //  int spot = 0;
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["visitor_id"]);
                    string fname = (string)(reader["first_name"]);
                    string ln = (string)(reader["last_name"]);
                 //   string bd= (string)(reader["birthday"]);
                    string phone = null;
                    string email = (string)(reader["ifnull(email,'')"]);
                    string ticket = (string)(reader["ticket_id"]);
                  
                      //  spot = Convert.ToInt32(reader["spot_id"]);
                    
                    string rrfid = (string)(reader["ifnull(rfid,'null')"]);
                    //  v = new Visitors(Convert.ToString(reader["first_name"]), Convert.ToString(reader["last_name"]), Convert.ToDateTime(reader["birthday"]), Convert.ToString(reader["phone"]), Convert.ToString(reader["email"]), Convert.ToString(reader["ticket_id"]), Convert.ToString(reader["spot_id"]), Convert.ToString(reader["rfid"]));
                    v = new Visitors(id, fname, ln, phone, email, ticket, 0, rrfid);
                }
            }
            catch (MySqlException e) {
                MessageBox.Show(e.Message);
            }
            finally {if(connection!=null)
                    connection.Close(); }
           return v;
        }

        //Get Message for RFID
        public List<string> GetMessageForRFID(string rfid) {
            List<string> list = new List<string>();
            string sql = string.Format("SELECT checking_action, checking_location from visitor v join checking_history c on v.visitor_id = c.visitor_id where v.rfid='{0}'",rfid);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(Convert.ToString(reader[0]));
                    list.Add(Convert.ToString(reader[1]));
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally {
                connection.Close();
            }
            return list;
        }
        //Insert new row for checking_history
        public void InsertCheckingHistory(string rfid,string inOut)
        {
            Visitors v = this.getVisitorByRfid(rfid);
            int v_id = v.Id;
            DateTime d = DateTime.Now;
            string s = d.ToString("yyyy-MM-dd HH:MM:ss");
            string sql = string.Format("Insert into checking_history (history_id,visitor_id,checking_action,checking_location,checking_time) values(null,{0},'{1}','ENTRANCE',NOW())",v_id,inOut);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally { connection.Close(); }
        }

        //Update checkin status when check in
        public void UpdateWhenCheckIn(string rfid) {
            Visitors v = this.getVisitorByRfid(rfid);
            int v_id = v.Id;
            string sql = string.Format("UPDATE checking_history SET checking_action='IN' where visitor_id={0}", v_id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException e) {
                MessageBox.Show(e.Message);
            }
            finally { connection.Close(); }
        }

        //Update Check-out status when check out
        public void UpdateWhenCheckOut(string rfid) {
            int id = this.getVisitorIdbyRFID(rfid);
                string sql = string.Format("UPDATE checking_history SET checking_action='OUT' where visitor_id={0}", id);
                MySqlCommand command = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message);
                }
                finally { connection.Close(); }
            
        }
        //Get Balance of a specific visitor
        public decimal GetBalanceOfaVisitor(string id) {
            decimal  balance = 0;
            string sql = string.Format("SELECT balance FROM visitor where rfid='{0}'",id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                balance =Convert.ToDecimal( command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return balance;
        }
        //Get All RFID code that has been assigned to visitor (valid RFID)
        public List<string> GetValidRFID() {
            List<string> list = new List<string>();
            string sql = "SELECT rfid FROM visitor where rfid is not null";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    list.Add((string)reader[0]);
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return list;
        }
        //Check is it a valid RFID
        public bool CheckRFID(string rfid) {
            foreach (string str in this.GetValidRFID()) {
                if (str == rfid)
                    return true;
            }
            return false;
        }

        //3/12
        ///check if visitor return item yet
        public bool CheckRentalStatus(string rfid) {
            List<string> list = new List<string>();
            Visitors v = this.getVisitorByRfid(rfid);
            string sql = string.Format("SELECT IFNULL(return_time,'Not return yet') FROM rental WHERE visitor_id={0}", v.Id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    list.Add((string)reader["IFNULL(return_time,'Not return yet')"]);
                }
                foreach (string s in list) {
                    if (s == "Not return yet")
                        return false;
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return true;
        }
        //Delete an rfid when visitor check out and withdraw money
        public void DeleteRfid(int id) {
            string sql = string.Format("update visitor set rfid=null , balance=0 where visitor_id={0}",id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Something wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.Message);
            }
            finally {
                connection.Close();
            }
        }
     
        //Get visitor Id by rfid
        public int getVisitorIdbyRFID(string rfid) {
            int id = 0;
            string sql = string.Format("SELECT visitor_id FROM visitor WHERE rfid='{0}'", rfid);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
            return id;
        }
        //Get list of visitor_id of visitors borrow items
        public List<int> ListVisitorBorrowedItems() {
            List<int> list = new List<int>();
            string sql = string.Format("SELECT visitor_id FROM rental");
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    list.Add(Convert.ToInt32(reader["visitor_id"]));
                }
            } catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
            return list;
        }
        //check if visitor borrow item or not
        public bool CheckIfBorrowedItems(int id) {
            foreach (int i in ListVisitorBorrowedItems()) {
                if (i == id)
                    return true;
            }
            return false;
        }
        ///Get value for checked in =1, checked out =0
        /// <summary>
        /// return string to get IN or OUT
        /// </summary>
        
        public string GetCheckedInorOut(string rfid) {
            int id = this.getVisitorIdbyRFID(rfid);
            string check = "";
            string sql = string.Format("SELECT checking_action FROM checking_history WHERE visitor_id={0}", id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    check = reader["checking_action"].ToString();
                }
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
            }
            finally { connection.Close(); }
            return check;
        }
        ///Check visitor has checked in or out before
        /// <summary>
        /// if visitor has checked in or out before, return true
        /// else, return false
        /// </summary>
       
        public bool VisitorHasCheckedBefore(string rfid) {
            int id = this.getVisitorIdbyRFID(rfid);
            List<int> list = new List<int>();
            string sql = "SELECT visitor_id FROM checking_history";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    list.Add(Convert.ToInt32(reader["visitor_id"]));
            } catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
            foreach (int i in list) {
                if (i == id)
                    return true;
            }
            return false;
        }
    }
}
