//Joel Campos
//10/3/2024
//Settings Form Class

using System;
using System.Windows.Forms;
using System.Collections.Generic;

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

                //Include/Exclude music list
                ReloadSongListsUI();
            }
        }
        #endregion

        #region Private Methods
        private void ReloadSongListsUI()
        {
            //Include/Exclude music list
            List<Song> IncludeList = SettingsData.IncludeSongList;
            List<Song> ExcludeList = SettingsData.ExcludeSongList;
            ListIncludeMusic.Items.Clear();
            ListExcludeMusic.Items.Clear();
            foreach (Song s in IncludeList)
            {
                ListIncludeMusic.Items.Add(SoundServer.GetSongNameToString(s));
            }
            if (ExcludeList.Count == 0)
            {
                ListExcludeMusic.Items.Add("No excluded songs");
            }
            else
            {
                foreach (Song s in ExcludeList)
                {
                    ListExcludeMusic.Items.Add(SoundServer.GetSongNameToString(s));
                }
            }
            ListIncludeMusic.SetSelected(0, true);
            ListExcludeMusic.SetSelected(0, true);
            if (IncludeList.Count == 1)
            {
                btnExcludeSong.Visible = false;
            }
            else
            {
                btnExcludeSong.Visible = true;
            }
            if (ExcludeList.Count == 0)
            {
                btnIncludeSong.Visible = false;
            }
            else
            {
                btnIncludeSong.Visible = true;
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
        private void btnExcludeSong_Click(object sender, EventArgs e)
        {
            int index = ListIncludeMusic.SelectedIndex;
            SettingsData.MoveSongToExcludeList(index);
            ReloadSongListsUI();
        }
        private void btnIncludeSong_Click(object sender, EventArgs e)
        {
            int index = ListExcludeMusic.SelectedIndex;
            SettingsData.MoveSongToIncludeList(index);
            ReloadSongListsUI();
        }
        #endregion    
    }
}