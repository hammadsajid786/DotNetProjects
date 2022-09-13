namespace BinanceBot
{
    partial class OpenOrders
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
            this.openOrderGV = new System.Windows.Forms.DataGridView();
            this.btnFetchOpenOrders = new System.Windows.Forms.Button();
            this.cbOrderPairs = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPriceRange = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.nuPRFrom = new System.Windows.Forms.NumericUpDown();
            this.nuPRTo = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.openOrderGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPRFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPRTo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.openOrderGV.AllowUserToOrderColumns = true;
            this.openOrderGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.openOrderGV.Location = new System.Drawing.Point(12, 157);
            this.openOrderGV.Name = "dataGridView1";
            this.openOrderGV.ReadOnly = true;
            this.openOrderGV.RowTemplate.Height = 25;
            this.openOrderGV.Size = new System.Drawing.Size(714, 194);
            this.openOrderGV.TabIndex = 0;
            // 
            // btnFetchOpenOrders
            // 
            this.btnFetchOpenOrders.Location = new System.Drawing.Point(544, 128);
            this.btnFetchOpenOrders.Name = "btnFetchOpenOrders";
            this.btnFetchOpenOrders.Size = new System.Drawing.Size(182, 23);
            this.btnFetchOpenOrders.TabIndex = 1;
            this.btnFetchOpenOrders.Text = "Fetch Orders";
            this.btnFetchOpenOrders.UseVisualStyleBackColor = true;
            this.btnFetchOpenOrders.Click += new System.EventHandler(this.btnFetchOpenOrders_Click);
            // 
            // cbOrderPairs
            // 
            this.cbOrderPairs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderPairs.FormattingEnabled = true;
            this.cbOrderPairs.Items.AddRange(new object[] {
            "BTCBUSD",
            "ETHBUSD",
            "BNBBUSD"});
            this.cbOrderPairs.Location = new System.Drawing.Point(50, 12);
            this.cbOrderPairs.Name = "cbOrderPairs";
            this.cbOrderPairs.Size = new System.Drawing.Size(100, 23);
            this.cbOrderPairs.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Pair:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Price Range From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Price Range To:";
            // 
            // cbPriceRange
            // 
            this.cbPriceRange.AutoSize = true;
            this.cbPriceRange.Location = new System.Drawing.Point(10, 5);
            this.cbPriceRange.Name = "cbPriceRange";
            this.cbPriceRange.Size = new System.Drawing.Size(126, 19);
            this.cbPriceRange.TabIndex = 14;
            this.cbPriceRange.Text = "Enable Range Filter";
            this.cbPriceRange.UseVisualStyleBackColor = true;
            this.cbPriceRange.CheckedChanged += new System.EventHandler(this.cbPriceRange_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(588, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Records Count: ";
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Location = new System.Drawing.Point(685, 105);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(13, 15);
            this.lblRecordsCount.TabIndex = 16;
            this.lblRecordsCount.Text = "0";
            // 
            // nuPRFrom
            // 
            this.nuPRFrom.Enabled = false;
            this.nuPRFrom.Location = new System.Drawing.Point(119, 28);
            this.nuPRFrom.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nuPRFrom.Name = "nuPRFrom";
            this.nuPRFrom.Size = new System.Drawing.Size(100, 23);
            this.nuPRFrom.TabIndex = 17;
            // 
            // nuPRTo
            // 
            this.nuPRTo.Enabled = false;
            this.nuPRTo.Location = new System.Drawing.Point(119, 57);
            this.nuPRTo.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nuPRTo.Name = "nuPRTo";
            this.nuPRTo.Size = new System.Drawing.Size(100, 23);
            this.nuPRTo.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.nuPRFrom);
            this.panel1.Controls.Add(this.nuPRTo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbPriceRange);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(14, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 90);
            this.panel1.TabIndex = 19;
            // 
            // OpenOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 363);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbOrderPairs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnFetchOpenOrders);
            this.Controls.Add(this.openOrderGV);
            this.Name = "OpenOrders";
            this.Text = "OpenOrders";
            ((System.ComponentModel.ISupportInitialize)(this.openOrderGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPRFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPRTo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView openOrderGV;
        private Button btnFetchOpenOrders;
        private ComboBox cbOrderPairs;
        private Label label4;
        private Label label1;
        private Label label2;
        private CheckBox cbPriceRange;
        private Label label3;
        private Label lblRecordsCount;
        private NumericUpDown nuPRFrom;
        private NumericUpDown nuPRTo;
        private Panel panel1;
    }
}