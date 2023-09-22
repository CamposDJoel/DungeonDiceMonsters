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
    public partial class BoardForm : Form
    {
        public BoardForm(PlayerData Red, PlayerData Blue)
        {
            SoundServer.PlayBackgroundMusic(Song.FreeDuel, true);
            InitializeComponent();

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

            //Save Ref to each player's data
            RedData = Red; BlueData = Blue;

            //more test data
            RedData.AddCrests(Crest.Movement, 10);
            RedData.AddCrests(Crest.Attack, 10);
            RedData.AddCrests(Crest.Defense, 2);
            RedData.AddCrests(Crest.Magic, 7);
            RedData.AddCrests(Crest.Trap, 1);

            BlueData.AddCrests(Crest.Movement, 12);
            BlueData.AddCrests(Crest.Attack, 6);
            BlueData.AddCrests(Crest.Defense, 2);
            BlueData.AddCrests(Crest.Magic, 9);
            BlueData.AddCrests(Crest.Trap, 2);

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

        }

        //Public function
        public void SetupMainPhaseNoSummon()
        {
            //Switch to the Main Phase of the player
            _CurrentGameState = GameState.MainPhaseBoard;

            //Relaod the player info panels to update crests
            LoadPlayersInfo();

            //Enable the Board Panel to interact with it
            PanelBoard.Enabled = true;
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

        //Data
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
        Card _RedSymbol;
        Card _BlueSymbol;
        //Summoning data
        List<Tile> _SetCandidates = new List<Tile>();
        CardInfo _CardToBeSet;

        //Private functions
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
                        lblAttribute.Text = thisCard.Attribute;
                        lblStatsATK.Text = "";
                        lblStatsDEF.Text = "";
                        lblStatsLP.Text = thisCard.LP.ToString();
                        lblCardText.Text = thisCard.Info.CardText;
                    }
                    else
                    {
                        //Populate the UI
                        if (PicCardArtworkBottom.Image != null) { PicCardArtworkBottom.Image.Dispose(); }
                        PicCardArtworkBottom.Image = ImageServer.CardArtworkImage(cardID);

                        lblCardName.Text = thisCard.Name;

                        string secondaryType = "";
                        if (thisCard.Info.IsFusion) { secondaryType = "/Fusion"; }
                        if (thisCard.Info.IsRitual) { secondaryType = "/Ritual"; }
                        if (thisCard.Category == "Spell") { secondaryType = " Spell"; }
                        if (thisCard.Category == "Trap") { secondaryType = " Trap"; }


                        lblCardType.Text = thisCard.Info.Type + secondaryType;

                        if (thisCard.Category == "Monster") { lblCardLevel.Text = "Lv. " + thisCard.Info.Level; }
                        else { lblCardLevel.Text = ""; }

                        if (thisCard.Category == "Monster") { lblAttribute.Text = thisCard.Attribute; }
                        else { lblAttribute.Text = ""; }

                        if (thisCard.Category == "Monster")
                        {
                            lblStatsATK.Text = thisCard.ATK.ToString();
                            lblStatsDEF.Text = thisCard.DEF.ToString();
                            lblStatsLP.Text = thisCard.LP.ToString();

                            if (thisCard.ATK > thisCard.Info.ATK) { lblStatsATK.ForeColor = Color.Green; }
                            else if (thisCard.ATK < thisCard.Info.ATK) { lblStatsATK.ForeColor = Color.Red; }
                            else { lblStatsATK.ForeColor = Color.White; }

                            if (thisCard.DEF > thisCard.Info.DEF) { lblStatsDEF.ForeColor = Color.Green; }
                            else if (thisCard.DEF < thisCard.Info.DEF) { lblStatsDEF.ForeColor = Color.Red; }
                            else { lblStatsDEF.ForeColor = Color.White; }

                            if (thisCard.LP > thisCard.Info.LP) { lblStatsLP.ForeColor = Color.Green; }
                            else if (thisCard.LP < thisCard.Info.LP) { lblStatsLP.ForeColor = Color.Red; }
                            else { lblStatsLP.ForeColor = Color.White; }
                        }
                        else
                        {
                            lblStatsATK.Text = "";
                            lblStatsDEF.Text = "";
                            lblStatsLP.Text = "";
                        }

                        lblCardText.Text = thisCard.Info.CardText;
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
            if(Defender.Category == "Monster")
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
            if (attacker.Attribute == "LIGHT" && defender.Attribute == "DARK") { return true; }

            switch (attacker.Attribute)
            {
                case "LIGHT": if (defender.Attribute == "DARK") { return true; } else { return false; }
                case "DARK": if (defender.Attribute == "LIGHT") { return true; } else { return false; }
                case "WATER": if (defender.Attribute == "FIRE") { return true; } else { return false; }
                case "FIRE": if (defender.Attribute == "EARTH") { return true; } else { return false; }
                case "EARTH": if (defender.Attribute == "WIND") { return true; } else { return false; }
                case "WIND": if (defender.Attribute == "WATER") { return true; } else { return false; }
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
        public static void WaitNSeconds(double milliseconds)
        {
            if (milliseconds < 1) return;
            DateTime _desired = DateTime.Now.AddMilliseconds(milliseconds);
            while (DateTime.Now < _desired)
            {
                Application.DoEvents();
            }
        }

        //Events
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

        }
        private void OnMouseLeavePicture(object sender, EventArgs e)
        {
            if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard)
            {
                _CurrentTileSelected.Leave();
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
                        if (thiscard.AttackedThisTurn || thiscard.AttackCost > RedData.Crests_ATK || _AttackCandidates.Count == 0 || thiscard.Category != "Monster")
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
            RedData.RemoveCrests(Crest.Movement, amountUsed);
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
            if(_AttackTarger.CardInPlace.Category == "Monster")
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
                RedData.RemoveCrests(Crest.Attack, creststoremoveATK);
                BlueData.RemoveCrests(Crest.Defense, creststoremoveDEF);
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
            else if (_AttackTarger.CardInPlace.Category == "Symbol")
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
    }
}