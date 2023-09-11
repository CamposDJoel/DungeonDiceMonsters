//Joel Campos
//9/8/2023
//DeckBuilder Class

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class DeckBuilder : Form
    {
        public DeckBuilder()
        {
            SoundServer.PlayBackgroundMusic(Song.DeckBuildMenu, true);
            InitializeComponent();
         
            //Initialize the Deck List Selector
            listDeckList.Items.Clear();
            foreach (Deck deck in DecksData.Decks)
            {
                listDeckList.Items.Add(deck.Name);
            }
            _SkipForDeckSelectReload = true;
            listDeckList.SetSelected(0, true);
            _SkipForDeckSelectReload = false;
            _CurrentDeckSelected = DecksData.Decks[0];

            
            InitializeStorageComponents();
            InitializeDeckComponents();

            for (int x = 0; x < 30; x++)
            {
                _StorageIDsInCurrentPage[x] = 0;
            }
            for (int x = 0; x < 20; x++)
            {
                _DeckIDsInCurrentPage[x] = 0;
            }

            LoadStoragePage();
            LoadDeckPage();        
        }

        private void InitializeStorageComponents()
        {
            //Index will be save on the Image Object Tag value
            //so when it is clicked it knows which index card is being clicked.
            int pictureIndex = 0;

            int Y_Location = 2;
            for (int x = 0; x < 5; x++)
            {
                int X_Location = 2;
                for (int y = 0; y < 6; y++)
                {
                    //Initialize the border box Image
                    Panel CardBox = new Panel();
                    PanelStorage.Controls.Add(CardBox);
                    CardBox.Location = new Point(X_Location, Y_Location);
                    CardBox.BorderStyle = BorderStyle.FixedSingle;
                    CardBox.Size = new Size(58, 74);
                    _CardPanelList.Add(CardBox);

                    //Initialize the card Image
                    PictureBox CardImage = new PictureBox();
                    CardBox.Controls.Add(CardImage);
                    CardImage.Location = new Point(2, 2);
                    CardImage.BorderStyle = BorderStyle.FixedSingle;
                    CardImage.Size = new Size(52, 68);
                    CardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    _CardImageList.Add(CardImage);
                    CardImage.Tag = pictureIndex;
                    CardImage.Click += new EventHandler(StorageCard_click);


                    X_Location += 58;
                    pictureIndex++;
                }
                Y_Location += 73;
            }

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
        private void LoadStoragePage()
        {
            int startIndex = (_CurrentPage * 30) - 30;

            for (int x = 0; x < 30; x++)
            {
                //set the itearator
                int iterator = startIndex + x;
               
                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (iterator >= StorageData.Cards.Count)
                {
                    _CardPanelList[x].Visible = false;
                    _StorageIDsInCurrentPage[x] = 0;
                }
                else
                {
                    //if the ID of the card to be place in this card slot 
                    //is the same as the one before dont do anything...
                    if (_StorageIDsInCurrentPage[x] != StorageData.Cards[iterator])
                    {
                        //Update the ID of the card in this slow
                        _StorageIDsInCurrentPage[x] = StorageData.Cards[iterator];

                        //Make the card panel visible
                        _CardPanelList[x].Visible = true;

                        //Get the card ID of the card to be displayed
                        int cardID = StorageData.Cards[iterator];

                        //Dispose the current image in this picture box (if there was one)
                        //to clear memory
                        if (_CardImageList[x].Image != null) { _CardImageList[x].Image.Dispose(); }

                        //Populate the card image with the card ID
                        _CardImageList[x].Image = ImageServer.FullCardImage(cardID);
                    }                                     
                }
            }
        }
        private void LoadDeckPage()
        {
            for (int x = 0; x < 20; x++)
            {
                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (x >= _CurrentDeckSelected.MainDeckSize)
                {
                    _DeckCardPanelList[x].Visible = false;
                    _DeckIDsInCurrentPage[x] = 0;
                }
                else
                {
                    //if the ID of the card to be place in this card slot 
                    //is the same as the one before dont do anything...
                    if (_DeckIDsInCurrentPage[x] != _CurrentDeckSelected.GetMainCardIDAtIndex(x))
                    {
                        _DeckIDsInCurrentPage[x] = _CurrentDeckSelected.GetMainCardIDAtIndex(x);

                        //Make the card panel visible
                        _DeckCardPanelList[x].Visible = true;

                        //Get the card ID of the card to be displayed
                        int cardID = _CurrentDeckSelected.GetMainCardIDAtIndex(x);

                        //Dispose the current image in this picture box (if there was one)
                        //to clear memory
                        if (_DeckCardImageList[x].Image != null) { _DeckCardImageList[x].Image.Dispose(); }

                        //Populate the card image with the card ID
                        _DeckCardImageList[x].Image = ImageServer.FullCardImage(cardID);
                    }                    
                }
            }

            //Load the fusion deck
            for (int x = 0; x < 3; x++)
            {
                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (x >= _CurrentDeckSelected.FusionDeckSize)
                {
                    _DeckCardPanelList[x+20].Visible = false;
                }
                else
                {
                    //Make the card panel visible
                    _DeckCardPanelList[x+20].Visible = true;

                    //Get the card ID of the card to be displayed
                    int cardID = _CurrentDeckSelected.GetFusionCardIDAtIndex(x);

                    //Dispose the current image in this picture box (if there was one)
                    //to clear memory
                    if (_DeckCardImageList[x + 20].Image != null) { _DeckCardImageList[x + 20].Image.Dispose(); }

                    //Populate the card image with the card ID
                    _DeckCardImageList[x + 20].Image = ImageServer.FullCardImage(cardID);
                }
            }

            //Set the Ready flag
            if(PicDeckStatus.Image != null) { PicDeckStatus.Image.Dispose(); }
            PicDeckStatus.Image = ImageServer.DeckStatusIcon(_CurrentDeckSelected.UseStatus);
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
            if(thisCard.Category == "Spell") { secondaryType = " Spell"; }
            if(thisCard.Category == "Trap") { secondaryType = " Trap"; }


            lblCardType.Text = thisCard.Type + secondaryType;

            if (thisCard.Category == "Monster") { lblCardLevel.Text = "Lv. " + thisCard.Level; }
            else { lblCardLevel.Text = ""; }

            if (thisCard.Category == "Monster") { lblAttribute.Text =  thisCard.Attribute; }
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
        private void SetStorageSelector(int index)
        {
            //Clear all cards from being marked as selected
            for(int x = 0; x < 30; x ++) 
            {
                _CardPanelList[x].BackColor = Color.Transparent;
            }
            for (int x = 0; x < 23; x++)
            {
                _DeckCardPanelList[x].BackColor = Color.Transparent;
            }

            //Set the specific card selected
            _CardPanelList[index].BackColor = Color.Yellow;

            //Reload the Card Info Panel UI
            LoadCardInfoPanel();
        }
        private void SetDeckSelector(int index)
        {
            //Clear all cards from being marked as selected
            for (int x = 0; x < 30; x++)
            {
                _CardPanelList[x].BackColor = Color.Transparent;
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

        private int _CurrentPage = 1;
        private List<Panel> _CardPanelList = new List<Panel>();
        private List<PictureBox> _CardImageList = new List<PictureBox>();
        private List<Panel> _DeckCardPanelList = new List<Panel>();
        private List<PictureBox> _DeckCardImageList = new List<PictureBox>();
        private CardInfo _CurrentCardSelected = null;
        private Deck _CurrentDeckSelected = null;
        private int _CurrentStorageIndexSelected = 0;
        private int _CurrentDeckIndexSelected = 0;
        private bool _SkipForDeckSelectReload = false;

        private int[] _StorageIDsInCurrentPage = new int[30];
        private int[] _DeckIDsInCurrentPage = new int[30];


        private void StorageCard_click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click2);

            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);
        
            //Save a ref to the current card in view
            int startIndex = (_CurrentPage * 30) - 30;
            int indexInStorasge = startIndex + thiPictureBoxIndex;

            //Save the ref to this index number for outer use
            _CurrentStorageIndexSelected = indexInStorasge;

            int cardid = StorageData.Cards[indexInStorasge];
            _CurrentCardSelected = CardDataBase.GetCardWithID(cardid);

            SetStorageSelector(thiPictureBoxIndex);

            //Setup the transfer arrow buttons
            PicToStoArrow.Visible = false;
            PicToDeckArrow.Visible = false;

            if (_CurrentCardSelected.IsFusion)
            {
                if(_CurrentDeckSelected.FusionDeckSize < 3)                
                {
                    PicToDeckArrow.Visible = true;
                }
            }
            else
            {
                if (_CurrentDeckSelected.MainDeckSize < 20)
                {
                    //Also validate that the deck doesnt have 3 copies of that card already
                    int copies = _CurrentDeckSelected.GetCardCount(cardid);
                    if (copies < 3)
                    {
                        PicToDeckArrow.Visible = true;
                    }
                }
            }
        }
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
                cardid = _CurrentDeckSelected.GetFusionCardIDAtIndex(fusionIndex);
            }
            else
            {
                cardid = _CurrentDeckSelected.GetMainCardIDAtIndex(thiPictureBoxIndex);
            }
            _CurrentCardSelected = CardDataBase.GetCardWithID(cardid);

            SetDeckSelector(thiPictureBoxIndex);

            //Setup the transfer arrow buttons
            PicToStoArrow.Visible = true;
            PicToDeckArrow.Visible = false;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            _CurrentPage++;

            if(_CurrentPage == 6 ) { _CurrentPage = 1; }
            lblStorage.Text = "Storage Page: " + _CurrentPage;
            LoadStoragePage();
            SetStorageSelector(0);

            //Clear all cards from being marked as selected
            for (int x = 0; x < 30; x++)
            {
                _CardPanelList[x].BackColor = Color.Transparent;
            }
            for (int x = 0; x < 23; x++)
            {
                _DeckCardPanelList[x].BackColor = Color.Transparent;
            }

            //hide the arrow buttons
            PicToStoArrow.Visible = false;
            PicToDeckArrow.Visible = false;
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            _CurrentPage--;

            if (_CurrentPage == 0) { _CurrentPage = 5; }
            lblStorage.Text = "Storage Page: " + _CurrentPage;
            LoadStoragePage();
            SetStorageSelector(0);

            //Clear all cards from being marked as selected
            for (int x = 0; x < 30; x++)
            {
                _CardPanelList[x].BackColor = Color.Transparent;
            }
            for (int x = 0; x < 23; x++)
            {
                _DeckCardPanelList[x].BackColor = Color.Transparent;
            }

            //hide the arrow buttons
            PicToStoArrow.Visible = false;
            PicToDeckArrow.Visible = false;
        }       
        private void PicToDeckArrow_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.MoveCard);

            //add the card in the current storage index selected
            int cardid = _CurrentCardSelected.ID;

            if (_CurrentCardSelected.IsFusion)
            {
                _CurrentDeckSelected.AddFusionCard(cardid);
            }
            else
            {
                _CurrentDeckSelected.AddMainCard(cardid);
            }

            //Remove this one card from the storage
            StorageData.Cards.RemoveAt(_CurrentStorageIndexSelected);

            //Reload both sides
            LoadStoragePage();
            LoadDeckPage();

            //Hide both arrows until another selection is clicked
            PicToDeckArrow.Visible = false;
            PicToStoArrow.Visible = false;

            //Reload the Deck Status
            if (PicDeckStatus.Image != null) { PicDeckStatus.Image.Dispose(); }
            PicDeckStatus.Image = ImageServer.DeckStatusIcon(_CurrentDeckSelected.UseStatus);

            //Override the save file to save the changes
            SaveFileManger.WriteSaveFile();
        }
        private void PicToStoArrow_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.MoveCard);

            //add the card in the current deck index selected
            int cardid = _CurrentCardSelected.ID;
            StorageData.AddCard(cardid);

            //Remove this one card from the Deck
            if (_CurrentCardSelected.IsFusion)
            {
                int fusionIndex = _CurrentDeckIndexSelected - 20;
                _CurrentDeckSelected.RemoveFusionAtIndex(fusionIndex);
            }
            else
            {
                _CurrentDeckSelected.RemoveMainAtIndex(_CurrentDeckIndexSelected);
            }

            //Reload both sides
            LoadStoragePage();
            LoadDeckPage();

            //Hide both arrows until another selection is clicked
            PicToDeckArrow.Visible = false;
            PicToStoArrow.Visible = false;

            //Reload the Deck Status
            if (PicDeckStatus.Image != null) { PicDeckStatus.Image.Dispose(); }
            PicDeckStatus.Image = ImageServer.DeckStatusIcon(_CurrentDeckSelected.UseStatus);

            //Override the save file to save the changes
            SaveFileManger.WriteSaveFile();
        }
        private void listDeckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Change the current deck selected
            _CurrentDeckIndexSelected = listDeckList.SelectedIndex;
            _CurrentDeckSelected = DecksData.Decks[_CurrentDeckIndexSelected];

            if(!_SkipForDeckSelectReload)
            {
                //Reload both sides
                LoadDeckPage();

                //Hide both arrows until another selection is clicked
                PicToDeckArrow.Visible = false;
                PicToStoArrow.Visible = false;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            //Dispose the images of all cards
            for (int x = 0; x < 23; x++)
            {
                if (_DeckCardImageList[x].Image != null)
                {
                    _DeckCardImageList[x].Image.Dispose();
                }
            }

            for (int x = 0; x < 30; x++)
            {
                if (_CardImageList[x].Image != null) { _CardImageList[x].Image.Dispose(); }
            }

            SoundServer.PlayBackgroundMusic(Song.DeckBuildMenu, false);
            MainMenu MM = new MainMenu();
            Dispose();
            MM.Show();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}