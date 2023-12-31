﻿//Joel campos
//9/12/2023
//PlayerData Class

using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public class PlayerData
    {
        #region Constructors
        public PlayerData(string playerName, Deck d)
        {
            _name = playerName;
            _deck = d;
        }
        #endregion

        #region Public Accessors
        public string Name { get{ return _name;} }
        public Deck Deck { get { return _deck;} }
        public int FreeSummonTiles { get 
        {
                int count = 0;
                foreach(Tile tile in _SummoningTiles)
                {
                    if (!tile.IsOccupied) { count++; }
                }
                return count; } 
        }
        public int Crests_MOV { get { return _MoveCrests; } }
        public int Crests_ATK { get { return _AttackCrests; } }
        public int Crests_DEF { get { return _DefenseCrests; } }
        public int Crests_MAG { get { return _MagicCrests; } }
        public int Crests_TRAP { get { return _TrapCrests; } }
        #endregion

        #region Public Methods
        public void AddCrests(Crest c, int amount)
        {
            switch(c) 
            {
                case Crest.MOV: _MoveCrests+=amount; break;
                case Crest.ATK: _AttackCrests += amount; break;
                case Crest.DEF: _DefenseCrests += amount; break;
                case Crest.MAG: _MagicCrests += amount; break;
                case Crest.TRAP: _TrapCrests += amount; break;
            }
        }
        public void RemoveCrests(Crest c, int amount)
        {
            switch (c)
            {
                case Crest.MOV: _MoveCrests -= amount; break;
                case Crest.ATK: _AttackCrests -= amount; break;
                case Crest.DEF: _DefenseCrests -= amount; break;
                case Crest.MAG: _MagicCrests -= amount; break;
                case Crest.TRAP: _TrapCrests -= amount; break;
            }
        }
        public void AddSummoningTile(Tile tile)
        {
            _SummoningTiles.Add(tile);
        }
        public List<Tile> GetSetCardTileCandidates()
        {
            List<Tile> candidates = new List<Tile>();

            foreach (Tile tile in _SummoningTiles) 
            {
                if (!tile.IsOccupied) {  candidates.Add(tile); }
            }

            return candidates;
        }
        #endregion

        #region Data
        private string _name;
        private Deck _deck;
        private int _MoveCrests = 0;
        private int _AttackCrests = 0;
        private int _DefenseCrests = 0;
        private int _MagicCrests = 0;
        private int _TrapCrests = 0;
        private List<Tile> _SummoningTiles = new List<Tile>();
        #endregion
    }
}
