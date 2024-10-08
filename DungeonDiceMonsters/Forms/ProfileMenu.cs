//Joel Campos
//10/8/2024
//Profile Menu Form

using System;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class ProfileMenu : Form
    {
        #region Constructors
        public ProfileMenu()
        {
            SoundServer.PlayBackgroundMusic(Song.SettingsMenu);
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
                lblProfileExp.Text = string.Format("Exp: {0}/{1}", GameData.ExpPoints, GameData.GetNextLevelExp());
            }
            void InitializeEventListeners()
            {
                PicAvatar0.MouseEnter += OnMouseEnterLabel;
                PicAvatar0.MouseLeave += OnMouseLeaveLabel;
                PicAvatar1.MouseEnter += OnMouseEnterLabel;
                PicAvatar1.MouseLeave += OnMouseLeaveLabel;
                PicAvatar2.MouseEnter += OnMouseEnterLabel;
                PicAvatar2.MouseLeave += OnMouseLeaveLabel;
                PicAvatar3.MouseEnter += OnMouseEnterLabel;
                PicAvatar3.MouseLeave += OnMouseLeaveLabel;
                PicAvatar4.MouseEnter += OnMouseEnterLabel;
                PicAvatar4.MouseLeave += OnMouseLeaveLabel;
                PicAvatar5.MouseEnter += OnMouseEnterLabel;
                PicAvatar5.MouseLeave += OnMouseLeaveLabel;
                PicAvatar6.MouseEnter += OnMouseEnterLabel;
                PicAvatar6.MouseLeave += OnMouseLeaveLabel;
                PicAvatar7.MouseEnter += OnMouseEnterLabel;
                PicAvatar7.MouseLeave += OnMouseLeaveLabel;
                PicAvatar8.MouseEnter += OnMouseEnterLabel;
                PicAvatar8.MouseLeave += OnMouseLeaveLabel;
                PicAvatar9.MouseEnter += OnMouseEnterLabel;
                PicAvatar9.MouseLeave += OnMouseLeaveLabel;
                PicAvatar10.MouseEnter += OnMouseEnterLabel;
                PicAvatar10.MouseLeave += OnMouseLeaveLabel;
                PicAvatar11.MouseEnter += OnMouseEnterLabel;
                PicAvatar11.MouseLeave += OnMouseLeaveLabel;
                PicAvatar12.MouseEnter += OnMouseEnterLabel;
                PicAvatar12.MouseLeave += OnMouseLeaveLabel;
                PicAvatar13.MouseEnter += OnMouseEnterLabel;
                PicAvatar13.MouseLeave += OnMouseLeaveLabel;
                PicAvatar14.MouseEnter += OnMouseEnterLabel;
                PicAvatar14.MouseLeave += OnMouseLeaveLabel;
                PicAvatar15.MouseEnter += OnMouseEnterLabel;
                PicAvatar15.MouseLeave += OnMouseLeaveLabel;
                PicAvatar16.MouseEnter += OnMouseEnterLabel;
                PicAvatar16.MouseLeave += OnMouseLeaveLabel;
                PicAvatar17.MouseEnter += OnMouseEnterLabel;
                PicAvatar17.MouseLeave += OnMouseLeaveLabel;
                PicAvatar18.MouseEnter += OnMouseEnterLabel;
                PicAvatar18.MouseLeave += OnMouseLeaveLabel;
                PicAvatar19.MouseEnter += OnMouseEnterLabel;
                PicAvatar19.MouseLeave += OnMouseLeaveLabel;
                PicAvatar20.MouseEnter += OnMouseEnterLabel;
                PicAvatar20.MouseLeave += OnMouseLeaveLabel;
                PicAvatar21.MouseEnter += OnMouseEnterLabel;
                PicAvatar21.MouseLeave += OnMouseLeaveLabel;
                PicAvatar22.MouseEnter += OnMouseEnterLabel;
                PicAvatar22.MouseLeave += OnMouseLeaveLabel;
                PicAvatar23.MouseEnter += OnMouseEnterLabel;
                PicAvatar23.MouseLeave += OnMouseLeaveLabel;
                PicAvatar24.MouseEnter += OnMouseEnterLabel;
                PicAvatar24.MouseLeave += OnMouseLeaveLabel;
                PicAvatar0.Click += AvatarClick;
                PicAvatar1.Click += AvatarClick;
                PicAvatar2.Click += AvatarClick;
                PicAvatar3.Click += AvatarClick;
                PicAvatar4.Click += AvatarClick;
                PicAvatar5.Click += AvatarClick;
                PicAvatar6.Click += AvatarClick;
                PicAvatar7.Click += AvatarClick;
                PicAvatar8.Click += AvatarClick;
                PicAvatar9.Click += AvatarClick;
                PicAvatar10.Click += AvatarClick;
                PicAvatar11.Click += AvatarClick;
                PicAvatar12.Click += AvatarClick;
                PicAvatar13.Click += AvatarClick;
                PicAvatar14.Click += AvatarClick;
                PicAvatar15.Click += AvatarClick;
                PicAvatar16.Click += AvatarClick;
                PicAvatar17.Click += AvatarClick;
                PicAvatar18.Click += AvatarClick;
                PicAvatar19.Click += AvatarClick;
                PicAvatar20.Click += AvatarClick;
                PicAvatar21.Click += AvatarClick;
                PicAvatar22.Click += AvatarClick;
                PicAvatar23.Click += AvatarClick;
                PicAvatar24.Click += AvatarClick;
            }
        }
        #endregion

        #region Event Listeners
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            btnExit.Visible = false;
            GroupSettings.Visible = false;
            PanelAvatarSelector.Visible = true;
        }
        private void btnAvatarWindowClose_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            btnExit.Visible = true;
            GroupSettings.Visible = true;
            PanelAvatarSelector.Visible = false;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.GoBack);
            MainMenu MM = new MainMenu();
            Dispose();
            MM.Show();
        }
        private void OnMouseEnterLabel(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
            PictureBox thisPanel = (PictureBox)sender;
            int id = Convert.ToInt32(thisPanel.Tag);

            switch(id)
            {
                case 0: PanelAvatar0.BackColor = Color.Yellow; break;
                case 1: PanelAvatar1.BackColor = Color.Yellow; break;
                case 2: PanelAvatar2.BackColor = Color.Yellow; break;
                case 3: PanelAvatar3.BackColor = Color.Yellow; break;
                case 4: PanelAvatar4.BackColor = Color.Yellow; break;
                case 5: PanelAvatar5.BackColor = Color.Yellow; break;
                case 6: PanelAvatar6.BackColor = Color.Yellow; break;
                case 7: PanelAvatar7.BackColor = Color.Yellow; break;
                case 8: PanelAvatar8.BackColor = Color.Yellow; break;
                case 9: PanelAvatar9.BackColor = Color.Yellow; break;
                case 10: PanelAvatar10.BackColor = Color.Yellow; break;
                case 11: PanelAvatar11.BackColor = Color.Yellow; break;
                case 12: PanelAvatar12.BackColor = Color.Yellow; break;
                case 13: PanelAvatar13.BackColor = Color.Yellow; break;
                case 14: PanelAvatar14.BackColor = Color.Yellow; break;
                case 15: PanelAvatar15.BackColor = Color.Yellow; break;
                case 16: PanelAvatar16.BackColor = Color.Yellow; break;
                case 17: PanelAvatar17.BackColor = Color.Yellow; break;
                case 18: PanelAvatar18.BackColor = Color.Yellow; break;
                case 19: PanelAvatar19.BackColor = Color.Yellow; break;
                case 20: PanelAvatar20.BackColor = Color.Yellow; break;
                case 21: PanelAvatar21.BackColor = Color.Yellow; break;
                case 22: PanelAvatar22.BackColor = Color.Yellow; break;
                case 23: PanelAvatar23.BackColor = Color.Yellow; break;
                case 24: PanelAvatar24.BackColor = Color.Yellow; break;
            }
        }
        private void OnMouseLeaveLabel(object sender, EventArgs e)
        {
            PictureBox thisPanel = (PictureBox)sender;
            int id = Convert.ToInt32(thisPanel.Tag);

            switch (id)
            {
                case 0: PanelAvatar0.BackColor = Color.Transparent; break;
                case 1: PanelAvatar1.BackColor = Color.Transparent; break;
                case 2: PanelAvatar2.BackColor = Color.Transparent; break;
                case 3: PanelAvatar3.BackColor = Color.Transparent; break;
                case 4: PanelAvatar4.BackColor = Color.Transparent; break;
                case 5: PanelAvatar5.BackColor = Color.Transparent; break;
                case 6: PanelAvatar6.BackColor = Color.Transparent; break;
                case 7: PanelAvatar7.BackColor = Color.Transparent; break;
                case 8: PanelAvatar8.BackColor = Color.Transparent; break;
                case 9: PanelAvatar9.BackColor = Color.Transparent; break;
                case 10: PanelAvatar10.BackColor = Color.Transparent; break;
                case 11: PanelAvatar11.BackColor = Color.Transparent; break;
                case 12: PanelAvatar12.BackColor = Color.Transparent; break;
                case 13: PanelAvatar13.BackColor = Color.Transparent; break;
                case 14: PanelAvatar14.BackColor = Color.Transparent; break;
                case 15: PanelAvatar15.BackColor = Color.Transparent; break;
                case 16: PanelAvatar16.BackColor = Color.Transparent; break;
                case 17: PanelAvatar17.BackColor = Color.Transparent; break;
                case 18: PanelAvatar18.BackColor = Color.Transparent; break;
                case 19: PanelAvatar19.BackColor = Color.Transparent; break;
                case 20: PanelAvatar20.BackColor = Color.Transparent; break;
                case 21: PanelAvatar21.BackColor = Color.Transparent; break;
                case 22: PanelAvatar22.BackColor = Color.Transparent; break;
                case 23: PanelAvatar23.BackColor = Color.Transparent; break;
                case 24: PanelAvatar24.BackColor = Color.Transparent; break;
            }
        }
        private void AvatarClick(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            PictureBox thisPanel = (PictureBox)sender;
            int id = Convert.ToInt32(thisPanel.Tag);
            GameData.ChangeAvatarID(id);
            PicProfilePic.Image = ImageServer.AvatarIcon(GameData.AvatarName);
        }
        private void btnChangeName_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            btnExit.Visible = false;
            GroupSettings.Visible = false;
            PanelNameChanger.Visible = true;
        }
        private void BtnNameChangeSubmit_Click(object sender, EventArgs e)
        {
            string newName = txtNameInput.Text;
            if (string.IsNullOrEmpty(newName) || newName.Contains("|") || newName.Length > 15)
            {
                //invalid name
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                lblNameChangeError.Visible = true;
                BoardPvP.WaitNSeconds(2000);
                lblNameChangeError.Visible = false;
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.Accept);
                GameData.ChangePlayerName(newName);
                lblProfileName.Text = GameData.Name;
                btnExit.Visible = true;
                GroupSettings.Visible = true;
                PanelNameChanger.Visible = false;
            }
        }
        private void btnNameWindowClose_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            btnExit.Visible = true;
            GroupSettings.Visible = true;
            PanelNameChanger.Visible = false;
        }
        #endregion
    }
}
