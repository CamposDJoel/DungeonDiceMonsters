//Joel Campos
//4/2/2024
//BoardPvP Form

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class BoardPvP : Form
    {
        #region Constructors
        public BoardPvP(PlayerData Red, PlayerData Blue, PlayerColor UserColor, NetworkStream tmpns, PvPMenu pvpmenu)
        {
            //Save a reference to the NetworkStream of our active client connection to send messages to the server
            ns = tmpns;
            _PvPMenuRef = pvpmenu;

            //Initialize Music
            SoundServer.PlayBackgroundMusic(Song.FreeDuel, true);

            //Initalize Compents
            InitializeComponent();

            //Set Custom Event Listener
            btnRoll.MouseEnter += OnMouseHoverSound;
            btnViewBoard.MouseEnter += OnMouseHoverSound;
            btnExit.MouseEnter += OnMouseHoverSound;
            btnReturnToTurnMenu.MouseEnter += OnMouseHoverSound;

            //Save Ref to each player's data
            UserPlayerColor = UserColor;
            RedData = Red; BlueData = Blue;

            //Initialize the board tiles
            int tileId = 0;
            int Y_Location = 25;
            int TILESIDE_SIZE = 48;
            int CARDIMAGE_SIZE = TILESIDE_SIZE - 6;
            for (int x = 0; x < 18; x++)
            {
                int X_Location = 2;
                for (int y = 0; y < 13; y++)
                {
                    //Create the ATK/DEF label
                    Label statsLabelDEF = new Label();
                    PanelBoard.Controls.Add(statsLabelDEF);
                    statsLabelDEF.AutoSize = false;
                    statsLabelDEF.Location = new Point(X_Location + 23, Y_Location + 34);
                    statsLabelDEF.Size = new Size(22, 11);
                    statsLabelDEF.BorderStyle = BorderStyle.None;
                    statsLabelDEF.ForeColor = Color.White;
                    statsLabelDEF.Font = new Font("Calibri", 6, FontStyle.Bold);
                    statsLabelDEF.TextAlign = ContentAlignment.TopLeft;
                    statsLabelDEF.Text = "9999";
                    statsLabelDEF.Visible = false;

                    Label statsLabelATK = new Label();
                    PanelBoard.Controls.Add(statsLabelATK);
                    statsLabelATK.AutoSize = false;
                    statsLabelATK.Location = new Point(X_Location + 3, Y_Location + 34);
                    statsLabelATK.Size = new Size(22, 11);
                    statsLabelATK.BorderStyle = BorderStyle.None;
                    statsLabelATK.ForeColor = Color.White;
                    statsLabelATK.Font = new Font("Calibri", 6, FontStyle.Bold);
                    statsLabelATK.TextAlign = ContentAlignment.TopLeft;
                    statsLabelATK.Text = "9999";
                    statsLabelATK.Visible = false;

                    //Create each inside picture box
                    PictureBox insidePicture = new PictureBox();
                    PanelBoard.Controls.Add(insidePicture);
                    insidePicture.Location = new Point(X_Location + 3, Y_Location + 3);
                    insidePicture.Size = new Size(CARDIMAGE_SIZE, CARDIMAGE_SIZE);
                    insidePicture.BorderStyle = BorderStyle.None;
                    insidePicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    insidePicture.BackColor = System.Drawing.Color.Transparent;
                    insidePicture.Tag = tileId;
                    insidePicture.MouseEnter += OnMouseEnterPicture;
                    insidePicture.MouseLeave += OnMouseLeavePicture;
                    insidePicture.Click += Tile_Click;

                    //Create each border picture box 
                    //(create this one after so it is created "behind" the inside picture
                    PictureBox borderPicture = new PictureBox();
                    PanelBoard.Controls.Add(borderPicture);
                    borderPicture.Location = new Point(X_Location, Y_Location);
                    borderPicture.Size = new Size(TILESIDE_SIZE, TILESIDE_SIZE);
                    borderPicture.BorderStyle = BorderStyle.FixedSingle;
                    borderPicture.BackColor = Color.Transparent;

                    //create and add a new tile object using the above 2 picture boxes
                    _Tiles.Add(new Tile(insidePicture, borderPicture, statsLabelATK, statsLabelDEF));

                    //update the Tile ID for the next one
                    tileId++;

                    //Move the X-Axis for the next picture
                    X_Location += TILESIDE_SIZE;
                }

                //Move the Y-Axis for the next row of pictures
                Y_Location += TILESIDE_SIZE;
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
            _CardsOnBoard.Add(_RedSymbol);
            _Tiles[227].SummonCard(_RedSymbol);

            _BlueSymbol = new Card(_CardsOnBoard.Count, BlueData.Deck.Symbol, PlayerColor.BLUE);
            _CardsOnBoard.Add(_BlueSymbol);
            _Tiles[6].SummonCard(_BlueSymbol);

            //Initialize the Player's Info Panels
            LoadPlayersInfo();

            //Load the CardInfo Panel
            _CurrentTileSelected = _Tiles[0];
            LoadCardInfoPanel();

            //Set the initial game state and start the turn start panel            
            LaunchTurnStartPanel();

            //Initialize both Symbols Continuous Effect
            Effect RedsymbolEffect = new Effect(_RedSymbol, Effect.EffectType.Continuous);
            ActivateEffect(RedsymbolEffect);
            _ActiveEffects.Add(RedsymbolEffect);
            Effect BluesymbolEffect = new Effect(_BlueSymbol, Effect.EffectType.Continuous);
            ActivateEffect(BluesymbolEffect);
            _ActiveEffects.Add(BluesymbolEffect);
        }
        public BoardPvP(PlayerData Red, PlayerData Blue, PlayerColor UserColor, NetworkStream tmpns, PvPMenu pvpmenu, bool testing)
        {
            //Save a reference to the NetworkStream of our active client connection to send messages to the server
            ns = tmpns;
            _PvPMenuRef = pvpmenu;

            //Initialize Music
            SoundServer.PlayBackgroundMusic(Song.FreeDuel, true);

            //Initalize Compents
            InitializeComponent();

            //Set Custom Event Listener
            btnRoll.MouseEnter += OnMouseHoverSound;
            btnViewBoard.MouseEnter += OnMouseHoverSound;
            btnExit.MouseEnter += OnMouseHoverSound;
            btnReturnToTurnMenu.MouseEnter += OnMouseHoverSound;

            //Save Ref to each player's data
            UserPlayerColor = UserColor;
            RedData = Red; BlueData = Blue;

            //Initialize the board tiles
            int tileId = 0;
            int Y_Location = 25;
            int TILESIDE_SIZE = 48;
            int CARDIMAGE_SIZE = TILESIDE_SIZE - 6;
            for (int x = 0; x < 18; x++)
            {
                int X_Location = 2;
                for (int y = 0; y < 13; y++)
                {
                    //Create the ATK/DEF label
                    Label statsLabelDEF = new Label();
                    PanelBoard.Controls.Add(statsLabelDEF);
                    statsLabelDEF.AutoSize = false;
                    statsLabelDEF.Location = new Point(X_Location + 23, Y_Location + 34);
                    statsLabelDEF.Size = new Size(22, 11);
                    statsLabelDEF.BorderStyle = BorderStyle.None;
                    statsLabelDEF.ForeColor = Color.White;
                    statsLabelDEF.Font = new Font("Calibri", 6, FontStyle.Bold);
                    statsLabelDEF.TextAlign = ContentAlignment.TopLeft;
                    statsLabelDEF.Text = "9999";
                    statsLabelDEF.Visible = false;

                    Label statsLabelATK = new Label();
                    PanelBoard.Controls.Add(statsLabelATK);
                    statsLabelATK.AutoSize = false;
                    statsLabelATK.Location = new Point(X_Location + 3, Y_Location + 34);
                    statsLabelATK.Size = new Size(22, 11);
                    statsLabelATK.BorderStyle = BorderStyle.None;
                    statsLabelATK.ForeColor = Color.White;
                    statsLabelATK.Font = new Font("Calibri", 6, FontStyle.Bold);
                    statsLabelATK.TextAlign = ContentAlignment.TopLeft;
                    statsLabelATK.Text = "9999";
                    statsLabelATK.Visible = false;

                    //Create each inside picture box
                    PictureBox insidePicture = new PictureBox();
                    PanelBoard.Controls.Add(insidePicture);
                    insidePicture.Location = new Point(X_Location + 3, Y_Location + 3);
                    insidePicture.Size = new Size(CARDIMAGE_SIZE, CARDIMAGE_SIZE);
                    insidePicture.BorderStyle = BorderStyle.None;
                    insidePicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    insidePicture.BackColor = System.Drawing.Color.Transparent;
                    insidePicture.Tag = tileId;
                    insidePicture.MouseEnter += OnMouseEnterPicture;
                    insidePicture.MouseLeave += OnMouseLeavePicture;
                    insidePicture.Click += Tile_Click;

                    //Create each border picture box 
                    //(create this one after so it is created "behind" the inside picture
                    PictureBox borderPicture = new PictureBox();
                    PanelBoard.Controls.Add(borderPicture);
                    borderPicture.Location = new Point(X_Location, Y_Location);
                    borderPicture.Size = new Size(TILESIDE_SIZE, TILESIDE_SIZE);
                    borderPicture.BorderStyle = BorderStyle.FixedSingle;
                    borderPicture.BackColor = Color.Transparent;

                    //create and add a new tile object using the above 2 picture boxes
                    _Tiles.Add(new Tile(insidePicture, borderPicture, statsLabelATK, statsLabelDEF));

                    //update the Tile ID for the next one
                    tileId++;

                    //Move the X-Axis for the next picture
                    X_Location += TILESIDE_SIZE;
                }

                //Move the Y-Axis for the next row of pictures
                Y_Location += TILESIDE_SIZE;
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
            _CardsOnBoard.Add(_RedSymbol);
            _Tiles[227].SummonCard(_RedSymbol);

            _BlueSymbol = new Card(_CardsOnBoard.Count, BlueData.Deck.Symbol, PlayerColor.BLUE);
            _CardsOnBoard.Add(_BlueSymbol);
            _Tiles[6].SummonCard(_BlueSymbol);

            //TEST add Crests to both players
            RedData.AddCrests(Crest.MOV, 20);
            RedData.AddCrests(Crest.ATK, 20);
            RedData.AddCrests(Crest.DEF, 20);
            RedData.AddCrests(Crest.MAG, 20);
            RedData.AddCrests(Crest.TRAP, 20);

            BlueData.AddCrests(Crest.MOV, 20);
            BlueData.AddCrests(Crest.ATK, 20);
            BlueData.AddCrests(Crest.DEF, 20);
            BlueData.AddCrests(Crest.MAG, 20);
            BlueData.AddCrests(Crest.TRAP, 20);

            //Initialize the Player's Info Panels
            LoadPlayersInfo();

            //Load the CardInfo Panel
            _CurrentTileSelected = _Tiles[0];
            LoadCardInfoPanel();

            //Set the initial game state and start the turn start panel            
            LaunchTurnStartPanel();

            //Initialize both Symbols Continuous Effect
            //Initialize both Symbols Continuous Effect
            Effect RedsymbolEffect = new Effect(_RedSymbol, Effect.EffectType.Continuous);
            ActivateEffect(RedsymbolEffect);
            _ActiveEffects.Add(RedsymbolEffect);
            Effect BluesymbolEffect = new Effect(_BlueSymbol, Effect.EffectType.Continuous);
            ActivateEffect(BluesymbolEffect);
            _ActiveEffects.Add(BluesymbolEffect);

            //Set tiles
            //Set the starting tiles for each player           
            _Tiles[7].ChangeOwner(PlayerColor.BLUE);
            _Tiles[19].ChangeOwner(PlayerColor.BLUE); _Tiles[19].ChangeFieldType(Tile.FieldTypeValue.Forest);
            _Tiles[32].ChangeOwner(PlayerColor.BLUE); _Tiles[32].ChangeFieldType(Tile.FieldTypeValue.Mountain);
            _Tiles[45].ChangeOwner(PlayerColor.BLUE); _Tiles[45].ChangeFieldType(Tile.FieldTypeValue.Sogen);
            _Tiles[58].ChangeOwner(PlayerColor.BLUE); _Tiles[58].ChangeFieldType(Tile.FieldTypeValue.Umi);
            _Tiles[71].ChangeOwner(PlayerColor.BLUE); _Tiles[71].ChangeFieldType(Tile.FieldTypeValue.Wasteland);
            _Tiles[84].ChangeOwner(PlayerColor.BLUE); _Tiles[84].ChangeFieldType(Tile.FieldTypeValue.Yami);
            _Tiles[97].ChangeOwner(PlayerColor.BLUE);
            _Tiles[110].ChangeOwner(PlayerColor.BLUE); _Tiles[110].ChangeFieldType(Tile.FieldTypeValue.Sogen);

            _Tiles[123].ChangeOwner(PlayerColor.RED);
            _Tiles[136].ChangeOwner(PlayerColor.RED);
            _Tiles[149].ChangeOwner(PlayerColor.RED);
            _Tiles[162].ChangeOwner(PlayerColor.RED);
            _Tiles[175].ChangeOwner(PlayerColor.RED);
            _Tiles[188].ChangeOwner(PlayerColor.RED);
            _Tiles[201].ChangeOwner(PlayerColor.RED);
            _Tiles[214].ChangeOwner(PlayerColor.RED);
            _Tiles[226].ChangeOwner(PlayerColor.RED);
            _Tiles[116].ChangeOwner(PlayerColor.RED);
            _Tiles[104].ChangeOwner(PlayerColor.RED);

            //TEST add high ATK monsters adj to opps symbol
            CardInfo infoofblueeyes = CardDataBase.GetCardWithID(89631139);
            Card blueeyes = new Card(_CardsOnBoard.Count, infoofblueeyes, PlayerColor.RED, false);
            _CardsOnBoard.Add(blueeyes);
            _Tiles[7].SummonCard(blueeyes);

            CardInfo infohito = CardDataBase.GetCardWithID(75745607);
            Card hito = new Card(_CardsOnBoard.Count, infohito, PlayerColor.BLUE, false);
            _CardsOnBoard.Add(hito);
            _Tiles[226].SummonCard(hito);

            CardInfo pegasusinfo = CardDataBase.GetCardWithID(27054370);
            Card pegasus = new Card(_CardsOnBoard.Count, pegasusinfo, PlayerColor.BLUE, false);
            _CardsOnBoard.Add(pegasus);
            _Tiles[110].SummonCard(pegasus);

            CardInfo umiinfo = CardDataBase.GetCardWithID(22702055);
            Card umi = new Card(_CardsOnBoard.Count, umiinfo, PlayerColor.RED, true);
            _CardsOnBoard.Add(umi);
            _Tiles[123].SetCard(umi);

            CardInfo koalainfo = CardDataBase.GetCardWithID(42129512);
            Card koala = new Card(_CardsOnBoard.Count, koalainfo, PlayerColor.RED, false);
            _CardsOnBoard.Add(koala);
            _Tiles[136].SummonCard(koala);

            CardInfo kazejininfo = CardDataBase.GetCardWithID(62340868);
            Card kazejin = new Card(_CardsOnBoard.Count, kazejininfo, PlayerColor.RED, false);
            _CardsOnBoard.Add(kazejin);
            _Tiles[116].SummonCard(kazejin);

            CardInfo gagainfo = CardDataBase.GetCardWithID(39674352);
            Card gaga = new Card(_CardsOnBoard.Count, gagainfo, PlayerColor.RED, false);
            _CardsOnBoard.Add(gaga);
            _Tiles[104].SummonCard(gaga);
        }
        #endregion

        #region Public Methods
        public List<Tile> GetTiles()
        {
            return _Tiles;
        }
        public List<Tile> GetUnoccupiedSummoningTiles(PlayerColor playerColor)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Tile thisTile in _Tiles)
            {
                if (thisTile.IsSummonTile && thisTile.Owner == playerColor && !thisTile.IsOccupied)
                {
                    tiles.Add(thisTile);
                }
            }
            return tiles;
        }
        public void SetupMainPhaseNoSummon()
        {
            //Switch to the Main Phase of the player
            _CurrentGameState = GameState.MainPhaseBoard;

            //Relaod the player info panels to update crests
            LoadPlayersInfo();

            //Enable the Board Panel to interact with it
            PanelBoard.Enabled = true;

            //Update the Phase Banner
            UpdateBanner("MainPhase");

            if (UserPlayerColor == TURNPLAYER)
            {
                btnEndTurn.Visible = true;
            }
            else
            {
                btnEndTurn.Visible = false;
                lblActionInstruction.Text = "Opponent is inspecting the board for his next action!";
                lblActionInstruction.Visible = true;
            }
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

            //Update the Phase Banner
            UpdateBanner("SummonPhase");

            lblActionInstruction.Visible = true;
            PanelDimenFormSelector.Visible = true;

            if (UserPlayerColor == TURNPLAYER)
            {
                lblActionInstruction.Text = "Select Tile to Dimension the Dice and summon!";
                //Display the Dimension shape selector
                UpdateDimensionPreview();
                //Enable the bttons of the Dimension Form Selector
                btnPreviousForm.Enabled = true;
                btnNextForm.Enabled = true;
                BtnFormTurnLeft.Enabled = true;
                btnFormFlip.Enabled = true;
                BtnFormTurnRight.Enabled = true;
            }
            else
            {
                lblActionInstruction.Text = "Opponent is Selecting a Tile to Dimension the Dice and summon!";
                //Disable the bttons of the Dimension Form Selector
                btnPreviousForm.Enabled = false;
                btnNextForm.Enabled = false;
                BtnFormTurnLeft.Enabled = false;
                btnFormFlip.Enabled = false;
                BtnFormTurnRight.Enabled = false;
            }
        }
        public void SetupSetCardPhase(CardInfo card)
        {
            //Switch to the Set Card Phase of the player
            _CurrentGameState = GameState.SetCard;

            //Save the ref to the data of the card to be set
            _CardToBeSet = card;

            //Relaod the player info panels to update crests
            LoadPlayersInfo();

            //Update the Phase Banner
            UpdateBanner("SummonPhase");
            lblActionInstruction.Visible = true;

            _SetCandidates.Clear();
            _SetCandidates = GetUnoccupiedSummoningTiles(TURNPLAYER);
            DisplaySetCandidates();


            if (UserPlayerColor == TURNPLAYER)
            {
                //Enable the Board Panel to interact with it
                PanelBoard.Enabled = true;

                lblActionInstruction.Text = "Select the tile to set the card at.";
            }
            else
            {
                lblActionInstruction.Text = "Opponent is selecting the tile to set the card at.";
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
        public void CloseWithoutShuttingDownTheApp()
        {
            if (_RollDiceForm != null)
            {
                _RollDiceForm.CloseWithoutShuttingDownTheApp();
            }

            //Raise this flag so the whole app doesnt shutdown when closing the form
            _AppShutDownWhenClose = false;
            Dispose();
        }
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

            lblRedMovCount.Text = RedData.Crests_MOV.ToString();
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

                if (thisCard.IsFaceDown && thisCard.Owner != UserPlayerColor)
                {
                    ImageServer.LoadImage(PicCardArtworkBottom, CardImageType.CardArtwork, "0");

                    lblCardName.Text = string.Empty;
                    lblCardType.Text = string.Empty;
                    lblCardLevel.Text = string.Empty;
                    lblAttribute.Text = string.Empty;
                    lblStatsATK.Text = string.Empty;
                    lblStatsDEF.Text = string.Empty;
                    lblStatsLP.Text = string.Empty;
                    lblCardText.Text = "Opponent's Facedown card.";
                    lblAttackLeftAmount.Text = string.Empty;
                    lblMovesLeftAmount.Text = string.Empty;
                    lblMovesCostAmount.Text = string.Empty;
                    lblAttackCostAmount.Text = string.Empty;
                    lblDefenseCostAmount.Text = string.Empty;
                    lblTurnCounters.Text = string.Empty;
                    lblCounters.Text = string.Empty;
                    lblSpellboundCounters.Text = string.Empty;
                }
                else
                {
                    if (thisCard.IsASymbol)
                    {
                        ImageServer.LoadImage(PicCardArtworkBottom, CardImageType.Symbol, thisCard.Attribute.ToString());

                        lblCardName.Text = thisCard.Owner + "'s " + thisCard.Name;
                        lblCardType.Text = string.Empty;
                        lblCardLevel.Text = string.Empty;
                        lblAttribute.Text = thisCard.Attribute.ToString();
                        lblStatsATK.Text = string.Empty;
                        lblStatsDEF.Text = string.Empty;
                        lblStatsLP.Text = thisCard.LP.ToString();
                        lblCardText.Text = thisCard.ContinuousEffect;
                        lblAttackLeftAmount.Text = string.Empty;
                        lblMovesLeftAmount.Text = string.Format("{0} / {1}", thisCard.MovesAvaiable, thisCard.MovesPerTurn);
                        lblMovesCostAmount.Text = thisCard.MoveCost.ToString();
                        lblAttackCostAmount.Text = string.Empty;
                        lblDefenseCostAmount.Text = string.Empty;
                        lblTurnCounters.Text = string.Empty;
                        lblCounters.Text = string.Empty;
                        lblSpellboundCounters.Text = string.Empty;
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

                            if (thisCard.ATK > thisCard.OriginalATK) { lblStatsATK.ForeColor = Color.Green; }
                            else if (thisCard.ATK < thisCard.OriginalATK) { lblStatsATK.ForeColor = Color.Red; }
                            else { lblStatsATK.ForeColor = Color.White; }

                            if (thisCard.DEF > thisCard.OriginalDEF) { lblStatsDEF.ForeColor = Color.Green; }
                            else if (thisCard.DEF < thisCard.OriginalDEF) { lblStatsDEF.ForeColor = Color.Red; }
                            else { lblStatsDEF.ForeColor = Color.White; }
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

                        if (thisCard.Category == Category.Monster)
                        {
                            lblAttackLeftAmount.Text = string.Format("{0} / {1}", thisCard.AttacksAvaiable, thisCard.AttacksPerTurn);
                        }
                        else { lblAttackLeftAmount.Text = string.Empty; }

                        if (thisCard.Category == Category.Monster)
                        {
                            lblMovesLeftAmount.Text = string.Format("{0} / {1}", thisCard.MovesAvaiable, thisCard.MovesPerTurn);
                        }
                        else { lblMovesLeftAmount.Text = string.Empty; }

                        if (thisCard.Category == Category.Monster) { lblMovesCostAmount.Text = thisCard.MoveCost.ToString(); }
                        else { lblMovesCostAmount.Text = string.Empty; }

                        if (thisCard.Category == Category.Monster) { lblAttackCostAmount.Text = thisCard.AttackCost.ToString(); }
                        else { lblAttackCostAmount.Text = string.Empty; }

                        if (thisCard.Category == Category.Monster) { lblDefenseCostAmount.Text = thisCard.DefenseCost.ToString(); }
                        else { lblDefenseCostAmount.Text = string.Empty; }

                        lblTurnCounters.Text = thisCard.TurnCounters.ToString();
                        lblCounters.Text = thisCard.Counters.ToString();
                        lblSpellboundCounters.Text = thisCard.SpellboundCounter.ToString();
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
                lblAttackLeftAmount.Text = string.Empty;
                lblMovesLeftAmount.Text = string.Empty;
                lblMovesCostAmount.Text = string.Empty;
                lblAttackCostAmount.Text = string.Empty;
                lblDefenseCostAmount.Text = string.Empty;
                lblTurnCounters.Text = string.Empty;
                lblCounters.Text = string.Empty;
                lblSpellboundCounters.Text = string.Empty;
            }
        }
        private void LoadFieldTypeDisplay(bool isHovering)
        {
            if (isHovering && _CurrentTileSelected.Owner != PlayerColor.NONE)
            {
                //Panel will display
                PanelFieldType.Visible = true;

                //Give the display the color of the Tile Owner Color, this is in case the tile has no field type
                //set, then the tile display will display the base tile color.
                if (_CurrentTileSelected.Owner == PlayerColor.RED) { PicFieldTypeDisplay.BackColor = Color.DarkRed; }
                else { PicFieldTypeDisplay.BackColor = Color.DarkBlue; }

                //If field type is set, load the proper image
                if (_CurrentTileSelected.FieldType != Tile.FieldTypeValue.None)
                {
                    ImageServer.LoadImage(PicFieldTypeDisplay, CardImageType.FieldTile, _CurrentTileSelected.FieldType.ToString());

                }
                else
                {
                    if (PicFieldTypeDisplay.Image != null) { PicFieldTypeDisplay.Image.Dispose(); }
                    PicFieldTypeDisplay.Image = null;
                }

                //Update the Field Type name label
                lblFieldTypeName.Text = _CurrentTileSelected.FieldType.ToString();
            }
            else
            {
                PanelFieldType.Visible = false;
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
        private void DisplaySetCandidates()
        {
            foreach (Tile tile in _SetCandidates)
            {
                tile.MarkSetTarget();
            }
        }
        private void PlaceMoveMenu()
        {
            Point referencePoint = _CurrentTileSelected.Location;
            int X_Location = referencePoint.X;
            int Y_Location = referencePoint.Y;

            int newX = referencePoint.X;
            int newY = referencePoint.Y;

            //IF the tile clicked is on the FAR RIGHT: Display the Move Menu on the left side of the Tile
            if (X_Location > 500)
            {
                newX = newX - 83;
            }
            //OTHERWISE: Display the Move Menu on the right side of the Tile.
            else
            {
                newX = newX + 48;
            }

            //IF the tile clicked in on the TOP ROW: Display the Move Menu on the row below
            if (Y_Location < 30)
            {
                newY = newY + 48;
            }
            //OTHERWISE: display the Move Menu on the row above
            else
            {
                newY = newY - 77;
            }
            //Set the new location based on the mods above
            PanelMoveMenu.Location = new Point(newX, newY);
        }
        private void PlaceAttackMenu()
        {
            Point referencePoint = _AttackerTile.Location;
            int X_Location = referencePoint.X;
            int Y_Location = referencePoint.Y;

            int newX = referencePoint.X;
            int newY = referencePoint.Y;

            //IF the tile clicked is on the FAR RIGHT: Display the Move Menu on the left side of the Tile
            if (X_Location > 500)
            {
                newX = newX - 83;
            }
            //OTHERWISE: Display the Move Menu on the right side of the Tile.
            else
            {
                newX = newX + 48;
            }

            //IF the tile clicked in on the TOP ROW: Display the Move Menu on the row below
            if (Y_Location < 30)
            {
                newY = newY + 48;
            }
            //OTHERWISE: display the Move Menu on the row above
            else
            {
                newY = newY - 27;
            }
            //Set the new location based on the mods above
            PanelAttackMenu.Location = new Point(newX, newY);
            PanelAttackMenu.Visible = true;
        }
        private void OpenBattleMenu()
        {
            PanelBattleMenu.Visible = true;
            btnEndBattle.Visible = false;

            //Set the attacker's data
            Card Attacker = _AttackerTile.CardInPlace;
            ImageServer.LoadImageToPanel(PicAttacker, CardImageType.FullCardImage, Attacker.CardID.ToString());
            lblBattleMenuATALP.Text = "LP: " + Attacker.LP;
            lblAttackerATK.Text = "ATK: " + Attacker.ATK;

            //Update the LP labels to reflect the Color owner
            if (TURNPLAYER == PlayerColor.RED)
            {
                PanelAttackerCard.BackColor = Color.DarkRed;
                PanelDefenderCard.BackColor = Color.MidnightBlue;
            }
            else
            {
                PanelAttackerCard.BackColor = Color.MidnightBlue;
                PanelDefenderCard.BackColor = Color.DarkRed;
            }

            //Set the defender's data. if the defender is a non-monster place the clear data.
            Card Defender = _AttackTarger.CardInPlace;
            if (Defender.Category == Category.Monster)
            {
                ImageServer.LoadImageToPanel(PicDefender2, CardImageType.FullCardImage, Defender.CardID.ToString());
                lblBattleMenuDEFLP.Text = "LP: " + Defender.LP;
                lblDefenderDEF.Text = "DEF: " + Defender.DEF;
            }
            else if (Defender.Category == Category.Symbol)
            {
                ImageServer.LoadImageToPanel(PicDefender2, CardImageType.FullCardSymbol, Defender.Attribute.ToString());
                lblBattleMenuDEFLP.Text = "LP: " + Defender.LP;
                lblDefenderDEF.Text = "DEF: 0";
            }
            else
            {
                //At this point, if the attack target was a face down card, it was flipped face up
                ImageServer.LoadImageToPanel(PicDefender2, CardImageType.FullCardImage, Defender.CardID.ToString());
                lblBattleMenuDEFLP.Text = "LP: -";
                lblDefenderDEF.Text = "DEF: -";
            }

            //Hide the "Destroyed" labels just in case
            PicAttackerDestroyed.Visible = false;
            PicDefenderDestroyed.Visible = false;

            //Set the initial Damage Calculation as "?"
            //Damage calculation will be done after both player set their Atk/Def
            //choices (defender has to choose if they defend if can)
            //also both player have the choice to add advantage bonuses if available
            lblBattleMenuDamage.Text = "Damage: ?";
            lblAttackerBonus.Text = "Bonus: ?";
            lblDefenderBonus.Text = "Bonus: ?";

            //Display either the ATTACK/DEFEND controls based on the user player
            if (UserPlayerColor == TURNPLAYER)
            {
                //Display the base Attack Controls Panel
                _AttackBonusCrest = 0;
                lblAttackerBonus.Text = "Bonus: 0";
                PlayerData AttackerData = RedData; if (TURNPLAYER == PlayerColor.BLUE) { AttackerData = BlueData; }
                lblAttackerCrestCount.Text = string.Format("[ATK] to use: {0}/{1}", (Attacker.AttackCost + _AttackBonusCrest), AttackerData.Crests_ATK);
                PanelAttackControls.Visible = true;

                //If attacker monster has an advantage, enable the adv subpanel
                bool AttackerHasAdvantage = HasAttributeAdvantage(Attacker, Defender);
                if (AttackerHasAdvantage && Defender.Category == Category.Monster)
                {
                    PanelAttackerAdvBonus.Visible = true;
                    //Show the + button at the start                    
                    lblAttackerAdvMinus.Visible = false;
                    lblAttackerAdvPlus.Visible = true;
                }
                else
                {
                    PanelAttackerAdvBonus.Visible = false;
                }

                //Show the 'waiting for opponent' label on the other ide
                lblWaitingfordefender.Visible = true;
                lblWaitingforattacker.Visible = false;
                lblWaitingfordefender.Text = "Waiting for opponent...";
                lblWaitingfordefender.ForeColor = Color.Yellow;

                //Hide the Defend Controsl
                PanelDefendControls.Visible = false;
            }
            else
            {
                //Display the base Defend Controls Panel

                if (Defender.Category == Category.Monster)
                {
                    PanelDefendControls.Visible = true;
                    _DefenseBonusCrest = 0;
                    lblDefenderBonus.Text = "Bonus: 0";
                    PlayerData DefenderData = RedData; if (TURNPLAYER == PlayerColor.RED) { DefenderData = BlueData; }
                    if (DefenderData.Crests_DEF == 0) { lblDefenderCrestCount.Text = "[DEF] to use: 0/0"; }
                    else { lblDefenderCrestCount.Text = string.Format("[DEF] to use: {0}/{1}", (Defender.DefenseCost + _DefenseBonusCrest), DefenderData.Crests_DEF); }
                    PanelDefendControls.Visible = true;
                    //If the defender does not have enought [DEF] to defend. Hide the "Defend" button
                    if (Defender.DefenseCost > DefenderData.Crests_DEF)
                    {
                        btnBattleMenuDefend.Visible = false;
                    }
                    else
                    {
                        btnBattleMenuDefend.Visible = true;
                    }
                    //If defender monster has an advantage, enable the adv subpanel
                    bool DefenderHasAdvantage = HasAttributeAdvantage(Defender, Attacker);
                    if (DefenderHasAdvantage && DefenderData.Crests_DEF > 0 && Defender.Category == Category.Monster)
                    {
                        PanelDefenderAdvBonus.Visible = true;
                        //Show the + button at the start
                        lblDefenderAdvMinus.Visible = false;
                        lblDefenderAdvPlus.Visible = true;
                    }
                    else
                    {
                        PanelDefenderAdvBonus.Visible = false;
                    }
                }
                else
                {
                    //Enable the Defense controls but only enable the PASS option
                    //This is meant to work when defending with Symbols/Spells/Traps
                    PanelDefendControls.Visible = true;
                    _DefenseBonusCrest = 0;
                    lblDefenderBonus.Text = "Bonus: 0";
                    PlayerData DefenderData = RedData; if (TURNPLAYER == PlayerColor.RED) { DefenderData = BlueData; }
                    lblDefenderCrestCount.Text = "[DEF] to use: 0";
                    btnBattleMenuDefend.Visible = false;
                    PanelDefenderAdvBonus.Visible = false;
                }

                //Show the 'waiting for opponent' label on the other ide
                lblWaitingforattacker.Visible = true;
                lblWaitingfordefender.Visible = false;
                lblWaitingforattacker.Text = "Waiting for opponent...";
                lblWaitingforattacker.ForeColor = Color.Yellow;

                //Hide the Attack Controls
                PanelAttackControls.Visible = false;
            }

            //Update the Phase Banner
            UpdateBanner("BattlePhase");
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
        private void ReduceCrestsToPlayer(PlayerColor playerColor, Crest crestToBeReduced, int amountToBeReduce)
        {
            PlayerData playerData = RedData;
            if (playerColor == PlayerColor.BLUE) { playerData = BlueData; }

            for (int x = 0; x < amountToBeReduce; x++)
            {
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                playerData.RemoveCrests(crestToBeReduced, 1);
                LoadPlayersInfo();
                BoardForm.WaitNSeconds(200);
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
            //Update UI
            ImageServer.LoadImage(PicCurrentForm, CardImageType.DimensionForm, _CurrentDimensionForm.ToString());
            lblFormName.Text = _CurrentDimensionForm.ToString();
        }
        private void UpdateBanner(string currentPhase)
        {
            if (PicPhaseBanner.Image != null) { PicPhaseBanner.Image.Dispose(); }
            PicPhaseBanner.Image = ImageServer.PhaseBanner(TURNPLAYER, currentPhase);
        }
        private void UpdateEffectLogs(string message)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                _EffectsLog.Add(message);
                File.WriteAllLines(Directory.GetCurrentDirectory() + "\\Save Files\\EffectsLog.txt", _EffectsLog);
            }
        }
        #endregion

        #region TCPServer Connection Methods
        private void SendMessageToServer(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            ns.Write(buffer, 0, buffer.Length);
        }
        public void ReceiveMesageFromServer(string DATARECEIVED)
        {
            //Step 1: Extract the Message Key and GameState
            string[] MessageTokens = DATARECEIVED.Split('|');
            string MessageKey = MessageTokens[0];
            string strGameState = MessageTokens[1];

            //Step 2: Validate the game state
            if (strGameState == _CurrentGameState.ToString())
            {
                //Step 3: Handle the message
                switch (MessageKey)
                {
                    case "[View Board Action]": btnViewBoard_Base(); break;
                    case "[ON MOUSE ENTER TILE]": OnMouseEnterPicture_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[ON MOUSE LEAVE TILE]": OnMouseLeavePicture_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[EXIT VIEW BOARD MODE]": btnReturnToTurnMenu_Base(); break;
                    case "[Roll Dice Action]": btnRoll_Base(); break;
                    //Messages from the RollDiceMenu From will have a share Message Key so they can be forward it to the form
                    //These messages will have a secondary key that will be used inside that form for processing.
                    case "[ROLL DICE FORM REQUEST]": _RollDiceForm.ReceiveMesageFromServer(DATARECEIVED); break;
                    case "[CLICK TILE TO SUMMON]": TileClick_SummonCard_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[CLICK TILE TO SET]": TileClick_SetCard_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[END TURN]": btnEndTurn_Base(); break;
                    case "[CHANGE DIMENSION SELECTION]": UpdateDimension_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[CLICK TILE TO ACTION]": TileClick_MainPhase_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[CLICK CANCEL ACTION MENU]": btnActionCancel_Base(); break;
                    case "[CLICK MOVE ACTION MENU]": btnActionMove_Base(); break;
                    case "[CLICK CANCEL MOVE MENU]": btnMoveMenuCancel_Base(); break;
                    case "[CLICK TILE TO MOVE]": TileClick_MoveCard_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[CLICK FINISH MOVE MENU]": btnMoveMenuFinish_Base(); break;
                    case "[CLICK ATTACK ACTION MENU]": btnActionAttack_Base(); break;
                    case "[CLICK TILE TO ATTACK]": TileClick_AttackTarget_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[CLICK CANCEL ATTACK MENU]": btnAttackMenuCancel_Base(); break;
                    case "[CLICK EFFECT ACTION MENU]": btnActionEffect_Base(); break;
                    case "[CLICK ACTIVATE EFFECT MENU]": btnEffectMenuActivate_Base(); break;
                    case "[CLICK CANCEL EFFECT MENU]": btnEffectMenuCancel_Base(); break;
                    case "[ATTACK!]": BattleMessageReceived_Attack(Convert.ToInt32(MessageTokens[2])); break;
                    case "[DEFEND!]": BattleMessageReceived_Defend(Convert.ToInt32(MessageTokens[2])); break;
                    case "[PASS!]": BattleMessageReceived_Pass(); break;
                    case "[END BATTLE]": btnEndBattle_Base(); break;
                }
            }
            else
            {
                throw new Exception("Message Received with an invalid game state");
            }
        }
        #endregion

        #region Turn Steps Functions
        private void LaunchTurnStartPanel()
        {
            //Depending on the TURNPLAYER enable/disable the buttons
            //Only the TURN PLAYER can take action
            if (UserPlayerColor == TURNPLAYER)
            {
                btnRoll.Visible = true;
                btnViewBoard.Visible = true;
                lblOponentActionWarning.Visible = false;
            }
            else
            {
                btnRoll.Visible = false;
                btnViewBoard.Visible = false;
                lblOponentActionWarning.Visible = true;
            }

            //Show the panel
            lblTurnStartMessage.Text = string.Format("{0} Player Turn!", TURNPLAYER);
            PanelTurnStartMenu.Visible = true;

            //Update the Phase Banner
            UpdateBanner("PrepPhase");

            //Set the Game State
            _CurrentGameState = GameState.TurnStartMenu;
        }
        private void PerformDamageCalculation()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                //Reset the ready flags for the next battle
                _AttackerIsReadyToBattle = false;
                _DefenderIsReadyToBattle = false;

                //hide the waiting for opponent labels
                lblWaitingforattacker.Visible = false;
                lblWaitingfordefender.Visible = false;

                //Handle the results based on the type of attack target
                if (_AttackTarger.CardInPlace.Category == Category.Monster)
                {
                    //Step 1: Determine the BonusCrest (if ANY)
                    // _AttackBonusCrest = THIS HAS BEEN PREVIOUSLY SET AT THIS POINT
                    // _DefenseBonusCrest = THIS HAS BEEN PREVIOUSLY SET AT THIS POINT
                    //Reveal the Bonus points
                    lblAttackerBonus.Text = string.Format("Bonus: {0}", (_AttackBonusCrest * 200));
                    lblDefenderBonus.Text = string.Format("Bonus: {0}", (_DefenseBonusCrest * 200));

                    //Step 2: Calculate the Final Attack Value (Attacker's Base ATK + (Bonus [ATK] used * 200))
                    int FinalAttack = _CurrentTileSelected.CardInPlace.ATK + (_AttackBonusCrest * 200);

                    //Step 3: Calculate the Final Defense Value (if Defender choose to defend) (Defender's Base DEF + (Bonus [DEF] used * 200))
                    int FinalDefense = 0;
                    if (_DefenderDefended)
                    {
                        FinalDefense = _AttackTarger.CardInPlace.DEF + (_DefenseBonusCrest * 200);
                    }

                    //Step 4: Reduce the [ATK] and [DEF] to the respective player. 
                    Card Attacker = _CurrentTileSelected.CardInPlace;
                    Card Defender = _AttackTarger.CardInPlace;
                    int creststoremoveATK = _AttackBonusCrest + Attacker.AttackCost;
                    int creststoremoveDEF = _DefenseBonusCrest + Defender.DefenseCost;
                    if (!_DefenderDefended) { creststoremoveDEF = 0; }
                    PlayerData AttackerPlayerData = RedData;
                    PlayerData DefenderPlayerData = BlueData;
                    PlayerColor DefenderColor = PlayerColor.BLUE;
                    if (TURNPLAYER == PlayerColor.BLUE) { AttackerPlayerData = BlueData; DefenderPlayerData = RedData; DefenderColor = PlayerColor.RED; }
                    ReduceCrestsToPlayer(TURNPLAYER, Crest.ATK, creststoremoveATK);
                    ReduceCrestsToPlayer(DefenderColor, Crest.DEF, creststoremoveDEF);

                    //Step 5: Perform the calculation
                    int Damage = FinalAttack - FinalDefense;
                    if (Damage <= 0)
                    {
                        //Display the end battle button
                        lblBattleMenuDamage.Text = "Damage: 0";
                        btnEndBattle.Visible = true;
                        if (UserPlayerColor == TURNPLAYER)
                        {
                            btnEndBattle.Enabled = true;
                        }
                        else
                        {
                            btnEndBattle.Enabled = false;
                        }
                    }
                    else
                    {
                        //Step 5A: Show the final Damange Amount on the UI
                        lblBattleMenuDamage.Text = string.Format("Damage: {0}", Damage);

                        //Step 5B: Reduce the defender's monster'LP
                        int damagetodealtomonster = Damage;
                        if (damagetodealtomonster > Defender.LP)
                        {
                            damagetodealtomonster = Defender.LP;
                        }

                        //Reduce the total damage left
                        Damage -= damagetodealtomonster;

                        //Do the damage animation
                        int iterations = damagetodealtomonster / 10;
                        int waittime = 0;
                        if (iterations < 100) { waittime = 40; }
                        else if (iterations < 200) { waittime = 30; }
                        else if (iterations < 300) { waittime = 10; }
                        else { waittime = 5; }
                        for (int i = 0; i < iterations; i++)
                        {
                            Defender.ReduceLP(10);
                            lblBattleMenuDEFLP.Text = string.Format("LP: {0}", Defender.LP);
                            SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                            WaitNSeconds(waittime);
                        }

                        //Step 5C: Destroy the defender monster if the LP of the defender were reduced to 0
                        if (Defender.LP == 0)
                        {
                            SoundServer.PlaySoundEffect(SoundEffect.CardDestroyed);
                            PicDefenderDestroyed.Visible = true;
                            WaitNSeconds(1000);
                            //Remove the card from the actual tile
                            DestroyCard(_AttackTarger);
                        }


                        //Step 6D: if there is damage left deal it to the player's symbol
                        if (Damage > 0)
                        {
                            //Stablish the Defender Symbol
                            Card DefenderSymbol = _RedSymbol;
                            Label DefenderSymbolLPLabel = lblRedLP;
                            if (TURNPLAYER == PlayerColor.RED) { DefenderSymbol = _BlueSymbol; DefenderSymbolLPLabel = lblBlueLP; }

                            //Deal the damage
                            if (Damage > DefenderSymbol.LP) { Damage = DefenderSymbol.LP; }

                            //Deal damage to the player
                            iterations = Damage / 10;

                            waittime = 0;
                            if (iterations < 100) { waittime = 50; }
                            else if (iterations < 200) { waittime = 30; }
                            else if (iterations < 300) { waittime = 10; }
                            else { waittime = 5; }

                            for (int i = 0; i < iterations; i++)
                            {
                                DefenderSymbol.ReduceLP(10);
                                DefenderSymbolLPLabel.Text = DefenderSymbol.LP.ToString();
                                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                                WaitNSeconds(waittime);
                            }

                            if (DefenderSymbol.LP == 0)
                            {
                                StartGameOver();
                            }
                            else
                            {
                                //otherwise let the attacker finish the battle phase
                                btnEndBattle.Visible = true;
                                if (UserPlayerColor == TURNPLAYER)
                                {
                                    btnEndBattle.Enabled = true;
                                }
                                else
                                {
                                    btnEndBattle.Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            //Display the end battle button
                            btnEndBattle.Visible = true;
                            if (UserPlayerColor == TURNPLAYER)
                            {
                                btnEndBattle.Enabled = true;
                            }
                            else
                            {
                                btnEndBattle.Enabled = false;
                            }
                        }
                    }
                }
                else if (_AttackTarger.CardInPlace.Category == Category.Symbol)
                {
                    //Reduce the Symbol's LP and update player info panel

                    //Step 1: Bonus Crests cant be use when attacking a symbol (from either player)
                    lblAttackerBonus.Text = "Bonus: 0";
                    lblDefenderBonus.Text = "Bonus: 0";

                    //Step 2: Final Attack is the Attackers ATK without bonus
                    int FinalAttack = _CurrentTileSelected.CardInPlace.ATK;

                    //Step 3: Final Defense is always 0
                    int FinalDefense = 0;

                    //Step 4: Reduce the [ATK] from the attacker. Defender always "Passes"
                    Card Attacker = _CurrentTileSelected.CardInPlace;
                    ReduceCrestsToPlayer(TURNPLAYER, Crest.ATK, Attacker.AttackCost);

                    //Step 5: Perform the calculation
                    int Damage = FinalAttack - FinalDefense;
                    if (Damage <= 0)
                    {
                        //Display the end battle button
                        lblBattleMenuDamage.Text = "Damage: 0";
                        btnEndBattle.Visible = true;
                        if (UserPlayerColor == TURNPLAYER)
                        {
                            btnEndBattle.Enabled = true;
                        }
                        else
                        {
                            btnEndBattle.Enabled = false;
                        }
                    }
                    else
                    {
                        //Step 5A: Show the final Damange Amount on the UI
                        lblBattleMenuDamage.Text = string.Format("Damage: {0}", Damage);

                        //Step 6D: if there is damage deal it to the player's symbol
                        if (Damage > 0)
                        {
                            //Stablish the Defender Symbol
                            Card DefenderSymbol = _RedSymbol;
                            Label DefenderSymbolLPLabel = lblRedLP;
                            if (TURNPLAYER == PlayerColor.RED) { DefenderSymbol = _BlueSymbol; DefenderSymbolLPLabel = lblBlueLP; }

                            //Deal the damage
                            if (Damage > DefenderSymbol.LP) { Damage = DefenderSymbol.LP; }

                            //Deal damage to the player
                            int iterations = Damage / 10;

                            int waittime = 0;
                            if (iterations < 100) { waittime = 50; }
                            else if (iterations < 200) { waittime = 30; }
                            else if (iterations < 300) { waittime = 10; }
                            else { waittime = 5; }

                            for (int i = 0; i < iterations; i++)
                            {
                                DefenderSymbol.ReduceLP(10);
                                DefenderSymbolLPLabel.Text = DefenderSymbol.LP.ToString();
                                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                                WaitNSeconds(waittime);
                            }

                            if (DefenderSymbol.LP == 0)
                            {
                                StartGameOver();
                            }
                            else
                            {
                                //otherwise let the attacker finish the battle phase
                                btnEndBattle.Visible = true;
                                if (UserPlayerColor == TURNPLAYER)
                                {
                                    btnEndBattle.Enabled = true;
                                }
                                else
                                {
                                    btnEndBattle.Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            //Display the end battle button
                            btnEndBattle.Visible = true;
                            if (UserPlayerColor == TURNPLAYER)
                            {
                                btnEndBattle.Enabled = true;
                            }
                            else
                            {
                                btnEndBattle.Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    //Destroy the defender card automatically
                    
                    //Reduce the [ATK] from the attacker. Defender always "Passes"
                    Card Attacker = _CurrentTileSelected.CardInPlace;
                    ReduceCrestsToPlayer(TURNPLAYER, Crest.ATK, Attacker.AttackCost);

                    SoundServer.PlaySoundEffect(SoundEffect.CardDestroyed);
                    PicDefenderDestroyed.Visible = true;
                    lblBattleMenuDamage.Text = "Damage: 0";
                    WaitNSeconds(1000);
                    //Remove the card from the actual tile
                    _AttackTarger.DestroyCard();

                    //Enable the end battle button for the turn player only
                    if (UserPlayerColor == TURNPLAYER)
                    {
                        btnEndBattle.Enabled = true;
                    }
                    else
                    {
                        btnEndBattle.Enabled = false;
                    }

                    //Display the end battle button
                    btnEndBattle.Visible = true;
                }
            }));
        }
        private void DestroyCard(Tile tileLocation)
        {
            SoundServer.PlaySoundEffect(SoundEffect.CardDestroyed);

            //Save the ref of the Card Object before destroying it, we are going to need it
            Card thisCard = tileLocation.CardInPlace;

            //Now "Destroy" the card from the tile, this will remove the card link from the tile 
            //and update the UI to show the card is gone
            tileLocation.DestroyCard();

            //Now check if this card had any active Continuous effect, if so, remove the effect and revert the effect changes
            bool activeEffectFound = false;
            int indexFound = -1;
            for (int x = 0; x < _ActiveEffects.Count; x++)
            {
                //Set the ref to the effect object 
                Effect thisEffect = _ActiveEffects[x];

                //Check if the effect corresponds to the Origin Card 
                if (thisEffect.OriginCard.OnBoardID == thisCard.OnBoardID)
                {
                    //and if this is a continuos effect or one-turn ignition effect
                    if (thisEffect.Type == Effect.EffectType.Continuous || thisEffect.IsAOneTurnIgnition)
                    {
                        activeEffectFound = true;
                        indexFound = x;
                        break;
                    }
                }
            }

            //If an effect was found, perform it RemoveEffect method and then remove the effect from the active list
            if (activeEffectFound)
            {
                Effect thisEffect = _ActiveEffects[indexFound];
                UpdateEffectLogs(string.Format("Card Destroyed: [{0}] On Board ID: [{1}] Owned by: [{2}] | Removing Continuous Effect: [{3}]", thisCard.Name, thisCard.OnBoardID, thisCard.Owner, thisEffect.ID));
                //then remove the effect from the Board
                switch (thisEffect.ID)
                {
                    case Effect.EffectID.ThunderDragon_Continuous: ThunderDragon_RemoveEffect(thisEffect, indexFound); break;
                    default: throw new Exception(string.Format("This effect id: [{0}] does not have a Remove Effect method assigned", thisEffect.ID));
                }

                //Now remove the effect
                _ActiveEffects.RemoveAt(indexFound);
            }
        }
        private void ResolveEffectsWithSummonReactionTo(Card targetCard)
        {
            foreach (Effect thisActiveEffect in _ActiveEffects)
            {
                if (thisActiveEffect.ReactsToMonsterSummon)
                {
                    switch (thisActiveEffect.ID)
                    {
                        case Effect.EffectID.DARKSymbol: DarkSymbol_ReactTo_MonsterSummon(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.LIGHTSymbol: LightSymbol_ReactTo_MonsterSummon(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.WATERSymbol: WaterSymbol_ReactTo_MonsterSummon(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.FIRESymbol: FireSymbol_ReactTo_MonsterSummon(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.EARTHSymbol: EarthSymbol_ReactTo_MonsterSummon(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.WINDSymbol: WindSymbol_ReactTo_MonsterSummon(thisActiveEffect, targetCard); break;



                        case Effect.EffectID.ThunderDragon_Continuous: ThunderDragon_TryToApplyToNewCard(thisActiveEffect, targetCard); break;
                        default: throw new Exception(string.Format("Effect ID: [{0}] does not have an EffectToApply Function", thisActiveEffect.ID));
                    }
                }
            }
        }
        private void EnterMainPhase()
        {
            //Proceed to finish the Summon Phase
            UpdateBanner("MainPhase");

            //Only enable the "End Turn" button for the TURN PLAYER
            if (UserPlayerColor == TURNPLAYER)
            {
                btnEndTurn.Visible = true;
                lblActionInstruction.Visible = false;
            }
            else
            {
                btnEndTurn.Visible = false;
                lblActionInstruction.Text = "Opponent is inspecting the board for his next action!";
                lblActionInstruction.Visible = true;
            }

            _CurrentGameState = GameState.MainPhaseBoard;
        }
        private void StartGameOver()
        {
            //TODO: defender player loses the game
            SoundServer.PlayBackgroundMusic(Song.YouWin, true);
            PanelBattleMenu.Visible = false;
            PanelEndGameResults.Visible = true;

            //Send the server the gameover message
            SendMessageToServer("[GAME OVER]");

            WaitNSeconds(5000);
            btnExit.Visible = true;
        }
        #endregion

        #region Event Listeners
        #region Generic Listeners
        private void OnMouseHoverSound(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_AppShutDownWhenClose)
            {
                Environment.Exit(Environment.ExitCode);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            _PvPMenuRef.ReturnToPvPMenuFromGameOverBoard();
        }
        #endregion

        #region Turn Star Panel Elements
        private void btnRoll_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[Roll Dice Action]|" + _CurrentGameState.ToString());

            //Perform the action
            btnRoll_Base();
        }
        private void btnViewBoard_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[View Board Action]|" + _CurrentGameState.ToString());

            //Perform the action
            btnViewBoard_Base();
        }
        private void btnReturnToTurnMenu_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[EXIT VIEW BOARD MODE]|" + _CurrentGameState.ToString());

            //Perform the action
            btnReturnToTurnMenu_Base();
        }
        #endregion

        #region Board Tiles
        private void OnMouseEnterPicture(object sender, EventArgs e)
        {
            //Only allow this event if the user is the TURN PLAYER
            if (UserPlayerColor == TURNPLAYER)
            {
                //Extract the TileID from the tile in action
                PictureBox thisPicture = sender as PictureBox;
                int tileID = Convert.ToInt32(thisPicture.Tag);

                //Send the action message to the server
                if ((_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard || _CurrentGameState == GameState.SummonCard || _CurrentGameState == GameState.SetCard))
                {
                    SendMessageToServer(string.Format("{0}|{1}|{2}", "[ON MOUSE ENTER TILE]", _CurrentGameState.ToString(), tileID.ToString()));
                }

                //Perform the action
                OnMouseEnterPicture_Base(tileID);
            }
        }
        private void OnMouseLeavePicture(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Extract the TileID from the tile in action
                PictureBox thisPicture = sender as PictureBox;
                int tileID = Convert.ToInt32(thisPicture.Tag);

                //Send the action message to the server
                if ((_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard || _CurrentGameState == GameState.SummonCard || _CurrentGameState == GameState.SetCard))
                {
                    SendMessageToServer(string.Format("{0}|{1}|{2}", "[ON MOUSE LEAVE TILE]", _CurrentGameState.ToString(), tileID.ToString()));
                }

                //Perform the action
                OnMouseLeavePicture_Base(tileID);
            }
        }
        private void Tile_Click(object sender, EventArgs e)
        {
            //Extract the TileID from the tile in action
            //set the current tile selected
            PictureBox thisPicture = sender as PictureBox;
            int tileID = Convert.ToInt32(thisPicture.Tag);
            _CurrentTileSelected = _Tiles[tileID];

            if (UserPlayerColor == TURNPLAYER)
            {
                if (_CurrentGameState == GameState.MainPhaseBoard)
                {
                    if (_CurrentTileSelected.IsOccupied)
                    {
                        if (_CurrentTileSelected.CardInPlace.Owner == UserPlayerColor)
                        {
                            //Send the action message to the server
                            SendMessageToServer(string.Format("[CLICK TILE TO ACTION]|{0}|{1}", _CurrentGameState.ToString(), _CurrentTileSelected.ID));

                            //Perform the action
                            TileClick_MainPhase_Base(_CurrentTileSelected.ID);
                        }
                        else
                        {
                            SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                        }
                    }
                }
                else if (_CurrentGameState == GameState.MovingCard)
                {
                    //check this tile is one of the candidates
                    bool thisIsACandidate = false;
                    for (int x = 0; x < _MoveCandidates.Count; x++)
                    {
                        if (_MoveCandidates[x].ID == tileID)
                        {
                            thisIsACandidate = true; break;
                        }
                    }

                    if (thisIsACandidate)
                    {
                        //Send the action message to the server
                        SendMessageToServer(string.Format("[CLICK TILE TO MOVE]|{0}|{1}", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_MoveCard_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.SelectingAttackTarger)
                {
                    //check this tile is one of the candidates
                    bool thisIsACandidate = false;
                    for (int x = 0; x < _AttackCandidates.Count; x++)
                    {
                        if (_AttackCandidates[x].ID == tileID)
                        {
                            thisIsACandidate = true;
                            break;
                        }
                    }

                    if (thisIsACandidate)
                    {
                        //Send the action message to the server
                        SendMessageToServer(string.Format("[CLICK TILE TO ATTACK]|{0}|{1}", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_AttackTarget_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.SetCard)
                {
                    //check this tile is one of the candidates
                    bool thisIsACandidate = false;
                    for (int x = 0; x < _SetCandidates.Count; x++)
                    {
                        if (_SetCandidates[x].ID == tileID)
                        {
                            thisIsACandidate = true;
                            break;
                        }
                    }

                    if (thisIsACandidate)
                    {
                        //Send the action message to the server
                        SendMessageToServer(string.Format("[CLICK TILE TO SET]|{0}|{1}", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_SetCard_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.SummonCard)
                {
                    if (_validDimension)
                    {
                        //Send the action message to the server
                        SendMessageToServer("[CLICK TILE TO SUMMON]|" + _CurrentGameState.ToString() + "|" + tileID);

                        //Perform the action
                        TileClick_SummonCard_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
            }
        }
        #endregion

        #region Side Bar and Dimension Menu
        private void btnEndTurn_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}", "[END TURN]", _CurrentGameState.ToString()));

            //Perform the action
            btnEndTurn_Base();
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

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

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

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

            UpdateDimensionPreview();
        }
        private void btnFormFlip_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase: break;
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

                case DimensionForms.TBase: break;
                case DimensionForms.TRight: _CurrentDimensionForm = DimensionForms.TLeft; break;
                case DimensionForms.TLeft: _CurrentDimensionForm = DimensionForms.TRight; break;
                case DimensionForms.TUpSideDown: break;
            }

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

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

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

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

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

            UpdateDimensionPreview();
        }
        private void UpdateDimension_Base(int selectionID)
        {
            _CurrentDimensionForm = (DimensionForms)selectionID;
            UpdateDimensionPreview();
        }
        #endregion

        #region Action Menu
        private void btnActionCancel_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Send the action message to the server
                SendMessageToServer("[CLICK CANCEL ACTION MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnActionCancel_Base();
            }
        }
        private void btnActionMove_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Send the action message to the server
                SendMessageToServer("[CLICK MOVE ACTION MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnActionMove_Base();
            }
        }
        private void btnActionAttack_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Send the action message to the server
                SendMessageToServer("[CLICK ATTACK ACTION MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnActionAttack_Base();
            }
        }
        private void btnActionEffect_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Send the action message to the server
                SendMessageToServer("[CLICK EFFECT ACTION MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnActionEffect_Base();
            }
        }
        private void btnMoveMenuFinish_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Send the action message to the server
                SendMessageToServer("[CLICK FINISH MOVE MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnMoveMenuFinish_Base();
            }
        }
        private void btnMoveMenuCancel_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Send the action message to the server
                SendMessageToServer("[CLICK CANCEL MOVE MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnMoveMenuCancel_Base();
            }
        }
        private void btnAttackMenuCancel_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Send the action message to the server
                SendMessageToServer("[CLICK CANCEL ATTACK MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnAttackMenuCancel_Base();
            }
        }
        #endregion

        #region Battle Menu
        private void btnBattleMenuAttack_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Attack);

            //Send the opponent the action taken
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[ATTACK!]", _CurrentGameState.ToString(), _AttackBonusCrest));

            //Hide the Attack Controls Panel
            PanelAttackControls.Visible = false;

            //Marks the ready flags
            _AttackerIsReadyToBattle = true;

            if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
            {
                PerformDamageCalculation();
            }
        }
        private void btnBattleMenuDefend_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Attack);

            //Send the opponent the action taken
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[DEFEND!]", _CurrentGameState.ToString(), _DefenseBonusCrest));

            //Hide the Defend Controls Panel
            PanelDefendControls.Visible = false;
            //Marks the ready flags
            _DefenderDefended = true;
            _DefenderIsReadyToBattle = true;

            if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
            {
                PerformDamageCalculation();
            }
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
                lblAttackerBonus.Text = string.Format("Bonus: {0}", (_AttackBonusCrest * 200));
                PlayerData AttackerData = RedData; if (TURNPLAYER == PlayerColor.BLUE) { AttackerData = BlueData; }
                Card Attacker = _CurrentTileSelected.CardInPlace;
                lblAttackerCrestCount.Text = string.Format("[ATK] to use: {0}/{1}", (Attacker.AttackCost + _AttackBonusCrest), AttackerData.Crests_ATK);
            }
        }
        private void lblAttackerAdvPlus_Click(object sender, EventArgs e)
        {
            if (_AttackBonusCrest < 5)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                PlayerData AttackerData = RedData; if (TURNPLAYER == PlayerColor.BLUE) { AttackerData = BlueData; }
                Card Attacker = _CurrentTileSelected.CardInPlace;

                _AttackBonusCrest++;
                if (_AttackBonusCrest == 5 || (_AttackBonusCrest + Attacker.AttackCost + 1) > AttackerData.Crests_ATK)
                {
                    lblAttackerAdvPlus.Visible = false;
                }

                lblAttackerAdvMinus.Visible = true;
                lblAttackerBonusCrest.Text = _AttackBonusCrest.ToString();
                lblAttackerBonus.Text = string.Format("Bonus: {0}", (_AttackBonusCrest * 200));
                lblAttackerCrestCount.Text = string.Format("[ATK] to use: {0}/{1}", (Attacker.AttackCost + _AttackBonusCrest), AttackerData.Crests_ATK);
            }
        }
        private void lblDefenderAdvMinus_Click(object sender, EventArgs e)
        {
            if (_DefenseBonusCrest > 0)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _DefenseBonusCrest--;
                if (_DefenseBonusCrest == 0)
                {
                    lblDefenderAdvMinus.Visible = false;
                }

                lblDefenderAdvPlus.Visible = true;
                lblDefenderBonusCrest.Text = _DefenseBonusCrest.ToString();
                lblDefenderBonus.Text = string.Format("Bonus: {0}", (_DefenseBonusCrest * 200));
                PlayerData DefenderData = RedData; if (TURNPLAYER == PlayerColor.RED) { DefenderData = BlueData; }
                Card Defender = _AttackTarger.CardInPlace;
                lblDefenderCrestCount.Text = string.Format("[DEF] to use: {0}/{1}", (Defender.DefenseCost + _DefenseBonusCrest), DefenderData.Crests_DEF);
            }
        }
        private void lblDefenderAdvPlus_Click(object sender, EventArgs e)
        {
            if (_DefenseBonusCrest < 5)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                PlayerData DefenderData = RedData; if (TURNPLAYER == PlayerColor.RED) { DefenderData = BlueData; }
                Card Defender = _AttackTarger.CardInPlace;

                _DefenseBonusCrest++;
                if (_DefenseBonusCrest == 5 || (Defender.DefenseCost + _DefenseBonusCrest + 1) > DefenderData.Crests_DEF)
                {
                    lblDefenderAdvMinus.Visible = false;
                }

                lblDefenderAdvMinus.Visible = true;
                lblDefenderBonusCrest.Text = _DefenseBonusCrest.ToString();
                lblDefenderBonus.Text = string.Format("Bonus: {0}", (_DefenseBonusCrest * 200));
                lblDefenderCrestCount.Text = string.Format("[DEF] to use: {0}/{1}", (Defender.DefenseCost + _DefenseBonusCrest), DefenderData.Crests_DEF);
            }
        }
        private void btnBattleMenuDontDefend_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Attack);

            //Send the opponent the action taken
            SendMessageToServer(string.Format("{0}|{1}", "[PASS!]", _CurrentGameState.ToString()));

            //Hide the Defend Controls Panel
            PanelDefendControls.Visible = false;
            //Marks the ready flags
            _DefenderIsReadyToBattle = true;
            _DefenderDefended = false;

            if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
            {
                PerformDamageCalculation();
            }
        }
        private void btnEndBattle_Click(object sender, EventArgs e)
        {
            //Send the action message to the server
            SendMessageToServer("[END BATTLE]|" + _CurrentGameState.ToString());

            //Perform the action
            btnEndBattle_Base();
        }
        #endregion

        #region Effect Menu
        private void btnEffectMenuCancel_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Send the action message to the server
                SendMessageToServer("[CLICK CANCEL EFFECT MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnEffectMenuCancel_Base();
            }
        }
        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                //Send the action message to the server
                SendMessageToServer("[CLICK ACTIVATE EFFECT MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnEffectMenuActivate_Base();
            }
        }
        #endregion
        #endregion

        #region Base Player Actions
        //These are the action of the event listners without sending the action messages to the server
        private void btnRoll_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Preamble set the board in the Main Phase 
                _CurrentGameState = GameState.MainPhaseBoard;
                PanelTurnStartMenu.Visible = false;

                bool IsUserTurn = (UserPlayerColor == TURNPLAYER);
                if (TURNPLAYER == PlayerColor.RED)
                {
                    _RollDiceForm = new RollDiceMenu(IsUserTurn, TURNPLAYER, RedData, this, ns);
                    Hide();
                    _RollDiceForm.Show();
                }
                else
                {
                    _RollDiceForm = new RollDiceMenu(IsUserTurn, TURNPLAYER, BlueData, this, ns);
                    Hide();
                    _RollDiceForm.Show();
                }
            }));
        }
        private void btnViewBoard_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Change Game State
                _CurrentGameState = GameState.BoardViewMode;
                PanelBoard.Enabled = true;
                PanelTurnStartMenu.Visible = false;
                //The "Exit Board View Menu" button will only be visible for the Turn Player
                //Also, the player on waiting will have the Action Instruction visible stating
                //the turn player is viewing the board
                if (UserPlayerColor == TURNPLAYER)
                {
                    btnReturnToTurnMenu.Visible = true;
                }
                else
                {
                    btnReturnToTurnMenu.Visible = false;
                    lblActionInstruction.Text = "Opponent is currently inspecting the board.";
                    lblActionInstruction.Visible = true;
                }
            }));
        }
        private void OnMouseEnterPicture_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard || _CurrentGameState == GameState.SetCard)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Hover);
                    _CurrentTileSelected = _Tiles[tileId];
                    _CurrentTileSelected.Hover();

                    UpdateDebugWindow();
                    LoadCardInfoPanel();
                    LoadFieldTypeDisplay(true);
                }
                else if (_CurrentGameState == GameState.SummonCard)
                {
                    //Highlight the possible dimmension
                    SoundServer.PlaySoundEffect(SoundEffect.Hover);

                    //Use the following function to get the ref to the tiles that compose the dimension
                    _dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                    //Check if it is valid or not (it becomes invalid if at least 1 tile is Null AND 
                    //if none of the tiles are adjecent to any other owned by the player)
                    _validDimension = Dimension.IsThisDimensionValid(_dimensionTiles, TURNPLAYER);

                    //Draw the dimension shape
                    _dimensionTiles[0].MarkDimensionSummonTile();
                    for (int x = 1; x < _dimensionTiles.Length; x++)
                    {
                        if (_dimensionTiles[x] != null) { _dimensionTiles[x].MarkDimensionTile(_validDimension); }
                    }
                }
            }));
        }
        private void OnMouseLeavePicture_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard)
                {
                    _CurrentTileSelected.ReloadTileUI();
                    LoadFieldTypeDisplay(false);
                }
                else if (_CurrentGameState == GameState.SetCard)
                {
                    _CurrentTileSelected.ReloadTileUI();
                    LoadFieldTypeDisplay(false);
                    //Redraw the candudates
                    DisplaySetCandidates();
                }
                else if (_CurrentGameState == GameState.SummonCard)
                {
                    //Restore the possible dimmension tiles to their OG colors
                    SoundServer.PlaySoundEffect(SoundEffect.Hover);

                    //Use the following function to get the ref to the tiles that compose the dimension
                    Tile[] dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                    //Reset the color of the dimensionTiles
                    for (int x = 0; x < dimensionTiles.Length; x++)
                    {
                        if (dimensionTiles[x] != null) { dimensionTiles[x].ReloadTileUI(); }
                    }
                }
            }));
        }
        private void btnReturnToTurnMenu_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //Return to the turn start menui
                _CurrentGameState = GameState.TurnStartMenu;
                PanelBoard.Enabled = false;
                btnReturnToTurnMenu.Visible = false;
                PanelTurnStartMenu.Visible = true;
                lblActionInstruction.Visible = false;
            }));
        }
        private void TileClick_SummonCard_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                //TODO: I need a sound effect here 

                //Initialize the Dimension tiles again (Oppoenent's UI doesnt get them initialize them on hover)
                _dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                //Dimension the tiles
                foreach (Tile tile in _dimensionTiles)
                {
                    tile.ChangeOwner(TURNPLAYER);
                }

                //then summon the card
                Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(_CardToBeSummon.ID), TURNPLAYER, false);
                _CardsOnBoard.Add(thisCard);
                _Tiles[tileId].SummonCard(thisCard);

                //Complete the summon
                lblActionInstruction.Visible = false;
                PanelDimenFormSelector.Visible = false;
                _CurrentDimensionForm = DimensionForms.CrossBase;
                _CurrentTileSelected = _dimensionTiles[0];

                //Check for active effects that react to monster summons
                UpdateEffectLogs(string.Format("Card Summoned: [{0}] On Board ID: [{1}] Owned By: [{2}] - Checking for Active Effects to Apply.", thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                ResolveEffectsWithSummonReactionTo(thisCard);

                //Now check if the Monster has an "On Summon"/"Continuous" effect and try to activate
                if (thisCard.HasOnSummonEffect && thisCard.EffectsAreImplemented)
                {
                    //Create the effect object and activate
                    Effect thisCardsEffect = new Effect(thisCard, Effect.EffectType.OnSummon);
                    ActivateEffect(thisCardsEffect);
                    _ActiveEffects.Add(thisCardsEffect);
                }
                else if (thisCard.HasContinuousEffect && thisCard.EffectsAreImplemented)
                {
                    //Create the effect object and activate
                    Effect thisCardsEffect = new Effect(thisCard, Effect.EffectType.Continuous);
                    ActivateEffect(thisCardsEffect);
                    _ActiveEffects.Add(thisCardsEffect);
                }
                else
                {
                    EnterMainPhase();
                }
            }));
        }
        private void TileClick_SetCard_Base(int tileId)
        {
            //TODO: Add a sounds effect here to set the card

            //Set Card here
            Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(_CardToBeSet.ID), TURNPLAYER, true);
            _CardsOnBoard.Add(thisCard);
            _Tiles[tileId].SetCard(thisCard);

            //Reset the UI of all the candidate tiles
            foreach (Tile thisTile in _SetCandidates)
            {
                if (thisTile.ID != tileId) { thisTile.ReloadTileUI(); }
            }

            //Once this action is completed, move to the main phase
            EnterMainPhase();
        }
        private void TileClick_MainPhase_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Step 1: Open the Action menu by setting its dynamic location and making it visible
                PositionActionMenu();
                PanelActionMenu.Visible = true;
                btnActionCancel.Enabled = true;

                //Step 2: Generate the Card object on the card in action, we'll use it later.
                Card thiscard = _CurrentTileSelected.CardInPlace;

                //Step 3A: Enable the Action Buttons based on the card in action
                if (UserPlayerColor == TURNPLAYER)
                {
                    //Hide the End Turn Button for the turn player 
                    btnEndTurn.Visible = false;

                    //genrate the PlayerData object of the attacker and the actual user
                    PlayerData UserPlayerData = RedData;
                    if (TURNPLAYER == PlayerColor.BLUE) { UserPlayerData = BlueData; }
                    PlayerData TurnPlayerData = RedData;
                    if (TURNPLAYER == PlayerColor.BLUE)
                    {
                        TurnPlayerData = BlueData;
                    }

                    //Check if Card can move                
                    if (CanCardMove(thiscard, TurnPlayerData))
                    {
                        btnActionMove.Enabled = true;
                        _TMPMoveCrestCount = TurnPlayerData.Crests_MOV;
                        lblMoveMenuCrestCount.Text = string.Format("[MOV]x {0}", _TMPMoveCrestCount);
                    }
                    else
                    {
                        btnActionMove.Enabled = false;
                    }

                    //Check if Card can attack
                    if (CanCardAttack(thiscard, TurnPlayerData))
                    {
                        btnActionAttack.Enabled = true;
                    }
                    else
                    {
                        btnActionAttack.Enabled = false;
                    }

                    //Check if Card has an effect to activate
                    if (DoesCardHasAnEffectToActivate(thiscard))
                    {
                        btnActionEffect.Enabled = true;
                    }
                    else
                    {
                        btnActionEffect.Enabled = false;
                    }
                }
                //Step 3B: Disable the Action Button for the non-turn player
                else
                {
                    //Show the action message to the opposite player
                    lblActionInstruction.Text = string.Format("Opponent selected {0} for action!", thiscard.Name);
                    lblActionInstruction.Visible = true;
                    btnActionMove.Enabled = false;
                    btnActionAttack.Enabled = false;
                    btnActionMove.Enabled = false;
                    btnActionCancel.Enabled = false;
                }

                //Change the game state
                _CurrentGameState = GameState.ActionMenuDisplay;

            }));

            void PositionActionMenu()
            {
                //Set the location in relation to the Tile location and cursor location
                Point referencePoint = _CurrentTileSelected.Location;
                int X_Location = referencePoint.X;
                //IF the tile clicked is on the FAR RIGHT: Display the Action Menu on the left side of the Tile
                if (X_Location > 500)
                {
                    PanelActionMenu.Location = new Point(referencePoint.X - 83, referencePoint.Y - 25);
                }
                //OTHERWISE: Display the Action Menu on the right side of the Tile.
                else
                {
                    PanelActionMenu.Location = new Point(referencePoint.X + 48, referencePoint.Y - 25);
                }
            }
            bool CanCardMove(Card thiscard, PlayerData TurnPlayerData)
            {
                if (thiscard.MovesAvaiable == 0 || thiscard.MoveCost > TurnPlayerData.Crests_MOV)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            bool CanCardAttack(Card thiscard, PlayerData TurnPlayerData)
            {
                //Determine if this card can attack if:
                // 1. Has Attacks available left
                // 2. Player has enough atack crest to pay its cost
                // 3. Card is a monster

                //Disable the Attack option if: Card is not a monster OR Monster has no available attacks OR Monster's Attack Cost is higher than [ATK] available.
                if (thiscard.AttacksAvaiable == 0 || thiscard.AttackCost > TurnPlayerData.Crests_ATK || thiscard.Category != Category.Monster)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            bool DoesCardHasAnEffectToActivate(Card thiscard)
            {
                if (thiscard.Category == Category.Monster && thiscard.HasIgnitionEffect)
                {
                    return true;
                }
                else if (thiscard.Category == Category.Spell && (thiscard.HasContinuousEffect || thiscard.HasIgnitionEffect))
                {
                    return true;
                }
                else { return false; }
            }
        }
        private void btnEndTurn_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                //Update the Phase Banner
                UpdateBanner("EndPhase");

                //Clean up the board
                btnEndTurn.Visible = false;
                lblActionInstruction.Visible = false;

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

                //Change the TURNPLAYER
                if (TURNPLAYER == PlayerColor.RED) { TURNPLAYER = PlayerColor.BLUE; } else { TURNPLAYER = PlayerColor.RED; }

                //Start the TURN at the TURN START PHASE
                LaunchTurnStartPanel();

                //Update GameState
                _CurrentGameState = GameState.TurnStartMenu;
            }));
        }
        private void btnActionCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //Close the Action menu/Card info panel and return to the MainPhase Stage 
                _CurrentTileSelected.ReloadTileUI();
                PanelActionMenu.Visible = false;
                _CurrentGameState = GameState.MainPhaseBoard;
                if (UserPlayerColor == TURNPLAYER)
                {
                    btnEndTurn.Visible = true;
                }
                else
                {
                    lblActionInstruction.Text = "Opponent is inspecting the board for his next action!";
                    lblActionInstruction.Visible = true;
                }
            }));
        }
        private void btnActionMove_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _CurrentGameState = GameState.MovingCard;

                _InitialTileMove = _CurrentTileSelected;
                _PreviousTileMove = _InitialTileMove;

                PanelActionMenu.Visible = false;
                DisplayMoveCandidates();
                PlaceMoveMenu();

                btnMoveMenuFinish.Enabled = false;
                btnMoveMenuCancel.Enabled = true;
                lblMoveMenuCrestCount.ForeColor = Color.Yellow;
                PanelMoveMenu.Visible = true;

                if (UserPlayerColor != TURNPLAYER)
                {
                    lblActionInstruction.Text = "Opponent is selecting a tile to move into!";
                    lblActionInstruction.Visible = true;
                }
            }));
        }
        private void btnActionAttack_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _AttackerTile = _CurrentTileSelected;

                PlayerColor TargetPlayerColor = PlayerColor.RED;
                if (TURNPLAYER == PlayerColor.RED) { TargetPlayerColor = PlayerColor.BLUE; }
                _AttackCandidates = _CurrentTileSelected.GetAttackTargerCandidates(TargetPlayerColor);
                DisplayAttackCandidate();
                PlaceAttackMenu();

                PanelActionMenu.Visible = false;

                if (UserPlayerColor != TURNPLAYER)
                {
                    lblActionInstruction.Text = "Opponent is selecting an attack target!";
                    lblActionInstruction.Visible = true;
                }

                _CurrentGameState = GameState.SelectingAttackTarger;
            }));
        }
        private void btnActionEffect_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Step 1: Change the game state and hide the action menu
                _CurrentGameState = GameState.ActionMenuDisplay;
                PanelActionMenu.Visible = false;

                //Step 2: Open the Effect Menu
                DisplayIgnitionEfectPanel(_CurrentTileSelected.CardInPlace);
            }));
        }
        private void btnMoveMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //Reload the card's tile before the mouse trigers hovering another tile.
                _CurrentTileSelected.ReloadTileUI();

                //Now clear the borders of all the candidates tiles to their og color
                for (int x = 0; x < _MoveCandidates.Count; x++)
                {
                    _MoveCandidates[x].ReloadTileUI();
                }
                _MoveCandidates.Clear();

                //Move Menu can close now
                PanelMoveMenu.Visible = false;

                //Return card to the OG spot
                Card thiscard = _PreviousTileMove.CardInPlace;
                _PreviousTileMove.RemoveCard();
                _InitialTileMove.MoveInCard(thiscard);

                //Change the _current Selected card back to OG
                _CurrentTileSelected = _InitialTileMove;

                //Reload the Player info panel to reset the crest count
                LoadPlayersInfo();
                UpdateDebugWindow();


                if (UserPlayerColor == TURNPLAYER)
                {
                    btnEndTurn.Visible = true;
                }
                else
                {
                    lblActionInstruction.Text = "Opponent is inspecting the board for his next action!";
                    lblActionInstruction.Visible = true;
                }

                //Update game state 
                _CurrentGameState = GameState.MainPhaseBoard;
            }));
        }
        private void TileClick_MoveCard_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.MoveCard);

                //Move the card to this location
                Card thiscard = _PreviousTileMove.CardInPlace;

                _PreviousTileMove.RemoveCard();
                _Tiles[tileId].MoveInCard(thiscard);
                _PreviousTileMove.ReloadTileUI();

                //Now clear the borders of all the candidates tiles to their og color
                for (int x = 0; x < _MoveCandidates.Count; x++)
                {
                    _MoveCandidates[x].ReloadTileUI();
                }

                //Now change the selection to this one tile
                _PreviousTileMove = _Tiles[tileId];
                _PreviousTileMove.Hover();
                UpdateDebugWindow();

                //Drecease the available crests to use
                _TMPMoveCrestCount -= thiscard.MoveCost;
                lblMoveMenuCrestCount.Text = string.Format("[MOV]x {0}", _TMPMoveCrestCount);


                //Now display the next round of move candidate if there are any MOV crest left.
                if (thiscard.MoveCost > _TMPMoveCrestCount)
                {
                    //No more available moves. do no generate more candidates
                    _MoveCandidates.Clear();
                    lblMoveMenuCrestCount.ForeColor = Color.Red;
                }
                else
                {
                    DisplayMoveCandidates();
                }

                PlaceMoveMenu();
                btnMoveMenuFinish.Enabled = true;
                btnMoveMenuCancel.Enabled = true;
            }));
        }
        private void btnMoveMenuFinish_Base()
        {
            Invoke(new MethodInvoker(delegate ()
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
                _PreviousTileMove.CardInPlace.RemoveMoveCounter();

                //Apply the amoutn of crests used
                PlayerData TurnPlayerData = RedData;
                if (TURNPLAYER == PlayerColor.BLUE) { TurnPlayerData = BlueData; }

                int amountUsed = TurnPlayerData.Crests_MOV - _TMPMoveCrestCount;
                TurnPlayerData.RemoveCrests(Crest.MOV, amountUsed);
                LoadPlayersInfo();

                _CurrentGameState = GameState.MainPhaseBoard;

                if (UserPlayerColor == TURNPLAYER)
                {
                    btnEndTurn.Visible = true;
                }
                else
                {
                    lblActionInstruction.Text = "Opponent is inspecting the board for his next action!";
                    lblActionInstruction.Visible = true;
                }
            }));
        }
        private void TileClick_AttackTarget_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Remove an Attack Available Counter from this card
                _CurrentTileSelected.CardInPlace.RemoveAttackCounter();

                //Attack the card in this tile
                _AttackTarger = _Tiles[tileId];

                //if the card is facedow, flip it
                _AttackTarger.CardInPlace.FlipFaceUp();
                WaitNSeconds(1000);

                //Close the Attack Menu and clear the color of all attack candidates
                PanelAttackMenu.Visible = false;
                foreach (Tile tile in _AttackCandidates)
                {
                    tile.ReloadTileUI();
                }
                _AttackCandidates.Clear();

                lblActionInstruction.Visible = false;

                //Open the Battle Panel
                OpenBattleMenu();
                _CurrentGameState = GameState.BattlePhase;
            }));
        }
        private void btnAttackMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //Reload the card's tile before the mouse trigers hovering another tile.
                _CurrentTileSelected.ReloadTileUI();

                //Unmark all the attack candidates
                foreach (Tile tile in _AttackCandidates)
                {
                    tile.ReloadTileUI();
                }

                _AttackCandidates.Clear();
                PanelAttackMenu.Visible = false;

                if (UserPlayerColor == TURNPLAYER)
                {
                    btnEndTurn.Visible = true;
                }
                else
                {
                    lblActionInstruction.Text = "Opponent is inspecting the board for his next action!";
                    lblActionInstruction.Visible = true;
                }

                _CurrentGameState = GameState.MainPhaseBoard;
            }));
        }
        private void btnEffectMenuActivate_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Step 1: Hide the Active/Cancel buttons to prevent any input while
                //the cost is reduce from the player's crest pool
                btnActivate.Visible = false;
                btnEffectMenuCancel.Visible = false;

                //Step 2: Flip the facedown card (if it was a facedown card)
                if (_CardEffectToBeActivated.OriginCard.IsFaceDown)
                {
                    _CardEffectToBeActivated.OriginCard.FlipFaceUp();
                    //Reveal the hidden info for the opposite player
                    if (UserPlayerColor != TURNPLAYER)
                    {
                        ImageServer.LoadImage(PicEffectMenuCardImage, CardImageType.FullCardImage, _CardEffectToBeActivated.OriginCard.CardID.ToString());
                        lblEffectMenuTittle.Text = string.Format("{0} Effect", _CardEffectToBeActivated.Type);
                        lblEffectMenuDescriiption.Text = _CardEffectToBeActivated.EffectText;
                        ImageServer.LoadImage(PicCostCrest, CardImageType.CrestIcon, _CardEffectToBeActivated.CrestCost.ToString());
                        lblCostAmount.Text = string.Format("x {0}", _CardEffectToBeActivated.CostAmount);
                        lblCostAmount.ForeColor = Color.White;
                        PanelCost.Visible = true;
                    }
                }

                //Step 3: Reduce the cost from the player's crest pool
                ReduceCrestsToPlayer(TURNPLAYER, _CardEffectToBeActivated.CrestCost, _CardEffectToBeActivated.CostAmount);

                //Step 4: Give a small pause to allow the opposite player to see the effect revealed on their end
                BoardForm.WaitNSeconds(2000);

                //Step 5: Close the Effect Menu and active the effect
                PanelEffectActivationMenu.Visible = false;
                BoardForm.WaitNSeconds(500);
                ActivateEffect(_CardEffectToBeActivated);

                //NOTE: The ActivateEffect() will decide the direction of the next game state
                //based on the Activation method of the specific effect that will be activated.
            }));
        }
        private void btnEffectMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Close the Effect Menu and return to the main phase
                PanelEffectActivationMenu.Visible = false;
                _CurrentTileSelected.ReloadTileUI();

                //Reset UI for each player
                if (UserPlayerColor == TURNPLAYER)
                {
                    btnEndTurn.Visible = true;
                }
                else
                {
                    lblActionInstruction.Text = "Opponent is inspecting the board for his next action!";
                    lblActionInstruction.Visible = true;
                }

                //Update the gamestate
                _CurrentGameState = GameState.MainPhaseBoard;
            }));
        }
        private void BattleMessageReceived_Attack(int crestBonusAmount)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _AttackBonusCrest = crestBonusAmount;
                _AttackerIsReadyToBattle = true;
                lblWaitingforattacker.ForeColor = Color.Green;
                lblWaitingforattacker.Text = "Opponent is ready!";
                if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
                {
                    PerformDamageCalculation();
                }
            }));
        }
        private void BattleMessageReceived_Defend(int crestBonusAmount)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _DefenderDefended = true;
                _DefenseBonusCrest = crestBonusAmount;
                _DefenderIsReadyToBattle = true;
                lblWaitingfordefender.ForeColor = Color.Green;
                lblWaitingfordefender.Text = "Opponent is ready!";
                if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
                {
                    PerformDamageCalculation();
                }
            }));
        }
        private void BattleMessageReceived_Pass()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _DefenderDefended = false;
                _DefenseBonusCrest = 0;
                _DefenderIsReadyToBattle = true;
                lblWaitingfordefender.ForeColor = Color.Green;
                lblWaitingfordefender.Text = "Opponent is ready!";
                if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
                {
                    PerformDamageCalculation();
                }
            }));
        }
        private void btnEndBattle_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _CurrentTileSelected.ReloadTileUI();
                PanelBattleMenu.Visible = false;
                _CurrentGameState = GameState.MainPhaseBoard;
                //Update Banner
                UpdateBanner("MainPhase");
                if (UserPlayerColor == TURNPLAYER)
                {
                    btnEndTurn.Visible = true;
                }
                else
                {
                    lblActionInstruction.Text = "Opponent is inspecting the board for his next action!";
                    lblActionInstruction.Visible = true;
                }
            }));
        }
        #endregion

        #region Data
        private PlayerColor TURNPLAYER = PlayerColor.RED;
        private PlayerColor UserPlayerColor;
        private GameState _CurrentGameState = GameState.MainPhaseBoard;
        private PlayerData RedData;
        private PlayerData BlueData;
        private List<Tile> _Tiles = new List<Tile>();
        private Tile _CurrentTileSelected = null;
        private List<Card> _CardsOnBoard = new List<Card>();
        private List<Tile> _MoveCandidates = new List<Tile>();
        private Tile _InitialTileMove = null;
        private Tile _PreviousTileMove = null;
        private int _TMPMoveCrestCount = 0;
        private List<Tile> _AttackCandidates = new List<Tile>();
        private Tile _AttackTarger;
        private Tile _AttackerTile = null;
        //Battle menu data
        private int _AttackBonusCrest = 0;
        private int _DefenseBonusCrest = 0;
        private bool _DefenderDefended = false;
        private bool _AttackerIsReadyToBattle = false;
        private bool _DefenderIsReadyToBattle = false;
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
        //Client NetworkStream to send message to the server
        private NetworkStream ns;
        private RollDiceMenu _RollDiceForm;
        //Active Effects Data
        private List<string> _EffectsLog = new List<string>();
        private List<Effect> _ActiveEffects = new List<Effect>();
        private bool _AppShutDownWhenClose = true;
        private PvPMenu _PvPMenuRef;
        private Effect _CardEffectToBeActivated;
        #endregion

        #region Effect Activation Methods
        #region Share Methods for Effects Execution
        private void ActivateEffect(Effect thisEffect)
        {
            switch (thisEffect.ID)
            {
                case Effect.EffectID.DARKSymbol: DarkSymbol_Activation(thisEffect); break;
                case Effect.EffectID.LIGHTSymbol: LightSymbol_Activation(thisEffect); break;
                case Effect.EffectID.WATERSymbol: WaterSymbol_Activation(thisEffect); break;
                case Effect.EffectID.FIRESymbol: FireSymbol_Activation(thisEffect); break;
                case Effect.EffectID.EARTHSymbol: EarthSymbol_Activation(thisEffect); break;
                case Effect.EffectID.WINDSymbol: WindSymbol_Activation(thisEffect); break;
                case Effect.EffectID.Mountain: Mountain_Activation(thisEffect); break;
                case Effect.EffectID.Sogen: Sogen_Activation(thisEffect); break;
                case Effect.EffectID.Wasteland: Wasteland_Activation(thisEffect); break;
                case Effect.EffectID.Forest: Forest_Activation(thisEffect); break;
                case Effect.EffectID.Yami: Yami_Activation(thisEffect); break;
                case Effect.EffectID.Umi: Umi_Activation(thisEffect); break;
                case Effect.EffectID.Volcano: Volcano_Activation(thisEffect); break;
                case Effect.EffectID.Swamp: Swamp_Activation(thisEffect); break;
                case Effect.EffectID.Cyberworld: Cyberworld_Activation(thisEffect); break;
                case Effect.EffectID.Sanctuary: Sanctuary_Activation(thisEffect); break;
                case Effect.EffectID.Scrapyard: Scrapyard_Activation(thisEffect); break;
                case Effect.EffectID.MWarrior1_OnSummon: MWarrior1_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.HitotsumeGiant_OnSummon: HitotsumeGiant_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.ThunderDragon_Continuous: ThunderDragon_Continuous(thisEffect); break;
                default: throw new Exception(string.Format("Effect ID: [{0}] does not have an Activate Effect Function"));
            }
        }
        private void DisplayOnSummonEffectPanel(Effect thisEffect)
        {
            ImageServer.LoadImage(PicEffectMenuCardImage, CardImageType.FullCardImage, thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Summon Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.OnSummonEffect;
            PanelEffectActivationMenu.Visible = true;
            BoardForm.WaitNSeconds(2000);
        }
        private void DisplayOnSummonContinuousEffectPanel(Effect thisEffect)
        {
            ImageServer.LoadImage(PicEffectMenuCardImage, CardImageType.FullCardImage, thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Continuous Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.ContinuousEffect;
            PanelEffectActivationMenu.Visible = true;
            BoardForm.WaitNSeconds(2000);
        }
        private void DisplayIgnitionEfectPanel(Card thisCard)
        {
            //Create the Effect Object,
            //This will initialize the _CardEffectToBeActivated to be use across the other methods for
            //this effect activation sequence.
            if (thisCard.Category == Category.Monster && thisCard.HasIgnitionEffect)
            {
                _CardEffectToBeActivated = new Effect(thisCard, Effect.EffectType.Ignition);
            }
            else if (thisCard.Category == Category.Spell)
            {
                if (thisCard.HasContinuousEffect)
                {
                    _CardEffectToBeActivated = new Effect(thisCard, Effect.EffectType.Continuous);
                }
                else if (thisCard.HasIgnitionEffect)
                {
                    _CardEffectToBeActivated = new Effect(thisCard, Effect.EffectType.Ignition);
                }
            }

            //Now load the actual Effect Menu Panel
            if (UserPlayerColor == TURNPLAYER)
            {
                //Show the full menu for the turn player
                //Card Image
                ImageServer.LoadImage(PicEffectMenuCardImage, CardImageType.FullCardImage, thisCard.CardID.ToString());
                //Effect Type Title
                lblEffectMenuTittle.Text = string.Format("{0} Effect", _CardEffectToBeActivated.Type);
                //Effect Text
                lblEffectMenuDescriiption.Text = _CardEffectToBeActivated.EffectText;
                //Cost
                if (_CardEffectToBeActivated.HasACost)
                {
                    ImageServer.LoadImage(PicCostCrest, CardImageType.CrestIcon, _CardEffectToBeActivated.CrestCost.ToString());
                    lblCostAmount.Text = string.Format("x {0}", _CardEffectToBeActivated.CostAmount);
                    lblCostAmount.ForeColor = Color.White;
                    PanelCost.Visible = true;

                    //Activate button
                    //Check if the activation cost is met
                    if (IsCostMet(_CardEffectToBeActivated.CrestCost, _CardEffectToBeActivated.CostAmount))
                    {
                        //then check if the activation requirements is met
                        string ActivationRequirementStatus = GetActivationRequirementStatus(_CardEffectToBeActivated.ID);
                        if (ActivationRequirementStatus == "Requirements Met")
                        {
                            lblActivationRequirementOutput.Visible = false;
                            btnActivate.Visible = true;
                            btnActivate.Enabled = true;
                        }
                        else
                        {
                            lblActivationRequirementOutput.Text = ActivationRequirementStatus;
                            lblActivationRequirementOutput.Visible = true;
                            btnActivate.Visible = false;
                        }
                    }
                    else
                    {
                        lblActivationRequirementOutput.Text = "Cost not met.";
                        lblActivationRequirementOutput.Visible = true;
                        lblCostAmount.ForeColor = Color.Red;
                        btnActivate.Visible = false;
                    }
                }
                else
                {
                    PanelCost.Visible = false;
                }

                btnEffectMenuCancel.Enabled = true;
            }
            else
            {
                //Load the Effect Menu with hidden info
                //Card Image will be face down
                ImageServer.LoadImage(PicEffectMenuCardImage, CardImageType.FullCardImage, "0");
                //Effect Type Title
                lblEffectMenuTittle.Text = "Effect";
                //Effect Text
                lblEffectMenuDescriiption.Text = "Hidden";
                //Hide the cost panel regardless if the effect has a cost
                PanelCost.Visible = false;
                lblActivationRequirementOutput.Visible = false;
                btnActivate.Visible = true;
                //disable both actyion buttons to prevent the opposite player from interacting with them
                btnActivate.Enabled = false;
                btnEffectMenuCancel.Enabled = false;
            }

            //Now show the effect menu itself
            btnEffectMenuCancel.Visible = true;
            PanelEffectActivationMenu.Visible = true;
        }
        private void HideOnSummonEffectPanel()
        {
            //Now you can close the On Summon Panel
            PanelEffectActivationMenu.Visible = false;
        }
        private void AdjustPlayerCrestCount(PlayerColor targetPlayer, Crest thisCrest, int amount)
        {
            //Set the Player Data Object to modify
            PlayerData Player = RedData;
            if (targetPlayer == PlayerColor.BLUE) { Player = BlueData; }

            //Adjust the Crest 
            if (amount > 0)
            {
                //Use a loop to anime adding the crests
                for (int x = 0; x < amount; x++)
                {
                    Player.AddCrests(thisCrest, 1);
                    SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                    LoadPlayersInfo();
                    BoardForm.WaitNSeconds(200);
                }
            }
            else
            {
                //Use a loop to anime removing the crests
                for (int x = 0; x < amount; x++)
                {
                    Player.AddCrests(thisCrest, 1);
                    SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                    LoadPlayersInfo();
                    BoardForm.WaitNSeconds(200);
                }
            }
        }
        private bool IsCostMet(Crest crestCost, int amount)
        {
            PlayerData turnPlayerData = RedData;
            if (TURNPLAYER == PlayerColor.BLUE) { turnPlayerData = BlueData; }

            switch (crestCost)
            {
                case Crest.MAG: return amount <= turnPlayerData.Crests_MAG;
                case Crest.TRAP: return amount <= turnPlayerData.Crests_TRAP;
                case Crest.ATK: return amount <= turnPlayerData.Crests_ATK;
                case Crest.DEF: return amount <= turnPlayerData.Crests_DEF;
                case Crest.MOV: return amount <= turnPlayerData.Crests_MOV;
                default: throw new Exception(string.Format("Crest undefined for Cost Met calculation. Crest: [{0}]", crestCost));
            }
        }
        private string GetActivationRequirementStatus(Effect.EffectID thisEffectID)
        {
            return "Requirements Met";
        }
        #endregion

        #region "Dark Symbol"
        private void DarkSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your DARK monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will reach to all summons: if the summon is the owner's monster and it a DARK Attribute: affect it.            
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;

            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Owner == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.Attribute == Attribute.DARK)
                    {
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        //Reload The Tile UI for the card affected
                        thisCard.ReloadTileUI();
                        UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                    }
                }
            }
        }
        private void DarkSymbol_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same owner AND...
            if (targetCard.Owner == thisEffect.Owner)
            {
                //is DARK Attribute...
                if (targetCard.Attribute == Attribute.DARK)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    targetCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] Origin Card Board ID: [{1}] | TO: [{2}] On Board ID: [{3}] Owned by [{4}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID, targetCard.Name, targetCard.OnBoardID, targetCard.Owner));
                }
            }
        }
        #endregion

        #region "LIGHT Symbol"
        private void LightSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your LIGHT monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will reach to all summons: if the summon is the owner's monster and it a LIGHT Attribute: affect it.            
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;

            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Owner == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.Attribute == Attribute.LIGHT)
                    {
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        //Reload The Tile UI for the card affected
                        thisCard.ReloadTileUI();
                        UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                    }
                }
            }
        }
        private void LightSymbol_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same owner AND...
            if (targetCard.Owner == thisEffect.Owner)
            {
                //is LIGHT Attribute...
                if (targetCard.Attribute == Attribute.LIGHT)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    targetCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] Origin Card Board ID: [{1}] | TO: [{2}] On Board ID: [{3}] Owned by [{4}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID, targetCard.Name, targetCard.OnBoardID, targetCard.Owner));
                }
            }
        }
        #endregion

        #region "WATER Symbol"
        private void WaterSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your WATER monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will react to all summons: if the summon is the owner's monster and its a WATER Attribute: affect it.            
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;

            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Owner == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.Attribute == Attribute.WATER)
                    {
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        //Reload The Tile UI for the card affected
                        thisCard.ReloadTileUI();
                        UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                    }
                }
            }
        }
        private void WaterSymbol_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same owner AND...
            if (targetCard.Owner == thisEffect.Owner)
            {
                //is WATER Attribute...
                if (targetCard.Attribute == Attribute.WATER)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    targetCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] Origin Card Board ID: [{1}] | TO: [{2}] On Board ID: [{3}] Owned by [{4}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID, targetCard.Name, targetCard.OnBoardID, targetCard.Owner));
                }
            }
        }
        #endregion

        #region "FIRE Symbol"
        private void FireSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your FIRE monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will react to all summons: if the summon is the owner's monster and its a FIRE Attribute: affect it.            
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;

            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Owner == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.Attribute == Attribute.FIRE)
                    {
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        //Reload The Tile UI for the card affected
                        thisCard.ReloadTileUI();
                        UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                    }
                }
            }
        }
        private void FireSymbol_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same owner AND...
            if (targetCard.Owner == thisEffect.Owner)
            {
                //is FIRE Attribute...
                if (targetCard.Attribute == Attribute.FIRE)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    targetCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] Origin Card Board ID: [{1}] | TO: [{2}] On Board ID: [{3}] Owned by [{4}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID, targetCard.Name, targetCard.OnBoardID, targetCard.Owner));
                }
            }
        }
        #endregion

        #region "EARTH Symbol"
        private void EarthSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your FIRE monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will react to all summons: if the summon is the owner's monster and its a EARTH Attribute: affect it.            
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;

            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Owner == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.Attribute == Attribute.EARTH)
                    {
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        //Reload The Tile UI for the card affected
                        thisCard.ReloadTileUI();
                        UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                    }
                }
            }
        }
        private void EarthSymbol_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same owner AND...
            if (targetCard.Owner == thisEffect.Owner)
            {
                //is EARTH Attribute...
                if (targetCard.Attribute == Attribute.EARTH)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    targetCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] Origin Card Board ID: [{1}] | TO: [{2}] On Board ID: [{3}] Owned by [{4}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID, targetCard.Name, targetCard.OnBoardID, targetCard.Owner));
                }
            }
        }
        #endregion

        #region "WIND Symbol"
        private void WindSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your FIRE monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will react to all summons: if the summon is the owner's monster and its a WIND Attribute: affect it.            
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;

            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Owner == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.Attribute == Attribute.WIND)
                    {
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        //Reload The Tile UI for the card affected
                        thisCard.ReloadTileUI();
                        UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                    }
                }
            }
        }
        private void WindSymbol_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same owner AND...
            if (targetCard.Owner == thisEffect.Owner)
            {
                //is WIND Attribute...
                if (targetCard.Attribute == Attribute.WIND)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    targetCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] Origin Card Board ID: [{1}] | TO: [{2}] On Board ID: [{3}] Owned by [{4}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID, targetCard.Name, targetCard.OnBoardID, targetCard.Owner));
                }
            }
        }
        #endregion

        #region Base Field Spells
        private void BaseFieldSpellActivation(Effect thisEffect, Tile.FieldTypeValue fieldType)
        {
            //EFFECT DESCRIPTION
            //Change the field type of all the active tile in the field activation area (7x7 tile grid)       
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));

            //Step 1: Set the "Reaction To" flags
            //Field Spells are one and done and do not react to other events

            //Step 2: Resolve the effect initial activation
            List<Tile> AllFieldActivationTiles = _CurrentTileSelected.GetFieldSpellActivationTiles(false);
            List<Tile> FieldActivationTiles = _CurrentTileSelected.GetFieldSpellActivationTiles(true);

            //Highligh all the tiles in the field activation area
            foreach (Tile thisTile in AllFieldActivationTiles)
            {
                if (thisTile != null) { thisTile.HighlightTile(); }
            }
            WaitNSeconds(500);
            //then change the field type of all the active tiles
            foreach (Tile thisTile in FieldActivationTiles)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                thisTile.ChangeFieldType(fieldType);
                BoardForm.WaitNSeconds(300);
            }
            //finally. reset the all the tiles to clean up the UI
            foreach (Tile thisTile in AllFieldActivationTiles)
            {
                if (thisTile != null) { thisTile.ReloadTileUI(); }
            }

            //Step 3: Destroy the card once done
            DestroyCard(_CurrentTileSelected);

            //Step 4: Once activation resolution is over, return to the main phase
            EnterMainPhase();
        }
        private void Mountain_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Mountain);
        }
        private void Sogen_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Sogen);
        }
        private void Forest_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Forest);
        }
        private void Wasteland_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Wasteland);
        }
        private void Yami_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Yami);
        }
        private void Umi_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Umi);
        }
        private void Volcano_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Volcano);
        }
        private void Swamp_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Swamp);
        }
        private void Cyberworld_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Cyberworld);
        }
        private void Sanctuary_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Sanctuary);
        }
        private void Scrapyard_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Scrapyard);
        }
        #endregion

        #region "M-Warrior #1"
        private void MWarrior1_OnSummonActivation(Effect thisEffect)
        {
            //Since this is a ON SUMMON EFFECT, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonEffectPanel(thisEffect);

            //EFFECT DESCRIPTION
            //Will increase the Attack of ANY monster on the board with the name "M-Warrior #2" by 500.
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsASymbol && !thisCard.IsDiscardted && thisCard.Name == "M-Warrior #2")
                {
                    thisCard.AdjustAttackBonus(500);
                    thisEffect.AddAffectedByCard(thisCard);
                    //Reload The Tile UI for the card affected
                    thisCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                }
            }

            HideOnSummonEffectPanel();

            //At this point end the summoning phase
            EnterMainPhase();
        }
        #endregion

        #region "Hitotsu-Me Giant"
        private void HitotsumeGiant_OnSummonActivation(Effect thisEffect)
        {
            //Since this is a ON SUMMON EFFECT, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonEffectPanel(thisEffect);

            //EFFECT DESCRIPTION
            // Add 3 [ATK] to the controller's crest pool
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
            PlayerColor ControllersColor = thisEffect.Owner;
            AdjustPlayerCrestCount(ControllersColor, Crest.ATK, 3);

            HideOnSummonEffectPanel();

            //At this point end the summoning phase
            EnterMainPhase();
        }
        #endregion

        #region "Thunder Dragon"
        private void ThunderDragon_Continuous(Effect thisEffect)
        {
            //Since this is a ON SUMMON EFFECT, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonContinuousEffectPanel(thisEffect);

            //EFFECT DESCRIPTION
            //increase the DEF off all controller's owned Thunder-Type monsters by 500. (EXCEPT THE ORIGIN CARD)
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Owner == thisEffect.Owner)
                {
                    if (!thisCard.IsDiscardted && thisCard.Type == Type.Thunder && thisCard.OnBoardID != thisEffect.OriginCard.OnBoardID)
                    {
                        thisCard.AdjustDefenseBonus(500);
                        thisEffect.AddAffectedByCard(thisCard);
                        //Reload The Tile UI for the card affected
                        thisCard.ReloadTileUI();
                        UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                    }
                }
            }

            HideOnSummonEffectPanel();

            //At this point end the summoning phase
            EnterMainPhase();
        }
        private void ThunderDragon_TryToApplyToNewCard(Effect thisEffect, Card targetCard)
        {
            if (targetCard.Owner == thisEffect.Owner)
            {
                if (targetCard.Type == Type.Thunder)
                {
                    targetCard.AdjustDefenseBonus(500);
                    thisEffect.AddAffectedByCard(targetCard);
                    //Reload The Tile UI for the card affected
                    targetCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] Origin Card Board ID: [{1}] | TO: [{2}] On Board ID: [{3}] Owned by [{4}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID, targetCard.Name, targetCard.OnBoardID, targetCard.Owner));
                }
            }
        }
        private void ThunderDragon_RemoveEffect(Effect thisEffect, int indexID)
        {
            //Remove Effect Description:
            //DECREASE the DEF of all affected by monsters by 500
            foreach (Card thisCard in thisEffect.AffectedByList)
            {
                thisCard.AdjustDefenseBonus(-500);
                //Reload The Tile UI for the card affected
                thisCard.ReloadTileUI();
                UpdateEffectLogs(string.Format("Effect Removed: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
            }
        }
        #endregion
        #endregion

        #region Enums
        private enum GameState
        {
            TurnStartMenu,
            BoardViewMode,
            MainPhaseBoard,
            ActionMenuDisplay,
            MovingCard,
            SelectingAttackTarger,
            BattlePhase,
            SetCard,
            SummonCard,
            EffectMenuDisplay
        }
        #endregion      
    }

    public enum PlayerColor
    {
        NONE,
        RED,
        BLUE,
    }
}