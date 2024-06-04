//Joel Campos
//10/3/2023
//Card Info (Ver 2) Class

using System;
using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public class CardInfo
    {
        #region Constructors
        public CardInfo(rawcardinfo cardData)
        {
            //Convert all the raw info into the actual Card Info
            _Id = Convert.ToInt32(cardData.id);
            _CardNumber = Convert.ToInt32(cardData.cardNumber);
            _Name = cardData.name;
            switch (cardData.category)
            {
                case "Monster": _Category = Category.Monster; break;
                case "Spell": _Category = Category.Spell; break;
                case "Trap": _Category = Category.Trap; break;
                default: throw new Exception("Card Category in raw DB has an invalid value; Value: " + cardData.category + " | card id: " + cardData.id);
            }
            switch (cardData.type)
            {
                case "Aqua": _Type = Type.Aqua; break;
                case "Beast": _Type = Type.Beast; break;
                case "Beast-Warrior": _Type = Type.BeastWarrior; break;
                case "Cyberse": _Type = Type.Cyberse; break;
                case "Dinosaur": _Type = Type.Dinosaur; break;
                case "Divine-Beast": _Type = Type.DivineBeast; break;
                case "Dragon": _Type = Type.Dragon; break;
                case "Fairy": _Type = Type.Fairy; break;
                case "Fiend": _Type = Type.Fiend; break;
                case "Fish": _Type = Type.Fish; break;
                case "Insect": _Type = Type.Insect; break;
                case "Machine": _Type = Type.Machine; break;
                case "Plant": _Type = Type.Plant; break;
                case "Psychic": _Type = Type.Psychic; break;
                case "Pyro": _Type = Type.Pyro; break;
                case "Reptile": _Type = Type.Reptile; break;
                case "Rock": _Type = Type.Rock; break;
                case "Sea Serpent": _Type = Type.SeaSerpent; break;
                case "Spellcaster": _Type = Type.Spellcaster; break;
                case "Thunder": _Type = Type.Thunder; break;
                case "Warrior": _Type = Type.Warrior; break;
                case "Winged Beast": _Type = Type.WingedBeast; break;
                case "Wyrm": _Type = Type.Wyrm; break;
                case "Zombie": _Type = Type.Zombie; break;
                case "Normal": _Type = Type.Normal; break;
                case "Continuous": _Type = Type.Continuous; break;
                case "Equip": _Type = Type.Equip; break;
                case "Field": _Type = Type.Field; break;
                case "Ritual": _Type = Type.Ritual; break;
                default: throw new Exception("Card Type in raw DB has an invalid value; Value: " + cardData.type + " | card id: " + cardData.id);
            }
            switch (cardData.sectype)
            {
                case "Normal": _SecType = SecType.Normal; break;
                case "Fusion": _SecType = SecType.Fusion; break;
                case "Ritual": _SecType = SecType.Ritual; break;
                case "NONE": _SecType = SecType.NONE; break;
                default: throw new Exception("Card SecType in raw DB has an invalid value; Value: " + cardData.sectype + " | card id: " + cardData.id);
            }
            switch (cardData.attribute)
            {
                case "DARK": _Attribute = Attribute.DARK; break;
                case "LIGHT": _Attribute = Attribute.LIGHT; break;
                case "WATER": _Attribute = Attribute.WATER; break;
                case "FIRE": _Attribute = Attribute.FIRE; break;
                case "EARTH": _Attribute = Attribute.EARTH; break;
                case "WIND": _Attribute = Attribute.WIND; break;
                case "DIVINE": _Attribute = Attribute.DIVINE; break;
                case "SPELL": _Attribute = Attribute.SPELL; break;
                case "TRAP": _Attribute = Attribute.TRAP; break;
                default: throw new Exception("Card atribute in raw DB has an invalid value; Value: " + cardData.attribute + " | card id: " + cardData.id);
            }
            _Atk = Convert.ToInt32(cardData.atk);
            _Def = Convert.ToInt32(cardData.def);
            _Lp = Convert.ToInt32(cardData.lp);
            _MonsterLevel = Convert.ToInt32(cardData.monsterLevel);
            _DiceLevel = Convert.ToInt32(cardData.diceLevel);
            for (int x = 0; x < 6; x++)
            {
                string face = "notset";
                switch (x)
                {
                    case 0: face = cardData.face1; break;
                    case 1: face = cardData.face2; break;
                    case 2: face = cardData.face3; break;
                    case 3: face = cardData.face4; break;
                    case 4: face = cardData.face5; break;
                    case 5: face = cardData.face6; break;
                }


                if (face.Contains("STAR")) { _Crest[x] = Crest.STAR; }
                if (face.Contains("MOV")) { _Crest[x] = Crest.MOV; }
                if (face.Contains("ATK")) { _Crest[x] = Crest.ATK; }
                if (face.Contains("DEF")) { _Crest[x] = Crest.DEF; }
                if (face.Contains("MAG")) { _Crest[x] = Crest.MAG; }
                if (face.Contains("TRAP")) { _Crest[x] = Crest.TRAP; }
                if (face.Contains("RITU")) { _Crest[x] = Crest.RITU; }

                if (face.Contains("1")) { _CrestValue[x] = 1; }
                if (face.Contains("2")) { _CrestValue[x] = 2; }
                if (face.Contains("3")) { _CrestValue[x] = 3; }
                if (face.Contains("4")) { _CrestValue[x] = 4; }
                if (face.Contains("5")) { _CrestValue[x] = 5; }
            }
            _OnSummonEffect = cardData.onSummonEffect;
            _ContEffect = cardData.continuousEffect;
            _IgnitionEffect = cardData.ignitionEffect;
            _Ability = cardData.ability;
            _FusionMaterial1 = cardData.fusionMaterial1;
            _FusionMaterial2 = cardData.fusionMaterial2;
            _FusionMaterial3 = cardData.fusionMaterial3;
            _RitualCard = cardData.ritualSpell;
            _EffectsImplemeted = cardData.effectsImplemented;
        }
        public CardInfo(Attribute attribute)
        {
            _Id = 0;
            _Name = attribute + " Symbol";
            _MonsterLevel = 1;
            _Attribute = attribute;
            _Type = Type.Symbol;
            _Category = Category.Symbol;
            _Atk = 0;
            _Def = 0;
            _Lp = 8000;
            _ContEffect = "Increase the ATK of all " + attribute + " monsters you control by 200.";
        }
        #endregion

        #region Public Accesors
        public int ID { get { return _Id; } }
        public int CardNumber { get { return _CardNumber; } }
        public string Name { get { return _Name; } }
        public int Level { get { return _MonsterLevel; } }
        public Attribute Attribute { get { return _Attribute; } }
        public Type Type { get { return _Type; } }
        public string TypeAsString
        {
            get
            {
                switch(_Type)
                {
                    case Type.BeastWarrior: return "Beast-Warrior";
                    case Type.DivineBeast: return "Divine-Beast";
                    case Type.SeaSerpent: return "Sea Serpent";
                    case Type.WingedBeast: return "Winged Beast";
                    default: return _Type.ToString();
                }
            }
        }
        public SecType SecType { get { return _SecType; } }
        public Category Category { get { return _Category; } }
        public int ATK { get { return _Atk; } }
        public int DEF { get { return _Def; } }
        public int LP { get { return _Lp; } }
        public bool IsFusion { get { return _SecType == SecType.Fusion; } }
        public bool IsRitual { get { return _SecType == SecType.Ritual; } }
        public int DiceLevel { get { return _DiceLevel; } }
        public string FusionMaterial1 { get { return _FusionMaterial1; } }
        public string FusionMaterial2 { get { return _FusionMaterial2; } }
        public bool HasThirdFusionMaterial { get { return _FusionMaterial3 != "-"; } }
        public string FusionMaterial3 { get { return _FusionMaterial3; } }
        public string RitualCard { get { return _RitualCard; } }
        public bool HasOnSummonEffect { get { return _OnSummonEffect != "-"; } }
        public bool HasContinuousEffect { get { return _ContEffect != "-"; } }  
        public bool HasIgnitionEffect { get { return _IgnitionEffect != "-"; } }
        public bool HasAbility { get {  return _Ability != "-"; } }
        public string OnSummonEffect {  get { return _OnSummonEffect; } }
        public string ContinuousEffect { get { return _ContEffect; } }
        public string IgnitionEffect {  get { return _IgnitionEffect; } }
        public string Ability { get { return _Ability; } }
        public bool EffectsAreImplemented { get { return _EffectsImplemeted; } }
        public Crest DiceFace(int index)
        {
            if(index < 0 || index > 5)
            {
                throw new Exception("Index to access a CardInfo's Dice Face Crest is invalid; index: " +index);
            }
            else
            {
                return _Crest[index];
            }
        }
        public int DiceFaceValue(int index)
        {
            if (index < 0 || index > 5)
            {
                throw new Exception("Index to access a CardInfo's Dice Face value is invalid; index: " + index);
            }
            else
            {
                return _CrestValue[index];
            }
        }
        public List<string> GetFusionMaterials()
        {
            List<string> fusionMaterials = new List<string>();
            fusionMaterials.Add(FusionMaterial1);
            fusionMaterials.Add(FusionMaterial2);
            if (HasThirdFusionMaterial) { fusionMaterials.Add(FusionMaterial3); }
            return fusionMaterials;
        }
        #endregion

        #region Private Data
        private int _Id;
        private int _CardNumber;
        private string _Name;
        private Category _Category;
        private Type _Type;
        private SecType _SecType;
        private Attribute _Attribute;
        private int _Atk;
        private int _Def;
        private int _Lp;
        private int _MonsterLevel;
        private int _DiceLevel;
        private Crest[] _Crest = new Crest[6];
        private int[] _CrestValue = new int[6];
        private string _OnSummonEffect;
        private string _ContEffect;
        private string _IgnitionEffect;
        private string _Ability;
        private string _FusionMaterial1;
        private string _FusionMaterial2;
        private string _FusionMaterial3;
        private string _RitualCard;
        private bool _EffectsImplemeted;
        #endregion
    }

    public enum Category
    {
        Monster,
        Spell,
        Trap,
        Symbol
    }
    public enum Type
    {
        Aqua,
        Beast,
        BeastWarrior,
        Cyberse,
        Dinosaur,
        DivineBeast,
        Dragon,
        Fairy,
        Fiend,
        Fish,
        Illusion,
        Insect,
        Machine,
        Plant,
        Psychic,
        Pyro,
        Reptile,
        Rock,
        SeaSerpent,
        Spellcaster,
        Thunder,
        Warrior,
        WingedBeast,
        Wyrm,
        Zombie,
        Normal,
        Continuous,
        Equip,
        Field,
        Ritual,
        Symbol
    }
    public enum SecType
    {
        NONE,
        Normal,
        Fusion,
        Ritual,
    }
    public enum Attribute
    {
        DARK,
        LIGHT,
        WATER,
        FIRE,
        EARTH,
        WIND,
        DIVINE,
        SPELL,
        TRAP
    }
    public enum Crest
    { 
        NONE,
        STAR,
        MOV,
        ATK,
        DEF,
        MAG,
        TRAP,
        RITU
    }
}
