namespace BinanceBot
{
    partial class OrdersToBeExecuted
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
            this.components = new System.ComponentModel.Container();
            this.btnCancelOpenOrders = new System.Windows.Forms.Button();
            this.nuPRTo = new System.Windows.Forms.NumericUpDown();
            this.nuPRFrom = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPriceRange = new System.Windows.Forms.CheckBox();
            this.cbOrderPairs = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFetchOrdersFromDb = new System.Windows.Forms.Button();
            this.openOrderGV = new System.Windows.Forms.DataGridView();
            this.cbOrderSide = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bSOpenGV = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nuPRTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPRFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openOrderGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSOpenGV)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancelOpenOrders
            // 
            this.btnCancelOpenOrders.Location = new System.Drawing.Point(542, 371);
            this.btnCancelOpenOrders.Name = "btnCancelOpenOrders";
            this.btnCancelOpenOrders.Size = new System.Drawing.Size(182, 23);
            this.btnCancelOpenOrders.TabIndex = 31;
            this.btnCancelOpenOrders.Text = "Create Orders";
            this.btnCancelOpenOrders.UseVisualStyleBackColor = true;
            this.btnCancelOpenOrders.Visible = false;
            this.btnCancelOpenOrders.Click += new System.EventHandler(this.btnCancelOpenOrders_Click);
            // 
            // nuPRTo
            // 
            this.nuPRTo.Enabled = false;
            this.nuPRTo.Location = new System.Drawing.Point(342, 70);
            this.nuPRTo.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nuPRTo.Name = "nuPRTo";
            this.nuPRTo.Size = new System.Drawing.Size(100, 23);
            this.nuPRTo.TabIndex = 30;
            // 
            // nuPRFrom
            // 
            this.nuPRFrom.Enabled = false;
            this.nuPRFrom.Location = new System.Drawing.Point(208, 71);
            this.nuPRFrom.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nuPRFrom.Name = "nuPRFrom";
            this.nuPRFrom.Size = new System.Drawing.Size(100, 23);
            this.nuPRFrom.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "To:";
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Location = new System.Drawing.Point(685, 47);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(13, 15);
            this.lblRecordsCount.TabIndex = 28;
            this.lblRecordsCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "From:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(588, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Records Count: ";
            // 
            // cbPriceRange
            // 
            this.cbPriceRange.AutoSize = true;
            this.cbPriceRange.Location = new System.Drawing.Point(14, 73);
            this.cbPriceRange.Name = "cbPriceRange";
            this.cbPriceRange.Size = new System.Drawing.Size(126, 19);
            this.cbPriceRange.TabIndex = 26;
            this.cbPriceRange.Text = "Enable Price Range";
            this.cbPriceRange.UseVisualStyleBackColor = true;
            this.cbPriceRange.CheckedChanged += new System.EventHandler(this.cbPriceRange_CheckedChanged);
            // 
            // cbOrderPairs
            // 
            this.cbOrderPairs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderPairs.FormattingEnabled = true;
            this.cbOrderPairs.Items.AddRange(new object[] {
            "BTCBUSD",
            "ETHBUSD",
            "BNBBUSD"});
            this.cbOrderPairs.Location = new System.Drawing.Point(92, 8);
            this.cbOrderPairs.Name = "cbOrderPairs";
            this.cbOrderPairs.Size = new System.Drawing.Size(100, 23);
            this.cbOrderPairs.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "Pair:";
            // 
            // btnFetchOrdersFromDb
            // 
            this.btnFetchOrdersFromDb.Location = new System.Drawing.Point(544, 70);
            this.btnFetchOrdersFromDb.Name = "btnFetchOrdersFromDb";
            this.btnFetchOrdersFromDb.Size = new System.Drawing.Size(182, 23);
            this.btnFetchOrdersFromDb.TabIndex = 21;
            this.btnFetchOrdersFromDb.Text = "Fetch Orders From Db";
            this.btnFetchOrdersFromDb.UseVisualStyleBackColor = true;
            this.btnFetchOrdersFromDb.Click += new System.EventHandler(this.btnFetchOrdersFromDb_Click);
            // 
            // openOrderGV
            // 
            this.openOrderGV.AllowUserToAddRows = false;
            this.openOrderGV.AllowUserToOrderColumns = true;
            this.openOrderGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.openOrderGV.Location = new System.Drawing.Point(12, 99);
            this.openOrderGV.Name = "openOrderGV";
            this.openOrderGV.ReadOnly = true;
            this.openOrderGV.RowTemplate.Height = 25;
            this.openOrderGV.Size = new System.Drawing.Size(714, 266);
            this.openOrderGV.TabIndex = 20;
            this.openOrderGV.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.openOrderGV_RowsRemoved);
            // 
            // cbOrderSide
            // 
            this.cbOrderSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderSide.FormattingEnabled = true;
            this.cbOrderSide.Items.AddRange(new object[] {
            "ALL",
            "Buy",
            "Sell"});
            this.cbOrderSide.Location = new System.Drawing.Point(92, 44);
            this.cbOrderSide.Name = "cbOrderSide";
            this.cbOrderSide.Size = new System.Drawing.Size(100, 23);
            this.cbOrderSide.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "Order Side:";
            // 
            // OrdersToBeExecuted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 441);
            this.Controls.Add(this.cbOrderSide);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancelOpenOrders);
            this.Controls.Add(this.nuPRTo);
            this.Controls.Add(this.nuPRFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbPriceRange);
            this.Controls.Add(this.cbOrderPairs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnFetchOrdersFromDb);
            this.Controls.Add(this.openOrderGV);
            this.Name = "OrdersToBeExecuted";
            this.Text = "Orders To Be Executed from DB";
            ((System.ComponentModel.ISupportInitialize)(this.nuPRTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPRFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openOrderGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSOpenGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCancelOpenOrders;
        private NumericUpDown nuPRTo;
        private NumericUpDown nuPRFrom;
        private Label label2;
        private Label lblRecordsCount;
        private Label label1;
        private Label label3;
        private CheckBox cbPriceRange;
        private ComboBox cbOrderPairs;
        private Label label4;
        private Button btnFetchOrdersFromDb;
        private DataGridView openOrderGV;
        private ComboBox cbOrderSide;
        private Label label5;
        private BindingSource bSOpenGV;
    }
}