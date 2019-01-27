using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Phidget22;
using Phidget22.Events;
using System.Drawing.Printing;


namespace Store_Application {
    public partial class Form1 : Form {

        private DataHelper dh;
        private List<Store> listStore;
        private List<Item> listItemPurchase,listItemRent;
        private Bitmap bmp;
        private int count = 0;
        private string rfid;
        //print
        PrintDocument pdoc = null;

        private RFID myRFIDReader;
        public Form1() {

            InitializeComponent();
            listItemPurchase = new List<Item>();
            listItemRent = new List<Item>();
            dh = new DataHelper();
            listStore = new List<Store>();
            LoadStoreToCombobox();
            dataGridView_StoreInfo.Visible = false;

            timer1.Start();
            //Keep real time updated
            timer1.Tick += new System.EventHandler(timer1_Tick);

            //disable button make payment
            button3.Enabled =btn_payRental.Enabled= false;

            //full screen
            this.WindowState = FormWindowState.Maximized;

            //creat columns for datagridview Cart
            this.dt_cart.Columns.Add("Item", "Item");
            this.dt_cart.Columns.Add("Quantity", "Quantity");
            this.dt_cart.Columns.Add("Price", "Price");

            //Set datetime for label
            lb_DateTime2.Text = (DateTime.Now).ToString();

          lb_total.Text=lb_rentTotal.Text=  lb_storeID.Text = lb_totalItem.Text = lb_TotalUnit.Text = lb_totalProfits.Text =lb_test.Text=lb_rfidRental.Text=lb_rfid_purchase.Text=lb_nameRent.Text= "";

            //Read rfid
            try
            {
                myRFIDReader = new RFID();
                myRFIDReader.Attach += new AttachEventHandler(ShowWhoIsAttached);
                myRFIDReader.Detach += new DetachEventHandler(ShowWhoIsDetached);
                myRFIDReader.Tag += new RFIDTagEventHandler(ShowRfidTag);

                //open RFID
                try
                {
                    myRFIDReader.Open(); //this will cost some time, but this app continues . . .
                                         // this.timer1.Enabled = true;
                    lb_rfidReader.Text = ("an RFID-reader is found and opened, device-id is: " + myRFIDReader.DeviceID);
                }
                catch (PhidgetException) { lb_rfidReader.Text = ("no RFID-reader opened???????????"); }
            }
            catch (PhidgetException)
            {
                MessageBox.Show("Error with RIFD reader!");
            }
        }

        private void btn_showInfo_Click(object sender, EventArgs e)
        {
            if (cb_listStore.Text == "Choose a store"||cb_listStore.Text=="")
            {
                MessageBox.Show("No store selected","Attention!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                int index = cb_listStore.SelectedIndex;
                Store temp = listStore[index];
                lb_storeID.Text = temp.ID.ToString();
                lb_totalItem.Text = dh.getStoreInfo(temp.ID).ToString();
                dataGridView_StoreInfo.DataSource = dh.getDataOfAStore(temp.ID);
                dataGridView_StoreInfo.Visible = true;
                lb_totalProfits.Text = dh.GetProfitByStoreId(temp.ID).ToString() + " EUROS";
                lb_TotalUnit.Text = dh.GetUnitSoldOrLoaned(temp.ID).ToString() + " units";
            }
        }
        //Load all store to combobox
        public void LoadStoreToCombobox() {
            listStore = dh.getListOfStore();
            foreach (Store s in listStore) {
                cb_listStore.Items.Add(s.ToString());
            }
        }
    
        //Load all items for Sweets
        private void button9_Click(object sender, EventArgs e)
        {
            layout_item.Controls.Clear();
            int widthOfAButton = 231, heightOfAButton = 123;
            int spaceBetweenButtons = 10;
            int x = 3; // (x,y) is the left-top of the button
            int y = 3;
            Font myFont = new Font("Verdana", 8);

            Button myButton;
            foreach (Item ds in this.dh.GetListOfSpecifiedItem("SWEETS"))
            {
                myButton = new Button();
                myButton.Location = new System.Drawing.Point(x, y);
                myButton.Size = new System.Drawing.Size(widthOfAButton, heightOfAButton);
                myButton.Text = ds.Item_name;
                myButton.Font = myFont;
                myButton.Tag = ds.Item_price;
                myButton.Click += new System.EventHandler(UpdateCartPurchase);
                layout_item.Controls.Add(myButton);

                x = x + widthOfAButton + spaceBetweenButtons;
            }
        }
       
        //Load all items for DRINK
        private void button1_Click(object sender, EventArgs e)
        {    
            layout_item.Controls.Clear();
            int widthOfAButton = 231, heightOfAButton = 123;
            int spaceBetweenButtons = 10;
            int x = 3; // (x,y) is the left-top of the button
            int y = 3;
            Font myFont = new Font("Verdana", 8);

            Button myButton;
            foreach (Item ds in this.dh.GetListOfSpecifiedItem("DRINK"))
            {
                    myButton = new Button();
                    myButton.Location = new System.Drawing.Point(x, y);
                    myButton.Size = new System.Drawing.Size(widthOfAButton, heightOfAButton);
                    myButton.Text = ds.Item_name;
                    myButton.Font = myFont;
                    myButton.Tag = ds.Item_price;
                    myButton.Click += new System.EventHandler(UpdateCartPurchase);
                    layout_item.Controls.Add(myButton);

                    x = x + widthOfAButton + spaceBetweenButtons;
            }
        }
        //Load all items for food
        private void button8_Click(object sender, EventArgs e)
        {
            layout_item.Controls.Clear();
            int widthOfAButton = 231, heightOfAButton = 123;
            int spaceBetweenButtons = 10;
            int x = 3; // (x,y) is the left-top of the button
            int y = 3;
            Font myFont = new Font("Verdana", 8);

            Button myButton;
            foreach (Item ds in this.dh.GetListOfSpecifiedItem("FOOD"))
            {
                myButton = new Button();
                myButton.Location = new System.Drawing.Point(x, y);
                myButton.Size = new System.Drawing.Size(widthOfAButton, heightOfAButton);
                myButton.Text = ds.Item_name;
                myButton.Font = myFont;
                myButton.Tag = ds.Item_price;
                myButton.Click += new System.EventHandler(UpdateCartPurchase);
                layout_item.Controls.Add(myButton);

                x = x + widthOfAButton + spaceBetweenButtons;
            }
        }
        //Load all items for snacks
        private void button12_Click(object sender, EventArgs e)
        {
            layout_item.Controls.Clear();
            int widthOfAButton = 231, heightOfAButton = 123;
            int spaceBetweenButtons = 10;
            int x = 3; // (x,y) is the left-top of the button
            int y = 3;
            Font myFont = new Font("Verdana", 8);

            Button myButton;
            foreach (Item ds in this.dh.GetListOfSpecifiedItem("SNACKS"))
            {
                myButton = new Button();
                myButton.Location = new System.Drawing.Point(x, y);
                myButton.Size = new System.Drawing.Size(widthOfAButton, heightOfAButton);
                myButton.Text = ds.Item_name;
                myButton.Font = myFont;
                myButton.Tag = ds.Item_price;
                myButton.Click += new System.EventHandler(UpdateCartPurchase);
                layout_item.Controls.Add(myButton);
                x = x + widthOfAButton + spaceBetweenButtons;
            }
        }
        //********************EXTRA METHODS***************//

        //Update Cart
        private void UpdateCartPurchase(object sender, EventArgs e)
        {
            dt_cart.Rows.Clear();
            dt_cart.Refresh();
            count = 1;
            decimal totalPrice = 0;
            bool exist = false;
            bool check = false;
            Item i = new Item(0, ((Button)sender).Text, "", dh.GetPriceForItem(((Button)sender).Text), count, 0);
            if (listItemPurchase.Count == 0)
            {
                listItemPurchase.Add(i);
                dt_cart.Rows.Add(i.Item_name, i.Item_quantity.ToString(), i.Item_quantity * i.Item_price);
                lb_total.Text = (i.Item_quantity * i.Item_price).ToString();
            }
            else
            {
                for (int j = 0; j < listItemPurchase.Count&&check!=true; j++)
                {
                if(listItemPurchase[j].Item_name.ToString()==((Button)sender).Text.ToString())
                    {
                        int quantity = ++listItemPurchase[j].Item_quantity;
                        listItemPurchase.Remove(listItemPurchase[j]);
                        i = new Item(0, ((Button)sender).Text, "", dh.GetPriceForItem(((Button)sender).Text), quantity, 0);
                        listItemPurchase.Add(i);
                        exist = true;
                        check = true;
                    }
                }
                 if(exist==false)
                    {
                    listItemPurchase.Add(i);
                }
                foreach (Item temp1 in listItemPurchase)
                {
                    dt_cart.Rows.Add(temp1.Item_name, temp1.Item_quantity.ToString(), temp1.Item_quantity * temp1.Item_price);
                    totalPrice += (temp1.Item_quantity * temp1.Item_price);
                    lb_total.Text = totalPrice.ToString();
                }
            }
        }
        private void UpdateCartRental(Object sender,EventArgs e) {
            dt_rental.Rows.Clear();
            dt_rental.Refresh();
            decimal totalPrice = 0;
          //  decimal deposit = 0;
            bool exist = false;
            bool check = false;
            DateTime dt = DateTime.Now;
            string s = dt.ToString("yy-MM-dd HH:mm");
            RentalItem i = new RentalItem(0, ((Button)sender).Text, "", dh.GetPriceForItem(((Button)sender).Text), 1, dh.GetDeposit(((Button)sender).Text), s, null);
            if (listItemRent.Count == 0)
            {
                listItemRent.Add(i);
                dt_rental.Rows.Add(i.Item_name,1, dh.GetDeposit(((Button)sender).Text).ToString(), dh.GetPriceForItem(((Button)sender).Text).ToString(),s,null);
                lb_rentTotal.Text = (dh.GetPriceForItem(((Button)sender).Text) + dh.GetDeposit(((Button)sender).Text)).ToString();
            }
            else
            {
                //  foreach (Item temp in listItem)
                for (int j = 0; j < listItemRent.Count && check != true; j++)
                {
                    if (listItemRent[j].Item_name.ToString() == ((Button)sender).Text.ToString())
                    {
                        MessageBox.Show("Can not rent this item 2 times at one order");
                        exist = true;
                        check = true;
                    }
                }
                if (exist == false)
                {
                    listItemRent.Add(i);
                }
                foreach (RentalItem temp1 in listItemRent)
                {
                    dt_rental.Rows.Add(temp1.Item_name, 1, dh.GetDeposit(temp1.Item_name), dh.GetPriceForItem(temp1.Item_name), s, null);
                    totalPrice += (temp1.Item_price + temp1.Deposit);
                    lb_rentTotal.Text = totalPrice.ToString();
                }
            }
        }
        /// <summary>
        /// Click on each button for the list of items in menu
        /// </summary>
        /// RENTAL ITEMS
        private void btn_camera_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            int widthOfAButton = 231, heightOfAButton = 123;
            int spaceBetweenButtons = 10;
            int x = 3; // (x,y) is the left-top of the button
            int y = 3;
            Font myFont = new Font("Verdana", 8);

            Button myButton;
            foreach (Item ds in this.dh.GetListOfSpecifiedItem("CAMERA"))
            {
                myButton = new Button();
                myButton.Location = new System.Drawing.Point(x, y);
                myButton.Size = new System.Drawing.Size(widthOfAButton, heightOfAButton);
                myButton.Text = ds.Item_name;
                myButton.Font = myFont;
                myButton.Tag = ds.Item_price;
                myButton.Click += new System.EventHandler(UpdateCartRental);
                tableLayoutPanel1.Controls.Add(myButton);

                x = x + widthOfAButton + spaceBetweenButtons;
            }
        }

        private void btn_otherItems_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            int widthOfAButton = 231, heightOfAButton = 123;
            int spaceBetweenButtons = 10;
            int x = 3; // (x,y) is the left-top of the button
            int y = 3;
            Font myFont = new Font("Verdana", 8);

            Button myButton;
            foreach (Item ds in this.dh.GetListOfSpecifiedItem("OTHER ITEMS"))
            {
                myButton = new Button();
                myButton.Location = new System.Drawing.Point(x, y);
                myButton.Size = new System.Drawing.Size(widthOfAButton, heightOfAButton);
                myButton.Text = ds.Item_name;
                myButton.Font = myFont;
                myButton.Tag = ds.Item_price;
                myButton.Click += new System.EventHandler(UpdateCartRental);
                tableLayoutPanel1.Controls.Add(myButton);

                x = x + widthOfAButton + spaceBetweenButtons;
            }
        }
        private void btn_charger_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            int widthOfAButton = 231, heightOfAButton = 123;
            int spaceBetweenButtons = 10;
            int x = 3; // (x,y) is the left-top of the button
            int y = 3;
            Font myFont = new Font("Verdana", 8);

            Button myButton;
            foreach (Item ds in this.dh.GetListOfSpecifiedItem("CHARGER"))
            {
                myButton = new Button();
                myButton.Location = new System.Drawing.Point(x, y);
                myButton.Size = new System.Drawing.Size(widthOfAButton, heightOfAButton);
                myButton.Text = ds.Item_name;
                myButton.Font = myFont;
                myButton.Tag = ds.Item_price;
                myButton.Click += new System.EventHandler(UpdateCartRental);
                tableLayoutPanel1.Controls.Add(myButton);

                x = x + widthOfAButton + spaceBetweenButtons;
            }
        }
        private void btn_powerBank_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            int widthOfAButton = 231, heightOfAButton = 123;
            int spaceBetweenButtons = 10;
            int x = 3; // (x,y) is the left-top of the button
            int y = 3;
            Font myFont = new Font("Verdana", 8);

            Button myButton;
            foreach (Item ds in this.dh.GetListOfSpecifiedItem("POWER BANK"))
            {
                myButton = new Button();
                myButton.Location = new System.Drawing.Point(x, y);
                myButton.Size = new System.Drawing.Size(widthOfAButton, heightOfAButton);
                myButton.Text = ds.Item_name;
                myButton.Font = myFont;
                myButton.Tag = ds.Item_price;
                myButton.Click += new System.EventHandler(UpdateCartRental);
                tableLayoutPanel1.Controls.Add(myButton);

                x = x + widthOfAButton + spaceBetweenButtons;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            int widthOfAButton = 231, heightOfAButton = 123;
            int spaceBetweenButtons = 10;
            int x = 3; // (x,y) is the left-top of the button
            int y = 3;
            Font myFont = new Font("Verdana", 8);

            Button myButton;
            foreach (Item ds in this.dh.GetListOfSpecifiedItem("LENS"))
            {
                myButton = new Button();
                myButton.Location = new System.Drawing.Point(x, y);
                myButton.Size = new System.Drawing.Size(widthOfAButton, heightOfAButton);
                myButton.Text = ds.Item_name;
                myButton.Font = myFont;
                myButton.Tag = ds.Item_price;
                myButton.Click += new System.EventHandler(UpdateCartRental);
                tableLayoutPanel1.Controls.Add(myButton);

                x = x + widthOfAButton + spaceBetweenButtons;
            }
        }

        private void dt_cart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult d = MessageBox.Show("Do you want to reduce one unit for this item?", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (d == DialogResult.OK)
            {
                string item_name = "";
                item_name = (string)(dt_cart.Rows[e.RowIndex].Cells[0].Value);
                foreach (Item i in listItemPurchase)
                {
                    if (i.Item_name == item_name)
                    {
                        --i.Item_quantity;
                    }
                }
                dt_cart.Rows.Clear();
                dt_cart.Refresh();
                foreach (Item i in listItemPurchase)
                {
                    decimal totalPrice = 0;
                    dt_cart.Rows.Add(i.Item_name, i.Item_quantity, i.Item_price);
                    totalPrice += (i.Item_price * i.Item_quantity);
                    lb_total.Text = totalPrice.ToString();
                }

            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }
        // make a payment for visitor
        private void button3_Click(object sender, EventArgs e)
        {

            rfid = lb_rfid_purchase.Text;
            decimal total = 0;
            decimal balance = dh.GetBalance(lb_rfid_purchase.Text); //But it shows firstname of visitor???
            if (balance > this.totalPrice())//enough balance to purchase
            {
                
                int visitor_id = dh.GetVisitorIdByRFID(lb_rfid_purchase.Text);
                DateTime dt = DateTime.Now;
                string s = dt.ToString("yyyy-MM-dd HH:mm:ss");
                foreach (Item i in listItemPurchase)
                {
                    int id = dh.IncrementPurchaseId();
                    dh.UpdatePurchaseTable(id,lb_rfid_purchase.Text,i.Item_name,i.Item_quantity,s);
                    total += (i.Item_quantity * i.Item_price);
                }
                dh.UpdateBalanceOfVisitor(rfid,total);
                MessageBox.Show("Print the bill....", "Success purchasing", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                lb_rfid_purchase.ForeColor = Color.Black;
                lb_rfid_purchase.Text = "";
                button3.Enabled = false;
                lb_total.Text = "";
                lb_test.Text = "";

                //clear the datagridview 
                dt_cart.Rows.Clear();
                dt_cart.Refresh();
                Print();
                listItemPurchase.Clear();
               
            }
            else
                MessageBox.Show("NOT ENOUGH MONEY IN BALANCE", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Print the bill
        public void Print()
        {
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);

            PaperSize psize = new PaperSize("Custom", 100, 100);
            //ps.DefaultPageSettings.PaperSize = psize;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            pdoc.DefaultPageSettings.PaperSize.Height = 50;

            pdoc.DefaultPageSettings.PaperSize.Width = 50;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintPreviewDialog pp = new PrintPreviewDialog();
                pp.Document = pdoc;
                pp.Width = 50;
                pp.Height = 50;
                pp.PerformAutoScale();
                result = pp.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pdoc.Print();
                }
            }

        }
        //Print rental bills
        public void PrintRental()
        {
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);

            PaperSize psize = new PaperSize("Custom", 100, 100);
            //ps.DefaultPageSettings.PaperSize = psize;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            pdoc.DefaultPageSettings.PaperSize.Height = 50;

            pdoc.DefaultPageSettings.PaperSize.Width = 50;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPageRent);

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintPreviewDialog pp = new PrintPreviewDialog();
                pp.Document = pdoc;
                pp.Width = 50;
                pp.Height = 50;
                pp.PerformAutoScale();
                result = pp.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pdoc.Print();
                }
            }
            lb_rfidRental.Text = lb_nameRent.Text = lb_rentTotal.Text = "";
        }
        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            decimal totalPrice = 0;

          //  string visitor_name = dh.getVisitorNameByRFID(rfid);

            graphics.DrawString("SPORTIFY EVENT", new Font("Courier New", 16),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Purchase", new Font("Courier New", 16),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("VISITOR: "+ dh.getVisitorNameByRFID(rfid)+ "--"+ rfid, new Font("Courier New", 16),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("" + DateTime.Now, new Font("Courier New", 10),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            String underLine = "--------------------------";
            graphics.DrawString(underLine,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            Offset = Offset + 20;
            foreach (Item i in listItemPurchase)
            {
                graphics.DrawString(i.Item_quantity+" x "+ i.Item_price+ "      " + i.Item_name,
                    new Font("Courier New", 13),
                    new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                totalPrice += (i.Item_quantity * i.Item_price);
            }
            String underLine2 = "--------------------------";
            graphics.DrawString(underLine2,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("TOTAL     : " + Convert.ToString(totalPrice) + "",
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString("*****" + "THANKS FOR YOUR VISIT" + "*****", new Font("Courier New", 13),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;

            graphics.DrawString(underLine, new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
        }
        void pdoc_PrintPageRent(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            decimal totalPrice = 0;

            //  string visitor_name = dh.getVisitorNameByRFID(rfid);

            graphics.DrawString("SPORTIFY EVENT", new Font("Courier New", 16),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Rental", new Font("Courier New", 16),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("VISITOR: " + dh.getVisitorNameByRFID(rfid) + "--" + rfid, new Font("Courier New", 16),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("" + DateTime.Now, new Font("Courier New", 10),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            String underLine = "--------------------------";
            graphics.DrawString(underLine,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            Offset = Offset + 20;
            foreach (RentalItem i in listItemRent)
            {
                graphics.DrawString(i.Item_quantity + " x " +( i.Item_price+i.Deposit) + "      "+ i.Item_name,
                    new Font("Courier New", 13),
                    new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                totalPrice += i.Item_quantity *(i.Deposit+ i.Item_price);
            }
            String underLine2 = "--------------------------";
            graphics.DrawString(underLine2,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("TOTAL     : " + Convert.ToString(totalPrice) + "",
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString("*****" + "THANKS FOR YOUR VISIT" + "*****", new Font("Courier New", 13),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;

            graphics.DrawString(underLine, new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
        }
        //Adjust Item when double-click on the row in datagridview Cart ////Havent done 14/12
        private void AdjustItemInCart() {
            AdjustQuantity ad = new AdjustQuantity();
            ad.Show();

        }
        //Get total price from the bill
        private decimal totalPrice() {
            decimal totalPrice = 0;
            foreach (Item i in listItemPurchase) {
                totalPrice += (i.Item_price*i.Item_quantity);
            }
            return totalPrice;
        }
        private decimal totalRentPrice() {
            decimal totalPrice = 0;
            foreach (RentalItem i in listItemRent) {
                totalPrice += (i.Item_price + i.Deposit)*i.Item_quantity;
            }
            return totalPrice;
        }
        //attach the rfid
        private void ShowWhoIsAttached(object sender, AttachEventArgs e)
        {
            try
            {
                lb_rfidReader.Text = string.Format("RFID device serial nr: {0} is attached", myRFIDReader.DeviceSerialNumber);

            }
            catch (Exception ex)
            {
                DialogResult d = MessageBox.Show("There's something wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (d == DialogResult.OK)
                {
                    // this.Refresh();
                }
            }
        }
        //detach the rfid
        private void ShowWhoIsDetached(object sender, DetachEventArgs e)
        {
            lb_rfidReader.BackColor = Color.Red;
            lb_rfidReader.Text = string.Format("RFID device serial nr: {0} is detached", myRFIDReader.DeviceSerialNumber);
        }
        //Show rfid to buy items
        private void ShowRfidTag(object sender, RFIDTagEventArgs e)
        {

            string tabName = tab_Store.SelectedTab.Name;
            if (tabName == "tab_purchase")
            {
               
                if (dh.CheckRFID(e.Tag))//Check is it a valid rfid
                {
                    //check is it already checked in?
                    if (dh.VisitorHasCheckedBefore(e.Tag))
                    {// checked in
                        lb_rfid_purchase.ForeColor = Color.Black;
                        lb_rfid_purchase.Text = e.Tag;
                        lb_test.Text = dh.getVisitorNameByRFID(e.Tag);
                        button3.Enabled = true;
                    }
                    else//havn't checked in
                    { 
                        lb_rfid_purchase.ForeColor = Color.Red;
                        lb_rfid_purchase.Text = "NOT CHECK IN YET!!";
                        lb_test.Text =dh.getVisitorNameByRFID(e.Tag);
                        button3.Enabled = false;
                    }
                }
                else {//Invalid rfid
                    lb_rfid_purchase.ForeColor = Color.Red;
                    lb_rfid_purchase.Text = "INVALID VISITOR!!";
                    lb_test.Text = "";
                    button3.Enabled = false;
                }
            }
              
            if (tabName == "tab_rental")
            {
               
                if (dh.CheckRFID(e.Tag))//Check is it a valid rfid
                {
                    //check is it already checked in?
                    if (dh.VisitorHasCheckedBefore(e.Tag))
                    {// checked in
                        lb_rfidRental.ForeColor = Color.Black;
                        lb_rfidRental.Text = e.Tag;
                        lb_nameRent.Text = dh.getVisitorNameByRFID(e.Tag);
                        btn_payRental.Enabled = true;
                    }
                    else
                    { //havn't checked in
                        lb_rfidRental.ForeColor = Color.Red;
                        lb_rfidRental.Text = "NOT CHECK IN YET!!";
                        lb_nameRent.Text = dh.getVisitorNameByRFID(e.Tag);
                        btn_payRental.Enabled = false;
                    }
                }
                else
                {//Invalid rfid
                    lb_rfidRental.ForeColor = Color.Red;
                    lb_rfidRental.Text = "INVALID VISITOR!!";
                    lb_nameRent.Text = "";
                    btn_payRental.Enabled = false;
                }
            }
        }
        
        //clear button at purchase section
        private void button10_Click(object sender, EventArgs e)
        {
            lb_rfid_purchase.ForeColor = Color.Black;
            lb_rfid_purchase.Text = "";
            listItemPurchase = new List<Item>();
            button3.Enabled = false;
            lb_total.Text = "";

            //clear the datagridview 
            dt_cart.Rows.Clear();
            dt_cart.Refresh();
        }

        private void btn_payRental_Click(object sender, EventArgs e)
        {
            decimal total = 0;
            decimal balance = dh.GetBalance(lb_rfidRental.Text); //But it shows firstname of visitor???
            if (balance > this.totalRentPrice())//enough balance to purchase
            {

                int visitor_id = dh.GetVisitorIdByRFID(lb_rfidRental.Text);
                DateTime dt = DateTime.Now;
                string s = dt.ToString("yyyy-MM-dd HH:mm:ss");
                foreach (Item i in listItemRent)
                {
                    int id = dh.IncrementPurchaseId();
                    dh.UpdateRentalTable(lb_rfidRental.Text,i.Item_name,s);
                    total += (i.Item_price + i.Deposit)*i.Item_quantity;
                    
                }
                dh.UpdateBalanceOfVisitor(lb_rfidRental.Text, total);
                MessageBox.Show("Print the bill....", "Success purchasing", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Print the bill
                PrintRental();
                int height = dt_cart.Height;
              //  dt_cart.Height = dt_cart.RowCount * dt_cart.RowTemplate.Height * 2;
              //  bmp = new Bitmap(dt_cart.Width, dt_cart.Height);
              //  dt_cart.DrawToBitmap(bmp, new Rectangle(0, 0, dt_cart.Width, dt_cart.Height));
              //  dt_cart.Height = height;
              //  printPreviewDialog1.ShowDialog();
                lb_rfid_purchase.ForeColor = Color.Black;
                lb_rfid_purchase.Text = "";
                button3.Enabled = false;
                lb_total.Text = "";

                //clear the datagridview 
                dt_rental.Rows.Clear();
                dt_rental.Refresh();
                listItemRent.Clear();
            }
            else
                MessageBox.Show("NOT ENOUGH MONEY IN BALANCE", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Quantity of item will be deducted 1 unit when user double clicks on it in the Cart
        /// </summary>
        private void dt_rental_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult d = MessageBox.Show("Remove one unit from this item?", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (d == DialogResult.OK)
            {
                string item_name = "";
                item_name = (string)(dt_rental.Rows[e.RowIndex].Cells[0].Value);
                foreach (RentalItem i in listItemRent) {
                    if (i.Item_name == item_name) {
                        --i.Item_quantity;
                    }
                }
                dt_rental.Rows.Clear();
                dt_rental.Refresh();
                decimal totalPrice = 0;
                foreach (RentalItem i in listItemRent) {
                   
                    dt_rental.Rows.Add(i.Item_name, i.Item_quantity, i.Deposit, i.Item_price, i.RentTime, null);
                    totalPrice += (i.Item_price + i.Deposit)*i.Item_quantity;
                    lb_rentTotal.Text = totalPrice.ToString();
                }

            }
        }

        private void dt_cart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)//Keep real time updated
        {
            lb_DateTime2.Text  = (DateTime.Now).ToString();
        }
    }
}
