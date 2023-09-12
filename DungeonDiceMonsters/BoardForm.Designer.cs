namespace DungeonDiceMonsters
{
    partial class BoardForm
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
            this.PanelBoard = new System.Windows.Forms.Panel();
            this.PanelBluePlayer = new System.Windows.Forms.Panel();
            this.lblBlueLP = new System.Windows.Forms.Label();
            this.lblLPLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblRedLP = new System.Windows.Forms.Label();
            this.lblLPlabel2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelDebug = new System.Windows.Forms.Panel();
            this.lblDebugCardOwner = new System.Windows.Forms.Label();
            this.lblDebugCard = new System.Windows.Forms.Label();
            this.lblDebugIsOccupied = new System.Windows.Forms.Label();
            this.lblDebugOwner = new System.Windows.Forms.Label();
            this.lblDebugWestAdj = new System.Windows.Forms.Label();
            this.lblDebugEastAdj = new System.Windows.Forms.Label();
            this.lblDebugSouthAdj = new System.Windows.Forms.Label();
            this.lblDebugNorthAdj = new System.Windows.Forms.Label();
            this.lblDebugTileID = new System.Windows.Forms.Label();
            this.PanelCardInfo = new System.Windows.Forms.Panel();
            this.lblCardText = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblAttribute = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.lblCardLevel = new System.Windows.Forms.Label();
            this.lblCardName = new System.Windows.Forms.Label();
            this.PicCardArtwork = new System.Windows.Forms.PictureBox();
            this.lblMouseCords = new System.Windows.Forms.Label();
            this.PanelBluePlayer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PanelDebug.SuspendLayout();
            this.PanelCardInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicCardArtwork)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelBoard
            // 
            this.PanelBoard.AutoScroll = true;
            this.PanelBoard.BackColor = System.Drawing.Color.Black;
            this.PanelBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelBoard.Location = new System.Drawing.Point(151, 0);
            this.PanelBoard.Name = "PanelBoard";
            this.PanelBoard.Size = new System.Drawing.Size(633, 561);
            this.PanelBoard.TabIndex = 0;
            // 
            // PanelBluePlayer
            // 
            this.PanelBluePlayer.BackColor = System.Drawing.Color.MidnightBlue;
            this.PanelBluePlayer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelBluePlayer.Controls.Add(this.lblBlueLP);
            this.PanelBluePlayer.Controls.Add(this.lblLPLabel);
            this.PanelBluePlayer.Controls.Add(this.label1);
            this.PanelBluePlayer.Location = new System.Drawing.Point(3, 2);
            this.PanelBluePlayer.Name = "PanelBluePlayer";
            this.PanelBluePlayer.Size = new System.Drawing.Size(144, 100);
            this.PanelBluePlayer.TabIndex = 1;
            // 
            // lblBlueLP
            // 
            this.lblBlueLP.AutoSize = true;
            this.lblBlueLP.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlueLP.ForeColor = System.Drawing.Color.White;
            this.lblBlueLP.Location = new System.Drawing.Point(49, 24);
            this.lblBlueLP.Name = "lblBlueLP";
            this.lblBlueLP.Size = new System.Drawing.Size(54, 22);
            this.lblBlueLP.TabIndex = 2;
            this.lblBlueLP.Text = "8000";
            // 
            // lblLPLabel
            // 
            this.lblLPLabel.AutoSize = true;
            this.lblLPLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLPLabel.ForeColor = System.Drawing.Color.White;
            this.lblLPLabel.Location = new System.Drawing.Point(3, 24);
            this.lblLPLabel.Name = "lblLPLabel";
            this.lblLPLabel.Size = new System.Drawing.Size(40, 22);
            this.lblLPLabel.TabIndex = 1;
            this.lblLPLabel.Text = "LP:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "BLUE";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkRed;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lblRedLP);
            this.panel2.Controls.Add(this.lblLPlabel2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(4, 459);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(144, 100);
            this.panel2.TabIndex = 2;
            // 
            // lblRedLP
            // 
            this.lblRedLP.AutoSize = true;
            this.lblRedLP.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedLP.ForeColor = System.Drawing.Color.White;
            this.lblRedLP.Location = new System.Drawing.Point(48, 24);
            this.lblRedLP.Name = "lblRedLP";
            this.lblRedLP.Size = new System.Drawing.Size(54, 22);
            this.lblRedLP.TabIndex = 4;
            this.lblRedLP.Text = "8000";
            // 
            // lblLPlabel2
            // 
            this.lblLPlabel2.AutoSize = true;
            this.lblLPlabel2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLPlabel2.ForeColor = System.Drawing.Color.White;
            this.lblLPlabel2.Location = new System.Drawing.Point(2, 24);
            this.lblLPlabel2.Name = "lblLPlabel2";
            this.lblLPlabel2.Size = new System.Drawing.Size(40, 22);
            this.lblLPlabel2.TabIndex = 3;
            this.lblLPlabel2.Text = "LP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "RED";
            // 
            // PanelDebug
            // 
            this.PanelDebug.BackColor = System.Drawing.Color.Black;
            this.PanelDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDebug.Controls.Add(this.lblMouseCords);
            this.PanelDebug.Controls.Add(this.lblDebugCardOwner);
            this.PanelDebug.Controls.Add(this.lblDebugCard);
            this.PanelDebug.Controls.Add(this.lblDebugIsOccupied);
            this.PanelDebug.Controls.Add(this.lblDebugOwner);
            this.PanelDebug.Controls.Add(this.lblDebugWestAdj);
            this.PanelDebug.Controls.Add(this.lblDebugEastAdj);
            this.PanelDebug.Controls.Add(this.lblDebugSouthAdj);
            this.PanelDebug.Controls.Add(this.lblDebugNorthAdj);
            this.PanelDebug.Controls.Add(this.lblDebugTileID);
            this.PanelDebug.Location = new System.Drawing.Point(807, 20);
            this.PanelDebug.Name = "PanelDebug";
            this.PanelDebug.Size = new System.Drawing.Size(139, 205);
            this.PanelDebug.TabIndex = 3;
            // 
            // lblDebugCardOwner
            // 
            this.lblDebugCardOwner.AutoSize = true;
            this.lblDebugCardOwner.ForeColor = System.Drawing.Color.White;
            this.lblDebugCardOwner.Location = new System.Drawing.Point(7, 145);
            this.lblDebugCardOwner.Name = "lblDebugCardOwner";
            this.lblDebugCardOwner.Size = new System.Drawing.Size(32, 13);
            this.lblDebugCardOwner.TabIndex = 8;
            this.lblDebugCardOwner.Text = "Card:";
            // 
            // lblDebugCard
            // 
            this.lblDebugCard.AutoSize = true;
            this.lblDebugCard.ForeColor = System.Drawing.Color.White;
            this.lblDebugCard.Location = new System.Drawing.Point(8, 130);
            this.lblDebugCard.Name = "lblDebugCard";
            this.lblDebugCard.Size = new System.Drawing.Size(32, 13);
            this.lblDebugCard.TabIndex = 7;
            this.lblDebugCard.Text = "Card:";
            // 
            // lblDebugIsOccupied
            // 
            this.lblDebugIsOccupied.AutoSize = true;
            this.lblDebugIsOccupied.ForeColor = System.Drawing.Color.White;
            this.lblDebugIsOccupied.Location = new System.Drawing.Point(8, 117);
            this.lblDebugIsOccupied.Name = "lblDebugIsOccupied";
            this.lblDebugIsOccupied.Size = new System.Drawing.Size(56, 13);
            this.lblDebugIsOccupied.TabIndex = 6;
            this.lblDebugIsOccupied.Text = "Occupied:";
            // 
            // lblDebugOwner
            // 
            this.lblDebugOwner.AutoSize = true;
            this.lblDebugOwner.ForeColor = System.Drawing.Color.White;
            this.lblDebugOwner.Location = new System.Drawing.Point(9, 102);
            this.lblDebugOwner.Name = "lblDebugOwner";
            this.lblDebugOwner.Size = new System.Drawing.Size(41, 13);
            this.lblDebugOwner.TabIndex = 5;
            this.lblDebugOwner.Text = "Owner:";
            // 
            // lblDebugWestAdj
            // 
            this.lblDebugWestAdj.AutoSize = true;
            this.lblDebugWestAdj.ForeColor = System.Drawing.Color.White;
            this.lblDebugWestAdj.Location = new System.Drawing.Point(8, 80);
            this.lblDebugWestAdj.Name = "lblDebugWestAdj";
            this.lblDebugWestAdj.Size = new System.Drawing.Size(69, 13);
            this.lblDebugWestAdj.TabIndex = 4;
            this.lblDebugWestAdj.Text = "West Tile ID:";
            // 
            // lblDebugEastAdj
            // 
            this.lblDebugEastAdj.AutoSize = true;
            this.lblDebugEastAdj.ForeColor = System.Drawing.Color.White;
            this.lblDebugEastAdj.Location = new System.Drawing.Point(8, 63);
            this.lblDebugEastAdj.Name = "lblDebugEastAdj";
            this.lblDebugEastAdj.Size = new System.Drawing.Size(65, 13);
            this.lblDebugEastAdj.TabIndex = 3;
            this.lblDebugEastAdj.Text = "East Tile ID:";
            // 
            // lblDebugSouthAdj
            // 
            this.lblDebugSouthAdj.AutoSize = true;
            this.lblDebugSouthAdj.ForeColor = System.Drawing.Color.White;
            this.lblDebugSouthAdj.Location = new System.Drawing.Point(8, 46);
            this.lblDebugSouthAdj.Name = "lblDebugSouthAdj";
            this.lblDebugSouthAdj.Size = new System.Drawing.Size(72, 13);
            this.lblDebugSouthAdj.TabIndex = 2;
            this.lblDebugSouthAdj.Text = "South Tile ID:";
            // 
            // lblDebugNorthAdj
            // 
            this.lblDebugNorthAdj.AutoSize = true;
            this.lblDebugNorthAdj.ForeColor = System.Drawing.Color.White;
            this.lblDebugNorthAdj.Location = new System.Drawing.Point(8, 28);
            this.lblDebugNorthAdj.Name = "lblDebugNorthAdj";
            this.lblDebugNorthAdj.Size = new System.Drawing.Size(70, 13);
            this.lblDebugNorthAdj.TabIndex = 1;
            this.lblDebugNorthAdj.Text = "North Tile ID:";
            // 
            // lblDebugTileID
            // 
            this.lblDebugTileID.AutoSize = true;
            this.lblDebugTileID.ForeColor = System.Drawing.Color.White;
            this.lblDebugTileID.Location = new System.Drawing.Point(8, 7);
            this.lblDebugTileID.Name = "lblDebugTileID";
            this.lblDebugTileID.Size = new System.Drawing.Size(41, 13);
            this.lblDebugTileID.TabIndex = 0;
            this.lblDebugTileID.Text = "Tile ID:";
            // 
            // PanelCardInfo
            // 
            this.PanelCardInfo.BackColor = System.Drawing.Color.DarkRed;
            this.PanelCardInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelCardInfo.Controls.Add(this.lblCardText);
            this.PanelCardInfo.Controls.Add(this.lblStats);
            this.PanelCardInfo.Controls.Add(this.lblAttribute);
            this.PanelCardInfo.Controls.Add(this.lblCardType);
            this.PanelCardInfo.Controls.Add(this.lblCardLevel);
            this.PanelCardInfo.Controls.Add(this.lblCardName);
            this.PanelCardInfo.Controls.Add(this.PicCardArtwork);
            this.PanelCardInfo.Location = new System.Drawing.Point(790, 324);
            this.PanelCardInfo.Name = "PanelCardInfo";
            this.PanelCardInfo.Size = new System.Drawing.Size(214, 237);
            this.PanelCardInfo.TabIndex = 4;
            this.PanelCardInfo.Visible = false;
            // 
            // lblCardText
            // 
            this.lblCardText.BackColor = System.Drawing.Color.Black;
            this.lblCardText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardText.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardText.ForeColor = System.Drawing.Color.White;
            this.lblCardText.Location = new System.Drawing.Point(3, 88);
            this.lblCardText.Name = "lblCardText";
            this.lblCardText.Size = new System.Drawing.Size(205, 145);
            this.lblCardText.TabIndex = 12;
            this.lblCardText.Text = "Card Text";
            // 
            // lblStats
            // 
            this.lblStats.BackColor = System.Drawing.Color.Black;
            this.lblStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStats.ForeColor = System.Drawing.Color.White;
            this.lblStats.Location = new System.Drawing.Point(3, 72);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(205, 15);
            this.lblStats.TabIndex = 16;
            this.lblStats.Text = "Stats";
            // 
            // lblAttribute
            // 
            this.lblAttribute.BackColor = System.Drawing.Color.Black;
            this.lblAttribute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAttribute.ForeColor = System.Drawing.Color.White;
            this.lblAttribute.Location = new System.Drawing.Point(159, 37);
            this.lblAttribute.Name = "lblAttribute";
            this.lblAttribute.Size = new System.Drawing.Size(48, 15);
            this.lblAttribute.TabIndex = 15;
            this.lblAttribute.Text = "Attribute";
            // 
            // lblCardType
            // 
            this.lblCardType.BackColor = System.Drawing.Color.Black;
            this.lblCardType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardType.ForeColor = System.Drawing.Color.White;
            this.lblCardType.Location = new System.Drawing.Point(80, 53);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(127, 15);
            this.lblCardType.TabIndex = 4;
            this.lblCardType.Text = "Type";
            // 
            // lblCardLevel
            // 
            this.lblCardLevel.BackColor = System.Drawing.Color.Black;
            this.lblCardLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardLevel.ForeColor = System.Drawing.Color.White;
            this.lblCardLevel.Location = new System.Drawing.Point(80, 37);
            this.lblCardLevel.Name = "lblCardLevel";
            this.lblCardLevel.Size = new System.Drawing.Size(35, 15);
            this.lblCardLevel.TabIndex = 3;
            this.lblCardLevel.Text = "Level";
            // 
            // lblCardName
            // 
            this.lblCardName.BackColor = System.Drawing.Color.Black;
            this.lblCardName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardName.ForeColor = System.Drawing.Color.White;
            this.lblCardName.Location = new System.Drawing.Point(80, 2);
            this.lblCardName.Name = "lblCardName";
            this.lblCardName.Size = new System.Drawing.Size(128, 34);
            this.lblCardName.TabIndex = 2;
            this.lblCardName.Text = "Card Name";
            // 
            // PicCardArtwork
            // 
            this.PicCardArtwork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicCardArtwork.Location = new System.Drawing.Point(3, 2);
            this.PicCardArtwork.Name = "PicCardArtwork";
            this.PicCardArtwork.Size = new System.Drawing.Size(68, 68);
            this.PicCardArtwork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicCardArtwork.TabIndex = 1;
            this.PicCardArtwork.TabStop = false;
            // 
            // lblMouseCords
            // 
            this.lblMouseCords.AutoSize = true;
            this.lblMouseCords.ForeColor = System.Drawing.Color.White;
            this.lblMouseCords.Location = new System.Drawing.Point(9, 167);
            this.lblMouseCords.Name = "lblMouseCords";
            this.lblMouseCords.Size = new System.Drawing.Size(32, 13);
            this.lblMouseCords.TabIndex = 9;
            this.lblMouseCords.Text = "Card:";
            // 
            // BoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.PanelCardInfo);
            this.Controls.Add(this.PanelDebug);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PanelBluePlayer);
            this.Controls.Add(this.PanelBoard);
            this.Name = "BoardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDM - Board";
            this.PanelBluePlayer.ResumeLayout(false);
            this.PanelBluePlayer.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.PanelDebug.ResumeLayout(false);
            this.PanelDebug.PerformLayout();
            this.PanelCardInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicCardArtwork)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelBoard;
        private System.Windows.Forms.Panel PanelBluePlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBlueLP;
        private System.Windows.Forms.Label lblLPLabel;
        private System.Windows.Forms.Label lblRedLP;
        private System.Windows.Forms.Label lblLPlabel2;
        private System.Windows.Forms.Panel PanelDebug;
        private System.Windows.Forms.Label lblDebugWestAdj;
        private System.Windows.Forms.Label lblDebugEastAdj;
        private System.Windows.Forms.Label lblDebugSouthAdj;
        private System.Windows.Forms.Label lblDebugNorthAdj;
        private System.Windows.Forms.Label lblDebugTileID;
        private System.Windows.Forms.Label lblDebugOwner;
        private System.Windows.Forms.Label lblDebugIsOccupied;
        private System.Windows.Forms.Label lblDebugCard;
        private System.Windows.Forms.Label lblDebugCardOwner;
        private System.Windows.Forms.Panel PanelCardInfo;
        private System.Windows.Forms.PictureBox PicCardArtwork;
        private System.Windows.Forms.Label lblCardName;
        private System.Windows.Forms.Label lblCardLevel;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label lblAttribute;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblCardText;
        private System.Windows.Forms.Label lblMouseCords;
    }
}