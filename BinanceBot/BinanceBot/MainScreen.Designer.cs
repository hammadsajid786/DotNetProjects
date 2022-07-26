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
            this.txtOrdersExecuted = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbMaxOrderCountSMBL = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPairsMSLB = new System.Windows.Forms.ComboBox();
            this.txtPurchaseMarginMSLB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnMarketBuyLimitSell = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.panel1.Controls.Add(this.txtOrdersExecuted);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.cbMaxOrderCountSMBL);
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
            this.panel1.Size = new System.Drawing.Size(307, 236);
            this.panel1.TabIndex = 3;
            // 
            // txtOrdersExecuted
            // 
            this.txtOrdersExecuted.Enabled = false;
            this.txtOrdersExecuted.Location = new System.Drawing.Point(177, 180);
            this.txtOrdersExecuted.Name = "txtOrdersExecuted";
            this.txtOrdersExecuted.Size = new System.Drawing.Size(100, 23);
            this.txtOrdersExecuted.TabIndex = 12;
            this.txtOrdersExecuted.Text = "0";
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
            // cbMaxOrderCountSMBL
            // 
            this.cbMaxOrderCountSMBL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMaxOrderCountSMBL.FormattingEnabled = true;
            this.cbMaxOrderCountSMBL.Items.AddRange(new object[] {
            "10",
            "7",
            "5",
            "2"});
            this.cbMaxOrderCountSMBL.Location = new System.Drawing.Point(177, 113);
            this.cbMaxOrderCountSMBL.Name = "cbMaxOrderCountSMBL";
            this.cbMaxOrderCountSMBL.Size = new System.Drawing.Size(100, 23);
            this.cbMaxOrderCountSMBL.TabIndex = 10;
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
            "ETHBUSD"});
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
            this.txtPurchaseMarginMSLB.Text = "5";
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
            this.label2.Location = new System.Drawing.Point(14, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sell at market and then buy with limit";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.btnMarketBuyLimitSell);
            this.panel2.Location = new System.Drawing.Point(368, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(281, 195);
            this.panel2.TabIndex = 4;
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
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "BTCBUSD",
            "ETHBUSD"});
            this.comboBox1.Location = new System.Drawing.Point(155, 23);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 23);
            this.comboBox1.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(155, 79);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "5";
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
            this.label7.Location = new System.Drawing.Point(23, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(241, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Buy at market and Sell with Limit";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(155, 52);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 23);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "50";
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
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 271);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainScreen";
            this.Text = "Binance Bot";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private ComboBox comboBox1;
        private TextBox textBox1;
        private Label label6;
        private Label label7;
        private TextBox textBox2;
        private Label label8;
        private Button btnMarketBuyLimitSell;
        private ComboBox cbMaxOrderCountSMBL;
        private Label label9;
        private TextBox txtOrdersExecuted;
        private Label label11;
    }
}