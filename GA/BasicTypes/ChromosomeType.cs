using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GA.Extensions;
using GA.Helpers;
using GA.Abstracts;

namespace GA.BasicTypes
{
    public class ChromosomeType : ICloneable<ChromosomeType>
    {
        public int Size { get { return Genes.Count(); } }

        public bool this[int index]
        {
            get { return Genes[index]; }
            set { Genes[index] = value; }
        }

        public bool[] Genes { get; set; }
        public double DecodedValue { get { return GetDecodedValue(); } }

        public ChromosomeType(int chromosomeSize)
        {
            var random = RandomProvider.Current;

            //StopwatchProvider.StartStopWatch();
            Genes = new bool[chromosomeSize];
            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = random.NextBool();
            }
            //StopwatchProvider.DisplayStopWatchTime();

            //StopwatchProvider.StartStopWatch();
            //Parallel.For(0, Genes.Length, i =>
            //{
            //    Genes[i] = random.NextBool();
            //});
            //StopwatchProvider.DisplayStopWatchTime();
        }

        private double GetDecodedValue()
        {
            return Genes
                .Reverse()
                .Select((x, i) => (x ? Math.Pow(2, i) : 0))
                .Sum();
        }

        public ChromosomeType Clone()
        {
            var result = new ChromosomeType(Size);

            result.Genes = Genes
                .Select(x => x)
                .ToArray();

            return result;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }

}
