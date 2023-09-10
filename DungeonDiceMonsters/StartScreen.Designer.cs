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
            this.btnStartGame.Location = new System.Drawing.Point(234, 422);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(332, 60);
            this.btnStartGame.TabIndex = 1;
            this.btnStartGame.Text = "Star Game";
            this.btnStartGame.UseVisualStyleBackColor = false;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnOpenDBManager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartScreen";
            this.Text = "DDM - Start Screen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDBManager;
        private System.Windows.Forms.Button btnStartGame;
    }
}

