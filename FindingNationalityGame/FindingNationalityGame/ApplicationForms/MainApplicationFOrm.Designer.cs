
namespace FindingNationalityGame
{
    partial class MainApplicationForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainApplicationForm));
            this.pictureBoxInput = new System.Windows.Forms.PictureBox();
            this.panelThai = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.timerInputImageDrop = new System.Windows.Forms.Timer(this.components);
            this.panelJapan = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panelKorean = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTotalScore = new System.Windows.Forms.Label();
            this.labelDisclaimer = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelChinese = new System.Windows.Forms.Panel();
            this.timerImageAnimate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInput)).BeginInit();
            this.panelThai.SuspendLayout();
            this.panelJapan.SuspendLayout();
            this.panelKorean.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelChinese.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxInput
            // 
            this.pictureBoxInput.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxInput.Image")));
            this.pictureBoxInput.Location = new System.Drawing.Point(337, 4);
            this.pictureBoxInput.Name = "pictureBoxInput";
            this.pictureBoxInput.Size = new System.Drawing.Size(154, 142);
            this.pictureBoxInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxInput.TabIndex = 0;
            this.pictureBoxInput.TabStop = false;
            this.pictureBoxInput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDropInput_MouseDown);
            this.pictureBoxInput.MouseEnter += new System.EventHandler(this.pictureBoxDropInput_MouseEnter);
            this.pictureBoxInput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDropInput_MouseMove);
            this.pictureBoxInput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxInput_MouseUp);
            // 
            // panelThai
            // 
            this.panelThai.AllowDrop = true;
            this.panelThai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelThai.Controls.Add(this.label2);
            this.panelThai.Location = new System.Drawing.Point(552, 278);
            this.panelThai.Name = "panelThai";
            this.panelThai.Size = new System.Drawing.Size(270, 214);
            this.panelThai.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(109, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Thai";
            // 
            // timerInputImageDrop
            // 
            this.timerInputImageDrop.Tick += new System.EventHandler(this.timerInputImageDrop_Tick);
            // 
            // panelJapan
            // 
            this.panelJapan.AllowDrop = true;
            this.panelJapan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelJapan.Controls.Add(this.label3);
            this.panelJapan.Location = new System.Drawing.Point(3, 2);
            this.panelJapan.Name = "panelJapan";
            this.panelJapan.Size = new System.Drawing.Size(270, 214);
            this.panelJapan.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(105, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Japan";
            // 
            // panelKorean
            // 
            this.panelKorean.AllowDrop = true;
            this.panelKorean.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKorean.Controls.Add(this.label4);
            this.panelKorean.Location = new System.Drawing.Point(3, 278);
            this.panelKorean.Name = "panelKorean";
            this.panelKorean.Size = new System.Drawing.Size(270, 214);
            this.panelKorean.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(105, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Korean";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelTotalScore);
            this.panel1.Controls.Add(this.labelDisclaimer);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Location = new System.Drawing.Point(279, 335);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 157);
            this.panel1.TabIndex = 7;
            // 
            // labelTotalScore
            // 
            this.labelTotalScore.AutoSize = true;
            this.labelTotalScore.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTotalScore.Location = new System.Drawing.Point(76, 97);
            this.labelTotalScore.Name = "labelTotalScore";
            this.labelTotalScore.Size = new System.Drawing.Size(98, 20);
            this.labelTotalScore.TabIndex = 2;
            this.labelTotalScore.Text = "Total Score: 0";
            // 
            // labelDisclaimer
            // 
            this.labelDisclaimer.AutoSize = true;
            this.labelDisclaimer.Location = new System.Drawing.Point(10, 12);
            this.labelDisclaimer.Name = "labelDisclaimer";
            this.labelDisclaimer.Size = new System.Drawing.Size(252, 75);
            this.labelDisclaimer.TabIndex = 1;
            this.labelDisclaimer.Text = "Drag and drop droping image to the matching\r\n box bfore times run out to get poin" +
    "ts.\r\nFinding right box would give you + 20 points.\r\nWrong answer would reduct 5 " +
    "points.\r\nGood Luck!";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStart.Location = new System.Drawing.Point(63, 120);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(121, 27);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(105, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chinese";
            // 
            // panelChinese
            // 
            this.panelChinese.AllowDrop = true;
            this.panelChinese.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChinese.Controls.Add(this.label1);
            this.panelChinese.Location = new System.Drawing.Point(552, 2);
            this.panelChinese.Name = "panelChinese";
            this.panelChinese.Size = new System.Drawing.Size(270, 214);
            this.panelChinese.TabIndex = 3;
            // 
            // timerImageAnimate
            // 
            this.timerImageAnimate.Interval = 10;
            this.timerImageAnimate.Tick += new System.EventHandler(this.timerImageAnimate_Tick);
            // 
            // MainApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(824, 494);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelKorean);
            this.Controls.Add(this.panelJapan);
            this.Controls.Add(this.panelThai);
            this.Controls.Add(this.panelChinese);
            this.Controls.Add(this.pictureBoxInput);
            this.MaximizeBox = false;
            this.Name = "MainApplicationForm";
            this.Text = "Finding Nationality Game";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInput)).EndInit();
            this.panelThai.ResumeLayout(false);
            this.panelThai.PerformLayout();
            this.panelJapan.ResumeLayout(false);
            this.panelJapan.PerformLayout();
            this.panelKorean.ResumeLayout(false);
            this.panelKorean.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelChinese.ResumeLayout(false);
            this.panelChinese.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxInput;
        private System.Windows.Forms.Panel panelThai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerInputImageDrop;
        private System.Windows.Forms.Panel panelJapan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelKorean;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panelChinese;
        private System.Windows.Forms.Timer timerImageAnimate;
        private System.Windows.Forms.Label labelTotalScore;
        private System.Windows.Forms.Label labelDisclaimer;
    }
}

