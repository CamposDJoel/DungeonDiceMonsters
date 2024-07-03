//Joel Campos
//10/4/2023
//Game Data Class

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;

namespace DungeonDiceMonsters
{
    public static class GameData
    {
        #region Public Accessors
        public static string Name { get{ return _PlayerName;} }
        public static int StarChips { get{ return _StarChips; } }
        #endregion

        #region Public Methods
        public static void SetPlayerName(string playerName)
        {
            _PlayerName = playerName;
        }
        public static void LoadGameData(string[] data)
        {
            _PlayerName = data[0];
            _StarChips = Convert.ToInt32(data[1]);
        }
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
        public static void AddLibraryCard()
        {
            _LibraryMarks.Add(false);
        }
        public static void AddExchangeCardRecord()
        {
            _ExchangeMarks.Add(false);
        }
        public static void MarkLibraryCardObtained(int index)
        {
            _LibraryMarks[index] = true;
        }
        public static void MarkLibraryCardObtainedbyID(int id)
        {
            _LibraryMarks[CardDataBase.GetCardWithID(id).CardNumber - 1] = true;
        }
        public static void MarkExchangeCardObtained(int index)
        {
            _ExchangeMarks[index] = true;
        }
        public static string GetLibraryMarksLine()
        {
            StringBuilder sb = new StringBuilder();
            foreach (bool thisMark in _LibraryMarks)
            {
                if (thisMark)
                {
                    sb.Append("1|");
                }
                else
                {
                    sb.Append("0|");
                }
            }

            return sb.ToString();
        }
        public static string GetExchangeMarksLine()
        {
            StringBuilder sb = new StringBuilder();
            foreach (bool thisMark in _ExchangeMarks)
            {
                if (thisMark)
                {
                    sb.Append("1|");
                }
                else
                {
                    sb.Append("0|");
                }
            }

            return sb.ToString();
        }
        public static void AdjustStarchipsAmount(int amount)
        {
            _StarChips += amount;
        }
        public static void InitializeLibraryMarksFromSaveFile(string data)
        {
            string[] Tokens = data.Split('|');

            for (int x = 0; x < Tokens.Length - 1; x++) 
            {
                string token = Tokens[x];
                if (token.Equals("1")) { MarkLibraryCardObtained(x); }
            }
        }
        public static void InitializeExchangeMarksFromSaveFile(string data)
        {
            string[] Tokens = data.Split('|');

            for (int x = 0; x < Tokens.Length - 1; x++)
            {
                string token = Tokens[x];
                if (token.Equals("1")) { MarkExchangeCardObtained(x); }
            }
        }
        public static int GetLibraryCollectionCount()
        {
            int count = 0;

            foreach (bool thisMark in _LibraryMarks) { if (thisMark) { count++; } }

            return count;
        }
        public static bool IsLibraryCardObtainedAtIndex(int index)
        {
            return _LibraryMarks[index];
        }
        public static bool IsExchangeCardObtainedAtIndex(int index)
        {
            return _ExchangeMarks[index];
        }
        public static void MarkDeckCardsAsLibraryObtained(Deck thisDeck)
        {
            for (int x = 0; x < thisDeck.MainDeckSize; x++) 
            {
                int thisMainCardID = thisDeck.GetMainCardIDAtIndex(x);
                CardInfo thisMainCardInfo = CardDataBase.GetCardWithID(thisMainCardID);
                MarkLibraryCardObtained(thisMainCardInfo.CardNumber - 1);
            }
            for (int x = 0; x < thisDeck.FusionDeckSize; x++)
            {
                int thisMainCardID = thisDeck.GetFusionCardIDAtIndex(x);
                CardInfo thisMainCardInfo = CardDataBase.GetCardWithID(thisMainCardID);
                MarkLibraryCardObtained(thisMainCardInfo.CardNumber - 1);
            }

        }
        #endregion

        #region Data
        private static string _PlayerName = "tmp";
        private static int _StarChips = 0;
        private static  bool[] _CharactersUnlocked = new bool[35];
        private static  int[] _CharactersWins = new int[35];
        private static  int[] _CharactersLoss = new int[35];
        private static List<bool> _LibraryMarks = new List<bool>();
        private static List<bool> _ExchangeMarks = new List<bool>();
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