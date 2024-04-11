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
            _Duration = GetEfectDuration(_ID);
            _CanAffectNewCard = GetCanAffectNewCards(_ID);
        }

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
                default: throw new NotImplementedException(string.Format("Card Name: [{0}] does not have a Effect ID assignment.", originCard.Name));
            }
        }
        private static EffectDuration GetEfectDuration(EffectID id)
        {
            switch(id) 
            {
                case EffectID.DARKSymbol: return EffectDuration.Continuous;
                case EffectID.LIGHTSymbol: return EffectDuration.Continuous;
                case EffectID.WATERSymbol: return EffectDuration.Continuous;
                case EffectID.FIRESymbol: return EffectDuration.Continuous;
                case EffectID.EARTHSymbol: return EffectDuration.Continuous;
                case EffectID.WINDSymbol: return EffectDuration.Continuous;
                default: throw new Exception(string.Format("Effect ID: [{0}] doesn not have an Duration assigment.", id));
            }
        }

        private static bool GetCanAffectNewCards(EffectID id)
        {
            switch (id)
            {
                case EffectID.DARKSymbol: return true;
                case EffectID.LIGHTSymbol: return true;
                case EffectID.WATERSymbol: return true;
                case EffectID.FIRESymbol: return true;
                case EffectID.EARTHSymbol: return true;
                case EffectID.WINDSymbol: return true;
                default: throw new Exception(string.Format("Effect ID: [{0}] doesn not have an Can Affect New Cards assigment.", id));
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
        WINDSymbol
    }
}