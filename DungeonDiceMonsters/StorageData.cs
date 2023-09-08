using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonDiceMonsters
{
    public static class StorageData
    {
        public static List<int> Cards = new List<int>();

        public static void AddCard(int id)
        {  
            Cards.Add(id); 
        }
    }
}
