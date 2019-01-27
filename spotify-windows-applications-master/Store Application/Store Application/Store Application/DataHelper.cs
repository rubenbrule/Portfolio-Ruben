using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace Store_Application
{


    class DataHelper : IData
    {
        //fields
        private string connectionInfo;
        private MySqlConnection connection;
        Visitors v;

        private Form1 f;
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
        //****************************************methods************************//
        //Store Application

        //Get list of Stores
        public List<Store> getListOfStore()
        {
            List<Store> list = new List<Store>();
            string sql = "SELECT * FROM store";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["store_id"]);
                    string name = Convert.ToString(reader["store_name"]);
                    Store s = new Store(id, name);
                    list.Add(s);
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return list;
        }
        // Get Store info by StoreID      //FIX????????
        public int getStoreInfo(int id)
        {
            int result = 0;
            string sql = string.Format("SELECT count(item_id) FROM item WHERE store_id={0} GROUP BY store_id", id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                result = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }

            return result;
        }
        //get data table for all Item of a Store   
        public DataTable getDataOfAStore(int id)
        {
            DataTable dtTable = new DataTable();
            string sql = string.Format("SELECT item_name As NAME, item_category as CATEGORY, item_price as PRICE, item_quantity as QUANTITY, IFNULL(deposit_price,0) as DEPOSIT  FROM item where store_id={0}", id);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dtTable);
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return dtTable;
        }
        //Get list of Purchase item 
        //drinks or food or sweets or snacks = string type
        public List<Item> GetListOfSpecifiedItem(string type)
        {
            List<Item> list = new List<Item>();
            string sql = string.Format("SELECT * FROM item WHERE item_category='{0}'", type);
            Item p;
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["item_id"]);
                    string name = (string)(reader["item_name"]);
                    string category = (string)(reader["item_category"]);
                    decimal price = Convert.ToDecimal(reader["item_price"]);
                    int quantity = Convert.ToInt32(reader["item_quantity"]);
                    decimal deposit = 0; //Because purchase item doesnt have deposit
                    p = new Item(id, name, category, price, quantity, deposit);
                    list.Add(p);
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return list;
        }

        //3-12
        ///Get price from item name
        public decimal GetPriceForItem(string name)
        {
            decimal price = 0;
            string sql = string.Format("SELECT item_price FROM item WHERE item_name='{0}'", name);

            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                price = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return price;
        }
        public decimal GetProfitByStoreId(int id)
        {
            decimal profit = 0;
            string sql = string.Format("SELECT IFNULL(deposit_price,0) FROM item WHERE store_id ={0}", id);
            MySqlCommand command;
            command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                decimal deposit = Convert.ToDecimal(command.ExecuteScalar());
                if (deposit == 0)
                { //purchase items
                    string sql1 = string.Format("SELECT IFNULL( sum(p.purchase_quantity * i.item_price),0) from item i join purchase p on i.item_id =p.item_id where i.store_id ={0}", id);
                    command = new MySqlCommand(sql1, connection);
                    profit = Convert.ToDecimal(command.ExecuteScalar());

                }
                else//rental items
                {
                    string sql2 = string.Format("SELECT IFNULL(sum(i.item_price),0) FROM item i join rental r on i.item_id = r.item_id where i.store_id={0}", id);
                    command = new MySqlCommand(sql2, connection);
                    profit = Convert.ToDecimal(command.ExecuteScalar());
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return profit;
        }
        //Get unit sold/loaned
        public int GetUnitSoldOrLoaned(int id)
        {
            int unit = 0;
            string sql = string.Format("SELECT IFNULL(deposit_price,0) FROM item WHERE store_id ={0}", id);
            MySqlCommand command;
            command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                decimal deposit = Convert.ToDecimal(command.ExecuteScalar());
                if (deposit != 0)
                { //purchase items
                    string sql1 = string.Format("select count(r.item_id) from rental r join item i on r.item_id=i.item_id where i.store_id={0}", id);
                    command = new MySqlCommand(sql1, connection);
                    unit = Convert.ToInt32(command.ExecuteScalar());

                }
                else//rental items
                {
                    string sql2 = string.Format("select IFNULL(sum(p.purchase_quantity),0) from purchase p join item i on p.item_id=i.item_id where i.store_id={0};", id);
                    command = new MySqlCommand(sql2, connection);
                    unit = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return unit;

        }
        //update database by editing datagridview
        public void UpdateDatagrideview(int id)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(@"UPDATE item set *  where store_id =" + id, connection);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            DataTable dt;
            dt = this.getDataOfAStore(id);
            adapter.Update(dt);
            MessageBox.Show("Information updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Get item Id by Item name for insert to purchase and rental table when visitor buying or borrowing stuff
        public int GetIdByItemName(string name)
        {
            int id = 0;
            string sql = string.Format("SELECT item_id FROM item WHERE item_name='{0}'", name);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
            return id;
        }
  
        //Get Visitor Id by RFID
        public int GetVisitorIdByRFID(string rfid)
        {
            int v_id = 0;
            string sql = string.Format("SELECT visitor_id FROM visitor WHERE rfid='{0}'", rfid);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                v_id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
            return v_id;
        }
       
        public void UpdatePurchaseTable(int id,string rfid, string name, int quantity, string time) {
            int item_id = this.GetIdByItemName(name);
            int v_id = this.GetVisitorIdByRFID(rfid);
            string sql = string.Format("INSERT INTO purchase VALUES ({0},{1},{2},{3},'{4}')",id, v_id, item_id, quantity, time);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
        }
        //Get All RFID code that has been assigned to visitor (valid RFID)
        public List<string> GetValidRFID()
        {
            List<string> list = new List<string>();
            string sql = "SELECT rfid FROM visitor where rfid is not null";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add((string)reader[0]);
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return list;
        }
        //Check is it a valid RFID
        public bool CheckRFID(string rfid)
        {
            foreach (string str in this.GetValidRFID())
            {
                if (str == rfid)
                    return true;
            }
            return false;
        }
        //Get visitor name by rfid
        public string getVisitorNameByRFID(string rfid) {
            string name = "";
            string sql = string.Format("SELECT first_name FROM visitor WHERE rfid='{0}'", rfid);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                name = (string)command.ExecuteScalar();
            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { connection.Close(); }
            return name;
        }

        //Get visitor Id by rfid
        public int getVisitorIdbyRFID(string rfid)
        {
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
        //check if visitors already checked-in
        public bool VisitorHasCheckedBefore(string rfid)
        {
            int id = this.getVisitorIdbyRFID(rfid);
            List<int> list = new List<int>();
            string sql = "SELECT visitor_id FROM checking_history";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    list.Add(Convert.ToInt32(reader["visitor_id"]));
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
            foreach (int i in list)
            {
                if (i == id)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// Increment the purchase id
        /// <returns> purchase id after incremental</returns>
        public int IncrementPurchaseId() {
            int id = 0;
            string sql = "SELECT ifnull(max(id),0) from purchase;";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
            return ++id;
        }
        //Get balance of a visitor by RFID
        public decimal GetBalance(string rfid) {
            decimal balance = 0;
            string sql = string.Format("SELECT balance FROM visitor WHERE rfid='{0}'", rfid);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                balance = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
            return balance;
        }
        /// <summary>
        /// Update Rental table after visitor rent items
        /// </summary>
        public void UpdateRentalTable(string rfid,string name,string rent_time) {
            int v_id = this.GetVisitorIdByRFID(rfid);
            int i_id = this.GetIdByItemName(name);
            string sql = string.Format("INSERT INTO rental (visitor_id, item_id,rent_time, return_time) VALUES ({0},{1},'{2}',null);", v_id, i_id,rent_time);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
        }
       
        //Get deposit for rent item
        public decimal GetDeposit(string itemName) {
            decimal price = 0;
            string sql = string.Format("select deposit_price from item where item_name ='{0}'", itemName);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                price = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
            return price;
        }
        //Update balance of visitor after buying or renting
        public void UpdateBalanceOfVisitor(string rfid, decimal amount) {
            string sql = string.Format("Update visitor set balance =(balance-{0}) where rfid='{1}'", amount, rfid);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
        }
    }
}
