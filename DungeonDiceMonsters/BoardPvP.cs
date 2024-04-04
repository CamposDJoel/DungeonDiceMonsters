//Joel Campos
//4/2/2024
//BoardPvP Form

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms; 

namespace DungeonDiceMonsters
{
    public partial class BoardPvP : Form
    {
        #region Constructors
        public BoardPvP(PlayerData Red, PlayerData Blue, PlayerColor UserColor, NetworkStream tmpns)
        {
            //Save a reference to the NetworkStream of our active client connection to send messages to the server
            ns = tmpns;

            //Initialize Music
            SoundServer.PlayBackgroundMusic(Song.FreeDuel, true);

            //Initalize Compents
            InitializeComponent();

            //Set Custom Event Listener
            btnRoll.MouseEnter += OnMouseHoverSound;
            btnViewBoard.MouseEnter += OnMouseHoverSound;
            //TODO: btnExit.MouseEnter += OnMouseHoverSound;
            btnReturnToTurnMenu.MouseEnter += OnMouseHoverSound;

            //Save Ref to each player's data
            UserPlayerColor = UserColor;
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
                    borderPicture.Size = new Size(46, 46);
                    borderPicture.BorderStyle = BorderStyle.FixedSingle;
                    borderPicture.BackColor = Color.Transparent;

                    //Create the ATK/DEF label
                    Label statsLabel = new Label();
                    PanelBoard.Controls.Add(statsLabel);
                    statsLabel.Location = new Point(X_Location + 2, Y_Location + 34);
                    statsLabel.Size = new Size(42, 10);
                    statsLabel.BorderStyle = BorderStyle.None;
                    statsLabel.ForeColor = System.Drawing.Color.White;
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
            _Tiles[227].ChangeOwner(PlayerColor.RED);
            _Tiles[6].ChangeOwner(PlayerColor.BLUE);

            //Summon both Symbols: Blue on TIle ID 6 and Red on Tile ID 227
            _RedSymbol = new Card(_CardsOnBoard.Count, RedData.Deck.Symbol, PlayerColor.RED);
            RedData.AddSummoningTile(_Tiles[227]);
            _Tiles[227].SummonCard(_RedSymbol);

            _BlueSymbol = new Card(_CardsOnBoard.Count, BlueData.Deck.Symbol, PlayerColor.BLUE);
            BlueData.AddSummoningTile(_Tiles[6]);
            _Tiles[6].SummonCard(_BlueSymbol);

            //Initialize the Player's Info Panels
            LoadPlayersInfo();

            //Set the initial game state and start the turn start panel            
            LaunchTurnStartPanel();
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


            lblSummonMessage.Visible = true;

            if (UserPlayerColor == TURNPLAYER)
            {
                lblSummonMessage.Text = "Select Tile to Dimension the Dice!";
                //Display the Dimension shape selector
                UpdateDimensionPreview();
                PanelDimenFormSelector.Visible = true;
            }
            else
            {
                lblSummonMessage.Text = "Opponent is Selecting a Tile to Summon!";
                PanelDimenFormSelector.Visible = false;
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

            //Enable the Board Panel to interact with it
            PanelBoard.Enabled = true;

            //Setup the tile candidates
            if (UserPlayerColor == TURNPLAYER)
            {
                _SetCandidates.Clear();
                _SetCandidates = RedData.GetSetCardTileCandidates();
                lblSetCardMessage.Visible = true;
                foreach (Tile tile in _SetCandidates)
                {
                    tile.MarkSetTarget();
                }
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
                    if (PicCardArtworkBottom.Image != null) { PicCardArtworkBottom.Image.Dispose(); }
                    PicCardArtworkBottom.Image = ImageServer.CardArtworkImage(0);

                    lblCardName.Text = "";
                    lblCardType.Text = "";
                    lblCardLevel.Text = "";
                    lblAttribute.Text = "";
                    lblStatsATK.Text = "";
                    lblStatsDEF.Text = "";
                    lblStatsLP.Text = "";
                    lblCardText.Text = "Opponent's Facedown card.";
                }
                else
                {
                    if (thisCard.IsASymbol)
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

            if (_CurrentTileSelected.HasAnAdjecentTile(TileDirection.South))
            {
                Tile southTile = _CurrentTileSelected.GetAdjencentTile(TileDirection.South);
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

            if (_CurrentTileSelected.HasAnAdjecentTile(TileDirection.East))
            {
                Tile eastTile = _CurrentTileSelected.GetAdjencentTile(TileDirection.East);
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

            if (_CurrentTileSelected.HasAnAdjecentTile(TileDirection.West))
            {
                Tile westTile = _CurrentTileSelected.GetAdjencentTile(TileDirection.West);
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
            if (PicAttacker.BackgroundImage != null) { PicAttacker.BackgroundImage.Dispose(); }
            PicAttacker.BackgroundImage = null;
            PicAttacker.BackgroundImage = ImageServer.FullCardImage(Attacker.CardID);
            lblBattleMenuATALP.Text = "LP: " + Attacker.LP;
            lblAttackerATK.Text = "ATK: " + Attacker.ATK;

            //Set the defender's data. if the defender is a non-monster place the clear data.
            Card Defender = _AttackTarger.CardInPlace;
            if (Defender.Category == Category.Monster)
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
        private void UpdateDimensionPreview()
        {           
            //Update UI
            PicCurrentForm.Image = ImageServer.DimensionForm(_CurrentDimensionForm);
            lblFormName.Text = _CurrentDimensionForm.ToString();
        }
        #endregion

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
                    case "[END TURN]": btnEndTurn_Base(); break;
                    case "[CHANGE DIMENSION SELECTION]": UpdateDimension_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[CLICK TILE TO ACTION]": TileClick_MainPhase_Base(); break;
                    case "[CLICK CANCEL ACTION MENU]": btnActionCancel_Base(); break;
                    case "[CLICK MOVE ACTION MENU]": btnActionMove_Base(); break;
                    case "[CLICK CANCEL MOVE MENU]": btnMoveMenuCancel_Base(); break;
                    case "[CLICK TILE TO MOVE]": TileClick_MoveCard_Base(Convert.ToInt32(MessageTokens[2])); break;
                    case "[CLICK FINISH MOVE MENU]": btnMoveMenuFinish_Base(); break;
                }
            }
            else
            {
                throw new Exception("Message Received with an invalid game state");
            }
        }

        #region Turn Steps Functions
        private void LaunchTurnStartPanel()
        {
            //Depending on the TURNPLAYER enable/disable the buttons
            //Only the TURN PLAYER can take action
            if (UserPlayerColor == TURNPLAYER)
            {
                btnRoll.Visible = true;
                btnViewBoard.Visible = true;
                lblTurnStartInactiveWarning.Visible = false;
            }
            else
            {
                btnRoll.Visible = false;
                btnViewBoard.Visible = false;
                lblTurnStartInactiveWarning.Visible = true;
            }

            //Show the panel
            lblTurnStartMessage.Text = string.Format("{0} Player Turn!", TURNPLAYER);
            PanelTurnStartMenu.Visible = true;

            //Set the Game State
            _CurrentGameState = GameState.TurnStartMenu;
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
            Application.Exit();
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
                if ((_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard) && _Tiles[tileID].IsOccupied)
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
                if ((_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard) && _Tiles[tileID].IsOccupied)
                {
                    SendMessageToServer(string.Format("{0}|{1}|{2}", "[ON MOUSE LEAVE TILE]", _CurrentGameState.ToString(), tileID.ToString()));
                }

                //Perform the action
                OnMouseLeavePicture_Base(tileID);
            }
        }
        private void Tile_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                if (_CurrentGameState == GameState.MainPhaseBoard)
                {
                    if (_CurrentTileSelected.IsOccupied)
                    {
                        if (_CurrentTileSelected.CardInPlace.Owner == UserPlayerColor)
                        {
                            //Send the action message to the server
                            SendMessageToServer("[CLICK TILE TO ACTION]|" + _CurrentGameState.ToString());

                            //Perform the action
                            TileClick_MainPhase_Base();
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
                        //Send the action message to the server
                        SendMessageToServer(string.Format("[CLICK TILE TO MOVE]|{0}|{1}", _CurrentGameState.ToString(), tileId));

                        //Perform the action
                        TileClick_MoveCard_Base(tileId);
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
                        Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(_CardToBeSet.ID), TURNPLAYER, true);
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

                    if (_validDimension)
                    {
                        //Send the action message to the server
                        SendMessageToServer("[CLICK TILE TO SUMMON]|"+ _CurrentGameState.ToString() + "|" + tileId);

                        //Perform the action
                        TileClick_SummonCard_Base(tileId);
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
        }
        #endregion

        #region Action Menu
        private void btnActionCancel_Click(object sender, EventArgs e)
        {
            if(UserPlayerColor == TURNPLAYER)
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
                if (UserPlayerColor == TURNPLAYER)
                {
                    btnReturnToTurnMenu.Visible = true;
                }
                else
                {
                    btnReturnToTurnMenu.Visible = false;
                }
            }));
        }
        private void OnMouseEnterPicture_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Hover);
                    _CurrentTileSelected = _Tiles[tileId];
                    _CurrentTileSelected.Hover();

                    UpdateDebugWindow();
                    LoadCardInfoPanel();
                }
                else if (_CurrentGameState == GameState.SummonCard)
                {
                    //Highlight the possible dimmension
                    SoundServer.PlaySoundEffect(SoundEffect.Hover);

                    //Use the following function to get the ref to the tiles that compose the dimension
                    _dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                    //Check if it is valid or not (it becomes invalid if at least 1 tile is Null AND 
                    //if none of the tiles are adjecent to any other owned by the player)
                    _validDimension = Dimension.IsThisDimensionValid(_dimensionTiles, UserPlayerColor);

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
                    _CurrentTileSelected.Leave();
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
                        if (dimensionTiles[x] != null) { dimensionTiles[x].SetTileColor(); }
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
            }));
        }
        private void TileClick_SummonCard_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate () {
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
                lblSummonMessage.Visible = false;
                PanelDimenFormSelector.Visible = false;
                _CurrentDimensionForm = DimensionForms.CrossBase;
                _CurrentTileSelected = _dimensionTiles[0];
                _CurrentGameState = GameState.MainPhaseBoard;

                //Only enable the "End Turn" button for the TURN PLAYER
                if(UserPlayerColor == TURNPLAYER)
                {
                    btnEndTurn.Visible = true;
                    lblOponentActionWarning.Visible = false;
                }
                else
                {
                    btnEndTurn.Visible = false;
                    lblOponentActionWarning.Visible = true;
                }
                
            }));
        }
        private void TileClick_MainPhase_Base()
        {
            Invoke(new MethodInvoker(delegate () {
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
                    PlayerData TurnPlayerData = RedData;
                    if (TURNPLAYER == PlayerColor.BLUE)
                    {
                        TurnPlayerData = BlueData;
                    }
                    _TMPMoveCrestCount = TurnPlayerData.Crests_MOV;
                    lblMoveMenuCrestCount.Text = string.Format("[MOV]x {0}", _TMPMoveCrestCount);
                }

                //Determine if this card can attack if:
                // 1. Has Attacks available left
                // 2. Player has enough atack crest to pay its cost
                // 3. Has at least 1 adjecent attack candidate
                // 4. Card is a monster

                PlayerColor TargetPlayerColor = PlayerColor.RED;
                if (UserPlayerColor == PlayerColor.RED) { TargetPlayerColor = PlayerColor.BLUE; }
                PlayerData UserPlayerData = RedData;
                if (UserPlayerColor == PlayerColor.BLUE) { UserPlayerData = BlueData; }

                _AttackCandidates = _CurrentTileSelected.GetAttackTargerCandidates(TargetPlayerColor);
                if (thiscard.AttacksAvaiable == 0 || thiscard.AttackCost > UserPlayerData.Crests_ATK || _AttackCandidates.Count == 0 || thiscard.Category != Category.Monster)
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

            }));   
        }
        private void btnEndTurn_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                //Clean up the board
                btnEndTurn.Visible = false;
                lblOponentActionWarning.Visible = false;

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
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //Close the Action menu/Card info panel and return to the MainPhase Stage 
                _CurrentTileSelected.Leave();
                PanelActionMenu.Visible = false;
                _CurrentGameState = GameState.MainPhaseBoard;
                if(UserPlayerColor == TURNPLAYER)
                {
                    btnEndTurn.Visible = true;
                }
            }));           
        }
        private void btnActionMove_Base()
        {
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _CurrentGameState = GameState.MovingCard;

                _InitialTileMove = _CurrentTileSelected;

                PanelActionMenu.Visible = false;
                DisplayMoveCandidates();
                PlaceMoveMenu();

                btnMoveMenuFinish.Enabled = false;
                btnMoveMenuCancel.Enabled = true;
                lblMoveMenuCrestCount.ForeColor = Color.Yellow;
                PanelMoveMenu.Visible = true;
            }));            
        }
        private void btnMoveMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate () {
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
                btnEndTurn.Visible = true;
            }));
        }
        private void TileClick_MoveCard_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate () {
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
            Invoke(new MethodInvoker(delegate () {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //Now clear the borders of all the candidates tiles to their og color
                for (int x = 0; x < _MoveCandidates.Count; x++)
                {
                    _MoveCandidates[x].SetTileColor();
                }
                _MoveCandidates.Clear();

                PanelMoveMenu.Visible = false;

                //Flag that this card moved already this turn
                _CurrentTileSelected.CardInPlace.RemoveMoveCounter();

                //Apply the amoutn of crests used
                PlayerData TurnPlayerData = RedData;
                if (TURNPLAYER == PlayerColor.BLUE) { TurnPlayerData = BlueData; }

                int amountUsed = TurnPlayerData.Crests_MOV - _TMPMoveCrestCount;               
                TurnPlayerData.RemoveCrests(Crest.MOV, amountUsed);
                LoadPlayersInfo();

                _CurrentGameState = GameState.MainPhaseBoard;

                if(UserPlayerColor == TURNPLAYER)
                {
                    btnEndTurn.Visible = true;
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
        //Client NetworkStream to send message to the server
        private NetworkStream ns;
        private RollDiceMenu _RollDiceForm;
        #endregion        

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
        }      
    }

    public enum PlayerColor
    {
        NONE,
        RED,
        BLUE,
    }    
}
