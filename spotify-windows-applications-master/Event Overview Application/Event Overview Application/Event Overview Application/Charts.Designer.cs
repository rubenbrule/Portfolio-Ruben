namespace Event_Overview_Application
{
    partial class Charts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Charts));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.chart_shops = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_spentMoney = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_exportToExcel = new System.Windows.Forms.Button();
            this.btn_pdf = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_shops)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_spentMoney)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1359, 123);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(216, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(607, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Event Overview Application";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 667);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1359, 81);
            this.panel2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(345, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(400, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "All Rights Reserved © G5 Software Solutions";
            // 
            // chart_shops
            // 
            chartArea5.Name = "ChartArea1";
            this.chart_shops.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart_shops.Legends.Add(legend5);
            this.chart_shops.Location = new System.Drawing.Point(79, 156);
            this.chart_shops.Name = "chart_shops";
            series9.ChartArea = "ChartArea1";
            series9.Legend = "Legend1";
            series9.Name = "Profit";
            this.chart_shops.Series.Add(series9);
            this.chart_shops.Size = new System.Drawing.Size(544, 440);
            this.chart_shops.TabIndex = 8;
            this.chart_shops.Text = "chart1";
            // 
            // chart_spentMoney
            // 
            chartArea6.Name = "ChartArea1";
            this.chart_spentMoney.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart_spentMoney.Legends.Add(legend6);
            this.chart_spentMoney.Location = new System.Drawing.Point(681, 156);
            this.chart_spentMoney.Name = "chart_spentMoney";
            series10.ChartArea = "ChartArea1";
            series10.Legend = "Legend1";
            series10.Name = "Daily profit from shops";
            series11.ChartArea = "ChartArea1";
            series11.Legend = "Legend1";
            series11.Name = "Daily profit from Rental shop";
            series12.ChartArea = "ChartArea1";
            series12.Legend = "Legend1";
            series12.Name = "Daily profit from purchase shop";
            this.chart_spentMoney.Series.Add(series10);
            this.chart_spentMoney.Series.Add(series11);
            this.chart_spentMoney.Series.Add(series12);
            this.chart_spentMoney.Size = new System.Drawing.Size(614, 450);
            this.chart_spentMoney.TabIndex = 25;
            this.chart_spentMoney.Text = "chart1";
            // 
            // btn_exportToExcel
            // 
            this.btn_exportToExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_exportToExcel.BackgroundImage")));
            this.btn_exportToExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_exportToExcel.ImageIndex = 3;
            this.btn_exportToExcel.Location = new System.Drawing.Point(12, 198);
            this.btn_exportToExcel.Name = "btn_exportToExcel";
            this.btn_exportToExcel.Size = new System.Drawing.Size(47, 40);
            this.btn_exportToExcel.TabIndex = 26;
            this.btn_exportToExcel.UseVisualStyleBackColor = true;
            this.btn_exportToExcel.Click += new System.EventHandler(this.btn_exportToExcel_Click);
            // 
            // btn_pdf
            // 
            this.btn_pdf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_pdf.BackgroundImage")));
            this.btn_pdf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_pdf.Location = new System.Drawing.Point(12, 274);
            this.btn_pdf.Name = "btn_pdf";
            this.btn_pdf.Size = new System.Drawing.Size(47, 42);
            this.btn_pdf.TabIndex = 27;
            this.btn_pdf.UseVisualStyleBackColor = true;
            this.btn_pdf.Click += new System.EventHandler(this.btn_pdf_Click);
            // 
            // Charts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 748);
            this.Controls.Add(this.btn_pdf);
            this.Controls.Add(this.btn_exportToExcel);
            this.Controls.Add(this.chart_spentMoney);
            this.Controls.Add(this.chart_shops);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Charts";
            this.Text = "Charts";
            this.Load += new System.EventHandler(this.Charts_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_shops)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_spentMoney)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_shops;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_spentMoney;
        private System.Windows.Forms.Button btn_exportToExcel;
        private System.Windows.Forms.Button btn_pdf;
    }
}