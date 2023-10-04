//Joel Campos
//10/4/2023
//Game Data Class

namespace DungeonDiceMonsters
{
    public static class GameData
    {
        #region Public Accessors
        public static string Name { get{ return _PlayerName;} }
        public static int StarChips { get{ return _StarChips; } }
        #endregion

        #region Public Methods
        public static void UnlockCharacter(Character c)
        {
            _CharactersUnlocked[(int)c] = true;
        }
        public static bool IsCharacterUnlocked(Character character)
        {
            return _CharactersUnlocked[(int)character];
        }
        public static int GetCharacterWins(Character character)
        {
            return _CharactersWins[(int)character];
        }
        public static int GetCharacterLoss(Character character)
        {
            return _CharactersLoss[(int)character];
        }
        public static string CharacterName(Character c)
        {
            string name = c.ToString();
            return name.Replace("_", " ");
        }
        #endregion

        #region Data
        private static string _PlayerName = "tmp";
        private static int _StarChips = 0;
        private static  bool[] _CharactersUnlocked = new bool[35];
        private static  int[] _CharactersWins = new int[35];
        private static  int[] _CharactersLoss = new int[35];
        #endregion
    }

    public enum Character
    {
        Duel_Master_K,
        //Round 1
        Simon_Muran,
        //Round 2
        Villager_1,
        Villager_2,
        Villager_3,
        //Round 3
        Tea,
        //Round 4
        Weevil_Underwood,
        Rex_Raptor,
        //Round 5
        Mai_Valentine,
        Joey,
        Bandit_Keith,
        //Round 6
        Bakura,
        Pegasus,
        //Round 7
        Shadi,
        Ishizu,
        //Round 8
        Kaiba,
        Yami_Yugi,
        //Round 9
        Mage_Soldier,
        //Round 10
        Meadow_Mage,
        Ocean_Mage,
        Mountain_Mage,
        Desert_Mage,
        Forest_Mage,
        //Round 11
        High_Mage_Kepura,
        High_Mage_Secmenton,
        High_Mage_Antenza,
        High_Mage_Martis,
        High_Mage_Anubisius,
        //Round 12
        Labyrinth_Mage,
        //Round 13
        Neku,
        Sebek,
        //Round 14
        Seto,
        Heishin,
        //Round 15
        DarkNite,
        //Final Match
        NiteMare,
        //EXtra Chars... TBD
        Yugi
    }
}