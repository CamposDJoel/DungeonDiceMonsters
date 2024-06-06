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
                    Panel insidePicture = new Panel();
                    PanelBoard.Controls.Add(insidePicture);
                    insidePicture.Location = new Point(X_Location + 3, Y_Location + 3);
                    insidePicture.Size = new Size(CARDIMAGE_SIZE, CARDIMAGE_SIZE);
                    insidePicture.BorderStyle = BorderStyle.None;
                    insidePicture.BackgroundImageLayout = ImageLayout.Stretch;
                    insidePicture.BackColor = Color.Transparent;
                    insidePicture.Tag = tileId;
                    insidePicture.MouseEnter += OnMouseEnterPicture_OnInsidePicture;
                    insidePicture.MouseLeave += OnMouseLeavePicture_OnInsidePicture;
                    insidePicture.Click += Tile_Click_OnInsidePicture;

                    //Create the overlay icon picture box
                    PictureBox overlayIcon = new PictureBox();
                    insidePicture.Controls.Add(overlayIcon);
                    overlayIcon.Location = new Point(0, 0);
                    overlayIcon.Size = new Size(CARDIMAGE_SIZE, CARDIMAGE_SIZE);
                    overlayIcon.BorderStyle = BorderStyle.None;
                    overlayIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                    overlayIcon.BackColor = Color.Transparent;
                    overlayIcon.Tag = tileId;
                    overlayIcon.Click += Tile_Click_OnOverlay;
                    overlayIcon.MouseEnter += OnMouseEnterPicture_OnOverlay;
                    overlayIcon.MouseLeave += OnMouseLeavePicture_OnOverlay;
                    overlayIcon.Visible = false;

                    //Create each border picture box 
                    //(create this one after so it is created "behind" the inside picture
                    PictureBox borderPicture = new PictureBox();
                    PanelBoard.Controls.Add(borderPicture);
                    borderPicture.Location = new Point(X_Location, Y_Location);
                    borderPicture.Size = new Size(TILESIDE_SIZE, TILESIDE_SIZE);
                    borderPicture.BorderStyle = BorderStyle.FixedSingle;
                    borderPicture.BackColor = Color.Transparent;

                    //create and add a new tile object using the above 2 picture boxes
                    _Tiles.Add(new Tile(insidePicture, borderPicture, overlayIcon, statsLabelATK, statsLabelDEF));

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
                if (tile.ID >= 13)
                {
                    Tile linkedTile = _Tiles[tile.ID - 13];
                    tile.SetAdjecentTileLink(Tile.TileDirection.North, linkedTile);
                }

                //assign south links
                if (tile.ID <= 220)
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
            ActivateEffect(_RedSymbol.GetContinuousEffect());
            ActivateEffect(_BlueSymbol.GetContinuousEffect());
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
                    Panel insidePicture = new Panel();
                    PanelBoard.Controls.Add(insidePicture);
                    insidePicture.Location = new Point(X_Location + 3, Y_Location + 3);
                    insidePicture.Size = new Size(CARDIMAGE_SIZE, CARDIMAGE_SIZE);
                    insidePicture.BorderStyle = BorderStyle.None;
                    insidePicture.BackgroundImageLayout = ImageLayout.Stretch;
                    insidePicture.BackColor = Color.Transparent;
                    insidePicture.Tag = tileId;
                    insidePicture.MouseEnter += OnMouseEnterPicture_OnInsidePicture;
                    insidePicture.MouseLeave += OnMouseLeavePicture_OnInsidePicture;
                    insidePicture.Click += Tile_Click_OnInsidePicture;

                    //Create the overlay icon picture box
                    PictureBox overlayIcon = new PictureBox();
                    insidePicture.Controls.Add(overlayIcon);
                    overlayIcon.Location = new Point(0, 0);
                    overlayIcon.Size = new Size(CARDIMAGE_SIZE, CARDIMAGE_SIZE);
                    overlayIcon.BorderStyle = BorderStyle.None;
                    overlayIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                    overlayIcon.BackColor = Color.Transparent;
                    overlayIcon.Tag = tileId;
                    overlayIcon.Click += Tile_Click_OnOverlay;
                    overlayIcon.MouseEnter += OnMouseEnterPicture_OnOverlay;
                    overlayIcon.MouseLeave += OnMouseLeavePicture_OnOverlay;
                    overlayIcon.Visible = false;

                    //Create each border picture box 
                    //(create this one after so it is created "behind" the inside picture
                    PictureBox borderPicture = new PictureBox();
                    PanelBoard.Controls.Add(borderPicture);
                    borderPicture.Location = new Point(X_Location, Y_Location);
                    borderPicture.Size = new Size(TILESIDE_SIZE, TILESIDE_SIZE);
                    borderPicture.BorderStyle = BorderStyle.FixedSingle;
                    borderPicture.BackColor = Color.Transparent;

                    //create and add a new tile object using the above 2 picture boxes
                    _Tiles.Add(new Tile(insidePicture, borderPicture, overlayIcon, statsLabelATK, statsLabelDEF));

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
                if (tile.ID >= 13)
                {
                    Tile linkedTile = _Tiles[tile.ID - 13];
                    tile.SetAdjecentTileLink(Tile.TileDirection.North, linkedTile);
                }

                //assign south links
                if (tile.ID <= 220)
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
            ActivateEffect(_RedSymbol.GetContinuousEffect());
            ActivateEffect(_BlueSymbol.GetContinuousEffect());

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
                    Card thisCard = new Card(_CardsOnBoard.Count, thisRedMonsterInfo, PlayerColor.RED, false);
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

                    Card thisCard = new Card(_CardsOnBoard.Count, thisBlueMonsterInfo, PlayerColor.BLUE, false);
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
        public List<Tile> GetUnoccupiedSpellTrapZoneTiles(PlayerColor playerColor)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Tile thisTile in _Tiles)
            {
                if (thisTile.IsSpellTrapZone && thisTile.Owner == playerColor && !thisTile.IsOccupied)
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
            _SetCandidates = GetUnoccupiedSpellTrapZoneTiles(TURNPLAYER);
            DisplaySetCandidates();


            if (UserPlayerColor == TURNPLAYER)
            {
                //Enable the Board Panel to interact with it
                PanelBoard.Enabled = true;

                lblActionInstruction.Text = "Select the Spell/Trap Zone tile to set the card at.";
            }
            else
            {
                lblActionInstruction.Text = "Opponent is selecting the Spell/Trap Zone tile to set the card at.";
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

            ImageServer.ClearImage(PicBlueSymbol);
            ImageServer.ClearImage(PicRedSymbol);
            PicBlueSymbol.Image = ImageServer.Symbol(_BlueSymbol.CurrentAttribute.ToString());
            PicRedSymbol.Image = ImageServer.Symbol(_RedSymbol.CurrentAttribute.ToString());

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
                if (thisCard.Controller == PlayerColor.RED)
                {
                    PanelCardInfo.BackColor = Color.DarkRed;
                }
                else
                {
                    PanelCardInfo.BackColor = Color.DarkBlue;
                }

                if (thisCard.IsFaceDown && thisCard.Controller != UserPlayerColor)
                {
                    ImageServer.ClearImage(PicCardArtworkBottom);
                    PicCardArtworkBottom.Image = ImageServer.CardArtworkImage("0");

                    lblCardName.Text = string.Empty;
                    lblCardType.Text = string.Empty;
                    lblCardLevel.Text = string.Empty;
                    PicCardAttribute.Visible = false;
                    PicCardMonsterType.Visible = false;
                    lblStatsATK.Text = string.Empty;
                    lblStatsDEF.Text = string.Empty;
                    lblStatsLP.Text = string.Empty;
                    lblCardText.Text = "Opponent's Facedown card.";
                    lblAttackLeftAmount.Text = string.Empty;
                    lblMovesLeftAmount.Text = string.Empty;
                    lblMovesCostAmount.Text = string.Empty;
                    lblAttackCostAmount.Text = string.Empty;
                    lblDefenseCostAmount.Text = string.Empty;
                    lblAttackRangeAmount.Text = string.Empty;
                    lblMoveRangeAmount.Text = string.Empty;
                    lblTurnCounters.Text = string.Empty;
                    lblCounters.Text = string.Empty;
                    lblSpellboundCounters.Text = string.Empty;
                }
                else
                {
                    if (thisCard.IsASymbol)
                    {
                        ImageServer.ClearImage(PicCardArtworkBottom);
                        PicCardArtworkBottom.Image = ImageServer.Symbol(thisCard.CurrentAttribute.ToString());

                        lblCardName.Text = thisCard.Controller + "'s " + thisCard.Name;
                        lblCardType.Text = string.Empty;
                        lblCardLevel.Text = string.Empty;
                        ImageServer.ClearImage(PicCardAttribute);
                        PicCardAttribute.Image = ImageServer.AttributeIcon(thisCard.CurrentAttribute);
                        PicCardAttribute.Visible = true;
                        PicCardMonsterType.Visible = false;
                        lblStatsATK.Text = string.Empty;
                        lblStatsDEF.Text = string.Empty;
                        lblStatsLP.Text = thisCard.LP.ToString();
                        lblCardText.Text = thisCard.ContinuousEffectText;
                        lblAttackLeftAmount.Text = string.Empty;
                        lblMovesLeftAmount.Text = string.Format("{0} / {1}", thisCard.MovesAvaiable, thisCard.MovesPerTurn);
                        lblMovesCostAmount.Text = thisCard.MoveCost.ToString();
                        lblAttackCostAmount.Text = string.Empty;
                        lblDefenseCostAmount.Text = string.Empty;
                        lblAttackRangeAmount.Text = string.Empty;
                        lblMoveRangeAmount.Text = "1";
                        lblTurnCounters.Text = string.Empty;
                        lblCounters.Text = string.Empty;
                        lblSpellboundCounters.Text = string.Empty;
                    }
                    else
                    {
                        //Populate the UI
                        ImageServer.ClearImage(PicCardArtworkBottom);
                        PicCardArtworkBottom.Image = ImageServer.CardArtworkImage(cardID.ToString());
                        lblCardName.Text = thisCard.Name;

                        string secondaryType = thisCard.SecType.ToString();
                        lblCardType.Text = thisCard.TypeAsString + "/" + secondaryType;
                        if (thisCard.Category == Category.Spell) { lblCardType.Text = thisCard.TypeAsString + " spell"; }
                        if (thisCard.Category == Category.Trap) { lblCardType.Text = thisCard.TypeAsString + " trap"; }

                        if (thisCard.Category == Category.Monster) { lblCardLevel.Text = "Lv. " + thisCard.Level; }
                        else { lblCardLevel.Text = ""; }

                        ImageServer.ClearImage(PicCardAttribute);
                        PicCardAttribute.Image = ImageServer.AttributeIcon(thisCard.CurrentAttribute);
                        PicCardAttribute.Visible = true;

                        ImageServer.ClearImage(PicCardMonsterType);
                        PicCardMonsterType.Image = ImageServer.MonsterTypeIcon(thisCard.TypeAsString);
                        PicCardMonsterType.Visible = true;

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
                            string fusionMaterials = thisCard.FusionMaterial1 + " + " + thisCard.FusionMaterial2;
                            if (thisCard.FusionMaterial3 != "-") { fusionMaterials = fusionMaterials + " + " + thisCard.FusionMaterial3; }
                            fullcardtext = fullcardtext + fusionMaterials + "\n\n";
                        }

                        if (thisCard.HasOnSummonEffect)
                        {
                            fullcardtext = fullcardtext + "(ON SUMMON) - " + thisCard.OnSummonEffectText + "\n\n";
                        }

                        if (thisCard.HasAbility)
                        {
                            fullcardtext = fullcardtext + "(ABILITY) - " + thisCard.Ability + "\n\n";
                        }

                        if (thisCard.HasContinuousEffect)
                        {
                            fullcardtext = fullcardtext + "(CONTINUOUS) - " + thisCard.ContinuousEffectText + "\n\n";
                        }
                        
                        if (thisCard.HasIgnitionEffect)
                        {
                            fullcardtext = fullcardtext + "(EFFECT)" + thisCard.IgnitionEffectText + "\n\n";
                        }

                        if (thisCard.HasTriggerEffect)
                        {
                            fullcardtext = fullcardtext + "(TRIGGER)" + thisCard.TriggerEffect + "\n\n";
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

                        lblMovesCostAmount.Text = thisCard.MoveCost.ToString();

                        if (thisCard.Category == Category.Monster) { lblAttackCostAmount.Text = thisCard.AttackCost.ToString(); }
                        else { lblAttackCostAmount.Text = string.Empty; }

                        if (thisCard.Category == Category.Monster) { lblDefenseCostAmount.Text = thisCard.DefenseCost.ToString(); }
                        else { lblDefenseCostAmount.Text = string.Empty; }

                        if (thisCard.Category == Category.Monster) { lblAttackRangeAmount.Text = thisCard.AttackRange.ToString(); }
                        else { lblAttackRangeAmount.Text = string.Empty; }

                        lblMoveRangeAmount.Text = thisCard.MoveRange.ToString();

                        lblTurnCounters.Text = thisCard.TurnCounters.ToString();
                        lblCounters.Text = thisCard.Counters.ToString();
                        lblSpellboundCounters.Text = thisCard.SpellboundCounter.ToString();
                    }
                }
            }
            else
            {
                PanelCardInfo.BackColor = Color.Gray;
                ImageServer.ClearImage(PicCardArtworkBottom);
                PicCardArtworkBottom.Image = ImageServer.CardArtworkImage("0");

                lblCardName.Text = string.Empty;
                lblCardType.Text = string.Empty;
                lblCardLevel.Text = string.Empty;
                PicCardAttribute.Visible = false;
                PicCardMonsterType.Visible = false;
                lblStatsATK.Text = string.Empty;
                lblStatsDEF.Text = string.Empty;
                lblStatsLP.Text = string.Empty;
                lblCardText.Text = string.Empty;
                lblAttackLeftAmount.Text = string.Empty;
                lblMovesLeftAmount.Text = string.Empty;
                lblMovesCostAmount.Text = string.Empty;
                lblAttackCostAmount.Text = string.Empty;
                lblDefenseCostAmount.Text = string.Empty;
                lblAttackRangeAmount.Text = string.Empty;
                lblMoveRangeAmount.Text = string.Empty;
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
                    ImageServer.ClearImage(PicFieldTypeDisplay);
                    PicFieldTypeDisplay.Image = ImageServer.FieldTile(thisTile.FieldType.ToString());
                }
                else
                {
                    ImageServer.ClearImage(PicFieldTypeDisplay);
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
            _MoveCandidates = _PreviousTileMove.GetMoveRangeTiles();

            //Mark the candidate tiles
            foreach(Tile thisTile in _MoveCandidates)
            {
                thisTile.MarkMoveTarget();
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
            switch (attacker.CurrentAttribute)
            {
                case Attribute.LIGHT: if (defender.CurrentAttribute == Attribute.DARK) { return true; } else { return false; }
                case Attribute.DARK: if (defender.CurrentAttribute == Attribute.LIGHT) { return true; } else { return false; }
                case Attribute.WATER: if (defender.CurrentAttribute == Attribute.FIRE) { return true; } else { return false; }
                case Attribute.FIRE: if (defender.CurrentAttribute == Attribute.EARTH) { return true; } else { return false; }
                case Attribute.EARTH: if (defender.CurrentAttribute == Attribute.WIND) { return true; } else { return false; }
                case Attribute.WIND: if (defender.CurrentAttribute == Attribute.WATER) { return true; } else { return false; }
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
                    lblDebugCardOwner.Text = "Card Owner: " + _CurrentTileSelected.CardInPlace.Controller;
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
            ImageServer.ClearImage(PicCurrentForm);
            PicCurrentForm.Image = ImageServer.DimensionForm(_CurrentDimensionForm.ToString());
            lblFormName.Text = _CurrentDimensionForm.ToString();
        }
        private void UpdateBanner(string currentPhase)
        {
            ImageServer.ClearImage(PicPhaseBanner);
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
            //Add the split mark
            message += "$";
            //and send it
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            ns.Write(buffer, 0, buffer.Length);
        }
        public void ReceiveMesageFromServer(string DATARECEIVED)
        {
            //Step 1: Extract the Message Key and GameState
            string[] MessageTokens = DATARECEIVED.Split('|');

            string MessageKey = MessageTokens[0];

            bool validMessage = false;

            if (MessageKey.StartsWith("[") && MessageKey.EndsWith("]")) 
            { 
                if(MessageKey == "[ON MOUSE ENTER TILE]" || MessageKey == "[ON MOUSE LEAVE TILE]")
                {
                    if (MessageTokens.Length > 1 && MessageTokens[1] != "") { validMessage = true; }
                }
                else
                {
                    validMessage = true;
                }
            }

            if(validMessage)
            {
                switch (MessageKey)
                {
                    case "[View Board Action]": btnViewBoard_Base(); break;
                    case "[ON MOUSE ENTER TILE]": OnMouseEnterPicture_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[ON MOUSE LEAVE TILE]": OnMouseLeavePicture_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[EXIT VIEW BOARD MODE]": btnReturnToTurnMenu_Base(); break;
                    case "[Roll Dice Action]": btnRoll_Base(); break;
                    //Messages from the RollDiceMenu From will have a share Message Key so they can be forward it to the form
                    //These messages will have a secondary key that will be used inside that form for processing.
                    case "[ROLL DICE FORM REQUEST]": _RollDiceForm.ReceiveMesageFromServer(DATARECEIVED); break;
                    case "[CLICK TILE TO SUMMON]": TileClick_SummonCard_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[CLICK TILE TO SET]": TileClick_SetCard_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[END TURN]": btnEndTurn_Base(); break;
                    case "[CHANGE DIMENSION SELECTION]": UpdateDimension_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[CLICK TILE TO ACTION]": TileClick_MainPhase_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[CLICK CANCEL ACTION MENU]": btnActionCancel_Base(); break;
                    case "[CLICK MOVE ACTION MENU]": btnActionMove_Base(); break;
                    case "[CLICK CANCEL MOVE MENU]": btnMoveMenuCancel_Base(); break;
                    case "[CLICK TILE TO MOVE]": TileClick_MoveCard_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[CLICK FINISH MOVE MENU]": btnMoveMenuFinish_Base(); break;
                    case "[CLICK ATTACK ACTION MENU]": btnActionAttack_Base(); break;
                    case "[CLICK TILE TO ATTACK]": TileClick_AttackTarget_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[CLICK CANCEL ATTACK MENU]": btnAttackMenuCancel_Base(); break;
                    case "[CLICK EFFECT ACTION MENU]": btnActionEffect_Base(); break;
                    case "[CLICK ACTIVATE EFFECT MENU]": btnEffectMenuActivate_Base(); break;
                    case "[CLICK CANCEL EFFECT MENU]": btnEffectMenuCancel_Base(); break;
                    case "[ATTACK!]": BattleMessageReceived_Attack(Convert.ToInt32(MessageTokens[1])); break;
                    case "[DEFEND!]": BattleMessageReceived_Defend(Convert.ToInt32(MessageTokens[1])); break;
                    case "[PASS!]": BattleMessageReceived_Pass(); break;
                    case "[END BATTLE]": btnEndBattle_Base(); break;
                    case "[READY FUSION CANDIDATES]": ReadyFusionCandidatesReceived(MessageTokens[1], MessageTokens[2], MessageTokens[3]); break;
                    case "[FUSION SELECTION MENU SELECT]": btnFusionSummon_Base(MessageTokens[2]); break;
                    case "[CLICK TILE TO FUSION MATERIAL]": TileClick_FusionMaterial_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[CLICK TILE TO FUSION SUMMON]": TileClick_FusionSummon_Base(Convert.ToInt32(MessageTokens[1])); break;
                    case "[CLICK TILE TO EFFECT TARGET]": TileClick_EffectTarget_Base(Convert.ToInt32(MessageTokens[1])); break;
                }
            }           
        }
        #endregion

        #region Turn Steps Functions
        private void SummonMonster(CardInfo thisCardToBeSummoned, int tileId, SummonType thisSummonType)
        {
            //then summon the card
            Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(thisCardToBeSummoned.ID), TURNPLAYER, false);
            _CardsOnBoard.Add(thisCard);

            //Normal and Ritual summons are the only ones that dimension a dice on the board.
            if (thisSummonType == SummonType.Normal || thisSummonType == SummonType.Ritual) 
            {
                _Tiles[tileId].SummonCard(thisCard);
            }
            else
            {
                _Tiles[tileId].NonDimensionSummon(thisCard);
            }

            //if this was a Transform Summon flag the monster as such
            if(thisSummonType == SummonType.Transform)
            {
                thisCard.MarkAsTransformedInto();
            }

            //Wait 1 sec for the sound effect to finish
            WaitNSeconds(1000);

            //Check for active effects that react to monster summons
            UpdateEffectLogs(string.Format(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>Card Summoned: [{0}] On Board ID: [{1}] Owned By: [{2}]", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
            ResolveEffectsWithSummonReactionTo(thisCard);

            //Now check if the Monster has an "On Summon"/"Continuous" effect and try to activate
            if (thisCard.HasOnSummonEffect && thisCard.EffectsAreImplemented)
            {
                //Create the effect object and activate
                Effect thisCardsEffect = thisCard.GetOnSummonEffect();
                ActivateEffect(thisCardsEffect);
            }
            else if (thisCard.HasContinuousEffect && thisCard.EffectsAreImplemented)
            {
                //Create the effect object and activate
                Effect thisCardsEffect = thisCard.GetContinuousEffect();
                ActivateEffect(thisCardsEffect);
            }
            else
            {
                EnterMainPhase();
            }

            UpdateEffectLogs("-----------------------------------------------------------------------------------------" + Environment.NewLine);

            void ResolveEffectsWithSummonReactionTo(Card targetCard)
            {
                foreach (Effect thisActiveEffect in _ActiveEffects)
                {
                    if (thisActiveEffect.ReactsToMonsterSummon)
                    {
                        UpdateEffectLogs(string.Format("Reaction Check for Effect: [{0}] Origin Card Board ID: [{1}]", thisActiveEffect.ID, thisActiveEffect.OriginCard.OnBoardID));
                        switch (thisActiveEffect.ID)
                        {
                            case Effect.EffectID.DARKSymbol: DarkSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                            case Effect.EffectID.LIGHTSymbol: LightSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                            case Effect.EffectID.WATERSymbol: WaterSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                            case Effect.EffectID.FIRESymbol: FireSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                            case Effect.EffectID.EARTHSymbol: EarthSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                            case Effect.EffectID.WINDSymbol: WindSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                            case Effect.EffectID.KarbonalaWarrior_Continuous: KarbonalaWarrior_ReactTo_MonsterSummon(thisActiveEffect, targetCard); break;
                            case Effect.EffectID.TwinHeadedThunderDragon: TwinHeadedThunderDragon_ReactTo_MonsterSummon(thisActiveEffect, targetCard); break;
                            default: throw new Exception(string.Format("Effect ID: [{0}] does not have an EffectToApply Function", thisActiveEffect.ID));
                        }
                    }
                }
            }
        }
        private void DestroyCard(Tile tileLocation)
        {
            SoundServer.PlaySoundEffect(SoundEffect.CardDestroyed);

            //Save the ref of the Card Object before destroying it, we are going to need it
            Card thisCard = tileLocation.CardInPlace;

            //Now "Destroy" the card from the tile, this will remove the card link from the tile 
            //and update the UI to show the card is gone
            tileLocation.DestroyCard();

            UpdateEffectLogs(string.Format(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Card Destroyed: [{0}] On Board ID: [{1}] Owned by: [{2}]", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));


            //Now check if this card had any active Continuous effect, if so, remove the effect and revert the effect changes
            List<Effect> effectsToBeRemoved = new List<Effect>();
            foreach (Effect thisActiveEffect in _ActiveEffects)
            {
                if (thisActiveEffect.OriginCard == thisCard && thisActiveEffect.Type == Effect.EffectType.Continuous)
                {
                    effectsToBeRemoved.Add(thisActiveEffect);
                }
            }
            //Actually remove the effect from the active list
            foreach (Effect thisActiveEffect in effectsToBeRemoved)
            {
                RemoveEffect(thisActiveEffect);
            }

            //Finally, check if any active effects react to a card destryuction
            UpdateEffectLogs("Checking for active effects that react to the Card destruction.");
            foreach (Effect thisEffect in _ActiveEffects)
            {
                if (thisEffect.ReactsToMonsterDestroyed && thisCard.Category == Category.Monster)
                {
                    UpdateEffectLogs(string.Format("Reaction Check for Effect: [{0}] Origin Card Board ID: [{1}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
                    //then resolve the effct reaction
                    switch (thisEffect.ID)
                    {
                        case Effect.EffectID.KarbonalaWarrior_Continuous: KarbonalaWarrior_ReactTo_MonsterDestroyed(thisEffect, thisCard); break;
                        case Effect.EffectID.TwinHeadedThunderDragon: TwinHeadedThunderDragon_ReactTo_MonsterDestroyed(thisEffect, thisCard); break;
                        default: throw new Exception(string.Format("This effect id: [{0}] does not have a React to Monster Destroyed method assigned", thisEffect.ID));
                    }
                }
            }
        }
        private void TransformMonster(Tile tileLocation, int newMonsterID)
        {
            //Step 1: Destroy the monster to be transformed
            string oldMonstersName = tileLocation.CardInPlace.Name;
            string oldMonsterOnBoardId = tileLocation.CardInPlace.OnBoardID.ToString();
            DestroyCard(tileLocation);

            //Step 2: Create the CardInfo for the monster to transform into
            CardInfo thisNewMonsterInfo = CardDataBase.GetCardWithID(newMonsterID);
            string newMonstersName = thisNewMonsterInfo.Name;

            //Step 3: Update effect logs
            UpdateEffectLogs(string.Format("Card [{0}] with On Board ID [{1}] was transformed into [{2}].", oldMonstersName, oldMonsterOnBoardId, newMonstersName));

            //Step 4: Now Transform Summon the new monster
            SummonMonster(thisNewMonsterInfo, tileLocation.ID, SummonType.Transform);
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
        //Attack Action Data
        private List<Tile> _AttackCandidates = new List<Tile>();
        private List<Tile> _AttackRangeTiles = new List<Tile>();
        private Tile _AttackTarger;
        private Tile _AttackerTile = null;
        //Effect Action Data
        private Tile _EffectOriginTile = null;
        private List<Tile> _EffectTargetCandidates = new List<Tile>();
        private PostTargetState _CurrentPostTargetState = PostTargetState.NONE;      
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
            FusionSummonTileSelection,
            EffectTargetSelection,
        }
        private enum PostTargetState
        {
            NONE,
            FireKrakenEffect,
            ChangeOfHeartEffect,
            ThunderDragonEffect,
        }
        private enum SummonType
        {
            Normal,
            Ritual,
            Fusion,
            Transform,
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