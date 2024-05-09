namespace DungeonDiceMonsters
{
    partial class DeckBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeckBuilder));
            this.PanelDeck = new System.Windows.Forms.Panel();
            this.btnSymbolNext = new System.Windows.Forms.Button();
            this.btnSymbolPrevious = new System.Windows.Forms.Button();
            this.PicSymbol = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFusion = new System.Windows.Forms.Label();
            this.lblDeckName = new System.Windows.Forms.Label();
            this.lblStorage = new System.Windows.Forms.Label();
            this.PanelStorage = new System.Windows.Forms.Panel();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.GroupDiceInfo = new System.Windows.Forms.GroupBox();
            this.PanelCardText = new System.Windows.Forms.Panel();
            this.lblCardText = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblAttribute = new System.Windows.Forms.Label();
            this.PicDiceFace6 = new System.Windows.Forms.PictureBox();
            this.PicDiceFace5 = new System.Windows.Forms.PictureBox();
            this.PicDiceFace4 = new System.Windows.Forms.PictureBox();
            this.PicDiceFace3 = new System.Windows.Forms.PictureBox();
            this.PicDiceFace2 = new System.Windows.Forms.PictureBox();
            this.PicDiceFace1 = new System.Windows.Forms.PictureBox();
            this.lblDiceLevel = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.lblCardLevel = new System.Windows.Forms.Label();
            this.lblCardName = new System.Windows.Forms.Label();
            this.PicCardArtwork = new System.Windows.Forms.PictureBox();
            this.listDeckList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PicDeckStatus = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblSaveDeckOutput = new System.Windows.Forms.Label();
            this.PanelDeck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicSymbol)).BeginInit();
            this.GroupDiceInfo.SuspendLayout();
            this.PanelCardText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicCardArtwork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDeckStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelDeck
            // 
            this.PanelDeck.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PanelDeck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDeck.Controls.Add(this.btnSymbolNext);
            this.PanelDeck.Controls.Add(this.btnSymbolPrevious);
            this.PanelDeck.Controls.Add(this.PicSymbol);
            this.PanelDeck.Controls.Add(this.label2);
            this.PanelDeck.Controls.Add(this.lblFusion);
            this.PanelDeck.Location = new System.Drawing.Point(10, 36);
            this.PanelDeck.Name = "PanelDeck";
            this.PanelDeck.Size = new System.Drawing.Size(299, 395);
            this.PanelDeck.TabIndex = 0;
            // 
            // btnSymbolNext
            // 
            this.btnSymbolNext.BackColor = System.Drawing.Color.Black;
            this.btnSymbolNext.ForeColor = System.Drawing.Color.White;
            this.btnSymbolNext.Location = new System.Drawing.Point(276, 337);
            this.btnSymbolNext.Name = "btnSymbolNext";
            this.btnSymbolNext.Size = new System.Drawing.Size(20, 20);
            this.btnSymbolNext.TabIndex = 4;
            this.btnSymbolNext.Text = ">";
            this.btnSymbolNext.UseVisualStyleBackColor = false;
            this.btnSymbolNext.Click += new System.EventHandler(this.btnSymbolNext_Click);
            // 
            // btnSymbolPrevious
            // 
            this.btnSymbolPrevious.BackColor = System.Drawing.Color.Black;
            this.btnSymbolPrevious.ForeColor = System.Drawing.Color.White;
            this.btnSymbolPrevious.Location = new System.Drawing.Point(179, 337);
            this.btnSymbolPrevious.Name = "btnSymbolPrevious";
            this.btnSymbolPrevious.Size = new System.Drawing.Size(20, 20);
            this.btnSymbolPrevious.TabIndex = 3;
            this.btnSymbolPrevious.Text = "<";
            this.btnSymbolPrevious.UseVisualStyleBackColor = false;
            this.btnSymbolPrevious.Click += new System.EventHandler(this.btnSymbolPrevious_Click);
            // 
            // PicSymbol
            // 
            this.PicSymbol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicSymbol.Image = ((System.Drawing.Image)(resources.GetObject("PicSymbol.Image")));
            this.PicSymbol.Location = new System.Drawing.Point(200, 313);
            this.PicSymbol.Name = "PicSymbol";
            this.PicSymbol.Size = new System.Drawing.Size(75, 75);
            this.PicSymbol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicSymbol.TabIndex = 2;
            this.PicSymbol.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(197, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Symbol:";
            // 
            // lblFusion
            // 
            this.lblFusion.AutoSize = true;
            this.lblFusion.BackColor = System.Drawing.Color.Transparent;
            this.lblFusion.ForeColor = System.Drawing.Color.Transparent;
            this.lblFusion.Location = new System.Drawing.Point(4, 297);
            this.lblFusion.Name = "lblFusion";
            this.lblFusion.Size = new System.Drawing.Size(71, 13);
            this.lblFusion.TabIndex = 0;
            this.lblFusion.Text = "Fusion Cards:";
            // 
            // lblDeckName
            // 
            this.lblDeckName.BackColor = System.Drawing.Color.Transparent;
            this.lblDeckName.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeckName.ForeColor = System.Drawing.Color.White;
            this.lblDeckName.Location = new System.Drawing.Point(10, 1);
            this.lblDeckName.Name = "lblDeckName";
            this.lblDeckName.Size = new System.Drawing.Size(215, 28);
            this.lblDeckName.TabIndex = 1;
            this.lblDeckName.Text = "My Deck:";
            // 
            // lblStorage
            // 
            this.lblStorage.BackColor = System.Drawing.Color.Transparent;
            this.lblStorage.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStorage.ForeColor = System.Drawing.Color.White;
            this.lblStorage.Location = new System.Drawing.Point(419, 1);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(215, 28);
            this.lblStorage.TabIndex = 3;
            this.lblStorage.Text = "Storage Page: 1";
            // 
            // PanelStorage
            // 
            this.PanelStorage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PanelStorage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelStorage.Location = new System.Drawing.Point(419, 37);
            this.PanelStorage.Name = "PanelStorage";
            this.PanelStorage.Size = new System.Drawing.Size(354, 374);
            this.PanelStorage.TabIndex = 2;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.Location = new System.Drawing.Point(421, 412);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(88, 25);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(685, 413);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(88, 25);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // GroupDiceInfo
            // 
            this.GroupDiceInfo.BackColor = System.Drawing.Color.Black;
            this.GroupDiceInfo.Controls.Add(this.PanelCardText);
            this.GroupDiceInfo.Controls.Add(this.lblID);
            this.GroupDiceInfo.Controls.Add(this.lblAttribute);
            this.GroupDiceInfo.Controls.Add(this.PicDiceFace6);
            this.GroupDiceInfo.Controls.Add(this.PicDiceFace5);
            this.GroupDiceInfo.Controls.Add(this.PicDiceFace4);
            this.GroupDiceInfo.Controls.Add(this.PicDiceFace3);
            this.GroupDiceInfo.Controls.Add(this.PicDiceFace2);
            this.GroupDiceInfo.Controls.Add(this.PicDiceFace1);
            this.GroupDiceInfo.Controls.Add(this.lblDiceLevel);
            this.GroupDiceInfo.Controls.Add(this.lblStats);
            this.GroupDiceInfo.Controls.Add(this.lblCardType);
            this.GroupDiceInfo.Controls.Add(this.lblCardLevel);
            this.GroupDiceInfo.Controls.Add(this.lblCardName);
            this.GroupDiceInfo.Controls.Add(this.PicCardArtwork);
            this.GroupDiceInfo.ForeColor = System.Drawing.Color.White;
            this.GroupDiceInfo.Location = new System.Drawing.Point(10, 437);
            this.GroupDiceInfo.Name = "GroupDiceInfo";
            this.GroupDiceInfo.Size = new System.Drawing.Size(762, 119);
            this.GroupDiceInfo.TabIndex = 8;
            this.GroupDiceInfo.TabStop = false;
            this.GroupDiceInfo.Text = "Card Info";
            // 
            // PanelCardText
            // 
            this.PanelCardText.AutoScroll = true;
            this.PanelCardText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelCardText.Controls.Add(this.lblCardText);
            this.PanelCardText.Location = new System.Drawing.Point(317, 14);
            this.PanelCardText.Name = "PanelCardText";
            this.PanelCardText.Size = new System.Drawing.Size(281, 96);
            this.PanelCardText.TabIndex = 16;
            // 
            // lblCardText
            // 
            this.lblCardText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardText.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardText.Location = new System.Drawing.Point(-1, -1);
            this.lblCardText.Name = "lblCardText";
            this.lblCardText.Size = new System.Drawing.Size(259, 160);
            this.lblCardText.TabIndex = 12;
            this.lblCardText.Text = "Card Text";
            // 
            // lblID
            // 
            this.lblID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblID.Location = new System.Drawing.Point(113, 34);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(112, 15);
            this.lblID.TabIndex = 15;
            this.lblID.Text = "ID";
            // 
            // lblAttribute
            // 
            this.lblAttribute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAttribute.Location = new System.Drawing.Point(231, 55);
            this.lblAttribute.Name = "lblAttribute";
            this.lblAttribute.Size = new System.Drawing.Size(63, 15);
            this.lblAttribute.TabIndex = 14;
            this.lblAttribute.Text = "Attribute";
            // 
            // PicDiceFace6
            // 
            this.PicDiceFace6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicDiceFace6.Location = new System.Drawing.Point(707, 63);
            this.PicDiceFace6.Name = "PicDiceFace6";
            this.PicDiceFace6.Size = new System.Drawing.Size(50, 50);
            this.PicDiceFace6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDiceFace6.TabIndex = 11;
            this.PicDiceFace6.TabStop = false;
            // 
            // PicDiceFace5
            // 
            this.PicDiceFace5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicDiceFace5.Location = new System.Drawing.Point(654, 63);
            this.PicDiceFace5.Name = "PicDiceFace5";
            this.PicDiceFace5.Size = new System.Drawing.Size(50, 50);
            this.PicDiceFace5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDiceFace5.TabIndex = 10;
            this.PicDiceFace5.TabStop = false;
            // 
            // PicDiceFace4
            // 
            this.PicDiceFace4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicDiceFace4.Location = new System.Drawing.Point(602, 63);
            this.PicDiceFace4.Name = "PicDiceFace4";
            this.PicDiceFace4.Size = new System.Drawing.Size(50, 50);
            this.PicDiceFace4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDiceFace4.TabIndex = 9;
            this.PicDiceFace4.TabStop = false;
            // 
            // PicDiceFace3
            // 
            this.PicDiceFace3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicDiceFace3.Location = new System.Drawing.Point(707, 10);
            this.PicDiceFace3.Name = "PicDiceFace3";
            this.PicDiceFace3.Size = new System.Drawing.Size(50, 50);
            this.PicDiceFace3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDiceFace3.TabIndex = 8;
            this.PicDiceFace3.TabStop = false;
            // 
            // PicDiceFace2
            // 
            this.PicDiceFace2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicDiceFace2.Location = new System.Drawing.Point(654, 10);
            this.PicDiceFace2.Name = "PicDiceFace2";
            this.PicDiceFace2.Size = new System.Drawing.Size(50, 50);
            this.PicDiceFace2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDiceFace2.TabIndex = 7;
            this.PicDiceFace2.TabStop = false;
            // 
            // PicDiceFace1
            // 
            this.PicDiceFace1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicDiceFace1.Location = new System.Drawing.Point(602, 10);
            this.PicDiceFace1.Name = "PicDiceFace1";
            this.PicDiceFace1.Size = new System.Drawing.Size(50, 50);
            this.PicDiceFace1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDiceFace1.TabIndex = 6;
            this.PicDiceFace1.TabStop = false;
            // 
            // lblDiceLevel
            // 
            this.lblDiceLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDiceLevel.Location = new System.Drawing.Point(179, 75);
            this.lblDiceLevel.Name = "lblDiceLevel";
            this.lblDiceLevel.Size = new System.Drawing.Size(63, 15);
            this.lblDiceLevel.TabIndex = 5;
            this.lblDiceLevel.Text = "Dice Level";
            // 
            // lblStats
            // 
            this.lblStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStats.Location = new System.Drawing.Point(113, 97);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(189, 15);
            this.lblStats.TabIndex = 4;
            this.lblStats.Text = "Stats";
            // 
            // lblCardType
            // 
            this.lblCardType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardType.Location = new System.Drawing.Point(113, 55);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(112, 15);
            this.lblCardType.TabIndex = 3;
            this.lblCardType.Text = "Type";
            // 
            // lblCardLevel
            // 
            this.lblCardLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardLevel.Location = new System.Drawing.Point(113, 75);
            this.lblCardLevel.Name = "lblCardLevel";
            this.lblCardLevel.Size = new System.Drawing.Size(65, 15);
            this.lblCardLevel.TabIndex = 2;
            this.lblCardLevel.Text = "Level";
            // 
            // lblCardName
            // 
            this.lblCardName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardName.Location = new System.Drawing.Point(113, 14);
            this.lblCardName.Name = "lblCardName";
            this.lblCardName.Size = new System.Drawing.Size(200, 18);
            this.lblCardName.TabIndex = 1;
            this.lblCardName.Text = "Card Name";
            // 
            // PicCardArtwork
            // 
            this.PicCardArtwork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicCardArtwork.Location = new System.Drawing.Point(9, 13);
            this.PicCardArtwork.Name = "PicCardArtwork";
            this.PicCardArtwork.Size = new System.Drawing.Size(100, 100);
            this.PicCardArtwork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicCardArtwork.TabIndex = 0;
            this.PicCardArtwork.TabStop = false;
            // 
            // listDeckList
            // 
            this.listDeckList.FormattingEnabled = true;
            this.listDeckList.Items.AddRange(new object[] {
            "DECK A",
            "DECK B",
            "DECK C"});
            this.listDeckList.Location = new System.Drawing.Point(322, 33);
            this.listDeckList.Name = "listDeckList";
            this.listDeckList.Size = new System.Drawing.Size(86, 43);
            this.listDeckList.TabIndex = 9;
            this.listDeckList.SelectedIndexChanged += new System.EventHandler(this.listDeckList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(309, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ready to use:";
            // 
            // PicDeckStatus
            // 
            this.PicDeckStatus.BackColor = System.Drawing.Color.Transparent;
            this.PicDeckStatus.Location = new System.Drawing.Point(396, 79);
            this.PicDeckStatus.Name = "PicDeckStatus";
            this.PicDeckStatus.Size = new System.Drawing.Size(22, 22);
            this.PicDeckStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDeckStatus.TabIndex = 11;
            this.PicDeckStatus.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.Location = new System.Drawing.Point(640, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(132, 22);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "SAVE AND EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblSaveDeckOutput
            // 
            this.lblSaveDeckOutput.AutoSize = true;
            this.lblSaveDeckOutput.BackColor = System.Drawing.Color.Transparent;
            this.lblSaveDeckOutput.ForeColor = System.Drawing.Color.Red;
            this.lblSaveDeckOutput.Location = new System.Drawing.Point(622, 23);
            this.lblSaveDeckOutput.Name = "lblSaveDeckOutput";
            this.lblSaveDeckOutput.Size = new System.Drawing.Size(159, 13);
            this.lblSaveDeckOutput.TabIndex = 14;
            this.lblSaveDeckOutput.Text = "Player must have 1 ready deck!!";
            this.lblSaveDeckOutput.Visible = false;
            // 
            // DeckBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lblSaveDeckOutput);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.PicDeckStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listDeckList);
            this.Controls.Add(this.GroupDiceInfo);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.lblStorage);
            this.Controls.Add(this.PanelStorage);
            this.Controls.Add(this.lblDeckName);
            this.Controls.Add(this.PanelDeck);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DeckBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DDM - Deck Builder Menu";
            this.PanelDeck.ResumeLayout(false);
            this.PanelDeck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicSymbol)).EndInit();
            this.GroupDiceInfo.ResumeLayout(false);
            this.PanelCardText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDiceFace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicCardArtwork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicDeckStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelDeck;
        private System.Windows.Forms.Label lblDeckName;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.Panel PanelStorage;
        private System.Windows.Forms.Label lblFusion;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox GroupDiceInfo;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label lblCardLevel;
        private System.Windows.Forms.Label lblCardName;
        private System.Windows.Forms.PictureBox PicCardArtwork;
        private System.Windows.Forms.Label lblCardText;
        private System.Windows.Forms.PictureBox PicDiceFace6;
        private System.Windows.Forms.PictureBox PicDiceFace5;
        private System.Windows.Forms.PictureBox PicDiceFace4;
        private System.Windows.Forms.PictureBox PicDiceFace3;
        private System.Windows.Forms.PictureBox PicDiceFace2;
        private System.Windows.Forms.PictureBox PicDiceFace1;
        private System.Windows.Forms.Label lblDiceLevel;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblAttribute;
        private System.Windows.Forms.Panel PanelCardText;
        private System.Windows.Forms.ListBox listDeckList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PicDeckStatus;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox PicSymbol;
        private System.Windows.Forms.Button btnSymbolNext;
        private System.Windows.Forms.Button btnSymbolPrevious;
        private System.Windows.Forms.Label lblSaveDeckOutput;
    }
}