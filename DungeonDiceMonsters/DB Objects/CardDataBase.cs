//Joel Campos
//9/1/2023
//CardDataBase Class

using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public static class CardDataBase
    {
        private static List<CardInfo> CardList = new List<CardInfo>();
        public static List<rawcardinfo> rawCardList = new List<rawcardinfo>();

        public static int CardCount { get { return CardList.Count; } }
        public static void AddCardToDB(CardInfo thisCardInfo)
        {
            CardList.Add(thisCardInfo);
            GameData.AddLibraryCard();
            GameData.AddExchangeCardRecord();
        }
        public static CardInfo GetCardWithID(int id)
        {
            CardInfo cardtoreturn = null;

            for(int x = 0; x < CardList.Count; x++) 
            {
                if (CardList[x].ID ==  id)
                {
                    cardtoreturn = CardList[x];
                    break;
                }
            }

            return cardtoreturn;
        }
        public static CardInfo GetCardWithName(string name)
        {
            CardInfo cardtoreturn = null;

            for (int x = 0; x < CardList.Count; x++)
            {
                if (CardList[x].Name == name)
                {
                    cardtoreturn = CardList[x];
                    break;
                }
            }

            return cardtoreturn;
        }
        public static CardInfo GetCardWithCardNo(int no)
        {
            CardInfo cardtoreturn = null;

            for (int x = 0; x < CardList.Count; x++)
            {
                if (CardList[x].CardNumber == no)
                {
                    cardtoreturn = CardList[x];
                    break;
                }
            }

            return cardtoreturn;
        }
        public static int GetRandomCardID()
        {
            int cardDBCardCount = CardList.Count;
            int rand = Rand.Range(0, cardDBCardCount);
            return CardList[rand].ID;

        }
    }
}
