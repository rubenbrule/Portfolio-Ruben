using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //for DataTable
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Balance_Adjustment_Application
{


    class DataHelper 
    {
        //fields
        private string connectionInfo;
        private MySqlConnection connection;
        Visitor v;

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
        //*******************************************METHOD******************************//
        /// <summary>
        /// Override methods from IData Interface
        /// </summary>

        // Get Total Visitors 
        public int getTotalVisitors()
        {
            int total = 0;
            string sql = string.Format("select count(*) from visitor");
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                // Int32 count = (Int32)command.ExecuteScalar();
                total = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return total;
        }
        //Get Total Visitors At the event
        public int getTotalVisitorsAtEvent()
        {
            int total = 0;
            MySqlCommand command = new MySqlCommand(string.Format("select count(distinct visitor_id) from checking_history where checking_action='IN'"), connection);
            try
            {
                connection.Open();

                total = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally
            {
                connection.Close();
            }
            return total;
        }
        /// <summary>
        /// Get Visisor History
        /// </summary>
        /// <param name="id"></param>
        /// <returns> string History </returns>
      
        public List<string> GetVisitorCheckingHistory(int id) //Get from checking_history
        {
            //get DateTIme //todo
            List<string> list = new List<string>();
            string sql = string.Format("SELECT checking_action, checking_location, DATE_FORMAT(checking_time,'%d-%m-%y') FROM checking_history WHERE visitor_id={0}", id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string history = (string)reader["checking_action"] + " - " + (string)reader["checking_location"] +" - "+(string)reader["DATE_FORMAT(checking_time,'%d-%m-%y')"];
                    list.Add(history);
                }
               
            }
            catch (MySqlException e) { MessageBox.Show("tHAO"); }
            finally { connection.Close(); }
            return list;
        }
        public List<string> GetVisitorTransactionHistory(int id) //Get from transaction table
        {
            //get DateTIme //todo
            List<string> list = new List<string>();
            string sql = string.Format("SELECT IFNULL(transaction_action,'NONE'), IFNULL(transaction_amount,'NONE') FROM transaction WHERE visitor_id={0}", id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string history = (string)reader["IFNULL(transaction_action,'NONE')"] + " " + (string)reader["IFNULL(transaction_amount,'NONE')"];
                    list.Add(history);
                }

            }
            catch (MySqlException e) { MessageBox.Show("tHAO2"); }
            finally { connection.Close(); }
            return list;
        }


        //Get Visitor by id
        public Visitor GetVisitorById(int id) {
            string sql = string.Format("SELECT first_name,last_name,DATE_FORMAT(birthday,'%d-%m-%y'),IFNULL( email,''),IFNULL( phone,'None'),ticket_id,IFNULL(spot_id,0),IFNULL(rfid,'Have not assigned yet') from visitor WHERE visitor_id={0}", id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string fname = (string)(reader["first_name"]);
                    string ln = (string)(reader["last_name"]);
                    string bd = (string)reader["DATE_FORMAT(birthday,'%d-%m-%y')"];
                    
                    string phone = (string)reader["IFNULL( phone,'None')"];
                    string email = (string)(reader["IFNULL( email,'')"]);
                    string ticket = (string)(reader["ticket_id"]);
                    int spot =  Convert.ToInt32(reader["IFNULL(spot_id,0)"]);
                    string rrfid = (string)(reader["IFNULL(rfid,'Have not assigned yet')"]);
                    v = new Visitor(id, fname, ln,bd, phone, email, ticket, spot, rrfid);
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                    connection.Close();
            }
            return v;
        }

        //Get List of Visitors from database
        public List<Visitor> getListOfVisitors()
        {
            List<Visitor> listVisitors = new List<Visitor>();
            MySqlCommand command = new MySqlCommand("SELECT visitor_id,ticket_id,rfid,IFNULL(spot_id,0),first_name,last_name,birthday,IFNULL(email,'null') FROM visitor where rfid is not null", connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["visitor_id"]);
                    string fname = (string)(reader["first_name"]);
                    string ln = (string)(reader["last_name"]);
                    DateTime bd1 = (DateTime)reader["birthday"];
                    string bd =bd1.ToString("DD-MM-YYYY");
                    string phone = null;
                    string email = (string)(reader["IFNULL(email,'null')"]);
                    string ticket = (string)(reader["ticket_id"]);
                    // int spot = Convert.ToInt32(reader["spot_id"]);
                    int spot = 0;
                    string rfid = (string)(reader["rfid"]);
                    v = new Visitor(id, fname, ln,bd, phone, email, ticket, spot, rfid);
                    listVisitors.Add(v);
                }
            }

            catch (MySqlException e) { MessageBox.Show(e.Message); }

            finally
            {
                connection.Close();
            }
            return listVisitors;

        }
        //Update Balance 
        public void UpdateBalance(Visitor v, decimal balance) {
            string sql = string.Format("UPDATE visitor SET balance={0} WHERE visitor_id={1}",balance,v.Id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch { }
            finally { connection.Close(); }
        }
       

        //Get Store by Name
        public int getStoreIdByName(string name)
        {
            int id = 0;
            string sql = string.Format("SELECT store_id FROM store WHERE store_name='{0}'", name);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return id;
        }

     

        //Get total balance
        public double GetTotalBalance()
        {
            double total_balance = 0;
            string sql = "SELECT sum(balance) FROM visitor";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                total_balance = Convert.ToDouble(command.ExecuteScalar());

            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return total_balance;
        }
        public double GetTotalSpentMoneyFromPurchase(string start, string end) //total money from purchase table due to day
        {
            double total = 0;
            string sql = string.Format("SELECT IFNULL(sum(p.purchase_quantity*i.item_price),0) FROM purchase p join item i on i.item_id = p.item_id WHERE purchase_time BETWEEN '{0}' and '{1}'",start,end);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                total =Convert.ToInt32( command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return total;
        }
        public double GetTotalSpentMoneyFromPurchase() //total money from purchase table in general
        {
            double total = 0;
            string sql = string.Format("SELECT sum(p.purchase_quantity*i.item_price) FROM purchase p join item i on i.item_id = p.item_id");
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                total = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return total;
        }
        public double GetTotalSpentMoneyFromRental(string start, string end) //total money from rental table due to day
        {
            double total = 0;
            string sql = string.Format("SELECT IFNULL(sum(if(r.return_time is null,i.item_price,i.item_price+i.deposit_price)),0) from rental r join item i on r.item_id=i.item_id WHERE rent_time BETWEEN '{0}' and '{1}'",start,end);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                total = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return total;
        }
        public double GetTotalSpentMoneyFromRental() //total money from rental table in general
        { 
            double total = 0;
            string sql = string.Format("SELECT sum(if(r.return_time is null,i.item_price,i.item_price+i.deposit_price)) from rental r join item i on r.item_id=i.item_id ");
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                total = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return total;
        }
        public double GetTotalSpentMoneyFromCampingSpot() //total money from camping_spot table
        {
            double total = 0;
            string sql = "SELECT sum(if(spot_price is null,0,spot_price)) from camping_spot";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                total = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return total;
        }

        //get total spent money
        public double GetTotalMoneySpent(string start, string end)//due to day
        {
            double total_money_spent = 0;
            total_money_spent = this.GetTotalSpentMoneyFromRental(start,end) + this.GetTotalSpentMoneyFromPurchase(start,end);
            return total_money_spent;
        }
        public double GetTotalMoneySpent()//in general
        {
            double total_money_spent = 0;
            total_money_spent = this.GetTotalSpentMoneyFromRental() + this.GetTotalSpentMoneyFromPurchase()+this.GetTotalSpentMoneyFromCampingSpot();
            return total_money_spent;
        }

        //Get List of Camping Spot id
     
       
    }
    
}
