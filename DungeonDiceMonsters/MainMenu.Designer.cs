namespace DungeonDiceMonsters
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.lblMenuArcade = new System.Windows.Forms.Label();
            this.lblMenuFreeDuel = new System.Windows.Forms.Label();
            this.lblMenuDeckBuilder = new System.Windows.Forms.Label();
            this.lblMenuCardShop = new System.Windows.Forms.Label();
            this.lblDuelsNotAvailable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMenuArcade
            // 
            this.lblMenuArcade.BackColor = System.Drawing.Color.Transparent;
            this.lblMenuArcade.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuArcade.ForeColor = System.Drawing.Color.White;
            this.lblMenuArcade.Location = new System.Drawing.Point(39, 246);
            this.lblMenuArcade.Name = "lblMenuArcade";
            this.lblMenuArcade.Size = new System.Drawing.Size(255, 52);
            this.lblMenuArcade.TabIndex = 0;
            this.lblMenuArcade.Text = "Arcade Mode";
            this.lblMenuArcade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuFreeDuel
            // 
            this.lblMenuFreeDuel.BackColor = System.Drawing.Color.Transparent;
            this.lblMenuFreeDuel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuFreeDuel.ForeColor = System.Drawing.Color.White;
            this.lblMenuFreeDuel.Location = new System.Drawing.Point(39, 308);
            this.lblMenuFreeDuel.Name = "lblMenuFreeDuel";
            this.lblMenuFreeDuel.Size = new System.Drawing.Size(255, 52);
            this.lblMenuFreeDuel.TabIndex = 1;
            this.lblMenuFreeDuel.Text = "Free Duel";
            this.lblMenuFreeDuel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenuDeckBuilder
            // 
            this.lblMenuDeckBuilder.BackColor = System.Drawing.Color.Transparent;
            this.lblMenuDeckBuilder.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuDeckBuilder.ForeColor = System.Drawing.Color.White;
            this.lblMenuDeckBuilder.Location = new System.Drawing.Point(39, 370);
            this.lblMenuDeckBuilder.Name = "lblMenuDeckBuilder";
            this.lblMenuDeckBuilder.Size = new System.Drawing.Size(255, 52);
            this.lblMenuDeckBuilder.TabIndex = 2;
            this.lblMenuDeckBuilder.Text = "Deck Builder";
            this.lblMenuDeckBuilder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMenuDeckBuilder.Click += new System.EventHandler(this.lblMenuDeckBuilder_Click);
            // 
            // lblMenuCardShop
            // 
            this.lblMenuCardShop.BackColor = System.Drawing.Color.Transparent;
            this.lblMenuCardShop.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuCardShop.ForeColor = System.Drawing.Color.White;
            this.lblMenuCardShop.Location = new System.Drawing.Point(39, 433);
            this.lblMenuCardShop.Name = "lblMenuCardShop";
            this.lblMenuCardShop.Size = new System.Drawing.Size(255, 52);
            this.lblMenuCardShop.TabIndex = 3;
            this.lblMenuCardShop.Text = "Card Shop";
            this.lblMenuCardShop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMenuCardShop.Click += new System.EventHandler(this.lblMenuCardShop_Click);
            // 
            // lblDuelsNotAvailable
            // 
            this.lblDuelsNotAvailable.BackColor = System.Drawing.Color.Transparent;
            this.lblDuelsNotAvailable.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuelsNotAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDuelsNotAvailable.Location = new System.Drawing.Point(39, 308);
            this.lblDuelsNotAvailable.Name = "lblDuelsNotAvailable";
            this.lblDuelsNotAvailable.Size = new System.Drawing.Size(459, 52);
            this.lblDuelsNotAvailable.TabIndex = 4;
            this.lblDuelsNotAvailable.Text = "No Decks Ready to Duel!!";
            this.lblDuelsNotAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDuelsNotAvailable.Visible = false;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lblDuelsNotAvailable);
            this.Controls.Add(this.lblMenuCardShop);
            this.Controls.Add(this.lblMenuDeckBuilder);
            this.Controls.Add(this.lblMenuFreeDuel);
            this.Controls.Add(this.lblMenuArcade);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDM - Main Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMenuArcade;
        private System.Windows.Forms.Label lblMenuFreeDuel;
        private System.Windows.Forms.Label lblMenuDeckBuilder;
        private System.Windows.Forms.Label lblMenuCardShop;
        private System.Windows.Forms.Label lblDuelsNotAvailable;
    }
}