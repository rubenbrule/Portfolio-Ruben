using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Balance_Adjustment_Application {
    public partial class Form1 : Form {
        private string fileName;
        private List<Visitor> listvisitor;
        private DataHelper dh;
        public Form1() {
            InitializeComponent();
            dh = new DataHelper();
            listvisitor = dh.getListOfVisitors();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                FileStream fs = null;
                StreamReader sr = null;
                try
                {
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs);
                    string s = sr.ReadLine();
                    while (s != null)
                    {
                        int visitorid;
                        decimal balance;
                        s = sr.ReadLine();
                        i++;
                        if (i >= 4 && s != null)
                        {
                            visitorid = Convert.ToInt32(s.Split(' ')[0]);
                            balance = Convert.ToDecimal(s.Split(' ')[1]);
                            foreach (Visitor visitor in listvisitor)
                            {
                                if (visitor.Id == visitorid)
                                {
                                    dh.UpdateBalance(visitor, (visitor.Balance + balance));
                                    listbox.Items.Add($"{visitor.FirstName} {visitor.LastName}, Deposit: € {balance}");
                                }
                            }

                        }
                    }
                    listbox.Items.Add("--------------------Loading is done--------------------");
                }
                catch (IOException)
                {
                    MessageBox.Show("Error reading file");
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                    }
                    if (fs != null)
                    {
                        fs.Close();
                    }

                }
            }

            catch (Exception)
            {
                MessageBox.Show("Please choose the file");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            if (result==DialogResult.OK)
            {
                fileName = ofd.FileName;
                txt_fileName.Text = fileName;
            }
        }
    }
}
