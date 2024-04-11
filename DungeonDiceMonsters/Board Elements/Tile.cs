//Joel Campos
//9/11/2023
//TileClass

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public class Tile
    {
        #region Constructors
        public Tile(PictureBox inside, PictureBox border, Label statsLabel, Label statsLabel3)
        {
            _Border = border;
            _CardImage = inside;
            _StatsLabelATK = statsLabel;
            _StatsLabelDEF = statsLabel3;
        }
        #endregion

        #region UI Mod Methods
        public void ReloadTileUI()
        {
            //THERE ARE 3 UI ELEMENT: The Outer box, aka _Border, the insider box, aka _CardImage, and the Stats Label (for monster ATK/DEF)
            //Update this 3 elemeents based on the current state of the tile

            //BORDER AND CARDIMAGE Backcolors
            //Set backcolor based on owner:  [NOT OWNED: Transparent | RED: DarkRed | BLUE: DarkBlue]
            switch (_Owner)
            {
                case PlayerColor.NONE: _CardImage.BackColor = Color.Transparent; _Border.BackColor = Color.Transparent; break;
                case PlayerColor.RED: _CardImage.BackColor = Color.DarkRed; _Border.BackColor = Color.DarkRed; break;
                case PlayerColor.BLUE: _CardImage.BackColor = Color.DarkBlue; _Border.BackColor = Color.DarkBlue; break;
            }
            //If the tile is a "Summoning Tile" update its backcolor to Orange
            if (_IsSummonTile)
            {
                _CardImage.BackColor = Color.Orange;
            }


            //_InsideBox image and _Stats
            //Set the Card Image of the Card in the Tile, if this tile is occupied, otherwise Card Image will be null
            //Card Image backcolor will be 
            //Sets the stats visible if the card is a face up card (monster)
            if (_Occupied)
            {
                //If the Card is a symbol load the Symbol card artwork
                if (_card.IsASymbol)
                {
                    ImageServer.LoadImage(_CardImage, CardImageType.Symbol, _card.Attribute.ToString());
                }
                else
                {
                    //if the Card is a Monster (Faceup) load the monster artwork and stats
                    if (_card.Category == Category.Monster)
                    {
                        ImageServer.LoadImage(_CardImage, CardImageType.CardArtwork, _card.CardID.ToString());
                        _StatsLabelATK.Text = _card.ATK.ToString();
                        _StatsLabelDEF.Text = _card.DEF.ToString();
                        _StatsLabelATK.ForeColor = _card.GetATKStatus();
                        _StatsLabelDEF.ForeColor = _card.GetDEFStatus();
                        _StatsLabelATK.Visible = true;
                        _StatsLabelDEF.Visible = true;
                    }
                    else
                    {
                        //The card will be a facedown Spell/Trap
                        ImageServer.LoadImage(_CardImage, CardImageType.CardArtwork, "0");
                        _StatsLabelATK.Visible = false;
                        _StatsLabelDEF.Visible = false;
                    }
                }
            }
            else
            {
                if (_CardImage.Image != null) { _CardImage.Image.Dispose(); }
                _CardImage.Image = null;
                _StatsLabelATK.Visible = false;
                _StatsLabelDEF.Visible = false;
            }
        }
        #endregion

        #region Public Methods       
        public void SetAdjecentTileLink(TileDirection d, Tile tile)
        {
            switch (d)
            {
                case TileDirection.North: _NorthTile = tile; break;
                case TileDirection.South: _SouthTile = tile; break;
                case TileDirection.East: _EastTile = tile; break;
                case TileDirection.West: _WestTile = tile; break;
            }
        }
        public Tile GetAdjencentTile(TileDirection d)
        {
            switch (d)
            {
                case TileDirection.North: return _NorthTile;
                case TileDirection.South: return _SouthTile;
                case TileDirection.East: return _EastTile;
                case TileDirection.West: return _WestTile;
                default: return null;
            }
        }
        public int GetAdjencentTileID(TileDirection d)
        {
            switch (d)
            {
                case TileDirection.North: if (_NorthTile != null) { return _NorthTile.ID; } else { return -1; }
                case TileDirection.South: if (_SouthTile != null) { return _SouthTile.ID; } else { return -1; }
                case TileDirection.East: if (_EastTile != null) { return _EastTile.ID; } else { return -1; }
                case TileDirection.West: if (_WestTile != null) { return _WestTile.ID; } else { return -1; }
                default: return -1;
            }
        }
        public bool HasAnAdjecentTile(TileDirection d)
        {
            switch (d)
            {
                case TileDirection.North: if (_NorthTile == null) { return false; } else { return true; }
                case TileDirection.South: if (_SouthTile == null) { return false; } else { return true; }
                case TileDirection.East: if (_EastTile == null) { return false; } else { return true; }
                case TileDirection.West: if (_WestTile == null) { return false; } else { return true; }
                default: return false;
            }
        }
        public void Hover()
        {
            _Border.BackColor = Color.Yellow;
        }
        public void ChangeOwner(PlayerColor owner)
        {
            _Owner = owner;
            ReloadTileUI();
        }
        public void SummonCard(Card card)
        {
            _card = card;
            _Occupied = true;
            _IsSummonTile = true;
            _card.SetCurrentTile(this);
            ReloadTileUI();
        }
        public void SetCard(Card card)
        {
            _card = card;
            _Occupied = true;
            _card.SetCurrentTile(this);
            ReloadTileUI();
        }
        public void MoveInCard(Card card)
        {
            _card = card;
            _Occupied = true;
            _card.SetCurrentTile(this);
            ReloadTileUI();
        }
        public void RemoveCard()
        {
            _card.SetCurrentTile(null);
            _card = null;
            _Occupied = false;
            ReloadTileUI();
        }
        public void DestroyCard()
        {
            _card.Destroy();
            _card = null;
            _Occupied = false;
            _card.SetCurrentTile(null);
            ReloadTileUI();
        }
        public List<Tile> GetAttackTargerCandidates(PlayerColor enemy)
        {
            List<Tile> candidates = new List<Tile>();

            if (HasAnAdjecentEnemyCard(TileDirection.North, enemy)){ candidates.Add(_NorthTile); }
            if (HasAnAdjecentEnemyCard(TileDirection.South, enemy)){ candidates.Add(_SouthTile); }
            if (HasAnAdjecentEnemyCard(TileDirection.East, enemy)){ candidates.Add(_EastTile); }
            if (HasAnAdjecentEnemyCard(TileDirection.West, enemy)){ candidates.Add(_WestTile); }

            return candidates;
        }
        private bool HasAnAdjecentEnemyCard(TileDirection direction, PlayerColor enemy)
        {
            bool hasIt = false;
            switch (direction) 
            {
                case TileDirection.North:
                    if(HasAnAdjecentTile(TileDirection.North))
                    {
                        if(_NorthTile.IsOccupied)
                        {
                            if (_NorthTile.CardInPlace.Owner == enemy) { hasIt = true; }
                        }
                    }
                break;
                case TileDirection.South:
                    if (HasAnAdjecentTile(TileDirection.South))
                    {
                        if (_SouthTile.IsOccupied)
                        {
                            if (_SouthTile.CardInPlace.Owner == enemy) { hasIt = true; }
                        }
                    }
                    break;
                case TileDirection.East:
                    if (HasAnAdjecentTile(TileDirection.East))
                    {
                        if (_EastTile.IsOccupied)
                        {
                            if (_EastTile.CardInPlace.Owner == enemy) { hasIt = true; }
                        }
                    }
                    break;
                case TileDirection.West:
                    if (HasAnAdjecentTile(TileDirection.West))
                    {
                        if (_WestTile.IsOccupied)
                        {
                            if (_WestTile.CardInPlace.Owner == enemy) { hasIt = true; }
                        }
                    }
                    break;
            }

            return hasIt;
        }
        public bool HasAnAdjecentTileOwnBy(PlayerColor expectedOwner)
        {
            bool hasIt = false;

            if (HasAnAdjecentTile(TileDirection.North))
            {
                if (_NorthTile.Owner == expectedOwner)
                {
                    hasIt = true;
                }
            }

            if (HasAnAdjecentTile(TileDirection.South))
            {
                if (_SouthTile.Owner == expectedOwner)
                {
                    hasIt = true;
                }
            }

            if (HasAnAdjecentTile(TileDirection.East))
            {
                if (_EastTile.Owner == expectedOwner)
                {
                    hasIt = true;
                }
            }

            if (HasAnAdjecentTile(TileDirection.West))
            {
                if (_WestTile.Owner == expectedOwner)
                {
                    hasIt = true;
                }
            }            

            return hasIt;
        }     
        public void MarkFreeToMove()
        {
            _CardImage.BackColor = Color.Green;            
        }
        public void MarkAttackTarget()
        {
            _Border.BackColor = Color.Chartreuse;
        }
        public void MarkSetTarget()
        {
            _CardImage.BackColor = Color.Green;
        }
        public void MarkDimensionTile(bool isValid)
        {
            if(isValid)
            {
                _CardImage.BackColor = Color.Green;
            }
            else
            {
                _CardImage.BackColor = Color.Red;
            }
        }
        public void MarkDimensionSummonTile()
        {
            _CardImage.BackColor = Color.Orange;
        }
        public Tile[] GetDimensionTiles(DimensionForms dForm)
        {
            Tile[] tiles = new Tile[6];

            switch (dForm)
            {
                case DimensionForms.CrossBase:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 West
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 South
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 5: 1 South, 1 South
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South });
                    break;
                case DimensionForms.CrossRight:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 West
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 West, 1 West
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 5: 1 South
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    break;
                case DimensionForms.CrossLeft:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 East, 1 East
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 4: 1 West
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 5: 1 South
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    break;
                case DimensionForms.CrossUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 North, 1 North
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 3: 1 East
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 4: 1 West
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 5: 1 South
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    break;
                case DimensionForms.LongBase:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 North, 1 North
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 3: 1 West
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 West, 1 South
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.South });
                    //Tile 5: 1 West, 1 South , 1 South
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.South, TileDirection.South });
                    break;
                case DimensionForms.LongRight:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 East, 1 East
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 4: 1 North, 1 West
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.West });
                    //Tile 5: 1 Noth, 1 West, 1 West
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.West, TileDirection.West });
                    break;
                case DimensionForms.LongLeft:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 West, 1 West
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 3: 1 South
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 4: 1 South, 1 East
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.East });
                    //Tile 5: 1 South, 1 East, 1 East
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.East, TileDirection.East });
                    break;
                case DimensionForms.LongUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 East
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 2: 1 East, 1 North
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.North });
                    //Tile 3: 1 East, 1 North, 1 North
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.North, TileDirection.North });
                    //Tile 4: 1 South
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 5: 1 South, 1 South
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South });
                    break;
                case DimensionForms.LongFlippedBase:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 North, 1 North
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 3: 1 East
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 4: 1 East, 1 South
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.South });
                    //Tile 5: 1 East, 1 South 1 south
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.South, TileDirection.South });
                    break;
                case DimensionForms.LongFlippedRight:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 East
                    tiles[1] = GetTileInDirection( new List<TileDirection> { TileDirection.East });
                    //Tile 2: 1 East, 1 East
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 3: 1 South
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 4: 1 South, 1 West
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.West });
                    //Tile 5: 1 South, 1 West, 1 West
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.West, TileDirection.West });
                    break;
                case DimensionForms.LongFlippedLeft:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 West, 1 West
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 3: 1 North
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 4: 1 North, 1 East
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.East });
                    //Tile 5: 1 North, 1 East, 1 East
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.East, TileDirection.East });
                    break;
                case DimensionForms.LongFlippedUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 South
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 2: 1 South, 1 South
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South });
                    //Tile 3: 1 West
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 West, 1 North
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.North });
                    //Tile 5: 1 West, 1 North, 1 North
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.North, TileDirection.North });
                    break;
                case DimensionForms.ZBase:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 East
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 2: 1 South
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 3: 1 South 1 South
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South });
                    //Tile 4: 1 South 1 South 1 South
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South });
                    //Tile 5: 1 South 1 South 1 South 1 West
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.West });
                    break;
                case DimensionForms.ZRight:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 South
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 2: 1 West
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 3: 1 West 1 West
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 4: 1 West 1 West 1 West
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West });
                    //Tile 5: 1 West 1 West 1 West 1 Noth
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West, TileDirection.North });
                    break;
                case DimensionForms.ZLeft:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 East 1 East
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 4: 1 East 1 East 1 East
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East });
                    //Tile 5: 1 East 1 East 1 East 1 South
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East, TileDirection.South });
                    break;
                case DimensionForms.ZUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 North
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 3: 1 North 1 North
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 4: 1 North 1 North 1 North
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North });
                    //Tile 5: 1 North 1 North 1 North 1 East
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.East });
                    break;
                case DimensionForms.ZFlippedBase:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 South
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 3: 1 South 1 South
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South });
                    //Tile 4: 1 South 1 South 1 South
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South });
                    //Tile 5: 1 South 1 South 1 South 1 East
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.East });
                    break;
                case DimensionForms.ZFlippedRight:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 West
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 3: 1 West 1 West
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 4: 1 West 1 West 1 West
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West });
                    //Tile 5: 1 West 1 West 1 West 1 South
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West, TileDirection.South });
                    break;
                case DimensionForms.ZFlippedLeft:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 South
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 East 1 East
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 4: 1 East 1 East 1 East
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East });
                    //Tile 5: 1 East 1 East 1 East 1 North
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East, TileDirection.North });
                    break;
                case DimensionForms.ZFlippedUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 North
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 3: 1 North 1 North
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 4: 1 North 1 North 1 North
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North });
                    //Tile 5: 1 North 1 North 1 North 1 East
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.East });
                    break;
                case DimensionForms.TBase:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 West
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 2: 1 East
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 3: 1 South 
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 4: 1 South 1 South
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South });
                    //Tile 5: 1 South 1 South 1 South
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South });
                    break;
                case DimensionForms.TRight:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 South
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 3: 1 West 
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 4: 1 West  1 West 
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West });
                    //Tile 5: 1 West  1 West  1 West 
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West });
                    break;
                case DimensionForms.TLeft:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 North
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 2: 1 South
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.South });
                    //Tile 3: 1 East 
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 4: 1 East  1 East 
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East });
                    //Tile 5: 1 East  1 East  1 East
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East });
                    break;
                case DimensionForms.TUpSideDown:
                    //Tile 0: The base tile
                    tiles[0] = this;
                    //Tile 1: 1 East
                    tiles[1] = GetTileInDirection(new List<TileDirection> { TileDirection.East });
                    //Tile 2: 1 West
                    tiles[2] = GetTileInDirection(new List<TileDirection> { TileDirection.West });
                    //Tile 3: 1 North 
                    tiles[3] = GetTileInDirection(new List<TileDirection> { TileDirection.North });
                    //Tile 4: 1 North  1 North
                    tiles[4] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North });
                    //Tile 5: 1 North 1 North  1 North
                    tiles[5] = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North });
                    break;
                default: throw new Exception("GetDimensionTiles function took an invalid dimension form value: " + dForm);
            }


            return tiles;
        }
        #endregion

        #region Private Methods
        private Tile GetTileInDirection(List<TileDirection> directions)
        {
            //Set the starting tile this
            Tile destinationTile = this;

            //move the dimension tile based on the list of directions.
            for (int x = 0; x < directions.Count; x++)
            {
                destinationTile = destinationTile.GetAdjencentTile(directions[x]);
                if (destinationTile == null) { break; }
            }

            //Return the "landing" tile
            return destinationTile;
        }
        #endregion

        #region Public Accessors
        public int ID { get { return (int)_CardImage.Tag; } }
        public Card CardInPlace { get { return _card; } }
        public bool IsOccupied { get { return _Occupied; } }
        public bool IsSummonTile { get { return _IsSummonTile; } }
        public PlayerColor Owner { get { return _Owner; } }
        public int CardID
        {
            get
            {
                if(_Occupied)
                {
                    return _card.CardID;
                }
                else
                {
                    return 0;
                }
            }
        }
        public Point Location { get { return _Border.Location; }}       
        #endregion

        #region Data
        private PictureBox _Border;
        private PictureBox _CardImage;
        private Label _StatsLabelATK;
        private Label _StatsLabelDEF;
        private bool _Occupied = false;
        private bool _IsSummonTile = false;
        private Card _card = null;
        private PlayerColor _Owner = PlayerColor.NONE;
        private Tile _NorthTile = null;
        private Tile _SouthTile = null;
        private Tile _EastTile = null;
        private Tile _WestTile = null;
        #endregion
    }

    public enum TileDirection 
    {
        North,
        South,
        East,
        West
    }
}
