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
        public Effect(Card originCard, EffectType type)
        {
            _OriginCard = originCard;
            _Type = type;
            _ID = GetEffectID(_OriginCard);
            switch (_Type)
            {
                case EffectType.Ability: _Duration = EffectDuration.Continuous; break;
                case EffectType.Continuous: _Duration = EffectDuration.Continuous; break;
                case EffectType.Ignition: _Duration = GetEfectDuration(_ID); break;
                case EffectType.OnSummon: _Duration = EffectDuration.OneAndDone; break;
            }
            if(_Type == EffectType.Continuous) { _CanAffectNewCard = true; }
        }

        public Card OriginCard { get { return _OriginCard; } }
        public EffectID ID { get { return _ID; } }
        public PlayerColor Owner{ get { return _OriginCard.Owner; } }
        public bool CanAffectNewCards { get { return _CanAffectNewCard; } }

        public void AddAffectedByCard(Card thisCard)
        {
            _AffectedByList.Add(thisCard);
        }


        private EffectID _ID;
        private EffectType _Type;
        private EffectDuration _Duration;
        private Card _OriginCard;
        private List<Card> _AffectedByList = new List<Card>();

        private bool _CanAffectNewCard = false;

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
    }

    public enum EffectType
    {
        OnSummon,
        Ability,
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
    }
}