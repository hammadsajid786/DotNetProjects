namespace BinanceBot
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.operationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mIPlaceOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.openOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToExecuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operationsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(747, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // operationsToolStripMenuItem
            // 
            this.operationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mIPlaceOrder,
            this.openOrdersToolStripMenuItem,
            this.ordersToExecuteToolStripMenuItem});
            this.operationsToolStripMenuItem.Name = "operationsToolStripMenuItem";
            this.operationsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.operationsToolStripMenuItem.Text = "Operations";
            // 
            // mIPlaceOrder
            // 
            this.mIPlaceOrder.Name = "mIPlaceOrder";
            this.mIPlaceOrder.Size = new System.Drawing.Size(194, 22);
            this.mIPlaceOrder.Text = "Place Orders";
            this.mIPlaceOrder.Click += new System.EventHandler(this.mIPlaceOrder_Click);
            // 
            // openOrdersToolStripMenuItem
            // 
            this.openOrdersToolStripMenuItem.Name = "openOrdersToolStripMenuItem";
            this.openOrdersToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.openOrdersToolStripMenuItem.Text = "Open Orders (Client)";
            this.openOrdersToolStripMenuItem.Click += new System.EventHandler(this.openOrdersToolStripMenuItem_Click);
            // 
            // ordersToExecuteToolStripMenuItem
            // 
            this.ordersToExecuteToolStripMenuItem.Name = "ordersToExecuteToolStripMenuItem";
            this.ordersToExecuteToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.ordersToExecuteToolStripMenuItem.Text = "Orders To Execute (Db)";
            this.ordersToExecuteToolStripMenuItem.Click += new System.EventHandler(this.ordersToExecuteToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 426);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Binance Bot - v18.2.0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem operationsToolStripMenuItem;
        private ToolStripMenuItem mIPlaceOrder;
        private ToolStripMenuItem openOrdersToolStripMenuItem;
        private ToolStripMenuItem ordersToExecuteToolStripMenuItem;
    }
}