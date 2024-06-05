//Joel Campos
//4/11/2024
//Effect Object Class

using System;
using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public class Effect
    {
        #region Constructors
        public Effect(Card originCard, EffectType type)
        {
            _OriginCard = originCard;
            _Type = type;
            _ID = GetEffectID(_OriginCard, type);
            _EffectText = GetEffectText(_OriginCard, _Type);
            _IsOneTurnIgnition = IsAOneTurnIgnitionEffect(_EffectText);
            InitializeCost();
        }
        #endregion

        #region Public Methods
        public Card OriginCard { get { return _OriginCard; } }
        public EffectID ID { get { return _ID; } }
        public PlayerColor Owner{ get { return _OriginCard.Controller; } }
        public EffectType Type { get { return _Type; } }
        public bool IsAOneTurnIgnition { get { return _IsOneTurnIgnition; } }
        public List<Card> AffectedByList { get { return _AffectedByList; } }
        public string EffectText { get { return _EffectText; } }
        public bool HasACost { get { return _CrestCost != Crest.NONE; } }
        public Crest CrestCost { get { return _CrestCost; } }
        public int CostAmount {  get { return _CostAmount; } }
        //Custom Fields
        public int CustomInt1 { get; set; }
        public void AddAffectedByCard(Card thisCard)
        {
            _AffectedByList.Add(thisCard);
        }
        public void RemoveAffectedByCard(Card thisCard)
        {
            _AffectedByList.Remove(thisCard);
        }
        #endregion

        #region Data
        private EffectID _ID;
        private EffectType _Type;
        private bool _IsOneTurnIgnition = false;
        private Card _OriginCard;
        private string _EffectText;
        private Crest _CrestCost = Crest.NONE;
        private int _CostAmount = 0;
        private List<Card> _AffectedByList = new List<Card>();
        #endregion

        #region "React To" Flags
        public bool ReactsToMonsterSummon { get; set; }
        public bool ReactsToMonsterDestroyed { get; set; }
        public bool ReactsToAttributeChange { get; set; }
        public bool ReactsToMonsterControlChange { get; set; }
        #endregion

        #region Private Methods
        private void InitializeCost()
        {
            if (_EffectText.StartsWith("[MAG")) { _CrestCost = Crest.MAG; }
            if (_EffectText.StartsWith("[TRAP")) { _CrestCost = Crest.TRAP; }
            if (_EffectText.StartsWith("[ATK")) { _CrestCost = Crest.ATK; }
            if (_EffectText.StartsWith("[DEF")) { _CrestCost = Crest.DEF; }
            if (_EffectText.StartsWith("[MOV")) { _CrestCost = Crest.MOV; }

            if(_EffectText.Contains(" 1] -")) { _CostAmount = 1; }
            if(_EffectText.Contains(" 2] -")) { _CostAmount = 2; }
            if(_EffectText.Contains(" 3] -")) { _CostAmount = 3; }
            if(_EffectText.Contains(" 4] -")) { _CostAmount = 4; }
            if(_EffectText.Contains(" 5] -")) { _CostAmount = 5; }
            if(_EffectText.Contains(" 6] -")) { _CostAmount = 6; }
            if(_EffectText.Contains(" 7] -")) { _CostAmount = 7; }
            if(_EffectText.Contains(" 8] -")) { _CostAmount = 8; }
            if(_EffectText.Contains(" 9] -")) { _CostAmount = 9; }
            if(_EffectText.Contains(" 10] -")) { _CostAmount = 10; }
        }
        private static EffectID GetEffectID(Card originCard, EffectType type) 
        {
            switch(originCard.Name) 
            {
                case "DARK Symbol": return EffectID.DARKSymbol;
                case "LIGHT Symbol": return EffectID.LIGHTSymbol;
                case "WATER Symbol": return EffectID.WATERSymbol;
                case "FIRE Symbol": return EffectID.FIRESymbol;
                case "EARTH Symbol": return EffectID.EARTHSymbol;
                case "WIND Symbol": return EffectID.WINDSymbol;
                case "Mountain": return EffectID.Mountain;
                case "Sogen": return EffectID.Sogen;
                case "Wasteland": return EffectID.Wasteland;
                case "Forest": return EffectID.Forest;
                case "Yami": return EffectID.Yami;
                case "Umi": return EffectID.Umi;
                case "Volcano": return EffectID.Volcano;
                case "Swamp": return EffectID.Swamp;
                case "Sanctuary": return EffectID.Sanctuary;
                case "Cyberworld": return EffectID.Cyberworld;
                case "Scrapyard": return EffectID.Scrapyard;
                case "M-Warrior #1": if (type == EffectType.OnSummon) { return EffectID.MWarrior1_OnSummon; } else { return EffectID.MWarrior1_Ignition; }
                case "M-Warrior #2": if (type == EffectType.OnSummon) { return EffectID.MWarrior2_OnSummon; } else { return EffectID.MWarrior2_Ignition; }
                case "Polymerization": return EffectID.Polymerization;
                case "Karbonala Warrior": if (type == EffectType.Continuous) { return EffectID.KarbonalaWarrior_Continuous; } else { return EffectID.KarbonalaWarrior_Ignition; }
                case "Fire Kraken": return EffectID.FireKraken;
                case "Change of Heart": return EffectID.ChangeOfHeart;
                case "Hitotsu-Me Giant": return EffectID.HitotsumeGiant_OnSummon;
                case "Thunder Dragon": return EffectID.ThunderDragon;
                case "Twin-Headed Thunder Dragon": return EffectID.TwinHeadedThunderDragon;
                default: throw new NotImplementedException(string.Format("Card Name: [{0}] does not have a Effect ID assignment.", originCard.Name));
            }
        }
        private static bool IsAOneTurnIgnitionEffect(string effectText)
        {
            return effectText.Contains("until the end of this turn.");
        }
        private static string GetEffectText(Card originCard, EffectType type) 
        {
            switch (type) 
            {
                case EffectType.OnSummon: return originCard.OnSummonEffectText;
                case EffectType.Continuous: return originCard.ContinuousEffectText;
                case EffectType.Ignition: return originCard.IgnitionEffectText;
                default: throw new Exception(string.Format("Effect type not defined to get Effect Text. EffectType:[{0}]", type));
            }
        }
        #endregion

        #region Enums
        public enum EffectType
        {
            OnSummon,
            Continuous,
            Ignition
        }
        public enum EffectID
        {
            DARKSymbol,
            LIGHTSymbol,
            WATERSymbol,
            FIRESymbol,
            EARTHSymbol,
            WINDSymbol,
            Mountain,
            Sogen,
            Wasteland,
            Forest,
            Umi,
            Yami,
            Volcano,
            Swamp,
            Cyberworld,
            Sanctuary,
            Scrapyard,
            MWarrior1_OnSummon,
            MWarrior1_Ignition,
            MWarrior2_OnSummon,
            MWarrior2_Ignition,
            Polymerization,
            KarbonalaWarrior_Continuous,
            KarbonalaWarrior_Ignition,
            FireKraken,
            ChangeOfHeart,
            ThunderDragon,
            TwinHeadedThunderDragon,
            HitotsumeGiant_OnSummon,
        }
        #endregion
    }
}