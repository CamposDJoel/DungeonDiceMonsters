//Joel Campos
//9/8/2023
//Storage Data Class

using System.Collections.Generic;

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
