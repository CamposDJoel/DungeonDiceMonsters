//Joel Campos
//9/11/2023
//TileClass

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public class Tile
    {
        #region Constructors
        public Tile(Panel inside, PictureBox border, PictureBox overlayIcon, Label statsLabel, Label statsLabel3)
        {
            _Border = border;
            _CardImage = inside;
            _OverlayIcon = overlayIcon;
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
            if (_IsSpellTrapZone)
            {
                if(!IsOccupied)
                {
                    _CardImage.BackgroundImage = ImageServer.SpellTrapZone();
                }                
            }

            //Hide the overlay icon (this should only be visible in specific situations)
            _OverlayIcon.Visible = false;


            //_InsideBox image and _Stats
            //Set the Card Image of the Card in the Tile, if this tile is occupied, otherwise Card Image will be null unless it has
            //a field type other than "None", in which case it will load the field image.
            //Card Image backcolor will be 
            //Sets the stats visible if the card is a face up card (monster)
            if (_Occupied)
            {
                //If the Card is a symbol load the Symbol card artwork
                if (_card.IsASymbol)
                {
                    _CardImage.BackgroundImage = ImageServer.Symbol(_card.CurrentAttribute.ToString());
                }
                else
                {
                    //if the Card is a Monster (Faceup) load the monster artwork and stats
                    if (_card.Category == Category.Monster)
                    {
                        _CardImage.BackgroundImage = ImageServer.CardArtworkImage(_card.CardID.ToString());

                        _StatsLabelATK.Text = _card.ATK.ToString();
                        _StatsLabelDEF.Text = _card.DEF.ToString();
                        _StatsLabelATK.ForeColor = _card.GetATKStatus();
                        _StatsLabelDEF.ForeColor = _card.GetDEFStatus();
                        _StatsLabelATK.Visible = true;
                        _StatsLabelDEF.Visible = true;
                    }
                    else
                    {
                        //The card will be a Spell/Trap
                        if(_card.IsFaceDown)
                        {
                            _CardImage.BackgroundImage = ImageServer.FaceDownSetCard();
                        }
                        else
                        {
                            _CardImage.BackgroundImage = ImageServer.CardArtworkImage(_card.CardID.ToString());
                        }
                        
                        _StatsLabelATK.Visible = false;
                        _StatsLabelDEF.Visible = false;
                    }
                }
            }
            else
            {
                if (!_IsSpellTrapZone) { ImageServer.RemoveImageFromPanel(_CardImage); }
                
                //if the Fiel Type value is set, load its image
                if(_Owner != PlayerColor.NONE && _FieldType != FieldTypeValue.None && !_IsSpellTrapZone)
                {
                    _CardImage.BackgroundImage = ImageServer.FieldTile(_FieldType.ToString());
                }

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
            if(_Occupied)
            {
                throw new Exception("Cannot summon on an already occupied tile");
            }
            else
            {
                _card = card;
                _Occupied = true;
                _IsSpellTrapZone = true;
                _card.SetCurrentTile(this);
                ReloadTileUI();
            }          
        }
        public void NonDimensionSummon(Card card)
        {
            if (_Occupied)
            {
                throw new Exception("Cannot summon on an already occupied tile");
            }
            else
            {
                _card = card;
                _Occupied = true;
                _card.SetCurrentTile(this);
                ReloadTileUI();
            }
        }
        public void SetCard(Card card)
        {
            if (_Occupied)
            {
                throw new Exception("Cannot set on an already occupied tile");
            }
            else
            {
                _card = card;
                _Occupied = true;
                _IsSpellTrapZone = false;
                _card.SetCurrentTile(this);
                ReloadTileUI();
            }            
        }
        public void MoveInCard(Card card)
        {
            _card = card;
            _Occupied = true;
            _card.SetCurrentTile(this);
            _card.UpdateFieldTypeBonus();
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
            _card.SetCurrentTile(null);
            _card = null;
            _Occupied = false;            
            ReloadTileUI();
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
        public void MarkMoveTarget()
        {
            _CardImage.BackColor = Color.Green;
            //In case the tile has a field type set, remove the card image temparely
            if (_CardImage.BackgroundImage != null) { _CardImage.BackgroundImage.Dispose(); _CardImage.BackgroundImage = null; }
        }
        public void MarkAttackTarget()
        {
            //Place the overlay icon of the attack target
            _OverlayIcon.Image = ImageServer.AttackTargetIcon();
            _OverlayIcon.Visible = true;
        }
        public void HighlightAttackRange()
        {
            _Border.BackColor = Color.Orange;
        }
        public void MarkEffectTarget()
        {
            _Border.BackColor = Color.Green;
            //Place the overlay icon of the attack target
            _OverlayIcon.Image = ImageServer.EffectTargetIcon();
            _OverlayIcon.Visible = true;
        }
        public void MarkSetTarget()
        {
            _CardImage.BackColor = Color.Green;
        }
        public void MarkFusionMaterialTarget()
        {
            _Border.BackColor = Color.Green;
            //Place the overlay icon of the attack target
            _OverlayIcon.Image = ImageServer.FusionMaterialTarget();
            _OverlayIcon.Visible = true;
        }
        public void MarkFusionSummonTile()
        {
            _CardImage.BackColor = Color.Green;
            //In case the tile has a field type set, remove the card image temparely
            if (_CardImage.BackgroundImage != null) { _CardImage.BackgroundImage.Dispose(); _CardImage.BackgroundImage = null; }
        }
        public void HighlightTile()
        {
            _Border.BackColor = Color.Yellow;
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
        public void ChangeFieldType(FieldTypeValue field)
        {
            _FieldType = field;
            ReloadTileUI();
            //After the UI has been updated, if the tile is occupied by a monster
            //check if the monster can receive a field type bonus
            if(_Occupied)
            {
                if(_card.Category == Category.Monster) 
                {
                    _card.UpdateFieldTypeBonus();
                }
            }
        }
        public List<Tile> GetFieldSpellActivationTiles(bool returnCandidatesOnly)
        {
            List<Tile> Alltiles = new List<Tile>();
            List<Tile> tiles = new List<Tile>();
            //Tile (A1)
            Tile tileA1 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.West, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileA1);
            if (tileA1 != null) { if(tileA1.Owner != PlayerColor.NONE) tiles.Add(tileA1); }
            //Tile (A2)
            Tile tileA2 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.West, TileDirection.West});
            Alltiles.Add(tileA2);
            if (tileA2 != null) { if (tileA2.Owner != PlayerColor.NONE) tiles.Add(tileA2); }
            //Tile (A3)
            Tile tileA3 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.West });
            Alltiles.Add(tileA3);
            if (tileA3 != null) { if (tileA3.Owner != PlayerColor.NONE) tiles.Add(tileA3); }
            //Tile (A4)
            Tile tileA4 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North });
            Alltiles.Add(tileA4);
            if (tileA4 != null) { if (tileA4.Owner != PlayerColor.NONE) tiles.Add(tileA4); }
            //Tile (A5)
            Tile tileA5 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.East });
            Alltiles.Add(tileA5);
            if (tileA5 != null) { if (tileA5.Owner != PlayerColor.NONE) tiles.Add(tileA5); }
            //Tile (A6)
            Tile tileA6 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileA6);
            if (tileA6 != null) { if (tileA6.Owner != PlayerColor.NONE) tiles.Add(tileA6); }
            //Tile (A7)
            Tile tileA7 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.North, TileDirection.East, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileA7);
            if (tileA7 != null) { if (tileA7.Owner != PlayerColor.NONE) tiles.Add(tileA7); }

            //Tile (B1)
            Tile tileB1 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.West, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileB1);
            if (tileB1 != null) { if (tileB1.Owner != PlayerColor.NONE) tiles.Add(tileB1); }
            //Tile (B2)
            Tile tileB2 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileB2);
            if (tileB2 != null) { if (tileB2.Owner != PlayerColor.NONE) tiles.Add(tileB2); }
            //Tile (B3)
            Tile tileB3 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.West });
            Alltiles.Add(tileB3);
            if (tileB3 != null) { if (tileB3.Owner != PlayerColor.NONE) tiles.Add(tileB3); }
            //Tile (B4)
            Tile tileB4 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North });
            Alltiles.Add(tileB4);
            if (tileB4 != null) { if (tileB4.Owner != PlayerColor.NONE) tiles.Add(tileB4); }
            //Tile (B5)
            Tile tileB5 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.East });
            Alltiles.Add(tileB5);
            if (tileB5 != null) { if (tileB5.Owner != PlayerColor.NONE) tiles.Add(tileB5); }
            //Tile (B6)
            Tile tileB6 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileB6);
            if (tileB6 != null) { if (tileB6.Owner != PlayerColor.NONE) tiles.Add(tileB6); }
            //Tile (B7)
            Tile tileB7 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.North, TileDirection.East, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileB7);
            if (tileB7 != null) { if (tileB7.Owner != PlayerColor.NONE) tiles.Add(tileB7); }

            //Tile (C1)
            Tile tileC1 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.West, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileC1);
            if (tileC1 != null) { if (tileC1.Owner != PlayerColor.NONE) tiles.Add(tileC1); }
            //Tile (C2)
            Tile tileC2 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileC2);
            if (tileC2 != null) { if (tileC2.Owner != PlayerColor.NONE) tiles.Add(tileC2); }
            //Tile (C3)
            Tile tileC3 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.West });
            Alltiles.Add(tileC3);
            if (tileC3 != null) { if (tileC3.Owner != PlayerColor.NONE) tiles.Add(tileC3); }
            //Tile (C4)
            Tile tileC4 = GetTileInDirection(new List<TileDirection> { TileDirection.North });
            Alltiles.Add(tileC4);
            if (tileC4 != null) { if (tileC4.Owner != PlayerColor.NONE) tiles.Add(tileC4); }
            //Tile (C5)
            Tile tileC5 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.East });
            Alltiles.Add(tileC5);
            if (tileC5 != null) { if (tileC5.Owner != PlayerColor.NONE) tiles.Add(tileC5); }
            //Tile (C6)
            Tile tileC6 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileC6);
            if (tileC6 != null) { if (tileC6.Owner != PlayerColor.NONE) tiles.Add(tileC6); }
            //Tile (C7)
            Tile tileC7 = GetTileInDirection(new List<TileDirection> { TileDirection.North, TileDirection.East, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileC7);
            if (tileC7 != null) { if (tileC7.Owner != PlayerColor.NONE) tiles.Add(tileC7); }

            //Tile (D1)
            Tile tileD1 = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileD1);
            if (tileD1 != null) { if (tileD1.Owner != PlayerColor.NONE) tiles.Add(tileD1); }
            //Tile (D2)
            Tile tileD2 = GetTileInDirection(new List<TileDirection> { TileDirection.West, TileDirection.West });
            Alltiles.Add(tileD2);
            if (tileD2 != null) { if (tileD2.Owner != PlayerColor.NONE) tiles.Add(tileD2); }
            //Tile (D3)
            Tile tileD3 = GetTileInDirection(new List<TileDirection> { TileDirection.West });
            Alltiles.Add(tileD3);
            if (tileD3 != null) { if (tileD3.Owner != PlayerColor.NONE) tiles.Add(tileD3); }
            //Tile (D4)
            Tile tileD4 = this;
            Alltiles.Add(this);
            tiles.Add(tileD4);
            //Tile (D5)
            Tile tileD5 = GetTileInDirection(new List<TileDirection> { TileDirection.East });
            Alltiles.Add(tileD5);
            if (tileD5 != null) { if (tileD5.Owner != PlayerColor.NONE) tiles.Add(tileD5); }
            //Tile (D6)
            Tile tileD6 = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East });
            Alltiles.Add(tileD6);
            if (tileD6 != null) { if (tileD6.Owner != PlayerColor.NONE) tiles.Add(tileD6); }
            //Tile (D7)
            Tile tileD7 = GetTileInDirection(new List<TileDirection> { TileDirection.East, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileD7);
            if (tileD7 != null) { if (tileD7.Owner != PlayerColor.NONE) tiles.Add(tileD7); }

            //Tile (E1)
            Tile tileE1 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.West, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileE1);
            if (tileE1 != null) { if (tileE1.Owner != PlayerColor.NONE) tiles.Add(tileE1); }
            //Tile (E2)
            Tile tileE2 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileE2);
            if (tileE2 != null) { if (tileE2.Owner != PlayerColor.NONE) tiles.Add(tileE2); }
            //Tile (E3)
            Tile tileE3 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.West });
            Alltiles.Add(tileE3);
            if (tileE3 != null) { if (tileE3.Owner != PlayerColor.NONE) tiles.Add(tileE3); }
            //Tile (E4)
            Tile tileE4 = GetTileInDirection(new List<TileDirection> { TileDirection.South });
            Alltiles.Add(tileE4);
            if (tileE4 != null) { if (tileE4.Owner != PlayerColor.NONE) tiles.Add(tileE4); }
            //Tile (E5)
            Tile tileE5 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.East });
            Alltiles.Add(tileE5);
            if (tileE5 != null) { if (tileE5.Owner != PlayerColor.NONE) tiles.Add(tileE5); }
            //Tile (E6)
            Tile tileE6 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileE6);
            if (tileE6 != null) { if (tileE6.Owner != PlayerColor.NONE) tiles.Add(tileE6); }
            //Tile (E7)
            Tile tileE7 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.East, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileE7);
            if (tileE7 != null) { if (tileE7.Owner != PlayerColor.NONE) tiles.Add(tileE7); }
            Alltiles.Add(tileE7);

            //Tile (F1)
            Tile tileF1 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.West, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileF1);
            if (tileF1 != null) { if (tileF1.Owner != PlayerColor.NONE) tiles.Add(tileF1); }
            //Tile (F2)
            Tile tileF2 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileF2);
            if (tileF2 != null) { if (tileF2.Owner != PlayerColor.NONE) tiles.Add(tileF2); }
            //Tile (F3)
            Tile tileF3 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.West });
            Alltiles.Add(tileF3);
            if (tileF3 != null) { if (tileF3.Owner != PlayerColor.NONE) tiles.Add(tileF3); }
            //Tile (F4)
            Tile tileF4 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South });
            Alltiles.Add(tileF4);
            if (tileF4 != null) { if (tileF4.Owner != PlayerColor.NONE) tiles.Add(tileF4); }
            //Tile (F5)
            Tile tileF5 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.East });
            Alltiles.Add(tileF5);
            if (tileF5 != null) { if (tileF5.Owner != PlayerColor.NONE) tiles.Add(tileF5); }
            //Tile (F6)
            Tile tileF6 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileF6);
            if (tileF6 != null) { if (tileF6.Owner != PlayerColor.NONE) tiles.Add(tileF6); }
            //Tile (F7)
            Tile tileF7 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.East, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileF7);
            if (tileF7 != null) { if (tileF7.Owner != PlayerColor.NONE) tiles.Add(tileF7); }

            //Tile (G1)
            Tile tileG1 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.West, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileG1);
            if (tileG1 != null) { if (tileG1.Owner != PlayerColor.NONE) tiles.Add(tileG1); }
            //Tile (G2)
            Tile tileG2 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.West, TileDirection.West });
            Alltiles.Add(tileG2);
            if (tileG2 != null) { if (tileG2.Owner != PlayerColor.NONE) tiles.Add(tileG2); }
            //Tile (G3)
            Tile tileG3 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.West });
            Alltiles.Add(tileG3);
            if (tileG3 != null) { if (tileG3.Owner != PlayerColor.NONE) tiles.Add(tileG3); }
            //Tile (G4)
            Tile tileG4 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South });
            Alltiles.Add(tileG4);
            if (tileG4 != null) { if (tileG4.Owner != PlayerColor.NONE) tiles.Add(tileG4); }
            //Tile (G5)
            Tile tileG5 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.East });
            Alltiles.Add(tileG5);
            if (tileG5 != null) { if (tileG5.Owner != PlayerColor.NONE) tiles.Add(tileG5); }
            //Tile (G6)
            Tile tileG6 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileG6);
            if (tileG6 != null) { if (tileG6.Owner != PlayerColor.NONE) tiles.Add(tileG6); }
            //Tile (G7)
            Tile tileG7 = GetTileInDirection(new List<TileDirection> { TileDirection.South, TileDirection.South, TileDirection.South, TileDirection.East, TileDirection.East, TileDirection.East });
            Alltiles.Add(tileG7);
            if (tileG7 != null) { if (tileG7.Owner != PlayerColor.NONE) tiles.Add(tileG7); }

            if (returnCandidatesOnly) { return tiles; } else { return Alltiles; }
        }
        public List<Tile> GetAttackRangeTiles(bool returnCandidatesOnly)
        {
            List<Tile> tiles = new List<Tile>();

            //Set the attack range to use
            int attackRange = _card.AttackRange;

            //Gather the north tiles
            for (int i = 0; i < attackRange; i++)
            {
                List<TileDirection> northDirections = new List<TileDirection>();
                for (int j = 0; j <= i; j++)
                {
                    northDirections.Add(TileDirection.North);
                }
                Tile thisNorthTile = GetTileInDirection(northDirections);
                if (thisNorthTile == null)
                {
                    break;
                }
                else
                {
                    if (thisNorthTile.IsActive)
                    {
                        if (returnCandidatesOnly)
                        {
                            if (thisNorthTile.IsOccupied)
                            {
                                if (thisNorthTile.CardInPlace.Controller != _card.Controller)
                                {
                                    tiles.Add(thisNorthTile);
                                }

                            }
                        }
                        else
                        {
                            tiles.Add(thisNorthTile);
                        }

                        //If this tile was occupied break the loop
                        if (thisNorthTile.IsOccupied)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //Gather the south tiles
            for (int i = 0; i < attackRange; i++)
            {
                List<TileDirection> southDirections = new List<TileDirection>();
                for (int j = 0; j <= i; j++)
                {
                    southDirections.Add(TileDirection.South);
                }
                Tile thisSouthTile = GetTileInDirection(southDirections);
                if (thisSouthTile == null)
                {
                    break;
                }
                else
                {
                    if (thisSouthTile.IsActive)
                    {
                        if (returnCandidatesOnly)
                        {
                            if (thisSouthTile.IsOccupied)
                            {
                                if (thisSouthTile.CardInPlace.Controller != _card.Controller)
                                {
                                    tiles.Add(thisSouthTile);
                                }
                            }
                        }
                        else
                        {
                            tiles.Add(thisSouthTile);
                        }

                        //If this tile was occupied break the loop
                        if (thisSouthTile.IsOccupied)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //Gather the east tiles
            for (int i = 0; i < attackRange; i++)
            {
                List<TileDirection> eastDirections = new List<TileDirection>();
                for (int j = 0; j <= i; j++)
                {
                    eastDirections.Add(TileDirection.East);
                }
                Tile thisEastTile = GetTileInDirection(eastDirections);
                if (thisEastTile == null)
                {
                    break;
                }
                else
                {
                    if (thisEastTile.IsActive)
                    {
                        if (returnCandidatesOnly)
                        {
                            if (thisEastTile.IsOccupied)
                            {
                                if (thisEastTile.CardInPlace.Controller != _card.Controller)
                                {
                                    tiles.Add(thisEastTile);
                                }
                            }
                        }
                        else
                        {
                            tiles.Add(thisEastTile);
                        }

                        //If this tile was occupied break the loop
                        if (thisEastTile.IsOccupied)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //Gather the west tiles
            for (int i = 0; i < attackRange; i++)
            {
                List<TileDirection> westDirections = new List<TileDirection>();
                for (int j = 0; j <= i; j++)
                {
                    westDirections.Add(TileDirection.West);
                }
                Tile thisWestTile = GetTileInDirection(westDirections);
                if (thisWestTile == null)
                {
                    break;
                }
                else
                {
                    if (thisWestTile.IsActive)
                    {
                        if (returnCandidatesOnly)
                        {
                            if (thisWestTile.IsOccupied)
                            {
                                if (thisWestTile.CardInPlace.Controller != _card.Controller)
                                {
                                    tiles.Add(thisWestTile);
                                }
                            }
                        }
                        else
                        {
                            tiles.Add(thisWestTile);
                        }

                        //If this tile was occupied break the loop
                        if (thisWestTile.IsOccupied)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return tiles;
        }
        public List<Tile> GetMoveRangeTiles()
        {
            List<Tile> tiles = new List<Tile>();

            //Set the attack range to use
            int moveRange = _card.MoveRange;

            //Gather the north tiles
            for (int i = 0; i < moveRange; i++)
            {
                List<TileDirection> northDirections = new List<TileDirection>();
                for (int j = 0; j <= i; j++)
                {
                    northDirections.Add(TileDirection.North);
                }
                Tile thisNorthTile = GetTileInDirection(northDirections);
                if (thisNorthTile == null)
                {
                    break;
                }
                else
                {
                    if (thisNorthTile.IsActive)
                    {
                        if (thisNorthTile.IsOccupied)
                        {
                            break;
                        }
                        else
                        {
                            tiles.Add(thisNorthTile);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //Gather the south tiles
            for (int i = 0; i < moveRange; i++)
            {
                List<TileDirection> southDirections = new List<TileDirection>();
                for (int j = 0; j <= i; j++)
                {
                    southDirections.Add(TileDirection.South);
                }
                Tile thisSouthTile = GetTileInDirection(southDirections);
                if (thisSouthTile == null)
                {
                    break;
                }
                else
                {
                    if (thisSouthTile.IsActive)
                    {
                        if (thisSouthTile.IsOccupied)
                        {
                            break;
                        }
                        else
                        {
                            tiles.Add(thisSouthTile);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //Gather the east tiles
            for (int i = 0; i < moveRange; i++)
            {
                List<TileDirection> eastDirections = new List<TileDirection>();
                for (int j = 0; j <= i; j++)
                {
                    eastDirections.Add(TileDirection.East);
                }
                Tile thisEastTile = GetTileInDirection(eastDirections);
                if (thisEastTile == null)
                {
                    break;
                }
                else
                {
                    if (thisEastTile.IsActive)
                    {
                        if (thisEastTile.IsOccupied)
                        {
                            break;
                        }
                        else
                        {
                            tiles.Add(thisEastTile);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //Gather the west tiles
            for (int i = 0; i < moveRange; i++)
            {
                List<TileDirection> westDirections = new List<TileDirection>();
                for (int j = 0; j <= i; j++)
                {
                    westDirections.Add(TileDirection.West);
                }
                Tile thisWestTile = GetTileInDirection(westDirections);
                if (thisWestTile == null)
                {
                    break;
                }
                else
                {
                    if (thisWestTile.IsActive)
                    {
                        if (thisWestTile.IsOccupied)
                        {
                            break;
                        }
                        else
                        {
                            tiles.Add(thisWestTile);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
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
        public bool IsActive { get { return Owner != PlayerColor.NONE; } }
        public bool IsSpellTrapZone { get { return _IsSpellTrapZone; } }
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
        public FieldTypeValue FieldType { get { return _FieldType; } }
        #endregion

        #region Data
        private PictureBox _Border;
        private Panel _CardImage;
        private PictureBox _OverlayIcon;
        private Label _StatsLabelATK;
        private Label _StatsLabelDEF;
        private bool _Occupied = false;
        private bool _IsSpellTrapZone = false;
        private Card _card = null;
        private PlayerColor _Owner = PlayerColor.NONE;
        private Tile _NorthTile = null;
        private Tile _SouthTile = null;
        private Tile _EastTile = null;
        private Tile _WestTile = null;
        private FieldTypeValue _FieldType = FieldTypeValue.None;
        #endregion

        #region Enums
        public enum TileDirection
        {
            North,
            South,
            East,
            West
        }
        public enum FieldTypeValue
        {
            None,
            Forest,
            Mountain,
            Sogen,
            Umi,
            Wasteland,
            Yami,
            Swamp,
            Volcano,
            Sanctuary,
            Cyberworld,
            Scrapyard
        }  
        #endregion
    }   
}