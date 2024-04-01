//Joel Campos
//9/15/2023
//Save File Manager Class

using System;
using System.Collections.Generic;
using System.IO;

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

            //Line[1-2-3] : each line will hold the card list of each deck
            for (int x = 0; x < DecksData.Decks.Length; x++)
            {
                string deckData = "";
                for (int y = 0; y < 20; y++)
                {
                    if (y >= DecksData.Decks[x].MainDeckSize)
                    {
                        deckData += "0|";
                    }
                    else
                    {
                        deckData = deckData + DecksData.Decks[x].GetMainCardIDAtIndex(y) + "|";
                    }
                }

                for (int y = 0; y < 3; y++)
                {
                    if (y >= DecksData.Decks[x].FusionDeckSize)
                    {
                        deckData += "0|";
                    }
                    else
                    {
                        deckData = deckData + DecksData.Decks[x].GetFusionCardIDAtIndex(y) + "|";
                    }
                }

                //Set symbol
                deckData = deckData + DecksData.Decks[x].Symbol;
                Lines.Add(deckData);
            }

            //Line[d + 1] : The list of cards in the storage
            string storageDataString = "";
            string storageDataAmountString = "";
            for (int x = 0; x < StorageData.Cards.Count; x++)
            {
                storageDataString = storageDataString + StorageData.Cards[x].ID.ToString() + "|";
                storageDataAmountString = storageDataAmountString + StorageData.Cards[x].Amount.ToString() + "|";
            }
            Lines.Add(storageDataString);
            Lines.Add(storageDataAmountString);

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


            //Line[1-2-3]: each line will hold the card list of each deck
            for (int x = 0; x < DecksData.Decks.Length; x++)
            {
                Line = SR_SaveFile.ReadLine();
                string[] Tokens = Line.Split('|');

                //Initialize the 20 cards in the deck
                string symbolstring = Tokens[23];
                Attribute Symbol = Attribute.DIVINE;
                switch(symbolstring)
                {
                    case "DARK": Symbol = Attribute.DARK; break;
                    case "LIGHT": Symbol = Attribute.LIGHT; break;
                    case "WATER": Symbol = Attribute.WATER; break;
                    case "FIRE": Symbol = Attribute.FIRE; break;
                    case "EARTH": Symbol = Attribute.EARTH; break;
                    case "WIND": Symbol = Attribute.WIND; break;
                }

                DecksData.Decks[x] = new Deck();
                DecksData.Decks[x].ChangeSymbol(Symbol);

                for(int y = 0; y < 20; y++) 
                {
                    int cardid = Convert.ToInt32(Tokens[y]);
                    if(cardid != 0) 
                    {
                        DecksData.Decks[x].AddMainCard(cardid);
                    }
                }
                for (int y = 0; y < 3; y++)
                {
                    int cardid = Convert.ToInt32(Tokens[y+20]);
                    if (cardid != 0)
                    {
                        DecksData.Decks[x].AddFusionCard(cardid);
                    }
                }
            }

            //Line[4] : The list of cards in the storage
            Line = SR_SaveFile.ReadLine();
            string[] CardIDs = Line.Split('|');

            //Line[5] : The amounts of the cards in storage
            Line = SR_SaveFile.ReadLine();
            string[] Amounts = Line.Split('|');

            //Add the cards
            for (int y = 0; y < CardIDs.Length-1; y++)
            {
                int amount = Convert.ToInt32(Amounts[y]);
                int cardid = Convert.ToInt32(CardIDs[y]);

                for(int x = 0; x < amount; x++)
                {
                    StorageData.AddCard(cardid);
                }
            }         

            //Close the stream
            SR_SaveFile.Close();
        }
    }
}
