using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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

            if (!DecksData.HasOneReadyDeck())
            {
                lblMenuArcade.Visible = false;
                lblMenuFreeDuel.Visible = false;
            }
        }
        #endregion

        #region Events
        private void OnMouseEnterLabel(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
            Label thisLabel = (Label)sender;
           thisLabel.BorderStyle = BorderStyle.FixedSingle;
        }
        private void OnMouseLeaveLabel(object sender, EventArgs e)
        {
            Label thisLabel = (Label)sender;
            thisLabel.BorderStyle = BorderStyle.None;
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
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}