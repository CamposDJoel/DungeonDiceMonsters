//Joel Campos
//9/15/2023
//Save File Manager Class

using System;
using System.Collections.Generic;
using System.IO;
using static System.Windows.Forms.LinkLabel;

namespace DungeonDiceMonsters
{
    public static class SaveFileManger
    {
        public static void WriteSaveFile()
        {
            //This will hold the actual data. Each item in the list will be a line in the save file
            List<string> Lines = new List<string>();

            //Line [0]: Will contain the Players name and Starchip count
            string playerdata = string.Format("{0}|{1}", GameData.Name, GameData.StarChips);
            Lines.Add(playerdata);

            //Line 1 : The total deck count
            Lines.Add(DecksData.GetDecksCount().ToString());

            //Line[2 - n] : each line will hold the card list of each deck
            for (int x = 0; x < DecksData.GetDecksCount(); x++)
            {
                string deckData = DecksData.GetDeckAtIndex(x).GetDataStringLine();              
                Lines.Add(deckData);
            }

            //Line[2+n + 1]: Storage card count
            int storageCardCount = StorageData.GetCardCount();
            Lines.Add(storageCardCount.ToString());

            //Line [storagecount + n]: each card
            for (int x = 0; x < storageCardCount; x++)
            {
                int cardId = StorageData.GetCardID(x);
                string amount = StorageData.GetAmount(x).ToString();
                string cardName = CardDataBase.GetCardWithID(cardId).Name;

                Lines.Add(string.Format("{0}|{1}|{2}", cardId.ToString(), amount, cardName));
            }

            //Write the file
            File.WriteAllLines(Directory.GetCurrentDirectory() + "\\Save Files\\SaveFile.txt", Lines);
        }
        public static void ReadSaveFile()
        {
            //Stream that reads the actual save file.
            StreamReader SR_SaveFile = new StreamReader(
                Directory.GetCurrentDirectory() + "\\Save Files\\SaveFile.txt");

            string Line = "";

            //Line[0]: contains the Player's Name and Starchip count
            Line = SR_SaveFile.ReadLine();
            string[] gamedata = Line.Split('|');
            GameData.LoadGameData(gamedata);

            //Line 1 : The total deck count
            Line = SR_SaveFile.ReadLine();
            int totalDeckCount = Convert.ToInt32(Line);


            //Line[1-2-3]: each line will hold the card list of each deck
            for (int x = 0; x < totalDeckCount; x++)
            {
                Line = SR_SaveFile.ReadLine();
                string[] Tokens = Line.Split('|');
                DecksData.AddDeck(new Deck(Tokens));
            }

            //Line[4]: Storage Card Count
            Line = SR_SaveFile.ReadLine();
            int storageCardCount = Convert.ToInt32(Line);

            //Line [5 + n]: each card in the storage
            for (int y = 0; y < storageCardCount; y++)
            {
                Line = SR_SaveFile.ReadLine();
                string[] tokens = Line.Split('|');

                int cardid = Convert.ToInt32(tokens[0]);
                int amount = Convert.ToInt32(tokens[1]);
                
                for (int x = 0; x < amount; x++)
                {
                    StorageData.AddCard(cardid);
                }
            }       

            //Close the stream
            SR_SaveFile.Close();
        }
    }
}
