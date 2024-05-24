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
            btnFusionSummon1.MouseEnter += OnMouseHoverSound;
            btnFusionSummon2.MouseEnter += OnMouseHoverSound;
            btnFusionSummon3.MouseEnter += OnMouseHoverSound;

            //Save Ref to each player's data
            UserPlayerColor = UserColor;
            RedData = Red; BlueData = Blue;
            //RED player is the starting player so initialize the TURNPLAYERDATA
            TURNPLAYERDATA = RedData;
            OPPONENTPLAYERDATA = BlueData;

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
                    statsLabelDEF.TextAlign = System.Drawing.ContentAlignment.TopLeft;
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
                    statsLabelATK.TextAlign = System.Drawing.ContentAlignment.TopLeft;
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
            LoadCardInfoPanel(_Tiles[0]);

            //Set the initial game state and start the turn start panel            
            LaunchTurnStartPanel();

            //Initialize both Symbols Continuous Effect
            ActivateEffect(_RedSymbol.ContinuousEffect);
            ActivateEffect(_BlueSymbol.ContinuousEffect);
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
            btnFusionSummon1.MouseEnter += OnMouseHoverSound;
            btnFusionSummon2.MouseEnter += OnMouseHoverSound;
            btnFusionSummon3.MouseEnter += OnMouseHoverSound;

            //Save Ref to each player's data
            UserPlayerColor = UserColor;
            RedData = Red; BlueData = Blue;
            //RED player is the starting player so initialize the TURNPLAYERDATA
            TURNPLAYERDATA = RedData;
            OPPONENTPLAYERDATA = BlueData;

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
            LoadCardInfoPanel(_Tiles[0]);

            //Set the initial game state and start the turn start panel            
            LaunchTurnStartPanel();

            //Initialize both Symbols Continuous Effect
            ActivateEffect(_RedSymbol.ContinuousEffect);
            ActivateEffect(_BlueSymbol.ContinuousEffect);

            //Load the scenario file to set the board
            LoadScenarioFile();
           
            void LoadScenarioFile()
            {
                //Stream that reads the actual save file.
                StreamReader SR_SaveFile = new StreamReader(
                    Directory.GetCurrentDirectory() + "\\Save Files\\ScenarioFile.txt");

                //Line[0] = "RED TILES" Header
                string Line = SR_SaveFile.ReadLine();

                //Line[1] = tile ids
                Line = SR_SaveFile.ReadLine();
                string[] RedTileIds = Line.Split('|');
                foreach(string thisTileId in RedTileIds)
                {
                    int intThisTileID = Convert.ToInt32(thisTileId);
                    _Tiles[intThisTileID].ChangeOwner(PlayerColor.RED);
                }

                //Line[2] = "BLUE TILES" Header
                Line = SR_SaveFile.ReadLine();

                //Line[3] = tile ids
                Line = SR_SaveFile.ReadLine();
                string[] BlueTileIds = Line.Split('|');
                foreach (string thisTileId in BlueTileIds)
                {
                    int intThisTileID = Convert.ToInt32(thisTileId);
                    _Tiles[intThisTileID].ChangeOwner(PlayerColor.BLUE);
                }

                //Read the break line
                Line = SR_SaveFile.ReadLine();

                //Line[4] = RED MONSTERS COUNT
                Line = SR_SaveFile.ReadLine();
                string[] RedMonsterHeader = Line.Split('|');
                int RedMonsterCount = Convert.ToInt32(RedMonsterHeader[1]);

                //Line[5-N]
                for(int x = 0; x < RedMonsterCount; x++)
                {
                    Line = SR_SaveFile.ReadLine();
                    string[] MonsterInfo = Line.Split('|');
                    int thisMonsterId = Convert.ToInt32(MonsterInfo[0]);
                    int thisTileId = Convert.ToInt32(MonsterInfo[1]);
                    CardInfo thisRedMonsterInfo = CardDataBase.GetCardWithID(thisMonsterId);
                    Card thisCard = new Card(thisMonsterId, thisRedMonsterInfo, PlayerColor.RED, false);
                    _CardsOnBoard.Add(thisCard);
                    _Tiles[thisTileId].SummonCard(thisCard);
                }

                //Read the break line
                Line = SR_SaveFile.ReadLine();

                //Line[6] = BLUE MONSTERS COUNT
                Line = SR_SaveFile.ReadLine();
                string[] BlueMonsterHeader = Line.Split('|');
                int BlueMonsterCount = Convert.ToInt32(BlueMonsterHeader[1]);
               
                //Line[6-N]
                for (int x = 0; x < BlueMonsterCount; x++)
                {
                    Line = SR_SaveFile.ReadLine();
                    string[] MonsterInfo = Line.Split('|');
                    int thisMonsterId = Convert.ToInt32(MonsterInfo[0]);
                    int thisTileId = Convert.ToInt32(MonsterInfo[1]);
                    CardInfo thisBlueMonsterInfo = CardDataBase.GetCardWithID(thisMonsterId);

                    Card thisCard = new Card(thisMonsterId, thisBlueMonsterInfo, PlayerColor.BLUE, false);
                    _CardsOnBoard.Add(thisCard);
                    _Tiles[thisTileId].SummonCard(thisCard);
                }

                //Read the break line
                Line = SR_SaveFile.ReadLine();

                //Line[7] = RED SET CARDS COUNT
                Line = SR_SaveFile.ReadLine();
                string[] RedSetCardsHeader = Line.Split('|');
                int RedSetCardsCount = Convert.ToInt32(RedSetCardsHeader[1]);
                for (int x = 0; x < RedSetCardsCount; x++)
                {
                    Line = SR_SaveFile.ReadLine();
                    string[] CardInfo = Line.Split('|');
                    int thisCardId = Convert.ToInt32(CardInfo[0]);
                    int thisTileId = Convert.ToInt32(CardInfo[1]);
                    CardInfo thisRedCardInfo = CardDataBase.GetCardWithID(thisCardId);
                    Card thisSetCard = new Card(_CardsOnBoard.Count, thisRedCardInfo, PlayerColor.RED, true);
                    _CardsOnBoard.Add(thisSetCard);
                    _Tiles[thisTileId].SetCard(thisSetCard);
                }

                //Read the break line
                Line = SR_SaveFile.ReadLine();

                //Line[8] = BLUE SET CARDS COUNT
                Line = SR_SaveFile.ReadLine();
                string[] BlueSetCardsHeader = Line.Split('|');
                int BlueSetCardsCount = Convert.ToInt32(BlueSetCardsHeader[1]);
                for (int x = 0; x < BlueSetCardsCount; x++)
                {
                    Line = SR_SaveFile.ReadLine();
                    string[] CardInfo = Line.Split('|');
                    int thisCardId = Convert.ToInt32(CardInfo[0]);
                    int thisTileId = Convert.ToInt32(CardInfo[1]);
                    CardInfo thisBlueCardInfo = CardDataBase.GetCardWithID(thisCardId);
                    Card thisSetCard = new Card(_CardsOnBoard.Count, thisBlueCardInfo, PlayerColor.BLUE, true);
                    _CardsOnBoard.Add(thisSetCard);
                    _Tiles[thisTileId].SetCard(thisSetCard);
                }

                //Read the break line
                Line = SR_SaveFile.ReadLine();

                //LINE[9] = Red Crests
                Line = SR_SaveFile.ReadLine();
                string[] RedCrestsData = Line.Split('|');
                RedData.AddCrests(Crest.MOV, Convert.ToInt32(RedCrestsData[1]));
                RedData.AddCrests(Crest.ATK, Convert.ToInt32(RedCrestsData[2]));
                RedData.AddCrests(Crest.DEF, Convert.ToInt32(RedCrestsData[3]));
                RedData.AddCrests(Crest.MAG, Convert.ToInt32(RedCrestsData[4]));
                RedData.AddCrests(Crest.TRAP, Convert.ToInt32(RedCrestsData[5]));

                //LINE[10] = Blue Crests
                Line = SR_SaveFile.ReadLine();
                string[] BlueCrestsData = Line.Split('|');
                BlueData.AddCrests(Crest.MOV, Convert.ToInt32(BlueCrestsData[1]));
                BlueData.AddCrests(Crest.ATK, Convert.ToInt32(BlueCrestsData[2]));
                BlueData.AddCrests(Crest.DEF, Convert.ToInt32(BlueCrestsData[3]));
                BlueData.AddCrests(Crest.MAG, Convert.ToInt32(BlueCrestsData[4]));
                BlueData.AddCrests(Crest.TRAP, Convert.ToInt32(BlueCrestsData[5]));

                SR_SaveFile.Close();
            }
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
            //Relaod the player info panels to update crests
            LoadPlayersInfo();
            //And enter the main phase
            EnterMainPhase();
        }
        public void SetupSummonCardPhase(CardInfo card)
        {
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

            //Switch to the Summon Card State
            _CurrentGameState = GameState.SummonCard;
        }
        public void SetupSetCardPhase(CardInfo card)
        {            
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

            //Switch to the Set Card Phase of the player
            _CurrentGameState = GameState.SetCard;
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
        private void LoadCardInfoPanel(Tile targetTile)
        {
            if (targetTile.IsOccupied)
            {
                //Get the CardInfo object to populate the UI
                Card thisCard = targetTile.CardInPlace;

                int cardID = thisCard.CardID;

                //Set the Panel Back Color based on whose the card owner
                if (thisCard.Owner == PlayerColor.RED)
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
                        lblCardText.Text = thisCard.ContinuousEffectText;
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
                            fullcardtext = fullcardtext + "[On Summon] - " + thisCard.OnSummonEffectText + "\n\n";
                        }

                        if (thisCard.HasContinuousEffect)
                        {
                            fullcardtext = fullcardtext + "[Continuous] - " + thisCard.ContinuousEffectText + "\n\n";
                        }

                        if (thisCard.HasAbility)
                        {
                            fullcardtext = fullcardtext + "[Ability] - " + thisCard.Ability + "\n\n";
                        }

                        if (thisCard.HasIgnitionEffect)
                        {
                            fullcardtext = fullcardtext + thisCard.IgnitionEffectText + "\n\n";
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
        private void LoadFieldTypeDisplay(Tile thisTile, bool isHovering)
        {
            if (isHovering && thisTile.Owner != PlayerColor.NONE)
            {
                //Panel will display
                PanelFieldType.Visible = true;

                //Give the display the color of the Tile Owner Color, this is in case the tile has no field type
                //set, then the tile display will display the base tile color.
                if (thisTile.Owner == PlayerColor.RED) { PicFieldTypeDisplay.BackColor = Color.DarkRed; }
                else { PicFieldTypeDisplay.BackColor = Color.DarkBlue; }

                //If field type is set, load the proper image
                if (thisTile.FieldType != Tile.FieldTypeValue.None)
                {
                    ImageServer.LoadImage(PicFieldTypeDisplay, CardImageType.FieldTile, thisTile.FieldType.ToString());

                }
                else
                {
                    if (PicFieldTypeDisplay.Image != null) { PicFieldTypeDisplay.Image.Dispose(); }
                    PicFieldTypeDisplay.Image = null;
                }

                //Update the Field Type name label
                lblFieldTypeName.Text = thisTile.FieldType.ToString();
            }
            else
            {
                PanelFieldType.Visible = false;
            }
        }
        private void DisplayMoveCandidates()
        {
            //Clear the current candidate list
            _MoveCandidates.Clear();

            //Check each adjencent tile in every direction, if the Tile is Active and is not occupied, then it is a move candidate
            List<Tile.TileDirection> DirectionsToCheck = new List<Tile.TileDirection>() { Tile.TileDirection.North, Tile.TileDirection.South, Tile.TileDirection.East, Tile.TileDirection.West };
            foreach(Tile.TileDirection thisDirection in DirectionsToCheck)
            {
                if (_PreviousTileMove.HasAnAdjecentTile(thisDirection))
                {
                    Tile thisTile = _PreviousTileMove.GetAdjencentTile(thisDirection);
                    if (thisTile.Owner != PlayerColor.NONE)
                    {
                        if (!thisTile.IsOccupied)
                        {
                            //Change the Adjencent tile's border to gree to mark that you can move
                            thisTile.MarkFreeToMove();
                            _MoveCandidates.Add(thisTile);
                        }
                    }
                }
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
            Point referencePoint = _PreviousTileMove.Location;
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
        private void AdjustPlayerCrestCount(PlayerColor targetPlayer, Crest thisCrest, int amount)
        {
            //Set the Player Data Object to modify
            PlayerData Player = GetPlayerData(targetPlayer);

            //Adjust the Crest 
            if (amount > 0)
            {
                //Use a loop to anime adding the crests
                for (int x = 0; x < amount; x++)
                {
                    Player.AddCrests(thisCrest, 1);
                    SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                    LoadPlayersInfo();
                    WaitNSeconds(200);
                }
            }
            else
            {
                //Use a loop to anime removing the crests
                int Amount = -(amount);
                for (int x = Amount; x >= 1; x--)
                {
                    Player.RemoveCrests(thisCrest, 1);
                    SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                    LoadPlayersInfo();
                    WaitNSeconds(200);
                }
            }
        }
        private void UpdateDebugWindow()
        {
            if(_CurrentTileSelected == null)
            {
                lblDebugCard.Text = "No tile selected";
                lblDebugCardOwner.Text = "";
            }
            else
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
            }
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
        private PlayerData GetPlayerData(PlayerColor thisColor)
        {
            PlayerData Player = RedData;
            if (thisColor == PlayerColor.BLUE) { Player = BlueData; }
            return Player;
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
            /*if (strGameState == _CurrentGameState.ToString())
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
                    case "[READY FUSION CANDIDATES]": ReadyFusionCandidatesReceived(MessageTokens[2], MessageTokens[3], MessageTokens[4]); break;
                    case "[FUSION SELECTION MENU SELECT]": btnFusionSummon_Base(MessageTokens[2]); break;
                    case "[CLICK TILE TO FUSION MATERIAL]": TileClick_FusionMaterial_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[CLICK TILE TO FUSION SUMMON]": TileClick_FusionSummon_Base(Convert.ToInt32(MessageTokens[2])); break;
                }
            }
            else
            {
                throw new Exception("Message Received with an invalid game state");
            }*/

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
                case "[READY FUSION CANDIDATES]": ReadyFusionCandidatesReceived(MessageTokens[2], MessageTokens[3], MessageTokens[4]); break;
                case "[FUSION SELECTION MENU SELECT]": btnFusionSummon_Base(MessageTokens[2]); break;
                case "[CLICK TILE TO FUSION MATERIAL]": TileClick_FusionMaterial_Base(Convert.ToInt32(MessageTokens[2])); break;
                case "[CLICK TILE TO FUSION SUMMON]": TileClick_FusionSummon_Base(Convert.ToInt32(MessageTokens[2])); break;
            }
        }
        #endregion

        #region Turn Steps Functions
        private void SummonMonster(CardInfo thisCardToBeSummoned, int tileId)
        {
            //then summon the card
            Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(thisCardToBeSummoned.ID), TURNPLAYER, false);
            _CardsOnBoard.Add(thisCard);
            _Tiles[tileId].SummonCard(thisCard);

            //Wait 1 sec for the sound effect to finish
            WaitNSeconds(1000);

            //Check for active effects that react to monster summons
            UpdateEffectLogs(string.Format("Card Summoned: [{0}] On Board ID: [{1}] Owned By: [{2}] - Checking for Active Effects to Apply.", thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
            ResolveEffectsWithSummonReactionTo(thisCard);

            //Now check if the Monster has an "On Summon"/"Continuous" effect and try to activate
            if (thisCard.HasOnSummonEffect && thisCard.EffectsAreImplemented)
            {
                //Create the effect object and activate
                Effect thisCardsEffect = new Effect(thisCard, Effect.EffectType.OnSummon);
                ActivateEffect(thisCardsEffect);
            }
            else if (thisCard.HasContinuousEffect && thisCard.EffectsAreImplemented)
            {
                //Create the effect object and activate
                Effect thisCardsEffect = new Effect(thisCard, Effect.EffectType.Continuous);
                ActivateEffect(thisCardsEffect);
            }
            else
            {
                EnterMainPhase();
            }

            void ResolveEffectsWithSummonReactionTo(Card targetCard)
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
                            case Effect.EffectID.KarbonalaWarrior_Continuous: KarbonalaWarrior_ReactTo_MonsterSummon(thisActiveEffect, targetCard); break;
                            case Effect.EffectID.ThunderDragon_Continuous: ThunderDragon_TryToApplyToNewCard(thisActiveEffect, targetCard); break;
                            default: throw new Exception(string.Format("Effect ID: [{0}] does not have an EffectToApply Function", thisActiveEffect.ID));
                        }
                    }
                }
            }
        }
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
                    //Step 0: Define the Attaker/Defender Card Objects
                    Card Attacker = _AttackerTile.CardInPlace;
                    Card Defender = _AttackTarger.CardInPlace;

                    //Step 1: Determine the BonusCrest (if ANY)
                    // _AttackBonusCrest = THIS HAS BEEN PREVIOUSLY SET AT THIS POINT
                    // _DefenseBonusCrest = THIS HAS BEEN PREVIOUSLY SET AT THIS POINT
                    //Reveal the Bonus points
                    lblAttackerBonus.Text = string.Format("Bonus: {0}", (_AttackBonusCrest * 200));
                    lblDefenderBonus.Text = string.Format("Bonus: {0}", (_DefenseBonusCrest * 200));

                    //Step 2: Calculate the Final Attack Value (Attacker's Base ATK + (Bonus [ATK] used * 200))
                    int FinalAttack = Attacker.ATK + (_AttackBonusCrest * 200);

                    //Step 3: Calculate the Final Defense Value (if Defender choose to defend) (Defender's Base DEF + (Bonus [DEF] used * 200))
                    int FinalDefense = 0;
                    if (_DefenderDefended)
                    {
                        FinalDefense = Defender.DEF + (_DefenseBonusCrest * 200);
                    }

                    //Step 4: Reduce the [ATK] and [DEF] to the respective player.                   
                    int creststoremoveATK = _AttackBonusCrest + Attacker.AttackCost;
                    int creststoremoveDEF = _DefenseBonusCrest + Defender.DefenseCost;
                    if (!_DefenderDefended) { creststoremoveDEF = 0; }
                    PlayerData AttackerPlayerData = TURNPLAYERDATA;
                    PlayerData DefenderPlayerData = OPPONENTPLAYERDATA;
                    PlayerColor AttackerColor = TURNPLAYER;
                    PlayerColor DefenderColor = OPPONENTPLAYER;                   
                    AdjustPlayerCrestCount(AttackerColor, Crest.ATK, -creststoremoveATK);
                    AdjustPlayerCrestCount(DefenderColor, Crest.DEF, -creststoremoveDEF);

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
                    int FinalAttack = _AttackerTile.CardInPlace.ATK;

                    //Step 3: Final Defense is always 0
                    int FinalDefense = 0;

                    //Step 4: Reduce the [ATK] from the attacker. Defender always "Passes"
                    Card Attacker = _AttackerTile.CardInPlace;
                    AdjustPlayerCrestCount(TURNPLAYER, Crest.ATK, -Attacker.AttackCost);

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
                    Card Attacker = _AttackerTile.CardInPlace;
                    AdjustPlayerCrestCount(TURNPLAYER, Crest.ATK, -Attacker.AttackCost);

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
            if(_ActiveEffects.Contains(thisCard.ContinuousEffect))
            {
                Effect thisEffect = thisCard.ContinuousEffect;
                UpdateEffectLogs(string.Format("Card Destroyed: [{0}] On Board ID: [{1}] Owned by: [{2}] | Removing Continuous Effect: [{3}]", thisCard.Name, thisCard.OnBoardID, thisCard.Owner, thisEffect.ID));
                RemoveEffect(thisEffect);
            }

            //Finally, check if any active effects react to a card destryuction
            foreach (Effect thisEffect in _ActiveEffects) 
            {
                if(thisEffect.ReactsToMonsterDestroyed && thisCard.Category == Category.Monster) 
                {
                    //then resolve the effct reaction
                    switch (thisEffect.ID)
                    {
                        case Effect.EffectID.KarbonalaWarrior_Continuous: KarbonalaWarrior_ReactTo_MonsterDestroyed(thisEffect, thisCard); break;
                        default: throw new Exception(string.Format("This effect id: [{0}] does not have a React to Monster Destroyed method assigned", thisEffect.ID));
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

            if (_CurrentTileSelected != null) { _CurrentTileSelected.ReloadTileUI(); }
            PanelBoard.Enabled = true;
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
        private void PromptPlayerToSelectFusionMaterial()
        {
            //Prompts the player to select the fusion material on index 0 
            //in the _FusionMaterialsToBeUsed list

            //Step 1: Set the card name of the Fusion Material that will be selected
            string FusionMaterial = _FusionMaterialsToBeUsed[0];

            //Step 2: Display on the UI the Fusion Material candidates
            DisplayFusionMaterialCandidates();
            if (UserPlayerColor == TURNPLAYER)
            {
                lblActionInstruction.Text = string.Format("Select a [{0}] as fusion material!", FusionMaterial);
                lblActionInstruction.Visible = true;
            }
            else
            {
                lblActionInstruction.Text = "Opponent is selecting a fusion material!";
                lblActionInstruction.Visible = true;
            }

            //Step 3: Change the game state so the turn player can select the tile of the candidate
            PanelBoard.Enabled = true;
            _CurrentGameState = GameState.FusionMaterialCandidateSelection;

            void DisplayFusionMaterialCandidates()
            {
                //Just in case, reset the tile UI of the previous list
                if (_FusionCandidateTiles.Count > 0)
                {
                    foreach (Tile thisTile in _FusionCandidateTiles)
                    {
                        thisTile.ReloadTileUI();
                    }
                }


                //Generate the Tile list candidates for this Fusion Materials
                _FusionCandidateTiles.Clear();
                foreach (Tile thisTile in _Tiles)
                {
                    if (thisTile.IsOccupied)
                    {
                        if (thisTile.CardInPlace.Name == FusionMaterial && thisTile.CardInPlace.Owner == TURNPLAYER)
                        {
                            _FusionCandidateTiles.Add(thisTile);
                        }
                    }
                }

                //Now mark the Candidates
                foreach (Tile thisTile in _FusionCandidateTiles)
                {
                    thisTile.MarkFusionMaterialTarget();
                }
            }
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
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[Roll Dice Action]|" + _CurrentGameState.ToString());

                //Perform the action
                btnRoll_Base();
            }            
        }
        private void btnViewBoard_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[View Board Action]|" + _CurrentGameState.ToString());

                //Perform the action
                btnViewBoard_Base();
            }            
        }
        private void btnReturnToTurnMenu_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[EXIT VIEW BOARD MODE]|" + _CurrentGameState.ToString());

                //Perform the action
                btnReturnToTurnMenu_Base();
            }            
        }
        #endregion

        #region Board Tiles
        private void OnMouseEnterPicture(object sender, EventArgs e)
        {
            //Only allow this event if the user is the TURN PLAYER
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                //Extract the TileID from the tile in action
                PictureBox thisPicture = sender as PictureBox;
                int tileID = Convert.ToInt32(thisPicture.Tag);

                //Send the action message to the server
                if ((_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard ||
                    _CurrentGameState == GameState.SummonCard || _CurrentGameState == GameState.SetCard ||
                    _CurrentGameState == GameState.FusionMaterialCandidateSelection))
                {
                    SendMessageToServer(string.Format("{0}|{1}|{2}", "[ON MOUSE ENTER TILE]", _CurrentGameState.ToString(), tileID.ToString()));

                    //Perform the action
                    OnMouseEnterPicture_Base(tileID);
                }               
            }
        }
        private void OnMouseLeavePicture(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                //Extract the TileID from the tile in action
                PictureBox thisPicture = sender as PictureBox;
                int tileID = Convert.ToInt32(thisPicture.Tag);

                //Send the action message to the server
                if ((_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard ||
                    _CurrentGameState == GameState.SummonCard || _CurrentGameState == GameState.SetCard ||
                     _CurrentGameState == GameState.FusionMaterialCandidateSelection))
                {
                    SendMessageToServer(string.Format("{0}|{1}|{2}", "[ON MOUSE LEAVE TILE]", _CurrentGameState.ToString(), tileID.ToString()));

                    //Perform the action
                    OnMouseLeavePicture_Base(tileID);
                }               
            }
        }
        private void Tile_Click(object sender, EventArgs e)
        {
            //Extract the TileID from the tile in action
            
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                //set the current tile selected
                PictureBox thisPicture = sender as PictureBox;
                int tileID = Convert.ToInt32(thisPicture.Tag);
                Tile thisTile = _Tiles[tileID];

                if (_CurrentGameState == GameState.MainPhaseBoard)
                {
                    if (thisTile.IsOccupied)
                    {
                        if (thisTile.CardInPlace.Owner == UserPlayerColor)
                        {
                            _CurrentGameState = GameState.NOINPUT;

                            //Send the action message to the server
                            SendMessageToServer(string.Format("[CLICK TILE TO ACTION]|{0}|{1}", _CurrentGameState.ToString(), tileID));

                            //Perform the action
                            TileClick_MainPhase_Base(tileID);
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
                    if (_MoveCandidates.Contains(thisTile))
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
                        _CurrentGameState = GameState.NOINPUT;

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
                        _CurrentGameState = GameState.NOINPUT;

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
                        _CurrentGameState = GameState.NOINPUT;

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
                else if (_CurrentGameState == GameState.FusionMaterialCandidateSelection)
                {
                    if(_FusionCandidateTiles.Contains(thisTile))
                    {
                        _CurrentGameState = GameState.NOINPUT;

                        //Send the action message to the server
                        SendMessageToServer(string.Format("{0}|{1}|{2}", "[CLICK TILE TO FUSION MATERIAL]", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_FusionMaterial_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.FusionSummonTileSelection)
                {
                    if (_FusionSummonTiles.Contains(thisTile))
                    {
                        _CurrentGameState = GameState.NOINPUT;

                        //Send the action message to the server
                        SendMessageToServer(string.Format("{0}|{1}|{2}", "[CLICK TILE TO FUSION SUMMON]", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_FusionSummon_Base(tileID);
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
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer(string.Format("{0}|{1}", "[END TURN]", _CurrentGameState.ToString()));

                //Perform the action
                btnEndTurn_Base();
            }           
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
                _CurrentGameState = GameState.NOINPUT;

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
                _CurrentGameState = GameState.NOINPUT;

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
                _CurrentGameState = GameState.NOINPUT;

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
                _CurrentGameState = GameState.NOINPUT;

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
                _CurrentGameState = GameState.NOINPUT;

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
                _CurrentGameState = GameState.NOINPUT;

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
                _CurrentGameState = GameState.NOINPUT;

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
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
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
        }
        private void btnBattleMenuDefend_Click(object sender, EventArgs e)
        {
            //Hide the Defend Controls Panel immediately to prevent any input
            PanelDefendControls.Visible = false;
            SoundServer.PlaySoundEffect(SoundEffect.Attack);

            //Send the opponent the action taken
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[DEFEND!]", _CurrentGameState.ToString(), _DefenseBonusCrest));
            
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
                PlayerData AttackerData = TURNPLAYERDATA;
                Card Attacker = _AttackerTile.CardInPlace;
                lblAttackerCrestCount.Text = string.Format("[ATK] to use: {0}/{1}", (Attacker.AttackCost + _AttackBonusCrest), AttackerData.Crests_ATK);
            }
        }
        private void lblAttackerAdvPlus_Click(object sender, EventArgs e)
        {
            if (_AttackBonusCrest < 5)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                PlayerData AttackerData = TURNPLAYERDATA;
                Card Attacker = _AttackerTile.CardInPlace;

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
                PlayerData DefenderData = OPPONENTPLAYERDATA;
                Card Defender = _AttackTarger.CardInPlace;
                lblDefenderCrestCount.Text = string.Format("[DEF] to use: {0}/{1}", (Defender.DefenseCost + _DefenseBonusCrest), DefenderData.Crests_DEF);
            }
        }
        private void lblDefenderAdvPlus_Click(object sender, EventArgs e)
        {
            if (_DefenseBonusCrest < 5)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                PlayerData DefenderData = OPPONENTPLAYERDATA;
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
            //Hide the Defend Controls Panel immediately to prevent any input
            PanelDefendControls.Visible = false;

            SoundServer.PlaySoundEffect(SoundEffect.Attack);

            //Send the opponent the action taken
            SendMessageToServer(string.Format("{0}|{1}", "[PASS!]", _CurrentGameState.ToString()));

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
            _CurrentGameState = GameState.NOINPUT;

            //Send the action message to the server
            SendMessageToServer("[END BATTLE]|" + _CurrentGameState.ToString());

            //Perform the action
            btnEndBattle_Base();
        }
        #endregion

        #region Effect Menu
        private void btnEffectMenuCancel_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[CLICK CANCEL EFFECT MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnEffectMenuCancel_Base();
            }
        }
        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[CLICK ACTIVATE EFFECT MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnEffectMenuActivate_Base();
            }
        }
        #endregion

        #region Fusion Controls
        private void btnFusionSummon1_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer(string.Format("[FUSION SELECTION MENU SELECT]|{0}|{1}", _CurrentGameState.ToString(), "0"));

                //Perform the action
                btnFusionSummon_Base("0");
            }           
        }
        private void btnFusionSummon2_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer(string.Format("[FUSION SELECTION MENU SELECT]|{0}|{1}", _CurrentGameState.ToString(), "1"));

                //Perform the action
                btnFusionSummon_Base("1");
            }                
        }
        private void btnFusionSummon3_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer(string.Format("[FUSION SELECTION MENU SELECT]|{0}|{1}", _CurrentGameState.ToString(), "2"));

                //Perform the action
                btnFusionSummon_Base("2");
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
                Tile thisTile = _Tiles[tileId];
                if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard ||
                    _CurrentGameState == GameState.SetCard || _CurrentGameState == GameState.FusionMaterialCandidateSelection)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Hover);
                    thisTile.Hover();

                    UpdateDebugWindow();
                    LoadCardInfoPanel(thisTile);
                    LoadFieldTypeDisplay(thisTile, true);
                }
                else if (_CurrentGameState == GameState.SummonCard)
                {
                    //Highlight the possible dimmension
                    SoundServer.PlaySoundEffect(SoundEffect.Hover);

                    //Use the following function to get the ref to the tiles that compose the dimension
                    _dimensionTiles = thisTile.GetDimensionTiles(_CurrentDimensionForm);

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
                Tile thisTile = _Tiles[tileId];
                if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard)
                {
                    thisTile.ReloadTileUI();
                    LoadFieldTypeDisplay(thisTile, false);
                }
                else if (_CurrentGameState == GameState.SetCard)
                {
                    thisTile.ReloadTileUI();
                    LoadFieldTypeDisplay(thisTile, false);
                    //Redraw the candudates
                    DisplaySetCandidates();
                }
                else if (_CurrentGameState == GameState.SummonCard)
                {
                    //Restore the possible dimmension tiles to their OG colors

                    //Use the following function to get the ref to the tiles that compose the dimension
                    Tile[] dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                    //Reset the color of the dimensionTiles
                    for (int x = 0; x < dimensionTiles.Length; x++)
                    {
                        if (dimensionTiles[x] != null) { dimensionTiles[x].ReloadTileUI(); }
                    }
                }
                else if (_CurrentGameState == GameState.FusionMaterialCandidateSelection) 
                {
                    thisTile.ReloadTileUI();
                    if(_FusionCandidateTiles.Contains(thisTile))
                    {
                        thisTile.MarkFusionMaterialTarget();
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
                _CurrentGameState = GameState.NOINPUT;
                SoundServer.PlaySoundEffect(SoundEffect.SummonMonster);

                //Initialize the Dimension tiles again (Oppoenent's UI doesnt get them initialize them on hover)
                _dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                //Dimension the tiles
                foreach (Tile tile in _dimensionTiles)
                {
                    tile.ChangeOwner(TURNPLAYER);
                }
               
                //Clean the UI
                lblActionInstruction.Visible = false;
                PanelDimenFormSelector.Visible = false;
                _CurrentDimensionForm = DimensionForms.CrossBase;

                //Now run the Master Summon Monster function
                SummonMonster(_CardToBeSummon, tileId);
            }));
        }
        private void TileClick_SetCard_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _CurrentGameState = GameState.NOINPUT;
                SoundServer.PlaySoundEffect(SoundEffect.SetCard);

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
            }));            
        }
        private void TileClick_MainPhase_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Step 1: Generate the Card object on the card in action, we'll use it later.
                _CurrentTileSelected = _Tiles[tileId];
                Card thiscard = _CurrentTileSelected.CardInPlace;

                //Step 2: Open the Action menu by setting its dynamic location and making it visible
                //THIS DOESNT WORK BC IT SHUTS DOWN EVERYTHING INSIDE INCLUDING THE ACTION MENUS: PanelBoard.Enabled = false;
                PositionActionMenu();
                PanelActionMenu.Visible = true;
                btnActionCancel.Enabled = true;
              
                //Step 3A: Enable the Action Buttons based on the card in action
                if (UserPlayerColor == TURNPLAYER)
                {
                    //Hide the End Turn Button for the turn player 
                    btnEndTurn.Visible = false;

                    //Check if Card can move                
                    if (CanCardMove(thiscard, TURNPLAYERDATA))
                    {
                        btnActionMove.Enabled = true;
                        _TMPMoveCrestCount = TURNPLAYERDATA.Crests_MOV;
                        lblMoveMenuCrestCount.Text = string.Format("[MOV]x {0}", _TMPMoveCrestCount);
                    }
                    else
                    {
                        btnActionMove.Enabled = false;
                    }

                    //Check if Card can attack
                    if (CanCardAttack(thiscard, TURNPLAYERDATA))
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
                    if(thiscard.IsFaceDown)
                    {
                        lblActionInstruction.Text = "Opponent selected a face-down card for action!";
                    }
                    else
                    {
                        lblActionInstruction.Text = string.Format("Opponent selected {0} for action!", thiscard.Name);
                    }
                    lblActionInstruction.Visible = true;
                    btnActionMove.Enabled = false;
                    btnActionAttack.Enabled = false;
                    btnActionEffect.Enabled = false;
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
                else if (thiscard.Category == Category.Spell && thiscard.IsFaceDown && (thiscard.HasContinuousEffect || thiscard.HasIgnitionEffect))
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
                foreach(Effect thisEffect in _ActiveEffects)
                {
                    if(thisEffect.IsAOneTurnIgnition)
                    {
                        RemoveEffect(thisEffect);
                    }
                }

                //Change the TURNPLAYER
                if (TURNPLAYER == PlayerColor.RED) 
                { 
                    TURNPLAYER = PlayerColor.BLUE;
                    TURNPLAYERDATA = BlueData;
                    OPPONENTPLAYER = PlayerColor.RED;
                    OPPONENTPLAYERDATA = RedData;
                }
                else 
                { 
                    TURNPLAYER = PlayerColor.RED;
                    TURNPLAYERDATA = RedData;
                    OPPONENTPLAYER = PlayerColor.BLUE;
                    OPPONENTPLAYERDATA = BlueData;
                }

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
                SoundServer.PlaySoundEffect(SoundEffect.Cancel);
                //Close the Action menu/Card info panel and return to the MainPhase Stage 
                _CurrentTileSelected.ReloadTileUI();
                PanelActionMenu.Visible = false;

                EnterMainPhase();
            }));
        }
        private void btnActionMove_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Set the initial tile reference to use during the move sequence
                _InitialTileMove = _CurrentTileSelected;
                _PreviousTileMove = _InitialTileMove;

                //Hide the Action menu
                PanelActionMenu.Visible = false;

                //Generate and display the Move Tile Candidates and display the Move Menu
                DisplayMoveCandidates();
                PlaceMoveMenu();

                //Set the initial Enable status for the Move Menu Buttons
                if (UserPlayerColor == TURNPLAYER)
                {
                    //Finish button will be disable as no move have been performed yet
                    btnMoveMenuFinish.Enabled = false;
                    btnMoveMenuCancel.Enabled = true;
                }
                else
                {
                    //Both buttons will be disable for the opponent player
                    btnMoveMenuFinish.Enabled = false;
                    btnMoveMenuCancel.Enabled = false;
                    //additionally update the intruction message for the opponent
                    lblActionInstruction.Text = "Opponent is selecting a tile to move into!";
                    lblActionInstruction.Visible = true;
                }

                //The mov available crest count will show yellow for both players
                _TMPMoveCrestCount = TURNPLAYERDATA.Crests_MOV;
                lblMoveMenuCrestCount.Text = string.Format("[MOV]x {0}", _TMPMoveCrestCount);
                lblMoveMenuCrestCount.ForeColor = Color.Yellow;

                //Now display the Move Menu and update the game state
                PanelMoveMenu.Visible = true;
                _CurrentGameState = GameState.MovingCard;
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
                _AttackCandidates = _AttackerTile.GetAttackTargerCandidates(TargetPlayerColor);
                DisplayAttackCandidates();
                PlaceAttackMenu();

                PanelActionMenu.Visible = false;

                if (UserPlayerColor != TURNPLAYER)
                {
                    lblActionInstruction.Text = "Opponent is selecting an attack target!";
                    lblActionInstruction.Visible = true;
                }

                PanelBoard.Enabled = true;
                _CurrentGameState = GameState.SelectingAttackTarger;
            }));

            void DisplayAttackCandidates()
            {
                foreach (Tile tile in _AttackCandidates)
                {
                    tile.MarkAttackTarget();
                }
            }
            void PlaceAttackMenu()
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

            void DisplayIgnitionEfectPanel(Card thisCard)
            {
                SoundServer.PlaySoundEffect(SoundEffect.EffectMenu);
                //Create the Effect Object,
                //This will initialize the _CardEffectToBeActivated to be use across the other methods for
                //this effect activation sequence.
                _CardEffectToBeActivated = null;
                if (thisCard.Category == Category.Monster && thisCard.HasIgnitionEffect)
                {
                    _CardEffectToBeActivated = thisCard.IgnitionEffect;
                }
                else if (thisCard.Category == Category.Spell)
                {
                    if (thisCard.HasContinuousEffect)
                    {
                        _CardEffectToBeActivated = thisCard.ContinuousEffect;
                    }
                    else if (thisCard.HasIgnitionEffect)
                    {
                        _CardEffectToBeActivated = thisCard.IgnitionEffect;
                    }
                }

                //Now load the actual Effect Menu Panel
                if (UserPlayerColor == TURNPLAYER)
                {
                    LoadItVisible();
                }
                else
                {
                    //Load the Effect Menu with hidden info if it was face down
                    if (thisCard.IsFaceDown)
                    {
                        LoadItHidden();
                    }
                    else
                    {
                        LoadItVisible();
                    }
                }

                //Now show the effect menu itself
                btnEffectMenuCancel.Visible = true;
                PanelEffectActivationMenu.Visible = true;

                void LoadItVisible()
                {
                    //Show the full menu for the turn player
                    //Card Image
                    ImageServer.LoadImage(PicEffectMenuCardImage, CardImageType.FullCardImage, thisCard.CardID.ToString());
                    //Effect Type Title
                    lblEffectMenuTittle.Text = string.Format("{0} Effect", _CardEffectToBeActivated.Type);
                    //Effect Text
                    lblEffectMenuDescriiption.Text = _CardEffectToBeActivated.EffectText;
                    //Cost
                    ImageServer.LoadImage(PicCostCrest, CardImageType.CrestIcon, _CardEffectToBeActivated.CrestCost.ToString());
                    lblCostAmount.Text = string.Format("x {0}", _CardEffectToBeActivated.CostAmount);
                    lblCostAmount.ForeColor = Color.White;
                    PanelCost.Visible = true;

                    //Activate button
                    if (thisCard.EffectUsedThisTurn)
                    {
                        lblActivationRequirementOutput.Text = "Effect already used this turn.";
                        lblActivationRequirementOutput.Visible = true;
                        btnActivate.Visible = false;
                        PanelCost.Visible = false;
                    }
                    else
                    {
                        //Check if the activation cost is met
                        if (IsCostMet(_CardEffectToBeActivated.CrestCost, _CardEffectToBeActivated.CostAmount))
                        {
                            //then check if the activation requirements is met
                            string ActivationRequirementStatus = GetActivationRequirementStatus(_CardEffectToBeActivated.ID);
                            if (ActivationRequirementStatus == "Requirements Met")
                            {
                                lblActivationRequirementOutput.Visible = false;
                                btnActivate.Visible = true;
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

                    //Set the buttons ENABLE for the turn player and DISABLE for the opponent
                    if (UserPlayerColor == TURNPLAYER)
                    {
                        btnActivate.Enabled = true;
                        btnEffectMenuCancel.Enabled = true;
                    }
                    else
                    {
                        btnActivate.Enabled = false;
                        btnEffectMenuCancel.Enabled = false;
                    }
                }
                void LoadItHidden()
                {
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
                bool IsCostMet(Crest crestCost, int amount)
                {
                    switch (crestCost)
                    {
                        case Crest.MAG: return amount <= TURNPLAYERDATA.Crests_MAG;
                        case Crest.TRAP: return amount <= TURNPLAYERDATA.Crests_TRAP;
                        case Crest.ATK: return amount <= TURNPLAYERDATA.Crests_ATK;
                        case Crest.DEF: return amount <= TURNPLAYERDATA.Crests_DEF;
                        case Crest.MOV: return amount <= TURNPLAYERDATA.Crests_MOV;
                        default: throw new Exception(string.Format("Crest undefined for Cost Met calculation. Crest: [{0}]", crestCost));
                    }
                }
                string GetActivationRequirementStatus(Effect.EffectID thisEffectID)
                {
                    switch (thisEffectID)
                    {
                        case Effect.EffectID.Polymerization: return Polymerization_MetsRequirement();
                        default: return "Requirements Met";
                    }

                    string Polymerization_MetsRequirement()
                    {
                        //If the player has card in the fusion deck still               
                        if (TURNPLAYERDATA.Deck.FusionDeckSize > 0)
                        {
                            //Clear the Fusion Cards ready for fusion
                            _FusionCardsReadyForFusion[0] = false;
                            _FusionCardsReadyForFusion[1] = false;
                            _FusionCardsReadyForFusion[2] = false;
                            bool AtLeastOneFusionRequirementsMet = false;
                            //Check each card in the Fusion Deck to see if the player has the materials on the board
                            for (int i = 0; i < TURNPLAYERDATA.Deck.FusionDeckSize; i++)
                            {
                                //Create the CardInfo Object as we dont have a Card Object created yet
                                int thisFusionCardID = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(i);
                                CardInfo thisFusionCard = CardDataBase.GetCardWithID(thisFusionCardID);

                                //Use the CardInfo to create the list of materials
                                List<string> fusionMaterials = thisFusionCard.GetFusionMaterials();

                                //Check if all the fusion materials exist on the board under the turn player's control
                                List<int> candidatesFound = new List<int>();
                                foreach (string thisMeterial in fusionMaterials)
                                {
                                    for (int x = 0; x < _CardsOnBoard.Count; x++)
                                    {
                                        Card thisCardOnTheBoard = _CardsOnBoard[x];
                                        if (thisCardOnTheBoard.Name == thisMeterial && !thisCardOnTheBoard.IsDiscardted && 
                                            thisCardOnTheBoard.Owner == TURNPLAYER &&!candidatesFound.Contains(x))
                                        {
                                            candidatesFound.Add(x);
                                            break;
                                        }
                                    }
                                }

                                bool AllFusionMaterialsFound = fusionMaterials.Count == candidatesFound.Count;
                                if (AllFusionMaterialsFound)
                                {
                                    _FusionCardsReadyForFusion[i] = true;
                                    AtLeastOneFusionRequirementsMet = true;
                                }
                            }

                            if (AtLeastOneFusionRequirementsMet) 
                            {
                                //Opponent will not run this requirement validation, so send the 
                                //_fusionCardsReadyForFusion candidates to the oppoenent
                                string candidate1 = _FusionCardsReadyForFusion[0].ToString();
                                string candidate2 = "False";
                                if(_FusionCardsReadyForFusion[1]) { candidate2 = _FusionCardsReadyForFusion[1].ToString(); }
                                string candidate3 = "False";
                                if (_FusionCardsReadyForFusion[2]) { candidate3 = _FusionCardsReadyForFusion[2].ToString(); }
                                SendMessageToServer(string.Format("{0}|{1}|{2}|{3}|{4}", "[READY FUSION CANDIDATES]", _CurrentGameState.ToString(), candidate1, candidate2, candidate3));
                                return "Requirements Met"; 
                            }
                            else { return "No fusion requirements met."; }
                        }
                        else { return "No cards in the Fusion Deck."; }
                    }
                }
            }
        }
        private void btnMoveMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Cancel);
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

                EnterMainPhase();
            }));
        }
        private void TileClick_MoveCard_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.MoveCard);

                //Move the card to this location
                Card thiscard = _PreviousTileMove.CardInPlace;
                Tile tileToMoveInto = _Tiles[tileId];

                _PreviousTileMove.RemoveCard();
                tileToMoveInto.MoveInCard(thiscard);               

                //Now clear the borders of all the candidates tiles to their og color
                for (int x = 0; x < _MoveCandidates.Count; x++)
                {
                    _MoveCandidates[x].ReloadTileUI();
                }

                //Now change the selection to this one tile
                _PreviousTileMove = tileToMoveInto;

                //Drecease the available crests to use
                _TMPMoveCrestCount -= thiscard.MoveCost;
                lblMoveMenuCrestCount.Text = string.Format("[MOV]x {0}", _TMPMoveCrestCount);

                //Enable both the Finish and Cancel buttons for the TURNPLAYER but keep them disable for the opponent
                if(UserPlayerColor == TURNPLAYER)
                {
                    btnMoveMenuFinish.Enabled = true;
                    btnMoveMenuCancel.Enabled = true;
                }
                else
                {
                    btnMoveMenuFinish.Enabled = false;
                    btnMoveMenuCancel.Enabled = false;
                }
                PlaceMoveMenu();

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
                int amountUsed = TURNPLAYERDATA.Crests_MOV - _TMPMoveCrestCount;
                AdjustPlayerCrestCount(TURNPLAYER, Crest.MOV, -amountUsed);

                //Return to the Main Phase to end the Move Sequence
                EnterMainPhase();
            }));
        }
        private void TileClick_AttackTarget_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Remove an Attack Available Counter from this card
                _AttackerTile.CardInPlace.RemoveAttackCounter();

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

            void OpenBattleMenu()
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
                    PlayerData AttackerData = TURNPLAYERDATA;
                    lblAttackerCrestCount.Text = string.Format("[ATK] to use: {0}/{1}", (Attacker.AttackCost + _AttackBonusCrest), AttackerData.Crests_ATK);
                    PanelAttackControls.Visible = true;

                    //If attacker monster has an advantage, enable the adv subpanel
                    bool AttackerHasAdvantage = HasAttributeAdvantage(Attacker, Defender);
                    if (AttackerHasAdvantage && Defender.Category == Category.Monster)
                    {
                        //Start the bonus crests at default 0
                        _AttackBonusCrest = 0;
                        lblAttackerBonusCrest.Text = "0";
                        //Show the + button at the start                    
                        lblAttackerAdvMinus.Visible = false;
                        lblAttackerAdvPlus.Visible = true;
                        PanelAttackerAdvBonus.Visible = true;
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
                        PlayerData DefenderData = OPPONENTPLAYERDATA;
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
                            _DefenseBonusCrest = 0;
                            lblDefenderBonusCrest.Text = "0";
                            //Show the + button at the start
                            lblDefenderAdvMinus.Visible = false;
                            lblDefenderAdvPlus.Visible = true;
                            PanelDefenderAdvBonus.Visible = true;
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
                        PlayerData DefenderData = OPPONENTPLAYERDATA;
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
        }
        private void btnAttackMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Cancel);
                //Reload the card's tile before the mouse trigers hovering another tile.
                _CurrentTileSelected.ReloadTileUI();

                //Unmark all the attack candidates
                foreach (Tile tile in _AttackCandidates)
                {
                    tile.ReloadTileUI();
                }

                _AttackCandidates.Clear();
                PanelAttackMenu.Visible = false;

                EnterMainPhase();
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
                        LoadCardInfoPanel(_CurrentTileSelected);
                    }
                }

                //Step 3: Reduce the cost from the player's crest pool
                AdjustPlayerCrestCount(TURNPLAYER, _CardEffectToBeActivated.CrestCost, -_CardEffectToBeActivated.CostAmount);

                //Step 4: Give a small pause to allow the opposite player to see the effect revealed on their end
                WaitNSeconds(2000);

                //Step 5: Close the Effect Menu and active the effect
                PanelEffectActivationMenu.Visible = false;
                WaitNSeconds(500);
                ActivateEffect(_CardEffectToBeActivated);

                //NOTE: The ActivateEffect() will decide the direction of the next game state
                //based on the Activation method of the specific effect that will be activated.
            }));
        }
        private void btnEffectMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Cancel);

                //Close the Effect Menu and return to the main phase
                PanelEffectActivationMenu.Visible = false;
                _CurrentTileSelected.ReloadTileUI();

                //Reset UI for each player
                EnterMainPhase();
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
                EnterMainPhase();
            }));
        }
        private void ReadyFusionCandidatesReceived(string strcandidate1, string strcandidate2, string strcandidate3)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _FusionCardsReadyForFusion[0] = false;
                _FusionCardsReadyForFusion[1] = false;
                _FusionCardsReadyForFusion[2] = false;

                bool FusionCardSlot1Ready = Convert.ToBoolean(strcandidate1);
                bool FusionCardSlot2Ready = Convert.ToBoolean(strcandidate2);
                bool FusionCardSlot3Ready = Convert.ToBoolean(strcandidate3);

                _FusionCardsReadyForFusion[0] = FusionCardSlot1Ready;
                _FusionCardsReadyForFusion[1] = FusionCardSlot2Ready;
                _FusionCardsReadyForFusion[2] = FusionCardSlot3Ready;

            }));           
        }
        private void btnFusionSummon_Base(string selectionIndex)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                //Initialize the fusion summon of card in index 0
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _IndexOfFusionCardSelected = Convert.ToInt32(selectionIndex);
                int fusionID = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(_IndexOfFusionCardSelected);
                _FusionToBeSummoned = CardDataBase.GetCardWithID(fusionID);
                _FusionMaterialsToBeUsed.Clear();
                _FusionSummonTiles.Clear();
                _FusionMaterialsToBeUsed = _FusionToBeSummoned.GetFusionMaterials();
                PanelFusionMonsterSelector.Visible = false;

                //Now the UI will prompt the turn player to select the first fusion material
                PromptPlayerToSelectFusionMaterial();
            }));
        }
        private void TileClick_FusionMaterial_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);

                //Destroy the selected card and remove the fusion material from the _FusionMaterialsToBeUsed
                Tile thisTile = _Tiles[tileId];
                thisTile.DestroyCard();
                _FusionMaterialsToBeUsed.RemoveAt(0);

                //Save the reference to the tile of this Fusion Material so we know which tiles
                //can be selected to summon the fusion monster at
                _FusionSummonTiles.Add(thisTile);

                //reset the tiles of the summon candidates
                foreach (Tile tile in _FusionCandidateTiles)
                {
                    tile.ReloadTileUI();
                }

                //Now, if the _FusionMaterialsToBeUsed still contiains fusions materials to be selected,
                //repeat the selection process until all of them have been selected
                if (_FusionMaterialsToBeUsed.Count > 0)
                {
                    PromptPlayerToSelectFusionMaterial();
                }
                else
                {
                    //Mark the candidate tiles to summon the fusion monster at
                    DisplayFusionSummonTileCandidates();

                    //Change the game state so the turn player can select the tile to summon at
                    _CurrentGameState = GameState.FusionSummonTileSelection;
                }

            }));

            void DisplayFusionSummonTileCandidates()
            {
                foreach (Tile thisTile in _FusionSummonTiles)
                {
                    thisTile.MarkFreeToMove();
                }

                if (UserPlayerColor == TURNPLAYER)
                {
                    lblActionInstruction.Text = "Select Tile to Fusion Summon!";
                    lblActionInstruction.Visible = true;
                }
                else
                {
                    lblActionInstruction.Text = "Opponent is selecting the Tile to Fusion Summon.";
                    lblActionInstruction.Visible = true;
                }
            }
        }
        private void TileClick_FusionSummon_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _CurrentGameState = GameState.NOINPUT;
                SoundServer.PlaySoundEffect(SoundEffect.SummonMonster);

                //Reset the tileUI of all the Tile Candidates to summon trhe card
                foreach(Tile thisTile in _FusionSummonTiles)
                {
                    thisTile.ReloadTileUI();
                }

                //Remove the fusion to be summoned from the fusion deck
                TURNPLAYERDATA.Deck.RemoveFusionAtIndex(_IndexOfFusionCardSelected);

                //Now run the Master Summon Monster function
                SummonMonster(_FusionToBeSummoned, tileId);
            }));
        }
        #endregion

        #region Data
        private PlayerColor TURNPLAYER = PlayerColor.RED;
        private PlayerColor OPPONENTPLAYER = PlayerColor.BLUE;
        private PlayerColor UserPlayerColor;
        private PlayerData TURNPLAYERDATA;
        private PlayerData OPPONENTPLAYERDATA;
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
        //Fusion Sequence Data
        private bool[] _FusionCardsReadyForFusion = new bool[3];
        private CardInfo _FusionToBeSummoned;
        private List<string> _FusionMaterialsToBeUsed = new List<string>();
        private List<Tile> _FusionCandidateTiles = new List<Tile>();
        private List<Tile> _FusionSummonTiles = new List<Tile>();
        private int _IndexOfFusionCardSelected = -1;
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
                case Effect.EffectID.MWarrior1_Ignition: MWarrior1_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.MWarrior2_OnSummon: MWarrior2_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.MWarrior2_Ignition: MWarrior2_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.Polymerization: Polymerization_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.KarbonalaWarrior_Continuous: KarbonalaWarrior_ContinuousActivation(thisEffect); break;
                case Effect.EffectID.KarbonalaWarrior_Ignition: KarbonalaWarrior_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.HitotsumeGiant_OnSummon: HitotsumeGiant_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.ThunderDragon_Continuous: ThunderDragon_Continuous(thisEffect); break;
                default: throw new Exception(string.Format("Effect ID: [{0}] does not have an Activate Effect Function"));
            }
        }
        private void RemoveEffect(Effect thisEffect)
        {
            //then remove the effect from the Board
            switch (thisEffect.ID)
            {
                case Effect.EffectID.ThunderDragon_Continuous: ThunderDragon_RemoveEffect(thisEffect); break;
                default: throw new Exception(string.Format("This effect id: [{0}] does not have a Remove Effect method assigned", thisEffect.ID));
            }
            //Remove the effect from the Active effect list
            _ActiveEffects.Remove(thisEffect);
        }
        private void DisplayOnSummonEffectPanel(Effect thisEffect)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectMenu);
            //Load the menu with the On Summon effect
            ImageServer.LoadImage(PicEffectMenuCardImage, CardImageType.FullCardImage, thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Summon Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.OnSummonEffectText;
            //Hide the elements not needed.
            lblActivationRequirementOutput.Text = "";
            PanelCost.Visible = false;
            btnActivate.Visible = false;
            btnEffectMenuCancel.Visible = false;
            //Wait 2 sec for players to reach the effect
            PanelEffectActivationMenu.Visible = true;
            WaitNSeconds(2000);
        }
        private void DisplayOnSummonContinuousEffectPanel(Effect thisEffect)
        {
            ImageServer.LoadImage(PicEffectMenuCardImage, CardImageType.FullCardImage, thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Continuous Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.ContinuousEffectText;
            //Hide the elements not needed.
            lblActivationRequirementOutput.Text = "";
            PanelCost.Visible = false;
            btnActivate.Visible = false;
            btnEffectMenuCancel.Visible = false;
            //Wait 2 sec for players to reach the effect
            PanelEffectActivationMenu.Visible = true;
            WaitNSeconds(2000);
        }       
        private void HideEffectMenuPanel()
        {
            //Now you can close the On Summon Panel
            PanelEffectActivationMenu.Visible = false;
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

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
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

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
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

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
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

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
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

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
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

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
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
                WaitNSeconds(300);
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

            //Set the "Reaction To" flags
            //This effect is a One-Time activation, does not reach to any event.

            //Hide the panel now to resolve the effect
            HideEffectMenuPanel();

            //EFFECT DESCRIPTION
            //Will increase the ATK of all your monsters on the board with the name "M-Warrior #2" by 500.
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsASymbol && !thisCard.IsDiscardted && thisCard.Name == "M-Warrior #2" && thisCard.Owner == thisEffect.Owner)
                {
                    thisCard.AdjustAttackBonus(500);
                    thisEffect.AddAffectedByCard(thisCard);
                    //Reload The Tile UI for the card affected
                    SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
                    thisCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                }
            }
          
            //This monster does not have a continuous effect to active, move into the Main Phase
            EnterMainPhase();
        }
        private void MWarrior1_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION:
            //Add 1 [DEF] to the owener's crest pool
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
            AdjustPlayerCrestCount(thisEffect.Owner, Crest.DEF, 1);

            //Flag the Effect Activation this turn
            thisEffect.OriginCard.MarkEffectUsedThisTurn();

            //NO more action needed, return to the Main Phase
            EnterMainPhase();
        }
        #endregion

        #region "M-Warrior #2"
        private void MWarrior2_OnSummonActivation(Effect thisEffect)
        {
            //Since this is a ON SUMMON EFFECT, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonEffectPanel(thisEffect);

            //Set the "Reaction To" flags
            //This effect is a One-Time activation, does not reach to any event.

            //Hide the panel now to resolve the effect
            HideEffectMenuPanel();

            //EFFECT DESCRIPTION
            //Will increase the DEF of all your monsters on the board with the name "M-Warrior #1" by 500.
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsASymbol && !thisCard.IsDiscardted && thisCard.Name == "M-Warrior #1" && thisCard.Owner == thisEffect.Owner)
                {
                    thisCard.AdjustDefenseBonus(500);
                    thisEffect.AddAffectedByCard(thisCard);
                    //Reload The Tile UI for the card affected
                    SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
                    thisCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                }
            }

            //This monster does not have a continuous effect to active, move into the Main Phase
            EnterMainPhase();
        }
        private void MWarrior2_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION:
            //Add 1 [ATK] to the owener's crest pool
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
            AdjustPlayerCrestCount(thisEffect.Owner, Crest.ATK, 1);

            //Flag the Effect Activation this turn
            thisEffect.OriginCard.MarkEffectUsedThisTurn();

            //NO more action needed, return to the Main Phase
            EnterMainPhase();
        }
        #endregion

        #region Polymerization
        private void Polymerization_IgnitionActivation(Effect thisEffect)
        {
            //Destroy the Polymerization card
            _CurrentTileSelected.DestroyCard();
            
            //Now display the Fusion Selector Menu
            DisplayFusionSelectorMenu();
            
            //Change the game state and the player is now ready to pick the fusion monster
            _CurrentGameState = GameState.FusionSelectorMenu;

            void DisplayFusionSelectorMenu()
            {
                //Use the previously set list _FusionCardsReadyForFusion to display the available options to summon
                //Check the 3 fusion card slots in the deck

                //Check slot 1
                if (_FusionCardsReadyForFusion[0])
                {
                    int fusionCard1Id = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(0);
                    if(UserPlayerColor == TURNPLAYER)
                    {
                        ImageServer.LoadImage(PicFusionOption1, CardImageType.FullCardImage, fusionCard1Id.ToString());
                        btnFusionSummon1.Enabled = true;
                    }
                    else
                    {
                        ImageServer.LoadImage(PicFusionOption1, CardImageType.FullCardImage, "0");
                        btnFusionSummon1.Enabled = false;
                    }    
                    PicFusionOption1.Visible = true;
                    btnFusionSummon1.Visible = true;
                }
                else
                {
                    PicFusionOption1.Visible = false;
                    btnFusionSummon1.Visible = false;
                }

                //Check slot 2
                if (_FusionCardsReadyForFusion[1])
                {
                    int fusionCard2Id = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(1);
                    if (UserPlayerColor == TURNPLAYER)
                    {
                        ImageServer.LoadImage(PicFusionOption2, CardImageType.FullCardImage, fusionCard2Id.ToString());
                        btnFusionSummon2.Enabled = true;
                    }
                    else
                    {
                        ImageServer.LoadImage(PicFusionOption2, CardImageType.FullCardImage, "0");
                        btnFusionSummon2.Enabled = false;
                    }
                    PicFusionOption2.Visible = true;
                    btnFusionSummon2.Visible = true;
                }
                else
                {
                    PicFusionOption2.Visible = false;
                    btnFusionSummon2.Visible = false;
                }

                //Check slot 3
                if (_FusionCardsReadyForFusion[2])
                {
                    int fusionCard3Id = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(2);
                    if (UserPlayerColor == TURNPLAYER)
                    {
                        ImageServer.LoadImage(PicFusionOption3, CardImageType.FullCardImage, fusionCard3Id.ToString());
                        btnFusionSummon3.Enabled = true;
                    }
                    else
                    {
                        ImageServer.LoadImage(PicFusionOption3, CardImageType.FullCardImage, "0");
                        btnFusionSummon3.Enabled = false;
                    }
                    PicFusionOption3.Visible = true;
                    btnFusionSummon3.Visible = true;
                }
                else
                {
                    PicFusionOption3.Visible = false;
                    btnFusionSummon3.Visible = false;
                }

                //Update the message for the opponent
                if(UserPlayerColor != TURNPLAYER)
                {
                    lblActionInstruction.Text = "Opponent is selecting a Fusion Monster to summon.";
                }

                PanelFusionMonsterSelector.Visible = true;
            }
        }
        #endregion

        #region Karbonala Warrior
        private void KarbonalaWarrior_ContinuousActivation(Effect thisEffect)
        {
            //Step 1: Since this is a CONTINUOUS, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonContinuousEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToMonsterDestroyed = true;

            //Step 3: Resolve the effect
            //EFFECT DESCRIPTION
            //Increase the ATK/DEF of this monster by 500 for each “M-Warrior #1” or “M-Warrior #2” on the board
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
            foreach (Card thisCard in _CardsOnBoard)
            {
                if(!thisCard.IsDiscardted && (thisCard.Name == "M-Warrior #1" || thisCard.Name == "M-Warrior #2"))
                {
                    thisEffect.OriginCard.AdjustAttackBonus(500);
                    thisEffect.OriginCard.AdjustDefenseBonus(500);
                    thisEffect.OriginCard.ReloadTileUI();
                    UpdateEffectLogs(string.Format("Effect Applied to Origin Card: [{0}] | BY: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Owner));
                }
            }

            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);

            //Step 5: Hide the Effect Menu panel and enter the Main Phase
            HideEffectMenuPanel();
            EnterMainPhase();
        }
        private void KarbonalaWarrior_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is M-Warrior #1 or M-Warrior #2 regardless of owner
            if (targetCard.Name == "M-Warrior #1" || targetCard.Name == "M-Warrior #2")
            {
                UpdateEffectLogs(string.Format("Effect Reacted to sumoon: [{0}] | Summoned Card: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, targetCard.Name, targetCard.OnBoardID, targetCard.Owner));
                //Give an extra boost to the origin monster
                thisEffect.OriginCard.AdjustAttackBonus(500);
                thisEffect.OriginCard.AdjustDefenseBonus(500);
                thisEffect.OriginCard.ReloadTileUI();
                UpdateEffectLogs(string.Format("Origin Card [{0}] with Board ID: [{1}] ATK/DEF boost increased by 500.", thisEffect.OriginCard.Name, thisEffect.OriginCard.OnBoardID));
            }
        }
        private void KarbonalaWarrior_ReactTo_MonsterDestroyed(Effect thisEffect, Card targetCard)
        {
            //If the monster destroyed was M-Warrior #1 or M-Warrior #2 regardless of owner
            if (targetCard.Name == "M-Warrior #1" || targetCard.Name == "M-Warrior #2")
            {
                UpdateEffectLogs(string.Format("Effect Reacted to monster destroyed: [{0}] | Destroyed Card: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, targetCard.Name, targetCard.OnBoardID, targetCard.Owner));
                //reduce boost to the origin monster
                thisEffect.OriginCard.AdjustAttackBonus(-500);
                thisEffect.OriginCard.AdjustDefenseBonus(-500);
                thisEffect.OriginCard.ReloadTileUI();
                UpdateEffectLogs(string.Format("Origin Card [{0}] with Board ID: [{1}] ATK/DEF boost decreased by 500.", thisEffect.OriginCard.Name, thisEffect.OriginCard.OnBoardID));
            }
        }
        private void KarbonalaWarrior_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION:
            //Add 2 [ATK] and 2 [DEF] to the owener's crest pool
            UpdateEffectLogs(string.Format("Effect Activation: [{0}] - Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
            AdjustPlayerCrestCount(thisEffect.Owner, Crest.ATK, 2);
            AdjustPlayerCrestCount(thisEffect.Owner, Crest.DEF, 2);

            //Flag the Effect Activation this turn
            thisEffect.OriginCard.MarkEffectUsedThisTurn();

            //NO more action needed, return to the Main Phase
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

            HideEffectMenuPanel();

            //At this point end the summoning phase
            EnterMainPhase();
        }
        #endregion

        #region "Thunder Dragon"
        private void ThunderDragon_Continuous(Effect thisEffect)
        {
            //Since this is a CONTINUOUS, display the Effect Panel for 2 secs then execute the effect
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

            HideEffectMenuPanel();

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
        private void ThunderDragon_RemoveEffect(Effect thisEffect)
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
            NOINPUT,
            TurnStartMenu,
            BoardViewMode,
            MainPhaseBoard,
            ActionMenuDisplay,
            MovingCard,
            SelectingAttackTarger,
            BattlePhase,
            SetCard,
            SummonCard,
            EffectMenuDisplay,
            FusionSelectorMenu,
            FusionMaterialCandidateSelection,
            FusionSummonTileSelection
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