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
            menuStrip1 = new MenuStrip();
            operationsToolStripMenuItem = new ToolStripMenuItem();
            mIPlaceOrder = new ToolStripMenuItem();
            openOrdersToolStripMenuItem = new ToolStripMenuItem();
            ordersToExecuteToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { operationsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(747, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // operationsToolStripMenuItem
            // 
            operationsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mIPlaceOrder, openOrdersToolStripMenuItem, ordersToExecuteToolStripMenuItem });
            operationsToolStripMenuItem.Name = "operationsToolStripMenuItem";
            operationsToolStripMenuItem.Size = new Size(77, 20);
            operationsToolStripMenuItem.Text = "Operations";
            // 
            // mIPlaceOrder
            // 
            mIPlaceOrder.Name = "mIPlaceOrder";
            mIPlaceOrder.Size = new Size(194, 22);
            mIPlaceOrder.Text = "Place Orders";
            mIPlaceOrder.Click += mIPlaceOrder_Click;
            // 
            // openOrdersToolStripMenuItem
            // 
            openOrdersToolStripMenuItem.Name = "openOrdersToolStripMenuItem";
            openOrdersToolStripMenuItem.Size = new Size(194, 22);
            openOrdersToolStripMenuItem.Text = "Open Orders (Client)";
            openOrdersToolStripMenuItem.Click += openOrdersToolStripMenuItem_Click;
            // 
            // ordersToExecuteToolStripMenuItem
            // 
            ordersToExecuteToolStripMenuItem.Name = "ordersToExecuteToolStripMenuItem";
            ordersToExecuteToolStripMenuItem.Size = new Size(194, 22);
            ordersToExecuteToolStripMenuItem.Text = "Orders To Execute (Db)";
            ordersToExecuteToolStripMenuItem.Click += ordersToExecuteToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(747, 460);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Binance Bot - v27.0.0";
            TopMost = true;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem operationsToolStripMenuItem;
        private ToolStripMenuItem mIPlaceOrder;
        private ToolStripMenuItem openOrdersToolStripMenuItem;
        private ToolStripMenuItem ordersToExecuteToolStripMenuItem;
    }
}