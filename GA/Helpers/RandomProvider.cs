using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.Helpers
{
    public static class RandomProvider
    {
        private static Random _random = new Random();

        public static Random Current { get { return _random; } }
    }
}
