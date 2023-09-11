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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();

            lblMenuArcade.MouseEnter += OnMouseEnterLabel;
            lblMenuArcade.MouseLeave += OnMouseLeaveLabel;

            lblMenuFreeDuel.MouseEnter += OnMouseEnterLabel;
            lblMenuFreeDuel.MouseLeave += OnMouseLeaveLabel;

            lblMenuDeckBuilder.MouseEnter += OnMouseEnterLabel;
            lblMenuDeckBuilder.MouseLeave += OnMouseLeaveLabel;

            lblMenuCardShop.MouseEnter += OnMouseEnterLabel;
            lblMenuCardShop.MouseLeave += OnMouseLeaveLabel;

            if(!DecksData.HasOneReadyDeck())
            {
                lblMenuArcade.Visible = false;
                lblMenuFreeDuel.Visible = false;
                lblDuelsNotAvailable.Visible = true;
            }

        }

        //Events
        private void OnMouseEnterLabel(object sender, EventArgs e)
        {
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
            //Open the Deckbuilder form
            DeckBuilder DB = new DeckBuilder();
            Dispose();
            DB.Show();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void lblMenuFreeDuel_Click(object sender, EventArgs e)
        {
            //Open the Free Duel Form
            FreeDuelMenu FD = new FreeDuelMenu();
            Dispose();
            FD.Show();
        }

        private void lblMenuArcade_Click(object sender, EventArgs e)
        {

        }
    }
}
