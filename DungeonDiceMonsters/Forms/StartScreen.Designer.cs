namespace DungeonDiceMonsters
{
    partial class StartScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
            this.btnOpenDBManager = new System.Windows.Forms.Button();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.lblWarning = new System.Windows.Forms.Label();
            this.btnOpenTestForm = new System.Windows.Forms.Button();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.lblInvalidNameBanner = new System.Windows.Forms.Label();
            this.PanelSelectionA = new System.Windows.Forms.Panel();
            this.PanelLoadGameOptions = new System.Windows.Forms.Panel();
            this.lblPreviewStarchips = new System.Windows.Forms.Label();
            this.lblPreviewPlayerName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelNewGameOptions = new System.Windows.Forms.Panel();
            this.RadioLoadGameOption = new System.Windows.Forms.RadioButton();
            this.RadioNewGameOption = new System.Windows.Forms.RadioButton();
            this.PanelSelectionA.SuspendLayout();
            this.PanelLoadGameOptions.SuspendLayout();
            this.PanelNewGameOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenDBManager
            // 
            this.btnOpenDBManager.Location = new System.Drawing.Point(12, 502);
            this.btnOpenDBManager.Name = "btnOpenDBManager";
            this.btnOpenDBManager.Size = new System.Drawing.Size(107, 47);
            this.btnOpenDBManager.TabIndex = 0;
            this.btnOpenDBManager.Text = "Open DB Manager";
            this.btnOpenDBManager.UseVisualStyleBackColor = true;
            this.btnOpenDBManager.Click += new System.EventHandler(this.btnOpenDBManager_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.BackColor = System.Drawing.Color.Transparent;
            this.btnStartGame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStartGame.BackgroundImage")));
            this.btnStartGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStartGame.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartGame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnStartGame.Location = new System.Drawing.Point(232, 328);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(332, 60);
            this.btnStartGame.TabIndex = 1;
            this.btnStartGame.Text = "Star Game";
            this.btnStartGame.UseVisualStyleBackColor = false;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackColor = System.Drawing.Color.Black;
            this.btnNewGame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewGame.BackgroundImage")));
            this.btnNewGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewGame.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnNewGame.Location = new System.Drawing.Point(34, 73);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(219, 40);
            this.btnNewGame.TabIndex = 2;
            this.btnNewGame.Text = "Start New Game";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.BackColor = System.Drawing.Color.Black;
            this.btnLoadGame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLoadGame.BackgroundImage")));
            this.btnLoadGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadGame.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadGame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnLoadGame.Location = new System.Drawing.Point(34, 73);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(219, 40);
            this.btnLoadGame.TabIndex = 3;
            this.btnLoadGame.Text = "Load Game";
            this.btnLoadGame.UseVisualStyleBackColor = false;
            this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.BackColor = System.Drawing.Color.Transparent;
            this.lblWarning.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.ForeColor = System.Drawing.Color.Yellow;
            this.lblWarning.Location = new System.Drawing.Point(15, 113);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(268, 16);
            this.lblWarning.TabIndex = 4;
            this.lblWarning.Text = "Warning: Existing save file will be lost.\r\n";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWarning.Visible = false;
            // 
            // btnOpenTestForm
            // 
            this.btnOpenTestForm.Location = new System.Drawing.Point(12, 450);
            this.btnOpenTestForm.Name = "btnOpenTestForm";
            this.btnOpenTestForm.Size = new System.Drawing.Size(107, 47);
            this.btnOpenTestForm.TabIndex = 5;
            this.btnOpenTestForm.Text = "Open Test Form";
            this.btnOpenTestForm.UseVisualStyleBackColor = true;
            this.btnOpenTestForm.Click += new System.EventHandler(this.btnOpenTestForm_Click);
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(79, 30);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(204, 20);
            this.txtPlayerName.TabIndex = 6;
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayerName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerName.ForeColor = System.Drawing.Color.White;
            this.lblPlayerName.Location = new System.Drawing.Point(6, 6);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(168, 24);
            this.lblPlayerName.TabIndex = 7;
            this.lblPlayerName.Text = "Input Player Name:";
            this.lblPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInvalidNameBanner
            // 
            this.lblInvalidNameBanner.BackColor = System.Drawing.Color.Transparent;
            this.lblInvalidNameBanner.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvalidNameBanner.ForeColor = System.Drawing.Color.Yellow;
            this.lblInvalidNameBanner.Location = new System.Drawing.Point(77, 52);
            this.lblInvalidNameBanner.Name = "lblInvalidNameBanner";
            this.lblInvalidNameBanner.Size = new System.Drawing.Size(170, 19);
            this.lblInvalidNameBanner.TabIndex = 8;
            this.lblInvalidNameBanner.Text = "Invalid Name Input";
            this.lblInvalidNameBanner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInvalidNameBanner.Visible = false;
            // 
            // PanelSelectionA
            // 
            this.PanelSelectionA.BackColor = System.Drawing.Color.Black;
            this.PanelSelectionA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelSelectionA.Controls.Add(this.PanelLoadGameOptions);
            this.PanelSelectionA.Controls.Add(this.PanelNewGameOptions);
            this.PanelSelectionA.Controls.Add(this.RadioLoadGameOption);
            this.PanelSelectionA.Controls.Add(this.RadioNewGameOption);
            this.PanelSelectionA.Location = new System.Drawing.Point(214, 315);
            this.PanelSelectionA.Name = "PanelSelectionA";
            this.PanelSelectionA.Size = new System.Drawing.Size(369, 234);
            this.PanelSelectionA.TabIndex = 11;
            this.PanelSelectionA.Visible = false;
            // 
            // PanelLoadGameOptions
            // 
            this.PanelLoadGameOptions.BackColor = System.Drawing.Color.Black;
            this.PanelLoadGameOptions.Controls.Add(this.lblPreviewStarchips);
            this.PanelLoadGameOptions.Controls.Add(this.lblPreviewPlayerName);
            this.PanelLoadGameOptions.Controls.Add(this.label2);
            this.PanelLoadGameOptions.Controls.Add(this.label1);
            this.PanelLoadGameOptions.Controls.Add(this.btnLoadGame);
            this.PanelLoadGameOptions.Location = new System.Drawing.Point(69, 94);
            this.PanelLoadGameOptions.Name = "PanelLoadGameOptions";
            this.PanelLoadGameOptions.Size = new System.Drawing.Size(289, 132);
            this.PanelLoadGameOptions.TabIndex = 12;
            this.PanelLoadGameOptions.Visible = false;
            // 
            // lblPreviewStarchips
            // 
            this.lblPreviewStarchips.BackColor = System.Drawing.Color.Transparent;
            this.lblPreviewStarchips.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviewStarchips.ForeColor = System.Drawing.Color.White;
            this.lblPreviewStarchips.Location = new System.Drawing.Point(111, 37);
            this.lblPreviewStarchips.Name = "lblPreviewStarchips";
            this.lblPreviewStarchips.Size = new System.Drawing.Size(164, 19);
            this.lblPreviewStarchips.TabIndex = 12;
            this.lblPreviewStarchips.Text = "5ghrthteyhrehg";
            this.lblPreviewStarchips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPreviewPlayerName
            // 
            this.lblPreviewPlayerName.BackColor = System.Drawing.Color.Transparent;
            this.lblPreviewPlayerName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviewPlayerName.ForeColor = System.Drawing.Color.White;
            this.lblPreviewPlayerName.Location = new System.Drawing.Point(111, 9);
            this.lblPreviewPlayerName.Name = "lblPreviewPlayerName";
            this.lblPreviewPlayerName.Size = new System.Drawing.Size(164, 19);
            this.lblPreviewPlayerName.TabIndex = 11;
            this.lblPreviewPlayerName.Text = "5ghrthteyhrehg";
            this.lblPreviewPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "Starchips:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Player:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PanelNewGameOptions
            // 
            this.PanelNewGameOptions.BackColor = System.Drawing.Color.Black;
            this.PanelNewGameOptions.Controls.Add(this.lblWarning);
            this.PanelNewGameOptions.Controls.Add(this.btnNewGame);
            this.PanelNewGameOptions.Controls.Add(this.lblPlayerName);
            this.PanelNewGameOptions.Controls.Add(this.lblInvalidNameBanner);
            this.PanelNewGameOptions.Controls.Add(this.txtPlayerName);
            this.PanelNewGameOptions.Location = new System.Drawing.Point(69, 94);
            this.PanelNewGameOptions.Name = "PanelNewGameOptions";
            this.PanelNewGameOptions.Size = new System.Drawing.Size(289, 132);
            this.PanelNewGameOptions.TabIndex = 12;
            // 
            // RadioLoadGameOption
            // 
            this.RadioLoadGameOption.AutoSize = true;
            this.RadioLoadGameOption.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioLoadGameOption.ForeColor = System.Drawing.Color.White;
            this.RadioLoadGameOption.Location = new System.Drawing.Point(12, 56);
            this.RadioLoadGameOption.Name = "RadioLoadGameOption";
            this.RadioLoadGameOption.Size = new System.Drawing.Size(198, 32);
            this.RadioLoadGameOption.TabIndex = 1;
            this.RadioLoadGameOption.Text = "Load Save File";
            this.RadioLoadGameOption.UseVisualStyleBackColor = true;
            this.RadioLoadGameOption.CheckedChanged += new System.EventHandler(this.RadioLoadGameOption_CheckedChanged);
            // 
            // RadioNewGameOption
            // 
            this.RadioNewGameOption.AutoSize = true;
            this.RadioNewGameOption.Checked = true;
            this.RadioNewGameOption.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioNewGameOption.ForeColor = System.Drawing.Color.White;
            this.RadioNewGameOption.Location = new System.Drawing.Point(12, 9);
            this.RadioNewGameOption.Name = "RadioNewGameOption";
            this.RadioNewGameOption.Size = new System.Drawing.Size(156, 32);
            this.RadioNewGameOption.TabIndex = 0;
            this.RadioNewGameOption.TabStop = true;
            this.RadioNewGameOption.Text = "New Game";
            this.RadioNewGameOption.UseVisualStyleBackColor = true;
            this.RadioNewGameOption.CheckedChanged += new System.EventHandler(this.RadioNewGameOption_CheckedChanged);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.PanelSelectionA);
            this.Controls.Add(this.btnOpenTestForm);
            this.Controls.Add(this.btnOpenDBManager);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDM - Start Screen";
            this.PanelSelectionA.ResumeLayout(false);
            this.PanelSelectionA.PerformLayout();
            this.PanelLoadGameOptions.ResumeLayout(false);
            this.PanelNewGameOptions.ResumeLayout(false);
            this.PanelNewGameOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDBManager;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnLoadGame;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Button btnOpenTestForm;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblInvalidNameBanner;
        private System.Windows.Forms.Panel PanelSelectionA;
        private System.Windows.Forms.RadioButton RadioLoadGameOption;
        private System.Windows.Forms.RadioButton RadioNewGameOption;
        private System.Windows.Forms.Panel PanelNewGameOptions;
        private System.Windows.Forms.Panel PanelLoadGameOptions;
        private System.Windows.Forms.Label lblPreviewStarchips;
        private System.Windows.Forms.Label lblPreviewPlayerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

