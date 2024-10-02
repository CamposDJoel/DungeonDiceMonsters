//Joel campos
//9/12/2023
//PlayerData Class

using System.CodeDom;
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
            //Initialize the Bonus Record list
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B001_SummonerApprentice));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B002_SummonerKnight));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B003_SummonerMaster));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B004_DevlinsProdigy));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B005_KingOfDice));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B006_Fusionist));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B007_RitualMonk));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B008_ISetACard));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B009_RollDice));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B010_Fighter));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B011_BattleMaster));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B012_DefensiveWall));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B013_YouActivatedMyTrap));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B014_SpellboundMage));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B015_StopRightThere));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B016_ThatsGottaHurt));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B017_DoubleAttack));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B018_IWouldWalk));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B019_GiveMeThoseCrests));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B020_CrestCollector));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B021_Transform));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B022_AllOutAttack));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B023_AllOutDefense));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B024_MonsterPurist));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B025_SpellMaster));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B026_RitualGod));

            //Bonus item "Monster Purist" is "completed" from the get go until the player sets a spell/trap
            UpdateBonusItemRecord(BonusRecord.BonusItem.B024_MonsterPurist, 0, true);
        }
        #endregion

        #region Public Accessors
        public string Name { get{ return _name;} }
        public Deck Deck { get { return _deck;} }
        public int Crests_MOV { get { return _MoveCrests; } }
        public int Crests_ATK { get { return _AttackCrests; } }
        public int Crests_DEF { get { return _DefenseCrests; } }
        public int Crests_MAG { get { return _MagicCrests; } }
        public int Crests_TRAP { get { return _TrapCrests; } }
        public int Score_DamageDealt { get { return _DamageDealt; } }
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
        #endregion

        #region Player Score Mod Methods
        public List<BonusRecord> GetActiveBonusRecordsList()
        {
            List<BonusRecord> outputlist = new List<BonusRecord> ();
            foreach (BonusRecord record in _BonusRecords)
            {
                if (record.Completed)
                {
                    outputlist.Add(record);
                }
            }

            return outputlist;
        }
        public void IncreaseDamageDealtRecord(int amount)
        {
            _DamageDealt += amount;
        }
        public void AddScoreSummonRecord(BoardPvP.SummonType summonType, CardInfo cardSummoned)
        {
            switch(summonType)
            {
                case BoardPvP.SummonType.Normal:
                    switch (cardSummoned.DiceLevel)
                    {
                        case 1: _BonusRecords[(int)BonusRecord.BonusItem.B001_SummonerApprentice].UpdateRecord(1, true); break;
                        case 2: _BonusRecords[(int)BonusRecord.BonusItem.B002_SummonerKnight].UpdateRecord(1, true); break;
                        case 3: _BonusRecords[(int)BonusRecord.BonusItem.B003_SummonerMaster].UpdateRecord(1, true); break;
                        case 4: _BonusRecords[(int)BonusRecord.BonusItem.B004_DevlinsProdigy].UpdateRecord(1, true); break;
                        case 5: _BonusRecords[(int)BonusRecord.BonusItem.B005_KingOfDice].UpdateRecord(1, true); break;
                    }                                      
                    //Normal Summons dimension dices, add that record as well
                    _BonusRecords[(int)BonusRecord.BonusItem.B009_RollDice].UpdateRecord(1, true); break;
                case BoardPvP.SummonType.Fusion: _BonusRecords[(int)BonusRecord.BonusItem.B006_Fusionist].UpdateRecord(1, true); break;
                case BoardPvP.SummonType.Ritual: 
                    _BonusRecords[(int)BonusRecord.BonusItem.B007_RitualMonk].UpdateRecord(1, true);
                    //Normal Summons dimension dices, add that record as well
                    _BonusRecords[(int)BonusRecord.BonusItem.B009_RollDice].UpdateRecord(1, true);
                    //if the summon was a Dice Lv 5 update this other record as well
                    if(cardSummoned.DiceLevel == 5)
                    {
                        _BonusRecords[(int)BonusRecord.BonusItem.B026_RitualGod].UpdateRecord(1, true);
                    }
                    break;
                case BoardPvP.SummonType.Transform:
                    _BonusRecords[(int)BonusRecord.BonusItem.B021_Transform].UpdateRecord(1, true);
                    break;
            }
           
        }
        public void AddScoreEffectActivationRecord(Effect thisEffect)
        {
            switch(thisEffect.OriginCard.Category) 
            {
                case Category.Monster: break;
                case Category.Spell:
                    UpdateBonusItemRecord(BonusRecord.BonusItem.B025_SpellMaster, 1, true);
                    break;
                case Category.Trap:
                    if(thisEffect.Type == Effect.EffectType.Trigger)
                    {
                        UpdateBonusItemRecord(BonusRecord.BonusItem.B013_YouActivatedMyTrap, 1, true);
                    }
                    break;
            }
        }
        public void UpdateBonusItemRecord(BonusRecord.BonusItem item, int amount, bool flag)
        {
            _BonusRecords[(int)item].UpdateRecord(amount, flag);
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
        //scoring data
        private List<BonusRecord> _BonusRecords = new List<BonusRecord>();
        private int _DamageDealt = 0;
        #endregion
    }

    public class BonusRecord
    {
        public BonusRecord(BonusItem item)
        {
            _ItemId = item;
            switch (item) 
            {
                case BonusItem.B001_SummonerApprentice:
                    _Name = "Summoner Apprentice";                   
                    _Points = 100;
                    _Description = string.Format("Dice Level 1 Normal Summons Performed. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B002_SummonerKnight:
                    _Name = "Summoner Knight";
                    _Points = 100;
                    _Description = string.Format("Dice Level 2 Normal Summons Performed. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B003_SummonerMaster:
                    _Name = "Summoner Master";
                    _Points = 200;
                    _Description = string.Format("Dice Level 3 Normal Summons Performed. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B004_DevlinsProdigy:
                    _Name = "Devlin's Prodigy";
                    _Points = 300;
                    _Description = string.Format("Dice Level 4 Normal Summons Performed. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B005_KingOfDice:
                    _Name = "King of Dice";
                    _Points = 500;
                    _Description = string.Format("Dice Level 5 Normal Summons Performed. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B006_Fusionist:
                    _Name = "Fusionist";
                    _Points = 400;
                    _Description = string.Format("Fusion Summons Performed. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B007_RitualMonk:
                    _Name = "Ritual Monk";
                    _Points = 300;
                    _Description = string.Format("Ritual Summons Performed. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B008_ISetACard:
                    _Name = "I Set a Card!";
                    _Points = 200;
                    _Description = string.Format("Spell/Traps Set. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B009_RollDice:
                    _Name = "Roll Dice!";
                    _Points = 1000;
                    _Description = "Dimension 15 or more dices.";
                    break;
                case BonusItem.B010_Fighter:
                    _Name = "Fighter";
                    _Points = 100;
                    _Description = string.Format("Opponent monsters destroyed by battle. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B011_BattleMaster:
                    _Name = "Battle Master";
                    _Points = 500;
                    _Description = "Destroy 10 or more opponent monsters by battle.";
                    break;
                case BonusItem.B012_DefensiveWall:
                    _Name = "Defensive Wall";
                    _Points = 200;
                    _Description = "Take no damage in a battle while defending.";
                    break;
                case BonusItem.B013_YouActivatedMyTrap:
                    _Name = "You Activated My Trap!";
                    _Points = 200;
                    _Description = "Activate a Trap Card (TRIGGER) Effect.";
                    break;
                case BonusItem.B014_SpellboundMage:
                    _Name = "Spellbound Mage";
                    _Points = 100;
                    _Description = string.Format("Spellbounds applied to opponent cards by your card effects. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B015_StopRightThere:
                    _Name = "Stop right there!";
                    _Points = 500;
                    _Description = "Apply a permanent Spellbound to an opponent card by your card effects.";
                    break;
                case BonusItem.B016_ThatsGottaHurt:
                    _Name = "Thats gotta hurt!";
                    _Points = 300;
                    _Description = string.Format("Deal 2000 or more damage to an opponent monster with a single attack. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B017_DoubleAttack:
                    _Name = "Double Attack!";
                    _Points = 200;
                    _Description = "Attack twice with a monster during the same turn.";
                    break;
                case BonusItem.B018_IWouldWalk:
                    _Name = "I would walk five hundred miles...";
                    _Points = 300;
                    _Description = "Spend a total of 30 or more [MOV] during move actions (during the whole duel).";
                    break;
                case BonusItem.B019_GiveMeThoseCrests:
                    _Name = "Give me those crests!";
                    _Points = 500;
                    _Description = "Collect a combined of 10 resource crests in a single Dice Roll.";
                    break;
                case BonusItem.B020_CrestCollector:
                    _Name = "Crest Collector";
                    _Points = 500;
                    _Description = "Collect a collective total of 60 resource Crests from Dice Rolls.";
                    break;
                case BonusItem.B021_Transform:
                    _Name = "Transform!";
                    _Points = 300;
                    _Description = "Points 300 - Perform a Transform Summon.";
                    break;
                case BonusItem.B022_AllOutAttack:
                    _Name = "All Out Attack!";
                    _Points = 300;
                    _Description = "Use 5 [ATK] bonus Crest during a battle.";
                    break;
                case BonusItem.B023_AllOutDefense:
                    _Name = "All Out Defense!";
                    _Points = 300;
                    _Description = "Use 5 [DEF] bonus Crest during a battle.";
                    break;
                case BonusItem.B024_MonsterPurist:
                    _Name = "Monster Purist";
                    _Points = 700;
                    _Description = "Win without setting a single Spell/Trap.";
                    break;
                case BonusItem.B025_SpellMaster:
                    _Name = "Spell Master";
                    _Points = 300;
                    _Description = string.Format("Spells activated. ({0} Points Each.)", _Points);
                    break;
                case BonusItem.B026_RitualGod:
                    _Name = "Ritual God";
                    _Points = 1000;
                    _Description = "Perform a Dice Level 5 Ritual Summon.";
                    break;
                case BonusItem.B027_DivineSummoner:
                    _Name = "Divine Summoner";
                    _Points = 1000;
                    _Description = "Summon a Divine-Beast Type monster.";
                    break;
                case BonusItem.B028_Obliterate:
                    _Name = "Obliterate!";
                    _Points = 2000;
                    _Description = "Win by the effect of \"Exodia the Forbidden One\".";
                    break;
                default: throw new System.Exception("BonusItem Id not properly set.");
            }
        }

        public void UpdateRecord(int addAmount, bool newValue)
        {
            switch (_ItemId)
            {
                case BonusItem.B001_SummonerApprentice: _AmountCounter += addAmount; _Completed = newValue; break;
                case BonusItem.B002_SummonerKnight: _AmountCounter += addAmount; _Completed = newValue; break;
                case BonusItem.B003_SummonerMaster: _AmountCounter += addAmount; _Completed = newValue; break;
                case BonusItem.B004_DevlinsProdigy: _AmountCounter += addAmount; _Completed = newValue; break;
                case BonusItem.B005_KingOfDice: _AmountCounter += addAmount; _Completed = newValue; break;
                case BonusItem.B006_Fusionist: _AmountCounter += addAmount; _Completed = newValue; break;
                case BonusItem.B007_RitualMonk: _AmountCounter += addAmount; _Completed = newValue; break;
                case BonusItem.B008_ISetACard: _AmountCounter++; _Completed = newValue; break;
                case BonusItem.B009_RollDice: _AmountCounter++; _Completed = (_AmountCounter >= 15) ? true : false;  break;
                case BonusItem.B010_Fighter: _AmountCounter += addAmount; _Completed = newValue; break;
                case BonusItem.B011_BattleMaster: _AmountCounter++; _Completed = (_AmountCounter >= 10) ? true : false; break;
                case BonusItem.B012_DefensiveWall: _Completed = true; break;
                case BonusItem.B013_YouActivatedMyTrap: _Completed = true; break;
                case BonusItem.B014_SpellboundMage: _AmountCounter += addAmount; _Completed = newValue; break;
                case BonusItem.B015_StopRightThere: _Completed = true; break;
                case BonusItem.B016_ThatsGottaHurt: _AmountCounter++; _Completed = newValue; break;
                case BonusItem.B017_DoubleAttack: _Completed = true; break;
                case BonusItem.B018_IWouldWalk: _AmountCounter++; _Completed = (_AmountCounter >= 30) ? true : false; break;
                case BonusItem.B019_GiveMeThoseCrests: _Completed = true; break;
                case BonusItem.B020_CrestCollector: _AmountCounter++; _Completed = (_AmountCounter >= 60) ? true : false; break;
                case BonusItem.B021_Transform: _Completed = true; break;
                case BonusItem.B022_AllOutAttack: _Completed = true; break;
                case BonusItem.B023_AllOutDefense: _Completed = true; break;
                case BonusItem.B024_MonsterPurist: _AmountCounter++; _Completed = newValue; break;
                case BonusItem.B025_SpellMaster: _AmountCounter++; _Completed = newValue; break;
                case BonusItem.B026_RitualGod: _Completed = true; break;
                default: throw new System.Exception("BonusItem Id not properly set.");
            }
        }
        public int GetTotalPoints()
        {
            switch (_ItemId)
            {
                case BonusItem.B001_SummonerApprentice: return _AmountCounter * _Points;
                case BonusItem.B002_SummonerKnight: return _AmountCounter * _Points;
                case BonusItem.B003_SummonerMaster: return _AmountCounter * _Points;
                case BonusItem.B004_DevlinsProdigy: return _AmountCounter * _Points;
                case BonusItem.B005_KingOfDice: return _AmountCounter * _Points;
                case BonusItem.B006_Fusionist: return _AmountCounter * _Points;
                case BonusItem.B007_RitualMonk: return _AmountCounter * _Points;
                case BonusItem.B008_ISetACard: return _AmountCounter * _Points;
                case BonusItem.B009_RollDice: return (_Completed) ? _Points : 0;
                case BonusItem.B010_Fighter: return _AmountCounter * _Points;
                case BonusItem.B011_BattleMaster: return (_Completed) ? _Points : 0;
                case BonusItem.B012_DefensiveWall: return (_Completed) ? _Points : 0;
                case BonusItem.B013_YouActivatedMyTrap: return (_Completed) ? _Points : 0;
                case BonusItem.B014_SpellboundMage: return _AmountCounter * _Points;
                case BonusItem.B015_StopRightThere: return (_Completed) ? _Points : 0;
                case BonusItem.B016_ThatsGottaHurt: return _AmountCounter * _Points;
                case BonusItem.B017_DoubleAttack: return (_Completed) ? _Points : 0;
                case BonusItem.B018_IWouldWalk: return (_Completed) ? _Points : 0;
                case BonusItem.B019_GiveMeThoseCrests: return (_Completed) ? _Points : 0;
                case BonusItem.B020_CrestCollector: return (_Completed) ? _Points : 0;
                case BonusItem.B021_Transform: return (_Completed) ? _Points : 0;
                case BonusItem.B024_MonsterPurist: return (_Completed) ? _Points : 0;
                case BonusItem.B025_SpellMaster: return _AmountCounter * _Points;
                case BonusItem.B026_RitualGod: return (_Completed) ? _Points : 0;
                default: throw new System.Exception("BonusItem Id not properly set.");
            }
        }

        public BonusItem ID { get { return _ItemId; } }
        public string Name { get { return _Name; } }
        public string Description { get { return _Description; } }
        public bool Completed { get { return _Completed; } }



        private BonusItem _ItemId;
        private string _Name = "Not Set";
        private string _Description = "Not Set";
        private int _Points = 0;
        private int _AmountCounter = 0;
        private bool _Completed = false;

        public enum BonusItem
        {
            B001_SummonerApprentice,
            B002_SummonerKnight,
            B003_SummonerMaster,
            B004_DevlinsProdigy,
            B005_KingOfDice,
            B006_Fusionist,
            B007_RitualMonk,
            B008_ISetACard,
            B009_RollDice,
            B010_Fighter,
            B011_BattleMaster,
            B012_DefensiveWall,
            B013_YouActivatedMyTrap,
            B014_SpellboundMage,
            B015_StopRightThere,
            B016_ThatsGottaHurt,
            B017_DoubleAttack,
            B018_IWouldWalk,
            B019_GiveMeThoseCrests,
            B020_CrestCollector,
            B021_Transform,
            B022_AllOutAttack,
            B023_AllOutDefense,
            B024_MonsterPurist,
            B025_SpellMaster,
            B026_RitualGod,
            B027_DivineSummoner,
            B028_Obliterate
        }
    }
}
