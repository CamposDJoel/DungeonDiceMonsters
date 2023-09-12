﻿//Joel campos
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
        BoardViewMode,
        MainPhaseBoard,
        ActionMenuDisplay,
    }
    public partial class BoardForm : Form
    {
        public BoardForm()
        {
            SoundServer.PlayBackgroundMusic(Song.FreeDuel, true);
            InitializeComponent();

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
                    insidePicture.MouseEnter+=OnMouseEnterPicture;
                    insidePicture.MouseLeave+=OnMouseLeavePicture;
                    insidePicture.Click += Tile_Click;

                    //Create each border picture box 
                    //(create this one after so it is created "behind" the inside picture
                    PictureBox borderPicture = new PictureBox();
                    PanelBoard.Controls.Add(borderPicture);
                    borderPicture.Location = new Point(X_Location, Y_Location);
                    borderPicture.Size = new Size(46, 46);
                    borderPicture.BorderStyle = BorderStyle.FixedSingle;
                    borderPicture.BackColor = Color.Transparent;

                    //create and add a new tile object using the above 2 picture boxes
                    _Tiles.Add(new Tile(insidePicture, borderPicture));

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
                if(tile.ID > 13)
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
                if(tile.ID != 12 && tile.ID != 25 && tile.ID != 38 && tile.ID != 51 && tile.ID != 64
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
            _Tiles[6].ChangeOwner(PlayerOwner.Blue);
            _Tiles[227].ChangeOwner(PlayerOwner.Red);

            //TEST: Set some tiles
            _Tiles[214].ChangeOwner(PlayerOwner.Red);
            _Tiles[201].ChangeOwner(PlayerOwner.Red);
            _Tiles[188].ChangeOwner(PlayerOwner.Red);
            _Tiles[175].ChangeOwner(PlayerOwner.Red);
            _Tiles[200].ChangeOwner(PlayerOwner.Red);
            _Tiles[202].ChangeOwner(PlayerOwner.Red);

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

            //Summon a monster to move
            _CurrentTileSelected = _Tiles[188];
            Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(4), PlayerOwner.Red);
            _CardsOnBoard.Add(thisCard);
            _CurrentTileSelected.SummonCard(thisCard);

            _CurrentTileSelected = _Tiles[34];
            Card thisCard2 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(6), PlayerOwner.Red);
            _CardsOnBoard.Add(thisCard2);
            _CurrentTileSelected.SummonCard(thisCard2);

            _CurrentTileSelected = _Tiles[48];
            Card thisCard3 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(1), PlayerOwner.Blue);
            _CardsOnBoard.Add(thisCard3);
            _CurrentTileSelected.SummonCard(thisCard3);

            _CurrentTileSelected = _Tiles[40];
            Card thisCard4 = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(2), PlayerOwner.Blue);
            _CardsOnBoard.Add(thisCard4);
            _CurrentTileSelected.SummonCard(thisCard4);
        }

        private GameState _CurrentGameState = GameState.MainPhaseBoard;
        private List<Tile> _Tiles = new List<Tile>();
        private Tile _CurrentTileSelected = null;
        private List<Card> _CardsOnBoard = new List<Card>();

        private void LoadCardInfoPanel()
        {
            if (_CurrentTileSelected.IsOccupied)
            {
                //Get the CardInfo object to populate the UI
                CardInfo thisCard = _CurrentTileSelected.CardInPlace.Info;
                int cardID = thisCard.ID;

                //Set the Panel Back Color based on whose the card owner
                if (_CurrentTileSelected.CardInPlace.Owner == PlayerOwner.Red)
                {
                    PanelCardInfo.BackColor = Color.DarkRed;
                }
                else
                {
                    PanelCardInfo.BackColor = Color.DarkBlue;
                }

                //Populate the UI
                if (PicCardArtworkBottom.Image != null) { PicCardArtworkBottom.Image.Dispose(); }
                PicCardArtworkBottom.Image = ImageServer.CardArtworkImage(cardID);

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

                if (thisCard.Category == "Monster")
                {
                    lblStats.Text = "ATK " + thisCard.ATK + " / DEF " + thisCard.DEF + " / LP " + thisCard.LP;
                }
                else { lblStats.Text = ""; }

                lblCardText.Text = thisCard.CardText;
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
                lblStats.Text = "";
                lblCardText.Text = "";
            }
            
        }
        private void Tile_Click(object sender, EventArgs e)
        {
            if(_CurrentGameState == GameState.MainPhaseBoard)
            {
                if(_CurrentTileSelected.IsOccupied)
                {
                    if (_CurrentTileSelected.CardInPlace.Owner == PlayerOwner.Red)
                    {
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
                        //TODO

                        //Change the game state
                        _CurrentGameState = GameState.ActionMenuDisplay;
                    }
                }                          
            }
        }
        private void OnMouseEnterPicture(object sender, EventArgs e)
        {
            if(_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard)
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
        private void UpdateDebugWindow()
        {
            lblDebugTileID.Text = "Tile ID: " + _CurrentTileSelected.ID;
            lblDebugNorthAdj.Text = "North Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(TileDirection.North);
            lblDebugSouthAdj.Text = "South Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(TileDirection.South);
            lblDebugEastAdj.Text = "East Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(TileDirection.East);
            lblDebugWestAdj.Text = "West Tile ID: " + _CurrentTileSelected.GetAdjencentTileID(TileDirection.West);
            lblDebugOwner.Text = "Owner: " + _CurrentTileSelected.Owner;
            lblDebugIsOccupied.Text = "Occupied: " + _CurrentTileSelected.IsOccupied.ToString();
            if(_CurrentTileSelected.IsOccupied)
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

        private void btnActionCancel_Click(object sender, EventArgs e)
        {
            //Close the Action menu/Card info panel and return to the MainPhase Stage 
            _CurrentTileSelected.Leave();
            PanelActionMenu.Visible = false;
            _CurrentGameState = GameState.MainPhaseBoard;
        }
    }
}
