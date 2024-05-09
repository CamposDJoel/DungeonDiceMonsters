//Joel Campos
//4/11/2024
//Effect Object Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            switch (_Type)
            {
                case EffectType.Continuous: _Duration = EffectDuration.Continuous; break;
                case EffectType.Ignition: _Duration = GetEfectDuration(_ID); break;
                case EffectType.OnSummon: _Duration = EffectDuration.OneAndDone; break;
            }
        }
        #endregion

        #region Public Methods
        public Card OriginCard { get { return _OriginCard; } }
        public EffectID ID { get { return _ID; } }
        public PlayerColor Owner{ get { return _OriginCard.Owner; } }
        public EffectDuration Duration { get { return _Duration; } }
        public List<Card> AffectedByList { get { return _AffectedByList; } }
        public void AddAffectedByCard(Card thisCard)
        {
            _AffectedByList.Add(thisCard);
        }
        #endregion

        #region Data
        private EffectID _ID;
        private EffectType _Type;
        private EffectDuration _Duration;
        private Card _OriginCard;
        private List<Card> _AffectedByList = new List<Card>();
        #endregion

        #region "React To" Flags
        public bool ReactsToMonsterSummon { get; set; }
        public bool ReactsToAttributeChange { get; set; }
        #endregion

        #region Private Methods
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
                case "Hitotsu-Me Giant": return EffectID.HitotsumeGiant_OnSummon;
                case "Thunder Dragon": return EffectID.ThunderDragon_Continuous;
                default: throw new NotImplementedException(string.Format("Card Name: [{0}] does not have a Effect ID assignment.", originCard.Name));
            }
        }
        private static EffectDuration GetEfectDuration(EffectID id)
        {
            switch(id) 
            {
                default: throw new Exception(string.Format("Effect ID: [{0}] doesn not have an Duration assigment.", id));
            }
        }
        #endregion
    }

    public enum EffectType
    {
        OnSummon,
        Continuous,
        Ignition
    }
    public enum EffectDuration
    {
        OneAndDone,
        OneTurn,
        Continuous
    }
    public enum EffectID
    {
        DARKSymbol,
        LIGHTSymbol,
        WATERSymbol,
        FIRESymbol,
        EARTHSymbol,
        WINDSymbol,
        MWarrior1_OnSummon,
        HitotsumeGiant_OnSummon,
        ThunderDragon_Continuous,
    }
}