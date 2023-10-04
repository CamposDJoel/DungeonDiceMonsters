//Joel Campos
//9/1/2023
//CardDataBase Class

using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public static class CardDataBase
    {
        public static List<CardInfo> CardList;
        public static List<rawcardinfo> rawCardList;

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
    }
}
