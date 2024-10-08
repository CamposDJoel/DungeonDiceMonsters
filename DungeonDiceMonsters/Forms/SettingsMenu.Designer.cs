namespace DungeonDiceMonsters
{
    partial class SettingsMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsMenu));
            this.GroupMusic = new System.Windows.Forms.GroupBox();
            this.radioOptionMusicOFF = new System.Windows.Forms.RadioButton();
            this.radioOptionMusicON = new System.Windows.Forms.RadioButton();
            this.GroupSFX = new System.Windows.Forms.GroupBox();
            this.radioOptionSFXOFF = new System.Windows.Forms.RadioButton();
            this.radioOptionSFXON = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupMusic.SuspendLayout();
            this.GroupSFX.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupMusic
            // 
            this.GroupMusic.BackColor = System.Drawing.Color.Transparent;
            this.GroupMusic.Controls.Add(this.radioOptionMusicOFF);
            this.GroupMusic.Controls.Add(this.radioOptionMusicON);
            this.GroupMusic.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupMusic.ForeColor = System.Drawing.SystemColors.Control;
            this.GroupMusic.Location = new System.Drawing.Point(22, 141);
            this.GroupMusic.Name = "GroupMusic";
            this.GroupMusic.Size = new System.Drawing.Size(164, 64);
            this.GroupMusic.TabIndex = 0;
            this.GroupMusic.TabStop = false;
            this.GroupMusic.Text = "Music";
            // 
            // radioOptionMusicOFF
            // 
            this.radioOptionMusicOFF.AutoSize = true;
            this.radioOptionMusicOFF.Location = new System.Drawing.Point(94, 25);
            this.radioOptionMusicOFF.Name = "radioOptionMusicOFF";
            this.radioOptionMusicOFF.Size = new System.Drawing.Size(59, 22);
            this.radioOptionMusicOFF.TabIndex = 1;
            this.radioOptionMusicOFF.TabStop = true;
            this.radioOptionMusicOFF.Text = "OFF";
            this.radioOptionMusicOFF.UseVisualStyleBackColor = true;
            this.radioOptionMusicOFF.CheckedChanged += new System.EventHandler(this.radioOptionMusicOFF_CheckedChanged);
            // 
            // radioOptionMusicON
            // 
            this.radioOptionMusicON.AutoSize = true;
            this.radioOptionMusicON.Location = new System.Drawing.Point(10, 25);
            this.radioOptionMusicON.Name = "radioOptionMusicON";
            this.radioOptionMusicON.Size = new System.Drawing.Size(51, 22);
            this.radioOptionMusicON.TabIndex = 0;
            this.radioOptionMusicON.TabStop = true;
            this.radioOptionMusicON.Text = "ON";
            this.radioOptionMusicON.UseVisualStyleBackColor = true;
            this.radioOptionMusicON.CheckedChanged += new System.EventHandler(this.radioOptionMusicON_CheckedChanged);
            // 
            // GroupSFX
            // 
            this.GroupSFX.BackColor = System.Drawing.Color.Transparent;
            this.GroupSFX.Controls.Add(this.radioOptionSFXOFF);
            this.GroupSFX.Controls.Add(this.radioOptionSFXON);
            this.GroupSFX.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupSFX.ForeColor = System.Drawing.SystemColors.Control;
            this.GroupSFX.Location = new System.Drawing.Point(22, 211);
            this.GroupSFX.Name = "GroupSFX";
            this.GroupSFX.Size = new System.Drawing.Size(164, 64);
            this.GroupSFX.TabIndex = 1;
            this.GroupSFX.TabStop = false;
            this.GroupSFX.Text = "SFX";
            // 
            // radioOptionSFXOFF
            // 
            this.radioOptionSFXOFF.AutoSize = true;
            this.radioOptionSFXOFF.Location = new System.Drawing.Point(94, 25);
            this.radioOptionSFXOFF.Name = "radioOptionSFXOFF";
            this.radioOptionSFXOFF.Size = new System.Drawing.Size(59, 22);
            this.radioOptionSFXOFF.TabIndex = 1;
            this.radioOptionSFXOFF.TabStop = true;
            this.radioOptionSFXOFF.Text = "OFF";
            this.radioOptionSFXOFF.UseVisualStyleBackColor = true;
            this.radioOptionSFXOFF.CheckedChanged += new System.EventHandler(this.radioOptionSFXOFF_CheckedChanged);
            // 
            // radioOptionSFXON
            // 
            this.radioOptionSFXON.AutoSize = true;
            this.radioOptionSFXON.Location = new System.Drawing.Point(10, 25);
            this.radioOptionSFXON.Name = "radioOptionSFXON";
            this.radioOptionSFXON.Size = new System.Drawing.Size(51, 22);
            this.radioOptionSFXON.TabIndex = 0;
            this.radioOptionSFXON.TabStop = true;
            this.radioOptionSFXON.Text = "ON";
            this.radioOptionSFXON.UseVisualStyleBackColor = true;
            this.radioOptionSFXON.CheckedChanged += new System.EventHandler(this.radioOptionSFXON_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.Location = new System.Drawing.Point(667, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(105, 22);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 40);
            this.label1.TabIndex = 20;
            this.label1.Text = "Settings";
            // 
            // SettingsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.GroupSFX);
            this.Controls.Add(this.GroupMusic);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings Menu - Dungeon Dice Monsters";
            this.GroupMusic.ResumeLayout(false);
            this.GroupMusic.PerformLayout();
            this.GroupSFX.ResumeLayout(false);
            this.GroupSFX.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupMusic;
        private System.Windows.Forms.RadioButton radioOptionMusicOFF;
        private System.Windows.Forms.RadioButton radioOptionMusicON;
        private System.Windows.Forms.GroupBox GroupSFX;
        private System.Windows.Forms.RadioButton radioOptionSFXOFF;
        private System.Windows.Forms.RadioButton radioOptionSFXON;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
    }
}