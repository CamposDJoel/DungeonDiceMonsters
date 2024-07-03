namespace DungeonDiceMonsters
{
    partial class DecksManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecksManager));
            this.PanelDeck = new System.Windows.Forms.Panel();
            this.lblSymbolName = new System.Windows.Forms.Label();
            this.PicSymbol = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFusion = new System.Windows.Forms.Label();
            this.btnEditDeck = new System.Windows.Forms.Button();
            this.listDecks = new System.Windows.Forms.ListBox();
            this.lblSelectDeck = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtDeckNameInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDeckName = new System.Windows.Forms.Label();
            this.checkEnableDelete = new System.Windows.Forms.CheckBox();
            this.btnDeleteDeck = new System.Windows.Forms.Button();
            this.PicDeckStatus = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCopyDeck = new System.Windows.Forms.Button();
            this.GroupRename = new System.Windows.Forms.GroupBox();
            this.lblRenameWarning = new System.Windows.Forms.Label();
            this.btnRename = new System.Windows.Forms.Button();
            this.txtRenameNameInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDeleteWarning = new System.Windows.Forms.Label();
            this.lblStorageWarning = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.checkDefaultDeck = new System.Windows.Forms.CheckBox();
            this.PanelDeck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicSymbol)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicDeckStatus)).BeginInit();
            this.GroupRename.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelDeck
            // 
            this.PanelDeck.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PanelDeck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDeck.Controls.Add(this.lblSymbolName);
            this.PanelDeck.Controls.Add(this.PicSymbol);
            this.PanelDeck.Controls.Add(this.label2);
            this.PanelDeck.Controls.Add(this.lblFusion);
            this.PanelDeck.Location = new System.Drawing.Point(461, 56);
            this.PanelDeck.Name = "PanelDeck";
            this.PanelDeck.Size = new System.Drawing.Size(299, 395);
            this.PanelDeck.TabIndex = 1;
            // 
            // lblSymbolName
            // 
            this.lblSymbolName.AutoSize = true;
            this.lblSymbolName.BackColor = System.Drawing.Color.Transparent;
            this.lblSymbolName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSymbolName.ForeColor = System.Drawing.Color.PaleGreen;
            this.lblSymbolName.Location = new System.Drawing.Point(241, 297);
            this.lblSymbolName.Name = "lblSymbolName";
            this.lblSymbolName.Size = new System.Drawing.Size(43, 12);
            this.lblSymbolName.TabIndex = 5;
            this.lblSymbolName.Text = "EARTH";
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
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(190, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Symbol:";
            // 
            // lblFusion
            // 
            this.lblFusion.AutoSize = true;
            this.lblFusion.BackColor = System.Drawing.Color.Transparent;
            this.lblFusion.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFusion.ForeColor = System.Drawing.Color.Transparent;
            this.lblFusion.Location = new System.Drawing.Point(4, 297);
            this.lblFusion.Name = "lblFusion";
            this.lblFusion.Size = new System.Drawing.Size(81, 12);
            this.lblFusion.TabIndex = 0;
            this.lblFusion.Text = "Fusion Cards:";
            // 
            // btnEditDeck
            // 
            this.btnEditDeck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditDeck.BackgroundImage")));
            this.btnEditDeck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditDeck.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDeck.ForeColor = System.Drawing.Color.White;
            this.btnEditDeck.Location = new System.Drawing.Point(532, 485);
            this.btnEditDeck.Name = "btnEditDeck";
            this.btnEditDeck.Size = new System.Drawing.Size(178, 57);
            this.btnEditDeck.TabIndex = 2;
            this.btnEditDeck.Text = "Edit Deck";
            this.btnEditDeck.UseVisualStyleBackColor = true;
            this.btnEditDeck.Click += new System.EventHandler(this.btnEditDeck_Click);
            // 
            // listDecks
            // 
            this.listDecks.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.listDecks.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listDecks.ForeColor = System.Drawing.Color.Yellow;
            this.listDecks.FormattingEnabled = true;
            this.listDecks.ItemHeight = 22;
            this.listDecks.Location = new System.Drawing.Point(30, 57);
            this.listDecks.Name = "listDecks";
            this.listDecks.Size = new System.Drawing.Size(261, 312);
            this.listDecks.TabIndex = 3;
            this.listDecks.SelectedIndexChanged += new System.EventHandler(this.listDecks_SelectedIndexChanged);
            // 
            // lblSelectDeck
            // 
            this.lblSelectDeck.AutoSize = true;
            this.lblSelectDeck.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectDeck.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectDeck.ForeColor = System.Drawing.Color.White;
            this.lblSelectDeck.Location = new System.Drawing.Point(24, 23);
            this.lblSelectDeck.Name = "lblSelectDeck";
            this.lblSelectDeck.Size = new System.Drawing.Size(179, 32);
            this.lblSelectDeck.TabIndex = 4;
            this.lblSelectDeck.Text = "Select Deck:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Controls.Add(this.lblWarning);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.txtDeckNameInput);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Yellow;
            this.groupBox1.Location = new System.Drawing.Point(49, 390);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 156);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create New Deck";
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(10, 127);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(55, 17);
            this.lblWarning.TabIndex = 4;
            this.lblWarning.Text = "Name:";
            this.lblWarning.Visible = false;
            // 
            // btnCreate
            // 
            this.btnCreate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreate.BackgroundImage")));
            this.btnCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreate.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(42, 81);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(140, 43);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtDeckNameInput
            // 
            this.txtDeckNameInput.Location = new System.Drawing.Point(28, 50);
            this.txtDeckNameInput.Name = "txtDeckNameInput";
            this.txtDeckNameInput.Size = new System.Drawing.Size(166, 25);
            this.txtDeckNameInput.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // lblDeckName
            // 
            this.lblDeckName.AutoSize = true;
            this.lblDeckName.BackColor = System.Drawing.Color.Transparent;
            this.lblDeckName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeckName.ForeColor = System.Drawing.Color.White;
            this.lblDeckName.Location = new System.Drawing.Point(456, 21);
            this.lblDeckName.Name = "lblDeckName";
            this.lblDeckName.Size = new System.Drawing.Size(230, 32);
            this.lblDeckName.TabIndex = 6;
            this.lblDeckName.Text = "Test Deck Name";
            // 
            // checkEnableDelete
            // 
            this.checkEnableDelete.AutoSize = true;
            this.checkEnableDelete.BackColor = System.Drawing.Color.Black;
            this.checkEnableDelete.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEnableDelete.ForeColor = System.Drawing.Color.Red;
            this.checkEnableDelete.Location = new System.Drawing.Point(300, 227);
            this.checkEnableDelete.Name = "checkEnableDelete";
            this.checkEnableDelete.Size = new System.Drawing.Size(101, 16);
            this.checkEnableDelete.TabIndex = 7;
            this.checkEnableDelete.Text = "Enable Delete";
            this.checkEnableDelete.UseVisualStyleBackColor = false;
            this.checkEnableDelete.CheckedChanged += new System.EventHandler(this.checkEnableDelete_CheckedChanged);
            // 
            // btnDeleteDeck
            // 
            this.btnDeleteDeck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteDeck.BackgroundImage")));
            this.btnDeleteDeck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteDeck.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteDeck.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteDeck.Location = new System.Drawing.Point(300, 261);
            this.btnDeleteDeck.Name = "btnDeleteDeck";
            this.btnDeleteDeck.Size = new System.Drawing.Size(139, 36);
            this.btnDeleteDeck.TabIndex = 8;
            this.btnDeleteDeck.Text = "Delete Deck";
            this.btnDeleteDeck.UseVisualStyleBackColor = true;
            this.btnDeleteDeck.Visible = false;
            this.btnDeleteDeck.Click += new System.EventHandler(this.btnDeleteDeck_Click);
            // 
            // PicDeckStatus
            // 
            this.PicDeckStatus.BackColor = System.Drawing.Color.Transparent;
            this.PicDeckStatus.Location = new System.Drawing.Point(609, 454);
            this.PicDeckStatus.Name = "PicDeckStatus";
            this.PicDeckStatus.Size = new System.Drawing.Size(26, 26);
            this.PicDeckStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDeckStatus.TabIndex = 13;
            this.PicDeckStatus.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(464, 454);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Ready to use:";
            // 
            // btnCopyDeck
            // 
            this.btnCopyDeck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyDeck.BackgroundImage")));
            this.btnCopyDeck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCopyDeck.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyDeck.ForeColor = System.Drawing.Color.White;
            this.btnCopyDeck.Location = new System.Drawing.Point(310, 517);
            this.btnCopyDeck.Name = "btnCopyDeck";
            this.btnCopyDeck.Size = new System.Drawing.Size(139, 36);
            this.btnCopyDeck.TabIndex = 14;
            this.btnCopyDeck.Text = "Copy Deck";
            this.btnCopyDeck.UseVisualStyleBackColor = true;
            this.btnCopyDeck.Visible = false;
            this.btnCopyDeck.Click += new System.EventHandler(this.btnCopyDeck_Click);
            // 
            // GroupRename
            // 
            this.GroupRename.BackColor = System.Drawing.Color.Black;
            this.GroupRename.Controls.Add(this.lblRenameWarning);
            this.GroupRename.Controls.Add(this.btnRename);
            this.GroupRename.Controls.Add(this.txtRenameNameInput);
            this.GroupRename.Controls.Add(this.label5);
            this.GroupRename.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupRename.ForeColor = System.Drawing.Color.Yellow;
            this.GroupRename.Location = new System.Drawing.Point(299, 56);
            this.GroupRename.Name = "GroupRename";
            this.GroupRename.Size = new System.Drawing.Size(138, 165);
            this.GroupRename.TabIndex = 15;
            this.GroupRename.TabStop = false;
            this.GroupRename.Text = "Rename";
            // 
            // lblRenameWarning
            // 
            this.lblRenameWarning.ForeColor = System.Drawing.Color.Red;
            this.lblRenameWarning.Location = new System.Drawing.Point(13, 124);
            this.lblRenameWarning.Name = "lblRenameWarning";
            this.lblRenameWarning.Size = new System.Drawing.Size(115, 35);
            this.lblRenameWarning.TabIndex = 4;
            this.lblRenameWarning.Text = "Name:";
            this.lblRenameWarning.Visible = false;
            // 
            // btnRename
            // 
            this.btnRename.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRename.BackgroundImage")));
            this.btnRename.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRename.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRename.ForeColor = System.Drawing.Color.White;
            this.btnRename.Location = new System.Drawing.Point(9, 77);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(119, 43);
            this.btnRename.TabIndex = 3;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // txtRenameNameInput
            // 
            this.txtRenameNameInput.Location = new System.Drawing.Point(9, 46);
            this.txtRenameNameInput.Name = "txtRenameNameInput";
            this.txtRenameNameInput.Size = new System.Drawing.Size(119, 25);
            this.txtRenameNameInput.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "New Name:";
            // 
            // lblDeleteWarning
            // 
            this.lblDeleteWarning.AutoSize = true;
            this.lblDeleteWarning.BackColor = System.Drawing.Color.Black;
            this.lblDeleteWarning.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeleteWarning.ForeColor = System.Drawing.Color.Red;
            this.lblDeleteWarning.Location = new System.Drawing.Point(299, 299);
            this.lblDeleteWarning.Name = "lblDeleteWarning";
            this.lblDeleteWarning.Size = new System.Drawing.Size(142, 12);
            this.lblDeleteWarning.TabIndex = 5;
            this.lblDeleteWarning.Text = "Cannot Delete Last Deck";
            this.lblDeleteWarning.Visible = false;
            // 
            // lblStorageWarning
            // 
            this.lblStorageWarning.AutoSize = true;
            this.lblStorageWarning.BackColor = System.Drawing.Color.Black;
            this.lblStorageWarning.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStorageWarning.ForeColor = System.Drawing.Color.Yellow;
            this.lblStorageWarning.Location = new System.Drawing.Point(293, 246);
            this.lblStorageWarning.Name = "lblStorageWarning";
            this.lblStorageWarning.Size = new System.Drawing.Size(167, 12);
            this.lblStorageWarning.TabIndex = 16;
            this.lblStorageWarning.Text = "Cards will be sent to Storage.";
            this.lblStorageWarning.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.Location = new System.Drawing.Point(677, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(105, 22);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // checkDefaultDeck
            // 
            this.checkDefaultDeck.AutoSize = true;
            this.checkDefaultDeck.BackColor = System.Drawing.Color.Black;
            this.checkDefaultDeck.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkDefaultDeck.ForeColor = System.Drawing.Color.Yellow;
            this.checkDefaultDeck.Location = new System.Drawing.Point(654, 459);
            this.checkDefaultDeck.Name = "checkDefaultDeck";
            this.checkDefaultDeck.Size = new System.Drawing.Size(109, 19);
            this.checkDefaultDeck.TabIndex = 18;
            this.checkDefaultDeck.Text = "Default Deck";
            this.checkDefaultDeck.UseVisualStyleBackColor = false;
            this.checkDefaultDeck.CheckedChanged += new System.EventHandler(this.checkDefaultDeck_CheckedChanged);
            // 
            // DecksManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.checkDefaultDeck);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblStorageWarning);
            this.Controls.Add(this.lblDeleteWarning);
            this.Controls.Add(this.GroupRename);
            this.Controls.Add(this.btnCopyDeck);
            this.Controls.Add(this.PicDeckStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDeleteDeck);
            this.Controls.Add(this.checkEnableDelete);
            this.Controls.Add(this.lblDeckName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblSelectDeck);
            this.Controls.Add(this.listDecks);
            this.Controls.Add(this.btnEditDeck);
            this.Controls.Add(this.PanelDeck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DecksManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DDM - Decks Manager";
            this.PanelDeck.ResumeLayout(false);
            this.PanelDeck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicSymbol)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicDeckStatus)).EndInit();
            this.GroupRename.ResumeLayout(false);
            this.GroupRename.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelDeck;
        private System.Windows.Forms.PictureBox PicSymbol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFusion;
        private System.Windows.Forms.Button btnEditDeck;
        private System.Windows.Forms.ListBox listDecks;
        private System.Windows.Forms.Label lblSelectDeck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtDeckNameInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDeckName;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.CheckBox checkEnableDelete;
        private System.Windows.Forms.Button btnDeleteDeck;
        private System.Windows.Forms.PictureBox PicDeckStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSymbolName;
        private System.Windows.Forms.Button btnCopyDeck;
        private System.Windows.Forms.GroupBox GroupRename;
        private System.Windows.Forms.Label lblRenameWarning;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.TextBox txtRenameNameInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDeleteWarning;
        private System.Windows.Forms.Label lblStorageWarning;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox checkDefaultDeck;
    }
}