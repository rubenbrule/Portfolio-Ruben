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
using System.Media;
using System.Drawing.Printing;


namespace Entrance_Application {
    public partial class Form1 : Form {
        private RFID myRFIDReader;
        private DataHelper dh;
        private string rfid;
        PrintDocument pdoc = null;
        public Form1() {
            InitializeComponent();
            dh = new DataHelper();

            //Keep real time updated on screen
            timer2.Start();
            timer2.Tick += new EventHandler(timer2_Tick);

            lb_status.Text = lb_balance.Text = lb_balaceCheckIn.Text = lb_checkOUt.Text = "";
            btn_withdrawAndCheckOut.Visible = false;

            //set the timer tick event
            timer1.Tick += new System.EventHandler(timer1_Tick);
            timer1.Interval = 10000;
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
                   label45.Text=("an RFID-reader is found and opened, device-id is: " + myRFIDReader.DeviceID);
                }
                catch (PhidgetException) { label45.Text=("no RFID-reader opened???????????"); }
            }
            catch (PhidgetException) {
                MessageBox.Show("Error with RIFD reader!"); 
            }

            //font for label
            this.lb_status.Font =lb_balaceCheckIn.Font= new Font("Arial", 20, FontStyle.Bold); //check in

            this.lb_checkOUt.Font = this.lb_balance.Font= new Font("Arial", 30, FontStyle.Bold); //check out
            
        }
        private void Form1_Load(object sender, EventArgs e) {
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        //attach the rfid
        private void ShowWhoIsAttached(object sender, AttachEventArgs e) {
            try
            {
                label45.Text = string.Format("RFID device serial nr: {0} is attached", myRFIDReader.DeviceSerialNumber);

            }
            catch (Exception ex) {
               DialogResult d = MessageBox.Show("There's something wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (d == DialogResult.OK) {
                   // this.Refresh();
                }
            }
        }
        //detach the rfid
        private void ShowWhoIsDetached(object sender, DetachEventArgs e) {
            label45.BackColor = Color.Red;
            label45.Text = string.Format("RFID device serial nr: {0} is detached", myRFIDReader.DeviceSerialNumber);
        }
        //show the rfid tag
        private void ShowRfidTag(object sender, RFIDTagEventArgs e) {
            timer1.Start();
            rfid = e.Tag;
            string tabName = tab_entrance.SelectedTab.Name;
            if (tabName == "tab_checkIn")
            {
                //lb_rfid.Text = e.Tag;
                if (dh.CheckRFID(e.Tag))//Check is it a valid rfid
                {
                    //check is it already checked in?
                    if (dh.VisitorHasCheckedBefore(e.Tag) == false)// never check in and out
                    {
                      //  Visitors v = dh.getVisitorByRfid(e.Tag);
                        dh.UpdateWhenCheckIn(e.Tag);
                        // List<string> l = dh.GetMessageForRFID(e.Tag);
                        SoundPlayer audio1 = new SoundPlayer(Entrance_Application.Properties.Resources.Sucess);
                        audio1.Play();
                        lb_status.ForeColor = Color.Black;
                        lb_status.Text = "CHECKED IN";
                        lb_balaceCheckIn.Text = dh.GetBalanceOfaVisitor(e.Tag).ToString();
                        dh.InsertCheckingHistory(e.Tag,"IN");
                        dh.UpdateWhenCheckIn(e.Tag);
                    }
                    else {
                        if (dh.GetCheckedInorOut(e.Tag) == "IN") {
                            lb_status.ForeColor = Color.Red;
                            lb_status.Text = "Already IN";
                            SoundPlayer audio = new SoundPlayer(Entrance_Application.Properties.Resources.Alert); 
                            audio.Play();
                        }
                        else 
                        {
                            if (dh.GetCheckedInorOut(e.Tag) == "OUT")
                            {
                                Visitors v = dh.getVisitorByRfid(e.Tag);
                                dh.UpdateWhenCheckIn(e.Tag);
                                List<string> l = dh.GetMessageForRFID(e.Tag);
                                SoundPlayer audio = new SoundPlayer(Entrance_Application.Properties.Resources.Sucess);
                                audio.Play();
                                lb_status.ForeColor = Color.Black;
                                lb_status.Text = "CHECKED IN";
                                lb_balaceCheckIn.Text = dh.GetBalanceOfaVisitor(e.Tag).ToString();
                                dh.UpdateWhenCheckIn(e.Tag);
                            }
                            if (dh.GetCheckedInorOut(e.Tag) == "") {
                                dh.InsertCheckingHistory(e.Tag,"IN");
                                lb_status.Text = "CHECKED IN";
                                lb_balaceCheckIn.Text = dh.GetBalanceOfaVisitor(e.Tag).ToString();
                            }
                        }
                    }
                }
                else {
                    lb_status.ForeColor = Color.Red;
                    lb_status.Text = "INVALID!!!!";
                    lb_balaceCheckIn.Text = "";
                    SoundPlayer audio = new SoundPlayer(Entrance_Application.Properties.Resources.Alert);
                    audio.Play();
                }
            }
            if (tabName == "tab_checkOut")
            {
                rfid = e.Tag;
                int id = dh.getVisitorIdbyRFID(e.Tag);
             //   label2.Text = e.Tag;
                if (dh.CheckRFID(e.Tag)) //check valid rfid----> Yes
                {
                    if (dh.CheckIfBorrowedItems(id))//This visitor borrowed (some) item(s)
                    {
                        if (dh.CheckRentalStatus(e.Tag)) //Returned all item 
                        {
                            if (dh.GetCheckedInorOut(e.Tag) == "IN")//in the chekcing history table 
                            {
                                dh.UpdateWhenCheckOut(e.Tag);
                                lb_checkOUt.ForeColor = Color.Black;
                                lb_checkOUt.Text = "CHECKED OUT";
                                SoundPlayer audio1 = new SoundPlayer(Entrance_Application.Properties.Resources.Sucess);
                                audio1.Play();
                              btn_withdrawAndCheckOut.Visible = true;
                                lb_balance.Text = dh.GetBalanceOfaVisitor(e.Tag).ToString();
                            }
                            else
                            {
                                if (dh.GetCheckedInorOut(e.Tag) == "OUT")
                                {
                                    lb_checkOUt.ForeColor = Color.Red;
                                    lb_checkOUt.Text = "ALREADY OUT";
                                    SoundPlayer audio = new SoundPlayer(Entrance_Application.Properties.Resources.Alert);
                                    audio.Play();
                                    btn_withdrawAndCheckOut.Visible = false;
                                    lb_balance.Text = "";
                                }
                                else//not in checking history table yet
                                {
                                    dh.InsertCheckingHistory(e.Tag, "OUT");
                                    lb_checkOUt.ForeColor = Color.Black;
                                    lb_checkOUt.Text = "CHECKED OUT";
                                  btn_withdrawAndCheckOut.Visible = true;
                                    lb_balance.Text = dh.GetBalanceOfaVisitor(e.Tag).ToString();
                                }
                            }
                        }
                        else //havent returned items yet
                        {
                            lb_checkOUt.ForeColor = Color.Red;
                            lb_checkOUt.Text = "RETURN ITEM!!!!";
                           btn_withdrawAndCheckOut.Visible = false;
                            SoundPlayer audio = new SoundPlayer(Entrance_Application.Properties.Resources.Alert);
                            audio.Play();
                            lb_balance.Text = dh.GetBalanceOfaVisitor(e.Tag).ToString();
                        }
                    }
                    else// didnt borrow any items
                    {
                        if (dh.GetCheckedInorOut(e.Tag) == "IN")//in the chekcing history table 
                        {
                            dh.UpdateWhenCheckOut(e.Tag);
                            lb_checkOUt.ForeColor = Color.Black;
                            lb_checkOUt.Text = "CHECKED OUT";
                            btn_withdrawAndCheckOut.Visible = true;
                            SoundPlayer audio1 = new SoundPlayer(Entrance_Application.Properties.Resources.Sucess);
                            audio1.Play();
                            lb_balance.Text = dh.GetBalanceOfaVisitor(e.Tag).ToString();
                        }
                        else
                        {
                            if (dh.GetCheckedInorOut(e.Tag) == "OUT")
                            {
                                lb_checkOUt.ForeColor = Color.Red;
                                lb_checkOUt.Text = "ALREADY OUT";
                                SoundPlayer audio = new SoundPlayer(Entrance_Application.Properties.Resources.Alert);
                                audio.Play();
                                btn_withdrawAndCheckOut.Visible = false;
                                lb_balance.Text = "";
                            }
                            else//not in checking history table yet
                            {
                                dh.InsertCheckingHistory(e.Tag, "OUT");
                                lb_checkOUt.ForeColor = Color.Black;
                                lb_checkOUt.Text = "CHECKED OUT";
                                SoundPlayer audio1 = new SoundPlayer(Entrance_Application.Properties.Resources.Sucess);
                                audio1.Play();
                                lb_balance.Text = dh.GetBalanceOfaVisitor(e.Tag).ToString();
                                btn_withdrawAndCheckOut.Visible = true;
                            }
                        }
                    }

                }
                else
                { ////---> Invalid RFID
                    lb_checkOUt.ForeColor = Color.Red;
                    lb_checkOUt.Text = "INVALID";
                    SoundPlayer audio = new SoundPlayer(Entrance_Application.Properties.Resources.Alert);
                    audio.Play();
                    lb_balance.Text = "";
                    btn_withdrawAndCheckOut.Visible = false;
                } 

              
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        //Timer tick event Handler
        private void timer1_Tick(object sender, System.EventArgs e) {
           // timer1.Interval = 3000;
            lb_msg.Text= "";
            lb_status.Text =lb_balance.Text= lb_checkOUt.Text="";
            lb_balaceCheckIn.Text = "";
        }

        //Check out only button
        private void button5_Click(object sender, EventArgs e)
        {
            //dh.UpdateWhenCheckOut(lb_cho_rfid.Text);
            //lb_cho_rfid.Text = "";
        }

        private void btn_withdrawAndCheckOut_Click(object sender, EventArgs e)
        {

            DialogResult d = MessageBox.Show(string.Format("Withdraw: {0}, ", dh.GetBalanceOfaVisitor(rfid)), "Question", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int id = dh.getVisitorIdbyRFID(rfid);
            dh.DeleteRfid(id);
            Print();
            lb_checkOUt.Text = "";
            btn_withdrawAndCheckOut.Visible = false;
        }
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
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
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

            graphics.DrawString("WITHDAW MONEY BILL", new Font("Courier New", 16),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("" + DateTime.Now, new Font("Courier New", 10),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            String underLine = "------------------------------";
            graphics.DrawString(underLine,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            Offset = Offset + 20;
            
                graphics.DrawString("VISITOR: " + rfid+"-- "+dh.getVisitorByRfid(rfid).FirstName ,
                    new Font("Courier New", 13),
                    new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;

            graphics.DrawString("Balance: " + Convert.ToString(dh.GetBalanceOfaVisitor(rfid))+ "EUROS",
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString("******* " + "THANKS FOR YOUR VISIT" + "*******", new Font("Courier New", 13),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString("  ****** " + "SPORTIFY TEAM" + "*******", new Font("Courier New", 13),
                    new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;

            graphics.DrawString(underLine, new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lb_DateTimeIn.Text =  (DateTime.Now).ToString();
        }

        private void lb_DateTimeIn_Click(object sender, EventArgs e)
        {

        }
    }
}
