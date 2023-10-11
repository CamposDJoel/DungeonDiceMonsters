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
            this.btnStartGame.Location = new System.Drawing.Point(210, 369);
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
            this.btnNewGame.Location = new System.Drawing.Point(207, 335);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(335, 60);
            this.btnNewGame.TabIndex = 2;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Visible = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.BackColor = System.Drawing.Color.Black;
            this.btnLoadGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadGame.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadGame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnLoadGame.Location = new System.Drawing.Point(207, 401);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(335, 60);
            this.btnLoadGame.TabIndex = 3;
            this.btnLoadGame.Text = "Load Game";
            this.btnLoadGame.UseVisualStyleBackColor = false;
            this.btnLoadGame.Visible = false;
            this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.BackColor = System.Drawing.Color.Transparent;
            this.lblWarning.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.ForeColor = System.Drawing.Color.Yellow;
            this.lblWarning.Location = new System.Drawing.Point(209, 294);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(351, 41);
            this.lblWarning.TabIndex = 4;
            this.lblWarning.Text = "Warning: Existing save file will be lost when editing decks or playing your first" +
    " game.\r\n";
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
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnOpenTestForm);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnLoadGame);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.btnOpenDBManager);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDM - Start Screen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDBManager;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnLoadGame;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Button btnOpenTestForm;
    }
}

