using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonDiceMonsters
{
    public static class DecksData
    {
        public static List<Deck> Decks = new List<Deck>();

        public static bool HasOneReadyDeck()
        {
            int readycount = 0;

            foreach (var deck in Decks) { if (deck.UseStatus) { readycount++; } }

            return readycount > 0;
        }
    }

    public class Deck
    {
        public Deck(string name)
        {
            _name = name;
        }

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
            if (thisCard.Category == "Monster") { _MonsterCardCount++; }
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
            if (thisCard.Category == "Monster") { _MonsterCardCount--; }
        }
        public void RemoveFusionAtIndex(int index)
        {
            _FusionList.RemoveAt(index);
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

        public string Name { get { return _name; } }
        public int MainDeckSize { get { return _CardList.Count; } }
        public int FusionDeckSize { get { return _FusionList.Count; } }
        public int MonsterCardsCount { get { return _MonsterCardCount; } }

        public bool UseStatus { 
            get 
            { 
                return MainDeckSize == 20 && MonsterCardsCount >= 10;
            } 
        }

        private string _name;
        private List<int> _CardList = new List<int>();
        private List<int> _FusionList = new List<int>();
        private int _MonsterCardCount = 0;
    }
}
