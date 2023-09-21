//Joel Campos
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

            //Clear the dices selected to roll
            PanelDice1.BackColor = Color.Transparent;
            PanelDice2.BackColor = Color.Transparent;
            PanelDice3.BackColor = Color.Transparent;

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
        private int[] GetDiceSummonSetStatus(List<CardInfo> Dice, string[] diceFace, int[] diceValue)
        {
            //Return codes: -1 - No Dice | 0 - Non Star/Ritual | 1 - Summon | 2 - Set | 3 - Star/Ritual No Match | 4 - Ritual Summon | 5 - Ritual Spell
            int[] results = new int[3];

            //Check Dice
            results[0] = CompareDice(Dice[0], Dice[1], Dice[2], diceFace[0], diceFace[1], diceFace[2], diceValue[0], diceValue[1], diceValue[2]);
            results[1] = CompareDice(Dice[1], Dice[0], Dice[2], diceFace[1], diceFace[0], diceFace[2], diceValue[1], diceValue[0], diceValue[2]);
            results[2] = CompareDice(Dice[2], Dice[1], Dice[0], diceFace[2], diceFace[1], diceFace[0], diceValue[2], diceValue[1], diceValue[0]);

            return results;
        }
        private int CompareDice(CardInfo DiceZ, CardInfo DiceX, CardInfo DiceY,
                                    string DiceZCrest, string DiceXCrest, string DiceYCrest,
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
            if (DiceZCrest == "NONE")
            {
                //Set result as "No Dice"
                return -1;
            }
            else
            {
                if (DiceZCrest != "STAR" && DiceZCrest != "RITU")
                {
                    //Set result as "Non Star/Ritual
                    return 0;
                }
                else
                {
                    if (DiceZCrest == "RITU")
                    {
                        if (DiceZ.Category == "SPELL")
                        {
                            //Set result as "Ritual Spell
                            return 5;
                        }
                        else
                        {
                            //This is a Ritual Monster, validate the Ritual Summon can be completed.
                            bool dice2isRitualMatch = false;
                            bool dice3isRitualMatch = false;
                            if ((DiceXCrest == "RITU" && DiceXValue == DiceZValue))
                            {
                                dice2isRitualMatch = true;
                            }
                            if ((DiceYCrest == "RITU" && DiceYValue == DiceZValue))
                            {
                                dice3isRitualMatch = true;
                            }

                            //If Any of the other 2 dices are also ritual with the same value
                            if (dice2isRitualMatch || dice3isRitualMatch)
                            {
                                //then check if the ritual card is the designated ritual for this monster
                                switch (DiceZ.Name)
                                {
                                    case "Black Luster Soldier":
                                        if (dice2isRitualMatch && DiceX.Name == "Black Luster Ritual" || dice3isRitualMatch && DiceY.Name == "Black Luster Ritual")
                                        {
                                            //Set result as "Ritual Summon"
                                            return 4;
                                        }
                                        else { return 3; }                                        
                                    default:
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
                        if ((DiceXCrest == "STAR" && DiceXValue == DiceZValue) || (DiceYCrest == "STAR" && DiceYValue == DiceZValue))
                        {
                            //Set result as summon or set depending on the card category
                            if (DiceZ.Category == "Monster")
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

        BoardForm _Board;
        private PlayerData _PlayerData;
        private List<Panel> _DeckCardPanelList = new List<Panel>();
        private List<PictureBox> _DeckCardImageList = new List<PictureBox>();
        private CardInfo _CurrentCardSelected;
        private int _CurrentDeckIndexSelected = 0;
        private int _CurrentRollIndexSelected = 0;
        private bool _DiceRolled = false;

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
            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            if (thiPictureBoxIndex + 1 <= _DiceToRoll.Count)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click2);

                int cardid = _DiceToRoll[thiPictureBoxIndex].ID;

                //Show to deck arrow only
                PicToStoArrow.Visible = false;
                if (!_DiceRolled) { PicToDeckArrow.Visible = true; }

                _CurrentCardSelected = CardDataBase.GetCardWithID(cardid);
                _CurrentRollIndexSelected = thiPictureBoxIndex;

                SetRollCardSelector(thiPictureBoxIndex);
                LoadCardInfoPanel();
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
            }
            
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

            //if the card sent is a spell/trap and the player has not summon tiles open, display warning.
            if(_CurrentCardSelected.Category != "Monster" && _PlayerData.FreeSummonTiles == 0) 
            {
                lblNoSummonTilesWarning.Visible = true;
            }
            
        }
        private void PicToDeckArrow_Click(object sender, EventArgs e)
        {
            //send card back to dec;
            _PlayerData.Deck.AddMainCard(_CurrentCardSelected.ID);

            //remove from dice to roll
            _DiceToRoll.RemoveAt(_CurrentRollIndexSelected);

            //Reload both sides
            LoadDiceToRoll();
            LoadDeckPage();

            //Hide both arrows until another selection is clicked
            PicToDeckArrow.Visible = false;
            PicToStoArrow.Visible = false;

            //THis will always result on not bein able to roll
            btnRoll.Visible = false;

            //Verify if the no free summon tiles warnings need to be vanish
            if(_PlayerData.FreeSummonTiles == 0)
            {
                if(_DiceToRoll.Count == 2)
                {
                    if (_DiceToRoll[0].Category == "Monster" && _DiceToRoll[1].Category == "Monster")
                    {
                        lblNoSummonTilesWarning.Visible = false;
                    }
                }
                else if (_DiceToRoll.Count == 1)
                {
                    if (_DiceToRoll[0].Category == "Monster")
                    {
                        lblNoSummonTilesWarning.Visible = false;
                    }
                }
            }
        }
        private void btnRoll_Click(object sender, EventArgs e)
        {
            //Disable and hide all the not interactable elements 
            PanelDeck.Enabled = false;
            GroupDicesToRoll.Enabled = false;
            PicToDeckArrow.Visible = false;
            PicToStoArrow.Visible = false;
            btnRoll.Visible = false;

            //Roll the dice
            int[] diceIndex = new int[3] { -1, -1, -1 };
            string[] diceFace = new string[3] { "NONE", "NONE", "NONE" };
            int[] diceValue = new int[3] { 0, 0, 0 };

            //display the result faces
            diceIndex[0] = Rand.DiceRoll();
            CardInfo Dice1 = _DiceToRoll[0];

            for(int x  = 0; x < 6; x++) 
            {
                PicDiceResult1.Image = null;
                PicDiceResult1.BackColor = Color.White;
                BoardForm.WaitNSeconds(100);
                PicDiceResult1.Image = ImageServer.DiceFace(Dice1.DiceLevel, Dice1.DiceFace(x), Dice1.DiceFaceValue(x));
                BoardForm.WaitNSeconds(100);
            }
            diceFace[0] = Dice1.DiceFace(diceIndex[0]);
            diceValue[0] = Dice1.DiceFaceValue(diceIndex[0]);
            PicDiceResult1.Image = ImageServer.DiceFace(Dice1.DiceLevel, diceFace[0], diceValue[0]);


            if (_DiceToRoll.Count > 1) 
            {
                diceIndex[1] = Rand.DiceRoll();
                CardInfo Dice2 = _DiceToRoll[1];
                for (int x = 0; x < 6; x++)
                {
                    PicDiceResult2.Image = null;
                    PicDiceResult2.BackColor = Color.White;
                    BoardForm.WaitNSeconds(100);
                    PicDiceResult2.Image = ImageServer.DiceFace(Dice2.DiceLevel, Dice2.DiceFace(x), Dice2.DiceFaceValue(x));
                    BoardForm.WaitNSeconds(100);
                }
                diceFace[1] = Dice2.DiceFace(diceIndex[1]);
                diceValue[1] = Dice2.DiceFaceValue(diceIndex[1]);
                PicDiceResult2.Image = ImageServer.DiceFace(Dice2.DiceLevel, diceFace[1], diceValue[1]);
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
                    PicDiceResult3.Image = null;
                    PicDiceResult3.BackColor = Color.White;
                    BoardForm.WaitNSeconds(100);
                    PicDiceResult3.Image = ImageServer.DiceFace(Dice3.DiceLevel, Dice3.DiceFace(x), Dice3.DiceFaceValue(x));
                    BoardForm.WaitNSeconds(100);
                }
                diceFace[2] = Dice3.DiceFace(diceIndex[2]);
                diceValue[2] = Dice3.DiceFaceValue(diceIndex[2]);
                PicDiceResult3.Image = ImageServer.DiceFace(Dice3.DiceLevel, diceFace[2], diceValue[2]);
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
                        case "MOV": movToAdd += diceValue[x]; break;
                        case "ATK": atkToAdd += diceValue[x]; break;
                        case "DEF": defToAdd += diceValue[x]; break;
                        case "MAG": magToAdd += diceValue[x]; break;
                        case "TRAP": trapToAdd += diceValue[x]; break;
                    }
                }
            }

            //Add them to the pool
            for(int x = 0; x < movToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.Movement, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblMOVCount.ForeColor = Color.Green;
                lblMOVCount.Text = _PlayerData.Crests_MOV.ToString();
            }
            for (int x = 0; x < atkToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.Attack, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblATKCount.ForeColor = Color.Green;
                lblATKCount.Text = _PlayerData.Crests_ATK.ToString();
            }
            for (int x = 0; x < defToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.Defense, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblDEFCount.ForeColor = Color.Green;
                lblDEFCount.Text = _PlayerData.Crests_DEF.ToString();
            }
            for (int x = 0; x < magToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.Magic, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblMAGCount.ForeColor = Color.Green;
                lblMAGCount.Text = _PlayerData.Crests_MAG.ToString();
            }
            for (int x = 0; x < trapToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.Trap, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblTRAPCount.ForeColor = Color.Green;
                lblTRAPCount.Text = _PlayerData.Crests_TRAP.ToString();
            }

            //Display the Summon Set buttons for the dice that qualifyfir
            bool canSummonSet = false;
            switch (results[0])
            {
                //Normal Summon
                case 1: btnDice1Summon.Visible = true;  canSummonSet = true; break;
                //Set (Note that if there are not free summon tiles you cant set)
                case 2: if (_PlayerData.FreeSummonTiles != 0) { btnDice1Set.Visible = true; canSummonSet = true; } break;
                //Ritual Summon
                case 4: btnDice1Ritual.Visible = true;  canSummonSet = true; break;
            }
            switch (results[1])
            {
                //Normal Summon
                case 1: btnDice2Summon.Visible = true;  canSummonSet = true; break;
                case 2: if (_PlayerData.FreeSummonTiles != 0) { btnDice2Set.Visible = true; canSummonSet = true; } break;
                case 4: btnDice2Ritual.Visible = true;  canSummonSet = true; break;
            }
            switch (results[2])
            {
                //Normal Summon
                case 1: btnDice3Summon.Visible = true;  canSummonSet = true; break;
                case 2: if (_PlayerData.FreeSummonTiles != 0) { btnDice3Set.Visible = true; canSummonSet = true; } break;
                case 4: btnDice3Ritual.Visible = true;  canSummonSet = true; break;
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

        }
        private void btnDice2Summon_Click(object sender, EventArgs e)
        {

        }
        private void btnDice3Summon_Click(object sender, EventArgs e)
        {

        }
        private void btnDice1Ritual_Click(object sender, EventArgs e)
        {

        }
        private void btnDice2Ritual_Click(object sender, EventArgs e)
        {

        }
        private void btnDice3Ritual_Click(object sender, EventArgs e)
        {

        }
        private void btnDice1Set_Click(object sender, EventArgs e)
        {

        }
        private void btnDice2Set_Click(object sender, EventArgs e)
        {

        }
        private void btnDice3Set_Click(object sender, EventArgs e)
        {

        }
    }
}