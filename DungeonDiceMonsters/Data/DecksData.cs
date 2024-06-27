//Joel Campos
//10/3/2023
//Decks Data

using System;
using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public static class DecksData
    {
        #region Data
        private static List<Deck> _Decks = new List<Deck> ();
        #endregion

        #region Public Methods
        public static List<Deck> DecksList { get { return _Decks; } }
        public static void AddDeck(Deck newDeck)
        {
            _Decks.Add(newDeck);
        }
        public static void RemoveDeck(int index) 
        {
            _Decks.RemoveAt(index);
        }
        public static Deck GetDeckAtIndex(int index)
        {
            return _Decks[index];
        }
        public static bool HasOneReadyDeck()
        {
            int readycount = 0;

            foreach (var deck in _Decks) { if (deck.UseStatus) { readycount++; } }

            return readycount > 0;
        }
        public static int GetDecksCount()
        {
            return _Decks.Count;
        }
        public static Deck GetStarterDeck()
        {
            Deck starterDeck = new Deck("Starter Deck", Attribute.LIGHT);
            //LV1 Dice
            starterDeck.AddMainCard(8058240);
            starterDeck.AddMainCard(57935140);
            starterDeck.AddMainCard(85309439);
            starterDeck.AddMainCard(63515678);
            starterDeck.AddMainCard(1929294);
            //LV2 Dice
            starterDeck.AddMainCard(41158734);
            starterDeck.AddMainCard(80770678);
            starterDeck.AddMainCard(91939608);
            starterDeck.AddMainCard(79629370);
            starterDeck.AddMainCard(2863439);
            //LV3 Dice
            starterDeck.AddMainCard(31122090);
            starterDeck.AddMainCard(86088138);
            starterDeck.AddMainCard(75850803);
            starterDeck.AddMainCard(35565537);
            starterDeck.AddMainCard(2971090);
            //LV4 Dice
            starterDeck.AddMainCard(89631139);
            starterDeck.AddMainCard(25955164);
            starterDeck.AddMainCard(6740720);
            starterDeck.AddMainCard(62397231);
            starterDeck.AddMainCard(31447217);
            return starterDeck;
        }
        #endregion
    }

    public class Deck
    {
        #region Constructors
        public Deck(string name, Attribute symbol)
        {
            _Name = name;
            _Symbol = symbol;
        }
        public Deck(string[] data)
        {
            //Initialize the 20 cards in the deck
            string symbolstring = data[23];
            Attribute Symbol = Attribute.DIVINE;
            switch (symbolstring)
            {
                case "DARK": Symbol = Attribute.DARK; break;
                case "LIGHT": Symbol = Attribute.LIGHT; break;
                case "WATER": Symbol = Attribute.WATER; break;
                case "FIRE": Symbol = Attribute.FIRE; break;
                case "EARTH": Symbol = Attribute.EARTH; break;
                case "WIND": Symbol = Attribute.WIND; break;
            }

            _Name = data[24];
            ChangeSymbol(Symbol);

            for (int y = 0; y < 20; y++)
            {
                int cardid = Convert.ToInt32(data[y]);
                if (cardid != 0)
                {
                    AddMainCard(cardid);
                }
            }
            for (int y = 0; y < 3; y++)
            {
                int cardid = Convert.ToInt32(data[y + 20]);
                if (cardid != 0)
                {
                    AddFusionCard(cardid);
                }
            }
        }
        public Deck(string rawdata)
        {
            //Initialize the 20 cards in the deck
            string[] data = rawdata.Split('!');
            string symbolstring = data[23];
            Attribute Symbol = Attribute.DIVINE;
            switch (symbolstring)
            {
                case "DARK": Symbol = Attribute.DARK; break;
                case "LIGHT": Symbol = Attribute.LIGHT; break;
                case "WATER": Symbol = Attribute.WATER; break;
                case "FIRE": Symbol = Attribute.FIRE; break;
                case "EARTH": Symbol = Attribute.EARTH; break;
                case "WIND": Symbol = Attribute.WIND; break;
            }

            _Name = data[24];
            ChangeSymbol(Symbol);

            for (int y = 0; y < 20; y++)
            {
                int cardid = Convert.ToInt32(data[y]);
                if (cardid != 0)
                {
                    AddMainCard(cardid);
                }
            }
            for (int y = 0; y < 3; y++)
            {
                int cardid = Convert.ToInt32(data[y + 20]);
                if (cardid != 0)
                {
                    AddFusionCard(cardid);
                }
            }
        }
        #endregion

        #region Public Methods
        public int GetMainCardIDAtIndex(int index)
        {
            return _CardList[index];
        }
        public int GetFusionCardIDAtIndex(int index)
        {
            return _FusionList[index];
        }
        public void AddMainCard(int id)
        {
            _CardList.Add(id);
            CardInfo thisCard = CardDataBase.GetCardWithID(id);
            if (thisCard.Category == Category.Monster) { _MonsterCardCount++; }
        }
        public void AddFusionCard(int id)
        {
            _FusionList.Add(id);
        }
        public void RemoveMainAtIndex(int index)
        {
            int id = _CardList[index];
            _CardList.RemoveAt(index);
            CardInfo thisCard = CardDataBase.GetCardWithID(id);
            if (thisCard.Category == Category.Monster) { _MonsterCardCount--; }
        }
        public void RemoveFusionAtIndex(int index)
        {
            _FusionList.RemoveAt(index);
        }
        public void ChangeSymbol(Attribute symbol)
        {
            _Symbol = symbol;
        }
        public int GetCardCount(int cardId)
        {
            int count = 0;
            foreach (int idInDeck in _CardList)
            {
                if (idInDeck == cardId) { count++; }
            }
            return count;
        }
        public int GetCardCountWithDiceLevel(int diceLevel)
        {
            int count = 0;
            foreach (int idInDeck in _CardList)
            {
                CardInfo thisCard = CardDataBase.GetCardWithID(idInDeck);
                if(thisCard.DiceLevel == diceLevel) { count++; }
            }
            return count;
        }
        public List<int> GetCardsWithDiceLevelAndRemove(int diceLevel)
        {
            List<int> cardlist = new List<int>();

            for(int x = 0; x < _CardList.Count; x++)
            {
                CardInfo thisCard = CardDataBase.GetCardWithID(_CardList[x]);
                if (thisCard.DiceLevel == diceLevel) 
                { 
                    cardlist.Add(thisCard.ID);
                    _CardList.RemoveAt(x);
                    x--;
                }
            }

            //Remove the 

            return cardlist;
        }
        public Deck GetCopy()
        {
            Deck copy = new Deck(_Name, _Symbol);
            foreach (int thisCardID in _CardList)
            {
                copy.AddMainCard(thisCardID);
            }
            foreach (int thisCardID in _FusionList)
            {
                copy.AddFusionCard(thisCardID);
            }

            copy.ChangeSymbol(_Symbol);

            return copy;
        }
        public string GetDataStringLine()
        {
            string deckData = "";
            for (int y = 0; y < 20; y++)
            {
                if (y >= MainDeckSize)
                {
                    deckData += "0|";
                }
                else
                {
                    deckData = deckData + GetMainCardIDAtIndex(y) + "|";
                }
            }

            for (int y = 0; y < 3; y++)
            {
                if (y >= FusionDeckSize)
                {
                    deckData += "0|";
                }
                else
                {
                    deckData = deckData + GetFusionCardIDAtIndex(y) + "|";
                }
            }

            //Set symbol
            deckData = deckData + Symbol + "|";

            deckData = deckData + Name;
            return deckData;
        }
        public string GetDataStringLineForPVP()
        {
            string deckData = "";
            for (int y = 0; y < 20; y++)
            {
                if (y >= MainDeckSize)
                {
                    deckData += "0!";
                }
                else
                {
                    deckData = deckData + GetMainCardIDAtIndex(y) + "!";
                }
            }

            for (int y = 0; y < 3; y++)
            {
                if (y >= FusionDeckSize)
                {
                    deckData += "0!";
                }
                else
                {
                    deckData = deckData + GetFusionCardIDAtIndex(y) + "!";
                }
            }

            //Set symbol
            deckData = deckData + Symbol;
            return deckData;
        }
        public void Rename(string newName)
        {
            _Name = newName;
        }
        public void SendAllCardsToStorage()
        {
            for (int x = 0; x < MainDeckSize; x++)
            {
                int cardId = GetMainCardIDAtIndex(x);
                StorageData.AddCard(cardId);
            }
            for (int x = 0; x < FusionDeckSize; x++)
            {
                int cardId = GetFusionCardIDAtIndex(x);
                StorageData.AddCard(cardId);
            }
        }
        #endregion

        #region Public Accessors
        public string Name { get { return _Name; } }
        public int MainDeckSize { get { return _CardList.Count; } }
        public int FusionDeckSize { get { return _FusionList.Count; } }
        public int MonsterCardsCount { get { return _MonsterCardCount; } }
        public Attribute Symbol { get { return _Symbol; } }
        public bool UseStatus { 
            get 
            { 
                return MainDeckSize == 20 && MonsterCardsCount >= 10;
            } 
        }
        #endregion

        #region Data
        private string _Name = "";
        private List<int> _CardList = new List<int>();
        private List<int> _FusionList = new List<int>();
        private int _MonsterCardCount = 0;
        private Attribute _Symbol = Attribute.DARK;
        #endregion
    }
}
