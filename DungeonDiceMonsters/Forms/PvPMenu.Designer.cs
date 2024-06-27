namespace DungeonDiceMonsters
{
    partial class PvPMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PvPMenu));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnFindMatch = new System.Windows.Forms.Button();
            this.lblBluePlayerName = new System.Windows.Forms.Label();
            this.lblRedPlayerName = new System.Windows.Forms.Label();
            this.lblWaitMessage = new System.Windows.Forms.Label();
            this.PicDeckStatus = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listDeckList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelDeckSelection = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PicDeckStatus)).BeginInit();
            this.PanelDeckSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.Location = new System.Drawing.Point(673, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(105, 22);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnFindMatch
            // 
            this.btnFindMatch.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindMatch.ForeColor = System.Drawing.Color.White;
            this.btnFindMatch.Image = ((System.Drawing.Image)(resources.GetObject("btnFindMatch.Image")));
            this.btnFindMatch.Location = new System.Drawing.Point(299, 237);
            this.btnFindMatch.Name = "btnFindMatch";
            this.btnFindMatch.Size = new System.Drawing.Size(197, 63);
            this.btnFindMatch.TabIndex = 15;
            this.btnFindMatch.Text = "Find Match";
            this.btnFindMatch.UseVisualStyleBackColor = true;
            this.btnFindMatch.Click += new System.EventHandler(this.btnFindMatch_Click);
            // 
            // lblBluePlayerName
            // 
            this.lblBluePlayerName.AutoSize = true;
            this.lblBluePlayerName.BackColor = System.Drawing.Color.Transparent;
            this.lblBluePlayerName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBluePlayerName.ForeColor = System.Drawing.Color.White;
            this.lblBluePlayerName.Location = new System.Drawing.Point(116, 402);
            this.lblBluePlayerName.Name = "lblBluePlayerName";
            this.lblBluePlayerName.Size = new System.Drawing.Size(189, 24);
            this.lblBluePlayerName.TabIndex = 17;
            this.lblBluePlayerName.Text = "Blue Player Name";
            this.lblBluePlayerName.Visible = false;
            // 
            // lblRedPlayerName
            // 
            this.lblRedPlayerName.AutoSize = true;
            this.lblRedPlayerName.BackColor = System.Drawing.Color.Transparent;
            this.lblRedPlayerName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPlayerName.ForeColor = System.Drawing.Color.White;
            this.lblRedPlayerName.Location = new System.Drawing.Point(486, 116);
            this.lblRedPlayerName.Name = "lblRedPlayerName";
            this.lblRedPlayerName.Size = new System.Drawing.Size(183, 24);
            this.lblRedPlayerName.TabIndex = 18;
            this.lblRedPlayerName.Text = "Red Player Name";
            this.lblRedPlayerName.Visible = false;
            // 
            // lblWaitMessage
            // 
            this.lblWaitMessage.AutoSize = true;
            this.lblWaitMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblWaitMessage.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaitMessage.ForeColor = System.Drawing.Color.White;
            this.lblWaitMessage.Location = new System.Drawing.Point(223, 303);
            this.lblWaitMessage.Name = "lblWaitMessage";
            this.lblWaitMessage.Size = new System.Drawing.Size(353, 24);
            this.lblWaitMessage.TabIndex = 19;
            this.lblWaitMessage.Text = "Wait for Player 2! - Match Created!";
            this.lblWaitMessage.Visible = false;
            // 
            // PicDeckStatus
            // 
            this.PicDeckStatus.BackColor = System.Drawing.Color.Transparent;
            this.PicDeckStatus.Location = new System.Drawing.Point(122, 236);
            this.PicDeckStatus.Name = "PicDeckStatus";
            this.PicDeckStatus.Size = new System.Drawing.Size(22, 22);
            this.PicDeckStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDeckStatus.TabIndex = 22;
            this.PicDeckStatus.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Ready to use:";
            // 
            // listDeckList
            // 
            this.listDeckList.BackColor = System.Drawing.Color.Black;
            this.listDeckList.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listDeckList.ForeColor = System.Drawing.Color.Yellow;
            this.listDeckList.FormattingEnabled = true;
            this.listDeckList.ItemHeight = 15;
            this.listDeckList.Location = new System.Drawing.Point(6, 28);
            this.listDeckList.Name = "listDeckList";
            this.listDeckList.Size = new System.Drawing.Size(142, 199);
            this.listDeckList.TabIndex = 20;
            this.listDeckList.SelectedIndexChanged += new System.EventHandler(this.listDeckList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(5, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Select Deck:";
            // 
            // PanelDeckSelection
            // 
            this.PanelDeckSelection.BackColor = System.Drawing.Color.Black;
            this.PanelDeckSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDeckSelection.Controls.Add(this.label2);
            this.PanelDeckSelection.Controls.Add(this.listDeckList);
            this.PanelDeckSelection.Controls.Add(this.PicDeckStatus);
            this.PanelDeckSelection.Controls.Add(this.label1);
            this.PanelDeckSelection.Location = new System.Drawing.Point(12, 12);
            this.PanelDeckSelection.Name = "PanelDeckSelection";
            this.PanelDeckSelection.Size = new System.Drawing.Size(158, 270);
            this.PanelDeckSelection.TabIndex = 24;
            // 
            // PvPMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.PanelDeckSelection);
            this.Controls.Add(this.lblWaitMessage);
            this.Controls.Add(this.lblRedPlayerName);
            this.Controls.Add(this.lblBluePlayerName);
            this.Controls.Add(this.btnFindMatch);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PvPMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DDM - PvP Menu";
            ((System.ComponentModel.ISupportInitialize)(this.PicDeckStatus)).EndInit();
            this.PanelDeckSelection.ResumeLayout(false);
            this.PanelDeckSelection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnFindMatch;
        private System.Windows.Forms.Label lblBluePlayerName;
        private System.Windows.Forms.Label lblRedPlayerName;
        private System.Windows.Forms.Label lblWaitMessage;
        private System.Windows.Forms.PictureBox PicDeckStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listDeckList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PanelDeckSelection;
    }
}