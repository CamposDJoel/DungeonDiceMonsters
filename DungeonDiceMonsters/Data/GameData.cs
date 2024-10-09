//Joel Campos
//10/4/2023
//Game Data Class

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;

namespace DungeonDiceMonsters
{
    public static class GameData
    {
        #region Public Accessors
        public static string Name { get { return _PlayerName; } }
        public static int StarChips { get { return _StarChips; } }
        public static string AvatarName { get { return _PlayerAvatar.ToString(); } }
        public static int AvatarID { get { return (int)_PlayerAvatar; } }
        public static int Level { get { return _Playerlevel; } }
        public static int ExpPoints { get { return _PlayerExp; } }
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
            _Playerlevel = Convert.ToInt32(data[2]);
            _PlayerExp = Convert.ToInt32(data[3]);
            _PlayerAvatar = (Avatar)Convert.ToInt32(data[4]);
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
        public static int GetNextLevelExp()
        {
            return BaseLevelExp() + IncreaseAmount();

            int IncreaseAmount()
            {
                return (40 + (_Playerlevel * 10));
            }
        }
        public static int BaseLevelExp()
        {
            int sum = 0;
            for (int x = _Playerlevel - 1; x >= 1; x--)
            {
                int levelIncrease = (40 + (x * 10));
                sum += levelIncrease;
            }
            return sum;
        }
        public static void ChangeAvatarID(int id)
        {
            _PlayerAvatar = (Avatar)id;
            SaveFileManger.WriteSaveFile();
        }
        public static void ChangePlayerName(string newName)
        {
            _PlayerName = newName;
            SaveFileManger.WriteSaveFile();
        }
        public static string GetPlayerDataForPvPMatch()
        {
            return string.Format("{0}|{1}|{2}", _PlayerName, _Playerlevel, _PlayerAvatar);
        }
        public static void GainExp(int amount)
        {
            _PlayerExp += amount;
        }
        public static void LevelUp()
        {
            _Playerlevel++;
        }
        #endregion

        #region Data
        private static string _PlayerName = "tmp";
        private static int _StarChips = 0;
        private static int _Playerlevel = 1;
        private static int _PlayerExp = 0;
        private static Avatar _PlayerAvatar = Avatar.Duelist;
        private static  bool[] _CharactersUnlocked = new bool[35];
        private static  int[] _CharactersWins = new int[35];
        private static  int[] _CharactersLoss = new int[35];
        private static List<bool> _LibraryMarks = new List<bool>();
        private static List<bool> _ExchangeMarks = new List<bool>();
        #endregion

        private enum Avatar
        {
            Duelist,
            Yugi,
            Yami,
            Joey,
            Tea,
            Kaiba,
            Weevil,
            Rex,
            Mai,
            Mako,
            Bonz,
            BanditKeith,
            Panik,
            Pegasus,
            Bakura,
            Seeker,
            Arcana,
            Strings,
            Odion,
            Ichizu,
            Marik,
            Valon,
            Allister,
            Rafael,
            Dartz
        }
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