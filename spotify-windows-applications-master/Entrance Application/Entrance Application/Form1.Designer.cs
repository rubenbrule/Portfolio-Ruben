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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_DateTimeIn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tab_entrance = new System.Windows.Forms.TabControl();
            this.tab_checkIn = new System.Windows.Forms.TabPage();
            this.lb_balaceCheckIn = new System.Windows.Forms.Label();
            this.lb_status = new System.Windows.Forms.Label();
            this.lb_msg = new System.Windows.Forms.Label();
            this.tab_checkOut = new System.Windows.Forms.TabPage();
            this.lb_balance = new System.Windows.Forms.Label();
            this.lb_checkOUt = new System.Windows.Forms.Label();
            this.btn_withdrawAndCheckOut = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.tab_entrance.SuspendLayout();
            this.tab_checkIn.SuspendLayout();
            this.tab_checkOut.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lb_DateTimeIn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-12, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 123);
            this.panel1.TabIndex = 0;
            // 
            // lb_DateTimeIn
            // 
            this.lb_DateTimeIn.AutoSize = true;
            this.lb_DateTimeIn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lb_DateTimeIn.Location = new System.Drawing.Point(729, 91);
            this.lb_DateTimeIn.Name = "lb_DateTimeIn";
            this.lb_DateTimeIn.Size = new System.Drawing.Size(51, 20);
            this.lb_DateTimeIn.TabIndex = 13;
            this.lb_DateTimeIn.Text = "label5";
            this.lb_DateTimeIn.Click += new System.EventHandler(this.lb_DateTimeIn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(271, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(632, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entrance Control Application";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tab_entrance
            // 
            this.tab_entrance.Controls.Add(this.tab_checkIn);
            this.tab_entrance.Controls.Add(this.tab_checkOut);
            this.tab_entrance.Location = new System.Drawing.Point(18, 132);
            this.tab_entrance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tab_entrance.Name = "tab_entrance";
            this.tab_entrance.SelectedIndex = 0;
            this.tab_entrance.Size = new System.Drawing.Size(900, 472);
            this.tab_entrance.TabIndex = 1;
            // 
            // tab_checkIn
            // 
            this.tab_checkIn.Controls.Add(this.lb_balaceCheckIn);
            this.tab_checkIn.Controls.Add(this.lb_status);
            this.tab_checkIn.Controls.Add(this.lb_msg);
            this.tab_checkIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_checkIn.Location = new System.Drawing.Point(4, 29);
            this.tab_checkIn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tab_checkIn.Name = "tab_checkIn";
            this.tab_checkIn.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tab_checkIn.Size = new System.Drawing.Size(892, 439);
            this.tab_checkIn.TabIndex = 0;
            this.tab_checkIn.Text = "Check In";
            this.tab_checkIn.UseVisualStyleBackColor = true;
            // 
            // lb_balaceCheckIn
            // 
            this.lb_balaceCheckIn.AutoSize = true;
            this.lb_balaceCheckIn.Location = new System.Drawing.Point(397, 257);
            this.lb_balaceCheckIn.Name = "lb_balaceCheckIn";
            this.lb_balaceCheckIn.Size = new System.Drawing.Size(53, 20);
            this.lb_balaceCheckIn.TabIndex = 12;
            this.lb_balaceCheckIn.Text = "label6";
            // 
            // lb_status
            // 
            this.lb_status.AutoSize = true;
            this.lb_status.Location = new System.Drawing.Point(325, 201);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(53, 20);
            this.lb_status.TabIndex = 8;
            this.lb_status.Text = "label5";
            // 
            // lb_msg
            // 
            this.lb_msg.AutoSize = true;
            this.lb_msg.Location = new System.Drawing.Point(142, 395);
            this.lb_msg.Name = "lb_msg";
            this.lb_msg.Size = new System.Drawing.Size(0, 20);
            this.lb_msg.TabIndex = 7;
            // 
            // tab_checkOut
            // 
            this.tab_checkOut.Controls.Add(this.lb_balance);
            this.tab_checkOut.Controls.Add(this.lb_checkOUt);
            this.tab_checkOut.Controls.Add(this.btn_withdrawAndCheckOut);
            this.tab_checkOut.Location = new System.Drawing.Point(4, 29);
            this.tab_checkOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tab_checkOut.Name = "tab_checkOut";
            this.tab_checkOut.Size = new System.Drawing.Size(892, 439);
            this.tab_checkOut.TabIndex = 2;
            this.tab_checkOut.Text = "Check Out";
            this.tab_checkOut.UseVisualStyleBackColor = true;
            // 
            // lb_balance
            // 
            this.lb_balance.AutoSize = true;
            this.lb_balance.Location = new System.Drawing.Point(365, 238);
            this.lb_balance.Name = "lb_balance";
            this.lb_balance.Size = new System.Drawing.Size(51, 20);
            this.lb_balance.TabIndex = 11;
            this.lb_balance.Text = "label6";
            // 
            // lb_checkOUt
            // 
            this.lb_checkOUt.AutoSize = true;
            this.lb_checkOUt.Location = new System.Drawing.Point(268, 178);
            this.lb_checkOUt.Name = "lb_checkOUt";
            this.lb_checkOUt.Size = new System.Drawing.Size(51, 20);
            this.lb_checkOUt.TabIndex = 10;
            this.lb_checkOUt.Text = "label5";
            // 
            // btn_withdrawAndCheckOut
            // 
            this.btn_withdrawAndCheckOut.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_withdrawAndCheckOut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_withdrawAndCheckOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_withdrawAndCheckOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_withdrawAndCheckOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_withdrawAndCheckOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_withdrawAndCheckOut.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_withdrawAndCheckOut.Location = new System.Drawing.Point(272, 382);
            this.btn_withdrawAndCheckOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_withdrawAndCheckOut.Name = "btn_withdrawAndCheckOut";
            this.btn_withdrawAndCheckOut.Size = new System.Drawing.Size(344, 35);
            this.btn_withdrawAndCheckOut.TabIndex = 9;
            this.btn_withdrawAndCheckOut.Text = "Withdraw And Check Out";
            this.btn_withdrawAndCheckOut.UseVisualStyleBackColor = false;
            this.btn_withdrawAndCheckOut.Click += new System.EventHandler(this.btn_withdrawAndCheckOut_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(18, 610);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 20);
            this.label14.TabIndex = 6;
            this.label14.Text = "Message:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.label45);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(-12, 635);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(958, 56);
            this.panel2.TabIndex = 2;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(24, 14);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(60, 20);
            this.label45.TabIndex = 3;
            this.label45.Text = "label45";
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
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(43, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 111);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(936, 678);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tab_entrance);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label14);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tab_entrance.ResumeLayout(false);
            this.tab_checkIn.ResumeLayout(false);
            this.tab_checkIn.PerformLayout();
            this.tab_checkOut.ResumeLayout(false);
            this.tab_checkOut.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tab_entrance;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tab_checkOut;
        private System.Windows.Forms.Button btn_withdrawAndCheckOut;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TabPage tab_checkIn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_msg;
        private System.Windows.Forms.Label lb_status;
        private System.Windows.Forms.Label lb_balance;
        private System.Windows.Forms.Label lb_checkOUt;
        private System.Windows.Forms.Label lb_balaceCheckIn;
        private System.Windows.Forms.Label lb_DateTimeIn;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

