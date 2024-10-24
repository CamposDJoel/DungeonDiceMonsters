namespace DDMPvPServer
{
    partial class Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            label1 = new Label();
            btnStart = new Button();
            btnStop = new Button();
            label2 = new Label();
            listMatches = new ListBox();
            label3 = new Label();
            panel1 = new Panel();
            txtMatchOutput = new Label();
            panel2 = new Panel();
            txtConnectionLog = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(190, 4);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 0;
            label1.Text = "Connection Output:";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 26);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 68);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(104, 26);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 68);
            btnStop.TabIndex = 3;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Visible = false;
            btnStop.Click += btnStop_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 129);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 4;
            label2.Text = "Active Matches:";
            // 
            // listMatches
            // 
            listMatches.FormattingEnabled = true;
            listMatches.ItemHeight = 15;
            listMatches.Location = new Point(8, 148);
            listMatches.Name = "listMatches";
            listMatches.Size = new Size(79, 169);
            listMatches.TabIndex = 5;
            listMatches.SelectedIndexChanged += listMatches_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(106, 129);
            label3.Name = "label3";
            label3.Size = new Size(108, 15);
            label3.TabIndex = 6;
            label3.Text = "Match Log Output:";
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(txtMatchOutput);
            panel1.Location = new Point(104, 145);
            panel1.Name = "panel1";
            panel1.Size = new Size(597, 172);
            panel1.TabIndex = 8;
            // 
            // txtMatchOutput
            // 
            txtMatchOutput.AutoSize = true;
            txtMatchOutput.BackColor = Color.Black;
            txtMatchOutput.ForeColor = Color.White;
            txtMatchOutput.Location = new Point(5, 3);
            txtMatchOutput.Name = "txtMatchOutput";
            txtMatchOutput.Size = new Size(0, 15);
            txtMatchOutput.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(txtConnectionLog);
            panel2.Location = new Point(192, 23);
            panel2.Name = "panel2";
            panel2.Size = new Size(509, 89);
            panel2.TabIndex = 9;
            // 
            // txtConnectionLog
            // 
            txtConnectionLog.AutoSize = true;
            txtConnectionLog.BackColor = Color.Black;
            txtConnectionLog.ForeColor = Color.White;
            txtConnectionLog.Location = new Point(3, 3);
            txtConnectionLog.Name = "txtConnectionLog";
            txtConnectionLog.Size = new Size(0, 15);
            txtConnectionLog.TabIndex = 0;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 327);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(listMatches);
            Controls.Add(label2);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Server";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DDM - Server";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnStart;
        private Button btnStop;
        private Label label2;
        private ListBox listMatches;
        private Label label3;
        private Panel panel1;
        private Label txtMatchOutput;
        private Panel panel2;
        private Label txtConnectionLog;
    }
}