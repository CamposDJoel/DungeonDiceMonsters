//Joel Campos
//4/11/2024
//Effect Object Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;

namespace DungeonDiceMonsters
{
    public class Effect
    {
        #region Constructors
        public Effect(Card originCard, EffectType type)
        {
            _OriginCard = originCard;
            _Type = type;
            _ID = GetEffectID(_OriginCard);
            _EffectText = GetEffectText(_OriginCard, _Type);
            _IsOneTurnIgnition = IsAOneTurnIgnitionEffect(_EffectText);
            InitializeCost();
        }
        #endregion

        #region Public Methods
        public Card OriginCard { get { return _OriginCard; } }
        public EffectID ID { get { return _ID; } }
        public PlayerColor Owner{ get { return _OriginCard.Owner; } }
        public EffectType Type { get { return _Type; } }
        public bool IsAOneTurnIgnition { get { return _IsOneTurnIgnition; } }
        public List<Card> AffectedByList { get { return _AffectedByList; } }
        public string EffectText { get { return _EffectText; } }
        public bool HasACost { get { return _CrestCost != Crest.NONE; } }
        public Crest CrestCost { get { return _CrestCost; } }
        public int CostAmount {  get { return _CostAmount; } }
        public void AddAffectedByCard(Card thisCard)
        {
            _AffectedByList.Add(thisCard);
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
        public bool ReactsToAttributeChange { get; set; }
        public bool ReactsToTileRemoval { get; set; }
        #endregion

        #region Private Methods
        private void InitializeCost()
        {
            if (_EffectText.Contains("[MAG")) { _CrestCost = Crest.MAG; }
            if (_EffectText.Contains("[TRAP")) { _CrestCost = Crest.TRAP; }
            if (_EffectText.Contains("[ATK")) { _CrestCost = Crest.ATK; }
            if (_EffectText.Contains("[DEF")) { _CrestCost = Crest.DEF; }
            if (_EffectText.Contains("[MOV")) { _CrestCost = Crest.MOV; }

            if(_EffectText.Contains(" 1]")) { _CostAmount = 1; }
            if(_EffectText.Contains(" 2]")) { _CostAmount = 2; }
            if(_EffectText.Contains(" 3]")) { _CostAmount = 3; }
            if(_EffectText.Contains(" 4]")) { _CostAmount = 4; }
            if(_EffectText.Contains(" 5]")) { _CostAmount = 5; }
            if(_EffectText.Contains(" 6]")) { _CostAmount = 6; }
            if(_EffectText.Contains(" 7]")) { _CostAmount = 7; }
            if(_EffectText.Contains(" 8]")) { _CostAmount = 8; }
            if(_EffectText.Contains(" 9]")) { _CostAmount = 9; }
            if(_EffectText.Contains(" 10]")) { _CostAmount = 10; }
        }
        private static EffectID GetEffectID(Card originCard) 
        {
            switch(originCard.Name) 
            {
                case "DARK Symbol": return EffectID.DARKSymbol;
                case "LIGHT Symbol": return EffectID.LIGHTSymbol;
                case "WATER Symbol": return EffectID.WATERSymbol;
                case "FIRE Symbol": return EffectID.FIRESymbol;
                case "EARTH Symbol": return EffectID.EARTHSymbol;
                case "WIND Symbol": return EffectID.WINDSymbol;
                case "M-Warrior #1": return EffectID.MWarrior1_OnSummon;
                case "Mountain": return EffectID.Mountain;
                case "Sogen": return EffectID.Sogen;
                case "Wasteland": return EffectID.Wasteland;
                case "Forest": return EffectID.Forest;
                case "Yami": return EffectID.Yami;
                case "Umi": return EffectID.Umi;
                case "Hitotsu-Me Giant": return EffectID.HitotsumeGiant_OnSummon;
                case "Thunder Dragon": return EffectID.ThunderDragon_Continuous;
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
                case EffectType.OnSummon: return originCard.OnSummonEffect.ToString();
                case EffectType.Continuous: return originCard.ContinuousEffect.ToString();
                case EffectType.Ignition: return originCard.IgnitionEffect.ToString();
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
            MWarrior1_OnSummon,
            HitotsumeGiant_OnSummon,
            ThunderDragon_Continuous,
        }
        #endregion
    }
}