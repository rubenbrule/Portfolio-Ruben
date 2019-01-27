namespace Store_Application
{
    partial class AdjustQuantity
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
            this.numUpDown_quantity = new System.Windows.Forms.NumericUpDown();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_deleteItem = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_quantity)).BeginInit();
            this.SuspendLayout();
            // 
            // numUpDown_quantity
            // 
            this.numUpDown_quantity.Dock = System.Windows.Forms.DockStyle.Top;
            this.numUpDown_quantity.Location = new System.Drawing.Point(0, 0);
            this.numUpDown_quantity.Name = "numUpDown_quantity";
            this.numUpDown_quantity.Size = new System.Drawing.Size(313, 26);
            this.numUpDown_quantity.TabIndex = 0;
            this.numUpDown_quantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_ok.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ok.Location = new System.Drawing.Point(0, 26);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(313, 51);
            this.btn_ok.TabIndex = 3;
            this.btn_ok.Text = "Quantity";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_deleteItem
            // 
            this.btn_deleteItem.BackColor = System.Drawing.Color.Red;
            this.btn_deleteItem.Location = new System.Drawing.Point(-1, 92);
            this.btn_deleteItem.Name = "btn_deleteItem";
            this.btn_deleteItem.Size = new System.Drawing.Size(313, 64);
            this.btn_deleteItem.TabIndex = 4;
            this.btn_deleteItem.Text = "Remove";
            this.btn_deleteItem.UseVisualStyleBackColor = false;
            this.btn_deleteItem.Click += new System.EventHandler(this.btn_deleteItem_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.PaleGreen;
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_cancel.Location = new System.Drawing.Point(0, 181);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(313, 59);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // AdjustQuantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(313, 240);
            this.ControlBox = false;
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_deleteItem);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.numUpDown_quantity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdjustQuantity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdjustQuantity";
            this.Load += new System.EventHandler(this.AdjustQuantity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_quantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numUpDown_quantity;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_deleteItem;
        private System.Windows.Forms.Button btn_cancel;
    }
}