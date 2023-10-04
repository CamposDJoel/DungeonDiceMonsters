//Joel Campos
//10/3/2023
//Decks Data

using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public static class DecksData
    {
        public static Deck[] Decks = new Deck[3];

        public static bool HasOneReadyDeck()
        {
            int readycount = 0;

            foreach (var deck in Decks) { if (deck.UseStatus) { readycount++; } }

            return readycount > 0;
        }
    }

    public class Deck
    {
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
        #endregion

        #region Public Accessors
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
        private List<int> _CardList = new List<int>();
        private List<int> _FusionList = new List<int>();
        private int _MonsterCardCount = 0;
        private Attribute _Symbol = Attribute.DARK;
        #endregion
    }
}
