//Joel Campos
//9/12/2023
//Card Class

using System.Collections.Generic;
using System.Drawing;

namespace DungeonDiceMonsters
{
    public class Card
    {
        #region Constructors
        public Card(int id, CardInfo info, PlayerColor controller, bool isFaceDown)
        {
            _id = id;
            _cardInfo = info;
            _Controller = controller;
            _CurrentLP = _cardInfo.LP;
            _IsFaceDown = isFaceDown;
            _CardText = _cardInfo.CardText;
            if(HasAbility)
            {
                ApplyAbility();
            }
            InitializeMultableDataFields();
            if(HasTriggerEffect)
            {
                IntializeTriggerEvent();
            }

            void InitializeMultableDataFields()
            {
                _CurrentAttribute = _cardInfo.Attribute;
                _CurrentType = _cardInfo.Type;
            }
            void ApplyAbility()
            {
                switch (_cardInfo.Ability)
                {
                    case "Cannot be target by your opponent’s card effects.": _CanBeTarget = false; break;
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
                    case "Cannot Move": _CannotMoveCounters = 1; break;
                    case "Move Range: 2": _MoveRange = 2; break;
                    case "Move Range: 3": _MoveRange = 3; break;
                    case "Move Range: 4": _MoveRange = 4; break;
                    case "Move Range: 5": _MoveRange = 5; break;
                    case "Attack Range: 1": _AttackRange = 1; break;
                    case "Attack Range: 2": _AttackRange = 2; break;
                    case "Attack Range: 3": _AttackRange = 3; break;
                    case "Attack Range: 4": _AttackRange = 4; break;
                    case "Attack Range: 5": _AttackRange = 5; break;
                }
                ResetOneTurnData();
            }
            void IntializeTriggerEvent()
            {
                if(TriggerEffect.Contains("When your opponent summons"))
                {
                    _TriggerEvent = TriggeredBy.MonsterSummon;
                }
            }
        }
        public Card(int id, Attribute attribute, PlayerColor owner)
        {
            _id = id;
            _cardInfo = new CardInfo(attribute);
            _Controller = owner;
            _CurrentLP = _cardInfo.LP;
            _IsASymbol = true;
            _CurrentAttribute = _cardInfo.Attribute;
        }
        #endregion

        #region Base Card Info
        public int CardID { get { return _cardInfo.ID; } }
        public int CardNumber { get { return _cardInfo.CardNumber; } }
        public string Name { get { return _cardInfo.Name; } }
        public int Level { get { return _cardInfo.Level; } }
        public int DiceLevel { get { return _cardInfo.DiceLevel; } }
        public string FullCardText { get { return _cardInfo.CardText; } }
        public int FullCardTextItems { get { return _cardInfo.CardTextItems; } }
        public Type OriginalType { get { return _cardInfo.Type; } }
        public Type Type { get { return _CurrentType; } }
        public string OriginalTypeAsString { get { return _cardInfo.TypeAsString; } }
        public string TypeAsString { get { return CardInfo.GetMonsterTypeAsString(_CurrentType); } }
        public SecType SecType { get { return _cardInfo.SecType; } }
        public int ATK
        { 
            get 
            {
                int finalAttack = _cardInfo.ATK + _AttackBonus;
                if (finalAttack < 0) finalAttack = 0;
                return finalAttack;
            }
        }
        public int DEF 
        { 
            get 
            {
                int finalDefense = _cardInfo.DEF + _DefenseBonus;
                if (finalDefense < 0) finalDefense = 0;
                return finalDefense;
            } 
        }
        public int OriginalATK { get { return _cardInfo.ATK; } }
        public int OriginalDEF { get { return _cardInfo.DEF; } }
        public int LP { get { return _CurrentLP; } }
        public Attribute CurrentAttribute { get { return _CurrentAttribute; } }
        public Attribute OriginalAttribute { get { return _cardInfo.Attribute; } }
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
        public bool HasTriggerEffect { get { return _cardInfo.HasTriggerEffect; } } 
        public bool HasEquipEffect { get { return _cardInfo.HasEquipEffect; } } 
        public bool HasAbility { get { return _cardInfo.HasAbility; } }
        public string OnSummonEffectText { get { return _cardInfo.OnSummonEffect; } }
        public string ContinuousEffectText { get { return _cardInfo.ContinuousEffect; } }
        public string IgnitionEffectText { get { return _cardInfo.IgnitionEffect; } }
        public string TriggerEffectText { get { return _cardInfo.TriggerEffect; } }
        public string EquipEffectText { get { return _cardInfo.EquipEffect; } }
        public string Ability { get { return _cardInfo.Ability; } }
        public string TriggerEffect { get { return _cardInfo.TriggerEffect; } } 
        public bool EffectsAreImplemented { get { return _cardInfo.EffectsAreImplemented; } }
        #endregion

        #region On Board Data
        public int OnBoardID { get { return _id; } }       
        public PlayerColor Controller { get { return _Controller; } }
        public bool IsFaceDown { get { return _IsFaceDown; } }
        public bool IsDiscardted { get { return _IsDestroyed; } }
        public bool IsASymbol { get { return _IsASymbol; } }
        public bool IsAMonster { get { return !_IsASymbol && _CurrentAttribute != Attribute.SPELL && _CurrentAttribute != Attribute.TRAP; } }
        public bool WasTransformedInto { get { return _WasTransformedInto; } }
        public bool IsUnderSpellbound { get { return _IsUnderSpellbound; } }
        public bool IsPermanentSpellbound { get { return _IsPemanentSpellbound; } }
        public int SpellboundCounter { get { return _SpellboundCounter; } }
        public int TurnCounters { get { return _TurnCounters; } }
        public int Counters { get { return _Counters; } }
        public Tile CurrentTile { get { return _CurrentTile; } }
        public TriggeredBy TriggerEvent { get { return _TriggerEvent; } }
        public List<Card> EquipCards { get { return _EquipCards; } }
        public Card EquipToCard { get { return _EquipedTo; } }
        #endregion

        #region On Board Counters and Flags
        public bool CanBeTarget { get { return _CanBeTarget; } }
        public int MoveCost
        { 
            get 
            {
                int finalcost = _BaseMoveCost + _MoveCostBonus;
                if (finalcost < 1) { return 1; } else { return finalcost; }
            } 
        }
        public int AttackCost 
        {
            get
            {
                int finalcost = _BaseAttackCost + _AttackCostBonus;
                if (finalcost < 1) { return 1; } else { return finalcost; }
            }
        }
        public int DefenseCost 
        {
            get
            {
                int finalcost = _BaseDefenseCost + _DefenseCostBonus;
                if (finalcost < 1) { return 1; } else { return finalcost; }
            }
        }
        public int MovesAvaiable { get { return _MovesAvailable; } }
        public int AttacksAvaiable { get { return _AttacksAvailable; } }
        public int AttacksPerTurn { get { return _BaseAttacksPerTurn; } }
        public int MovesPerTurn { get { return _BaseMovesPerTurn; } }
        public bool EffectUsedThisTurn { get { return _EffectUsedThisTurn; } }
        public int AttackRange {
            get
            {
                int finalRange = _AttackRange + _AttackRangeBonus;
                if (finalRange < 1) { return 1; } else { return finalRange; }
            }
        }
        public int MoveRange 
        { 
            get 
            {
                int finalRange = _MoveRange + _MoveRangeBonus;
                if (finalRange < 1) { return 1; } else { return finalRange; }
            }
        }
        public int CannotAttackCounters { get { return _CannotAttackCounters; } }
        public int CannotMoveCounters { get { return _CannotMoveCounters; } }
        public int AttacksPerformedThisTurn { get { return _AttacksPerformedThisTurn; } }
        public int DefendsPerformedThisTurn { get { return _DefendsPerformedThisTurm; } }
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
        public void AdjustAvailableAttacks(int amount)
        {
            _AttacksAvailable += amount;
        }
        public void RemoveMoveCounter()
        {
            _MovesAvailable--;
        }
        public void ResetOneTurnData()
        {
            _MovesAvailable = _BaseMovesPerTurn;
            _AttacksAvailable = _BaseAttacksPerTurn;
            _EffectUsedThisTurn = false;
            _AttacksPerformedThisTurn = 0;
            _DefendsPerformedThisTurm = 0;
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
            ReloadTileUI();
        }
        public void AdjustAttackBonus(int amount)
        {
            _AttackBonus += amount;
            ReloadTileUI();
        }
        public void AdjustDefenseBonus(int amount)
        {
            _DefenseBonus += amount;
            ReloadTileUI();
        }
        public void AdjustAttackRangeBonus(int amount)
        {
            _AttackRangeBonus += amount;
        }
        public void AdjustMoveRangeBonus(int amount)
        {
            _MoveRangeBonus += amount;
        }
        public void IncreaseAttacksPerformedThisTurn(int amount)
        {
            _AttacksPerformedThisTurn += amount;
        }
        public void IncreaseDefendsPerformedThisTurn(int amount)
        {
            _DefendsPerformedThisTurm += amount;
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
            if(!_IsDestroyed)
            {
                _CurrentTile.ReloadTileUI();
            }           
        }
        public void UpdateFieldTypeBonus()
        {
            //Step 1: Determine if the card can get a field bonus on the current tile
            bool willReceiveFieldTypeBonus = false;
            Tile.FieldTypeValue currentFieldType = _CurrentTile.FieldType;
            switch (currentFieldType) 
            {
                case Tile.FieldTypeValue.Mountain:
                    if (_CurrentType == Type.Dragon || _CurrentType == Type.Thunder || _CurrentType == Type.WingedBeast) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Sogen:
                    if (_CurrentType == Type.BeastWarrior || _CurrentType == Type.Warrior) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Forest:
                    if (_CurrentType == Type.Insect || _CurrentType == Type.Beast || _CurrentType == Type.Plant || _CurrentType == Type.BeastWarrior) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Wasteland:
                    if (_CurrentType == Type.Dinosaur || _CurrentType == Type.Zombie || _CurrentType == Type.Rock) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Yami:
                    if (_CurrentType == Type.Fiend || _CurrentType == Type.Spellcaster || _CurrentType == Type.Illusion) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Umi:
                    if (_CurrentType == Type.Fish || _CurrentType == Type.SeaSerpent || _CurrentType == Type.Thunder || _CurrentType == Type.Aqua) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Volcano:
                    if (_CurrentType == Type.Pyro) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Swamp:
                    if (_CurrentType == Type.Reptile || _CurrentType == Type.Wyrm) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Cyberworld:
                    if (_CurrentType == Type.Cyberse) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Sanctuary:
                    if (_CurrentType == Type.Fairy) { willReceiveFieldTypeBonus = true; }
                    break;
                case Tile.FieldTypeValue.Scrapyard:
                    if (_CurrentType == Type.Machine) { willReceiveFieldTypeBonus = true; }
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
        public Effect GetOnSummonEffect()
        {
            return new Effect(this, Effect.EffectType.OnSummon);
        }
        public Effect GetContinuousEffect()
        {
            return new Effect(this, Effect.EffectType.Continuous);
        }
        public Effect GetIgnitionEffect()
        {
           return new Effect(this, Effect.EffectType.Ignition);
        }
        public Effect GetTriggerEffect()
        {
            return new Effect(this, Effect.EffectType.Trigger);
        }
        public Effect GetEquipEffect()
        {
            return new Effect(this, Effect.EffectType.Equip);
        }
        public void ChangeAttribute(Attribute newAttribute)
        {
            _CurrentAttribute = newAttribute;
        }
        public void ChangeMonsterType(Type newType)
        {
            _CurrentType = newType;
            UpdateFieldTypeBonus();
        }
        public void ResetAttribute()
        {
            _CurrentAttribute = _cardInfo.Attribute;
        }
        public void SwitchController(PlayerColor newColor)
        {
            _Controller = newColor;
        }
        public void MarkAsTransformedInto()
        {
            _WasTransformedInto = true;
        }
        public void SpellboundIt(int turns)
        {
            if (turns == 99)
            {
                _IsPemanentSpellbound = true;
            }

            _SpellboundCounter = turns;
            _IsUnderSpellbound = true;            
            ReloadTileUI();
        }
        public bool CanMove()
        {
            return !_IsUnderSpellbound && _CannotMoveCounters == 0 && _MovesAvailable >= 1;
        }
        public void AddCannotAttackCounter()
        {
            _CannotAttackCounters++;
        }
        public void RemoveCannotAttackCounter()
        {
            _CannotAttackCounters--;
        }
        public void AddCannotMoveCounter()
        {
            _CannotMoveCounters++;
        }
        public void PlaceTurnCounter()
        {
            _TurnCounters++;
        }
        public void AdjustAttackCostBonus(int amount)
        {
            _AttackCostBonus += amount;
        }
        public void AdjustDefenseCostBonus(int amount)
        {
            _DefenseCostBonus += amount;
        }
        public void AdjustMoveCostBonus(int amount)
        {
            _MoveCostBonus += amount;
        }
        public int GetRitualSpellID()
        {
            if(_cardInfo.Category == Category.Monster && _cardInfo.SecType == SecType.Ritual)
            {
                string ritualSpellName = _cardInfo.RitualCard;
                return CardDataBase.GetCardWithName(ritualSpellName).ID;
            }
            else
            {
                throw new System.Exception("Card cannot return Ritual Spell becuase it is NOT a Ritual Monster");
            }
        }
        public List<int> GetFusionMaterialsIDs()
        {
            if(_cardInfo.SecType == SecType.Fusion)
            {
                List<int> materials = new List<int>();
                string fusionMaterial1 = _cardInfo.FusionMaterial1;
                string fusionMaterial2 = _cardInfo.FusionMaterial2;
                materials.Add(CardDataBase.GetCardWithName(fusionMaterial1).ID);
                materials.Add(CardDataBase.GetCardWithName(fusionMaterial2).ID);
                if(_cardInfo.HasThirdFusionMaterial)
                {
                    string fusionMaterial3 = _cardInfo.FusionMaterial3;
                    materials.Add(CardDataBase.GetCardWithName(fusionMaterial3).ID);
                }

                return materials;
            }
            else
            {
                throw new System.Exception("Fusion Materials can be returned for a non-fusion monster.");
            }
        }
        public void AddEquipCard(Card equipCard)
        {
            _EquipCards.Add(equipCard);
        }
        public void RemoveEquipCard(Card equipCard)
        {
            if(_EquipCards.Contains(equipCard))
            {
                _EquipCards.Remove(equipCard);
            }
        }
        public bool IsEquipedWith(Card equipCard)
        {
            return _EquipCards.Contains(equipCard);
        }
        public bool IsEquipedWith(string cardName)
        {
            bool equipFound = false;
            foreach (Card card in _EquipCards) 
            {
                if (card.Name == cardName) equipFound = true; break;
            }
            return equipFound;
        }
        public void SetEquipedToCard(Card targetCard)
        {
            _EquipedTo = targetCard;
        }
        #endregion

        #region Data
        //Card Board Data
        private int _id = -1;
        private CardInfo _cardInfo;
        private PlayerColor _Controller;
        private bool _IsDestroyed = false;
        private bool _IsFaceDown = false;
        private bool _IsASymbol = false;
        private Tile _CurrentTile;
        private bool _FieldBonusActive = false;
        private bool _WasTransformedInto = false;
        private string _CardText;

        //Card Stats Data
        private int _CurrentLP;
        private int _AttackBonus = 0;
        private int _DefenseBonus = 0;

        //Mutable data fields
        private Attribute _CurrentAttribute;
        private Type _CurrentType;

        //Action Costs
        private int _BaseMoveCost = 1;
        private int _BaseAttackCost = 1;
        private int _BaseDefenseCost = 1;
        private int _MoveCostBonus = 0;
        private int _AttackCostBonus = 0;
        private int _DefenseCostBonus = 0;

        //Counters and Costs
        private int _BaseMovesPerTurn = 1;
        private int _BaseAttacksPerTurn = 1;
        private bool _CanBeTarget = true;
        private int _MovesAvailable = 1;
        private int _AttacksAvailable = 1;
        private int _TurnCounters = 0;
        private int _Counters = 0;
        private int _SpellboundCounter = 0;
        private bool _EffectUsedThisTurn = false;
        private int _CannotAttackCounters = 0;
        private int _CannotMoveCounters = 0;
        private int _AttacksPerformedThisTurn = 0;
        private int _DefendsPerformedThisTurm = 0;

        //Spellbound Data
        private bool _IsUnderSpellbound = false;
        private bool _IsPemanentSpellbound = false;

        //Ranges
        private int _AttackRange = 1;
        private int _MoveRange = 1;
        private int _AttackRangeBonus = 0;
        private int _MoveRangeBonus = 0;

        //Effect data
        private TriggeredBy _TriggerEvent = TriggeredBy.None;
        private List<Card> _EquipCards = new List<Card>();
        private Card _EquipedTo;
        #endregion

        #region enums
        public enum TriggeredBy
        {
            None = 0,
            MonsterSummon,
            DeclareAttack
        }
        #endregion
    }
}