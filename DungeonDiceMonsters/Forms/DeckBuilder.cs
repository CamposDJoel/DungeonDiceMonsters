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
        #region Constructors
        public DeckBuilder(Deck deck)
        {
            InitializeComponent();

            //Initialize the Deck List Selector
            _CurrentDeckSelected = deck;
            lblDeckName.Text = _CurrentDeckSelected.Name;
            lblStorage.Text = "Page: 1";
            InitializeStorageComponents();
            InitializeDeckComponents();
          
            for (int x = 0; x < CARDS_PER_PAGE; x++)
            {
                _StorageIDsInCurrentPage[x] = 0;
            }
            for (int x = 0; x < 20; x++)
            {
                _DeckIDsInCurrentPage[x] = 0;
            }

            btnExit.MouseEnter += OnMouseHover;
            btnNext.MouseEnter += OnMouseHover;
            btnPrevious.MouseEnter += OnMouseHover;
            btnSymbolNext.MouseEnter += OnMouseHover;
            btnSymbolPrevious.MouseEnter += OnMouseHover;
            btnFilterAqua.MouseEnter += OnMouseHover;
            btnFilterBeast.MouseEnter += OnMouseHover;
            btnFilterBeastWarrior.MouseEnter += OnMouseHover;
            btnFilterCyberce.MouseEnter += OnMouseHover;
            btnFilterDinosaur.MouseEnter += OnMouseHover;
            btnFilterDivine.MouseEnter += OnMouseHover;
            btnFilterDragon.MouseEnter += OnMouseHover;
            btnFilterFairy.MouseEnter += OnMouseHover;
            btnFilterFiend.MouseEnter += OnMouseHover;
            btnFilterFish.MouseEnter += OnMouseHover;
            btnFilterIllusion.MouseEnter += OnMouseHover;
            btnFilterInsect.MouseEnter += OnMouseHover;
            btnFilterMachine.MouseEnter += OnMouseHover;
            btnFilterPlant.MouseEnter += OnMouseHover;
            btnFilterPsychic.MouseEnter += OnMouseHover;
            btnFilterPyro.MouseEnter += OnMouseHover;
            btnFilterReptile.MouseEnter += OnMouseHover;
            btnFilterRock.MouseEnter += OnMouseHover;
            btnFilterSeaSerpent.MouseEnter += OnMouseHover;
            btnFilterSpellcaster.MouseEnter += OnMouseHover;
            btnFilterThunder.MouseEnter += OnMouseHover;
            btnFilterWarrior.MouseEnter += OnMouseHover;
            btnFilterWingedBeast.MouseEnter += OnMouseHover;
            btnFilterWyrm.MouseEnter += OnMouseHover;
            btnFilterZombie.MouseEnter += OnMouseHover;
            btnNormalSpell.MouseEnter += OnMouseHover;
            btnContinousSpell.MouseEnter += OnMouseHover;
            btnEquipSpell.MouseEnter += OnMouseHover;
            btnFieldSpell.MouseEnter += OnMouseHover;
            btnRitualSpell.MouseEnter += OnMouseHover;
            btnNormalTrap.MouseEnter += OnMouseHover;
            btnContinuosTrap.MouseEnter += OnMouseHover;
            btnMonster.MouseEnter += OnMouseHover;
            btnSpells.MouseEnter += OnMouseHover;
            btnTraps.MouseEnter += OnMouseHover;
            btnNormal.MouseEnter += OnMouseHover;
            btnFusion.MouseEnter += OnMouseHover;
            btnRitual.MouseEnter += OnMouseHover;
            btnDark.MouseEnter += OnMouseHover;
            btnLight.MouseEnter += OnMouseHover;
            btnEarth.MouseEnter += OnMouseHover;
            btnWater.MouseEnter += OnMouseHover;
            btnFire.MouseEnter += OnMouseHover;
            btnWind.MouseEnter += OnMouseHover;
            btnDivine.MouseEnter += OnMouseHover;
            btnTextSearch.MouseEnter += OnMouseHover;
            btnSortByCardNo.MouseEnter += OnMouseHover;
            btnSortByName.MouseEnter += OnMouseHover;
            btnSortByMonsterLv.MouseEnter += OnMouseHover;
            btnSortByDiceLv.MouseEnter += OnMouseHover;
            btnSortByLP.MouseEnter += OnMouseHover;
            btnSortByATK.MouseEnter += OnMouseHover;
            btnSortByDEF.MouseEnter += OnMouseHover;
            btnMov.MouseEnter += OnMouseHover;
            btnATK.MouseEnter += OnMouseHover;
            btnDEF.MouseEnter += OnMouseHover;
            btnMAG.MouseEnter += OnMouseHover;
            btnTrap.MouseEnter += OnMouseHover;

            //Initialize the Storage List
            _CurrentStorageList = StorageData.GetCardList();
            ReloadStorageCardList();
            LoadDeckPage();        
        }
        #endregion

        #region Private Methods
        private void InitializeStorageComponents()
        {
            //Index will be save on the Image Object Tag value
            //so when it is clicked it knows which index card is being clicked.
            int pictureIndex = 0;

            int Y_Location = 2;
            for (int x = 0; x < 5; x++)
            {
                int X_Location = 2;
                for (int y = 0; y < 7; y++)
                {
                    //Initialize the border box Image
                    Panel CardBox = new Panel();
                    PanelStorage.Controls.Add(CardBox);
                    CardBox.Location = new Point(X_Location, Y_Location);
                    CardBox.BorderStyle = BorderStyle.FixedSingle;
                    CardBox.Size = new Size(58, 74);
                    _CardPanelList.Add(CardBox);

                    //Initialize the DiceLvIcon
                    PictureBox DiceLevelIcon = new PictureBox();
                    CardBox.Controls.Add(DiceLevelIcon);
                    DiceLevelIcon.Location = new Point(2, 45);
                    DiceLevelIcon.Size = new Size(25, 25);
                    DiceLevelIcon.BorderStyle = BorderStyle.FixedSingle;
                    DiceLevelIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                    _StorageDiceLevelIconImageList.Add(DiceLevelIcon);

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
                    CardImage.MouseEnter += new EventHandler(OnMouseEnterStorageCard);

                    //Initialize the Amount Label
                    Label amountLabel = new Label();
                    CardBox.Controls.Add(amountLabel);
                    amountLabel.Location = new Point(2, 2);
                    amountLabel.BorderStyle = BorderStyle.FixedSingle;
                    amountLabel.Size = new Size(30, 15);
                    amountLabel.BackColor = Color.Black;
                    amountLabel.ForeColor = Color.White;
                    _CardAmountList.Add(amountLabel);
                    amountLabel.BringToFront();

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

                    //Initialize the DiceLvIcon
                    PictureBox DiceLevelIcon = new PictureBox();
                    CardBox.Controls.Add(DiceLevelIcon);
                    DiceLevelIcon.Location = new Point(2, 45);
                    DiceLevelIcon.Size = new Size(25, 25);
                    DiceLevelIcon.BorderStyle = BorderStyle.FixedSingle;
                    DiceLevelIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                    _DeckDiceLevelIconImageList.Add(DiceLevelIcon);

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
                    CardImage.MouseEnter += new EventHandler(OnMouseEnterDeckCard);

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

                //Initialize the DiceLvIcon
                PictureBox DiceLevelIcon = new PictureBox();
                CardBox.Controls.Add(DiceLevelIcon);
                DiceLevelIcon.Location = new Point(2, 45);
                DiceLevelIcon.Size = new Size(25, 25);
                DiceLevelIcon.BorderStyle = BorderStyle.FixedSingle;
                DiceLevelIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                _DeckDiceLevelIconImageList.Add(DiceLevelIcon);

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
                CardImage.MouseEnter += new EventHandler(OnMouseEnterDeckCard);

                X_Location2 += 58;
                pictureIndex++;
            }
        }
        private void LoadStoragePage()
        {
            int startIndex = (_CurrentPage * CARDS_PER_PAGE) - CARDS_PER_PAGE;

            for (int x = 0; x < CARDS_PER_PAGE; x++)
            {
                //set the itearator
                int iterator = startIndex + x;
               
                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (iterator >= _CurrentStorageList.Count)
                {
                    _CardPanelList[x].Visible = false;
                    _StorageIDsInCurrentPage[x] = 0;
                }
                else
                {
                    if (_StorageIDsInCurrentPage[x] != _CurrentStorageList[iterator].ID)
                    {
                        _StorageIDsInCurrentPage[x] = _CurrentStorageList[iterator].ID;

                        //Update the ID of the card in this slow
                        _StorageIDsInCurrentPage[x] = _CurrentStorageList[iterator].ID;

                        //Make the card panel visible
                        _CardPanelList[x].Visible = true;

                        //Get the card ID of the card to be displayed
                        int cardID = _CurrentStorageList[iterator].ID;
                        CardInfo thisCardIfo = CardDataBase.GetCardWithID(cardID);

                        //Populate the card image with the card ID
                        ImageServer.ClearImage(_CardImageList[x]);
                        _CardImageList[x].Image = ImageServer.FullCardImage(cardID.ToString());

                        //Load the dice level icon
                        if (thisCardIfo.SecType == SecType.Ritual)
                        {
                            ImageServer.ClearImage(_StorageDiceLevelIconImageList[x]);
                            _StorageDiceLevelIconImageList[x].Image = ImageServer.DiceFace(thisCardIfo.DiceLevel, "RITU", thisCardIfo.DiceLevel);
                        }
                        else
                        {
                            ImageServer.ClearImage(_StorageDiceLevelIconImageList[x]);
                            _StorageDiceLevelIconImageList[x].Image = ImageServer.DiceFace(thisCardIfo.DiceLevel, "STAR", thisCardIfo.DiceLevel);
                        }                       
                    }

                    //Update the amount label
                    _CardAmountList[x].Text = string.Format("x{0}", _CurrentStorageList[iterator].Amount.ToString());
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
                        CardInfo thisCardIfo = CardDataBase.GetCardWithID(cardID);

                        //Populate the card image with the card ID
                        ImageServer.ClearImage(_DeckCardImageList[x]);
                        _DeckCardImageList[x].Image = ImageServer.FullCardImage(cardID.ToString());

                        //Load the dice level icon
                        if (thisCardIfo.SecType == SecType.Ritual)
                        {
                            ImageServer.ClearImage(_DeckDiceLevelIconImageList[x]);
                            _DeckDiceLevelIconImageList[x].Image = ImageServer.DiceFace(thisCardIfo.DiceLevel, "RITU", thisCardIfo.DiceLevel);
                        }
                        else
                        {
                            ImageServer.ClearImage(_DeckDiceLevelIconImageList[x]);
                            _DeckDiceLevelIconImageList[x].Image = ImageServer.DiceFace(thisCardIfo.DiceLevel, "STAR", thisCardIfo.DiceLevel);
                        }
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
                    CardInfo thisCardIfo = CardDataBase.GetCardWithID(cardID);

                    //Populate the card image with the card ID
                    ImageServer.ClearImage(_DeckCardImageList[x + 20]);
                    _DeckCardImageList[x + 20].Image = ImageServer.FullCardImage(cardID.ToString());

                    //Load the dice level icon
                    if (thisCardIfo.SecType == SecType.Ritual)
                    {
                        ImageServer.ClearImage(_DeckDiceLevelIconImageList[x + 20]);
                        _DeckDiceLevelIconImageList[x + 20].Image = ImageServer.DiceFace(thisCardIfo.DiceLevel, "RITU", thisCardIfo.DiceLevel);
                    }
                    else
                    {
                        ImageServer.ClearImage(_DeckDiceLevelIconImageList[x + 20]);
                        _DeckDiceLevelIconImageList[x + 20].Image = ImageServer.DiceFace(thisCardIfo.DiceLevel, "STAR", thisCardIfo.DiceLevel);
                    }
                }
            }

            //Update the Symbol
            _CurrentSymbolSelection = _CurrentDeckSelected.Symbol;
            ImageServer.ClearImage(PicSymbol);
            PicSymbol.Image = ImageServer.Symbol(_CurrentSymbolSelection.ToString());
            lblSymbolName.Text = _CurrentSymbolSelection.ToString();
            switch (_CurrentSymbolSelection)
            {
                case Attribute.DARK: lblSymbolName.ForeColor = Color.Purple; break;
                case Attribute.LIGHT: lblSymbolName.ForeColor = Color.LightCyan; break;
                case Attribute.WATER: lblSymbolName.ForeColor = Color.RoyalBlue; break;
                case Attribute.FIRE: lblSymbolName.ForeColor = Color.Crimson; break;
                case Attribute.EARTH: lblSymbolName.ForeColor = Color.Orange; break;
                case Attribute.WIND: lblSymbolName.ForeColor = Color.PaleGreen; break;
            }
        }
        private void LoadCardInfoPanel()
        {
            //Get the CardInfo object to populate the UI
            //remember _currentCardSelected ref is set each time you click a card in the UI.
            CardInfo thisCard = _CurrentCardSelected;
            int cardID = thisCard.ID;

            //Populate the UI
            ImageServer.ClearImage(PicCardArtwork);
            PicCardArtwork.Image = ImageServer.CardArtworkImage(cardID.ToString());

            lblID.Text = "ID: " + cardID.ToString();
            lblCardName.Text = thisCard.Name;

            //Card Number
            lblCardNumber.Text = "No. " + thisCard.CardNumber.ToString();

            string secondaryType = thisCard.SecType.ToString();
            lblCardType.Text = thisCard.TypeAsString + "/" + secondaryType;
            if (thisCard.Category == Category.Spell) { lblCardType.Text = thisCard.TypeAsString + " spell"; }
            if (thisCard.Category == Category.Trap) { lblCardType.Text = thisCard.TypeAsString + " trap"; }

            if (thisCard.Category == Category.Monster) { lblCardLevel.Text = "Monster LV. " + thisCard.Level; }
            else { lblCardLevel.Text = ""; }

            ImageServer.ClearImage(PicCardAttribute);
            ImageServer.ClearImage(PicCardMonsterType);
            PicCardAttribute.Image = ImageServer.AttributeIcon(thisCard.Attribute);
            PicCardMonsterType.Image = ImageServer.MonsterTypeIcon(thisCard.TypeAsString);

            lblDiceLevel.Text = "Dice LV. " + thisCard.DiceLevel;

            if (thisCard.Category == Category.Monster)
            {
                lblStats.Text = "ATK " + thisCard.ATK + " / DEF " + thisCard.DEF + " / LP " + thisCard.LP;
            }
            else { lblStats.Text = ""; }

            string fullcardtext = "";
            if(thisCard.SecType == SecType.Fusion) 
            {               
                string fusionMaterials = thisCard.FusionMaterial1 + " + " + thisCard.FusionMaterial2;
                if (thisCard.FusionMaterial3 != "-") { fusionMaterials = fusionMaterials + " + " + thisCard.FusionMaterial3; }
                fullcardtext = fullcardtext + fusionMaterials + "\n\n";
            }

            if(thisCard.HasOnSummonEffect)
            {
                fullcardtext = fullcardtext + "(ON SUMMON) - " + thisCard.OnSummonEffect + "\n\n";
            }

            if (thisCard.HasAbility)
            {
                fullcardtext = fullcardtext + "(ABILITY) - " + thisCard.Ability + "\n\n";
            }

            if (thisCard.HasContinuousEffect)
            {
                fullcardtext = fullcardtext + "(CONTINUOUS) - " + thisCard.ContinuousEffect + "\n\n";
            }
         

            if (thisCard.HasIgnitionEffect)
            {
                fullcardtext = fullcardtext + "(EFFECT)" + thisCard.IgnitionEffect + "\n\n";
            }

            if(thisCard.HasTriggerEffect)
            {
                fullcardtext = fullcardtext + "(TRIGGER)" + thisCard.TriggerEffect + "\n\n";
            }

            lblCardText.Text = fullcardtext;

            //Dice Faces
            ImageServer.ClearImage(PicDiceFace1);
            ImageServer.ClearImage(PicDiceFace2);
            ImageServer.ClearImage(PicDiceFace3);
            ImageServer.ClearImage(PicDiceFace4);
            ImageServer.ClearImage(PicDiceFace5);
            ImageServer.ClearImage(PicDiceFace6);
            PicDiceFace1.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(0).ToString(), thisCard.DiceFaceValue(0));
            PicDiceFace2.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(1).ToString(), thisCard.DiceFaceValue(1));
            PicDiceFace3.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(2).ToString(), thisCard.DiceFaceValue(2));
            PicDiceFace4.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(3).ToString(), thisCard.DiceFaceValue(3));
            PicDiceFace5.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(4).ToString(), thisCard.DiceFaceValue(4));
            PicDiceFace6.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(5).ToString(), thisCard.DiceFaceValue(5));
        }
        private void SetStorageSelector(int index)
        {
            //Clear all cards from being marked as selected
            for(int x = 0; x < CARDS_PER_PAGE; x ++) 
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
        private int GetLastPage()
        {
            int CurrentCardListCount = _CurrentStorageList.Count;
            double pages = ((double)CurrentCardListCount / (double)CARDS_PER_PAGE);
            int lastpage = (int)pages;
            double remaining = pages - (int)pages;
            if (remaining > 0) { lastpage++; }
            return lastpage;
        }
        private void ReloadStorageCardList()
        {
            _CurrentPage = 1;
            lblStorage.Text = "Page: " + _CurrentPage;
            lblStorageCardCount.Text = "Cards: " + _CurrentStorageList.Count.ToString();
            LoadStoragePage();
        }
        #endregion

        #region Data
        private int _CurrentPage = 1;
        private List<Panel> _CardPanelList = new List<Panel>();
        private List<PictureBox> _CardImageList = new List<PictureBox>();
        private List<Panel> _DeckCardPanelList = new List<Panel>();
        private List<PictureBox> _DeckCardImageList = new List<PictureBox>();
        private List<PictureBox> _DeckDiceLevelIconImageList = new List<PictureBox>();
        private List<PictureBox> _StorageDiceLevelIconImageList = new List<PictureBox>();
        private List<Label> _CardAmountList = new List<Label>();
        private CardInfo _CurrentCardSelected = null;
        private Deck _CurrentDeckSelected = null;
        private int _CurrentDeckIndexSelected = 0;
        private const int CARDS_PER_PAGE = 35;

        private List<StorageSlot> _CurrentStorageList = new List<StorageSlot>();

        private int[] _StorageIDsInCurrentPage = new int[CARDS_PER_PAGE];
        private int[] _DeckIDsInCurrentPage = new int[CARDS_PER_PAGE];       

        private Attribute _CurrentSymbolSelection = Attribute.DARK;
        #endregion

        #region Events
        private void OnMouseEnterDeckCard(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);

            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);
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
        }
        private void OnMouseEnterStorageCard(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);

            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            //Save a ref to the current card in view
            int startIndex = (_CurrentPage * CARDS_PER_PAGE) - CARDS_PER_PAGE;
            int indexInStorasge = startIndex + thiPictureBoxIndex;

            int cardid = _CurrentStorageList[indexInStorasge].ID;
            _CurrentCardSelected = CardDataBase.GetCardWithID(cardid);

            SetStorageSelector(thiPictureBoxIndex);
        }
        private void OnMouseHover(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
        }
        private void StorageCard_click(object sender, EventArgs e)
        {
            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            //Save a ref to the current card in view
            int startIndex = (_CurrentPage * CARDS_PER_PAGE) - CARDS_PER_PAGE;
            int indexInStorasge = startIndex + thiPictureBoxIndex;

            int cardid = _CurrentStorageList[indexInStorasge].ID;
            _CurrentCardSelected = CardDataBase.GetCardWithID(cardid);

            bool CanBeMoved = false;
            if (_CurrentCardSelected.IsFusion)
            {
                if (_CurrentDeckSelected.FusionDeckSize < 3)
                {
                    CanBeMoved = true;
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
                        CanBeMoved = true;
                    }
                }
            }

            if(CanBeMoved)
            {
                SoundServer.PlaySoundEffect(SoundEffect.CardToDeck);

                if (_CurrentCardSelected.IsFusion)
                {
                    _CurrentDeckSelected.AddFusionCard(cardid);
                }
                else
                {
                    _CurrentDeckSelected.AddMainCard(cardid);
                }

                //Reduce this card amount's by 1, if the amount reached 0, it will be removed the list all together
                StorageData.RemoveCard(cardid);

                //Reload both sides
                LoadStoragePage();
                LoadDeckPage();
                SaveFileManger.WriteSaveFile();
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
            }
        }
        private void DeckCard_click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.CardToStorage);

            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);
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


            //add the card to the storage
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
            SaveFileManger.WriteSaveFile();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            int lastpage = GetLastPage();

            if (lastpage != 0)
            {
                if (_CurrentPage == lastpage) { _CurrentPage = 1; }
                else { _CurrentPage++; }

                lblStorage.Text = "Page: " + _CurrentPage;
                LoadStoragePage();
                SetStorageSelector(0);

                //Clear all cards from being marked as selected
                for (int x = 0; x < CARDS_PER_PAGE; x++)
                {
                    _CardPanelList[x].BackColor = Color.Transparent;
                }
                for (int x = 0; x < 23; x++)
                {
                    _DeckCardPanelList[x].BackColor = Color.Transparent;
                }
            }
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            int lastpage = GetLastPage();
            if (lastpage != 0)
            {
                if (_CurrentPage == 1) { _CurrentPage = lastpage; }
                else { _CurrentPage--; }

                lblStorage.Text = "Page: " + _CurrentPage;
                LoadStoragePage();
                SetStorageSelector(0);

                //Clear all cards from being marked as selected
                for (int x = 0; x < CARDS_PER_PAGE; x++)
                {
                    _CardPanelList[x].BackColor = Color.Transparent;
                }
                for (int x = 0; x < 23; x++)
                {
                    _DeckCardPanelList[x].BackColor = Color.Transparent;
                }
            }
        }       
        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            DecksManager DM = new DecksManager(false);
            Dispose();
            DM.Show();
        }
        private void btnSymbolPrevious_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentSymbolSelection)
            {
                case Attribute.DARK: _CurrentSymbolSelection = Attribute.WIND; break;
                case Attribute.LIGHT: _CurrentSymbolSelection = Attribute.DARK; break;
                case Attribute.WATER: _CurrentSymbolSelection = Attribute.LIGHT; break;
                case Attribute.FIRE: _CurrentSymbolSelection = Attribute.WATER; break;
                case Attribute.EARTH: _CurrentSymbolSelection = Attribute.FIRE; break;
                case Attribute.WIND: _CurrentSymbolSelection = Attribute.EARTH; break;
                default:
                    throw new Exception("Changing Symbol on the DeckBuilder: CurrentSymbolSelection is " + _CurrentSymbolSelection + " which is an invalid value.");
            }

            lblSymbolName.Text = _CurrentSymbolSelection.ToString();

            switch (_CurrentSymbolSelection)
            {
                case Attribute.DARK: lblSymbolName.ForeColor = Color.Purple; break;
                case Attribute.LIGHT: lblSymbolName.ForeColor = Color.LightCyan; break;
                case Attribute.WATER: lblSymbolName.ForeColor = Color.RoyalBlue; break;
                case Attribute.FIRE: lblSymbolName.ForeColor = Color.Crimson; break;
                case Attribute.EARTH: lblSymbolName.ForeColor = Color.Orange; break;
                case Attribute.WIND: lblSymbolName.ForeColor = Color.PaleGreen; break;
            }

            //Update the image
            ImageServer.ClearImage(PicSymbol);
            PicSymbol.Image = ImageServer.Symbol(_CurrentSymbolSelection.ToString());
            _CurrentDeckSelected.ChangeSymbol(_CurrentSymbolSelection);
            SaveFileManger.WriteSaveFile();
        }
        private void btnSymbolNext_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentSymbolSelection)
            {
                case Attribute.DARK: _CurrentSymbolSelection = Attribute.LIGHT; break;
                case Attribute.LIGHT: _CurrentSymbolSelection = Attribute.WATER; break;
                case Attribute.WATER: _CurrentSymbolSelection = Attribute.FIRE; break;
                case Attribute.FIRE: _CurrentSymbolSelection = Attribute.EARTH; break;
                case Attribute.EARTH: _CurrentSymbolSelection = Attribute.WIND; break;
                case Attribute.WIND: _CurrentSymbolSelection = Attribute.DARK; break;
                default:
                    throw new Exception("Changing Symbol on the DeckBuilder: CurrentSymbolSelection is " + _CurrentSymbolSelection + " which is an invalid value.");
            }

            lblSymbolName.Text = _CurrentSymbolSelection.ToString();

            switch (_CurrentSymbolSelection)
            {
                case Attribute.DARK: lblSymbolName.ForeColor = Color.Purple; break;
                case Attribute.LIGHT: lblSymbolName.ForeColor = Color.LightCyan; break;
                case Attribute.WATER: lblSymbolName.ForeColor = Color.RoyalBlue; break;
                case Attribute.FIRE: lblSymbolName.ForeColor = Color.Crimson; break;
                case Attribute.EARTH: lblSymbolName.ForeColor = Color.Orange; break;
                case Attribute.WIND: lblSymbolName.ForeColor = Color.PaleGreen; break;
            }

            //Update the image
            ImageServer.ClearImage(PicSymbol);
            PicSymbol.Image = ImageServer.Symbol(_CurrentSymbolSelection.ToString());
            _CurrentDeckSelected.ChangeSymbol(_CurrentSymbolSelection);
            SaveFileManger.WriteSaveFile();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Filters/Sorting
        private void FilterByMonsterType(Type type)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList = StorageData.FilterByType(type);
            ReloadStorageCardList();
            btnClear.Visible = true;
        }
        private void btnFilterAqua_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Aqua);
        }
        private void btnFilterBeast_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Beast);
        }
        private void btnFilterBeastWarrior_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.BeastWarrior);
        }
        private void btnFilterCyberce_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Cyberse);
        }
        private void btnFilterDinosaur_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Dinosaur);
        }
        private void btnFilterDivine_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.DivineBeast);
        }
        private void btnFilterDragon_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Dragon);
        }
        private void btnFilterFairy_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Fairy);
        }
        private void btnFilterFiend_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Fiend);
        }
        private void btnFilterFish_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Fish);
        }
        private void btnFilterIllusion_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Illusion);
        }
        private void btnFilterInsect_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Insect);
        }
        private void btnFilterMachine_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Machine);
        }
        private void btnFilterPlant_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Plant);
        }
        private void btnFilterPsychic_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Psychic);
        }
        private void btnFilterPyro_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Pyro);
        }
        private void btnFilterReptile_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Reptile);
        }
        private void btnFilterRock_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Rock);
        }
        private void btnFilterSeaSerpent_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.SeaSerpent);
        }
        private void btnFilterSpellcaster_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Spellcaster);
        }
        private void btnFilterThunder_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Thunder);
        }
        private void btnFilterWarrior_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Warrior);
        }
        private void btnFilterWingedBeast_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.WingedBeast);
        }
        private void btnFilterWyrm_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Wyrm);
        }
        private void btnFilterZombie_Click(object sender, EventArgs e)
        {
            FilterByMonsterType(Type.Zombie);
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.CardDestroyed);
            _CurrentStorageList = StorageData.GetCardList();
            ReloadStorageCardList();
            btnClear.Visible = false;
        }
        private void FilterBySpellType(Type type)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList = StorageData.FilterBySpellType(type);
            ReloadStorageCardList();
            btnClear.Visible = true;
        }
        private void btnNormalSpell_Click(object sender, EventArgs e)
        {
            FilterBySpellType(Type.Normal);
        }
        private void btnContinousSpell_Click(object sender, EventArgs e)
        {
            FilterBySpellType(Type.Continuous);
        }
        private void btnEquipSpell_Click(object sender, EventArgs e)
        {
            FilterBySpellType(Type.Equip);
        }
        private void btnFieldSpell_Click(object sender, EventArgs e)
        {
            FilterBySpellType(Type.Field);
        }
        private void btnRitualSpell_Click(object sender, EventArgs e)
        {
            FilterBySpellType(Type.Ritual);
        }
        private void FilterByTrapType(Type type)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList = StorageData.FilterByTrapType(type);
            ReloadStorageCardList();
            btnClear.Visible = true;
        }
        private void btnNormalTrap_Click(object sender, EventArgs e)
        {
            FilterByTrapType(Type.Normal);
        }
        private void btnContinuosTrap_Click(object sender, EventArgs e)
        {
            FilterByTrapType(Type.Continuous);
        }
        private void FilterByCardType(Category category)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList = StorageData.FilterByCardType(category);
            ReloadStorageCardList();
            btnClear.Visible = true;
        }
        private void btnMonster_Click(object sender, EventArgs e)
        {
            FilterByCardType(Category.Monster);
        }
        private void btnSpells_Click(object sender, EventArgs e)
        {
            FilterByCardType(Category.Spell);
        }
        private void btnTraps_Click(object sender, EventArgs e)
        {
            FilterByCardType(Category.Trap);
        }
        private void FilterByMonsterSecType(SecType type)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList = StorageData.FilterByMonsterSecType(type);
            ReloadStorageCardList();
            btnClear.Visible = true;
        }
        private void btnNormal_Click(object sender, EventArgs e)
        {
            FilterByMonsterSecType(SecType.Normal);
        }
        private void btnFusion_Click(object sender, EventArgs e)
        {
            FilterByMonsterSecType(SecType.Fusion);
        }
        private void btnRitual_Click(object sender, EventArgs e)
        {
            FilterByMonsterSecType(SecType.Ritual);
        }
        private void FilterByAttribute(Attribute attribute)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList = StorageData.FilterByAttribute(attribute);
            ReloadStorageCardList();
            btnClear.Visible = true;
        }
        private void btnDark_Click(object sender, EventArgs e)
        {
            FilterByAttribute(Attribute.DARK);
        }
        private void btnLight_Click(object sender, EventArgs e)
        {
            FilterByAttribute(Attribute.LIGHT);
        }
        private void btnEarth_Click(object sender, EventArgs e)
        {
            FilterByAttribute(Attribute.EARTH);
        }
        private void btnWater_Click(object sender, EventArgs e)
        {
            FilterByAttribute(Attribute.WATER);
        }
        private void btnFire_Click(object sender, EventArgs e)
        {
            FilterByAttribute(Attribute.FIRE);
        }
        private void btnWind_Click(object sender, EventArgs e)
        {
            FilterByAttribute(Attribute.WIND);
        }
        private void btnDivine_Click(object sender, EventArgs e)
        {
            FilterByAttribute(Attribute.DIVINE);
        }
        private void FilterByText(string searchTerm)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList = StorageData.FilterByText(searchTerm);
            ReloadStorageCardList();
            btnClear.Visible = true;
        }
        private void btnTextSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text;
            if (!string.IsNullOrEmpty(searchTerm)) 
            {
                FilterByText(searchTerm);
            }
        }
        private void btnSortByCardNo_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList.Sort(new StorageSlot.SortByCardNumber());
            ReloadStorageCardList();
        }
        private void btnSortByName_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList.Sort(new StorageSlot.SortByCardName());
            ReloadStorageCardList();
        }
        private void btnSortByMonsterLv_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList.Sort(new StorageSlot.SortByMonsterLV());
            ReloadStorageCardList();
        }
        private void btnSortByDiceLv_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList.Sort(new StorageSlot.SortByDiceLV());
            ReloadStorageCardList();
        }
        private void btnSortByLP_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList.Sort(new StorageSlot.SortByLP());
            ReloadStorageCardList();
        }
        private void btnSortByATK_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList.Sort(new StorageSlot.SortByATK());
            ReloadStorageCardList();
        }
        private void btnSortByDEF_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList.Sort(new StorageSlot.SortByDEF());
            ReloadStorageCardList();
        }
        private void FilterByCrest(Crest crest)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            _CurrentStorageList = StorageData.FilterByCrest(crest);
            ReloadStorageCardList();
            btnClear.Visible = true;
        }
        private void btnMov_Click(object sender, EventArgs e)
        {
            FilterByCrest(Crest.MOV);
        }
        private void btnATK_Click(object sender, EventArgs e)
        {
            FilterByCrest(Crest.ATK);
        }
        private void btnDEF_Click(object sender, EventArgs e)
        {
            FilterByCrest(Crest.DEF);
        }
        private void btnMAG_Click(object sender, EventArgs e)
        {
            FilterByCrest(Crest.MAG);
        }
        private void btnTrap_Click(object sender, EventArgs e)
        {
            FilterByCrest(Crest.TRAP);
        }
        #endregion
    }
}