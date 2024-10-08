//Joel Campos
//10/3/2024
//Settings Form Class

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class SettingsMenu : Form
    {
        public SettingsMenu()
        {
            InitializeComponent();
            InitializeUI();

            void InitializeUI()
            {
                if (SettingsData.IsMusicON) { radioOptionMusicON.Checked = true; } else { radioOptionMusicOFF.Checked = true; };
                if (SettingsData.IsSFXON) { radioOptionSFXON.Checked = true; } else { radioOptionSFXOFF.Checked = true; };
            }
        }

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
            }
        }
        private void radioOptionMusicOFF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOptionMusicOFF.Checked)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                SettingsData.SetMusicONSetting(false);
            }
        }
        private void radioOptionSFXON_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOptionSFXON.Checked)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                SettingsData.SetSFXONSetting(true);
            }
        }
        private void radioOptionSFXOFF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOptionSFXOFF.Checked)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                SettingsData.SetSFXONSetting(false);
            }
        }
    }
}
