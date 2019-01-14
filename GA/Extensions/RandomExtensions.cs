using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GA.Extensions
{
    public static class RandomExtensions
    {
        public static bool NextBool(this Random random, double probability = 0.5)
        {
            lock (random)
            {
                return random.NextDouble() < probability ? true : false;
            }
        }

        // Nie potrzebne jezeli jest dodatkowa zmienna Random w Roulette 
        public static double NextDoubleLock(this Random random)
        {
            lock (random)
            {
                return random.NextDouble();
            }
        }
    }
}

