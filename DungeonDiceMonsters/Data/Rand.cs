using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DungeonDiceMonsters
{
    public static class Rand
    {
        #region Public Member Functions
        /// <summary>
        /// Generates a random number between 1 and 100 for probability calculatios.
        /// </summary>
        /// <returns>A int between 1 and 100</returns>
        public static int Pro()
        {
            return R.Next(99) + 1;
        }
        /// <summary>
        /// Generates a random number with a limit max value range.
        /// </summary>
        /// <param name="MaxValue">The Max value that can be genrated.</param>
        /// <returns>An int between 0 and the Max Value provided.</returns>
        public static int V(int MaxValue)
        {
            return R.Next(MaxValue);
        }
        /// <summary>
        /// Generates a random number with a limit Min and Max.
        /// </summary>
        /// <param name="min">Minimun Value that can be generated.</param>
        /// <param name="max">Maximun Value that can be generated.</param>
        /// <returns>An int within the Range value.</returns>
        public static int Range(int min, int max)
        {
            return (min + R.Next(max - min));
        }
        /// <summary>
        /// Tells if an event will happen based on a probability of happening.
        /// </summary>
        /// <param name="Probability">The probability of the event of happening. (ie 50 == 50%)</param>
        /// <returns>True if the event will happen.</returns>
        public static bool WillHappen(int Probability)
        {
            int range = Pro();
            return range <= Probability;
        }

        public static int DiceRoll()
        {
            return (R.Next(6));
        }
        #endregion

        #region Data
        private static Random R = new Random();
        #endregion
    }
}
