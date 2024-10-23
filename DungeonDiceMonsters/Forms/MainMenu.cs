using System;
using System.Windows.Forms;
using System.Drawing;

namespace DungeonDiceMonsters
{
    public partial class MainMenu : Form
    {
        #region Constructors
        public MainMenu()
        {
            SoundServer.PlayBackgroundMusic(Song.MainMenu);
            InitializeComponent();
            InitializeProfilePanel();
            InitializeEventListeners();
            
            void InitializeProfilePanel()
            {
                //Initialize the Profile Panel
                lblProfileName.Text = GameData.Name;
                PicProfilePic.Image = ImageServer.AvatarIcon(GameData.AvatarName);
                lblProfileLv.Text = string.Format("Lv {0}", GameData.Level.ToString());
                lblProfileStarchips.Text = string.Format("x {0}", GameData.StarChips.ToString());
                //Reset the bar to defaul
                BarExp.Minimum = 0;
                BarExp.Maximum = 100;
                BarExp.Value = 0;
                //Then initialize it
                BarExp.Maximum = GameData.GetNextLevelExp();
                BarExp.Minimum = GameData.BaseLevelExp();
                BarExp.Value = GameData.ExpPoints;
                lblProfileExp.Text = string.Format("{0}/{1}", GameData.ExpPoints, GameData.GetNextLevelExp());
            }
            void InitializeEventListeners()
            {
                lblMenuFreeDuel.MouseEnter += OnMouseEnterLabel;
                lblMenuFreeDuel.MouseLeave += OnMouseLeaveLabel;

                lblMenuDeckBuilder.MouseEnter += OnMouseEnterLabel;
                lblMenuDeckBuilder.MouseLeave += OnMouseLeaveLabel;

                lblMenuCardShop.MouseEnter += OnMouseEnterLabel;
                lblMenuCardShop.MouseLeave += OnMouseLeaveLabel;

                lblPvPDuel.MouseEnter += OnMouseEnterLabel;
                lblPvPDuel.MouseLeave += OnMouseLeaveLabel;

                lblMenuLibrary.MouseEnter += OnMouseEnterLabel;
                lblMenuLibrary.MouseLeave += OnMouseLeaveLabel;

                lblMenuPassword.MouseEnter += OnMouseEnterLabel;
                lblMenuPassword.MouseLeave += OnMouseLeaveLabel;

                lblMenuSettings.MouseEnter += OnMouseEnterLabel;
                lblMenuSettings.MouseLeave += OnMouseLeaveLabel;

                PicProfileDetailsButton.MouseEnter += OnMouseHover;
                PicProfileDetailsButton.MouseLeave += OnMouseHoverLeave;
            }
        }
        #endregion

        #region Events
        private void OnMouseHover(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
            PicProfileDetailsButton.BackColor = Color.Yellow;
        }
        private void OnMouseHoverLeave(object sender, EventArgs e)
        {
            PicProfileDetailsButton.BackColor = Color.Transparent;
        }
        private void OnMouseEnterLabel(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
            Label thisLabel = (Label)sender;
            thisLabel.BackColor = Color.Black;
        }
        private void OnMouseLeaveLabel(object sender, EventArgs e)
        {
            Label thisLabel = (Label)sender;
            thisLabel.BackColor = Color.Transparent;
        }
        private void lblMenuCardShop_Click(object sender, EventArgs e)
        {
            //TODO
        }
        private void lblMenuDeckBuilder_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Open the Deck Manager Form
            DecksManager DM = new DecksManager(true);
            Dispose();
            DM.Show();
        }
        private void lblMenuFreeDuel_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Open the Free Duel Form
            FreeDuelMenu FD = new FreeDuelMenu();
            Dispose();
            FD.Show();
        }
        private void lblPvPDuel_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Open the PvPMenu
            PvPMenu PVPM = new PvPMenu();
            Dispose();
            PVPM.Show();
        }
        private void lblMenuLibrary_Click(object sender, EventArgs e)
        {
            LibraryMenu LM = new LibraryMenu();
            Dispose();
            LM.Show();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void lblMenuPassword_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            PasswordMenu LM = new PasswordMenu();
            Dispose();
            LM.Show();
        }
        private void lblMenuSettings_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Open the Settings Menu
            SettingsMenu SM = new SettingsMenu();
            Dispose();
            SM.Show();
        }
        #endregion

        private void PicProfileDetailsButton_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Open the Profile Menu
            ProfileMenu SM = new ProfileMenu();
            Dispose();
            SM.Show();
        }

        private void btntest_Click(object sender, EventArgs e)
        {
            SaveFileManger.WriteAFUllSaveFile();
        }
    }
}