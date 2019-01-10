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
            return random.NextDouble() < probability ? true : false;
        }
    }
}

