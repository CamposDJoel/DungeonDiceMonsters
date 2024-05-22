//Joel Campos
//10/3/2023
//Raw Card Info (Ver 2) Class

using System;

namespace DungeonDiceMonsters
{
    public class rawcardinfo
    {
        /*public rawcardinfo(string dataLine) 
        {
            string[] tokens = dataLine.Split('|');

            id = tokens[2];
            name = tokens[0];
            cardNumber = tokens[1];
            category = tokens[3];
            type = tokens[4];
            sectype = tokens[5];
            attribute = tokens[6];
            atk = tokens[7];
            def = tokens[8];
            lp = tokens[9];
            monsterLevel = tokens[10];
            diceLevel = tokens[11];
            face1 = tokens[12];
            face2 = tokens[13];
            face3 = tokens[14];
            face4 = tokens[15];
            face5 = tokens[16];
            face6 = tokens[17];
            onSummonEffect = tokens[18];
            continuousEffect = tokens[19];
            ability = tokens[20];
            ignitionEffect = tokens[21];
            fusionMaterial1 = tokens[22];
            fusionMaterial2 = tokens[23];
            fusionMaterial3 = tokens[24];
            ritualSpell = tokens[25];
            effectsImplemented = Convert.ToBoolean(tokens[26]);
        }*/
        public string id { get; set; }
        public string cardNumber { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string sectype { get; set; }
        public string attribute { get; set; }
        public string atk { get; set; }
        public string def { get; set; }
        public string lp { get; set; }
        public string monsterLevel { get; set; }
        public string diceLevel { get; set; }
        public string face1 { get; set; }
        public string face2 { get; set; }
        public string face3 { get; set; }
        public string face4 { get; set; }
        public string face5 { get; set; }
        public string face6 { get; set; }
        public string onSummonEffect { get; set; }
        public string continuousEffect { get; set; }
        public string ability { get; set; }
        public string ignitionEffect { get; set; }
        public string fusionMaterial1 { get; set; }
        public string fusionMaterial2 { get; set; }
        public string fusionMaterial3 { get; set; }
        public string ritualSpell { get; set; }
        public bool effectsImplemented { get; set; }
    }
}
