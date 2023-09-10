using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace DungeonDiceMonsters
{
    public static class SaveFileManger
    {
        public static void WriteSaveFile()
        {
            //This will hold the actual data. Each item in the list will be a line in the save file
            List<string> Lines = new List<string>();

            //Line[0] : The amount of decks saved.
            Lines.Add(DecksData.Decks.Count.ToString());

            //Line[1 - d] Where d is the int value of line [0] : each line will hold the card list of each deck
            for (int x = 0; x < DecksData.Decks.Count; x++)
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
                deckData = deckData + DecksData.Decks[x].Name;
                Lines.Add(deckData);
            }

            //Line[d + 1] : The list of cards in the storage
            string storageDataString = "";
            for (int x = 0; x < StorageData.Cards.Count; x++)
            {
                storageDataString = storageDataString + StorageData.Cards[x].ToString() + "|";                
            }
            Lines.Add(storageDataString);

            //Write the file
            File.WriteAllLines(Directory.GetCurrentDirectory() + "\\Save Files\\SaveFile.txt", Lines);
        }

        public static void ReadSaveFile()
        {
            //Stream that reads the actual save file.
            StreamReader SR_SaveFile = new StreamReader(
                Directory.GetCurrentDirectory() + "\\Save Files\\SaveFile.txt");

            string Line = "";
            string[] Tokens = new string[1];
            string[] Separator = new string[] { "|" };

            //Line[0] : The amount of decks s
            Line = SR_SaveFile.ReadLine();
            int totalDecks = Convert.ToInt32(Line);

            //Line[1 - d] Where d is the int value of line [0] : each line will hold the card list of each deck
            for (int x = 0; x < totalDecks; x++)
            {
                Line = SR_SaveFile.ReadLine();
                Tokens = new string[1];
                //Separate all the tokens
                Tokens = Line.Split(Separator, StringSplitOptions.None);

                //Initialize the 20 cards in the deck
                string deckname = Tokens[23];
                DecksData.Decks.Add(new Deck(deckname));

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

            //Line[d + 1] : The list of cards in the storage
            Line = SR_SaveFile.ReadLine();
            Tokens = new string[1];
            //Separate all the tokens
            Tokens = Line.Split(Separator, StringSplitOptions.None);
            for(int y = 0; y < Tokens.Length-1; y++)
            {
                int cardid = Convert.ToInt32(Tokens[y]);
                StorageData.AddCard(cardid);
            }

            //Close the stream
            SR_SaveFile.Close();
        }
    }
}
