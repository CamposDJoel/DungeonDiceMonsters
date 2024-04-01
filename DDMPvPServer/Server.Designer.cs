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
            label1 = new Label();
            txtConnectionLog = new TextBox();
            btnStart = new Button();
            btnStop = new Button();
            label2 = new Label();
            listMatches = new ListBox();
            label3 = new Label();
            txtMatchOutput = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(200, 9);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 0;
            label1.Text = "Connection Output:";
            // 
            // txtConnectionLog
            // 
            txtConnectionLog.Location = new Point(203, 25);
            txtConnectionLog.Multiline = true;
            txtConnectionLog.Name = "txtConnectionLog";
            txtConnectionLog.Size = new Size(180, 72);
            txtConnectionLog.TabIndex = 1;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 36);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 50);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(104, 36);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 50);
            btnStop.TabIndex = 3;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Visible = false;
            btnStop.Click += btnStop_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 115);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 4;
            label2.Text = "Active Matches:";
            // 
            // listMatches
            // 
            listMatches.FormattingEnabled = true;
            listMatches.ItemHeight = 15;
            listMatches.Location = new Point(8, 133);
            listMatches.Name = "listMatches";
            listMatches.Size = new Size(112, 184);
            listMatches.TabIndex = 5;
            listMatches.SelectedIndexChanged += listMatches_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(144, 115);
            label3.Name = "label3";
            label3.Size = new Size(108, 15);
            label3.TabIndex = 6;
            label3.Text = "Match Log Output:";
            // 
            // txtMatchOutput
            // 
            txtMatchOutput.Location = new Point(144, 134);
            txtMatchOutput.Multiline = true;
            txtMatchOutput.Name = "txtMatchOutput";
            txtMatchOutput.Size = new Size(259, 183);
            txtMatchOutput.TabIndex = 7;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(418, 327);
            Controls.Add(txtMatchOutput);
            Controls.Add(label3);
            Controls.Add(listMatches);
            Controls.Add(label2);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(txtConnectionLog);
            Controls.Add(label1);
            Name = "Server";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtConnectionLog;
        private Button btnStart;
        private Button btnStop;
        private Label label2;
        private ListBox listMatches;
        private Label label3;
        private TextBox txtMatchOutput;
    }
}