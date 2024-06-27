//Joel Campos
//9/8/2023
//Storage Data Class

using System;
using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public static class StorageData
    {
        #region Data
        private static List<StorageSlot> Cards = new List<StorageSlot>();
        #endregion

        #region Public Method
        public static int GetCardID(int index)
        {
            return Cards[index].ID;
        }
        public static int GetCardIDInIndex(int index)
        {
            return Cards[index].ID;
        }
        public static int GetAmount(int index)
        {
            return Cards[index].Amount;
        }
        public static void AddCard(int id)
        {
            //Find if there is a storage slot with that card id
            int indexOfCard = -1;
            for(int x = 0; x < Cards.Count; x++) 
            {
                if (Cards[x].ID == id) 
                {
                    indexOfCard = x; break;
                }
            }

            if (indexOfCard == -1)
            {
                //Add a new card...
                Cards.Add(new StorageSlot(id));
            }
            else
            {
                //increase the existing card amount
                Cards[indexOfCard].IncreaseAmount(1);
            }
        }
        public static void RemoveCard(int id) 
        {
            //Find if there is a storage slot with that card id
            int indexOfCard = -1;
            for (int x = 0; x < Cards.Count; x++)
            {
                if (Cards[x].ID == id)
                {
                    indexOfCard = x; break;
                }
            }

            if (indexOfCard == -1)
            {
                //throw an exception, you cant remove a card id that is not the the storagfe
                throw new System.Exception("Card was tried to be remove from storage with an ID that is not in there. ID: " + id);
            }
            else
            {
                //reduce the amount -1 
                Cards[indexOfCard].ReduceAmount(1);
                if(Cards[indexOfCard].Amount == 0)
                {
                    //Remove the card from storage completely
                    Cards.RemoveAt(indexOfCard);
                }
            }
        }
        public static int GetCardCount()
        {
            return Cards.Count;
        }
        public static List<StorageSlot> GetCardList()
        {
            return Cards;
        }
        public static List<StorageSlot> FilterByType(Type thisType)
        {
            List<StorageSlot> output = new List<StorageSlot>();
            foreach (StorageSlot slot in Cards) 
            {
                CardInfo thisCardInfo = CardDataBase.GetCardWithID(slot.ID);
                if(thisCardInfo.Type == thisType)
                {
                    output.Add(slot);
                }
            }
            return output;
        }
        public static List<StorageSlot> FilterBySpellType(Type thisType)
        {
            List<StorageSlot> output = new List<StorageSlot>();
            foreach (StorageSlot slot in Cards)
            {
                CardInfo thisCardInfo = CardDataBase.GetCardWithID(slot.ID);
                if (thisCardInfo.Category == Category.Spell && thisCardInfo.Type == thisType)
                {
                    output.Add(slot);
                }
            }
            return output;
        }
        public static List<StorageSlot> FilterByTrapType(Type thisType)
        {
            List<StorageSlot> output = new List<StorageSlot>();
            foreach (StorageSlot slot in Cards)
            {
                CardInfo thisCardInfo = CardDataBase.GetCardWithID(slot.ID);
                if (thisCardInfo.Category == Category.Trap && thisCardInfo.Type == thisType)
                {
                    output.Add(slot);
                }
            }
            return output;
        }
        public static List<StorageSlot> FilterByCardType(Category category)
        {
            List<StorageSlot> output = new List<StorageSlot>();
            foreach (StorageSlot slot in Cards)
            {
                CardInfo thisCardInfo = CardDataBase.GetCardWithID(slot.ID);
                if (thisCardInfo.Category == category)
                {
                    output.Add(slot);
                }
            }
            return output;
        }
        public static List<StorageSlot> FilterByMonsterSecType(SecType secType)
        {
            List<StorageSlot> output = new List<StorageSlot>();
            foreach (StorageSlot slot in Cards)
            {
                CardInfo thisCardInfo = CardDataBase.GetCardWithID(slot.ID);
                if (thisCardInfo.Category == Category.Monster && thisCardInfo.SecType == secType)
                {
                    output.Add(slot);
                }
            }
            return output;
        }
        public static List<StorageSlot> FilterByAttribute(Attribute attribute)
        {
            List<StorageSlot> output = new List<StorageSlot>();
            foreach (StorageSlot slot in Cards)
            {
                CardInfo thisCardInfo = CardDataBase.GetCardWithID(slot.ID);
                if (thisCardInfo.Category == Category.Monster && thisCardInfo.Attribute == attribute)
                {
                    output.Add(slot);
                }
            }
            return output;
        }
        public static List<StorageSlot> FilterByText(string searchTerm)
        {
            List<StorageSlot> output = new List<StorageSlot>();
            foreach (StorageSlot slot in Cards)
            {
                CardInfo thisCardInfo = CardDataBase.GetCardWithID(slot.ID);
                string cardname = thisCardInfo.Name;
                cardname = cardname.ToLower();
                searchTerm = searchTerm.ToLower();
                if (cardname.Contains(searchTerm))
                {
                    output.Add(slot);
                }
            }
            return output;
        }
        public static List<StorageSlot> FilterByCrest(Crest thisCrest)
        {
            List<StorageSlot> output = new List<StorageSlot>();
            foreach (StorageSlot slot in Cards)
            {
                CardInfo thisCardInfo = CardDataBase.GetCardWithID(slot.ID);
                if (thisCardInfo.HasThisCrest(thisCrest))
                {
                    output.Add(slot);
                }
            }
            return output;
        }
        #endregion
    }

    public class StorageSlot: IComparable
    {
        public StorageSlot(int cardId)
        {
            _CardId = cardId;
            _Amount = 1;
        }

        public int ID {  get { return _CardId; } }
        public int Amount { get { return _Amount; } }
        public void IncreaseAmount(int amount)
        { 
            _Amount += amount; 
        }
        public void ReduceAmount(int amount)
        {
            _Amount -= amount;
        }

        private int _CardId = 0;
        private int _Amount = 0;

        public int CompareTo(object obj)
        {
            StorageSlot otherItem = obj as StorageSlot;
            return otherItem._CardId.CompareTo(_CardId);
        }
        public class SortByCardNumber : IComparer<StorageSlot>
        {
            public int Compare(StorageSlot c1, StorageSlot c2)
            {
                CardInfo card1 = CardDataBase.GetCardWithID(c1.ID);
                CardInfo card2 = CardDataBase.GetCardWithID(c2.ID);
                int cardNumber1 = card1.CardNumber;
                int cardNumber2 = card2.CardNumber;

                if (cardNumber1 < cardNumber2) { return -1; }
                else if (cardNumber1 > cardNumber2) { return 1; }
                else { return 0; }
            }
        }
        public class SortByCardName : IComparer<StorageSlot>
        {
            public int Compare(StorageSlot c1, StorageSlot c2)
            {
                CardInfo card1 = CardDataBase.GetCardWithID(c1.ID);
                CardInfo card2 = CardDataBase.GetCardWithID(c2.ID);
                string cardNumber1 = card1.Name;
                string cardNumber2 = card2.Name;
                return string.Compare(cardNumber1, cardNumber2);
            }
        }
        public class SortByLP : IComparer<StorageSlot>
        {
            public int Compare(StorageSlot c1, StorageSlot c2)
            {
                CardInfo card1 = CardDataBase.GetCardWithID(c1.ID);
                CardInfo card2 = CardDataBase.GetCardWithID(c2.ID);
                int cardNumber1 = card1.LP;
                int cardNumber2 = card2.LP;

                if (cardNumber1 < cardNumber2) { return 1; }
                else if (cardNumber1 > cardNumber2) { return -1; }
                else { return 0; }
            }
        }
        public class SortByATK : IComparer<StorageSlot>
        {
            public int Compare(StorageSlot c1, StorageSlot c2)
            {
                CardInfo card1 = CardDataBase.GetCardWithID(c1.ID);
                CardInfo card2 = CardDataBase.GetCardWithID(c2.ID);
                int cardNumber1 = card1.ATK;
                int cardNumber2 = card2.ATK;

                if (cardNumber1 < cardNumber2) { return 1; }
                else if (cardNumber1 > cardNumber2) { return -1; }
                else { return 0; }
            }
        }
        public class SortByDEF : IComparer<StorageSlot>
        {
            public int Compare(StorageSlot c1, StorageSlot c2)
            {
                CardInfo card1 = CardDataBase.GetCardWithID(c1.ID);
                CardInfo card2 = CardDataBase.GetCardWithID(c2.ID);
                int cardNumber1 = card1.DEF;
                int cardNumber2 = card2.DEF;

                if (cardNumber1 < cardNumber2) { return 1; }
                else if (cardNumber1 > cardNumber2) { return -1; }
                else { return 0; }
            }
        }
        public class SortByMonsterLV : IComparer<StorageSlot>
        {
            public int Compare(StorageSlot c1, StorageSlot c2)
            {
                CardInfo card1 = CardDataBase.GetCardWithID(c1.ID);
                CardInfo card2 = CardDataBase.GetCardWithID(c2.ID);
                int cardNumber1 = card1.Level;
                int cardNumber2 = card2.Level;

                if (cardNumber1 < cardNumber2) { return 1; }
                else if (cardNumber1 > cardNumber2) { return -1; }
                else { return 0; }
            }
        }
        public class SortByDiceLV : IComparer<StorageSlot>
        {
            public int Compare(StorageSlot c1, StorageSlot c2)
            {
                CardInfo card1 = CardDataBase.GetCardWithID(c1.ID);
                CardInfo card2 = CardDataBase.GetCardWithID(c2.ID);
                int cardNumber1 = card1.DiceLevel;
                int cardNumber2 = card2.DiceLevel;

                if (cardNumber1 < cardNumber2) { return 1; }
                else if (cardNumber1 > cardNumber2) { return -1; }
                else { return 0; }
            }
        }
    }
}