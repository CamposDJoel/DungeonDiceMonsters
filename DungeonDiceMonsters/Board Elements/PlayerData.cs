//Joel campos
//9/12/2023
//PlayerData Class

namespace DungeonDiceMonsters
{
    public class PlayerData
    {
        public PlayerData(string playerName, Deck d)
        {
            _name = playerName;
            _deck = d;
        }

        public string Name { get{ return _name;} }
        public int LP { get{ return _lp;} }
        public Deck Deck { get { return _deck;} }

        public int Crests_MOV { get { return _MoveCrests; } }
        public int Crests_ATK { get { return _AttackCrests; } }
        public int Crests_DEF { get { return _DefenseCrests; } }
        public int Crests_MAG { get { return _MagicCrests; } }
        public int Crests_TRAP { get { return _TrapCrests; } }

        public void AddCrests(Crest c, int amount)
        {
            switch(c) 
            {
                case Crest.Movement: _MoveCrests+=amount; break;
                case Crest.Attack:   _AttackCrests += amount; break;
                case Crest.Defense:  _DefenseCrests += amount; break;
                case Crest.Magic:    _MagicCrests += amount; break;
                case Crest.Trap:     _TrapCrests += amount; break;
            }
        }
        public void RemoveCrests(Crest c, int amount)
        {
            switch (c)
            {
                case Crest.Movement: _MoveCrests -= amount; break;
                case Crest.Attack: _AttackCrests -= amount; break;
                case Crest.Defense: _DefenseCrests -= amount; break;
                case Crest.Magic: _MagicCrests -= amount; break;
                case Crest.Trap: _TrapCrests -= amount; break;
            }
        }
        public void ReduceLP(int amount)
        {
            _lp-= amount;
        }

        private string _name;
        private int _lp = 8000;
        private Deck _deck;
        private int _MoveCrests = 0;
        private int _AttackCrests = 0;
        private int _DefenseCrests = 0;
        private int _MagicCrests = 0;
        private int _TrapCrests = 0;
    }

    public enum Crest
    {
        Star,
        Movement,
        Attack,
        Defense,
        Magic,
        Trap,
        Ritual
    }
}
