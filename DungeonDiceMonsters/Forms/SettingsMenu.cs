//Joel Campos
//10/3/2024
//Settings Form Class

using System;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class SettingsMenu : Form
    {
        #region Constructors
        public SettingsMenu()
        {
            InitializeComponent();
            InitializeUI();
            SoundServer.PlayBackgroundMusic(Song.SettingsMenu);

            void InitializeUI()
            {
                if (SettingsData.IsMusicON) { radioOptionMusicON.Checked = true; } else { radioOptionMusicOFF.Checked = true; };
                if (SettingsData.IsSFXON) { radioOptionSFXON.Checked = true; } else { radioOptionSFXOFF.Checked = true; };
            }
        }
        #endregion

        #region Event Listeners
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.GoBack);
            MainMenu MM = new MainMenu();
            Dispose();
            MM.Show();
        }
        private void radioOptionMusicON_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOptionMusicON.Checked)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                SettingsData.SetMusicONSetting(true);
                SoundServer.PlayBackgroundMusic(Song.SettingsMenu);
            }
        }
        private void radioOptionMusicOFF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOptionMusicOFF.Checked)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                SoundServer.StopBackgroundMusic();
                SettingsData.SetMusicONSetting(false);
            }
        }
        private void radioOptionSFXON_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOptionSFXON.Checked)
            {
                SettingsData.SetSFXONSetting(true);
                SoundServer.PlaySoundEffect(SoundEffect.Click);
            }
        }
        private void radioOptionSFXOFF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOptionSFXOFF.Checked)
            {
                SettingsData.SetSFXONSetting(false);
                SoundServer.PlaySoundEffect(SoundEffect.Click);
            }
        }
        #endregion
    }
}