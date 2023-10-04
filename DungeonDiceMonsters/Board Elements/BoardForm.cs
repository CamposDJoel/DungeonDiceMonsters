//Joel campos
//9/11/2023
//BoardForm Class

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public enum GameState
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
    }
    public enum DimensionForms
    {
        //CROSS FORMs
        CrossBase,
        CrossRight,
        CrossLeft,
        CrossUpSideDown,

        //LONG FORM
        LongBase,
        LongRight,
        LongLeft,
        LongUpSideDown,

        //LONG FLIPPED
        LongFlippedBase,
        LongFlippedRight,
        LongFlippedLeft,
        LongFlippedUpSideDown,

        //Z FORM
        ZBase,
        ZRight,
        ZLeft,
        ZUpSideDown,

        //Z FLIPPED
        ZFlippedBase,
        ZFlippedRight,
        ZFlippedLeft,
        ZFlippedUpSideDown,

        //T FORM
        TBase,
        TRight,
        TLeft,
        TUpSideDown,
    }
    public partial class BoardForm : Form
    {
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
                    _Tiles.Add(new Tile(insidePicture, borderPicture, statsLabel));

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
                    tile.SetAdjecentTileLink(TileDirection.North, linkedTile);
                }

                //assign south links
                if (tile.ID < 216)
                {
                    Tile linkedTile = _Tiles[tile.ID + 13];
                    tile.SetAdjecentTileLink(TileDirection.South, linkedTile);
                }

                //assign east links
                if (tile.ID != 12 && tile.ID != 25 && tile.ID != 38 && tile.ID != 51 && tile.ID != 64
                    && tile.ID != 77 && tile.ID != 90 && tile.ID != 103 && tile.ID != 116 && tile.ID != 129
                    && tile.ID != 142 && tile.ID != 155 && tile.ID != 168 && tile.ID != 181 && tile.ID != 194
                    && tile.ID != 207 && tile.ID != 220 && tile.ID != 233)
                {
                    Tile linkedTile = _Tiles[tile.ID + 1];
                    tile.SetAdjecentTileLink(TileDirection.East, linkedTile);
                }

                //assign west links
                if (tile.ID != 0 && tile.ID != 13 && tile.ID != 26 && tile.ID != 39 && tile.ID != 52
                    && tile.ID != 65 && tile.ID != 78 && tile.ID != 91 && tile.ID != 104 && tile.ID != 117
                    && tile.ID != 130 && tile.ID != 143 && tile.ID != 156 && tile.ID != 169 && tile.ID != 182
                    && tile.ID != 195 && tile.ID != 208 && tile.ID != 221)
                {
                    Tile linkedTile = _Tiles[tile.ID - 1];
                    tile.SetAdjecentTileLink(TileDirection.West, linkedTile);
                }

            }

            //Set the starting tiles for each player
            _Tiles[227].ChangeOwner(PlayerOwner.Red);
            _Tiles[6].ChangeOwner(PlayerOwner.Blue);

            //Summon both Symbols: Blue on TIle ID 6 and Red on Tile ID 227
            _RedSymbol = new Card(_CardsOnBoard.Count, RedData.Deck.Symbol, PlayerOwner.Red);
            RedData.AddSummoningTile(_Tiles[227]);
            _Tiles[227].SummonCard(_RedSymbol);

            _BlueSymbol = new Card(_CardsOnBoard.Count, BlueData.Deck.Symbol, PlayerOwner.Blue);
            BlueData.AddSummoningTile(_Tiles[6]);
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
                    _Tiles.Add(new Tile(insidePicture, borderPicture, statsLabel));

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
                    tile.SetAdjecentTileLink(TileDirection.North, linkedTile);
                }

                //assign south links
                if (tile.ID < 216)
                {
                    Tile linkedTile = _Tiles[tile.ID + 13];
                    tile.SetAdjecentTileLink(TileDirection.South, linkedTile);
                }

                //assign east links
                if (tile.ID != 12 && tile.ID != 25 && tile.ID != 38 && tile.ID != 51 && tile.ID != 64
                    && tile.ID != 77 && tile.ID != 90 && tile.ID != 103 && tile.ID != 116 && tile.ID != 129
                    && tile.ID != 142 && tile.ID != 155 && tile.ID != 168 && tile.ID != 181 && tile.ID != 194
                    && tile.ID != 207 && tile.ID != 220 && tile.ID != 233)
                {
                    Tile linkedTile = _Tiles[tile.ID + 1];
                    tile.SetAdjecentTileLink(TileDirection.East, linkedTile);
                }

                //assign west links
                if (tile.ID != 0 && tile.ID != 13 && tile.ID != 26 && tile.ID != 39 && tile.ID != 52
                    && tile.ID != 65 && tile.ID != 78 && tile.ID != 91 && tile.ID != 104 && tile.ID != 117
                    && tile.ID != 130 && tile.ID != 143 && tile.ID != 156 && tile.ID != 169 && tile.ID != 182
                    && tile.ID != 195 && tile.ID != 208 && tile.ID != 221)
                {
                    Tile linkedTile = _Tiles[tile.ID - 1];
                    tile.SetAdjecentTileLink(TileDirection.West, linkedTile);
                }

            }

            //Set the starting tiles for each player
            _Tiles[227].ChangeOwner(PlayerOwner.Red);
            _Tiles[6].ChangeOwner(PlayerOwner.Blue);

            //TEST: Set some tiles
            _Tiles[214].ChangeOwner(PlayerOwner.Red);
            _Tiles[201].ChangeOwner(PlayerOwner.Red);
            _Tiles[188].ChangeOwner(PlayerOwner.Red);
            _Tiles[175].ChangeOwner(PlayerOwner.Red);
            _Tiles[200].ChangeOwner(PlayerOwner.Red);
            _Tiles[202].ChangeOwner(PlayerOwner.Red);

            _Tiles[97].ChangeOwner(PlayerOwner.Red);
            _Tiles[109].ChangeOwner(PlayerOwner.Red);
            _Tiles[110].ChangeOwner(PlayerOwner.Red);
            _Tiles[111].ChangeOwner(PlayerOwner.Red);
            _Tiles[123].ChangeOwner(PlayerOwner.Red);

            _Tiles[19].ChangeOwner(PlayerOwner.Blue);
            _Tiles[32].ChangeOwner(PlayerOwner.Blue);
            _Tiles[33].ChangeOwner(PlayerOwner.Blue);
            _Tiles[34].ChangeOwner(PlayerOwner.Blue);
            _Tiles[35].ChangeOwner(PlayerOwner.Blue);
            _Tiles[48].ChangeOwner(PlayerOwner.Blue);

            _Tiles[31].ChangeOwner(PlayerOwner.Blue);
            _Tiles[44].ChangeOwner(PlayerOwner.Blue);
            _Tiles[43].ChangeOwner(PlayerOwner.Blue);
            _Tiles[42].ChangeOwner(PlayerOwner.Blue);
            _Tiles[41].ChangeOwner(PlayerOwner.Blue);
            _Tiles[40].ChangeOwner(PlayerOwner.Blue);
            _Tiles[53].ChangeOwner(PlayerOwner.Blue);

            //Summon both Symbols: Blue on TIle ID 6 and Red on Tile ID 227
            _RedSymbol = new Card(_CardsOnBoard.Count, RedData.Deck.Symbol, PlayerOwner.Red);
            _Tiles[227].SummonCard(_RedSymbol);
            _BlueSymbol = new Card(_CardsOnBoard.Count, BlueData.Deck.Symbol, PlayerOwner.Blue);
            _Tiles[6].SummonCard(_BlueSymbol);

            //test
            _BlueSymbol.ReduceLP(7000);

            //Initialize the Player's Info Panels
            LoadPlayersInfo();

            //TEST Summon a monster to move
            _CurrentTileSelected = _Tiles[201];
            Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(4), PlayerOwner.Red, false);
            _CardsOnBoard.Add(thisCard);
            _CurrentTileSelected.SummonCard(thisCard);

            _CurrentTileSelected = _Tiles[35];
            Card thisCard2 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(6), PlayerOwner.Red, false);
            _CardsOnBoard.Add(thisCard2);
            _CurrentTileSelected.SummonCard(thisCard2);

            _CurrentTileSelected = _Tiles[48];
            Card thisCard3 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(5), PlayerOwner.Blue, false);
            _CardsOnBoard.Add(thisCard3);
            _CurrentTileSelected.SummonCard(thisCard3);

            _CurrentTileSelected = _Tiles[40];
            Card thisCard4 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(2), PlayerOwner.Blue, false);
            _CardsOnBoard.Add(thisCard4);
            _CurrentTileSelected.SummonCard(thisCard4);

            _CurrentTileSelected = _Tiles[110];
            Card thisCard5 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(10), PlayerOwner.Red, true);
            _CardsOnBoard.Add(thisCard5);
            _CurrentTileSelected.SetCard(thisCard5);

            _CurrentTileSelected = _Tiles[31];
            Card thisCard6 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(10), PlayerOwner.Blue, true);
            _CardsOnBoard.Add(thisCard6);
            _CurrentTileSelected.SetCard(thisCard6);

            PanelBoard.Enabled = true;
            UpdateDimensionPreview();
            PanelDimenFormSelector.Visible = true;
            _CurrentGameState = GameState.SummonCard;
        }
        #endregion

        #region Public Methods
        public void SetupMainPhaseNoSummon()
        {
            //Switch to the Main Phase of the player
            _CurrentGameState = GameState.MainPhaseBoard;

            //Relaod the player info panels to update crests
            LoadPlayersInfo();

            //Enable the Board Panel to interact with it
            PanelBoard.Enabled = true;
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
            _SetCandidates = RedData.GetSetCardTileCandidates();
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

            PicBlueSymbol.Image = ImageServer.Symbol(_BlueSymbol.Attribute);
            PicRedSymbol.Image = ImageServer.Symbol(_RedSymbol.Attribute);

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
                if (_CurrentTileSelected.CardInPlace.Owner == PlayerOwner.Red)
                {
                    PanelCardInfo.BackColor = Color.DarkRed;
                }
                else
                {
                    PanelCardInfo.BackColor = Color.DarkBlue;
                }

                if(thisCard.IsFaceDown && thisCard.Owner == PlayerOwner.Blue)
                {
                    if (PicCardArtworkBottom.Image != null) { PicCardArtworkBottom.Image.Dispose(); }
                    PicCardArtworkBottom.Image = ImageServer.CardArtworkImage(0);

                    lblCardName.Text = "";
                    lblCardType.Text = "";
                    lblCardLevel.Text = "";
                    lblAttribute.Text = "";
                    lblStatsATK.Text = "";
                    lblStatsDEF.Text = "";
                    lblStatsLP.Text = "";
                    lblCardText.Text = "Blue's Facedown card.";
                }
                else
                {
                    if(thisCard.IsASymbol)
                    {
                        if (PicCardArtworkBottom.Image != null) { PicCardArtworkBottom.Image.Dispose(); }
                        PicCardArtworkBottom.Image = ImageServer.Symbol(thisCard.Attribute);

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
                        if (PicCardArtworkBottom.Image != null) { PicCardArtworkBottom.Image.Dispose(); }
                        PicCardArtworkBottom.Image = ImageServer.CardArtworkImage(cardID);

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

                if (PicCardArtworkBottom.Image != null) { PicCardArtworkBottom.Image.Dispose(); }
                PicCardArtworkBottom.Image = ImageServer.CardArtworkImage(0);

                lblCardName.Text = "";
                lblCardType.Text = "";
                lblCardLevel.Text = "";
                lblAttribute.Text = "";
                lblStatsATK.Text = "";
                lblStatsDEF.Text = "";
                lblStatsLP.Text = "";
                lblCardText.Text = "";
            }

        }
        private void DisplayMoveCandidates()
        {
            _MoveCandidates.Clear();

            //Display arrows to move
            if (_CurrentTileSelected.HasAnAdjecentTile(TileDirection.North))
            {
                Tile northTile = _CurrentTileSelected.GetAdjencentTile(TileDirection.North);
                if (northTile.Owner != PlayerOwner.None)
                {
                    if (!(northTile.IsOccupied))
                    {
                        //Change the Adjencent tile's border to gree to mark that you can move
                        northTile.MarkFreeToMove();
                        _MoveCandidates.Add(northTile);
                    }
                }
            }

            if (_CurrentTileSelected.HasAnAdjecentTile(TileDirection.South))
            {
                Tile southTile = _CurrentTileSelected.GetAdjencentTile(TileDirection.South);
                if (southTile.Owner != PlayerOwner.None)
                {
                    if (!(southTile.IsOccupied))
                    {
                        //Change the Adjencent tile's border to gree to mark that you can move
                        southTile.MarkFreeToMove();
                        _MoveCandidates.Add(southTile);
                    }
                }
            }

            if (_CurrentTileSelected.HasAnAdjecentTile(TileDirection.East))
            {
                Tile eastTile = _CurrentTileSelected.GetAdjencentTile(TileDirection.East);
                if (eastTile.Owner != PlayerOwner.None)
                {
                    if (!(eastTile.IsOccupied))
                    {
                        //Change the Adjencent tile's border to gree to mark that you can move
                        eastTile.MarkFreeToMove();
                        _MoveCandidates.Add(eastTile);
                    }
                }
            }

            if (_CurrentTileSelected.HasAnAdjecentTile(TileDirection.West))
            {
                Tile westTile = _CurrentTileSelected.GetAdjencentTile(TileDirection.West);
                if (westTile.Owner != PlayerOwner.None)
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
            if(PicAttacker.BackgroundImage != null) { PicAttacker.BackgroundImage.Dispose(); }            
            PicAttacker.BackgroundImage = null;
            PicAttacker.BackgroundImage = ImageServer.FullCardImage(Attacker.CardID);
            lblBattleMenuATALP.Text = "LP: " + Attacker.LP;
            lblAttackerATK.Text = "ATK: " + Attacker.ATK;

            //Set the defender's data. if the defender is a non-monster place the clear data.
            Card Defender = _AttackTarger.CardInPlace;
            if(Defender.Category == Category.Monster)
            {
                if (PicDefender2.BackgroundImage != null) { PicDefender2.BackgroundImage.Dispose(); }
                PicDefender2.BackgroundImage = null;
                PicDefender2.BackgroundImage = ImageServer.FullCardImage(Defender.CardID);
                lblBattleMenuDEFLP.Text = "LP: " + Defender.LP;
                lblDefenderDEF.Text = "DEF: " + Defender.DEF;
            }
            else
            {
                if (PicDefender2.BackgroundImage != null) { PicDefender2.BackgroundImage.Dispose(); }
                PicDefender2.BackgroundImage = null;
                if (Defender.IsASymbol)
                {
                    PicDefender2.BackgroundImage = ImageServer.FullCardSymbol(Defender.Attribute);
                }
                else
                {
                    PicDefender2.BackgroundImage = ImageServer.FullCardImage(0);
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
            lblDebugNorthAdj.Text = "North Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(TileDirection.North);
            lblDebugSouthAdj.Text = "South Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(TileDirection.South);
            lblDebugEastAdj.Text = "East Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(TileDirection.East);
            lblDebugWestAdj.Text = "West Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(TileDirection.West);
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
        private Tile[] GetDimensionTiles(int BaseTileID)
        {
            Tile[] tiles = new Tile[6];

            switch(_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North});
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 West
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 South
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 5: 1 South, 1 South
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South });
                    break;
                case DimensionForms.CrossRight:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 West
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 West, 1 West
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 5: 1 South
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    break;
                case DimensionForms.CrossLeft:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 East, 1 East
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 4: 1 West
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 5: 1 South
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    break;
                case DimensionForms.CrossUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 North, 1 North
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 3: 1 East
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 4: 1 West
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 5: 1 South
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    break;
                case DimensionForms.LongBase:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 North, 1 North
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 3: 1 West
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 West, 1 South
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.South });
                    //Tile 5: 1 West, 1 South , 1 South
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.South, TileDirection.South});
                    break;
                case DimensionForms.LongRight:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 East, 1 East
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 4: 1 North, 1 West
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.West });
                    //Tile 5: 1 Noth, 1 West, 1 West
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.West, TileDirection.West });
                    break;
                case DimensionForms.LongLeft:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 West, 1 West
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 3: 1 South
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 4: 1 South, 1 East
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.East });
                    //Tile 5: 1 South, 1 East, 1 East
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.East, TileDirection.East });
                    break;
                case DimensionForms.LongUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 East
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 2: 1 East, 1 North
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.North });
                    //Tile 3: 1 East, 1 North, 1 North
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.North, TileDirection.North });
                    //Tile 4: 1 South
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 5: 1 South, 1 South
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South });
                    break;
                case DimensionForms.LongFlippedBase:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 North, 1 North
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 3: 1 East
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 4: 1 East, 1 South
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.South });
                    //Tile 5: 1 East, 1 South 1 south
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.South, TileDirection.South });
                    break;
                case DimensionForms.LongFlippedRight:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 East
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 2: 1 East, 1 East
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 3: 1 South
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 4: 1 South, 1 West
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.West });
                    //Tile 5: 1 South, 1 West, 1 West
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.West, TileDirection.West });
                    break;
                case DimensionForms.LongFlippedLeft:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 West, 1 West
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 3: 1 North
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 4: 1 North, 1 East
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.East });
                    //Tile 5: 1 North, 1 East, 1 East
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.East, TileDirection.East });
                    break;
                case DimensionForms.LongFlippedUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 South
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 2: 1 South, 1 South
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South });
                    //Tile 3: 1 West
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 West, 1 North
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.North });
                    //Tile 5: 1 West, 1 North, 1 North
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.North, TileDirection.North });
                    break;
                case DimensionForms.ZBase:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 East
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 2: 1 South
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 3: 1 South 1 South
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South });
                    //Tile 4: 1 South 1 South 1 South
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South });
                    //Tile 5: 1 South 1 South 1 South 1 West
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.West });
                    break;
                case DimensionForms.ZRight:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 South
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 2: 1 West
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 3: 1 West 1 West
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 4: 1 West 1 West 1 West
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West });
                    //Tile 5: 1 West 1 West 1 West 1 Noth
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West, TileDirection.North });
                    break;
                case DimensionForms.ZLeft:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 East 1 East
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 4: 1 East 1 East 1 East
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East });
                    //Tile 5: 1 East 1 East 1 East 1 South
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East, TileDirection.South });
                    break;
                case DimensionForms.ZUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 North
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 3: 1 North 1 North
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 4: 1 North 1 North 1 North
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North });
                    //Tile 5: 1 North 1 North 1 North 1 East
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.East });
                    break;
                case DimensionForms.ZFlippedBase:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 South
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 3: 1 South 1 South
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South });
                    //Tile 4: 1 South 1 South 1 South
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South });
                    //Tile 5: 1 South 1 South 1 South 1 East
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.East });
                    break;
                case DimensionForms.ZFlippedRight:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 West
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 3: 1 West 1 West
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 4: 1 West 1 West 1 West
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West });
                    //Tile 5: 1 West 1 West 1 West 1 South
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West, TileDirection.South });
                    break;
                case DimensionForms.ZFlippedLeft:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 South
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 East 1 East
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 4: 1 East 1 East 1 East
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East });
                    //Tile 5: 1 East 1 East 1 East 1 North
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East, TileDirection.North });
                    break;
                case DimensionForms.ZFlippedUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 North
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 3: 1 North 1 North
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 4: 1 North 1 North 1 North
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North });
                    //Tile 5: 1 North 1 North 1 North 1 East
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.East });
                    break;
                case DimensionForms.TBase:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 South 
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 4: 1 South 1 South
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South });
                    //Tile 5: 1 South 1 South 1 South
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South });
                    break;
                case DimensionForms.TRight:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 South
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 3: 1 West 
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 West  1 West 
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 5: 1 West  1 West  1 West 
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West});
                    break;
                case DimensionForms.TLeft:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 South
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.South });
                    //Tile 3: 1 East 
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 4: 1 East  1 East 
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 5: 1 East  1 East  1 East
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East });
                    break;
                case DimensionForms.TUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = _Tiles[BaseTileID];
                    //Tile 1: 1 East
                    tiles[1] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.East });
                    //Tile 2: 1 West
                    tiles[2] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.West });
                    //Tile 3: 1 North 
                    tiles[3] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North });
                    //Tile 4: 1 North  1 North
                    tiles[4] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 5: 1 North 1 North  1 North
                    tiles[5] = GetTileInDirection(_Tiles[BaseTileID], new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North });
                    break;
                default: throw new Exception("GetDimensionTiles function took an invalid dimension form value: " + _CurrentDimensionForm);
            }


            return tiles;
        }
        private Tile GetTileInDirection(Tile baseTile, List<TileDirection> directions)
        {
            Tile destinationTile = baseTile;
            for(int x = 0; x < directions.Count; x++) 
            {
                destinationTile = destinationTile.GetAdjencentTile(directions[x]);
                if (destinationTile == null) { break; }
            }
            return destinationTile;
        }
        private void UpdateDimensionPreview()
        {
            //if (PicCurrentForm.Image != null) { PicCurrentForm.Dispose(); }
            PicCurrentForm.Image = ImageServer.DimensionForm(_CurrentDimensionForm);

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
                _dimensionTiles = GetDimensionTiles(tileId);

                //Check if it is valid or not (it becomes invalid if at least 1 tile is Null AND 
                //if none of the tiles are adjecent to any other owned by the player)
                _validDimension = true;
                int adjencentToPlayerCount = 0;
                for (int x = 0; x < _dimensionTiles.Length; x++)
                {
                    if (_dimensionTiles[x] == null)
                    {
                        _validDimension = false; break;
                    }
                    else
                    {
                        if (_dimensionTiles[x].Owner != PlayerOwner.None)
                        {
                            _validDimension = false; break;
                        }
                        else
                        {
                            //check that at least ONE tile is adjecend to a own tile of the player
                            bool IsAdjecentToPlayer = _dimensionTiles[x].HasAnAdjecentTileOwnBy(PlayerOwner.Red);
                            if (IsAdjecentToPlayer) { adjencentToPlayerCount++; }
                        }
                    }
                }
                if (adjencentToPlayerCount == 0) { _validDimension = false; }

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
                _CurrentTileSelected.Leave();
            }
            else if (_CurrentGameState == GameState.SummonCard)
            {
                //Restore the possible dimmension tiles to their OG colors
                SoundServer.PlaySoundEffect(SoundEffect.Hover);
                PictureBox thisPicture = (PictureBox)sender;
                int tileId = Convert.ToInt32(thisPicture.Tag);

                //Use the following function to get the ref to the tiles that compose the dimension
                Tile[] dimensionTiles = GetDimensionTiles(tileId);

                //Reset the color of the dimensionTiles
                for (int x = 0; x < dimensionTiles.Length; x++)
                {
                    if (dimensionTiles[x] != null) { dimensionTiles[x].SetTileColor(); }
                }
            }
        }
        private void Tile_Click(object sender, EventArgs e)
        {
            if (_CurrentGameState == GameState.MainPhaseBoard)
            {
                if (_CurrentTileSelected.IsOccupied)
                {
                    if (_CurrentTileSelected.CardInPlace.Owner == PlayerOwner.Red)
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.Click);

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
                        if (thiscard.MovedThisTurn || thiscard.MoveCost > RedData.Crests_MOV)
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


                        _AttackCandidates = _CurrentTileSelected.GetAttackTargerCandidates(PlayerOwner.Blue);
                        if (thiscard.AttackedThisTurn || thiscard.AttackCost > RedData.Crests_ATK || _AttackCandidates.Count == 0 || thiscard.Category != Category.Monster)
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
                        _MoveCandidates[x].SetTileColor();
                    }

                    //Now change the selection to this one tile
                    _CurrentTileSelected.Leave();
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

                    //Flag the attecker used its attack of the turn
                    _CurrentTileSelected.CardInPlace.AttackedThisTurn = true;

                    //Attack the card in this tile
                    _AttackTarger = _Tiles[tileId];

                    //Close the Attack Menu and clear the color of all attack candidates
                    PanelAttackMenu.Visible = false;
                    foreach (Tile tile in _AttackCandidates)
                    {
                        tile.SetTileColor();
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
                    Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(_CardToBeSet.ID), PlayerOwner.Red, true);
                    _CardsOnBoard.Add(thisCard);
                    _Tiles[tileId].SetCard(thisCard);

                    //Once this action is completed, move to the main phase
                    lblSetCardMessage.Visible = false;
                    _CurrentGameState = GameState.MainPhaseBoard;
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
                        tile.ChangeOwner(PlayerOwner.Red);
                    }

                    //then summon the card
                    Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(_CardToBeSummon.ID), PlayerOwner.Red, false);
                    _CardsOnBoard.Add(thisCard);
                    _Tiles[tileId].SummonCard(thisCard);

                    //Complete the summon
                    lblSummonMessage.Visible = false;
                    PanelDimenFormSelector.Visible = false;
                    _CurrentDimensionForm = DimensionForms.CrossBase;
                    _CurrentTileSelected = _dimensionTiles[0];
                    _CurrentGameState = GameState.MainPhaseBoard;
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
            _CurrentTileSelected.Leave();
            PanelActionMenu.Visible = false;
            _CurrentGameState = GameState.MainPhaseBoard;
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
                _MoveCandidates[x].SetTileColor();
            }
            _MoveCandidates.Clear();

            PanelMoveMenu.Visible = false;

            //Return card to the OG spot
            Card thiscard = _CurrentTileSelected.CardInPlace;
            _CurrentTileSelected.Leave();
            _CurrentTileSelected.RemoveCard();
            _InitialTileMove.MoveInCard(thiscard);

            //Change the _current Selected card back to OG
            _CurrentTileSelected = _InitialTileMove;
            _CurrentTileSelected.Hover();

            //Reload the Player info panel to reset the crest count
            LoadPlayersInfo();

            UpdateDebugWindow();

            _CurrentGameState = GameState.MainPhaseBoard;
        }
        private void btnMoveMenuFinish_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Now clear the borders of all the candidates tiles to their og color
            for (int x = 0; x < _MoveCandidates.Count; x++)
            {
                _MoveCandidates[x].SetTileColor();
            }
            _MoveCandidates.Clear();

            PanelMoveMenu.Visible = false;

            //Flag that this card moved already this turn
            Card thiscard = _CurrentTileSelected.CardInPlace;
            thiscard.MovedThisTurn = true;

            //Apply the amoutn of crests used
            int amountUsed = RedData.Crests_MOV - _TMPMoveCrestCount;
            RedData.RemoveCrests(Crest.MOV, amountUsed);
            LoadPlayersInfo();

            _CurrentGameState = GameState.MainPhaseBoard;
        }
        private void btnAttackMenuCancel_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            //Unmark all the attack candidates
            foreach (Tile tile in _AttackCandidates)
            {
                tile.SetTileColor();
            }

            _AttackCandidates.Clear();
            PanelAttackMenu.Visible = false;
            _CurrentGameState = GameState.MainPhaseBoard;
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
                PicDefender2.BackgroundImage = ImageServer.FullCardImage(_AttackTarger.CardInPlace.CardID);
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
            _CurrentTileSelected.Leave();
            PanelBattleMenu.Visible = false;
            _CurrentGameState = GameState.MainPhaseBoard;
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
    }
}