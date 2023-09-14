//Joel Campos
//9/12/2023
//Card Class

namespace DungeonDiceMonsters
{
    public class Card
    {
        public Card(int id, CardInfo info, PlayerOwner owner, bool isFaceDown)
        {
            _id = id;
            _cardInfo = info;
            _Owner = owner;
            _CurrentLP = _cardInfo.LP;
            _IsFaceDown = isFaceDown;
        }

        public int OnBoardID { get { return _id; } }
        public int CardID { get { return _cardInfo.ID; } }
        public string Name { get { return _cardInfo.Name; } }
        public CardInfo Info { get { return _cardInfo; } }
        public PlayerOwner Owner { get { return _Owner; } }
        public bool MovedThisTurn{get { return _MovedThisTurn; } set { _MovedThisTurn = value; } }
        public bool AttackedThisTurn{get { return _AttackedThisTurn; } set { _AttackedThisTurn = value; } }
        public int ATK { get { return _cardInfo.ATK + _AttackBonus; } }
        public int DEF { get { return _cardInfo.DEF + _DefenseBonus; } }
        public int LP { get { return _CurrentLP; } }
        public string Attribute { get { return _cardInfo.Attribute; } }
        public int MoveCost { get { return _MoveCost; } }
        public int AttackCost { get { return _AttackCost; } }
        public int DefenseCost { get { return _DefenseCost; } }
        public string Category { get { return _cardInfo.Category; } }
        public bool IsFaceDown { get { return _IsFaceDown; } }

        public void ReduceLP(int amount)
        {
            _CurrentLP -= amount;
        }
        public void Discard()
        {
            _IsDiscardted = true;
        }

        
        //Card Board Data
        private int _id = -1;
        private CardInfo _cardInfo;
        private PlayerOwner _Owner;
        private bool _IsDiscardted = false;
        private bool _IsFaceDown = false;

        //Card Stats Data
        private int _CurrentLP;
        private int _AttackBonus = 0;
        private int _DefenseBonus = 0;

        private int _MoveCost = 1;
        private int _AttackCost = 1;
        private int _DefenseCost = 1;

        //Action Flags
        private bool _MovedThisTurn = false;
        private bool _AttackedThisTurn = false;
    }
}
