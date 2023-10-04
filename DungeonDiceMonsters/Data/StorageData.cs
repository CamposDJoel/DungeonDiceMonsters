//Joel Campos
//9/8/2023
//Storage Data Class

using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public static class StorageData
    {
        public static List<StorageSlot> Cards = new List<StorageSlot>();

        public static int GetCardID(int index)
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
    }

    public class StorageSlot
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
    }
}
