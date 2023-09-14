//Joel Campos
//9/11/2023
//TileClass

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public class Tile
    {
        public Tile(PictureBox inside, PictureBox border, Label statsLabel)
        {
            _Border = border;
            _CardImage = inside;
            _StatsLabel = statsLabel;
        }

        //Public Methods
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
        public void Leave()
        {
            _Border.BackColor = Color.Transparent;
            SetTileColor();
        }
        public void ChangeOwner(PlayerOwner owner)
        {
            _Owner = owner;
            SetTileColor();
        }
        public void SummonCard(Card card)
        {
            _card = card;
            _Occupied = true;
            _IsSummonTile = true;
            _CardImage.Image = ImageServer.CardArtworkImage(card.CardID);
            if(card.Category == "Monster")
            {
                _StatsLabel.Text = _card.ATK + "/" + _card.DEF;
                _StatsLabel.BringToFront();
            }
            else
            {
                _StatsLabel.SendToBack();
            }
        }
        public void MoveInCard(Card card)
        {
            _card = card;
            _Occupied = true;
            _CardImage.Image = ImageServer.CardArtworkImage(card.CardID);
            if (card.Category == "Monster")
            {
                _StatsLabel.Text = _card.ATK + "/" + _card.DEF;
                _StatsLabel.BringToFront();
            }
            else
            {
                _StatsLabel.SendToBack();
            }
        }
        public void RemoveCard()
        {
            _card = null;
            _Occupied = false;
            _CardImage.Image.Dispose();
            _CardImage.Image = null;
            _StatsLabel.SendToBack();
        }
        public void DestroyCard()
        {
            _card.Discard();
            _card = null;
            _Occupied = false;
            _CardImage.Image.Dispose();
            _CardImage.Image = null;
            _StatsLabel.Visible = false;
        }
        public List<Tile> GetAttackTargerCandidates(PlayerOwner enemy)
        {
            List<Tile> candidates = new List<Tile>();

            if (HasAnAdjecentEnemyCard(TileDirection.North, enemy)){ candidates.Add(_NorthTile); }
            if (HasAnAdjecentEnemyCard(TileDirection.South, enemy)){ candidates.Add(_SouthTile); }
            if (HasAnAdjecentEnemyCard(TileDirection.East, enemy)){ candidates.Add(_EastTile); }
            if (HasAnAdjecentEnemyCard(TileDirection.West, enemy)){ candidates.Add(_WestTile); }

            return candidates;
        }
        private bool HasAnAdjecentEnemyCard(TileDirection direction, PlayerOwner enemy)
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
                        if (_SouthTile.IsOccupied)
                        {
                            if (_SouthTile.CardInPlace.Owner == enemy) { hasIt = true; }
                        }
                    break;
                case TileDirection.East:
                    if (_EastTile.IsOccupied)
                    {
                        if (_EastTile.CardInPlace.Owner == enemy) { hasIt = true; }
                    }
                    break;
                case TileDirection.West:
                    if (_WestTile.IsOccupied)
                    {
                        if (_WestTile.CardInPlace.Owner == enemy) { hasIt = true; }
                    }
                    break;
            }

            return hasIt;
        }

        //Private Methods
        public void SetTileColor()
        {
            switch (_Owner)
            {
                case PlayerOwner.None: _CardImage.BackColor = Color.Transparent; break;
                case PlayerOwner.Red: _CardImage.BackColor = Color.DarkRed; break;
                case PlayerOwner.Blue: _CardImage.BackColor = Color.DarkBlue; break;
            }

            if( _IsSummonTile ) 
            {
                _CardImage.BackColor = Color.Orange;
            }

            _Border.BackColor = Color.Transparent;
        }
        public void MarkFreeToMove()
        {
            _CardImage.BackColor = Color.Green;            
        }
        public void MarkAttackTarget()
        {
            _Border.BackColor = Color.Chartreuse;
        }

        //Accessors
        public int ID { get { return (int)_CardImage.Tag; } }
        public Card CardInPlace { get { return _card; } }
        public bool IsOccupied { get { return _Occupied; } }
        public bool IsSummonTile { get { return _IsSummonTile; } }
        public PlayerOwner Owner { get { return _Owner; } }
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

        //Private data
        private PictureBox _Border;
        private PictureBox _CardImage;
        private Label _StatsLabel;
        private bool _Occupied = false;
        private bool _IsSummonTile = false;
        private Card _card = null;
        private PlayerOwner _Owner = PlayerOwner.None;
        private Tile _NorthTile = null;
        private Tile _SouthTile = null;
        private Tile _EastTile = null;
        private Tile _WestTile = null;
    }

    public enum PlayerOwner
    {
        None,
        Red,
        Blue
    }
    public enum TileDirection 
    {
        North,
        South,
        East,
        West
    }
}
