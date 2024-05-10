//Joel Campos
//9/12/2023
//Card Class

using System.Drawing;

namespace DungeonDiceMonsters
{
    public class Card
    {
        #region Constructors
        public Card(int id, CardInfo info, PlayerColor owner, bool isFaceDown)
        {
            _id = id;
            _cardInfo = info;
            _Owner = owner;
            _CurrentLP = _cardInfo.LP;
            _IsFaceDown = isFaceDown;
            if(HasAbility)
            {
                ApplyAbility();
            }

            void ApplyAbility()
            {
                switch (_cardInfo.Ability)
                {
                    case "Cannot be Target": _CanBeTarget = false; break;
                    case "Move Cost: 2": _BaseMoveCost = 2; break;
                    case "Move Cost: 3": _BaseMoveCost = 3; break;
                    case "Attack Cost: 2": _BaseAttackCost = 2; break;
                    case "Attack Cost: 3": _BaseAttackCost = 3; break;
                    case "Defense Cost: 2": _BaseDefenseCost = 2; break;
                    case "Defense Cost: 3": _BaseDefenseCost = 3; break;
                    case "Attacks per Turn: 2": _BaseAttacksPerTurn = 2; break;
                    case "Attacks per Turn: 3": _BaseAttacksPerTurn = 3; break;
                    case "Moves per Turn: 2": _BaseMovesPerTurn = 2; break;
                    case "Moves per Turn: 3": _BaseMovesPerTurn = 3; break;
                }
                ResetOneTurnData();
            }
        }
        public Card(int id, Attribute attribute, PlayerColor owner)
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
        public string TypeAsString { get { return _cardInfo.TypeAsString; } }
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
        public bool EffectsAreImplemented { get { return _cardInfo.EffectsAreImplemented; } }
        #endregion

        #region On Board Data
        public int OnBoardID { get { return _id; } }       
        public PlayerColor Owner { get { return _Owner; } }
        public bool IsFaceDown { get { return _IsFaceDown; } }
        public bool IsDiscardted { get { return _IsDestroyed; } }
        public bool IsASymbol { get { return _IsASymbol; } }
        public bool IsUnderSpellbound { get { return _IsUnderSpellbound; } }
        public bool IsPermanentSpellbound { get { return _IsPemanentSpellbound; } }
        public int SpellboundCounter { get { return _SpellboundCounter; } }
        public int TurnCounters { get { return _TurnCounters; } }
        public int Counters { get { return _Counters; } }
        #endregion

        #region On Board Counters and Flags
        public bool CanBeTarget { get { return _CanBeTarget; } }
        public int MoveCost { get { return _MoveCost; } }
        public int AttackCost { get { return _AttackCost; } }
        public int DefenseCost { get { return _DefenseCost; } }
        public int MovesAvaiable { get { return _MovesAvailable; } }
        public int AttacksAvaiable { get { return _AttacksAvailable; } }
        public int AttacksPerTurn { get { return _BaseAttacksPerTurn; } }
        public int MovesPerTurn { get { return _BaseMovesPerTurn; } }
        #endregion

        #region Public Funtions
        public void ReduceLP(int amount)
        {
            _CurrentLP -= amount;
        }
        public void Destroy()
        {
            _IsDestroyed = true;
        }
        public void RemoveAttackCounter()
        {
            _AttacksAvailable--;
        }
        public void RemoveMoveCounter()
        {
            _MovesAvailable--;
        }
        public void ResetOneTurnData()
        {
            _MoveCost = _BaseMoveCost;
            _AttackCost = _BaseAttackCost;
            _DefenseCost = _BaseDefenseCost;
            _MovesAvailable = _BaseMovesPerTurn;
            _AttacksAvailable = _BaseAttacksPerTurn;
        }
        public void ReduceSpellboundCounter(int amount)
        {
            _SpellboundCounter -= amount;

            if(_SpellboundCounter <= 0) 
            {
                //Remove the spellbound
                _IsUnderSpellbound = false;
                _SpellboundCounter = 0;
            }
        }
        public void AdjustAttackBonus(int amount)
        {
            _AttackBonus += amount;
        }
        public void AdjustDefenseBonus(int amount)
        {
            _DefenseBonus += amount;
        }
        public Color GetATKStatus()
        {
            if (_AttackBonus == 0) { return Color.White; }
            else if (_AttackBonus < 0) { return Color.Red; }
            else { return Color.Green; }
        }
        public Color GetDEFStatus()
        {
            if (_DefenseBonus == 0) { return Color.White; }
            else if (_DefenseBonus < 0) { return Color.Red; }
            else { return Color.Green; }
        }
        public void SetCurrentTile(Tile thisTile)
        {
            _CurrentTile = thisTile;
        }
        public void ReloadTileUI()
        {
            _CurrentTile.ReloadTileUI();
        }
        #endregion

        #region Data
        //Card Board Data
        private int _id = -1;
        private CardInfo _cardInfo;
        private PlayerColor _Owner;
        private bool _IsDestroyed = false;
        private bool _IsFaceDown = false;
        private bool _IsASymbol = false;
        private Tile _CurrentTile;

        //Card Stats Data
        private int _CurrentLP;
        private int _AttackBonus = 0;
        private int _DefenseBonus = 0;

        //Base Amounts for Counters and Costs (Abilities)
        private int _BaseMoveCost = 1;
        private int _BaseAttackCost = 1;
        private int _BaseDefenseCost = 1;
        private int _BaseMovesPerTurn = 1;
        private int _BaseAttacksPerTurn = 1;
        private bool _CanBeTarget = true;
       
        //Counters and Costs
        private int _MoveCost = 1;
        private int _AttackCost = 1;
        private int _DefenseCost = 1;
        private int _MovesAvailable = 1;
        private int _AttacksAvailable = 1;
        private int _TurnCounters = 0;
        private int _Counters = 0;
        private int _SpellboundCounter = 0;

        //Spellbound Data
        private bool _IsUnderSpellbound = false;
        private bool _IsPemanentSpellbound = false;
        #endregion
    }
}
