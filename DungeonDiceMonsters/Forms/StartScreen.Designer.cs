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
            this.GroupNewGame = new System.Windows.Forms.GroupBox();
            this.GroupLoadGame = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupNewGame.SuspendLayout();
            this.GroupLoadGame.SuspendLayout();
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
            this.btnStartGame.BackColor = System.Drawing.Color.Black;
            this.btnStartGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStartGame.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartGame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnStartGame.Location = new System.Drawing.Point(227, 358);
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
            this.btnNewGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewGame.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnNewGame.Location = new System.Drawing.Point(36, 98);
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
            this.btnLoadGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadGame.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadGame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnLoadGame.Location = new System.Drawing.Point(39, 98);
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
            this.lblWarning.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.ForeColor = System.Drawing.Color.Yellow;
            this.lblWarning.Location = new System.Drawing.Point(4, 138);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(291, 41);
            this.lblWarning.TabIndex = 4;
            this.lblWarning.Text = "Warning: Existing save file will be lost.\r\n";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWarning.Visible = false;
            // 
            // btnOpenTestForm
            // 
            this.btnOpenTestForm.Location = new System.Drawing.Point(137, 502);
            this.btnOpenTestForm.Name = "btnOpenTestForm";
            this.btnOpenTestForm.Size = new System.Drawing.Size(107, 47);
            this.btnOpenTestForm.TabIndex = 5;
            this.btnOpenTestForm.Text = "Open Test Form";
            this.btnOpenTestForm.UseVisualStyleBackColor = true;
            this.btnOpenTestForm.Click += new System.EventHandler(this.btnOpenTestForm_Click);
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(67, 38);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(204, 20);
            this.txtPlayerName.TabIndex = 6;
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayerName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerName.ForeColor = System.Drawing.Color.White;
            this.lblPlayerName.Location = new System.Drawing.Point(6, 13);
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
            this.lblInvalidNameBanner.Location = new System.Drawing.Point(42, 58);
            this.lblInvalidNameBanner.Name = "lblInvalidNameBanner";
            this.lblInvalidNameBanner.Size = new System.Drawing.Size(170, 30);
            this.lblInvalidNameBanner.TabIndex = 8;
            this.lblInvalidNameBanner.Text = "Invalid Name Input";
            this.lblInvalidNameBanner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInvalidNameBanner.Visible = false;
            // 
            // GroupNewGame
            // 
            this.GroupNewGame.BackColor = System.Drawing.Color.Transparent;
            this.GroupNewGame.Controls.Add(this.btnNewGame);
            this.GroupNewGame.Controls.Add(this.lblInvalidNameBanner);
            this.GroupNewGame.Controls.Add(this.lblPlayerName);
            this.GroupNewGame.Controls.Add(this.lblWarning);
            this.GroupNewGame.Controls.Add(this.txtPlayerName);
            this.GroupNewGame.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupNewGame.ForeColor = System.Drawing.Color.Tomato;
            this.GroupNewGame.Location = new System.Drawing.Point(81, 315);
            this.GroupNewGame.Name = "GroupNewGame";
            this.GroupNewGame.Size = new System.Drawing.Size(290, 165);
            this.GroupNewGame.TabIndex = 9;
            this.GroupNewGame.TabStop = false;
            this.GroupNewGame.Text = "New Game";
            this.GroupNewGame.Visible = false;
            // 
            // GroupLoadGame
            // 
            this.GroupLoadGame.BackColor = System.Drawing.Color.Transparent;
            this.GroupLoadGame.Controls.Add(this.label1);
            this.GroupLoadGame.Controls.Add(this.btnLoadGame);
            this.GroupLoadGame.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupLoadGame.ForeColor = System.Drawing.Color.Tomato;
            this.GroupLoadGame.Location = new System.Drawing.Point(420, 315);
            this.GroupLoadGame.Name = "GroupLoadGame";
            this.GroupLoadGame.Size = new System.Drawing.Size(290, 165);
            this.GroupLoadGame.TabIndex = 10;
            this.GroupLoadGame.TabStop = false;
            this.GroupLoadGame.Text = "Load Game";
            this.GroupLoadGame.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Location = new System.Drawing.Point(67, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "Save File Exists!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.GroupLoadGame);
            this.Controls.Add(this.GroupNewGame);
            this.Controls.Add(this.btnOpenTestForm);
            this.Controls.Add(this.btnOpenDBManager);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDM - Start Screen";
            this.GroupNewGame.ResumeLayout(false);
            this.GroupNewGame.PerformLayout();
            this.GroupLoadGame.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox GroupNewGame;
        private System.Windows.Forms.GroupBox GroupLoadGame;
        private System.Windows.Forms.Label label1;
    }
}

