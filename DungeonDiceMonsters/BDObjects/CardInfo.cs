﻿//Joel Campos
//9/1/2023
//CardInfo Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DungeonDiceMonsters
{
    public class CardInfo
    {
        public CardInfo(rawcardinfo rawdata) 
        {
            _ID = rawdata.id;
            _Name = rawdata.name;
            _Level = rawdata.level;
            _Attribute = rawdata.attribute;
            _Type = rawdata.type;
            _Category = rawdata.category;
            _ATK = rawdata.atk;
            _DEF = rawdata.def;
            _LP = rawdata.lp;
            _SetPack = rawdata.setpack;
            _Rarity = rawdata.rarity;
            _IsFusion = rawdata.fusion;

            rawdiceinfo diceinfo = rawdata.diceinforaw[0];
            string[] crests = new string[6];
            crests[0] = diceinfo.crest1;
            crests[1] = diceinfo.crest2;
            crests[2] = diceinfo.crest3;
            crests[3] = diceinfo.crest4;
            crests[4] = diceinfo.crest5;
            crests[5] = diceinfo.crest6;

            string[] values = new string[6];
            values[0] = diceinfo.value1;
            values[1] = diceinfo.value2;
            values[2] = diceinfo.value3;
            values[3] = diceinfo.value4;
            values[4] = diceinfo.value5;
            values[5] = diceinfo.value6;

            _DiceInfo = new DiceInfo(diceinfo.level, crests, values);
        }

        public int ID { get { return _ID; } }
        public string Name { get { return _Name; } }
        public int Level {  get { return _Level; } }
        public string Attribute { get { return _Attribute; } }
        public string Type { get { return _Type; } }
        public string Category { get { return _Category; } }
        public int ATK { get { return _ATK; } }
        public int DEF {  get { return _DEF; } }
        public int LP {  get { return _LP; } }
        public bool IsFusion {  get { return _IsFusion; } }
        public bool IsRitual { get 
            {
                bool isritual = _DiceInfo.HasRitualFaces() && _Category == "Monster";
                return isritual;
            }
        }

        private int _ID;
        private string _Name;
        private int _Level;
        private string _Attribute;
        private string _Type;
        private string _Category;
        private int _ATK;
        private int _DEF;
        private int _LP;
        private DiceInfo _DiceInfo;
        private string _SetPack;
        private string _Rarity;
        private bool _IsFusion;
    }
}
