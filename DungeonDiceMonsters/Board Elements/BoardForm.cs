//Joel campos
//9/11/2023
//BoardForm Class

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{   
    public partial class BoardForm : Form
    {
        private enum GameState
        {
            TurnStartMenu,
            BoardViewMode,
            MainPhaseBoard,
            ActionMenuDisplay,
            MovingCard,
            SelectingAttackTarger,
            BattleMenuAttackMode,
            BattleMenuDefenseMode,
            SetCard,
            SummonCard,
            CPUTurn,
        }

        #region Constructors
        public BoardForm(PlayerData Red, PlayerData Blue)
        {
            SoundServer.PlayBackgroundMusic(Song.FreeDuel, true);
            InitializeComponent();
            btnRoll.MouseEnter += OnMouseHoverSound;
            btnViewBoard.MouseEnter += OnMouseHoverSound;
            btnExit.MouseEnter += OnMouseHoverSound;
            btnReturnToTurnMenu.MouseEnter += OnMouseHoverSound;

            //Save Ref to each player's data
            RedData = Red; BlueData = Blue;           
          
            //Initialize the board tiles
            int tileId = 0;
            int Y_Location = 25;
            for (int x = 0; x < 18; x++)
            {
                int X_Location = 2;
                for (int y = 0; y < 13; y++)
                {
                    //Create each inside picture box
                    PictureBox insidePicture = new PictureBox();
                    PanelBoard.Controls.Add(insidePicture);
                    insidePicture.Location = new Point(X_Location + 2, Y_Location + 2);
                    insidePicture.Size = new Size(42, 42);
                    insidePicture.BorderStyle = BorderStyle.None;
                    insidePicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    insidePicture.BackColor = Color.Transparent;
                    insidePicture.Tag = tileId;
                    insidePicture.MouseEnter += OnMouseEnterPicture;
                    insidePicture.MouseLeave += OnMouseLeavePicture;
                    insidePicture.Click += Tile_Click;

                    //Create each border picture box 
                    //(create this one after so it is created "behind" the inside picture
                    PictureBox borderPicture = new PictureBox();
                    PanelBoard.Controls.Add(borderPicture);
                    borderPicture.Location = new Point(X_Location, Y_Location);
                    borderPicture.Size = new Size(46, 46);
                    borderPicture.BorderStyle = BorderStyle.FixedSingle;
                    borderPicture.BackColor = Color.Transparent;

                    //Create the ATK/DEF label
                    Label statsLabel = new Label();
                    PanelBoard.Controls.Add(statsLabel);
                    statsLabel.Location = new Point(X_Location + 2, Y_Location + 34);
                    statsLabel.Size = new Size(42, 10);
                    statsLabel.BorderStyle = BorderStyle.None;
                    statsLabel.ForeColor = Color.White;
                    statsLabel.Font = new Font("Calibri", 6);
                    //statsLabel.Visible = false;
                    statsLabel.TextAlign = ContentAlignment.MiddleCenter;
                    statsLabel.Text = "ID:" + tileId;

                    //create and add a new tile object using the above 2 picture boxes
                    //_Tiles.Add(new Tile(insidePicture, borderPicture, statsLabel, statsLabel2));

                    //update the Tile ID for the next one
                    tileId++;

                    //Move the X-Axis for the next picture
                    X_Location += 47;
                }

                //Move the Y-Axis for the next row of pictures
                Y_Location += 47;
            }

            //Link the tiles together
            foreach (Tile tile in _Tiles)
            {
                //assign north links
                if (tile.ID > 13)
                {
                    Tile linkedTile = _Tiles[tile.ID - 13];
                    tile.SetAdjecentTileLink(Tile.TileDirection.North, linkedTile);
                }

                //assign south links
                if (tile.ID < 216)
                {
                    Tile linkedTile = _Tiles[tile.ID + 13];
                    tile.SetAdjecentTileLink(Tile.TileDirection.South, linkedTile);
                }

                //assign east links
                if (tile.ID != 12 && tile.ID != 25 && tile.ID != 38 && tile.ID != 51 && tile.ID != 64
                    && tile.ID != 77 && tile.ID != 90 && tile.ID != 103 && tile.ID != 116 && tile.ID != 129
                    && tile.ID != 142 && tile.ID != 155 && tile.ID != 168 && tile.ID != 181 && tile.ID != 194
                    && tile.ID != 207 && tile.ID != 220 && tile.ID != 233)
                {
                    Tile linkedTile = _Tiles[tile.ID + 1];
                    tile.SetAdjecentTileLink(Tile.TileDirection.East, linkedTile);
                }

                //assign west links
                if (tile.ID != 0 && tile.ID != 13 && tile.ID != 26 && tile.ID != 39 && tile.ID != 52
                    && tile.ID != 65 && tile.ID != 78 && tile.ID != 91 && tile.ID != 104 && tile.ID != 117
                    && tile.ID != 130 && tile.ID != 143 && tile.ID != 156 && tile.ID != 169 && tile.ID != 182
                    && tile.ID != 195 && tile.ID != 208 && tile.ID != 221)
                {
                    Tile linkedTile = _Tiles[tile.ID - 1];
                    tile.SetAdjecentTileLink(Tile.TileDirection.West, linkedTile);
                }

            }

            //Set the starting tiles for each player
            _Tiles[227].ChangeOwner(PlayerColor.RED);
            _Tiles[6].ChangeOwner(PlayerColor.BLUE);

            //Summon both Symbols: Blue on TIle ID 6 and Red on Tile ID 227
            _RedSymbol = new Card(_CardsOnBoard.Count, RedData.Deck.Symbol, PlayerColor.RED);
            _Tiles[227].SummonCard(_RedSymbol);

            _BlueSymbol = new Card(_CardsOnBoard.Count, BlueData.Deck.Symbol, PlayerColor.BLUE);
            _Tiles[6].SummonCard(_BlueSymbol);

            //Initialize the Player's Info Panels
            LoadPlayersInfo();

            //Set the initial game state
            _CurrentGameState = GameState.TurnStartMenu;
            PanelTurnStartMenu.Visible = true;
        }
        public BoardForm(PlayerData Red, PlayerData Blue, bool test)
        {
            SoundServer.PlayBackgroundMusic(Song.FreeDuel, true);
            InitializeComponent();
            btnRoll.MouseEnter += OnMouseHoverSound;
            btnViewBoard.MouseEnter += OnMouseHoverSound;
            btnReturnToTurnMenu.MouseEnter += OnMouseHoverSound;

            //Save Ref to each player's data
            RedData = Red; BlueData = Blue;

            //more test data
            RedData.AddCrests(Crest.MOV, 10);
            RedData.AddCrests(Crest.ATK, 10);
            RedData.AddCrests(Crest.DEF, 2);
            RedData.AddCrests(Crest.MAG, 7);
            RedData.AddCrests(Crest.TRAP, 1);

            BlueData.AddCrests(Crest.MOV, 12);
            BlueData.AddCrests(Crest.ATK, 6);
            BlueData.AddCrests(Crest.DEF, 2);
            BlueData.AddCrests(Crest.MAG, 9);
            BlueData.AddCrests(Crest.TRAP, 2);

            //Initialize the board tiles
            int tileId = 0;
            int Y_Location = 2;
            for (int x = 0; x < 18; x++)
            {
                int X_Location = 2;
                for (int y = 0; y < 13; y++)
                {
                    //Create each inside picture box
                    PictureBox insidePicture = new PictureBox();
                    PanelBoard.Controls.Add(insidePicture);
                    insidePicture.Location = new Point(X_Location + 2, Y_Location + 2);
                    insidePicture.Size = new Size(42, 42);
                    insidePicture.BorderStyle = BorderStyle.None;
                    insidePicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    insidePicture.BackColor = Color.Transparent;
                    insidePicture.Tag = tileId;
                    insidePicture.MouseEnter += OnMouseEnterPicture;
                    insidePicture.MouseLeave += OnMouseLeavePicture;
                    insidePicture.Click += Tile_Click;

                    //Create each border picture box 
                    //(create this one after so it is created "behind" the inside picture
                    PictureBox borderPicture = new PictureBox();
                    PanelBoard.Controls.Add(borderPicture);
                    borderPicture.Location = new Point(X_Location, Y_Location);
                    borderPicture.Size = new Size(46, 46);
                    borderPicture.BorderStyle = BorderStyle.FixedSingle;
                    borderPicture.BackColor = Color.Transparent;

                    //Create the ATK/DEF label
                    Label statsLabel = new Label();
                    PanelBoard.Controls.Add(statsLabel);
                    statsLabel.Location = new Point(X_Location + 2, Y_Location + 34);
                    statsLabel.Size = new Size(42, 10);
                    statsLabel.BorderStyle = BorderStyle.None;
                    statsLabel.ForeColor = Color.White;
                    statsLabel.Font = new Font("Calibri", 6);
                    //statsLabel.Visible = false;
                    statsLabel.TextAlign = ContentAlignment.MiddleCenter;
                    statsLabel.Text = "ID:" + tileId;

                    //create and add a new tile object using the above 2 picture boxes
                    //_Tiles.Add(new Tile(insidePicture, borderPicture, statsLabel));

                    //update the Tile ID for the next one
                    tileId++;

                    //Move the X-Axis for the next picture
                    X_Location += 47;
                }

                //Move the Y-Axis for the next row of pictures
                Y_Location += 47;
            }

            //Link the tiles together
            foreach (Tile tile in _Tiles)
            {
                //assign north links
                if (tile.ID > 13)
                {
                    Tile linkedTile = _Tiles[tile.ID - 13];
                    tile.SetAdjecentTileLink(Tile.TileDirection.North, linkedTile);
                }

                //assign south links
                if (tile.ID < 216)
                {
                    Tile linkedTile = _Tiles[tile.ID + 13];
                    tile.SetAdjecentTileLink(Tile.TileDirection.South, linkedTile);
                }

                //assign east links
                if (tile.ID != 12 && tile.ID != 25 && tile.ID != 38 && tile.ID != 51 && tile.ID != 64
                    && tile.ID != 77 && tile.ID != 90 && tile.ID != 103 && tile.ID != 116 && tile.ID != 129
                    && tile.ID != 142 && tile.ID != 155 && tile.ID != 168 && tile.ID != 181 && tile.ID != 194
                    && tile.ID != 207 && tile.ID != 220 && tile.ID != 233)
                {
                    Tile linkedTile = _Tiles[tile.ID + 1];
                    tile.SetAdjecentTileLink(Tile.TileDirection.East, linkedTile);
                }

                //assign west links
                if (tile.ID != 0 && tile.ID != 13 && tile.ID != 26 && tile.ID != 39 && tile.ID != 52
                    && tile.ID != 65 && tile.ID != 78 && tile.ID != 91 && tile.ID != 104 && tile.ID != 117
                    && tile.ID != 130 && tile.ID != 143 && tile.ID != 156 && tile.ID != 169 && tile.ID != 182
                    && tile.ID != 195 && tile.ID != 208 && tile.ID != 221)
                {
                    Tile linkedTile = _Tiles[tile.ID - 1];
                    tile.SetAdjecentTileLink(Tile.TileDirection.West, linkedTile);
                }

            }

            //Set the starting tiles for each player
            _Tiles[227].ChangeOwner(PlayerColor.RED);
            _Tiles[6].ChangeOwner(PlayerColor.BLUE);

            //TEST: Set some tiles
            _Tiles[214].ChangeOwner(PlayerColor.RED);
            _Tiles[201].ChangeOwner(PlayerColor.RED);
            _Tiles[188].ChangeOwner(PlayerColor.RED);
            _Tiles[175].ChangeOwner(PlayerColor.RED);
            _Tiles[200].ChangeOwner(PlayerColor.RED);
            _Tiles[202].ChangeOwner(PlayerColor.RED);

            _Tiles[97].ChangeOwner(PlayerColor.RED);
            _Tiles[109].ChangeOwner(PlayerColor.RED);
            _Tiles[110].ChangeOwner(PlayerColor.RED);
            _Tiles[111].ChangeOwner(PlayerColor.RED);
            _Tiles[123].ChangeOwner(PlayerColor.RED);

            _Tiles[19].ChangeOwner(PlayerColor.BLUE);
            _Tiles[32].ChangeOwner(PlayerColor.BLUE);
            _Tiles[33].ChangeOwner(PlayerColor.BLUE);
            _Tiles[34].ChangeOwner(PlayerColor.BLUE);
            _Tiles[35].ChangeOwner(PlayerColor.BLUE);
            _Tiles[48].ChangeOwner(PlayerColor.BLUE);

            _Tiles[31].ChangeOwner(PlayerColor.BLUE);
            _Tiles[44].ChangeOwner(PlayerColor.BLUE);
            _Tiles[43].ChangeOwner(PlayerColor.BLUE);
            _Tiles[42].ChangeOwner(PlayerColor.BLUE);
            _Tiles[41].ChangeOwner(PlayerColor.BLUE);
            _Tiles[40].ChangeOwner(PlayerColor.BLUE);
            _Tiles[53].ChangeOwner(PlayerColor.BLUE);

            //Summon both Symbols: Blue on TIle ID 6 and Red on Tile ID 227
            _RedSymbol = new Card(_CardsOnBoard.Count, RedData.Deck.Symbol, PlayerColor.RED);
            _Tiles[227].SummonCard(_RedSymbol);
            _BlueSymbol = new Card(_CardsOnBoard.Count, BlueData.Deck.Symbol, PlayerColor.BLUE);
            _Tiles[6].SummonCard(_BlueSymbol);

            //test
            _BlueSymbol.ReduceLP(7000);

            //Initialize the Player's Info Panels
            LoadPlayersInfo();

            //TEST Summon a monster to move
            _CurrentTileSelected = _Tiles[201];
            Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(4), PlayerColor.RED, false);
            _CardsOnBoard.Add(thisCard);
            _CurrentTileSelected.SummonCard(thisCard);

            _CurrentTileSelected = _Tiles[35];
            Card thisCard2 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(6), PlayerColor.RED, false);
            _CardsOnBoard.Add(thisCard2);
            _CurrentTileSelected.SummonCard(thisCard2);

            _CurrentTileSelected = _Tiles[48];
            Card thisCard3 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(5), PlayerColor.BLUE, false);
            _CardsOnBoard.Add(thisCard3);
            _CurrentTileSelected.SummonCard(thisCard3);

            _CurrentTileSelected = _Tiles[40];
            Card thisCard4 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(2), PlayerColor.BLUE, false);
            _CardsOnBoard.Add(thisCard4);
            _CurrentTileSelected.SummonCard(thisCard4);

            _CurrentTileSelected = _Tiles[110];
            Card thisCard5 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(10), PlayerColor.RED, true);
            _CardsOnBoard.Add(thisCard5);
            _CurrentTileSelected.SetCard(thisCard5);

            _CurrentTileSelected = _Tiles[31];
            Card thisCard6 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(10), PlayerColor.BLUE, true);
            _CardsOnBoard.Add(thisCard6);
            _CurrentTileSelected.SetCard(thisCard6);

            PanelBoard.Enabled = true;
            UpdateDimensionPreview();
            PanelDimenFormSelector.Visible = true;
            _CurrentGameState = GameState.SummonCard;
        }
        #endregion

        #region Public Methods
        public List<Tile> GetTiles() 
        { 
            return _Tiles; 
        }
        public void SetupMainPhaseNoSummon()
        {
            //Switch to the Main Phase of the player
            _CurrentGameState = GameState.MainPhaseBoard;
            btnEndTurn.Visible = true;

            //Relaod the player info panels to update crests
            LoadPlayersInfo();

            //Enable the Board Panel to interact with it
            PanelBoard.Enabled = true;
        }
        public void SetupCPUMainPhaseNoSummon()
        {
            //Switch to the Main Phase of the CPU
            _CurrentGameState = GameState.CPUTurn;
            PanelTurnStartMenu.Visible = false;

            //Relaod the player info panels to update crests
            LoadPlayersInfo();

            //Enable the Board Panel to interact with it
            PanelBoard.Enabled = true;
        }
        public void StartCPUMainPhaseActions()
        {
            //TODO
            WaitNSeconds(4000);

            //TMP: End turn
            CPUEndTurn();
        }
        public void SetupSummonCardPhase(CardInfo card)
        {
            //Switch to the Summon Card State
            _CurrentGameState = GameState.SummonCard;

            //Save the ref to the data of the card to be summon
            _CardToBeSummon = card;

            //Relaod the player info panels to update crests
            LoadPlayersInfo();

            //Enable the Board Panel to interact with it
            PanelBoard.Enabled = true;

            //Display the Dimension shape selector
            PanelDimenFormSelector.Visible = true;
            lblSummonMessage.Visible = true;
        }
        public void SetupSetCardPhase(CardInfo card)
        {
            //Switch to the Set Card Phase of the player
            _CurrentGameState = GameState.SetCard;

            //Save the ref to the data of the card to be set
            _CardToBeSet = card;

            //Relaod the player info panels to update crests
            LoadPlayersInfo();

            //Enable the Board Panel to interact with it
            PanelBoard.Enabled = true;

            //Setup the tile candidates
            _SetCandidates.Clear();
            //TODO: _SetCandidates = RedData.GetSetCardTileCandidates();
            lblSetCardMessage.Visible = true;
            foreach (Tile tile in _SetCandidates)
            {
                tile.MarkSetTarget();
            }
        }
        public static void WaitNSeconds(double milliseconds)
        {
            if (milliseconds < 1) return;
            DateTime _desired = DateTime.Now.AddMilliseconds(milliseconds);
            while (DateTime.Now < _desired)
            {
                Application.DoEvents();
            }
        }
        #endregion

        private void CPUEndTurn()
        {
            //Change the game state back to turn start menu
            _CurrentGameState = GameState.TurnStartMenu;

            //End of the Player's Turn. No more actions are performed

            //All 1 turn data is reset for all monsters on the board
            //and All non-permanent spellbound counters are reduced.
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsDiscardted)
                {
                    thisCard.ResetOneTurnData();

                    if (thisCard.IsUnderSpellbound && !thisCard.IsPermanentSpellbound)
                    {
                        thisCard.ReduceSpellboundCounter(1);
                    }
                }
            }

            //1 turn effects are removed.
            //TODO

            //Player's Turn starts
            lblTurnStartMessage.Text = "Red's Turn Start";
            btnRoll.Visible = true;
            btnViewBoard.Visible = true;
            PanelTurnStartMenu.Visible = true;
        }

        #region Data
        private GameState _CurrentGameState = GameState.MainPhaseBoard;
        private PlayerData RedData;
        private PlayerData BlueData;
        private List<Tile> _Tiles = new List<Tile>();
        private Tile _CurrentTileSelected = null;
        private List<Card> _CardsOnBoard = new List<Card>();
        private List<Tile> _MoveCandidates = new List<Tile>();
        private Tile _InitialTileMove = null;
        private int _TMPMoveCrestCount = 0;
        private List<Tile> _AttackCandidates = new List<Tile>();
        private Tile _AttackTarger;
        //Battle menu data
        private int _AttackBonusCrest = 0;
        private int _DefenseBonusCrest = 0;
        //Symbols Refs
        private Card _RedSymbol;
        private Card _BlueSymbol;
        //Summoning data
        private List<Tile> _SetCandidates = new List<Tile>();
        private CardInfo _CardToBeSet;
        private CardInfo _CardToBeSummon;
        private DimensionForms _CurrentDimensionForm = DimensionForms.CrossBase;
        private Tile[] _dimensionTiles = new Tile[6];
        private bool _validDimension = false;
        #endregion

        #region Private Methods
        private void LoadPlayersInfo()
        {
            lblRedPlayerName.Text = RedData.Name;
            lblBluePlayerName.Text = BlueData.Name;

            ImageServer.LoadImage(PicBlueSymbol, CardImageType.Symbol, _BlueSymbol.Attribute.ToString());
            ImageServer.LoadImage(PicRedSymbol, CardImageType.Symbol, _RedSymbol.Attribute.ToString());


            lblRedLP.Text = _RedSymbol.LP.ToString();
            lblBlueLP.Text = _BlueSymbol.LP.ToString();

            lblBlueMovCount.Text = BlueData.Crests_MOV.ToString();
            lblBlueAtkCount.Text = BlueData.Crests_ATK.ToString();  
            lblBlueDefCount.Text = BlueData.Crests_DEF.ToString();
            lblBlueMagCount.Text = BlueData.Crests_MAG.ToString();
            lblBlueTrapCount.Text = BlueData.Crests_TRAP.ToString();

            lblRedMovCount.Text = RedData.Crests_MOV.ToString() ;
            lblRedAtkCount.Text = RedData.Crests_ATK.ToString();
            lblRedDefCount.Text = RedData.Crests_DEF.ToString();
            lblRedMagCount.Text = RedData.Crests_MAG.ToString();
            lblRedTrapCount.Text = RedData.Crests_TRAP.ToString();
        }
        private void LoadCardInfoPanel()
        {
            if (_CurrentTileSelected.IsOccupied)
            {
                //Get the CardInfo object to populate the UI
                Card thisCard = _CurrentTileSelected.CardInPlace;

                int cardID = thisCard.CardID;

                //Set the Panel Back Color based on whose the card owner
                if (_CurrentTileSelected.CardInPlace.Owner == PlayerColor.RED)
                {
                    PanelCardInfo.BackColor = Color.DarkRed;
                }
                else
                {
                    PanelCardInfo.BackColor = Color.DarkBlue;
                }

                if(thisCard.IsFaceDown && thisCard.Owner == PlayerColor.BLUE)
                {
                    ImageServer.LoadImage(PicCardArtworkBottom, CardImageType.CardArtwork, "0");

                    lblCardName.Text = string.Empty;
                    lblCardType.Text = string.Empty;
                    lblCardLevel.Text = string.Empty;
                    lblAttribute.Text = string.Empty;      
                    lblStatsATK.Text = string.Empty;
                    lblStatsDEF.Text = string.Empty;
                    lblStatsLP.Text = string.Empty;
                    lblCardText.Text = "Blue's Facedown card.";
                }
                else
                {
                    if(thisCard.IsASymbol)
                    {
                        ImageServer.LoadImage(PicCardArtworkBottom, CardImageType.Symbol, thisCard.Attribute.ToString());

                        lblCardName.Text = thisCard.Owner + "'s " + thisCard.Name;
                        lblCardType.Text = "";
                        lblCardLevel.Text = "";
                        lblAttribute.Text = thisCard.Attribute.ToString();
                        lblStatsATK.Text = "";
                        lblStatsDEF.Text = "";
                        lblStatsLP.Text = thisCard.LP.ToString();
                        lblCardText.Text = thisCard.ContinuousEffect;
                    }
                    else
                    {
                        //Populate the UI
                        ImageServer.LoadImage(PicCardArtworkBottom, CardImageType.CardArtwork, cardID.ToString());

                        lblCardName.Text = thisCard.Name;

                        string secondaryType = thisCard.SecType.ToString();
                        lblCardType.Text = thisCard.TypeAsString + "/" + secondaryType;
                        if (thisCard.Category == Category.Spell) { lblCardType.Text = thisCard.TypeAsString + " spell"; }
                        if (thisCard.Category == Category.Trap) { lblCardType.Text = thisCard.TypeAsString + " trap"; }

                        if (thisCard.Category == Category.Monster) { lblCardLevel.Text = "Lv. " + thisCard.Level; }
                        else { lblCardLevel.Text = ""; }

                        if (thisCard.Category == Category.Monster) { lblAttribute.Text = thisCard.Attribute.ToString(); }
                        else { lblAttribute.Text = ""; }

                        if (thisCard.Category == Category.Monster)
                        {
                            lblStatsATK.Text = thisCard.ATK.ToString();
                            lblStatsDEF.Text = thisCard.DEF.ToString();
                            lblStatsLP.Text = thisCard.LP.ToString();

                            if (thisCard.ATK > thisCard.ATK) { lblStatsATK.ForeColor = Color.Green; }
                            else if (thisCard.ATK < thisCard.ATK) { lblStatsATK.ForeColor = Color.Red; }
                            else { lblStatsATK.ForeColor = Color.White; }

                            if (thisCard.DEF > thisCard.DEF) { lblStatsDEF.ForeColor = Color.Green; }
                            else if (thisCard.DEF < thisCard.DEF) { lblStatsDEF.ForeColor = Color.Red; }
                            else { lblStatsDEF.ForeColor = Color.White; }

                            if (thisCard.LP > thisCard.LP) { lblStatsLP.ForeColor = Color.Green; }
                            else if (thisCard.LP < thisCard.LP) { lblStatsLP.ForeColor = Color.Red; }
                            else { lblStatsLP.ForeColor = Color.White; }
                        }
                        else
                        {
                            lblStatsATK.Text = "";
                            lblStatsDEF.Text = "";
                            lblStatsLP.Text = "";
                        }

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
                    }
                }               
            }
            else
            {
                PanelCardInfo.BackColor = Color.Gray;
                ImageServer.LoadImage(PicCardArtworkBottom, CardImageType.CardArtwork, "0");

                lblCardName.Text = string.Empty;
                lblCardType.Text = string.Empty;
                lblCardLevel.Text = string.Empty;
                lblAttribute.Text = string.Empty;
                lblStatsATK.Text = string.Empty;
                lblStatsDEF.Text = string.Empty;
                lblStatsLP.Text = string.Empty;
                lblCardText.Text = string.Empty;
            }

        }
        private void DisplayMoveCandidates()
        {
            _MoveCandidates.Clear();

            //Display arrows to move
            if (_CurrentTileSelected.HasAnAdjecentTile(Tile.TileDirection.North))
            {
                Tile northTile = _CurrentTileSelected.GetAdjencentTile(Tile.TileDirection.North);
                if (northTile.Owner != PlayerColor.NONE)
                {
                    if (!(northTile.IsOccupied))
                    {
                        //Change the Adjencent tile's border to gree to mark that you can move
                        northTile.MarkFreeToMove();
                        _MoveCandidates.Add(northTile);
                    }
                }
            }

            if (_CurrentTileSelected.HasAnAdjecentTile(Tile.TileDirection.South))
            {
                Tile southTile = _CurrentTileSelected.GetAdjencentTile(Tile.TileDirection.South);
                if (southTile.Owner != PlayerColor.NONE)
                {
                    if (!(southTile.IsOccupied))
                    {
                        //Change the Adjencent tile's border to gree to mark that you can move
                        southTile.MarkFreeToMove();
                        _MoveCandidates.Add(southTile);
                    }
                }
            }

            if (_CurrentTileSelected.HasAnAdjecentTile(Tile.TileDirection.East))
            {
                Tile eastTile = _CurrentTileSelected.GetAdjencentTile(Tile.TileDirection.East);
                if (eastTile.Owner != PlayerColor.NONE)
                {
                    if (!(eastTile.IsOccupied))
                    {
                        //Change the Adjencent tile's border to gree to mark that you can move
                        eastTile.MarkFreeToMove();
                        _MoveCandidates.Add(eastTile);
                    }
                }
            }

            if (_CurrentTileSelected.HasAnAdjecentTile(Tile.TileDirection.West))
            {
                Tile westTile = _CurrentTileSelected.GetAdjencentTile(Tile.TileDirection.West);
                if (westTile.Owner != PlayerColor.NONE)
                {
                    if (!(westTile.IsOccupied))
                    {
                        //Change the Adjencent tile's border to gree to mark that you can move
                        westTile.MarkFreeToMove();
                        _MoveCandidates.Add(westTile);
                    }
                }
            }
        }
        private void DisplayAttackCandidate()
        {
            foreach (Tile tile in _AttackCandidates)
            {
                tile.MarkAttackTarget();
            }
        }
        private void PlaceMoveMenu()
        {
            Point referencePoint = _CurrentTileSelected.Location;
            int X_Location = Cursor.Position.X;
            if (X_Location > 929)
            {
                PanelMoveMenu.Location = new Point(referencePoint.X - 83, referencePoint.Y - 55);
            }
            else
            {
                PanelMoveMenu.Location = new Point(referencePoint.X + 50, referencePoint.Y - 55);
            }
        }
        private void PlaceAttackMenu()
        {
            Point referencePoint = _CurrentTileSelected.Location;
            int X_Location = Cursor.Position.X;
            if (X_Location > 929)
            {
                PanelAttackMenu.Location = new Point(referencePoint.X - 85, referencePoint.Y - 30);
            }
            else
            {
                PanelAttackMenu.Location = new Point(referencePoint.X + 50, referencePoint.Y + 10);
            }
            PanelAttackMenu.Visible = true;
        }
        private void OpenBattleMenuAttackMode()
        {
            PanelBattleMenu.Visible = true;
            btnEndBattle.Visible = false;

            //Set the attacker's data
            Card Attacker = _CurrentTileSelected.CardInPlace;
            ImageServer.LoadImageToPanel(PicAttacker, CardImageType.FullCardImage, Attacker.CardID.ToString());
            lblBattleMenuATALP.Text = "LP: " + Attacker.LP;
            lblAttackerATK.Text = "ATK: " + Attacker.ATK;

            //Set the defender's data. if the defender is a non-monster place the clear data.
            Card Defender = _AttackTarger.CardInPlace;
            if(Defender.Category == Category.Monster)
            {
                ImageServer.LoadImageToPanel(PicDefender2, CardImageType.FullCardImage, Defender.CardID.ToString());
                lblBattleMenuDEFLP.Text = "LP: " + Defender.LP;
                lblDefenderDEF.Text = "DEF: " + Defender.DEF;
            }
            else
            {
                if (Defender.IsASymbol)
                {
                    ImageServer.LoadImageToPanel(PicDefender2, CardImageType.FullCardSymbol, Defender.Attribute.ToString());
                }
                else
                {
                    ImageServer.LoadImageToPanel(PicDefender2, CardImageType.FullCardImage, "0");
                }              
                lblBattleMenuDEFLP.Text = "";
                lblDefenderDEF.Text = "";
            }

            //Hide the "Destroyed" labels just in case
            PicAttackerDestroyed.Visible = false;
            PicDefenderDestroyed.Visible = false;

            //Set the initial Damage Calculation as "?"
            //Damage calculation will be done after both player set their Atk/Def
            //choices (defender has to choose if they defend if can)
            //also both player have the choice to add advantage bonuses if available
            lblBattleMenuDamage.Text = "Damage: ?";

            //Defende/No Defends are not available in attack mode
            btnBattleMenuDefend.Visible = false;
            btnBattleMenuDontDefend.Visible = false;
            PanelDefenderAdvBonus.Visible = false;
            lblDefenderCrestCount.Visible = false;

            //Reset the bonuscrest count for both players just in case
            _AttackBonusCrest = 0;
            _DefenseBonusCrest = 0;

            //Set the Attack button visible in case it wasnt
            btnBattleMenuAttack.Visible = true;
            lblAttackerCrestCount.Text = "Crests to be used: " + (1 + _AttackBonusCrest) + "/" + RedData.Crests_ATK;
            lblAttackerCrestCount.Visible = true;

            //Determine if Attacker has advantage
            bool AttackerHasAdvantage = HasAttributeAdvantage(Attacker, Defender);

            //Display Advantage elements
            if (AttackerHasAdvantage)
            {
                PanelAttackerAdvBonus.Visible = true;
                lblAttackerBonus.Text = "Bonus: 0";
                lblAttackerBonus.Visible = true;
                lblAttackerAdvMinus.Visible = false;
                lblAttackerAdvPlus.Visible = true;
            }
            else
            {
                PanelAttackerAdvBonus.Visible = false;
                lblAttackerBonus.Visible = false;
            }
        }
        private bool HasAttributeAdvantage(Card attacker, Card defender)
        {
            switch (attacker.Attribute)
            {
                case Attribute.LIGHT: if (defender.Attribute == Attribute.DARK) { return true; } else { return false; }
                case Attribute.DARK: if (defender.Attribute == Attribute.LIGHT) { return true; } else { return false; }
                case Attribute.WATER: if (defender.Attribute == Attribute.FIRE) { return true; } else { return false; }
                case Attribute.FIRE: if (defender.Attribute == Attribute.EARTH) { return true; } else { return false; }
                case Attribute.EARTH: if (defender.Attribute == Attribute.WIND) { return true; } else { return false; }
                case Attribute.WIND: if (defender.Attribute == Attribute.WATER) { return true; } else { return false; }
                default: return false;
            }
        }    
        private void UpdateDebugWindow()
        {
            lblDebugTileID.Text = "Tile ID: " + _CurrentTileSelected.ID;
            lblDebugNorthAdj.Text = "North Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(Tile.TileDirection.North);
            lblDebugSouthAdj.Text = "South Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(Tile.TileDirection.South);
            lblDebugEastAdj.Text = "East Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(Tile.TileDirection.East);
            lblDebugWestAdj.Text = "West Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(Tile.TileDirection.West);
            lblDebugOwner.Text = "Owner: " + _CurrentTileSelected.Owner;
            lblDebugIsOccupied.Text = "Occupied: " + _CurrentTileSelected.IsOccupied.ToString();
            if (_CurrentTileSelected.IsOccupied)
            {
                lblDebugCard.Text = "Card: " + _CurrentTileSelected.CardInPlace.CardID + "-Name:" + _CurrentTileSelected.CardInPlace.Name;
                lblDebugCardOwner.Text = "Card Owner: " + _CurrentTileSelected.CardInPlace.Owner;
            }
            else
            {
                lblDebugCard.Text = "Card: No Card In Tile.";
                lblDebugCardOwner.Text = "Card: No Card In Tile.";
            }

            int Y_Location = Cursor.Position.Y;
            int X_Location = Cursor.Position.X;
            lblMouseCords.Text = "Mouse Cords: (" + X_Location + "," + Y_Location + ")";
        }
        private void UpdateDimensionPreview()
        {
            ImageServer.LoadImage(PicCurrentForm, CardImageType.DimensionForm, _CurrentDimensionForm.ToString());
            lblFormName.Text = _CurrentDimensionForm.ToString();
        }
        #endregion

        #region Events
        private void OnMouseHoverSound(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
        }
        private void OnMouseEnterPicture(object sender, EventArgs e)
        {
            if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Hover);
                PictureBox thisPicture = (PictureBox)sender;
                int tileId = Convert.ToInt32(thisPicture.Tag);
                _CurrentTileSelected = _Tiles[tileId];
                _CurrentTileSelected.Hover();

                UpdateDebugWindow();
                LoadCardInfoPanel();
            }
            else if (_CurrentGameState == GameState.SummonCard)
            {
                //Highlight the possible dimmension
                SoundServer.PlaySoundEffect(SoundEffect.Hover);
                PictureBox thisPicture = (PictureBox)sender;
                int tileId = Convert.ToInt32(thisPicture.Tag);

                //Use the following function to get the ref to the tiles that compose the dimension
                _dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                //Check if it is valid or not (it becomes invalid if at least 1 tile is Null AND 
                //if none of the tiles are adjecent to any other owned by the player)
                _validDimension = Dimension.IsThisDimensionValid(_dimensionTiles, PlayerColor.RED);

                //Draw the dimension shape
                _dimensionTiles[0].MarkDimensionSummonTile();
                for (int x = 1; x < _dimensionTiles.Length; x++)
                {
                    if (_dimensionTiles[x] != null) { _dimensionTiles[x].MarkDimensionTile(_validDimension); }
                }
            }

        }
        private void OnMouseLeavePicture(object sender, EventArgs e)
        {
            if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard)
            {
                _CurrentTileSelected.ReloadTileUI();
            }
            else if (_CurrentGameState == GameState.SummonCard)
            {
                //Restore the possible dimmension tiles to their OG colors
                SoundServer.PlaySoundEffect(SoundEffect.Hover);
                PictureBox thisPicture = (PictureBox)sender;
                int tileId = Convert.ToInt32(thisPicture.Tag);

                //Use the following function to get the ref to the tiles that compose the dimension
                Tile[] dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                //Reset the color of the dimensionTiles
                for (int x = 0; x < dimensionTiles.Length; x++)
                {
                    if (dimensionTiles[x] != null) { dimensionTiles[x].ReloadTileUI(); }
                }
            }
        }
        private void Tile_Click(object sender, EventArgs e)
        {
            if (_CurrentGameState == GameState.MainPhaseBoard)
            {
                if (_CurrentTileSelected.IsOccupied)
                {
                    if (_CurrentTileSelected.CardInPlace.Owner == PlayerColor.RED)
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.Click);

                        //Hide the End Turn Button, this wont reappear until the player is done with the action
                        btnEndTurn.Visible = false;

                        //Open the Action menu
                        //Set the location in relation to the Tile location and cursor location
                        Point referencePoint = _CurrentTileSelected.Location;
                        int X_Location = Cursor.Position.X;
                        if (X_Location > 929)
                        {
                            PanelActionMenu.Location = new Point(referencePoint.X - 90, referencePoint.Y - 25);
                        }
                        else
                        {
                            PanelActionMenu.Location = new Point(referencePoint.X + 50, referencePoint.Y - 25);
                        }
                        PanelActionMenu.Visible = true;

                        //Disable the unavailable actions
                        Card thiscard = _CurrentTileSelected.CardInPlace;
                        if (thiscard.MovesAvaiable == 0 || thiscard.MoveCost > RedData.Crests_MOV)
                        {
                            btnActionMove.Enabled = false;
                        }
                        else
                        {
                            btnActionMove.Enabled = true;
                            //Set the temporary mov crest count
                            //this is the value that is going to be used until the move action is finalized.
                            _TMPMoveCrestCount = RedData.Crests_MOV;
                        }

                        //Determine if this card can attack if:
                        // 1. Has not attacked this turn alread
                        // 2. Player has enough atack crest to pay its cost
                        // 3. Has at least 1 adjecent attack candidate
                        // 4. Card is a monster


                        _AttackCandidates = _CurrentTileSelected.GetAttackTargerCandidates(PlayerColor.BLUE);
                        if (thiscard.AttacksAvaiable == 0 || thiscard.AttackCost > RedData.Crests_ATK || _AttackCandidates.Count == 0 || thiscard.Category != Category.Monster)
                        {
                            btnActionAttack.Enabled = false;
                        }
                        else
                        {
                            btnActionAttack.Enabled = true;
                        }
                        //TODO: Effects...
                        btnActionEffect.Enabled = false;

                        //Change the game state
                        _CurrentGameState = GameState.ActionMenuDisplay;
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
            }
            else if (_CurrentGameState == GameState.MovingCard)
            {
                PictureBox thisPicture = (PictureBox)sender;
                int tileId = Convert.ToInt32(thisPicture.Tag);

                //check this tile is one of the candidates
                bool thisIsACandidate = false;
                for (int x = 0; x < _MoveCandidates.Count; x++)
                {
                    if (_MoveCandidates[x].ID == tileId)
                    {
                        thisIsACandidate = true; break;
                    }
                }

                if (thisIsACandidate)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.MoveCard);

                    //Move the card to this location
                    Card thiscard = _CurrentTileSelected.CardInPlace;

                    _Tiles[tileId].MoveInCard(thiscard);
                    _CurrentTileSelected.RemoveCard();

                    //Now clear the borders of all the candidates tiles to their og color
                    for (int x = 0; x < _MoveCandidates.Count; x++)
                    {
                        _MoveCandidates[x].ReloadTileUI();
                    }

                    //Now change the selection to this one tile
                    _CurrentTileSelected.ReloadTileUI();
                    _CurrentTileSelected = _Tiles[tileId];
                    _CurrentTileSelected.Hover();
                    UpdateDebugWindow();

                    //Drecease the available crests to use
                    _TMPMoveCrestCount -= thiscard.MoveCost;
                    lblRedMovCount.Text = "x" + _TMPMoveCrestCount;

                    //Now display the next round of move candidate if there are any MOV crest left.
                    if (thiscard.MoveCost > _TMPMoveCrestCount)
                    {
                        //No more available moves. do no generate more candidates
                        _MoveCandidates.Clear();
                    }
                    else
                    {
                        DisplayMoveCandidates();
                    }

                    PlaceMoveMenu();
                    btnMoveMenuFinish.Enabled = true;
                    btnMoveMenuCancel.Enabled = true;
                }
                else
                {
                    SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                }
            }
            else if (_CurrentGameState == GameState.SelectingAttackTarger)
            {
                PictureBox thisPicture = (PictureBox)sender;
                int tileId = Convert.ToInt32(thisPicture.Tag);

                //check this tile is one of the candidates
                bool thisIsACandidate = false;
                for (int x = 0; x < _AttackCandidates.Count; x++)
                {
                    if (_AttackCandidates[x].ID == tileId)
                    {
                        thisIsACandidate = true;
                        break;
                    }
                }

                if (thisIsACandidate)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Click);

                    //Remove an Attack Available Counter from this card
                    _CurrentTileSelected.CardInPlace.RemoveAttackCounter();

                    //Attack the card in this tile
                    _AttackTarger = _Tiles[tileId];

                    //Close the Attack Menu and clear the color of all attack candidates
                    PanelAttackMenu.Visible = false;
                    foreach (Tile tile in _AttackCandidates)
                    {
                        tile.ReloadTileUI();
                    }
                    _AttackCandidates.Clear();

                    //Open the Battle Panel
                    OpenBattleMenuAttackMode();
                    _CurrentGameState = GameState.BattleMenuAttackMode;
                }
                else
                {
                    SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                }    
            }
            else if (_CurrentGameState == GameState.SetCard)
            {
                PictureBox thisPicture = (PictureBox)sender;
                int tileId = Convert.ToInt32(thisPicture.Tag);

                //check this tile is one of the candidates
                bool thisIsACandidate = false;
                for (int x = 0; x < _SetCandidates.Count; x++)
                {
                    if (_SetCandidates[x].ID == tileId)
                    {
                        thisIsACandidate = true;
                        break;
                    }
                }

                if (thisIsACandidate)
                {
                    //TODO: Add a sounds effect here to set the card

                    //Set Card here
                    Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(_CardToBeSet.ID), PlayerColor.RED, true);
                    _CardsOnBoard.Add(thisCard);
                    _Tiles[tileId].SetCard(thisCard);

                    //Once this action is completed, move to the main phase
                    lblSetCardMessage.Visible = false;
                    _CurrentGameState = GameState.MainPhaseBoard;
                    btnEndTurn.Visible = true;
                }
                else
                {
                    SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                }
            }
            else if (_CurrentGameState == GameState.SummonCard)
            {
                PictureBox thisPicture = (PictureBox)sender;
                int tileId = Convert.ToInt32(thisPicture.Tag);

                if(_validDimension)
                {
                    //TODO: I need a sound effect here 

                    //Dimension the tiles
                    foreach(Tile tile in _dimensionTiles)
                    {
                        tile.ChangeOwner(PlayerColor.RED);
                    }

                    //then summon the card
                    Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(_CardToBeSummon.ID), PlayerColor.RED, false);
                    _CardsOnBoard.Add(thisCard);
                    _Tiles[tileId].SummonCard(thisCard);

                    //Complete the summon
                    lblSummonMessage.Visible = false;
                    PanelDimenFormSelector.Visible = false;
                    _CurrentDimensionForm = DimensionForms.CrossBase;
                    _CurrentTileSelected = _dimensionTiles[0];
                    _CurrentGameState = GameState.MainPhaseBoard;
                    btnEndTurn.Visible = true;
                }
                else
                {
                    SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                }
            }
        }
        private void btnActionCancel_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Close the Action menu/Card info panel and return to the MainPhase Stage 
            _CurrentTileSelected.ReloadTileUI();
            PanelActionMenu.Visible = false;
            _CurrentGameState = GameState.MainPhaseBoard;
            btnEndTurn.Visible = true;
        }
        private void btnActionMove_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            _CurrentGameState = GameState.MovingCard;

            _InitialTileMove = _CurrentTileSelected;

            PanelActionMenu.Visible = false;
            DisplayMoveCandidates();
            PlaceMoveMenu();

            btnMoveMenuFinish.Enabled = false;
            btnMoveMenuCancel.Enabled = true;
            PanelMoveMenu.Visible = true;
        }
        private void btnActionAttack_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            _CurrentGameState = GameState.SelectingAttackTarger;

            DisplayAttackCandidate();
            PlaceAttackMenu();

            PanelActionMenu.Visible = false;
        }
        private void btnMoveMenuCancel_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Now clear the borders of all the candidates tiles to their og color
            for (int x = 0; x < _MoveCandidates.Count; x++)
            {
                _MoveCandidates[x].ReloadTileUI();
            }
            _MoveCandidates.Clear();

            PanelMoveMenu.Visible = false;

            //Return card to the OG spot
            Card thiscard = _CurrentTileSelected.CardInPlace;
            _CurrentTileSelected.ReloadTileUI();
            _CurrentTileSelected.RemoveCard();
            _InitialTileMove.MoveInCard(thiscard);

            //Change the _current Selected card back to OG
            _CurrentTileSelected = _InitialTileMove;
            _CurrentTileSelected.Hover();

            //Reload the Player info panel to reset the crest count
            LoadPlayersInfo();

            UpdateDebugWindow();

            _CurrentGameState = GameState.MainPhaseBoard;
            btnEndTurn.Visible = true;
        }
        private void btnMoveMenuFinish_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Now clear the borders of all the candidates tiles to their og color
            for (int x = 0; x < _MoveCandidates.Count; x++)
            {
                _MoveCandidates[x].ReloadTileUI();
            }
            _MoveCandidates.Clear();

            PanelMoveMenu.Visible = false;

            //Flag that this card moved already this turn
            _CurrentTileSelected.CardInPlace.RemoveMoveCounter();

            //Apply the amoutn of crests used
            int amountUsed = RedData.Crests_MOV - _TMPMoveCrestCount;
            RedData.RemoveCrests(Crest.MOV, amountUsed);
            LoadPlayersInfo();

            _CurrentGameState = GameState.MainPhaseBoard;
            btnEndTurn.Visible = true;
        }
        private void btnAttackMenuCancel_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            //Unmark all the attack candidates
            foreach (Tile tile in _AttackCandidates)
            {
                tile.ReloadTileUI();
            }

            _AttackCandidates.Clear();
            PanelAttackMenu.Visible = false;
            _CurrentGameState = GameState.MainPhaseBoard;
            btnEndTurn.Visible = true;
        }
        private void lblAttackerAdvMinus_Click(object sender, EventArgs e)
        {
            if (_AttackBonusCrest > 0)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _AttackBonusCrest--;
                if (_AttackBonusCrest == 0)
                {
                    lblAttackerAdvMinus.Visible = false;
                }

                lblAttackerAdvPlus.Visible = true;
                lblAttackerBonusCrest.Text = _AttackBonusCrest.ToString();
                lblAttackerBonus.Text = "Bonus: " + (_AttackBonusCrest * 200);
                lblAttackerCrestCount.Text = "Crests to be used: " + (1 + _AttackBonusCrest) + "/" + RedData.Crests_ATK;
            }
        }
        private void lblAttackerAdvPlus_Click(object sender, EventArgs e)
        {
            if(_AttackBonusCrest < 5)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _AttackBonusCrest++;
                if (_AttackBonusCrest == 5 || (RedData.Crests_ATK - (_AttackBonusCrest + 1) == 0))
                {
                    lblAttackerAdvPlus.Visible = false;
                }

                lblAttackerAdvMinus.Visible = true;
                lblAttackerBonusCrest.Text = _AttackBonusCrest.ToString();
                lblAttackerBonus.Text = "Bonus: " + (_AttackBonusCrest * 200);
                lblAttackerCrestCount.Text = "Crests to be used: " + (1 + _AttackBonusCrest) + "/" + RedData.Crests_ATK;
            }
        }
        private void btnBattleMenuAttack_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Attack);
            btnBattleMenuAttack.Visible = false;

            //Hide the plus/Minus advantage buttons 
            lblAttackerAdvMinus.Visible = false;
            lblAttackerAdvPlus.Visible = false;

            //if the card is not a monster simply destroy it
            if(_AttackTarger.CardInPlace.Category == Category.Monster)
            {
                //Check the OpponentIA Object to if it defends or not and if bonus crests are used...
                bool willDefend = false;
                _DefenseBonusCrest = 0;

                if (BlueData.Crests_DEF > 0)
                {
                    willDefend = true;
                }

                //Perform the battle calculation
                int FinalAttack = _CurrentTileSelected.CardInPlace.ATK + (_AttackBonusCrest * 200);
                int FinalDefense = 0;

                if (willDefend)
                {
                    FinalDefense = _AttackTarger.CardInPlace.DEF + (_DefenseBonusCrest * 200);
                }

                //Reduce the Crests used and update player data UI
                int creststoremoveATK = _AttackBonusCrest + 1;
                int creststoremoveDEF = _DefenseBonusCrest + 1;
                RedData.RemoveCrests(Crest.ATK, creststoremoveATK);
                BlueData.RemoveCrests(Crest.DEF, creststoremoveDEF);
                LoadPlayersInfo();

                int Damage = FinalAttack - FinalDefense;
                if (Damage <= 0) 
                {
                    //Display the end battle button
                    lblBattleMenuDamage.Text = "Damage: 0";
                    btnEndBattle.Visible = true;
                }
                else
                {
                    //Update the UI to show the results
                    lblBattleMenuDamage.Text = "Damage: " + Damage;

                    //Reduce the defender's monster'LP
                    int damagetodealtomonster = Damage;
                    if (damagetodealtomonster > _AttackTarger.CardInPlace.LP)
                    {
                        damagetodealtomonster = _AttackTarger.CardInPlace.LP;                       
                    }

                    //Reduce the total damage left
                    Damage -= damagetodealtomonster;

                    //DO the damage animation
                    int iterations = damagetodealtomonster / 10;
                    int waittime = 0;
                    if (iterations < 100) { waittime = 40; }
                    else if (iterations < 200) { waittime = 30; }
                    else if (iterations < 300) { waittime = 10; }
                    else { waittime = 5; }
                    for (int i = 0; i < iterations; i++)
                    {
                        _AttackTarger.CardInPlace.ReduceLP(10);
                        lblBattleMenuDEFLP.Text = "LP: " + _AttackTarger.CardInPlace.LP;
                        SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                        WaitNSeconds(waittime);
                    }

                    //Destroy that monster if the LP of the defender were reduced to 0
                    if(_AttackTarger.CardInPlace.LP == 0)
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.CardDestroyed);
                        PicDefenderDestroyed.Visible = true;
                        WaitNSeconds(1000);
                        //Remove the card from the actual tile

                        _AttackTarger.DestroyCard();
                    }


                    //if there is damage left deal it to the player's symbol
                    if (Damage > 0)
                    {
                        if (Damage > _BlueSymbol.LP) { Damage = _BlueSymbol.LP; }

                        //Deal damage to the player
                        iterations = Damage / 10;

                        waittime = 0;
                        if (iterations < 100) { waittime = 50; }
                        else if (iterations < 200) { waittime = 30; }
                        else if (iterations < 300) { waittime = 10; }
                        else { waittime = 5; }

                        for (int i = 0; i < iterations; i++)
                        {
                            _BlueSymbol.ReduceLP(10);
                            lblBlueLP.Text = _BlueSymbol.LP.ToString();
                            SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                            WaitNSeconds(waittime);
                        }
                        
                        if(_BlueSymbol.LP == 0)
                        {
                            //TODO: defender player loses the game
                            SoundServer.PlayBackgroundMusic(Song.YouWin, true);
                            PanelBattleMenu.Visible = false;
                            PanelEndGameResults.Visible = true;
                            WaitNSeconds(5000);
                            btnExit.Visible = true;
                        }
                        else
                        {
                            //otherwise let the attacker finish the battle phase
                            btnEndBattle.Visible = true;
                        }
                    }
                    else
                    {
                        //Display the end battle button
                        btnEndBattle.Visible = true;
                    }
                }          
            }
            else if (_AttackTarger.CardInPlace.Category == Category.Symbol)
            {
                //Reduce the Symbol's LP and update player info panel

                //Perform the battle calculation
                int FinalAttack = _CurrentTileSelected.CardInPlace.ATK;
                int FinalDefense = 0;

                int Damage = FinalAttack - FinalDefense;
                if (Damage <= 0)
                {
                    //Display the end battle button
                    lblBattleMenuDamage.Text = "Damage: 0";
                    btnEndBattle.Visible = true;
                }
                else
                {
                    if (Damage > _BlueSymbol.LP) { Damage = _BlueSymbol.LP; }

                    //Deal damage to the player
                    int iterations = Damage / 10;

                    int waittime = 0;
                    if (iterations < 100) { waittime = 50; }
                    else if (iterations < 200) { waittime = 30; }
                    else if (iterations < 300) { waittime = 10; }
                    else { waittime = 5; }

                    for (int i = 0; i < iterations; i++)
                    {
                        _BlueSymbol.ReduceLP(10);
                        lblBlueLP.Text = _BlueSymbol.LP.ToString();
                        SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                        WaitNSeconds(waittime);
                    }

                    if (_BlueSymbol.LP == 0)
                    {
                        //TODO: defender player loses the game
                        SoundServer.PlayBackgroundMusic(Song.YouWin, true);
                        PanelBattleMenu.Visible = false;
                        PanelEndGameResults.Visible = true;
                        WaitNSeconds(5000);
                        btnExit.Visible = true;
                    }
                    else
                    {
                        //otherwise let the attacker finish the battle phase
                        btnEndBattle.Visible = true;
                    }
                }
            }
            else
            {
                //Destroy the defender card automatically
                SoundServer.PlaySoundEffect(SoundEffect.CardDestroyed);
                ImageServer.LoadImageToPanel(PicDefender2, CardImageType.FullCardImage, _AttackTarger.CardInPlace.CardID.ToString());
                PicDefenderDestroyed.Visible = true;
                lblBattleMenuDamage.Text = "Damage: 0";
                WaitNSeconds(1000);
                //Remove the card from the actual tile
                _AttackTarger.DestroyCard();

                //Display the end battle button
                btnEndBattle.Visible = true;
            }           
        }
        private void btnEndBattle_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            _CurrentTileSelected.ReloadTileUI();
            PanelBattleMenu.Visible = false;
            _CurrentGameState = GameState.MainPhaseBoard;
            btnEndTurn.Visible = true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Open the Free Duel Form
            FreeDuelMenu FD = new FreeDuelMenu();
            Dispose();
            FD.Show();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void btnRoll_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            RollDiceMenu RD = new RollDiceMenu(RedData, this);
            Hide();

            //Preamble set the board in the Main Phase 
            _CurrentGameState = GameState.MainPhaseBoard;
            PanelTurnStartMenu.Visible = false;

            RD.Show();
        }
        private void btnViewBoard_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Change Game State
            _CurrentGameState = GameState.BoardViewMode;
            PanelBoard.Enabled = true;
            btnReturnToTurnMenu.Visible = true;
            PanelTurnStartMenu.Visible = false;
        }
        private void btnReturnToTurnMenu_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Return to the turn start menui
            _CurrentGameState = GameState.TurnStartMenu;
            PanelBoard.Enabled = false;
            btnReturnToTurnMenu.Visible = false;
            PanelTurnStartMenu.Visible = true;
        }
        private void btnNextForm_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase:
                case DimensionForms.CrossRight:
                case DimensionForms.CrossLeft:
                case DimensionForms.CrossUpSideDown:
                    _CurrentDimensionForm = DimensionForms.LongBase; break;

                case DimensionForms.LongBase:
                case DimensionForms.LongRight:
                case DimensionForms.LongLeft:
                case DimensionForms.LongUpSideDown:
                case DimensionForms.LongFlippedBase:
                case DimensionForms.LongFlippedRight:
                case DimensionForms.LongFlippedLeft:
                case DimensionForms.LongFlippedUpSideDown:
                    _CurrentDimensionForm = DimensionForms.ZBase; break;

                case DimensionForms.ZBase:
                case DimensionForms.ZRight:
                case DimensionForms.ZLeft:
                case DimensionForms.ZUpSideDown:
                case DimensionForms.ZFlippedBase:
                case DimensionForms.ZFlippedRight:
                case DimensionForms.ZFlippedLeft:
                case DimensionForms.ZFlippedUpSideDown:
                    _CurrentDimensionForm = DimensionForms.TBase; break;

                case DimensionForms.TBase:
                case DimensionForms.TRight:
                case DimensionForms.TLeft:
                case DimensionForms.TUpSideDown:
                    _CurrentDimensionForm = DimensionForms.CrossBase; break;
            }

            UpdateDimensionPreview();
        }
        private void btnPreviousForm_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase:
                case DimensionForms.CrossRight:
                case DimensionForms.CrossLeft:
                case DimensionForms.CrossUpSideDown:
                    _CurrentDimensionForm = DimensionForms.TBase; break;

                case DimensionForms.LongBase:
                case DimensionForms.LongRight:
                case DimensionForms.LongLeft:
                case DimensionForms.LongUpSideDown:
                case DimensionForms.LongFlippedBase:
                case DimensionForms.LongFlippedRight:
                case DimensionForms.LongFlippedLeft:
                case DimensionForms.LongFlippedUpSideDown:
                    _CurrentDimensionForm = DimensionForms.CrossBase; break;

                case DimensionForms.ZBase:
                case DimensionForms.ZRight:
                case DimensionForms.ZLeft:
                case DimensionForms.ZUpSideDown:
                case DimensionForms.ZFlippedBase:
                case DimensionForms.ZFlippedRight:
                case DimensionForms.ZFlippedLeft:
                case DimensionForms.ZFlippedUpSideDown:
                    _CurrentDimensionForm = DimensionForms.LongBase; break;

                case DimensionForms.TBase:
                case DimensionForms.TRight:
                case DimensionForms.TLeft:
                case DimensionForms.TUpSideDown:
                    _CurrentDimensionForm = DimensionForms.ZBase; break;
            }

            UpdateDimensionPreview();
        }
        private void btnFormFlip_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase:  break;
                case DimensionForms.CrossRight: _CurrentDimensionForm = DimensionForms.CrossLeft; break;
                case DimensionForms.CrossLeft: _CurrentDimensionForm = DimensionForms.CrossRight; break;
                case DimensionForms.CrossUpSideDown: break;

                case DimensionForms.LongBase: _CurrentDimensionForm = DimensionForms.LongFlippedBase; break;
                case DimensionForms.LongRight: _CurrentDimensionForm = DimensionForms.LongFlippedLeft; break;
                case DimensionForms.LongLeft: _CurrentDimensionForm = DimensionForms.LongFlippedRight; break;
                case DimensionForms.LongUpSideDown: _CurrentDimensionForm = DimensionForms.LongFlippedUpSideDown; break;

                case DimensionForms.LongFlippedBase: _CurrentDimensionForm = DimensionForms.LongBase; break;
                case DimensionForms.LongFlippedRight: _CurrentDimensionForm = DimensionForms.LongLeft; break;
                case DimensionForms.LongFlippedLeft: _CurrentDimensionForm = DimensionForms.LongRight; break;
                case DimensionForms.LongFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.LongUpSideDown; break;

                case DimensionForms.ZBase: _CurrentDimensionForm = DimensionForms.ZFlippedBase; break;
                case DimensionForms.ZRight: _CurrentDimensionForm = DimensionForms.ZFlippedLeft; break;
                case DimensionForms.ZLeft: _CurrentDimensionForm = DimensionForms.ZFlippedRight; break;
                case DimensionForms.ZUpSideDown: _CurrentDimensionForm = DimensionForms.ZFlippedUpSideDown; break;

                case DimensionForms.ZFlippedBase: _CurrentDimensionForm = DimensionForms.ZBase; break;
                case DimensionForms.ZFlippedRight: _CurrentDimensionForm = DimensionForms.ZLeft; break;
                case DimensionForms.ZFlippedLeft: _CurrentDimensionForm = DimensionForms.ZRight; break;
                case DimensionForms.ZFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.ZUpSideDown; break;

                case DimensionForms.TBase:  break;
                case DimensionForms.TRight: _CurrentDimensionForm = DimensionForms.TLeft; break;
                case DimensionForms.TLeft: _CurrentDimensionForm = DimensionForms.TRight; break;
                case DimensionForms.TUpSideDown: break;
            }

            UpdateDimensionPreview();
        }
        private void BtnFormTurnRight_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase: _CurrentDimensionForm = DimensionForms.CrossRight; break;
                case DimensionForms.CrossRight: _CurrentDimensionForm = DimensionForms.CrossUpSideDown; break;
                case DimensionForms.CrossLeft: _CurrentDimensionForm = DimensionForms.CrossBase; break;
                case DimensionForms.CrossUpSideDown: _CurrentDimensionForm = DimensionForms.CrossLeft; break;

                case DimensionForms.LongBase: _CurrentDimensionForm = DimensionForms.LongRight; break;
                case DimensionForms.LongRight: _CurrentDimensionForm = DimensionForms.LongUpSideDown; break;
                case DimensionForms.LongLeft: _CurrentDimensionForm = DimensionForms.LongBase; break;
                case DimensionForms.LongUpSideDown: _CurrentDimensionForm = DimensionForms.LongLeft; break;

                case DimensionForms.LongFlippedBase: _CurrentDimensionForm = DimensionForms.LongFlippedRight; break;
                case DimensionForms.LongFlippedRight: _CurrentDimensionForm = DimensionForms.LongFlippedUpSideDown; break;
                case DimensionForms.LongFlippedLeft: _CurrentDimensionForm = DimensionForms.LongFlippedBase; break;
                case DimensionForms.LongFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.LongFlippedLeft; break;

                case DimensionForms.ZBase: _CurrentDimensionForm = DimensionForms.ZRight; break;
                case DimensionForms.ZRight: _CurrentDimensionForm = DimensionForms.ZUpSideDown; break;
                case DimensionForms.ZLeft: _CurrentDimensionForm = DimensionForms.ZBase; break;
                case DimensionForms.ZUpSideDown: _CurrentDimensionForm = DimensionForms.ZLeft; break;

                case DimensionForms.ZFlippedBase: _CurrentDimensionForm = DimensionForms.ZFlippedRight; break;
                case DimensionForms.ZFlippedRight: _CurrentDimensionForm = DimensionForms.ZFlippedUpSideDown; break;
                case DimensionForms.ZFlippedLeft: _CurrentDimensionForm = DimensionForms.ZFlippedBase; break;
                case DimensionForms.ZFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.ZFlippedLeft; break;

                case DimensionForms.TBase: _CurrentDimensionForm = DimensionForms.TRight; break;
                case DimensionForms.TRight: _CurrentDimensionForm = DimensionForms.TUpSideDown; break;
                case DimensionForms.TLeft: _CurrentDimensionForm = DimensionForms.TBase; break;
                case DimensionForms.TUpSideDown: _CurrentDimensionForm = DimensionForms.TLeft; break;
            }

            UpdateDimensionPreview();
        }
        private void BtnFormTurnLeft_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase: _CurrentDimensionForm = DimensionForms.CrossLeft; break;
                case DimensionForms.CrossRight: _CurrentDimensionForm = DimensionForms.CrossBase; break;
                case DimensionForms.CrossLeft: _CurrentDimensionForm = DimensionForms.CrossUpSideDown; break;
                case DimensionForms.CrossUpSideDown: _CurrentDimensionForm = DimensionForms.CrossRight; break;

                case DimensionForms.LongBase: _CurrentDimensionForm = DimensionForms.LongLeft; break;
                case DimensionForms.LongRight: _CurrentDimensionForm = DimensionForms.LongBase; break;
                case DimensionForms.LongLeft: _CurrentDimensionForm = DimensionForms.LongUpSideDown; break;
                case DimensionForms.LongUpSideDown: _CurrentDimensionForm = DimensionForms.LongRight; break;

                case DimensionForms.LongFlippedBase: _CurrentDimensionForm = DimensionForms.LongLeft; break;
                case DimensionForms.LongFlippedRight: _CurrentDimensionForm = DimensionForms.LongFlippedBase; break;
                case DimensionForms.LongFlippedLeft: _CurrentDimensionForm = DimensionForms.LongFlippedUpSideDown; break;
                case DimensionForms.LongFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.LongFlippedRight; break;

                case DimensionForms.ZBase: _CurrentDimensionForm = DimensionForms.ZLeft; break;
                case DimensionForms.ZRight: _CurrentDimensionForm = DimensionForms.ZBase; break;
                case DimensionForms.ZLeft: _CurrentDimensionForm = DimensionForms.ZUpSideDown; break;
                case DimensionForms.ZUpSideDown: _CurrentDimensionForm = DimensionForms.ZRight; break;

                case DimensionForms.ZFlippedBase: _CurrentDimensionForm = DimensionForms.ZFlippedLeft; break;
                case DimensionForms.ZFlippedRight: _CurrentDimensionForm = DimensionForms.ZFlippedBase; break;
                case DimensionForms.ZFlippedLeft: _CurrentDimensionForm = DimensionForms.ZFlippedUpSideDown; break;
                case DimensionForms.ZFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.ZFlippedRight; break;

                case DimensionForms.TBase: _CurrentDimensionForm = DimensionForms.TLeft; break;
                case DimensionForms.TRight: _CurrentDimensionForm = DimensionForms.TBase; break;
                case DimensionForms.TLeft: _CurrentDimensionForm = DimensionForms.TUpSideDown; break;
                case DimensionForms.TUpSideDown: _CurrentDimensionForm = DimensionForms.TRight; break;
            }

            UpdateDimensionPreview();
        }
        #endregion

        private void btnEndTurn_Click(object sender, EventArgs e)
        {
            //Change the game state and prevent any inputs
            //Player should have no actions enable during the CPU turn.
            _CurrentGameState = GameState.CPUTurn;

            //End of the Player's Turn. No more actions are performed

            //All 1 turn data is reset for all monsters on the board
            //and All non-permanent spellbound counters are reduced.
            foreach (Card thisCard in _CardsOnBoard)
            {
                if(!thisCard.IsDiscardted)
                {
                    thisCard.ResetOneTurnData();

                    if (thisCard.IsUnderSpellbound && !thisCard.IsPermanentSpellbound)
                    {
                        thisCard.ReduceSpellboundCounter(1);
                    }
                }
            }

            //1 turn effects are removed.
            //TODO

            //Opponent's Turn starts
            btnEndTurn.Visible = false;
            lblTurnStartMessage.Text = "Blue's Turn Start";
            btnRoll.Visible = false;
            btnViewBoard.Visible = false;
            PanelTurnStartMenu.Visible = true;
            WaitNSeconds(2000);

            //Open the Roll Dice CPU form
            RollDiceCPU RDC = new RollDiceCPU(BlueData, this);
            Hide();
            RDC.Show();
        }
    }
}