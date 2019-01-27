using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;

using iTextSharp.text;//creating pdf
using iTextSharp.text.pdf;
using System.IO;

namespace Event_Overview_Application
{
    public partial class Charts : Form
    {
        private DataHelper dh;
        public Charts()
        {
            InitializeComponent();
            dh = new DataHelper();
            ShowCharts();
        }

        private void Charts_Load(object sender, EventArgs e)
        {

        }
        //Show charts
        private void ShowCharts() {
           // int id;
            string start = "";
            string end = "";
            /*Get the data for the charts*/
            //The profits chart
         //  chart_shops = new Chart();
         //  chart_shops.Series.Add("Profit");
           // id = dh.getStoreIdByName("NIKE");
            chart_shops.Series["Revenue"].Points.AddXY("NIKE", dh.GetProfitByStoreId(1));
           // id = dh.getStoreIdByName("ELECTRONICS");
            chart_shops.Series["Revenue"].Points.AddXY("ELECTRONICS", dh.GetProfitByStoreId(2));
           // id = dh.getStoreIdByName("TONYS WOK");
            chart_shops.Series["Revenue"].Points.AddXY("TONY'S WOK", dh.GetProfitByStoreId(3));
            //the pie chart
         //  chart_spentMoney = new Chart();
         //  chart_spentMoney.Series.Add("Total Profit Per day of Shops");
            start = "2018-01-09 00:00:00";
            end = "2018-01-09 23:59:59";
            chart_spentMoney.Series["Daily revenue from shops"].Points.AddXY("DAY 1", dh.GetTotalMoneySpent(start, end));
            chart_spentMoney.Series["Daily revenue from Rental shop"].Points.AddXY("DAY 1", dh.GetTotalSpentMoneyFromRental(start, end));
            chart_spentMoney.Series["Daily revenue from purchase shop"].Points.AddXY("DAY 1", dh.GetTotalSpentMoneyFromPurchase(start, end));
            start = "2018-01-10 00:00:00";
            end = "2018-01-10 23:59:59";
            chart_spentMoney.Series["Daily revenue from shops"].Points.AddXY("DAY 2", dh.GetTotalMoneySpent(start, end));
            chart_spentMoney.Series["Daily revenue from Rental shop"].Points.AddXY("DAY 2", dh.GetTotalSpentMoneyFromRental(start, end));
            chart_spentMoney.Series["Daily revenue from purchase shop"].Points.AddXY("DAY 2", dh.GetTotalSpentMoneyFromPurchase(start, end));
            start = "2018-01-12 00:00:00";
            end = "2018-01-12 23:59:59";
            chart_spentMoney.Series["Daily revenue from shops"].Points.AddXY("DAY 3", dh.GetTotalMoneySpent(start, end));
            chart_spentMoney.Series["Daily revenue from Rental shop"].Points.AddXY("DAY 3", dh.GetTotalSpentMoneyFromRental(start, end));
            chart_spentMoney.Series["Daily revenue from purchase shop"].Points.AddXY("DAY 3", dh.GetTotalSpentMoneyFromPurchase(start, end));
            chart_spentMoney.Show();
            /*-----------------------*/
            
        }

        private void btn_exportToExcel_Click(object sender, EventArgs e)
        {
            string start = ""; string end = "";
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //add data 
            // xlWorkSheet.Cells[1, 1] = "";
            xlWorkSheet.Cells[1, 2] = "Profit";

            int id;
            id = dh.getStoreIdByName("NIKE");
            xlWorkSheet.Cells[2, 1] = "NIKE";
            xlWorkSheet.Cells[2, 2] = dh.GetProfitByStoreId(id).ToString();

            id = dh.getStoreIdByName("ELECTRONICS");
            xlWorkSheet.Cells[3, 1] = "ELECTRONICS";
            xlWorkSheet.Cells[3, 2] = dh.GetProfitByStoreId(id).ToString();

            id = dh.getStoreIdByName("TONYS WOK");
            xlWorkSheet.Cells[4, 1] = "TONYS WOK";
            xlWorkSheet.Cells[4, 2] = dh.GetProfitByStoreId(id).ToString();

            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            Excel.Chart chartPage = myChart.Chart;

            chartRange = xlWorkSheet.get_Range("A1", "d5");
            chartPage.SetSourceData(chartRange, misValue);
            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;

            //for profit parts
            start = "2018-01-09 00:00:00";
            end = "2018-01-09 23:59:59";
            xlWorkSheet.Cells[1, 10] = "Total revenue per day ";
            xlWorkSheet.Cells[2, 9] = "Day 1";
            xlWorkSheet.Cells[2, 10] = dh.GetTotalMoneySpent(start,end).ToString();

            start = "2018-01-10 00:00:00";
            end = "2018-01-10 23:59:59";
            xlWorkSheet.Cells[3, 9] = "Day 2";
            xlWorkSheet.Cells[3, 10] = dh.GetTotalMoneySpent(start, end).ToString();

            start = "2018-01-11 00:00:00";
            end = "2018-01-11 23:59:59";
            xlWorkSheet.Cells[4, 9] = "Day 3";
            xlWorkSheet.Cells[4, 10] = dh.GetTotalMoneySpent(start, end).ToString();

            /*------------------*/
            start = "2018-01-09 00:00:00";
            end = "2018-01-09 23:59:59";
            xlWorkSheet.Cells[1, 11] = "Total revenue from Rental shops per day ";
            xlWorkSheet.Cells[2, 11] = dh.GetTotalSpentMoneyFromRental(start, end).ToString();


            start = "2018-01-10 00:00:00";
            end = "2018-01-10 23:59:59";
            xlWorkSheet.Cells[3, 11] = dh.GetTotalSpentMoneyFromRental(start, end).ToString();

            start = "2018-01-11 00:00:00";
            end = "2018-01-11 23:59:59";
            xlWorkSheet.Cells[4, 11] = dh.GetTotalSpentMoneyFromRental(start, end).ToString();
            /*------*/
            start = "2018-01-09 00:00:00";
            end = "2018-01-09 23:59:59";
            xlWorkSheet.Cells[1, 12] = "Total Revenue from Purchase shops per day ";
            xlWorkSheet.Cells[2, 12] = dh.GetTotalSpentMoneyFromPurchase(start, end).ToString();


            start = "2018-01-10 00:00:00";
            end = "2018-01-10 23:59:59";
            xlWorkSheet.Cells[3, 12] = dh.GetTotalSpentMoneyFromPurchase(start, end).ToString();

            start = "2018-01-11 00:00:00";
            end = "2018-01-11 23:59:59";
            xlWorkSheet.Cells[4, 12] = dh.GetTotalSpentMoneyFromPurchase(start, end).ToString();


            Excel.Range chartRange1;

            Excel.ChartObjects xlCharts1 = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart1 = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            Excel.Chart chartPage1 = myChart1.Chart;

            chartRange1 = xlWorkSheet.get_Range("H1", "o8");
            chartPage1.SetSourceData(chartRange1, misValue);
            chartPage1.ChartType = Excel.XlChartType.xlColumnClustered;




            //export chart as picture file
            chartPage.Export(@"C:\Users\Thao Nguyen\Desktop\excel_chart_export.bmp", "BMP", misValue);
            chartPage1.Export(@"C:\Users\Thao Nguyen\Desktop\excel_chart_export.bmp", "BMP", misValue);

            xlWorkBook.SaveAs("StatisticsRevenue-informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            MessageBox.Show("Excel file created , you can find the file c:\\Statistics-Excel.xls");
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

        private void btn_pdf_Click(object sender, EventArgs e)
        {
            try
            {
                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                PdfWriter wr = PdfWriter.GetInstance(doc, new FileStream("Event Report.pdf", FileMode.Create));
                doc.Open();//Open Document to write
                           //Write content
                Paragraph para = new Paragraph("SPORTIFY EVENT REPORT");
                para.IndentationLeft = 200f;
                iTextSharp.text.Font contentFont = iTextSharp.text.FontFactory.GetFont("Webdings", 12, iTextSharp.text.Font.BOLD,iTextSharp.text.BaseColor.BLUE);
                para.Font = contentFont;
                doc.Add(para);
                Paragraph para1 = new Paragraph("Created time: "+DateTime.Now.ToString());
                para1.IndentationLeft = 200f;
                doc.Add(para1);

                /* iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(Properties.Resources.logo);
                doc.Add(png);*/
                Paragraph p1 = new Paragraph("January 17 - January 18, 2018");
                Paragraph p2 = new Paragraph("1. Visitor");
                Paragraph p3 = new Paragraph("Total Visitors to the event: " + dh.getTotalVisitors());
                Paragraph p4 = new Paragraph("Total balance of visitors at the begining of the event: " + dh.GetTotalBalance());
                Paragraph p5 = new Paragraph("Total spent money during the event: " + dh.GetTotalMoneySpent());
                Paragraph p6 = new Paragraph("2. Shop");
                Paragraph p7 = new Paragraph("Total money sold per shop: ");
                Paragraph p8 = new Paragraph("* NIKE: " + dh.GetProfitByStoreId(1));
                Paragraph p9 = new Paragraph("* ELECTRONIC: " + dh.GetProfitByStoreId(2));
                Paragraph p10 = new Paragraph("* TONYs WOK: " + dh.GetProfitByStoreId(3));
                p1.IndentationLeft = p2.IndentationLeft = p3.IndentationLeft = p4.IndentationLeft = p5.IndentationLeft = p6.IndentationLeft=p7.IndentationLeft = 40f;
                p8.IndentationLeft = p9.IndentationLeft = p10.IndentationLeft = 70f;
                doc.Add(p1);
                doc.Add(p2); doc.Add(p3); doc.Add(p4); doc.Add(p5); doc.Add(p6); doc.Add(p7); doc.Add(p8);
                doc.Add(p9); doc.Add(p10);
                //add chart to pdf file
                Paragraph p11 = new Paragraph("Chart: Total Revenue per shop");
                var chartImage = new MemoryStream();
                chart_shops.SaveImage(chartImage, ChartImageFormat.Png);
                iTextSharp.text.Image chart_image = iTextSharp.text.Image.GetInstance(chartImage.GetBuffer());
                
                doc.Add(chart_image);
                doc.Add(p11);
                var chartImage1 = new MemoryStream();
                chart_spentMoney.SaveImage(chartImage1, ChartImageFormat.Png);
                iTextSharp.text.Image chart_image1 = iTextSharp.text.Image.GetInstance(chartImage1.GetBuffer());
                doc.Add(chart_image1);
                Paragraph p12 = new Paragraph("Chart: Total Revenue per day from rental and purchase shops");
                doc.Add(p12);
                doc.Close();
                MessageBox.Show("PDF file created succesfully!!");
            }
            catch
            {
                DialogResult result = new DialogResult();
                result = MessageBox.Show("Reading PDF file errorr", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                    this.Close();
            }
        }
    }
}
