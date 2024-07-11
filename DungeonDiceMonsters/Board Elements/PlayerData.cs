//Joel campos
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
            //Initialize the Bonus Record list
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B001_SummonerApprentice));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B002_SummonerKnight));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B003_SummonerMaster));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B004_DevlinsProdigy));
            _BonusRecords.Add(new BonusRecord(BonusRecord.BonusItem.B005_KingOfDice));
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
        public void AddNormalSummonRecord(int DiceLevel)
        {
            switch (DiceLevel)
            {
                case 1: _BonusRecords[(int)BonusRecord.BonusItem.B001_SummonerApprentice].UpdateRecord(1, true); break;
                case 2: _BonusRecords[(int)BonusRecord.BonusItem.B002_SummonerKnight].UpdateRecord(1, true); break;
                case 3: _BonusRecords[(int)BonusRecord.BonusItem.B003_SummonerMaster].UpdateRecord(1, true); break;
                case 4: _BonusRecords[(int)BonusRecord.BonusItem.B004_DevlinsProdigy].UpdateRecord(1, true); break;
                case 5: _BonusRecords[(int)BonusRecord.BonusItem.B004_DevlinsProdigy].UpdateRecord(1, true); break;
            }
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
        }
    }
}
