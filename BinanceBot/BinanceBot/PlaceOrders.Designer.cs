namespace BinanceBot
{
    partial class PlaceOrders
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
            this.btnPlaceOrderSMBL = new System.Windows.Forms.Button();
            this.txtBUSDSellSMBL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSMBLCountDown = new System.Windows.Forms.Label();
            this.cbSMBL = new System.Windows.Forms.CheckBox();
            this.pbSMBL = new System.Windows.Forms.ProgressBar();
            this.labelOrdersExecutedSMBL = new System.Windows.Forms.Label();
            this.labelU2OSMBL = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.ndlSleepSMBL = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnStopSMBL = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.nUpDownMOrderSMBL = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPurchaseMarginSMBL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbOrderPairs = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblBMSLCountDown = new System.Windows.Forms.Label();
            this.cbBMSL = new System.Windows.Forms.CheckBox();
            this.pbBMSL = new System.Windows.Forms.ProgressBar();
            this.labelU2OBMSL = new System.Windows.Forms.Label();
            this.labelOrdersExecutedBMSL = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.nudSleepBMSL = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.btnStopBMSL = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.nUpDownMOrderBMSL = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSellMarginBMSL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBUSDBuyBMSL = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnPlaceOrderBMSL = new System.Windows.Forms.Button();
            this.nUDTrancsacFeeP = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndlSleepSMBL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownMOrderSMBL)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleepBMSL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownMOrderBMSL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTrancsacFeeP)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPlaceOrderSMBL
            // 
            this.btnPlaceOrderSMBL.Location = new System.Drawing.Point(56, 192);
            this.btnPlaceOrderSMBL.Name = "btnPlaceOrderSMBL";
            this.btnPlaceOrderSMBL.Size = new System.Drawing.Size(100, 23);
            this.btnPlaceOrderSMBL.TabIndex = 0;
            this.btnPlaceOrderSMBL.Text = "Start";
            this.btnPlaceOrderSMBL.UseVisualStyleBackColor = true;
            this.btnPlaceOrderSMBL.Click += new System.EventHandler(this.btnPlaceSellMarketBuyLimit_SMBL_Click);
            // 
            // txtBUSDSellSMBL
            // 
            this.txtBUSDSellSMBL.Location = new System.Drawing.Point(162, 32);
            this.txtBUSDSellSMBL.Name = "txtBUSDSellSMBL";
            this.txtBUSDSellSMBL.Size = new System.Drawing.Size(100, 23);
            this.txtBUSDSellSMBL.TabIndex = 1;
            this.txtBUSDSellSMBL.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total BUSD Sell:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblSMBLCountDown);
            this.panel1.Controls.Add(this.cbSMBL);
            this.panel1.Controls.Add(this.pbSMBL);
            this.panel1.Controls.Add(this.labelOrdersExecutedSMBL);
            this.panel1.Controls.Add(this.labelU2OSMBL);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.ndlSleepSMBL);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnStopSMBL);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.nUpDownMOrderSMBL);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtPurchaseMarginSMBL);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtBUSDSellSMBL);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPlaceOrderSMBL);
            this.panel1.Location = new System.Drawing.Point(31, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 313);
            this.panel1.TabIndex = 3;
            // 
            // lblSMBLCountDown
            // 
            this.lblSMBLCountDown.AutoSize = true;
            this.lblSMBLCountDown.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblSMBLCountDown.Location = new System.Drawing.Point(109, 279);
            this.lblSMBLCountDown.Name = "lblSMBLCountDown";
            this.lblSMBLCountDown.Size = new System.Drawing.Size(63, 15);
            this.lblSMBLCountDown.TabIndex = 8;
            this.lblSMBLCountDown.Text = "Wait 0 sec.";
            this.lblSMBLCountDown.Visible = false;
            // 
            // cbSMBL
            // 
            this.cbSMBL.AutoSize = true;
            this.cbSMBL.Checked = true;
            this.cbSMBL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSMBL.Location = new System.Drawing.Point(141, 163);
            this.cbSMBL.Name = "cbSMBL";
            this.cbSMBL.Size = new System.Drawing.Size(15, 14);
            this.cbSMBL.TabIndex = 22;
            this.cbSMBL.UseVisualStyleBackColor = true;
            this.cbSMBL.CheckedChanged += new System.EventHandler(this.cbSMBL_CheckedChanged);
            // 
            // pbSMBL
            // 
            this.pbSMBL.Location = new System.Drawing.Point(16, 275);
            this.pbSMBL.Name = "pbSMBL";
            this.pbSMBL.Size = new System.Drawing.Size(248, 23);
            this.pbSMBL.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbSMBL.TabIndex = 8;
            this.pbSMBL.Visible = false;
            // 
            // labelOrdersExecutedSMBL
            // 
            this.labelOrdersExecutedSMBL.AutoSize = true;
            this.labelOrdersExecutedSMBL.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelOrdersExecutedSMBL.Location = new System.Drawing.Point(162, 229);
            this.labelOrdersExecutedSMBL.Name = "labelOrdersExecutedSMBL";
            this.labelOrdersExecutedSMBL.Size = new System.Drawing.Size(13, 15);
            this.labelOrdersExecutedSMBL.TabIndex = 21;
            this.labelOrdersExecutedSMBL.Text = "0";
            // 
            // labelU2OSMBL
            // 
            this.labelU2OSMBL.AutoSize = true;
            this.labelU2OSMBL.Location = new System.Drawing.Point(162, 255);
            this.labelU2OSMBL.Name = "labelU2OSMBL";
            this.labelU2OSMBL.Size = new System.Drawing.Size(13, 15);
            this.labelU2OSMBL.TabIndex = 20;
            this.labelU2OSMBL.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(14, 255);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(140, 15);
            this.label16.TabIndex = 19;
            this.label16.Text = "Unsuccessful 2nd Orders:";
            // 
            // ndlSleepSMBL
            // 
            this.ndlSleepSMBL.Location = new System.Drawing.Point(162, 159);
            this.ndlSleepSMBL.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ndlSleepSMBL.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.ndlSleepSMBL.Name = "ndlSleepSMBL";
            this.ndlSleepSMBL.Size = new System.Drawing.Size(100, 23);
            this.ndlSleepSMBL.TabIndex = 16;
            this.ndlSleepSMBL.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "Thread.Sleep (ms):";
            // 
            // btnStopSMBL
            // 
            this.btnStopSMBL.Enabled = false;
            this.btnStopSMBL.Location = new System.Drawing.Point(162, 192);
            this.btnStopSMBL.Name = "btnStopSMBL";
            this.btnStopSMBL.Size = new System.Drawing.Size(100, 23);
            this.btnStopSMBL.TabIndex = 15;
            this.btnStopSMBL.Text = "Stop";
            this.btnStopSMBL.UseVisualStyleBackColor = true;
            this.btnStopSMBL.Click += new System.EventHandler(this.btnStopOrder_SMBL_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(65, 128);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(190, 15);
            this.label13.TabIndex = 14;
            this.label13.Text = "Place Market Sell - Limit Buy Order";
            // 
            // nUpDownMOrderSMBL
            // 
            this.nUpDownMOrderSMBL.Location = new System.Drawing.Point(162, 91);
            this.nUpDownMOrderSMBL.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nUpDownMOrderSMBL.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUpDownMOrderSMBL.Name = "nUpDownMOrderSMBL";
            this.nUpDownMOrderSMBL.Size = new System.Drawing.Size(100, 23);
            this.nUpDownMOrderSMBL.TabIndex = 6;
            this.nUpDownMOrderSMBL.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 229);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 15);
            this.label11.TabIndex = 13;
            this.label11.Text = "Successful Order Executed:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 15);
            this.label9.TabIndex = 9;
            this.label9.Text = "Max Order executed:";
            // 
            // txtPurchaseMarginSMBL
            // 
            this.txtPurchaseMarginSMBL.Location = new System.Drawing.Point(162, 59);
            this.txtPurchaseMarginSMBL.Name = "txtPurchaseMarginSMBL";
            this.txtPurchaseMarginSMBL.Size = new System.Drawing.Size(100, 23);
            this.txtPurchaseMarginSMBL.TabIndex = 4;
            this.txtPurchaseMarginSMBL.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 62);
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
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sell at market and then buy with limit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pair:";
            // 
            // cbOrderPairs
            // 
            this.cbOrderPairs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderPairs.FormattingEnabled = true;
            this.cbOrderPairs.Items.AddRange(new object[] {
            "BTCBUSD",
            "ETHBUSD",
            "BNBBUSD"});
            this.cbOrderPairs.Location = new System.Drawing.Point(117, 12);
            this.cbOrderPairs.Name = "cbOrderPairs";
            this.cbOrderPairs.Size = new System.Drawing.Size(100, 23);
            this.cbOrderPairs.TabIndex = 6;
            this.cbOrderPairs.SelectedIndexChanged += new System.EventHandler(this.cbOrderPairs_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblBMSLCountDown);
            this.panel2.Controls.Add(this.cbBMSL);
            this.panel2.Controls.Add(this.pbBMSL);
            this.panel2.Controls.Add(this.labelU2OBMSL);
            this.panel2.Controls.Add(this.labelOrdersExecutedBMSL);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.nudSleepBMSL);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.btnStopBMSL);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.nUpDownMOrderBMSL);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtSellMarginBMSL);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtBUSDBuyBMSL);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.btnPlaceOrderBMSL);
            this.panel2.Location = new System.Drawing.Point(367, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 313);
            this.panel2.TabIndex = 4;
            // 
            // lblBMSLCountDown
            // 
            this.lblBMSLCountDown.AutoSize = true;
            this.lblBMSLCountDown.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblBMSLCountDown.Location = new System.Drawing.Point(113, 279);
            this.lblBMSLCountDown.Name = "lblBMSLCountDown";
            this.lblBMSLCountDown.Size = new System.Drawing.Size(63, 15);
            this.lblBMSLCountDown.TabIndex = 26;
            this.lblBMSLCountDown.Text = "Wait 0 sec.";
            this.lblBMSLCountDown.Visible = false;
            // 
            // cbBMSL
            // 
            this.cbBMSL.AutoSize = true;
            this.cbBMSL.Checked = true;
            this.cbBMSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBMSL.Location = new System.Drawing.Point(139, 163);
            this.cbBMSL.Name = "cbBMSL";
            this.cbBMSL.Size = new System.Drawing.Size(15, 14);
            this.cbBMSL.TabIndex = 25;
            this.cbBMSL.UseVisualStyleBackColor = true;
            this.cbBMSL.CheckedChanged += new System.EventHandler(this.cbBMSL_CheckedChanged);
            // 
            // pbBMSL
            // 
            this.pbBMSL.Location = new System.Drawing.Point(15, 275);
            this.pbBMSL.Name = "pbBMSL";
            this.pbBMSL.Size = new System.Drawing.Size(248, 23);
            this.pbBMSL.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbBMSL.TabIndex = 24;
            this.pbBMSL.Visible = false;
            // 
            // labelU2OBMSL
            // 
            this.labelU2OBMSL.AutoSize = true;
            this.labelU2OBMSL.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelU2OBMSL.Location = new System.Drawing.Point(161, 255);
            this.labelU2OBMSL.Name = "labelU2OBMSL";
            this.labelU2OBMSL.Size = new System.Drawing.Size(13, 15);
            this.labelU2OBMSL.TabIndex = 23;
            this.labelU2OBMSL.Text = "0";
            // 
            // labelOrdersExecutedBMSL
            // 
            this.labelOrdersExecutedBMSL.AutoSize = true;
            this.labelOrdersExecutedBMSL.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelOrdersExecutedBMSL.Location = new System.Drawing.Point(161, 224);
            this.labelOrdersExecutedBMSL.Name = "labelOrdersExecutedBMSL";
            this.labelOrdersExecutedBMSL.Size = new System.Drawing.Size(13, 15);
            this.labelOrdersExecutedBMSL.TabIndex = 22;
            this.labelOrdersExecutedBMSL.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 255);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(140, 15);
            this.label17.TabIndex = 21;
            this.label17.Text = "Unsuccessful 2nd Orders:";
            // 
            // nudSleepBMSL
            // 
            this.nudSleepBMSL.Location = new System.Drawing.Point(161, 159);
            this.nudSleepBMSL.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSleepBMSL.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudSleepBMSL.Name = "nudSleepBMSL";
            this.nudSleepBMSL.Size = new System.Drawing.Size(100, 23);
            this.nudSleepBMSL.TabIndex = 18;
            this.nudSleepBMSL.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(29, 162);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 15);
            this.label15.TabIndex = 19;
            this.label15.Text = "Thread.Sleep (ms):";
            // 
            // btnStopBMSL
            // 
            this.btnStopBMSL.Enabled = false;
            this.btnStopBMSL.Location = new System.Drawing.Point(161, 192);
            this.btnStopBMSL.Name = "btnStopBMSL";
            this.btnStopBMSL.Size = new System.Drawing.Size(100, 23);
            this.btnStopBMSL.TabIndex = 17;
            this.btnStopBMSL.Text = "Stop";
            this.btnStopBMSL.UseVisualStyleBackColor = true;
            this.btnStopBMSL.Click += new System.EventHandler(this.btnStopOrder_BMSL_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(85, 136);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(157, 15);
            this.label14.TabIndex = 16;
            this.label14.Text = "Place Market Buy - Limit Sell";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 224);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 15);
            this.label12.TabIndex = 15;
            this.label12.Text = "Successful Order Executed:";
            // 
            // nUpDownMOrderBMSL
            // 
            this.nUpDownMOrderBMSL.Location = new System.Drawing.Point(161, 97);
            this.nUpDownMOrderBMSL.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nUpDownMOrderBMSL.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUpDownMOrderBMSL.Name = "nUpDownMOrderBMSL";
            this.nUpDownMOrderBMSL.Size = new System.Drawing.Size(100, 23);
            this.nUpDownMOrderBMSL.TabIndex = 10;
            this.nUpDownMOrderBMSL.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(38, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 15);
            this.label10.TabIndex = 11;
            this.label10.Text = "Max Order executed:";
            // 
            // txtSellMarginBMSL
            // 
            this.txtSellMarginBMSL.Location = new System.Drawing.Point(161, 64);
            this.txtSellMarginBMSL.Name = "txtSellMarginBMSL";
            this.txtSellMarginBMSL.Size = new System.Drawing.Size(100, 23);
            this.txtSellMarginBMSL.TabIndex = 4;
            this.txtSellMarginBMSL.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 67);
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
            this.label7.Location = new System.Drawing.Point(22, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(241, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Buy at market and Sell with Limit";
            // 
            // txtBUSDBuyBMSL
            // 
            this.txtBUSDBuyBMSL.Location = new System.Drawing.Point(161, 37);
            this.txtBUSDBuyBMSL.Name = "txtBUSDBuyBMSL";
            this.txtBUSDBuyBMSL.Size = new System.Drawing.Size(100, 23);
            this.txtBUSDBuyBMSL.TabIndex = 1;
            this.txtBUSDBuyBMSL.Text = "20";
            this.txtBUSDBuyBMSL.Leave += new System.EventHandler(this.txtBUSDBuyBMSL_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(67, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Total BUSD Buy:";
            // 
            // btnPlaceOrderBMSL
            // 
            this.btnPlaceOrderBMSL.Location = new System.Drawing.Point(57, 192);
            this.btnPlaceOrderBMSL.Name = "btnPlaceOrderBMSL";
            this.btnPlaceOrderBMSL.Size = new System.Drawing.Size(100, 23);
            this.btnPlaceOrderBMSL.TabIndex = 0;
            this.btnPlaceOrderBMSL.Text = "Start";
            this.btnPlaceOrderBMSL.UseVisualStyleBackColor = true;
            this.btnPlaceOrderBMSL.Click += new System.EventHandler(this.btnPlaceBuyMarketSellLimit_BMSL_Click);
            // 
            // nUDTrancsacFeeP
            // 
            this.nUDTrancsacFeeP.DecimalPlaces = 3;
            this.nUDTrancsacFeeP.Location = new System.Drawing.Point(117, 41);
            this.nUDTrancsacFeeP.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUDTrancsacFeeP.Name = "nUDTrancsacFeeP";
            this.nUDTrancsacFeeP.Size = new System.Drawing.Size(100, 23);
            this.nUDTrancsacFeeP.TabIndex = 10;
            this.nUDTrancsacFeeP.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(31, 43);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 15);
            this.label18.TabIndex = 11;
            this.label18.Text = "Transac Fee%:";
            // 
            // PlaceOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 410);
            this.Controls.Add(this.nUDTrancsacFeeP);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbOrderPairs);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "PlaceOrders";
            this.Text = "Binance Bot";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndlSleepSMBL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownMOrderSMBL)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleepBMSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownMOrderBMSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTrancsacFeeP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnPlaceOrderSMBL;
        private TextBox txtBUSDSellSMBL;
        private Label label1;
        private Panel panel1;
        private Label label2;
        private TextBox txtPurchaseMarginSMBL;
        private Label label3;
        private Label label4;
        private ComboBox cbOrderPairs;
        private Panel panel2;
        private TextBox txtSellMarginBMSL;
        private Label label6;
        private Label label7;
        private TextBox txtBUSDBuyBMSL;
        private Label label8;
        private Button btnPlaceOrderBMSL;
        private Label label9;
        private Label label11;
        private NumericUpDown nUpDownMOrderSMBL;
        private Label label12;
        private NumericUpDown nUpDownMOrderBMSL;
        private Label label10;
        private Button btnStopSMBL;
        private Label label13;
        private Button btnStopBMSL;
        private Label label14;
        private NumericUpDown ndlSleepSMBL;
        private Label label5;
        private NumericUpDown nudSleepBMSL;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label labelU2OSMBL;
        private Label labelOrdersExecutedSMBL;
        private Label labelOrdersExecutedBMSL;
        private Label labelU2OBMSL;
        private ProgressBar pbSMBL;
        private ProgressBar pbBMSL;
        private CheckBox cbSMBL;
        private CheckBox cbBMSL;
        private Label lblSMBLCountDown;
        private Label lblBMSLCountDown;
        private NumericUpDown nUDTrancsacFeeP;
        private Label label18;
    }
}