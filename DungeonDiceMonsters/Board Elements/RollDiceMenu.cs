﻿//Joel Campos
//9/21/2023
//RollDiceMenu Class

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class RollDiceMenu : Form
    {
        #region Constructors
        public RollDiceMenu(PlayerData playerdata, BoardForm board)
        {
            InitializeComponent();
            _PlayerData = playerdata;
            _Board = board;

            PicDice1.Click += RollCard_click;
            PicDice2.Click += RollCard_click;
            PicDice3.Click += RollCard_click;
            PicDice1.MouseEnter += OnMouseEnterRollCard;
            PicDice2.MouseEnter += OnMouseEnterRollCard;
            PicDice3.MouseEnter += OnMouseEnterRollCard;

            btnDice1Summon.MouseEnter += OnMouseEnterPicture;
            btnDice2Summon.MouseEnter += OnMouseEnterPicture;
            btnDice3Summon.MouseEnter += OnMouseEnterPicture;
            btnDice1Ritual.MouseEnter += OnMouseEnterPicture;
            btnDice2Ritual.MouseEnter += OnMouseEnterPicture;
            btnDice3Ritual.MouseEnter += OnMouseEnterPicture;
            btnDice1Set.MouseEnter += OnMouseEnterPicture;
            btnDice2Set.MouseEnter += OnMouseEnterPicture;
            btnDice3Set.MouseEnter += OnMouseEnterPicture;

            btnRoll.MouseEnter += OnMouseEnterPicture;
            btnGoToBoard.MouseEnter += OnMouseEnterPicture;

            InitializeDeckComponents();
            LoadDeckPage();
            GenrateValidDimension();            
        }
        #endregion

        #region Private Methods
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
                    CardImage.MouseEnter += OnMouseEnterDeckCard;

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
                CardImage.MouseEnter += OnMouseEnterDeckCard;

                X_Location2 += 58;
                pictureIndex++;
            }
        }
        private void LoadDeckPage()
        {
            //Load the Crest Pool counter
            lblMOVCount.Text = _PlayerData.Crests_MOV.ToString();
            lblATKCount.Text = _PlayerData.Crests_ATK.ToString();
            lblDEFCount.Text = _PlayerData.Crests_DEF.ToString();
            lblMAGCount.Text = _PlayerData.Crests_MAG.ToString();
            lblTRAPCount.Text = _PlayerData.Crests_TRAP.ToString();

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

            for (int x = 0; x < 23; x++)
            {
                _DeckCardPanelList[x].BackColor = Color.Transparent;
            }

            //Clear the dices selected to roll
            PanelDice1.BackColor = Color.Transparent;
            PanelDice2.BackColor = Color.Transparent;
            PanelDice3.BackColor = Color.Transparent;

            //Set the specific card selected
            _DeckCardPanelList[index].BackColor = Color.Yellow;
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
                    PanelDice1.BackColor = Color.Yellow;
                    PanelDice2.BackColor = Color.Transparent;
                    PanelDice3.BackColor = Color.Transparent;
                    break;
                case 1:
                    PanelDice1.BackColor = Color.Transparent;
                    PanelDice2.BackColor = Color.Yellow;
                    PanelDice3.BackColor = Color.Transparent;
                    break;
                case 2:
                    PanelDice1.BackColor = Color.Transparent;
                    PanelDice2.BackColor = Color.Transparent;
                    PanelDice3.BackColor = Color.Yellow;
                    break;
            }    
        }
        private void LoadCardInfoPanel(CardInfo thisCard)
        {
            int cardID = thisCard.ID;

            //Populate the UI
            if (PicCardArtwork.Image != null) { PicCardArtwork.Image.Dispose(); }
            PicCardArtwork.Image = ImageServer.CardArtworkImage(cardID);

            lblID.Text = cardID.ToString();
            lblCardName.Text = thisCard.Name;

            string secondaryType = thisCard.SecType.ToString();
            lblCardType.Text = thisCard.TypeAsString + "/" + secondaryType;
            if (thisCard.Category == Category.Spell) { lblCardType.Text = thisCard.TypeAsString + " spell"; }
            if (thisCard.Category == Category.Trap) { lblCardType.Text = thisCard.TypeAsString + " trap"; }

            if (thisCard.Category == Category.Monster) { lblCardLevel.Text = "Card Lv. " + thisCard.Level; }
            else { lblCardLevel.Text = ""; }

            if (thisCard.Category == Category.Monster) { lblAttribute.Text = thisCard.Attribute.ToString(); }
            else { lblAttribute.Text = ""; }

            lblDiceLevel.Text = "Dice Lv. " + thisCard.DiceLevel;

            if (thisCard.Category == Category.Monster)
            {
                lblStats.Text = "ATK " + thisCard.ATK + " / DEF " + thisCard.DEF + " / LP " + thisCard.LP;
            }
            else { lblStats.Text = ""; }

            string fullcardtext = "";
            if (thisCard.SecType == SecType.Fusion)
            {
                string fusionMaterials = "[Fusion] " + thisCard.FusionMaterial1 + " + " + thisCard.FusionMaterial2;
                if (thisCard.FusionMaterial3 != "-") { fusionMaterials = fusionMaterials + " + " + thisCard.FusionMaterial3; }
                fullcardtext = fullcardtext + fusionMaterials + "\n\n";
            }

            if (thisCard.HasOnSummonEffect)
            {
                fullcardtext = fullcardtext + "[On Summon] - " + thisCard.OnSummonEffect + "\n\n";
            }

            if (thisCard.HasContinuousEffect)
            {
                fullcardtext = fullcardtext + "[Continuous] - " + thisCard.ContinuousEffect + "\n\n";
            }

            if (thisCard.HasAbility)
            {
                fullcardtext = fullcardtext + "[Ability] - " + thisCard.Ability + "\n\n";
            }

            if (thisCard.HasIgnitionEffect)
            {
                fullcardtext = fullcardtext + thisCard.IgnitionEffect + "\n\n";
            }
            lblCardText.Text = fullcardtext;

            //Dice Faces
            if (PicDiceFace1.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace2.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace3.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace4.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace5.Image != null) { PicDiceFace1.Image.Dispose(); }
            if (PicDiceFace6.Image != null) { PicDiceFace1.Image.Dispose(); }

            PicDiceFace1.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(0).ToString(), thisCard.DiceFaceValue(0));
            PicDiceFace2.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(1).ToString(), thisCard.DiceFaceValue(1));
            PicDiceFace3.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(2).ToString(), thisCard.DiceFaceValue(2));
            PicDiceFace4.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(3).ToString(), thisCard.DiceFaceValue(3));
            PicDiceFace5.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(4).ToString(), thisCard.DiceFaceValue(4));
            PicDiceFace6.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(5).ToString(), thisCard.DiceFaceValue(5));
        }
        public static int[] GetDiceSummonSetStatus(List<CardInfo> Dice, Crest[] diceFace, int[] diceValue)
        {
            //Return codes: -1 - No Dice | 0 - Non Star/Ritual | 1 - Summon | 2 - Set | 3 - Star/Ritual No Match | 4 - Ritual Summon | 5 - Ritual Spell
            int[] results = new int[3];

            //Check Dice
            results[0] = CompareDice(Dice[0], Dice[1], Dice[2], diceFace[0], diceFace[1], diceFace[2], diceValue[0], diceValue[1], diceValue[2]);
            results[1] = CompareDice(Dice[1], Dice[0], Dice[2], diceFace[1], diceFace[0], diceFace[2], diceValue[1], diceValue[0], diceValue[2]);
            results[2] = CompareDice(Dice[2], Dice[1], Dice[0], diceFace[2], diceFace[1], diceFace[0], diceValue[2], diceValue[1], diceValue[0]);

            return results;
        }
        public static int CompareDice(CardInfo DiceZ, CardInfo DiceX, CardInfo DiceY,
                                    Crest DiceZCrest, Crest DiceXCrest, Crest DiceYCrest,
                                    int DiceZValue, int DiceXValue, int DiceYValue)
        {
            //This function will valiudate Dice "Z" agains "X" and "Y" to return the status of it (Dice Z)
            //if Dice Z lands on Star this Dice's Card can be summon/set if Dice X or Y also land on a Star with the SAME value.
            //if Dice Z lands on Ritu (Ritual) this Dice's Card can be (Ritual) summon if Dice X or Y also land on RITU with the same value AND...
            //if the Dice's Card of that dice is the corresponding ritual for the Ritual Monster of Dice Z. For example:
            //              If Dice Z's Card is "Black Luster Soldier" THEN Dice X or Y's Card must be "Black Luster Ritual" and this dice must
            //              land on RITU. otherwise the summon cannot be performed.
            //If Dice Z lands on a non Star or Ritu then there is not Summon/Set validation as this is just a "Resource" Crest.

            //Return codes: -1 - No Dice | 0 - Non Star/Ritual | 1 - Summon | 2 - Set | 3 - Star/Ritual No Match | 4 - Ritual Summon | 5 - Ritual Spell
            if (DiceZCrest == Crest.NONE)
            {
                //Set result as "No Dice"
                return -1;
            }
            else
            {
                if (DiceZCrest != Crest.STAR && DiceZCrest != Crest.RITU)
                {
                    //Set result as "Non Star/Ritual
                    return 0;
                }
                else
                {
                    if (DiceZCrest == Crest.RITU)
                    {
                        if (DiceZ.Category == Category.Spell)
                        {
                            //Set result as "Ritual Spell
                            return 5;
                        }
                        else
                        {
                            //This is a Ritual Monster, validate the Ritual Summon can be completed.
                            bool dice2isRitualMatch = false;
                            bool dice3isRitualMatch = false;
                            if ((DiceXCrest == Crest.RITU && DiceXValue == DiceZValue))
                            {
                                dice2isRitualMatch = true;
                            }
                            if ((DiceYCrest == Crest.RITU && DiceYValue == DiceZValue))
                            {
                                dice3isRitualMatch = true;
                            }

                            //If Any of the other 2 dices are also ritual with the same value
                            if (dice2isRitualMatch || dice3isRitualMatch)
                            {
                                //then check if the ritual card is the designated ritual for this monster
                                if (DiceX.RitualCard == DiceY.Name)
                                {
                                    //Set result as "Ritual Summon"
                                    return 4;
                                }
                                else
                                {
                                    //Set result as "Star/Ritual No Match"
                                    return 3;
                                }
                            }
                            else
                            {
                                //Set result as "Star/Ritual No Match"
                                return 3;
                            }

                        }
                    }
                    else
                    {
                        //This is either a Monter/Spell/Trap that landed on a Star Crest.
                        //Validate that you can summon/set this card by having another 
                        //dice that mathes its star.
                        if ((DiceXCrest == Crest.STAR && DiceXValue == DiceZValue) || (DiceYCrest == Crest.STAR && DiceYValue == DiceZValue))
                        {
                            //Set result as summon or set depending on the card category
                            if (DiceZ.Category == Category.Monster)
                            {
                                //Set result as "Summon"
                                return 1;
                            }
                            else
                            {
                                //Set result as "Set"
                                return 2;
                            }
                        }
                        else
                        {
                            //Set result as "Star/Ritual No Match"
                            return 3;
                        }
                    }
                }
            }
        }
        private void GenrateValidDimension()
        {
            //Generate the valid dimension tiles
            List<Dimension> ValidDimensions = new List<Dimension>();

            //Each each tile with each dimension form
            List<Tile> BoardTiles = _Board.GetTiles();

            //There are 23 dimension form
            for (int f = 0; f < 23; f++)
            {
                DimensionForms thisForm = (DimensionForms)f;

                //Check all the tiles....
                for (int t = 0; t < BoardTiles.Count; t++)
                {
                    Tile thisTile = BoardTiles[t];
                    Tile[] dimensionTiles = thisTile.GetDimensionTiles(thisForm);
                    Dimension thisDimension = new Dimension(dimensionTiles, thisForm, PlayerOwner.Red);

                    if (thisDimension.IsValid)
                    {
                        ValidDimensions.Add(thisDimension);
                    }
                }
            }

            //Flag if there are valid dimensions
            _ValidDimensionAvailable = ValidDimensions.Count > 0;

            //Display the no dimension warning
            if (!_ValidDimensionAvailable)
            {
                lblNoDimensionTilesWarning.Visible = true;
            }
        }
        #endregion

        #region Data
        private BoardForm _Board;
        private PlayerData _PlayerData;
        private List<Panel> _DeckCardPanelList = new List<Panel>();
        private List<PictureBox> _DeckCardImageList = new List<PictureBox>();
        private bool _DiceRolled = false;
        private List<CardInfo> _DiceToRoll = new List<CardInfo>();
        private bool _ValidDimensionAvailable = false;
        #endregion

        #region Events
        private void OnMouseEnterPicture(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
        }
        private void OnMouseEnterDeckCard(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);

            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            //Determine the current card selected.
            int cardid = -1;
            if (thiPictureBoxIndex >= 20)
            {
                int fusionIndex = thiPictureBoxIndex - 20;
                cardid = _PlayerData.Deck.GetFusionCardIDAtIndex(fusionIndex);
            }
            else
            {
                cardid = _PlayerData.Deck.GetMainCardIDAtIndex(thiPictureBoxIndex);
            }
            CardInfo thisCard = CardDataBase.GetCardWithID(cardid);

            //Change the pointer selector
            SetDeckSelector(thiPictureBoxIndex);
            LoadCardInfoPanel(thisCard);
        }
        private void OnMouseEnterRollCard(object sender, EventArgs e)
        {
            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            //Only allow clicking on a occupied slot
            if (thiPictureBoxIndex + 1 <= _DiceToRoll.Count)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Hover);

                //Get the Card Data
                int cardid = _DiceToRoll[thiPictureBoxIndex].ID;
                CardInfo thisCard = CardDataBase.GetCardWithID(cardid);

                SetRollCardSelector(thiPictureBoxIndex);
                LoadCardInfoPanel(thisCard);
            }
        }
        private void DeckCard_click(object sender, EventArgs e)
        {           
            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);
            
            //Move card to the Roll Slots if there is space for it
            //(if the card clicked is index < 20 (main deck) AND that there are 2 or less cards in the roll section.
            if (thiPictureBoxIndex < 20 && _DiceToRoll.Count < 3 && !_DiceRolled)
            {
                //Generate the card object
                int thisCardID = _PlayerData.Deck.GetMainCardIDAtIndex(thiPictureBoxIndex);
                CardInfo thisCard = CardDataBase.GetCardWithID(thisCardID);

                //Move card to the next roll slot
                SoundServer.PlaySoundEffect(SoundEffect.Click2);

                //Add this card to the list of cards to roll dice for
                _DiceToRoll.Add(thisCard);

                //Remove from deck
                _PlayerData.Deck.RemoveMainAtIndex(thiPictureBoxIndex);

                //Reload both sides
                LoadDiceToRoll();
                LoadDeckPage();

                //if the dice to roll is 3 or there no more cards in deck allow to roll
                if (_DiceToRoll.Count == 3 || _PlayerData.Deck.MainDeckSize == 0)
                {
                    btnRoll.Visible = true;
                }

                //if the card sent is a spell/trap and the player has not summon tiles open, display warning.
                if (thisCard.Category != Category.Monster && _PlayerData.FreeSummonTiles == 0)
                {
                    lblNoSummonTilesWarning.Visible = true;
                }
            }
            else
            {
                //Invalid click
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
            }
        }
        private void RollCard_click(object sender, EventArgs e)
        {
            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            //Only allow clicking on a occupied slot
            if (thiPictureBoxIndex + 1 <= _DiceToRoll.Count && !_DiceRolled)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click2);

                //Generate the card object
                int cardid = _DiceToRoll[thiPictureBoxIndex].ID;
                CardInfo thisCard = CardDataBase.GetCardWithID(cardid);

                //send card back to deck;
                _PlayerData.Deck.AddMainCard(cardid);

                //remove from dice to roll
                _DiceToRoll.RemoveAt(thiPictureBoxIndex);

                //Reload both sides
                LoadDiceToRoll();
                LoadDeckPage();

                //This will always result on not bein able to roll
                btnRoll.Visible = false;
                //Also clear the selection mark of the card just removed
                switch(thiPictureBoxIndex)
                {
                    case 0: PanelDice1.BackColor = Color.Transparent; break;
                    case 1: PanelDice2.BackColor = Color.Transparent; break;
                    case 2: PanelDice2.BackColor = Color.Transparent; break;
                }

                //Verify if the no free summon tiles warnings need to be vanish
                if (_PlayerData.FreeSummonTiles == 0)
                {
                    if (_DiceToRoll.Count == 2)
                    {
                        if (_DiceToRoll[0].Category == Category.Monster && _DiceToRoll[1].Category == Category.Monster)
                        {
                            lblNoSummonTilesWarning.Visible = false;
                        }
                    }
                    else if (_DiceToRoll.Count == 1)
                    {
                        if (_DiceToRoll[0].Category == Category.Monster)
                        {
                            lblNoSummonTilesWarning.Visible = false;
                        }
                    }
                }
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
            }
            
        }
        private void btnRoll_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            //Disable and hide all the not interactable elements 
            _DiceRolled = true;
            PanelDeck.Enabled = false;
            GroupDicesToRoll.Enabled = false;
            btnRoll.Visible = false;

            //Roll the dice
            int[] diceIndex = new int[3] { -1, -1, -1 };
            Crest[] diceFace = new Crest[3] { Crest.NONE, Crest.NONE, Crest.NONE };
            int[] diceValue = new int[3] { 0, 0, 0 };

            //display the result faces
            diceIndex[0] = Rand.DiceRoll();
            CardInfo Dice1 = _DiceToRoll[0];

            for(int x  = 0; x < 6; x++) 
            {
                SoundServer.PlaySoundEffect(SoundEffect.Attack);
                PicDiceResult1.Image = null;
                PicDiceResult1.BackColor = Color.White;
                BoardForm.WaitNSeconds(100);
                PicDiceResult1.Image = ImageServer.DiceFace(Dice1.DiceLevel, Dice1.DiceFace(x).ToString(), Dice1.DiceFaceValue(x));
                BoardForm.WaitNSeconds(100);
            }
            diceFace[0] = Dice1.DiceFace(diceIndex[0]);
            diceValue[0] = Dice1.DiceFaceValue(diceIndex[0]);
            PicDiceResult1.Image = ImageServer.DiceFace(Dice1.DiceLevel, diceFace[0].ToString(), diceValue[0]);


            if (_DiceToRoll.Count > 1) 
            {
                diceIndex[1] = Rand.DiceRoll();
                CardInfo Dice2 = _DiceToRoll[1];
                for (int x = 0; x < 6; x++)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Attack);
                    PicDiceResult2.Image = null;
                    PicDiceResult2.BackColor = Color.White;
                    BoardForm.WaitNSeconds(100);
                    PicDiceResult2.Image = ImageServer.DiceFace(Dice2.DiceLevel, Dice2.DiceFace(x).ToString(), Dice2.DiceFaceValue(x));
                    BoardForm.WaitNSeconds(100);
                }
                diceFace[1] = Dice2.DiceFace(diceIndex[1]);
                diceValue[1] = Dice2.DiceFaceValue(diceIndex[1]);
                PicDiceResult2.Image = ImageServer.DiceFace(Dice2.DiceLevel, diceFace[1].ToString(), diceValue[1]);
            }
            else
            {
                PicDiceResult2.Image = null;
            }

            if (_DiceToRoll.Count > 2) 
            {
                diceIndex[2] = Rand.DiceRoll();
                CardInfo Dice3 = _DiceToRoll[2];
                for (int x = 0; x < 6; x++)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Attack);
                    PicDiceResult3.Image = null;
                    PicDiceResult3.BackColor = Color.White;
                    BoardForm.WaitNSeconds(100);
                    PicDiceResult3.Image = ImageServer.DiceFace(Dice3.DiceLevel, Dice3.DiceFace(x).ToString(), Dice3.DiceFaceValue(x));
                    BoardForm.WaitNSeconds(100);
                }
                diceFace[2] = Dice3.DiceFace(diceIndex[2]);
                diceValue[2] = Dice3.DiceFaceValue(diceIndex[2]);
                PicDiceResult3.Image = ImageServer.DiceFace(Dice3.DiceLevel, diceFace[2].ToString(), diceValue[2]);
            }
            else
            {
                PicDiceResult3.Image = null;
            }

            //Call the logic           
            int[] results = GetDiceSummonSetStatus(_DiceToRoll, diceFace, diceValue);
           
            //Calculate the crests to add to the pool
            int movToAdd = 0;
            int atkToAdd = 0;
            int defToAdd = 0;
            int magToAdd = 0;
            int trapToAdd = 0;
            for(int x = 0; x < 3; x++)
            {
                if (results[x] == 0)
                {
                    switch (diceFace[x])
                    {
                        case Crest.MOV: movToAdd += diceValue[x]; break;
                        case Crest.ATK: atkToAdd += diceValue[x]; break;
                        case Crest.DEF: defToAdd += diceValue[x]; break;
                        case Crest.MAG: magToAdd += diceValue[x]; break;
                        case Crest.TRAP: trapToAdd += diceValue[x]; break;
                    }
                }
            }

            //Do a little delay here to pace the animation
            BoardForm.WaitNSeconds(1000);

            //Add them to the pool
            for(int x = 0; x < movToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.MOV, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblMOVCount.ForeColor = Color.Green;
                lblMOVCount.Text = _PlayerData.Crests_MOV.ToString();
            }
            for (int x = 0; x < atkToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.ATK, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblATKCount.ForeColor = Color.Green;
                lblATKCount.Text = _PlayerData.Crests_ATK.ToString();
            }
            for (int x = 0; x < defToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.DEF, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblDEFCount.ForeColor = Color.Green;
                lblDEFCount.Text = _PlayerData.Crests_DEF.ToString();
            }
            for (int x = 0; x < magToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.MAG, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblMAGCount.ForeColor = Color.Green;
                lblMAGCount.Text = _PlayerData.Crests_MAG.ToString();
            }
            for (int x = 0; x < trapToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.TRAP, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblTRAPCount.ForeColor = Color.Green;
                lblTRAPCount.Text = _PlayerData.Crests_TRAP.ToString();
            }

            //Display the Summon Set buttons for the dice that qualifyfir
            bool canSummonSet = false;
            switch (results[0])
            {
                //Normal Summon (Note that if there are not dimesion spaces, you cannot summon)
                case 1: if (_ValidDimensionAvailable) { btnDice1Summon.Visible = true; canSummonSet = true; } break;
                //Set (Note that if there are not free summon tiles you cant set)
                case 2: if (_PlayerData.FreeSummonTiles != 0) { btnDice1Set.Visible = true; canSummonSet = true; } break;
                //Ritual Summon (Note that if there are not dimesion spaces, you cannot summon)
                case 4: if (_ValidDimensionAvailable) { btnDice1Ritual.Visible = true; canSummonSet = true; } break;
            }
            switch (results[1])
            {
                //Normal Summon
                case 1: if (_ValidDimensionAvailable) { btnDice2Summon.Visible = true; canSummonSet = true; } break;
                case 2: if (_PlayerData.FreeSummonTiles != 0) { btnDice2Set.Visible = true; canSummonSet = true; } break;
                case 4: if (_ValidDimensionAvailable) { btnDice2Ritual.Visible = true; canSummonSet = true; } break;
            }
            switch (results[2])
            {
                //Normal Summon
                case 1: if (_ValidDimensionAvailable) { btnDice3Summon.Visible = true; canSummonSet = true; } break;
                case 2: if (_PlayerData.FreeSummonTiles != 0) { btnDice3Set.Visible = true; canSummonSet = true; } break;
                case 4: if (_ValidDimensionAvailable) { btnDice3Ritual.Visible = true; canSummonSet = true; } break;
            }

            _DiceRolled = true;
            GroupDicesToRoll.Enabled = true;

            if(!canSummonSet)
            {
                //Display "Go to the board" button
                btnGoToBoard.Visible = true;
            }
        }
        private void btnDice1Summon_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            CardInfo cardToBeSet = _DiceToRoll[0];
            //Set the card in the board
            _Board.SetupSummonCardPhase(cardToBeSet);

            //Cards on slot 2 and 3 from the rollset must go back to the deck
            if (_DiceToRoll.Count > 1)
            {
                //Send the card on slot [1] back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[1].ID);

                if (_DiceToRoll.Count > 2)
                {
                    //Send the card on slot [2] back to deck
                    _PlayerData.Deck.AddMainCard(_DiceToRoll[2].ID);
                }
            }
          
            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        private void btnDice2Summon_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            CardInfo cardToBeSet = _DiceToRoll[1];
            //Set the card in the board
            _Board.SetupSummonCardPhase(cardToBeSet);

            //Cards on slot 1 and 3 from the rollset must go back to the deck
            //Send the card on slot [0] back to deck
            _PlayerData.Deck.AddMainCard(_DiceToRoll[0].ID);

            if (_DiceToRoll.Count > 2)
            {
                //Send the card on slot [2] back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[2].ID);
            }

            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        private void btnDice3Summon_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            CardInfo cardToBeSet = _DiceToRoll[2];
            //Set the card in the board
            _Board.SetupSummonCardPhase(cardToBeSet);

            //Cards on slot 1 and 2 from the rollset must go back to the deck
            _PlayerData.Deck.AddMainCard(_DiceToRoll[0].ID);
            _PlayerData.Deck.AddMainCard(_DiceToRoll[1].ID);

            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        private void btnDice1Ritual_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            CardInfo cardToBeSet = _DiceToRoll[0];
            //Set the card in the board
            _Board.SetupSummonCardPhase(cardToBeSet);

            //The Ritual card will be "Used" as well. send back the third card not used in the ritual (if any)
            string RitualSpellToBeUsed = cardToBeSet.RitualCard;
            //check cards on slots [1] and [2]
            CardInfo diceOnSlot1 = _DiceToRoll[1];
            if (diceOnSlot1.Name != RitualSpellToBeUsed)
            {
                //Send the card on slot [1] back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[1].ID);
            }
            else
            {
                //Send the card on slot [2] back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[2].ID);
            }


            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        private void btnDice2Ritual_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            CardInfo cardToBeSet = _DiceToRoll[1];
            //Set the card in the board
            _Board.SetupSummonCardPhase(cardToBeSet);

            //The Ritual card will be "Used" as well. send back the third card not used in the ritual (if any)
            string RitualSpellToBeUsed = cardToBeSet.RitualCard;
            //check cards on slots [0] and [2]
            CardInfo diceOnSlot0 = _DiceToRoll[0];
            if (diceOnSlot0.Name != RitualSpellToBeUsed)
            {
                //Send the card on slot [0] back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[0].ID);
            }
            else
            {
                //Send the card on slot [2] back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[2].ID);
            }

            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        private void btnDice3Ritual_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            CardInfo cardToBeSet = _DiceToRoll[2];
            //Set the card in the board
            _Board.SetupSummonCardPhase(cardToBeSet);

            //The Ritual card will be "Used" as well. send back the third card not used in the ritual (if any)
            string RitualSpellToBeUsed = cardToBeSet.RitualCard;
            //check cards on slots [0] and [1]
            CardInfo diceOnSlot0 = _DiceToRoll[0];
            if (diceOnSlot0.Name != RitualSpellToBeUsed)
            {
                //Send the card on slot [0] back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[0].ID);
            }
            else
            {
                //Send the card on slot [1 back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[1].ID);
            }

            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        private void btnDice1Set_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            CardInfo cardToBeSet = _DiceToRoll[0];
            //Set the card in the board
            _Board.SetupSetCardPhase(cardToBeSet);

            //Cards on slot 2 and 3 from the rollset must go back to the deck
            if (_DiceToRoll.Count > 1)
            {
                //Send the card on slot [1] back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[1].ID);

                if (_DiceToRoll.Count > 2)
                {
                    //Send the card on slot [2] back to deck
                    _PlayerData.Deck.AddMainCard(_DiceToRoll[2].ID);
                }
            }

            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        private void btnDice2Set_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            CardInfo cardToBeSet = _DiceToRoll[1];
            //Set the card in the board
            _Board.SetupSetCardPhase(cardToBeSet);

            //Cards on slot 1 and 3 from the rollset must go back to the deck
            //Send the card on slot [0] back to deck
            _PlayerData.Deck.AddMainCard(_DiceToRoll[0].ID);

            if (_DiceToRoll.Count > 2)
            {
                //Send the card on slot [2] back to deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[2].ID);
            }

            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        private void btnDice3Set_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            CardInfo cardToBeSet = _DiceToRoll[2];
            //Set the card in the board
            _Board.SetupSetCardPhase(cardToBeSet);

            //Cards on slot 1 and 2 from the rollset must go back to the deck
            _PlayerData.Deck.AddMainCard(_DiceToRoll[0].ID);
            _PlayerData.Deck.AddMainCard(_DiceToRoll[1].ID);

            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        private void btnGoToBoard_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //In the board reload the crest counts
            _Board.SetupMainPhaseNoSummon();

            //Return all the cards to the deck
            foreach (CardInfo card in _DiceToRoll) 
            {
                _PlayerData.Deck.AddMainCard(card.ID);
            }
          
            //Close this form and retrn to the board
            Dispose();
            _Board.Show();
        }
        #endregion
    }
}