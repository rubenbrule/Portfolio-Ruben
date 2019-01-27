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

namespace Entrance_Application {
    public partial class Form1 : Form {
        private RFID myRFIDReader;
        private DataHelper dh;
        private string rfid;
        private List<int> list;
        private int id;
        public Form1() {
            InitializeComponent();
            dh = new DataHelper();

            //Keep real time updated on screen
            timer2.Start();
            timer2.Tick += new EventHandler(timer2_Tick);

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
        }
        private void Form1_Load(object sender, EventArgs e) {
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
            if (dh.CheckRFID(e.Tag)) //check is it a valid rfid
            {
                lb_name.Text = dh.VisitorNameByRfid(e.Tag)+ " ---";
                lb_rfid.Text = e.Tag;
               
                //Check if visitor borrowed items??
                id = dh.getVisitorIdbyRFID(e.Tag);
                if (dh.CheckIfBorrowedItems(id) == true)
                {
                    if (this.CheckRental(dh.GetListReturnTime(e.Tag)) == false) //check if visitor has returned item before checking in?
                    {
                        lb_rental.Items.Clear();
                        list = dh.GetListOfRentaItem(id);
                        int index = 0;
                        //foreach (string i in dh.GetListReturnTime(e.Tag)) {
                        //    //if (i == "Not return yet") {
                        //    //    lb_rental.Items.Add("" +dh.GetItemNameById( list[index]));
                        //    //}
                        //    //index++;
                        //    lb_rental.Items.Add(i);
                        //}
                        foreach (int i in dh.GetListOfRentaItem(id)) {
                            lb_rental.Items.Add(dh.GetItemNameById(i));
                        }
                    }
                    else
                    {
                        lb_rental.Items.Clear();
                        lb_rental.Items.Add("ITEM RETURNED");
                        // MessageBox.Show("ITEM RETURNED", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                
            }

            else
            {
                    lb_rental.Items.Clear();
                    lb_rental.Items.Add("NO ITEMS RENT FROM THIS VISITOR");
                    //  MessageBox.Show("There is no rental items from this visitor", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                }
                else
                {
                    lb_rfid.Text = "INVALID!!!";
                lb_name.Text = "";
                    SoundPlayer audio = new SoundPlayer(Entrance_Application.Properties.Resources.Alert);
                    audio.Play();
                }
        }
        //Check is there any item that has not been returned
        private bool CheckRental(List<string> list) {
            foreach (string s in list) {
                if (s == "Not return yet")
                    return false;
            }
            return true;
        }
      
        //Timer tick event Handler
        private void timer1_Tick(object sender, System.EventArgs e) {
           // timer1.Interval = 3000;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
           lb_DateTimeIn.Text = (DateTime.Now).ToString();
        }
        //Return all items
        private void button1_Click(object sender, EventArgs e)
        {
            int index = 0;
            while(lb_rental.Items.Count>=1) {
                if (lb_rental.Items.Count == 1) {
                    int item_id = list[0];
                    dh.UpdateRentalTable(id, item_id);
                    lb_rental.Items.RemoveAt(0);
                    list.RemoveAt(0);
                }
                else
                {
                    int item_id = list[0];
                    dh.UpdateRentalTable(id, item_id);
                    lb_rental.Items.RemoveAt(index);
                    list.RemoveAt(0);
                    index++;
                }
              
            }
            MessageBox.Show("Return all item succesfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lb_rfid.Text = lb_name.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int index = lb_rental.SelectedIndex;
                foreach (string s in lb_rental.Items) {
                    int id = dh.GetItemIdByName(lb_rental.SelectedItem.ToString());
                    dh.UpdateRentalTable(id, id);
                    lb_rental.Items.RemoveAt(index);

                }
              
            }
            catch
            {
                MessageBox.Show("NO ITEMS SELECTED", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
    }
    
}
