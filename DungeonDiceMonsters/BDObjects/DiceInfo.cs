//Joel Campos
//9/1/2023
//DiceInfo Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace DungeonDiceMonsters
{
    public class DiceInfo
    {
        public DiceInfo(string level, string[] crests, string[] values)
        {
            _Level = Convert.ToInt32(level);
            for(int x = 0; x < 6; x++)
            {
                _Crests[x] = crests[x];
                _Values[x] = Convert.ToInt32(values[x]);
            }
        }

        public int Level
        {
            get { return _Level; }
        }
        public string Crest(int index)
        {
            return _Crests[index];
        }
        public int Value(int index)
        {
            return _Values[index];
        }

        private int _Level;
        private string[] _Crests = new string[6];
        private int[] _Values = new int[6];
    }
}
