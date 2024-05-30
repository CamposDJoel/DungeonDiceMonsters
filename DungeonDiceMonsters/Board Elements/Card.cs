//Joel Campos
//9/12/2023
//Card Class

using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

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
            InitializeEffects();

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
            void InitializeEffects()
            {
                if(HasOnSummonEffect)
                {
                    _OnSummonEffectObject = new Effect(this, Effect.EffectType.OnSummon);
                }
                if(HasContinuousEffect)
                {
                    _ContinuousEffectObject = new Effect(this, Effect.EffectType.Continuous);
                }
                if(HasIgnitionEffect)
                {
                    _IgnitionEffectObject = new Effect(this, Effect.EffectType.Ignition);
                }
            }
        }
        public Card(int id, Attribute attribute, PlayerColor owner)
        {
            _id = id;
            _cardInfo = new CardInfo(attribute);
            _Owner = owner;
            _CurrentLP = _cardInfo.LP;
            _IsASymbol = true;
            _ContinuousEffectObject = new Effect(this, Effect.EffectType.Continuous);
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
        public int OriginalATK { get { return _cardInfo.ATK; } }
        public int OriginalDEF { get { return _cardInfo.DEF; } }
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
        public Effect OnSummonEffect { get { return _OnSummonEffectObject; } }
        public Effect ContinuousEffect { get { return _ContinuousEffectObject; } }
        public Effect IgnitionEffect { get { return _IgnitionEffectObject; } }
        public bool HasAbility { get { return _cardInfo.HasAbility; } }
        public string OnSummonEffectText { get { return _cardInfo.OnSummonEffect; } }
        public string ContinuousEffectText { get { return _cardInfo.ContinuousEffect; } }
        public string IgnitionEffectText { get { return _cardInfo.IgnitionEffect; } }
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
        public bool EffectUsedThisTurn { get { return _EffectUsedThisTurn; } }
        public int AttackRange { get { return _AttackRange; } }
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
            _EffectUsedThisTurn = false;
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
        public void FlipFaceUp()
        {
            _IsFaceDown = false;
            ReloadTileUI();
        }
        public void MarkEffectUsedThisTurn()
        {
            _EffectUsedThisTurn = true;
        }
        public void ReloadTileUI()
        {
            _CurrentTile.ReloadTileUI();
        }
        public void UpdateFieldTypeBonus()
        {
            //Step 1: Determine if the card can get a field bonus on the current tile
            bool willReceiveFieldTypeBonus = false;
            Tile.FieldTypeValue currentFieldType = _CurrentTile.FieldType;
            switch (currentFieldType) 
            {
                case Tile.FieldTypeValue.Mountain:
                    if (_cardInfo.Type == Type.Dragon || _cardInfo.Type == Type.Thunder || _cardInfo.Type == Type.WingedBeast) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Sogen:
                    if (_cardInfo.Type == Type.BeastWarrior || _cardInfo.Type == Type.Warrior) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Forest:
                    if (_cardInfo.Type == Type.Insect || _cardInfo.Type == Type.Beast || _cardInfo.Type == Type.Plant || _cardInfo.Type == Type.BeastWarrior) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Wasteland:
                    if (_cardInfo.Type == Type.Dinosaur || _cardInfo.Type == Type.Zombie || _cardInfo.Type == Type.Rock) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Yami:
                    if (_cardInfo.Type == Type.Fiend || _cardInfo.Type == Type.Spellcaster || _cardInfo.Type == Type.Illusion) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Umi:
                    if (_cardInfo.Type == Type.Fish || _cardInfo.Type == Type.SeaSerpent || _cardInfo.Type == Type.Thunder || _cardInfo.Type == Type.Aqua) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Volcano:
                    if (_cardInfo.Type == Type.Pyro) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Swamp:
                    if (_cardInfo.Type == Type.Reptile || _cardInfo.Type == Type.Wyrm) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Cyberworld:
                    if (_cardInfo.Type == Type.Cyberse) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Sanctuary:
                    if (_cardInfo.Type == Type.Fairy) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Scrapyard:
                    if (_cardInfo.Type == Type.Machine) { willReceiveFieldTypeBonus = true; }
                    break;
                default: willReceiveFieldTypeBonus = false; break;
            }

            //Step 2: Give the monster the field bonus if it doesnt have one already 
            if(willReceiveFieldTypeBonus) 
            {
                if(!_FieldBonusActive)
                {
                    _AttackBonus += 500;
                    _DefenseBonus += 500;
                    _FieldBonusActive = true;
                }
            }
            //Step 3: Remove the field bonus if it had one
            else
            {
                if(_FieldBonusActive)
                {
                    _AttackBonus -= 500;
                    _DefenseBonus -= 500;
                    _FieldBonusActive = false;
                }
            }

            //Step 4: Reload the monster's tile UI
            ReloadTileUI();
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
        private bool _FieldBonusActive = false;

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
        private bool _EffectUsedThisTurn = false;

        //Spellbound Data
        private bool _IsUnderSpellbound = false;
        private bool _IsPemanentSpellbound = false;

        //Ranges
        private int _AttackRange = 1;
        //TODO: private int _MoveRango = 1;

        //Effects
        private Effect _OnSummonEffectObject;
        private Effect _ContinuousEffectObject;
        private Effect _IgnitionEffectObject;
        #endregion
    }
}