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
            this.GroupDuelMusic = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ListExcludeMusic = new System.Windows.Forms.ListBox();
            this.ListIncludeMusic = new System.Windows.Forms.ListBox();
            this.btnExcludeSong = new System.Windows.Forms.Button();
            this.btnIncludeSong = new System.Windows.Forms.Button();
            this.GroupMusic.SuspendLayout();
            this.GroupSFX.SuspendLayout();
            this.GroupDuelMusic.SuspendLayout();
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
            // GroupDuelMusic
            // 
            this.GroupDuelMusic.BackColor = System.Drawing.Color.Transparent;
            this.GroupDuelMusic.Controls.Add(this.btnIncludeSong);
            this.GroupDuelMusic.Controls.Add(this.btnExcludeSong);
            this.GroupDuelMusic.Controls.Add(this.label3);
            this.GroupDuelMusic.Controls.Add(this.label2);
            this.GroupDuelMusic.Controls.Add(this.ListExcludeMusic);
            this.GroupDuelMusic.Controls.Add(this.ListIncludeMusic);
            this.GroupDuelMusic.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupDuelMusic.ForeColor = System.Drawing.SystemColors.Control;
            this.GroupDuelMusic.Location = new System.Drawing.Point(22, 281);
            this.GroupDuelMusic.Name = "GroupDuelMusic";
            this.GroupDuelMusic.Size = new System.Drawing.Size(649, 175);
            this.GroupDuelMusic.TabIndex = 2;
            this.GroupDuelMusic.TabStop = false;
            this.GroupDuelMusic.Text = "Duel Music";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(340, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Exclude:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Include:";
            // 
            // ListExcludeMusic
            // 
            this.ListExcludeMusic.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListExcludeMusic.FormattingEnabled = true;
            this.ListExcludeMusic.ItemHeight = 15;
            this.ListExcludeMusic.Location = new System.Drawing.Point(343, 39);
            this.ListExcludeMusic.Name = "ListExcludeMusic";
            this.ListExcludeMusic.Size = new System.Drawing.Size(297, 124);
            this.ListExcludeMusic.TabIndex = 1;
            // 
            // ListIncludeMusic
            // 
            this.ListIncludeMusic.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListIncludeMusic.FormattingEnabled = true;
            this.ListIncludeMusic.ItemHeight = 15;
            this.ListIncludeMusic.Location = new System.Drawing.Point(6, 39);
            this.ListIncludeMusic.Name = "ListIncludeMusic";
            this.ListIncludeMusic.Size = new System.Drawing.Size(297, 124);
            this.ListIncludeMusic.TabIndex = 0;
            // 
            // btnExcludeSong
            // 
            this.btnExcludeSong.BackColor = System.Drawing.Color.Black;
            this.btnExcludeSong.ForeColor = System.Drawing.Color.Red;
            this.btnExcludeSong.Location = new System.Drawing.Point(306, 54);
            this.btnExcludeSong.Name = "btnExcludeSong";
            this.btnExcludeSong.Size = new System.Drawing.Size(32, 23);
            this.btnExcludeSong.TabIndex = 4;
            this.btnExcludeSong.Text = ">>>";
            this.btnExcludeSong.UseVisualStyleBackColor = false;
            this.btnExcludeSong.Click += new System.EventHandler(this.btnExcludeSong_Click);
            // 
            // btnIncludeSong
            // 
            this.btnIncludeSong.BackColor = System.Drawing.Color.Black;
            this.btnIncludeSong.ForeColor = System.Drawing.Color.Lime;
            this.btnIncludeSong.Location = new System.Drawing.Point(306, 116);
            this.btnIncludeSong.Name = "btnIncludeSong";
            this.btnIncludeSong.Size = new System.Drawing.Size(32, 23);
            this.btnIncludeSong.TabIndex = 5;
            this.btnIncludeSong.Text = "<<<";
            this.btnIncludeSong.UseVisualStyleBackColor = false;
            this.btnIncludeSong.Click += new System.EventHandler(this.btnIncludeSong_Click);
            // 
            // SettingsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.GroupDuelMusic);
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
            this.GroupDuelMusic.ResumeLayout(false);
            this.GroupDuelMusic.PerformLayout();
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
        private System.Windows.Forms.GroupBox GroupDuelMusic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox ListExcludeMusic;
        private System.Windows.Forms.ListBox ListIncludeMusic;
        private System.Windows.Forms.Button btnIncludeSong;
        private System.Windows.Forms.Button btnExcludeSong;
    }
}