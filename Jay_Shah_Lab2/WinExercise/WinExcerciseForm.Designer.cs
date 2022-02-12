namespace WinExercise
{
    partial class WinExcerciseForm
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
            this.btnShowTracker = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblVal = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.txtValSquared = new System.Windows.Forms.TextBox();
            this.lblValSquared = new System.Windows.Forms.Label();
            this.txtValCubed = new System.Windows.Forms.TextBox();
            this.lblValCubed = new System.Windows.Forms.Label();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.lblFName = new System.Windows.Forms.Label();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.lbnLName = new System.Windows.Forms.Label();
            this.lblInterpolatedName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnShowTracker
            // 
            this.btnShowTracker.Location = new System.Drawing.Point(12, 12);
            this.btnShowTracker.Name = "btnShowTracker";
            this.btnShowTracker.Size = new System.Drawing.Size(106, 23);
            this.btnShowTracker.TabIndex = 0;
            this.btnShowTracker.Text = "Show &Tracker";
            this.btnShowTracker.UseVisualStyleBackColor = true;
            this.btnShowTracker.Click += new System.EventHandler(this.btnShowTracker_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 42);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(446, 23);
            this.progressBar.TabIndex = 1;
            // 
            // lblVal
            // 
            this.lblVal.AutoSize = true;
            this.lblVal.Location = new System.Drawing.Point(13, 132);
            this.lblVal.Name = "lblVal";
            this.lblVal.Size = new System.Drawing.Size(34, 13);
            this.lblVal.TabIndex = 2;
            this.lblVal.Text = "&Value";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(16, 149);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 20);
            this.txtValue.TabIndex = 3;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(140, 149);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "&Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // txtValSquared
            // 
            this.txtValSquared.Location = new System.Drawing.Point(253, 149);
            this.txtValSquared.Name = "txtValSquared";
            this.txtValSquared.Size = new System.Drawing.Size(100, 20);
            this.txtValSquared.TabIndex = 6;
            // 
            // lblValSquared
            // 
            this.lblValSquared.AutoSize = true;
            this.lblValSquared.Location = new System.Drawing.Point(250, 132);
            this.lblValSquared.Name = "lblValSquared";
            this.lblValSquared.Size = new System.Drawing.Size(77, 13);
            this.lblValSquared.TabIndex = 5;
            this.lblValSquared.Text = "&Value Squared";
            // 
            // txtValCubed
            // 
            this.txtValCubed.Location = new System.Drawing.Point(359, 149);
            this.txtValCubed.Name = "txtValCubed";
            this.txtValCubed.Size = new System.Drawing.Size(100, 20);
            this.txtValCubed.TabIndex = 8;
            // 
            // lblValCubed
            // 
            this.lblValCubed.AutoSize = true;
            this.lblValCubed.Location = new System.Drawing.Point(356, 132);
            this.lblValCubed.Name = "lblValCubed";
            this.lblValCubed.Size = new System.Drawing.Size(68, 13);
            this.lblValCubed.TabIndex = 7;
            this.lblValCubed.Text = "&Value Cubed";
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(18, 207);
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(100, 20);
            this.txtFName.TabIndex = 10;
            this.txtFName.TextChanged += new System.EventHandler(this.txtFName_TextChanged);
            // 
            // lblFName
            // 
            this.lblFName.AutoSize = true;
            this.lblFName.Location = new System.Drawing.Point(15, 190);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(57, 13);
            this.lblFName.TabIndex = 9;
            this.lblFName.Text = "&First Name";
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(140, 207);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(100, 20);
            this.txtLName.TabIndex = 12;
            this.txtLName.TextChanged += new System.EventHandler(this.txtFName_TextChanged);
            // 
            // lbnLName
            // 
            this.lbnLName.AutoSize = true;
            this.lbnLName.Location = new System.Drawing.Point(137, 190);
            this.lbnLName.Name = "lbnLName";
            this.lbnLName.Size = new System.Drawing.Size(58, 13);
            this.lbnLName.TabIndex = 11;
            this.lbnLName.Text = "&Last Name";
            // 
            // lblInterpolatedName
            // 
            this.lblInterpolatedName.AutoSize = true;
            this.lblInterpolatedName.Location = new System.Drawing.Point(20, 244);
            this.lblInterpolatedName.Name = "lblInterpolatedName";
            this.lblInterpolatedName.Size = new System.Drawing.Size(0, 13);
            this.lblInterpolatedName.TabIndex = 13;
            // 
            // WinExcerciseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 279);
            this.Controls.Add(this.lblInterpolatedName);
            this.Controls.Add(this.txtLName);
            this.Controls.Add(this.lbnLName);
            this.Controls.Add(this.txtFName);
            this.Controls.Add(this.lblFName);
            this.Controls.Add(this.txtValCubed);
            this.Controls.Add(this.lblValCubed);
            this.Controls.Add(this.txtValSquared);
            this.Controls.Add(this.lblValSquared);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblVal);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnShowTracker);
            this.Name = "WinExcerciseForm";
            this.Text = "Win Excercise";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShowTracker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblVal;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.TextBox txtValSquared;
        private System.Windows.Forms.Label lblValSquared;
        private System.Windows.Forms.TextBox txtValCubed;
        private System.Windows.Forms.Label lblValCubed;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.Label lblFName;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.Label lbnLName;
        private System.Windows.Forms.Label lblInterpolatedName;
    }
}

