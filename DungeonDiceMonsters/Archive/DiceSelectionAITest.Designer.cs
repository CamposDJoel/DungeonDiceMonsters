namespace DungeonDiceMonsters
{
    partial class DiceSelectionAITest
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
            this.lblDiceLevelSelector = new System.Windows.Forms.Label();
            this.Pic1 = new System.Windows.Forms.PictureBox();
            this.Pic2 = new System.Windows.Forms.PictureBox();
            this.Pic3 = new System.Windows.Forms.PictureBox();
            this.btnRunTest = new System.Windows.Forms.Button();
            this.lblOutCome = new System.Windows.Forms.Label();
            this.lblSelectionCount = new System.Windows.Forms.Label();
            this.btntestrand = new System.Windows.Forms.Button();
            this.lblRandResults = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Pic1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDiceLevelSelector
            // 
            this.lblDiceLevelSelector.Location = new System.Drawing.Point(429, 9);
            this.lblDiceLevelSelector.Name = "lblDiceLevelSelector";
            this.lblDiceLevelSelector.Size = new System.Drawing.Size(230, 23);
            this.lblDiceLevelSelector.TabIndex = 0;
            this.lblDiceLevelSelector.Text = "Dice Level:";
            // 
            // Pic1
            // 
            this.Pic1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Pic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pic1.Location = new System.Drawing.Point(39, 288);
            this.Pic1.Name = "Pic1";
            this.Pic1.Size = new System.Drawing.Size(84, 115);
            this.Pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic1.TabIndex = 1;
            this.Pic1.TabStop = false;
            // 
            // Pic2
            // 
            this.Pic2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Pic2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pic2.Location = new System.Drawing.Point(162, 288);
            this.Pic2.Name = "Pic2";
            this.Pic2.Size = new System.Drawing.Size(84, 115);
            this.Pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic2.TabIndex = 2;
            this.Pic2.TabStop = false;
            // 
            // Pic3
            // 
            this.Pic3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Pic3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pic3.Location = new System.Drawing.Point(287, 288);
            this.Pic3.Name = "Pic3";
            this.Pic3.Size = new System.Drawing.Size(84, 115);
            this.Pic3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic3.TabIndex = 3;
            this.Pic3.TabStop = false;
            // 
            // btnRunTest
            // 
            this.btnRunTest.Location = new System.Drawing.Point(201, 97);
            this.btnRunTest.Name = "btnRunTest";
            this.btnRunTest.Size = new System.Drawing.Size(177, 74);
            this.btnRunTest.TabIndex = 4;
            this.btnRunTest.Text = "test function";
            this.btnRunTest.UseVisualStyleBackColor = true;
            this.btnRunTest.Click += new System.EventHandler(this.btnRunTest_Click);
            // 
            // lblOutCome
            // 
            this.lblOutCome.Location = new System.Drawing.Point(429, 47);
            this.lblOutCome.Name = "lblOutCome";
            this.lblOutCome.Size = new System.Drawing.Size(342, 165);
            this.lblOutCome.TabIndex = 5;
            this.lblOutCome.Text = "Outcome:";
            // 
            // lblSelectionCount
            // 
            this.lblSelectionCount.Location = new System.Drawing.Point(429, 298);
            this.lblSelectionCount.Name = "lblSelectionCount";
            this.lblSelectionCount.Size = new System.Drawing.Size(230, 23);
            this.lblSelectionCount.TabIndex = 6;
            this.lblSelectionCount.Text = "Selection Count:";
            // 
            // btntestrand
            // 
            this.btntestrand.Location = new System.Drawing.Point(16, 14);
            this.btntestrand.Name = "btntestrand";
            this.btntestrand.Size = new System.Drawing.Size(126, 64);
            this.btntestrand.TabIndex = 7;
            this.btntestrand.Text = "testRandRange";
            this.btntestrand.UseVisualStyleBackColor = true;
            this.btntestrand.Click += new System.EventHandler(this.btntestrand_Click);
            // 
            // lblRandResults
            // 
            this.lblRandResults.Location = new System.Drawing.Point(8, 90);
            this.lblRandResults.Name = "lblRandResults";
            this.lblRandResults.Size = new System.Drawing.Size(154, 180);
            this.lblRandResults.TabIndex = 8;
            // 
            // DiceSelectionAITest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblRandResults);
            this.Controls.Add(this.btntestrand);
            this.Controls.Add(this.lblSelectionCount);
            this.Controls.Add(this.lblOutCome);
            this.Controls.Add(this.btnRunTest);
            this.Controls.Add(this.Pic3);
            this.Controls.Add(this.Pic2);
            this.Controls.Add(this.Pic1);
            this.Controls.Add(this.lblDiceLevelSelector);
            this.Name = "DiceSelectionAITest";
            this.Text = "DiceSelectionAITest";
            ((System.ComponentModel.ISupportInitialize)(this.Pic1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDiceLevelSelector;
        private System.Windows.Forms.PictureBox Pic1;
        private System.Windows.Forms.PictureBox Pic2;
        private System.Windows.Forms.PictureBox Pic3;
        private System.Windows.Forms.Button btnRunTest;
        private System.Windows.Forms.Label lblOutCome;
        private System.Windows.Forms.Label lblSelectionCount;
        private System.Windows.Forms.Button btntestrand;
        private System.Windows.Forms.Label lblRandResults;
    }
}