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
    public partial class RollDiceMenu : Form
    {
        public RollDiceMenu(PlayerData playerdata, BoardForm board)
        {
            InitializeComponent();
            _PlayerData = playerdata;
            _Board = board;

            PicDice1.Click += RollCard_click;
            PicDice2.Click += RollCard_click;
            PicDice3.Click += RollCard_click;

            InitializeDeckComponents();
            LoadDeckPage();
        }

        private void InitializeDeckComponents()
        {
            //Index will be save on the Image Object Tag value
            //so when it is clicked it knows which index card is being clicked.
            int pictureIndex = 0;

            int Y_Location = 2;
            for (int x = 0; x < 4; x++)
            {
                int X_Location = 2;
                for (int y = 0; y < 5; y++)
                {
                    //Initialize the border box Image
                    Panel CardBox = new Panel();
                    PanelDeck.Controls.Add(CardBox);
                    CardBox.Location = new Point(X_Location, Y_Location);
                    CardBox.BorderStyle = BorderStyle.FixedSingle;
                    CardBox.Size = new Size(58, 74);
                    _DeckCardPanelList.Add(CardBox);

                    //Initialize the card Image
                    PictureBox CardImage = new PictureBox();
                    CardBox.Controls.Add(CardImage);
                    CardImage.Location = new Point(2, 2);
                    CardImage.BorderStyle = BorderStyle.FixedSingle;
                    CardImage.Size = new Size(52, 68);
                    CardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    _DeckCardImageList.Add(CardImage);
                    CardImage.Tag = pictureIndex;
                    CardImage.Click += new EventHandler(DeckCard_click);

                    X_Location += 58;
                    pictureIndex++;
                }
                Y_Location += 73;
            }

            //Initialize the 3 Cards for the Fusion Deck
            Y_Location = 314;
            int X_Location2 = 2;
            for (int x = 0; x < 3; x++)
            {
                //Initialize the border box Image
                Panel CardBox = new Panel();
                PanelDeck.Controls.Add(CardBox);
                CardBox.Location = new Point(X_Location2, Y_Location);
                CardBox.BorderStyle = BorderStyle.FixedSingle;
                CardBox.Size = new Size(58, 74);
                _DeckCardPanelList.Add(CardBox);

                //Initialize the card Image
                PictureBox CardImage = new PictureBox();
                CardBox.Controls.Add(CardImage);
                CardImage.Location = new Point(2, 2);
                CardImage.BorderStyle = BorderStyle.FixedSingle;
                CardImage.Size = new Size(52, 68);
                CardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                _DeckCardImageList.Add(CardImage);
                CardImage.Tag = pictureIndex;
                CardImage.Click += new EventHandler(DeckCard_click);

                X_Location2 += 58;
                pictureIndex++;
            }
        }
        private void LoadDeckPage()
        {
            for (int x = 0; x < 20; x++)
            {
                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (x >= _PlayerData.Deck.MainDeckSize)
                {
                    _DeckCardPanelList[x].Visible = false;
                }
                else
                {
                    //Make the card panel visible
                    _DeckCardPanelList[x].Visible = true;

                    //Get the card ID of the card to be displayed
                    int cardID = _PlayerData.Deck.GetMainCardIDAtIndex(x);

                    //Dispose the current image in this picture box (if there was one)
                    //to clear memory
                    if (_DeckCardImageList[x].Image != null) { _DeckCardImageList[x].Image.Dispose(); }

                    //Populate the card image with the card ID
                    _DeckCardImageList[x].Image = ImageServer.FullCardImage(cardID);
                }
            }

            //Load the fusion deck
            for (int x = 0; x < 3; x++)
            {
                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (x >= _PlayerData.Deck.FusionDeckSize)
                {
                    _DeckCardPanelList[x + 20].Visible = false;
                }
                else
                {
                    //Make the card panel visible
                    _DeckCardPanelList[x + 20].Visible = true;

                    //Get the card ID of the card to be displayed
                    int cardID = _PlayerData.Deck.GetFusionCardIDAtIndex(x);

                    //Dispose the current image in this picture box (if there was one)
                    //to clear memory
                    if (_DeckCardImageList[x + 20].Image != null) { _DeckCardImageList[x + 20].Image.Dispose(); }

                    //Populate the card image with the card ID
                    _DeckCardImageList[x + 20].Image = ImageServer.FullCardImage(cardID);
                }
            }

            //Update the Symbol
            _CurrentSymbolSelection = _PlayerData.Deck.Symbol;
            if (PicSymbol.Image != null) { PicSymbol.Image.Dispose(); }
            PicSymbol.Image = ImageServer.Symbol(_CurrentSymbolSelection);
        }
        private void LoadDiceToRoll()
        {
            PictureBox[] dices = new PictureBox[3];
            dices[0] = PicDice1;
            dices[1] = PicDice2;
            dices[2] = PicDice3;

            for(int x = 0; x < 3;x++) 
            {
                if(x >= _DiceToRoll.Count)
                {
                    //do not show picture
                    dices[x].Image = null;
                    dices[x].Enabled = false;
                }
                else
                {
                    dices[x].Enabled = true;
                    dices[x].Image = ImageServer.FullCardImage(_DiceToRoll[x].ID);
                }
            }
        }
        private void SetDeckSelector(int index)
        {
            //Clear all cards from being marked as selected
            for (int x = 0; x < 3; x++)
            {
                //TODO clear selector from the dice to roll
                //_CardPanelList[x].BackColor = Color.Transparent;
            }
            for (int x = 0; x < 23; x++)
            {
                _DeckCardPanelList[x].BackColor = Color.Transparent;
            }

            //Set the specific card selected
            _DeckCardPanelList[index].BackColor = Color.Yellow;

            //Reload the Card Info Panel UI
            LoadCardInfoPanel();
        }
        private void SetRollCardSelector(int index)
        {
            //Clear all cards from being marked as selected
            for (int x = 0; x < 23; x++)
            {
                _DeckCardPanelList[x].BackColor = Color.Transparent;
            }

            switch(index)
            {
                case 0:
                    PicDice1.BackColor = Color.Yellow;
                    PicDice2.BackColor = Color.Transparent;
                    PicDice1.BackColor = Color.Transparent;
                    break;
                case 1:
                    PicDice1.BackColor = Color.Transparent;
                    PicDice2.BackColor = Color.Yellow;
                    PicDice1.BackColor = Color.Transparent;
                    break;
                case 2:
                    PicDice1.BackColor = Color.Transparent;
                    PicDice2.BackColor = Color.Transparent;
                    PicDice1.BackColor = Color.Yellow;
                    break;
            }    

            //Reload the Card Info Panel UI
            LoadCardInfoPanel();
        }
        private void LoadCardInfoPanel()
        {
            //Get the CardInfo object to populate the UI
            //remember _currentCardSelected ref is set each time you click a card in the UI.
            CardInfo thisCard = _CurrentCardSelected;
            int cardID = thisCard.ID;

            //Populate the UI
            if (PicCardArtwork.Image != null) { PicCardArtwork.Image.Dispose(); }
            PicCardArtwork.Image = ImageServer.CardArtworkImage(cardID);

            string idstring = cardID.ToString();
            if (cardID < 10) { idstring = "000" + cardID; }
            else if (cardID < 100) { idstring = "00" + cardID; }
            else if (cardID < 1000) { idstring = "0" + cardID; }

            lblID.Text = idstring;
            lblCardName.Text = thisCard.Name;

            string secondaryType = "";
            if (thisCard.IsFusion) { secondaryType = "/Fusion"; }
            if (thisCard.IsRitual) { secondaryType = "/Ritual"; }
            if (thisCard.Category == "Spell") { secondaryType = " Spell"; }
            if (thisCard.Category == "Trap") { secondaryType = " Trap"; }


            lblCardType.Text = thisCard.Type + secondaryType;

            if (thisCard.Category == "Monster") { lblCardLevel.Text = "Lv. " + thisCard.Level; }
            else { lblCardLevel.Text = ""; }

            if (thisCard.Category == "Monster") { lblAttribute.Text = thisCard.Attribute; }
            else { lblAttribute.Text = ""; }

            lblDiceLevel.Text = "Dice Lv. " + thisCard.DiceLevel;

            if (thisCard.Category == "Monster")
            {
                lblStats.Text = "ATK " + thisCard.ATK + " / DEF " + thisCard.DEF + " / LP " + thisCard.LP;
            }
            else { lblStats.Text = ""; }

            lblCardText.Text = thisCard.CardText;

            //Dice Faces
            if (PicDiceFace1.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace2.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace3.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace4.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace5.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace6.Image != null) { PicDiceFace1.Image.Dispose(); }

            PicDiceFace1.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.Face1Crest, thisCard.Face1Value);
            PicDiceFace2.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.Face2Crest, thisCard.Face2Value);
            PicDiceFace3.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.Face3Crest, thisCard.Face3Value);
            PicDiceFace4.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.Face4Crest, thisCard.Face4Value);
            PicDiceFace5.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.Face5Crest, thisCard.Face5Value);
            PicDiceFace6.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.Face6Crest, thisCard.Face6Value);

        }

        BoardForm _Board;
        private PlayerData _PlayerData;
        private List<Panel> _DeckCardPanelList = new List<Panel>();
        private List<PictureBox> _DeckCardImageList = new List<PictureBox>();
        private CardInfo _CurrentCardSelected;
        private int _CurrentDeckIndexSelected = 0;
        private string _CurrentSymbolSelection;

        private List<CardInfo> _DiceToRoll = new List<CardInfo>();

        private void DeckCard_click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click2);

            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            //Save the ref to this index number for outer use
            _CurrentDeckIndexSelected = thiPictureBoxIndex;

            int cardid = -1;
            if (thiPictureBoxIndex >= 20)
            {
                int fusionIndex = thiPictureBoxIndex - 20;
                cardid = _PlayerData.Deck.GetFusionCardIDAtIndex(fusionIndex);

                //Hide arrows, player cannot roll dice of fusion cards
                PicToStoArrow.Visible = false;
                PicToDeckArrow.Visible = false;
            }
            else
            {
                cardid = _PlayerData.Deck.GetMainCardIDAtIndex(thiPictureBoxIndex);

                //Show arrows if there dice to roll list has space ( < 3 dice)
                if(_DiceToRoll.Count < 3)
                {
                    PicToStoArrow.Visible = true;
                }
                PicToDeckArrow.Visible = false;
            }
            _CurrentCardSelected = CardDataBase.GetCardWithID(cardid);

            //Change the pointer selector
            SetDeckSelector(thiPictureBoxIndex);           
        }
        private void RollCard_click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click2);

            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            int cardid = _DiceToRoll[thiPictureBoxIndex].ID;

            //Show to deck arrow only
            PicToStoArrow.Visible = false;
            PicToDeckArrow.Visible = true;

            _CurrentCardSelected = CardDataBase.GetCardWithID(cardid);

            SetRollCardSelector(thiPictureBoxIndex);
            LoadCardInfoPanel();
        }
        private void PicToStoArrow_Click(object sender, EventArgs e)
        {
            //Add this card to the list of cards to roll dice for
            _DiceToRoll.Add(_CurrentCardSelected);

            //Remove from deck
            _PlayerData.Deck.RemoveMainAtIndex(_CurrentDeckIndexSelected);

            //Reload both sides
            LoadDiceToRoll();
            LoadDeckPage();

            //Hide both arrows until another selection is clicked
            PicToDeckArrow.Visible = false;
            PicToStoArrow.Visible = false;

            //if the dice to roll is 3 or there no more cards in deck allow to roll
            if(_DiceToRoll.Count == 3 || _PlayerData.Deck.MainDeckSize == 0)
            {
                btnRoll.Visible = true;
            }
            
        }
        private void PicToDeckArrow_Click(object sender, EventArgs e)
        {
            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            //send card back to dec;
            _PlayerData.Deck.AddMainCard(_CurrentCardSelected.ID);

            //remove from dice to roll
            _DiceToRoll.RemoveAt(thiPictureBoxIndex);

            //Reload both sides
            LoadDiceToRoll();
            LoadDeckPage();

            //Hide both arrows until another selection is clicked
            PicToDeckArrow.Visible = false;
            PicToStoArrow.Visible = false;

            //THis will always result on not bein able to roll
            btnRoll.Visible = false;
        }
        private void btnRoll_Click(object sender, EventArgs e)
        {
            PanelDeck.Enabled = false;
            GroupDicesToRoll.Enabled = false;
            PicToDeckArrow.Visible = false;
            PicToStoArrow.Visible =false;
        }
    }
}
