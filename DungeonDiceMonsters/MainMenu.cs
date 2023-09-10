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

        }

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

        }

        private void lblMenuDeckBuilder_Click(object sender, EventArgs e)
        {
            //Open the Deckbuilder form
            DeckBuilder DB = new DeckBuilder();
            Dispose();
            DB.Show();
        }
    }
}
