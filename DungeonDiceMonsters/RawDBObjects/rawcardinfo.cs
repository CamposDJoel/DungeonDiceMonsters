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
            //Separator used by Split()
            string[] separator = new string[] { "\t" };
            //Array of tokens (Size will be rewritten when Split() is called)
            string[] tokens = new string[1];
            //Split the data
            tokens = dataLine.Split(separator, StringSplitOptions.None);

            id = tokens[1];
            name = tokens[0];
            category = tokens[2];
            type = tokens[3];
            sectype = tokens[4];
            attribute = tokens[5];
            atk = tokens[6];
            def = tokens[7];
            lp = tokens[8];
            monsterLevel = tokens[9];
            diceLevel = tokens[10];
            face1 = tokens[11];
            face2 = tokens[12];
            face3 = tokens[13];
            face4 = tokens[14];
            face5 = tokens[15];
            face6 = tokens[16];
            onSummonEffect = tokens[17];
            continuousEffect = tokens[18];
            ability = tokens[19];
            ignitionEffect = tokens[20];
            fusionMaterial1 = tokens[21];
            fusionMaterial2 = tokens[22];
            fusionMaterial3 = tokens[23];
            ritualSpell = tokens[24];
            setpack = "NONE";
            rarity = "Common";

        }*/
        public string id { get; set; }
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
        public string setpack { get; set; }
        public string rarity { get; set; }
    }
}
