using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using GemBox.Spreadsheet.Charts;
using iTextSharp;

using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using Zen.Barcode;
using iTextSharp.text;//creating pdf
using iTextSharp.text.pdf;


namespace Event_Overview_Application {
    public partial class Form1 : Form
    {

        private DataHelper dh;

        public Form1()
        {
            InitializeComponent();
            dh = new DataHelper();
            lb_visitorFromSuggestion.Visible = false; //invisible the suggesting listbox for typing names

            //set full screen
            this.WindowState = FormWindowState.Maximized;

            //Diable textbox and button search at first
            //When user click on visitor or shop radiobuttons, this will be enable again
            txt_Names.Enabled = btn_search.Enabled =btn_clearDT.Enabled=btn_clear.Enabled= false;

            pn_visitorStatistic.Visible = pn_shopStatistics.Visible = pn_chartStatistics.Visible = false;

            BindNames();
            BindNamesShop();
           // pn_charts.Visible = true;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //******************************EXTRA METHOD***************************//

        //Load Visitors from DB to combobox
        public void LoadVisitorsToCbx()
        {
            List<Visitors> list = dh.getListOfVisitors();
            foreach (Visitors v in list)
            {
               // cb_visitorID.Items.Add(v.ToString());
            }
        }
        //Load Camping spot Id from DB to combobox
        //Shop overview combobox


        ///List suggestions when typing text in the textbox
        private void BindNames()
        {
            List<string> listNames = new List<string>();
            foreach (Visitors v in dh.getListOfVisitors())
            {
                listNames.Add(v.FirstName + "," + v.LastName + "-" + v.Id);
            }
            var source = new AutoCompleteStringCollection();
            txt_Names.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            source.AddRange(listNames.ToArray());
            txt_Names.AutoCompleteCustomSource = source;
            txt_Names.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_Names.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void BindNamesShop()
        {
            List<string> listNames = new List<string>();
            foreach (Store s in dh.getListOfStore())
            {
                listNames.Add(s.Name);
            }
            var source1 = new AutoCompleteStringCollection();
            txt_Names.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            source1.AddRange(listNames.ToArray());
            txt_Names.AutoCompleteCustomSource = source1;
            txt_Names.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_Names.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void txt_Names_TextChanged(object sender, EventArgs e)
        {
            

        }

      

   
        //get id from Name string 
        private int GetIdFromNameString(string str) {
            if (str == "")
            {
                return -1;
            }
            else
            {
                string[] temp = str.Split('-');
                return Convert.ToInt32(temp[temp.Length - 1]);
            }
            }
        //click on search button on detail section

        private void chart_shops_Click(object sender, EventArgs e)
        {

        }

        //Export to Excel file
        private void btn_exportToExcel_Click(object sender, EventArgs e)
        {
           
        }
    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
        }
        finally
        {
            GC.Collect();
        }
    }
        
        //clear button
        private void button1_Click(object sender, EventArgs e)
        {
            txt_Names.Text = "";
            lbx_Infor.Items.Clear();
            lb_showStatus.Text = "";
            lb_fname.Text = "";
            lb_lname.Text = "";
            // lb_bd.Text = v.Birthday;
            lb_phone.Text = "";
            lb_email.Text = "";
            pb_qrCode.Image = null;
            lb_campingID.Text = "";
            btn_clear.Enabled = false;
        }
        private void dt_info_CellDoubeClick(object sender, DataGridViewCellEventArgs e)
        {
            lbx_Infor.Items.Clear();

            if (rd_Visitor.Checked)
            {
                lb_fname.Visible = true;
                lb_lname.Visible = true;
                lb_db.Visible = true;
                lb_phone.Visible = true;
                lb_email.Visible = true;
                lb_campingID.Visible = true;
                pb_qrCode.Visible = true;
                lb_firstname.Visible = true;
                lb_lastname.Visible = true;
                lb_birthday.Visible = true;
                lb_phonee.Visible = true;
                lb_eemail.Visible = true;
                lb_spot.Visible = true;
                lb_ticket.Visible = true;
                picturebox.Visible = false;

                lb_status.Text = "Status";
                lb_firstname.Text = "Firstname:";
                lbx_Infor.Items.Add("*******************HISTORY OF VISITOR******************");
                //For visitors
                int id = 0;
                id = Convert.ToInt32(dt_infor.Rows[e.RowIndex].Cells[0].Value);
                Visitors v;
                v = dh.GetVisitorById(id);
                //display visitor information
                lb_showStatus.Text = "NO INFORMATION";
                lb_fname.Text = v.FirstName;
                lb_lname.Text = v.LastName;
                lb_db.Text = v.Birthday;
                lb_phone.Text = v.Phone;
                lb_email.Text = v.Email;

                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                pb_qrCode.Image = qrcode.Draw(v.Ticket_id, 50);

                lb_campingID.Text = v.Spot_id.ToString();
                List<string> checking_history = dh.GetVisitorCheckingHistory(id);
                List<string> transaction_history = dh.GetVisitorTransactionHistory(id);
                lbx_Infor.Items.Add("------------checking-----------");
                foreach (string s in checking_history)
                {
                    lbx_Infor.Items.Add(s);
                }
                lbx_Infor.Items.Add("------------transaction--------");
                foreach (string s in transaction_history)
                {
                    lbx_Infor.Items.Add(s);
                }
                btn_clear.Enabled = true;
            }
            //For camping spots
            if (rd_CampingSpot.Checked)
            {
                //invisible labels of visitor's information
                lb_fname.Visible = false;
                lb_lname.Visible = false;
                lb_db.Visible = false;
                lb_phone.Visible = false;
                lb_email.Visible = false;
                lb_campingID.Visible = false;
                pb_qrCode.Visible = false;

                lb_firstname.Visible = false;
                lb_lastname.Visible = false;
                lb_birthday.Visible = false;
                lb_phonee.Visible = false;
                lb_eemail.Visible = false;
                lb_spot.Visible = false;
                lb_ticket.Visible = false;
                picturebox.Visible = true;
                lb_status.Text = "Status";
                lb_firstname.Text = "Firstname:";
                lbx_Infor.Items.Add("*******************LIST OF VISITORS RESERVED THIS SPOT******************");
                int id = 0;
                id = Convert.ToInt32(dt_infor.Rows[e.RowIndex].Cells[0].Value);
                string zoneName = (string)(dt_infor.Rows[e.RowIndex].Cells[1].Value);
                if (zoneName == "Zone A")
                {
                    picturebox.Image = Properties.Resources.ZONE_A;
                }
                else if (zoneName == "Zone B")
                {
                    picturebox.Image = Properties.Resources.ZONE_B;
                }
                else if (zoneName == "Zone C")
                {
                    picturebox.Image = Properties.Resources.ZONE_C;
                }
                else if (zoneName == "Zone D")
                {
                    picturebox.Image = Properties.Resources.ZONE_D;
                }
                else if (zoneName == "Zone E")
                {
                    picturebox.Image = Properties.Resources.ZONE_E;
                }
                else if (zoneName == "Zone F")
                {
                    picturebox.Image = Properties.Resources.ZONE_F;
                }
                if (dh.IsAFreeSpot(id))
                {
                    lb_showStatus.Text = "Booked";
                    List<Visitors> list = dh.GetVisitorsReservedCampingSpot(id);
                    foreach (Visitors v in list)
                    {
                        lbx_Infor.Items.Add(v);
                    }
                }
                else
                {
                    lb_showStatus.Text = "Free";
                    lbx_Infor.Items.Add("NO ONE HAS BOOKED THIS SPOT YET");
                }
                lb_fname.Text = "NO INFORMATION";
                lb_lname.Text = "NO INFORMATION";
                lb_phone.Text = "NO INFORMATION";
                lb_email.Text = "NO INFORMATION";
                pb_qrCode.Image = null;
                lb_campingID.Text = "NO INFORMATION";

            }
            //For Shops
            if (rd_shop.Checked)
            {
                lb_firstname.Text = "Unit sold:";
                int id = 0;
                id = Convert.ToInt32(dt_infor.Rows[e.RowIndex].Cells[0].Value);
                lb_status.Text = "Total Profit";
                lb_showStatus.Text = dh.GetProfitByStoreId(id).ToString();
                lb_fname.Visible = false;
                lb_lname.Visible = false;
                lb_db.Visible = false;
                lb_phone.Visible = false;
                lb_email.Visible = false;
                lb_campingID.Visible = false;
                pb_qrCode.Visible = false;
                lb_firstname.Visible = false;
                lb_lastname.Visible = false;
                lb_birthday.Visible = false;
                lb_phonee.Visible = false;
                lb_eemail.Visible = false;
                lb_spot.Visible = false;
                lb_ticket.Visible = false;
                picturebox.Visible = true;
            }
        }
        private void DataGridview_event_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lbx_Infor.Items.Clear();
           
            if (rd_Visitor.Checked)
            {
                lb_fname.Visible = true;
                lb_lname.Visible = true;
                lb_db.Visible = true;
                lb_phone.Visible = true;
                lb_email.Visible = true;
                lb_campingID.Visible = true;
                pb_qrCode.Visible = true;
                lb_firstname.Visible = true;
                lb_lastname.Visible = true;
                lb_birthday.Visible = true;
                lb_phonee.Visible = true;
                lb_eemail.Visible = true;
                lb_spot.Visible = true;
                lb_ticket.Visible = true;
                picturebox.Visible = false;

                lb_status.Text = "Status";
                lb_firstname.Text = "Firstname:";
                lbx_Infor.Items.Add("*******************HISTORY OF VISITOR******************");
                //For visitors
                int id = 0;
                id = Convert.ToInt32(dt_infor.Rows[e.RowIndex].Cells[0].Value);
                Visitors v;
                v = dh.GetVisitorById(id);
                //display visitor information
                lb_showStatus.Text = "NO INFORMATION";
                lb_fname.Text = v.FirstName;
                lb_lname.Text = v.LastName;
                lb_db.Text = v.Birthday;
                lb_phone.Text = v.Phone;
                lb_email.Text = v.Email;

                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                pb_qrCode.Image = qrcode.Draw(v.Ticket_id, 50);

                lb_campingID.Text = v.Spot_id.ToString();
                List<string> checking_history = dh.GetVisitorCheckingHistory(id);
                List<string> transaction_history = dh.GetVisitorTransactionHistory(id);
                lbx_Infor.Items.Add("------------checking-----------");
                foreach (string s in checking_history) {
                    lbx_Infor.Items.Add(s);
                }
                lbx_Infor.Items.Add("------------transaction--------");
                foreach (string s in transaction_history)
                {
                    lbx_Infor.Items.Add(s);
                }
                btn_clear.Enabled = true;
            }
            //For camping spots
            if (rd_CampingSpot.Checked) {
                //invisible labels of visitor's information
                lb_fname.Visible = false;
                lb_lname.Visible = false;
                lb_db.Visible = false;
                lb_phone.Visible = false;
                lb_email.Visible = false;
                lb_campingID.Visible = false;
                pb_qrCode.Visible = false;

                lb_firstname.Visible = false;
                lb_lastname.Visible = false;
                lb_birthday.Visible = false;
                lb_phonee.Visible = false;
                lb_eemail.Visible = false;
                lb_spot.Visible = false;
                lb_ticket.Visible = false;
                picturebox.Visible = true;
                lb_status.Text = "Status";
                lb_firstname.Text = "Firstname:";
                lbx_Infor.Items.Add("*******************LIST OF VISITORS RESERVED THIS SPOT******************");
                int id = 0;
                id = Convert.ToInt32(dt_infor.Rows[e.RowIndex].Cells[0].Value);
                string zoneName = (string)(dt_infor.Rows[e.RowIndex].Cells[1].Value);
                if (zoneName == "Zone A")
                {
                    picturebox.Image = Properties.Resources.ZONE_A;
                }
                else if(zoneName == "Zone B")
                {
                    picturebox.Image = Properties.Resources.ZONE_B;
                }
                else if (zoneName == "Zone C")
                {
                    picturebox.Image = Properties.Resources.ZONE_C;
                }
                else if (zoneName == "Zone D")
                {
                    picturebox.Image = Properties.Resources.ZONE_D;
                }
                else if (zoneName == "Zone E")
                {
                    picturebox.Image = Properties.Resources.ZONE_E;
                }
                else if (zoneName == "Zone F")
                {
                    picturebox.Image = Properties.Resources.ZONE_F;
                }
                if (dh.IsAFreeSpot(id))
                {
                    lb_showStatus.Text = "Booked";
                    List<Visitors> list = dh.GetVisitorsReservedCampingSpot(id);
                    foreach (Visitors v in list)
                    {
                        lbx_Infor.Items.Add(v);
                    }
                }
                else
                {
                    lb_showStatus.Text = "Free";
                    lbx_Infor.Items.Add("NO ONE HAS BOOKED THIS SPOT YET");
                }
                lb_fname.Text ="NO INFORMATION";
                lb_lname.Text = "NO INFORMATION";
                lb_phone.Text = "NO INFORMATION";
                lb_email.Text = "NO INFORMATION";
                pb_qrCode.Image = null;
                lb_campingID.Text = "NO INFORMATION";
               
            }
            //For Shops
            if (rd_shop.Checked) {
                lb_firstname.Text = "Unit sold:";
                int id = 0;
                id = Convert.ToInt32(dt_infor.Rows[e.RowIndex].Cells[0].Value);
                lb_status.Text = "Total Profit";
                lb_showStatus.Text = dh.GetProfitByStoreId(id).ToString();
                lb_fname.Visible = false;
                lb_lname.Visible = false;
                lb_db.Visible = false;
                lb_phone.Visible = false;
                lb_email.Visible = false;
                lb_campingID.Visible = false;
                pb_qrCode.Visible = false;
                lb_firstname.Visible = false;
                lb_lastname.Visible = false;
                lb_birthday.Visible = false;
                lb_phonee.Visible = false;
                lb_eemail.Visible = false;
                lb_spot.Visible = false;
                lb_ticket.Visible = false;
                picturebox.Visible = true;
            }
        }

    

      
        private void btn_search_Click(object sender, EventArgs e)
        {
            if (txt_Names.Enabled)
            {
                if (txt_Names.Text == "") { MessageBox.Show("Nothing selected!"); }
                else
                {
                    if (rd_Visitor.Checked)
                    {
                        int id = Convert.ToInt32(txt_Names.Text.Split('-').Last());
                        dt_infor.Refresh();
                        dt_infor.DataSource = dh.GetDataTableAVisitor(id);
                    }
                    if (rd_shop.Checked)
                    {
                        dt_infor.Refresh();
                        dt_infor.DataSource = dh.GetDataTableAShop(txt_Names.Text);
                    }
                }
            }
            if (rd_CampingSpot.Checked)
            {
                if (rd_freeSpots.Checked)
                {
                    dt_infor.Refresh();
                    dt_infor.DataSource = dh.GetDatatableFreeSpot();
                }
                if (rd_bookedSpots.Checked)
                {
                    dt_infor.Refresh();
                    dt_infor.DataSource = dh.GetDatatableBookedSpot();
                }
            }


            else
            {
                //TODO
            }
        }

        private void btn_showDataTable_Click_1(object sender, EventArgs e)
        {
            if (rd_Visitor.Checked)
            {
                BindNames();
                txt_Names.Text = "";//Clear the text box search
                rd_bookedSpots.Enabled = rd_freeSpots.Enabled = false;
                txt_Names.Enabled = btn_search.Enabled = true;
                dt_infor.Refresh();
                dt_infor.DataSource = dh.GetDataTableVisitor();
            }
            if (rd_CampingSpot.Checked)
            {
                txt_Names.Text = "";//Clear the text box search
                txt_Names.Enabled = false;
                btn_search.Enabled = true;
                rd_bookedSpots.Enabled = rd_freeSpots.Enabled = true;
                dt_infor.Refresh();
                dt_infor.DataSource = dh.GetDataTableSpot();
            }
            if (rd_shop.Checked)
            {
                BindNamesShop();
                txt_Names.Text = "";//Clear the text box search
                rd_bookedSpots.Enabled = rd_freeSpots.Enabled = false;
                txt_Names.Enabled = btn_search.Enabled = true;
                dt_infor.Refresh();
                dt_infor.DataSource = dh.GetDataTableShops();
            }
            btn_clearDT.Enabled = true;
        }

        private void btn_clearDT_Click_1(object sender, EventArgs e)
        {
            dt_infor.Refresh();
            txt_Names.Text = "";
        }

        //Suggestion when typing
        private void lb_visitorFromSuggestion_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txt_Names.Text = lb_visitorFromSuggestion.Items[lb_visitorFromSuggestion.SelectedIndex].ToString();
            lb_visitorFromSuggestion.Visible = false;
        }

        private void panel_numbers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tab_visitorOverview_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void rd_freeSpots_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_bookedSpots_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_Names_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void rd_Visitor_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_CampingSpot_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_shop_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lb_spotID_Click(object sender, EventArgs e)
        {

        }

        private void lb_bd_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void picturebox_Click(object sender, EventArgs e)
        {

        }

        private void pb_qrCode_Click(object sender, EventArgs e)
        {

        }

        private void lb_email_Click(object sender, EventArgs e)
        {

        }

        private void lb_phone_Click(object sender, EventArgs e)
        {

        }

        private void lb_campingID_Click(object sender, EventArgs e)
        {

        }

        private void lb_fname_Click(object sender, EventArgs e)
        {

        }

        private void lb_lname_Click(object sender, EventArgs e)
        {

        }

        private void lb_db_Click(object sender, EventArgs e)
        {

        }

        private void lb_showStatus_Click(object sender, EventArgs e)
        {

        }

        private void lbx_Infor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lb_status_Click(object sender, EventArgs e)
        {

        }

        private void lb_lastname_Click(object sender, EventArgs e)
        {

        }

        private void lb_firstname_Click(object sender, EventArgs e)
        {

        }

        private void lb_rfid_Click(object sender, EventArgs e)
        {

        }

        private void lb_birthday_Click(object sender, EventArgs e)
        {

        }

        private void lb_eemail_Click(object sender, EventArgs e)
        {

        }

        private void lb_phonee_Click(object sender, EventArgs e)
        {

        }

        private void lb_ticket_Click(object sender, EventArgs e)
        {

        }

        private void lb_spot_Click(object sender, EventArgs e)
        {

        }

        private void tab_statistics_Click(object sender, EventArgs e)
        {

        }

        private void btn_png_Click(object sender, EventArgs e)
        {

        }

        private void lb_bestSelling_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void lb_textBestShop_Click(object sender, EventArgs e)
        {

        }

        private void lb_BestShop_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void lb_textTotalBalance_Click(object sender, EventArgs e)
        {

        }

        private void lb_totalBalancee_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void lb_freeSpot_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void lb_VisitorsChecedIn_Click(object sender, EventArgs e)
        {

        }

        private void lb_spentMoney_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void lb_textSpentMoney_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void lb_text_Click(object sender, EventArgs e)
        {

        }

        private void lb_bookedspot_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lb_totalVisitor_Click(object sender, EventArgs e)
        {

        }

        private void lb_textTotalAtEvent_Click(object sender, EventArgs e)
        {

        }

        private void lb_textTotalVisitors_Click(object sender, EventArgs e)
        {

        }

        private void lb_totalvisitorsatEvent_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_numbers_Click_1(object sender, EventArgs e)
        {

        }

        private void pn_charts_Paint(object sender, PaintEventArgs e)
        {
            pn_chartStatistics.Visible = true;

            pn_shopStatistics.Visible =  pn_visitorStatistic.Visible = false;
            string start = "";
            string end = "";
            int id;
            /*Get the data for the charts*/
            //The profits chart
          
        }

        private void panel10_Paint_1(object sender, PaintEventArgs e)
        {
           
        }

        private void panel9_Paint_1(object sender, PaintEventArgs e)
        {
           

        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {
           

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pn_visitorStatistic.Visible = true;

            pn_shopStatistics.Visible = pn_chartStatistics.Visible = false;
            pn_chartStatistics.Visible = lb_textBookedSpot.Visible = picture_bookedSpot.Visible = lb_bookedspot.Visible = picture_checkedIn.Visible = lb_textFreeSpt.Visible = picture_freeSpot.Visible = lb_freeSpot.Visible =  false;// piechart.Visible = false;
            lb_textTotalVisitors.Visible = picture_totalVisitor.Visible = lb_totalVisitor.Visible = lb_textTotalVisitors.Visible = picture_totalBalance.Visible = lb_textTotalAtEvent.Visible = picture_totalAtEvent.Visible = lb_totalvisitorsatEvent.Visible =lb_textTotalBalance.Visible=picture_totalBalance.Visible=lb_totalBalancee.Visible= true;
            lb_totalVisitor.Text = dh.getTotalVisitors().ToString();
            lb_totalBalancee.Text = dh.GetTotalBalance().ToString();
            lb_totalvisitorsatEvent.Text = dh.getTotalVisitorsAtEvent().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pn_shopStatistics.Visible = true;

            pn_chartStatistics.Visible = pn_visitorStatistic.Visible = lb_textBookedSpot.Visible = picture_bookedSpot.Visible = lb_bookedspot.Visible = picture_checkedIn.Visible = lb_textFreeSpt.Visible = picture_freeSpot.Visible = lb_freeSpot.Visible = lb_VisitorsChecedIn.Visible = lb_textCheckedIn.Visible = lb_textCheckedIn.Visible = false;//piechart.Visible= false;
            lb_textBestShop.Visible = picture_bestShop.Visible = lb_BestShop.Visible = lb_textBestSelling.Visible = picture_bestSelling.Visible = lb_bestSelling.Visible = lb_textSpentMoney.Visible = picture_totalSpentMoney.Visible = lb_spentMoney.Visible =lb_totalVisitor.Visible=lb_textTotalVisitors.Visible= true;
            //The best shop
            lb_BestShop.Text = dh.TheBestShop().Name;

            //The best selling item
            lb_bestSelling.Text = dh.TheBestSelling().Item_name;

            lb_spentMoney.Text = dh.GetTotalMoneySpent().ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pn_shopStatistics.Visible = true;
            pn_chartStatistics.Visible = pn_visitorStatistic.Visible = lb_textBookedSpot.Visible = picture_bookedSpot.Visible = lb_bookedspot.Visible = picture_checkedIn.Visible = lb_textFreeSpt.Visible = picture_freeSpot.Visible = lb_freeSpot.Visible =lb_VisitorsChecedIn.Visible=lb_textCheckedIn.Visible= true;
            lb_textBestShop.Visible = picture_bestShop.Visible = lb_BestShop.Visible = lb_textBestSelling.Visible = picture_bestSelling.Visible = lb_bestSelling.Visible = lb_textSpentMoney.Visible = picture_totalSpentMoney.Visible = lb_spentMoney.Visible = false;

            pn_chartStatistics.Visible = pn_visitorStatistic.Visible = false;

            lb_bookedspot.Text = dh.TotalBookedSpots().ToString();
            lb_freeSpot.Text = dh.TotalFreeSpots().ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            pn_visitorStatistic.Visible = true;

            pn_shopStatistics.Visible =  false;
            pn_chartStatistics.Visible = lb_textBookedSpot.Visible = picture_bookedSpot.Visible = lb_bookedspot.Visible = picture_checkedIn.Visible = lb_textFreeSpt.Visible = picture_freeSpot.Visible = lb_freeSpot.Visible =  false;
            lb_textTotalVisitors.Visible = picture_totalVisitor.Visible = lb_totalVisitor.Visible = lb_textTotalVisitors.Visible = picture_totalBalance.Visible = lb_textTotalAtEvent.Visible = picture_totalAtEvent.Visible = lb_totalvisitorsatEvent.Visible = lb_textTotalBalance.Visible=picture_totalBalance.Visible=lb_totalBalancee.Visible= false;
            Charts ch = new Charts();
            ch.Show();
            lb_totalVisitor.Text = dh.getTotalVisitors().ToString();

           

        }

        //NEWWWW
        private void btn_showDataTable_Click(object sender, EventArgs e)
        {
            if (rd_Visitor.Checked)
            {
                BindNames();
                txt_Names.Text = "";//Clear the text box search
                rd_bookedSpots.Enabled = rd_freeSpots.Enabled = false;
                txt_Names.Enabled = btn_search.Enabled = true;
                dt_infor.Refresh();
                dt_infor.DataSource = dh.GetDataTableVisitor();
            }
            if (rd_CampingSpot.Checked)
            {
                txt_Names.Text = "";//Clear the text box search
                txt_Names.Enabled = false;
                btn_search.Enabled = true;
                rd_bookedSpots.Enabled = rd_freeSpots.Enabled = true;
                dt_infor.Refresh();
                dt_infor.DataSource = dh.GetDataTableSpot();
            }
            if (rd_shop.Checked)
            {
                BindNamesShop();
                txt_Names.Text = "";//Clear the text box search
                rd_bookedSpots.Enabled = rd_freeSpots.Enabled = false;
                txt_Names.Enabled = btn_search.Enabled = true;
                dt_infor.Refresh();
                dt_infor.DataSource = dh.GetDataTableShops();
            }
            btn_clearDT.Enabled = true;
        }

        private void btn_search_Click_1(object sender, EventArgs e)
        {
            if (txt_Names.Enabled)
            {
                if (txt_Names.Text == "") { MessageBox.Show("Nothing selected!"); }
                else
                {
                    if (rd_Visitor.Checked)
                    {
                        int id = Convert.ToInt32(txt_Names.Text.Split('-').Last());
                        dt_infor.Refresh();
                        dt_infor.DataSource = dh.GetDataTableAVisitor(id);
                    }
                    if (rd_shop.Checked)
                    {
                        dt_infor.Refresh();
                        dt_infor.DataSource = dh.GetDataTableAShop(txt_Names.Text);
                    }
                }
            }
            if (rd_CampingSpot.Checked)
            {
                if (rd_freeSpots.Checked)
                {
                    dt_infor.Refresh();
                    dt_infor.DataSource = dh.GetDatatableFreeSpot();
                }
                if (rd_bookedSpots.Checked)
                {
                    dt_infor.Refresh();
                    dt_infor.DataSource = dh.GetDatatableBookedSpot();
                }
            }


            else
            {
                //TODO
            }
        }

        private void txt_Names_TextChanged_2(object sender, EventArgs e)
        {
            if (rd_Visitor.Checked)
            {
                lb_visitorFromSuggestion.Items.Clear();
                if (txt_Names.Text.Length == 0)
                {
                    lb_visitorFromSuggestion.Visible = false;
                    return;
                }
                foreach (string s in txt_Names.AutoCompleteCustomSource)
                {
                    if (s.Contains(txt_Names.Text))
                    {
                        lb_visitorFromSuggestion.Items.Add(s);
                        lb_visitorFromSuggestion.Visible = true;
                    }
                }
            }
            if (rd_shop.Checked)
            {
                lb_visitorFromSuggestion.Items.Clear();
                if (txt_Names.Text.Length == 0)
                {
                    lb_visitorFromSuggestion.Visible = false;
                    return;
                }
                foreach (string s in txt_Names.AutoCompleteCustomSource)
                {
                    if (s.Contains(txt_Names.Text))
                    {
                        lb_visitorFromSuggestion.Items.Add(s);
                        lb_visitorFromSuggestion.Visible = true;
                    }
                }

            }
        }

        private void lb_visitorFromSuggestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Names.Text = lb_visitorFromSuggestion.Items[lb_visitorFromSuggestion.SelectedIndex].ToString();
            lb_visitorFromSuggestion.Visible = false;
        }

        private void btn_clearDT_Click(object sender, EventArgs e)
        {
            dt_infor.Refresh();
            txt_Names.Text = "";
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_Names.Text = "";
            lbx_Infor.Items.Clear();
            lb_showStatus.Text = "";
            lb_fname.Text = "";
            lb_lname.Text = "";
            // lb_bd.Text = v.Birthday;
            lb_phone.Text = "";
            lb_email.Text = "";
            pb_qrCode.Image = null;
            lb_campingID.Text = "";
            btn_clear.Enabled = false;
            picturebox.Image = null;
            lb_db.Text = "";
        }

        private void btn_exportToExcel_Click_1(object sender, EventArgs e)
        {
          
        }

        private void btn_png_Click_1(object sender, EventArgs e)
        {

        }

        
    }

}
