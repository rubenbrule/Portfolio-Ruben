namespace Entrance_Application {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_rfid = new System.Windows.Forms.Label();
            this.lb_name = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lb_rental = new System.Windows.Forms.ListBox();
            this.lb_DateTimeIn = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.lb_DateTimeIn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-12, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 123);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(162, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(522, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Return Item Application";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(-12, 635);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(958, 56);
            this.panel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(276, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(400, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "All Rights Reserved © G5 Software Solutions";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(25, 611);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(60, 20);
            this.label45.TabIndex = 3;
            this.label45.Text = "label45";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 532);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 53);
            this.button1.TabIndex = 5;
            this.button1.Text = "Return All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(533, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Visitor: ";
            // 
            // lb_rfid
            // 
            this.lb_rfid.AutoSize = true;
            this.lb_rfid.Location = new System.Drawing.Point(698, 190);
            this.lb_rfid.Name = "lb_rfid";
            this.lb_rfid.Size = new System.Drawing.Size(51, 20);
            this.lb_rfid.TabIndex = 8;
            this.lb_rfid.Text = "label4";
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(621, 190);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(51, 20);
            this.lb_name.TabIndex = 9;
            this.lb_name.Text = "label5";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(563, 532);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 53);
            this.button2.TabIndex = 10;
            this.button2.Text = "Return";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lb_rental
            // 
            this.lb_rental.FormattingEnabled = true;
            this.lb_rental.ItemHeight = 20;
            this.lb_rental.Location = new System.Drawing.Point(16, 238);
            this.lb_rental.Name = "lb_rental";
            this.lb_rental.Size = new System.Drawing.Size(908, 284);
            this.lb_rental.TabIndex = 11;
            // 
            // lb_DateTimeIn
            // 
            this.lb_DateTimeIn.AutoSize = true;
            this.lb_DateTimeIn.ForeColor = System.Drawing.SystemColors.Control;
            this.lb_DateTimeIn.Location = new System.Drawing.Point(725, 87);
            this.lb_DateTimeIn.Name = "lb_DateTimeIn";
            this.lb_DateTimeIn.Size = new System.Drawing.Size(51, 20);
            this.lb_DateTimeIn.TabIndex = 12;
            this.lb_DateTimeIn.Text = "label4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(936, 678);
            this.Controls.Add(this.lb_rental);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lb_name);
            this.Controls.Add(this.lb_rfid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_rfid;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox lb_rental;
        private System.Windows.Forms.Label lb_DateTimeIn;
    }
}

