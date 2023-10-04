﻿//Joel Campos
//9/12/2023
//Card Class

namespace DungeonDiceMonsters
{
    public class Card
    {
        #region Constructors
        public Card(int id, CardInfo info, PlayerOwner owner, bool isFaceDown)
        {
            _id = id;
            _cardInfo = info;
            _Owner = owner;
            _CurrentLP = _cardInfo.LP;
            _IsFaceDown = isFaceDown;
        }
        public Card(int id, Attribute attribute, PlayerOwner owner)
        {
            _id = id;
            _cardInfo = new CardInfo(attribute);
            _Owner = owner;
            _CurrentLP = _cardInfo.LP;
            _IsASymbol = true;
        }
        #endregion

        #region Base Card Info
        public int CardID { get { return _cardInfo.ID; } }
        public string Name { get { return _cardInfo.Name; } }
        public int Level { get { return _cardInfo.Level; } }
        public Type Type { get { return _cardInfo.Type; } }
        public SecType SecType { get { return _cardInfo.SecType; } }
        public int ATK { get { return _cardInfo.ATK + _AttackBonus; } }
        public int DEF { get { return _cardInfo.DEF + _DefenseBonus; } }
        public int LP { get { return _CurrentLP; } }
        public Attribute Attribute { get { return _cardInfo.Attribute; } }
        public Category Category { get { return _cardInfo.Category; } }
        //fusion Materials
        public string FusionMaterial1 { get { return _cardInfo.FusionMaterial1; } }
        public string FusionMaterial2 { get { return _cardInfo.FusionMaterial2; } }
        public string FusionMaterial3 { get { return _cardInfo.FusionMaterial3; } }
        //Ritual Card
        public string RitualCard { get { return _cardInfo.RitualCard; } }
        //EFFECTS
        public bool HasOnSummonEffect { get { return _cardInfo.HasOnSummonEffect; } }
        public bool HasContinuousEffect { get { return _cardInfo.HasContinuousEffect; } }
        public bool HasIgnitionEffect { get { return _cardInfo.HasIgnitionEffect; } }
        public bool HasAbility { get { return _cardInfo.HasAbility; } }
        public string OnSummonEffect { get { return _cardInfo.OnSummonEffect; } }
        public string ContinuousEffect { get { return _cardInfo.ContinuousEffect; } }
        public string IgnitionEffect { get { return _cardInfo.IgnitionEffect; } }
        public string Ability { get { return _cardInfo.Ability; } }
        #endregion

        #region On Board Data
        public int OnBoardID { get { return _id; } }       
        public PlayerOwner Owner { get { return _Owner; } }
        public bool IsFaceDown { get { return _IsFaceDown; } }
        public bool IsASymbol { get { return _IsASymbol; } }
        public int MoveCost { get { return _MoveCost; } }
        public int AttackCost { get { return _AttackCost; } }
        public int DefenseCost { get { return _DefenseCost; } }
        #endregion

        #region On Board Counters and Flags
        public bool MovedThisTurn{get { return _MovedThisTurn; } set { _MovedThisTurn = value; } }
        public bool AttackedThisTurn{get { return _AttackedThisTurn; } set { _AttackedThisTurn = value; } }
        #endregion

        #region Public Funtions
        public void ReduceLP(int amount)
        {
            _CurrentLP -= amount;
        }
        public void Discard()
        {
            _IsDiscardted = true;
        }
        #endregion

        #region Data
        //Card Board Data
        private int _id = -1;
        private CardInfo _cardInfo;
        private PlayerOwner _Owner;
        private bool _IsDiscardted = false;
        private bool _IsFaceDown = false;
        private bool _IsASymbol = false;

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
        #endregion
    }
}
