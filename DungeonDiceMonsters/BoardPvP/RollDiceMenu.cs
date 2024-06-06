//Joel Campos
//9/21/2023
//RollDiceMenu Class

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
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
            _PvPMatch = false;

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
            GenrateValidDimensions();            
        }
        public RollDiceMenu(bool isUserTurn, PlayerColor turnplayercolor, PlayerData playerdata, BoardPvP board, NetworkStream tmpns)
        {
            InitializeComponent();
            _PlayerData = playerdata;
            _PvPBoard = board;
            ns = tmpns;
            _PvPMatch = true;
            _IsUserTurn = isUserTurn;
            _TurnPlayerColor = turnplayercolor;

            //Only allow these even handlers if is the user turn
            if(_IsUserTurn) 
            {
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
                lblInactiveWarning.Visible = false;
            }
            else
            {
                lblInactiveWarning.Visible = true;
            }

            //Initialize the flag of free summon tiles, this is going to be used to determine if the player can set Spell/Traps
            _UnoccupiedSummonTiles = _PvPBoard.GetUnoccupiedSpellTrapZoneTiles(_TurnPlayerColor).Count > 0;

            InitializeDeckComponents();
            LoadDeckPage();
            GenrateValidDimensions();
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
                    if (_IsUserTurn)
                    {
                        CardImage.Click += new EventHandler(DeckCard_click);
                        CardImage.MouseEnter += OnMouseEnterDeckCard;
                    }

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
                if (_IsUserTurn)
                {
                    CardImage.Click += new EventHandler(DeckCard_click);
                    CardImage.MouseEnter += OnMouseEnterDeckCard;
                }

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
                    CardInfo thisCard = CardDataBase.GetCardWithID(cardID);

                    //Populate the card image with the card ID and show the dice level icon
                    if (_IsUserTurn)
                    {
                        ImageServer.ClearImage(_DeckCardImageList[x]);
                        _DeckCardImageList[x].Image = ImageServer.FullCardImage(cardID.ToString());
                        
                        //Load the dice level icon
                        if(thisCard.SecType == SecType.Ritual)
                        {
                            ImageServer.ClearImage(_DeckDiceLevelIconImageList[x]);
                            _DeckDiceLevelIconImageList[x].Image = ImageServer.DiceFace(thisCard.DiceLevel, "RITU", thisCard.DiceLevel);
                        }
                        else
                        {
                            ImageServer.ClearImage(_DeckDiceLevelIconImageList[x]);
                            _DeckDiceLevelIconImageList[x].Image = ImageServer.DiceFace(thisCard.DiceLevel, "STAR", thisCard.DiceLevel);
                        }


                        _DeckDiceLevelIconImageList[x].Visible = true;
                    }
                    else
                    {
                        ImageServer.ClearImage(_DeckCardImageList[x]);
                        _DeckCardImageList[x].Image = ImageServer.FullCardImage("0");
                        _DeckDiceLevelIconImageList[x].Visible = false;

                    }
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
                    if (_IsUserTurn)
                    {
                        ImageServer.ClearImage(_DeckCardImageList[x + 20]);
                        _DeckCardImageList[x + 20].Image = ImageServer.FullCardImage(cardID.ToString());
                    }
                    else
                    {
                        ImageServer.ClearImage(_DeckCardImageList[x + 20]);
                        _DeckCardImageList[x + 20].Image = ImageServer.FullCardImage("0");
                    }
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
                    if (dices[x].Image != null) { dices[x].Image.Dispose(); }
                    if(_IsUserTurn)
                    {
                        ImageServer.ClearImage(dices[x]);
                        dices[x].Image = ImageServer.FullCardImage(_DiceToRoll[x].ID.ToString());
                    }
                    else
                    {
                        ImageServer.ClearImage(dices[x]);
                        dices[x].Image = ImageServer.FullCardImage("0");
                    }
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
            ImageServer.ClearImage(PicCardArtwork);
            PicCardArtwork.Image = ImageServer.CardArtworkImage(cardID.ToString());

            lblID.Text = cardID.ToString();
            lblCardName.Text = thisCard.Name;

            string secondaryType = thisCard.SecType.ToString();
            lblCardType.Text = thisCard.TypeAsString + "/" + secondaryType;
            if (thisCard.Category == Category.Spell) { lblCardType.Text = thisCard.TypeAsString + " spell"; }
            if (thisCard.Category == Category.Trap) { lblCardType.Text = thisCard.TypeAsString + " trap"; }

            if (thisCard.Category == Category.Monster) { lblCardLevel.Text = "Card Lv. " + thisCard.Level; }
            else { lblCardLevel.Text = ""; }

            ImageServer.ClearImage(PicCardAttribute);
            ImageServer.ClearImage(PicCardMonsterType);
            PicCardAttribute.Image = ImageServer.AttributeIcon(thisCard.Attribute);
            PicCardMonsterType.Image = ImageServer.MonsterTypeIcon(thisCard.TypeAsString);

            lblDiceLevel.Text = "Dice Lv. " + thisCard.DiceLevel;

            if (thisCard.Category == Category.Monster)
            {
                lblStats.Text = "ATK " + thisCard.ATK + " / DEF " + thisCard.DEF + " / LP " + thisCard.LP;
            }
            else { lblStats.Text = ""; }

            string fullcardtext = "";
            if (thisCard.SecType == SecType.Fusion)
            {
                string fusionMaterials = thisCard.FusionMaterial1 + " + " + thisCard.FusionMaterial2;
                if (thisCard.FusionMaterial3 != "-") { fusionMaterials = fusionMaterials + " + " + thisCard.FusionMaterial3; }
                fullcardtext = fullcardtext + fusionMaterials + "\n\n";
            }

            if (thisCard.HasOnSummonEffect)
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

            if (thisCard.HasTriggerEffect)
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
        public static int[] GetDiceSummonSetStatus(List<CardInfo> DiceOG, Crest[] diceFace, int[] diceValue)
        {
            //Return codes: -1 - No Dice | 0 - Non Star/Ritual | 1 - Summon | 2 - Set | 3 - Star/Ritual No Match | 4 - Ritual Summon | 5 - Ritual Spell
            int[] results = new int[3];

            //We need to override the List of dice rolled... but this is going to mess things off later (bc we are passing a list by ref programing 101 dude)
            //so make a new list and get the items from the og list, then u can mod it without worries.
            List<CardInfo> Dice = new List<CardInfo>();
            foreach (var item in DiceOG)
            {
                Dice.Add(item);
            }

            //Adding "dummy" dices when rolling only 1 or 2 dice in the form. this is to prevent index out of range exceptions
            if (Dice.Count == 1) { Dice.Add(new CardInfo(Attribute.DARK)); }
            if(Dice.Count == 2) { Dice.Add(new CardInfo(Attribute.DARK)); }

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
                            if (DiceXCrest == Crest.RITU && (DiceZ.RitualCard == DiceX.Name))
                            {
                                //Set result as "Ritual Summon"
                                return 4;
                            }
                            else if (DiceYCrest == Crest.RITU && (DiceZ.RitualCard == DiceY.Name))
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
        private void GenrateValidDimensions()
        {
            //Generate the valid dimension tiles
            List<Dimension> ValidDimensions = new List<Dimension>();

            //Each each tile with each dimension form
            List<Tile> BoardTiles = new List<Tile>();
            if(_PvPMatch)
            {
                BoardTiles = _PvPBoard.GetTiles();
            }
            else
            {
                BoardTiles = _Board.GetTiles();
            }

            //There are 23 dimension form
            for (int f = 0; f < 23; f++)
            {
                DimensionForms thisForm = (DimensionForms)f;

                //Check all the tiles....
                for (int t = 0; t < BoardTiles.Count; t++)
                {
                    Tile thisTile = BoardTiles[t];
                    Tile[] dimensionTiles = thisTile.GetDimensionTiles(thisForm);
                    Dimension thisDimension = new Dimension(dimensionTiles, thisForm, _TurnPlayerColor);

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

        public void CloseWithoutShuttingDownTheApp()
        {
            //Raise this flag so the whole app doesnt shutdown when closing the form
            _AppShutDownWhenClose = false;
            Dispose();
        }

        #region Send/Receive To/From Server Methods
        private void SendMessageToServer(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            ns.Write(buffer, 0, buffer.Length);
        }
        public void ReceiveMesageFromServer(string DATARECEIVED)
        {
            //Step 1: Extract the Message SECONDARY Key
            string[] MessageTokens = DATARECEIVED.Split('|');
            string SecondaryKey = MessageTokens[2];

            //Step 2: Handle the message
            switch (SecondaryKey)
            {
                case "[SELECT DECK CARD TO ROLL]": DeckCard_Base(Convert.ToInt32(MessageTokens[3])); break;
                case "[SELECT ROLL CARD TO DECK]": RollCard_Base(Convert.ToInt32(MessageTokens[3])); break;
                case "[CLICK ROLL!!]":             btnRoll_Base(Convert.ToInt32(MessageTokens[3]), Convert.ToInt32(MessageTokens[4]), Convert.ToInt32(MessageTokens[5])); break;
                case "[SUMMON BUTTON 1]":          btnDice1Summon_Base(); break;
                case "[SUMMON BUTTON 2]":          btnDice2Summon_Base(); break;
                case "[SUMMON BUTTON 3]":          btnDice3Summon_Base(); break;
                case "[RITUAL BUTTON 1]":          btnDice1Ritual_Base(); break;
                case "[RITUAL BUTTON 2]":          btnDice2Ritual_Base(); break;
                case "[RITUAL BUTTON 3]":          btnDice3Ritual_Base(); break;
                case "[SET CARD BUTTON 1]":        btnDice1Set_Base(); break;
                case "[SET CARD BUTTON 2]":        btnDice2Set_Base(); break;
                case "[SET CARD BUTTON 3]":        btnDice3Set_Base(); break;
                case "[GO TO BOARD BUTTON]":       btnGoToBoard_Base(); break;
            }
        }
        #endregion

        #region Data
        private bool _IsUserTurn;
        private bool _PvPMatch;
        private BoardForm _Board;
        private BoardPvP _PvPBoard;
        private PlayerData _PlayerData;
        private PlayerColor _TurnPlayerColor = PlayerColor.NONE;
        private List<Panel> _DeckCardPanelList = new List<Panel>();
        private List<PictureBox> _DeckCardImageList = new List<PictureBox>();
        private List<PictureBox> _DeckDiceLevelIconImageList = new List<PictureBox>();
        private bool _DiceRolled = false;
        private List<CardInfo> _DiceToRoll = new List<CardInfo>();
        private bool _ValidDimensionAvailable = false;
        private bool _UnoccupiedSummonTiles = false;
        //Client NetworkStream to send message to the server
        private NetworkStream ns;
        private bool _AppShutDownWhenClose = true;
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
                //Send the action message to the server
                SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[SELECT DECK CARD TO ROLL]|" + thiPictureBoxIndex);

                //Perform the action
                DeckCard_Base(thiPictureBoxIndex);
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
                //Send the action message to the server
                SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[SELECT ROLL CARD TO DECK]|" + thiPictureBoxIndex);

                //Perform the action
                RollCard_Base(thiPictureBoxIndex);
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
            }
            
        }
        private void btnRoll_Click(object sender, EventArgs e)
        {
            //Run the rnd first so you can send the results on to the server
            int ResultA = Rand.DiceRoll();
            int ResultB = Rand.DiceRoll();
            int ResultC = Rand.DiceRoll();

            //Send the action message to the server
            SendMessageToServer(string.Format("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[CLICK ROLL!!]|{0}|{1}|{2}", ResultA, ResultB, ResultC));

            //Perform the action
            btnRoll_Base(ResultA, ResultB, ResultC);
        }
        private void btnDice1Summon_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[SUMMON BUTTON 1]");

            //Perform the action
            btnDice1Summon_Base();
        }
        private void btnDice2Summon_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[SUMMON BUTTON 2]");

            //Perform the action
            btnDice2Summon_Base();
        }
        private void btnDice3Summon_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[SUMMON BUTTON 3]");

            //Perform the action
            btnDice3Summon_Base();
        }
        private void btnDice1Ritual_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[RITUAL BUTTON 1]");

            //Perform the action
            btnDice1Ritual_Base();
        }
        private void btnDice2Ritual_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[RITUAL BUTTON 2]");

            //Perform the action
            btnDice2Ritual_Base();
        }
        private void btnDice3Ritual_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[RITUAL BUTTON 3]");

            //Perform the action
            btnDice3Ritual_Base();
        }
        private void btnDice1Set_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[SET CARD BUTTON 1]");

            //Perform the action
            btnDice1Set_Base();
        }
        private void btnDice2Set_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[SET CARD BUTTON 2]");

            //Perform the action
            btnDice2Set_Base();
        }
        private void btnDice3Set_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[SET CARD BUTTON 3]");

            //Perform the action
            btnDice3Set_Base();
        }
        private void btnGoToBoard_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[ROLL DICE FORM REQUEST]|MainPhaseBoard|[GO TO BOARD BUTTON]");

            //Perform the action
            btnGoToBoard_Base();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_AppShutDownWhenClose)
            {
                Environment.Exit(Environment.ExitCode);
            }
        }
        #endregion

        #region Base Player Actions
        private void DeckCard_Base(int PictureBoxIndex)
        {
            Invoke(new MethodInvoker(delegate () {
                //Generate the card object
                int thisCardID = _PlayerData.Deck.GetMainCardIDAtIndex(PictureBoxIndex);
                CardInfo thisCard = CardDataBase.GetCardWithID(thisCardID);

                //Move card to the next roll slot
                SoundServer.PlaySoundEffect(SoundEffect.Click2);

                //Add this card to the list of cards to roll dice for
                _DiceToRoll.Add(thisCard);

                //Remove from deck
                _PlayerData.Deck.RemoveMainAtIndex(PictureBoxIndex);

                //Reload both sides
                LoadDiceToRoll();
                LoadDeckPage();

                //if the dice to roll is 3 or there no more cards in deck allow to roll
                if (_DiceToRoll.Count == 3 || _PlayerData.Deck.MainDeckSize == 0)
                {
                    btnRoll.Visible = true;
                    if(!_IsUserTurn)
                    {
                        btnRoll.Enabled = false;
                    }
                }

                //if the card sent is a spell/trap and the player has not summon tiles open, display warning.
                if (thisCard.Category != Category.Monster && !_UnoccupiedSummonTiles && _IsUserTurn)
                {
                    lblNoSummonTilesWarning.Visible = true;
                }
            }));           
        }
        private void RollCard_Base(int thiPictureBoxIndex)
        {
            Invoke(new MethodInvoker(delegate () {
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
                switch (thiPictureBoxIndex)
                {
                    case 0: PanelDice1.BackColor = Color.Transparent; break;
                    case 1: PanelDice2.BackColor = Color.Transparent; break;
                    case 2: PanelDice2.BackColor = Color.Transparent; break;
                }

                //Verify if the no free summon tiles warnings need to be vanish
                if (!_UnoccupiedSummonTiles && _IsUserTurn)
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
                    else
                    {
                        lblNoSummonTilesWarning.Visible = false;
                    }
                }
            }));
        }
        private void btnRoll_Base(int resultA, int resultB, int resultC)
        {
            Invoke(new MethodInvoker(delegate () {
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
                diceIndex[0] = resultA;
                CardInfo Dice1 = _DiceToRoll[0];

                for (int x = 0; x < 6; x++)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Attack);
                    PicDiceResult1.Image = null;
                    PicDiceResult1.BackColor = Color.White;
                    BoardForm.WaitNSeconds(100);
                    ImageServer.ClearImage(PicDiceResult1);
                    PicDiceResult1.Image = ImageServer.DiceFace(Dice1.DiceLevel, Dice1.DiceFace(x).ToString(), Dice1.DiceFaceValue(x));
                    BoardForm.WaitNSeconds(100);
                }
                diceFace[0] = Dice1.DiceFace(diceIndex[0]);
                diceValue[0] = Dice1.DiceFaceValue(diceIndex[0]);
                ImageServer.ClearImage(PicDiceResult1);
                PicDiceResult1.Image = ImageServer.DiceFace(Dice1.DiceLevel, diceFace[0].ToString(), diceValue[0]);


                if (_DiceToRoll.Count > 1)
                {
                    diceIndex[1] = resultB;
                    CardInfo Dice2 = _DiceToRoll[1];
                    for (int x = 0; x < 6; x++)
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.Attack);
                        PicDiceResult2.Image = null;
                        PicDiceResult2.BackColor = Color.White;
                        BoardForm.WaitNSeconds(100);
                        ImageServer.ClearImage(PicDiceResult2);
                        PicDiceResult2.Image = ImageServer.DiceFace(Dice2.DiceLevel, Dice2.DiceFace(x).ToString(), Dice2.DiceFaceValue(x));
                        BoardForm.WaitNSeconds(100);
                    }
                    diceFace[1] = Dice2.DiceFace(diceIndex[1]);
                    diceValue[1] = Dice2.DiceFaceValue(diceIndex[1]);
                    ImageServer.ClearImage(PicDiceResult2);
                    PicDiceResult2.Image = ImageServer.DiceFace(Dice2.DiceLevel, diceFace[1].ToString(), diceValue[1]);
                }
                else
                {
                    PicDiceResult2.Image = null;
                }

                if (_DiceToRoll.Count > 2)
                {
                    diceIndex[2] = resultC;
                    CardInfo Dice3 = _DiceToRoll[2];
                    for (int x = 0; x < 6; x++)
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.Attack);
                        PicDiceResult3.Image = null;
                        PicDiceResult3.BackColor = Color.White;
                        BoardForm.WaitNSeconds(100);
                        ImageServer.ClearImage(PicDiceResult3);
                        PicDiceResult3.Image = ImageServer.DiceFace(Dice3.DiceLevel, Dice3.DiceFace(x).ToString(), Dice3.DiceFaceValue(x));
                        BoardForm.WaitNSeconds(100);
                    }
                    diceFace[2] = Dice3.DiceFace(diceIndex[2]);
                    diceValue[2] = Dice3.DiceFaceValue(diceIndex[2]);
                    ImageServer.ClearImage(PicDiceResult3);
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
                for (int x = 0; x < 3; x++)
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
                for (int x = 0; x < movToAdd; x++)
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

                //if the non-user turn, disable all action buttons (Regardless on which one end up "apearing")
                if (!_IsUserTurn)
                {
                    btnDice1Summon.Enabled = false;
                    btnDice2Summon.Enabled = false;
                    btnDice3Summon.Enabled = false;

                    btnDice1Set.Enabled = false;
                    btnDice2Set.Enabled = false;
                    btnDice3Set.Enabled = false;

                    btnDice1Ritual.Enabled = false;
                    btnDice2Ritual.Enabled = false;
                    btnDice3Ritual.Enabled = false;

                    btnGoToBoard.Enabled = false;
                }

                //Display the Summon Set buttons for the dice that qualifyfir
                switch (results[0])
                {
                    //Normal Summon (Note that if there are not dimesion spaces, you cannot summon)
                    case 1: if (_ValidDimensionAvailable) { btnDice1Summon.Visible = true; } break;
                    //Set (Note that if there are not free summon tiles you cant set)
                    case 2: if (_UnoccupiedSummonTiles) { btnDice1Set.Visible = true; } break;
                    //Ritual Summon (Note that if there are not dimesion spaces, you cannot summon)
                    case 4: if (_ValidDimensionAvailable) { btnDice1Ritual.Visible = true; } break;
                }
                switch (results[1])
                {
                    //Normal Summon
                    case 1: if (_ValidDimensionAvailable) { btnDice2Summon.Visible = true; } break;
                    case 2: if (_UnoccupiedSummonTiles) { btnDice2Set.Visible = true; } break;
                    case 4: if (_ValidDimensionAvailable) { btnDice2Ritual.Visible = true; } break;
                }
                switch (results[2])
                {
                    //Normal Summon
                    case 1: if (_ValidDimensionAvailable) { btnDice3Summon.Visible = true; } break;
                    case 2: if (_UnoccupiedSummonTiles) { btnDice3Set.Visible = true; } break;
                    case 4: if (_ValidDimensionAvailable) { btnDice3Ritual.Visible = true; } break;
                }
                
                _DiceRolled = true;
                GroupDicesToRoll.Enabled = true;
                btnGoToBoard.Visible = true;
            }));
        }
        private void btnDice1Summon_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                CardInfo cardToBeSet = _DiceToRoll[0];
                //Set the card in the board
                if (_PvPMatch)
                {
                    _PvPBoard.SetupSummonCardPhase(cardToBeSet);
                }
                else
                {
                    _Board.SetupSummonCardPhase(cardToBeSet);
                }

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
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        private void btnDice2Summon_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                CardInfo cardToBeSet = _DiceToRoll[1];
                //Set the card in the board
                if (_PvPMatch)
                {
                    _PvPBoard.SetupSummonCardPhase(cardToBeSet);
                }
                else
                {
                    _Board.SetupSummonCardPhase(cardToBeSet);
                }

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
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        private void btnDice3Summon_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                CardInfo cardToBeSet = _DiceToRoll[2];
                //Set the card in the board
                if (_PvPMatch)
                {
                    _PvPBoard.SetupSummonCardPhase(cardToBeSet);
                }
                else
                {
                    _Board.SetupSummonCardPhase(cardToBeSet);
                }

                //Cards on slot 1 and 2 from the rollset must go back to the deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[0].ID);
                _PlayerData.Deck.AddMainCard(_DiceToRoll[1].ID);

                //Close this form and retrn to the board
                Dispose();
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        private void btnDice1Ritual_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                CardInfo cardToBeSet = _DiceToRoll[0];
                //Set the card in the board
                if (_PvPMatch)
                {
                    _PvPBoard.SetupSummonCardPhase(cardToBeSet);
                }
                else
                {
                    _Board.SetupSummonCardPhase(cardToBeSet);
                }

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
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        private void btnDice2Ritual_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                CardInfo cardToBeSet = _DiceToRoll[1];
                //Set the card in the board
                if (_PvPMatch)
                {
                    _PvPBoard.SetupSummonCardPhase(cardToBeSet);
                }
                else
                {
                    _Board.SetupSummonCardPhase(cardToBeSet);
                }

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
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        private void btnDice3Ritual_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                CardInfo cardToBeSet = _DiceToRoll[2];
                //Set the card in the board
                if (_PvPMatch)
                {
                    _PvPBoard.SetupSummonCardPhase(cardToBeSet);
                }
                else
                {
                    _Board.SetupSummonCardPhase(cardToBeSet);
                }

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
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        private void btnDice1Set_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                CardInfo cardToBeSet = _DiceToRoll[0];
                //Set the card in the board
                if (_PvPMatch)
                {
                    _PvPBoard.SetupSetCardPhase(cardToBeSet);
                }
                else
                {
                    _Board.SetupSetCardPhase(cardToBeSet);
                }

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
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        private void btnDice2Set_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                CardInfo cardToBeSet = _DiceToRoll[1];
                //Set the card in the board
                if (_PvPMatch)
                {
                    _PvPBoard.SetupSetCardPhase(cardToBeSet);
                }
                else
                {
                    _Board.SetupSetCardPhase(cardToBeSet);
                }

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
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        private void btnDice3Set_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                CardInfo cardToBeSet = _DiceToRoll[2];
                //Set the card in the board
                if (_PvPMatch)
                {
                    _PvPBoard.SetupSetCardPhase(cardToBeSet);
                }
                else
                {
                    _Board.SetupSetCardPhase(cardToBeSet);
                }

                //Cards on slot 1 and 2 from the rollset must go back to the deck
                _PlayerData.Deck.AddMainCard(_DiceToRoll[0].ID);
                _PlayerData.Deck.AddMainCard(_DiceToRoll[1].ID);

                //Close this form and retrn to the board
                Dispose();
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        private void btnGoToBoard_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //In the board reload the crest counts
                if (_PvPMatch)
                {
                    _PvPBoard.SetupMainPhaseNoSummon();
                }
                else
                {
                    _Board.SetupMainPhaseNoSummon();
                }

                //Return all the cards to the deck
                foreach (CardInfo card in _DiceToRoll)
                {
                    _PlayerData.Deck.AddMainCard(card.ID);
                }

                //Close this form and retrn to the board
                Dispose();
                if (_PvPMatch)
                {
                    _PvPBoard.Show();
                }
                else
                {
                    _Board.Show();
                }
            }));
        }
        #endregion
    }
}