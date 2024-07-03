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
            SoundServer.PlayBackgroundMusic(Song.MainMenu, true);
            InitializeComponent();

            lblMenuArcade.MouseEnter += OnMouseEnterLabel;
            lblMenuArcade.MouseLeave += OnMouseLeaveLabel;

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
        }
        #endregion

        #region Events
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
        private void lblMenuArcade_Click(object sender, EventArgs e)
        {

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
        #endregion
    }
}