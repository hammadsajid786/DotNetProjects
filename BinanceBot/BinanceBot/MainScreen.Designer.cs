namespace BinanceBot
{
    partial class MainScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPlaceMarketOrderMSLB = new System.Windows.Forms.Button();
            this.txtBUSDSellMSLB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nUpDownControlSMBL = new System.Windows.Forms.NumericUpDown();
            this.txtOrdersExecutedSMBL = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPairsMSLB = new System.Windows.Forms.ComboBox();
            this.txtPurchaseMarginMSLB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtOrdersExecutedBMSL = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.nUpDownControlBMSL = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPairsBMSL = new System.Windows.Forms.ComboBox();
            this.txtSellMarginBMSL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBUSDBuyBMSL = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnMarketBuyLimitSell = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownControlSMBL)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownControlBMSL)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPlaceMarketOrderMSLB
            // 
            this.btnPlaceMarketOrderMSLB.Location = new System.Drawing.Point(43, 151);
            this.btnPlaceMarketOrderMSLB.Name = "btnPlaceMarketOrderMSLB";
            this.btnPlaceMarketOrderMSLB.Size = new System.Drawing.Size(234, 23);
            this.btnPlaceMarketOrderMSLB.TabIndex = 0;
            this.btnPlaceMarketOrderMSLB.Text = "Place Market Sell - Limit Buy Order";
            this.btnPlaceMarketOrderMSLB.UseVisualStyleBackColor = true;
            this.btnPlaceMarketOrderMSLB.Click += new System.EventHandler(this.btnPlaceMarketOrderMSLB_Click);
            // 
            // txtBUSDSellMSLB
            // 
            this.txtBUSDSellMSLB.Location = new System.Drawing.Point(177, 55);
            this.txtBUSDSellMSLB.Name = "txtBUSDSellMSLB";
            this.txtBUSDSellMSLB.Size = new System.Drawing.Size(100, 23);
            this.txtBUSDSellMSLB.TabIndex = 1;
            this.txtBUSDSellMSLB.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total BUSD Sell:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.nUpDownControlSMBL);
            this.panel1.Controls.Add(this.txtOrdersExecutedSMBL);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbPairsMSLB);
            this.panel1.Controls.Add(this.txtPurchaseMarginMSLB);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtBUSDSellMSLB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPlaceMarketOrderMSLB);
            this.panel1.Location = new System.Drawing.Point(32, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 223);
            this.panel1.TabIndex = 3;
            // 
            // nUpDownControlSMBL
            // 
            this.nUpDownControlSMBL.Location = new System.Drawing.Point(177, 114);
            this.nUpDownControlSMBL.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nUpDownControlSMBL.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUpDownControlSMBL.Name = "nUpDownControlSMBL";
            this.nUpDownControlSMBL.Size = new System.Drawing.Size(100, 23);
            this.nUpDownControlSMBL.TabIndex = 6;
            this.nUpDownControlSMBL.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // txtOrdersExecutedSMBL
            // 
            this.txtOrdersExecutedSMBL.Enabled = false;
            this.txtOrdersExecutedSMBL.Location = new System.Drawing.Point(177, 180);
            this.txtOrdersExecutedSMBL.Name = "txtOrdersExecutedSMBL";
            this.txtOrdersExecutedSMBL.Size = new System.Drawing.Size(100, 23);
            this.txtOrdersExecutedSMBL.TabIndex = 12;
            this.txtOrdersExecutedSMBL.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(80, 183);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 15);
            this.label11.TabIndex = 13;
            this.label11.Text = "Order Executed:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(54, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 15);
            this.label9.TabIndex = 9;
            this.label9.Text = "Max Order executed:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pair:";
            // 
            // cbPairsMSLB
            // 
            this.cbPairsMSLB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPairsMSLB.FormattingEnabled = true;
            this.cbPairsMSLB.Items.AddRange(new object[] {
            "BTCBUSD",
            "ETHBUSD",
            "BNBBUSD"});
            this.cbPairsMSLB.Location = new System.Drawing.Point(177, 26);
            this.cbPairsMSLB.Name = "cbPairsMSLB";
            this.cbPairsMSLB.Size = new System.Drawing.Size(100, 23);
            this.cbPairsMSLB.TabIndex = 6;
            // 
            // txtPurchaseMarginMSLB
            // 
            this.txtPurchaseMarginMSLB.Location = new System.Drawing.Point(177, 82);
            this.txtPurchaseMarginMSLB.Name = "txtPurchaseMarginMSLB";
            this.txtPurchaseMarginMSLB.Size = new System.Drawing.Size(100, 23);
            this.txtPurchaseMarginMSLB.TabIndex = 4;
            this.txtPurchaseMarginMSLB.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Purchase Price margin:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(14, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sell at market and then buy with limit";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtOrdersExecutedBMSL);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.nUpDownControlBMSL);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbPairsBMSL);
            this.panel2.Controls.Add(this.txtSellMarginBMSL);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtBUSDBuyBMSL);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.btnMarketBuyLimitSell);
            this.panel2.Location = new System.Drawing.Point(368, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(281, 223);
            this.panel2.TabIndex = 4;
            // 
            // txtOrdersExecutedBMSL
            // 
            this.txtOrdersExecutedBMSL.Enabled = false;
            this.txtOrdersExecutedBMSL.Location = new System.Drawing.Point(155, 180);
            this.txtOrdersExecutedBMSL.Name = "txtOrdersExecutedBMSL";
            this.txtOrdersExecutedBMSL.Size = new System.Drawing.Size(100, 23);
            this.txtOrdersExecutedBMSL.TabIndex = 14;
            this.txtOrdersExecutedBMSL.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(58, 183);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 15);
            this.label12.TabIndex = 15;
            this.label12.Text = "Order Executed:";
            // 
            // nUpDownControlBMSL
            // 
            this.nUpDownControlBMSL.Location = new System.Drawing.Point(155, 112);
            this.nUpDownControlBMSL.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nUpDownControlBMSL.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUpDownControlBMSL.Name = "nUpDownControlBMSL";
            this.nUpDownControlBMSL.Size = new System.Drawing.Size(100, 23);
            this.nUpDownControlBMSL.TabIndex = 10;
            this.nUpDownControlBMSL.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 15);
            this.label10.TabIndex = 11;
            this.label10.Text = "Max Order executed:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Pair:";
            // 
            // cbPairsBMSL
            // 
            this.cbPairsBMSL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPairsBMSL.FormattingEnabled = true;
            this.cbPairsBMSL.Items.AddRange(new object[] {
            "BTCBUSD",
            "ETHBUSD",
            "BNBBUSD"});
            this.cbPairsBMSL.Location = new System.Drawing.Point(155, 23);
            this.cbPairsBMSL.Name = "cbPairsBMSL";
            this.cbPairsBMSL.Size = new System.Drawing.Size(100, 23);
            this.cbPairsBMSL.TabIndex = 6;
            // 
            // txtSellMarginBMSL
            // 
            this.txtSellMarginBMSL.Location = new System.Drawing.Point(155, 79);
            this.txtSellMarginBMSL.Name = "txtSellMarginBMSL";
            this.txtSellMarginBMSL.Size = new System.Drawing.Size(100, 23);
            this.txtSellMarginBMSL.TabIndex = 4;
            this.txtSellMarginBMSL.Text = "2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Purchase Price margin:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(23, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(241, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Buy at market and Sell with Limit";
            // 
            // txtBUSDBuyBMSL
            // 
            this.txtBUSDBuyBMSL.Location = new System.Drawing.Point(155, 52);
            this.txtBUSDBuyBMSL.Name = "txtBUSDBuyBMSL";
            this.txtBUSDBuyBMSL.Size = new System.Drawing.Size(100, 23);
            this.txtBUSDBuyBMSL.TabIndex = 1;
            this.txtBUSDBuyBMSL.Text = "50";
            this.txtBUSDBuyBMSL.Leave += new System.EventHandler(this.txtBUSDBuyBMSL_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Total BUSD Buy:";
            // 
            // btnMarketBuyLimitSell
            // 
            this.btnMarketBuyLimitSell.Location = new System.Drawing.Point(21, 151);
            this.btnMarketBuyLimitSell.Name = "btnMarketBuyLimitSell";
            this.btnMarketBuyLimitSell.Size = new System.Drawing.Size(234, 23);
            this.btnMarketBuyLimitSell.TabIndex = 0;
            this.btnMarketBuyLimitSell.Text = "Place Market Buy - Limit Sell";
            this.btnMarketBuyLimitSell.UseVisualStyleBackColor = true;
            this.btnMarketBuyLimitSell.Click += new System.EventHandler(this.btnMarketBuyLimitSell_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 273);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.Text = "Binance Bot";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownControlSMBL)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownControlBMSL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnPlaceMarketOrderMSLB;
        private TextBox txtBUSDSellMSLB;
        private Label label1;
        private Panel panel1;
        private Label label2;
        private TextBox txtPurchaseMarginMSLB;
        private Label label3;
        private Label label4;
        private ComboBox cbPairsMSLB;
        private Panel panel2;
        private Label label5;
        private ComboBox cbPairsBMSL;
        private TextBox txtSellMarginBMSL;
        private Label label6;
        private Label label7;
        private TextBox txtBUSDBuyBMSL;
        private Label label8;
        private Button btnMarketBuyLimitSell;
        private Label label9;
        private TextBox txtOrdersExecutedSMBL;
        private Label label11;
        private NumericUpDown nUpDownControlSMBL;
        private TextBox txtOrdersExecutedBMSL;
        private Label label12;
        private NumericUpDown nUpDownControlBMSL;
        private Label label10;
    }
}