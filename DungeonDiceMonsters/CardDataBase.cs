﻿//Joel Campos
//9/1/2023
//CardDataBase Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
