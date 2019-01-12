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

            // Parallel - bad option
            //StopwatchProvider.StartStopWatch();
            //Parallel.For(0, Genes.Length, i =>
            //{
            //    Genes[i] = random.NextBool();
            //});
            //StopwatchProvider.DisplayStopWatchTime();
        }

        // Without any sense
        //public static Task<double> SumAsync(int leftC, int rightC, bool[] Array)
        //{
        //    var task = new Task<double>(() =>
        //    {
        //        double sum = 0;

        //        for (int i = leftC; i < rightC; i++)
        //        {
        //            if (Array[i] == true)
        //            {
        //                sum += Math.Pow(2, i);
        //            }

        //        }
        //        return sum;
        //    });
        //    task.Start();
        //    return task;
        //}

        //private double GetDecodedValue()
        //{
        //    double localsum = 0;

        //    // LINQ
        //    StopwatchProvider.StartStopWatch();
        //    double linqsum = (Genes.Reverse().Select((x, i) => (x ? Math.Pow(2, i) : 0)).Sum());
        //    Console.WriteLine(StopwatchProvider.StopWatchTime());

        //    StopwatchProvider.StartStopWatch();
        //    if (Genes.Length > 1000)
        //    {
        //        double tasks = (Genes.Length / 1000);
        //        int amountOfTask = (int)Math.Ceiling(tasks);
        //        Task<double>[] t = new Task<double>[amountOfTask];
        //        int LeftC = 0;
        //        int RightC = 1000;

        //        for (int i = 0; i < amountOfTask; i++)
        //        {
        //            if (i != amountOfTask - 1)
        //            {

        //            }
        //            else
        //            {
        //                t[i] = SumAsync(LeftC, RightC, Genes);
        //                LeftC = RightC;
        //                RightC += 1000;
        //            }
        //        }

        //        for (int i = 0; i < amountOfTask; i++)
        //        {
        //            localsum += t[i].Result;
        //        }
        //    }
        //    else
        //    {
        //        Array.Reverse(Genes);
        //        for (int i = 0; i < Genes.Length; i++)
        //        {
        //            if (Genes[i] == true)
        //            {
        //                localsum += Math.Pow(2, i);
        //            }
        //        }
        //        Console.WriteLine(StopwatchProvider.StopWatchTime());
        //    }


        //    return localsum;
        //}


        private double GetDecodedValue()
        {
            double localsum = 0;

            // LINQ
            //StopwatchProvider.StartStopWatch();
            //double linqsum = (Genes.Reverse().Select((x, i) => (x ? Math.Pow(2, i) : 0)) .Sum());
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            // Without LINQ
            // StopwatchProvider.StartStopWatch();
            for (int i = Genes.Length-1, j=0; i >= 0; i--, j++)
            {
                if (Genes[i] == true)
                {
                    localsum += Math.Pow(2, j);
                }
            }
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            return localsum;
        }

        public ChromosomeType Clone()
        {
            var result = new ChromosomeType(Size);

            // LINQ
            //StopwatchProvider.StartStopWatch();
            //result.Genes = Genes.Select(x => x).ToArray();
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            // Without LINQ
            //StopwatchProvider.StartStopWatch();
            Array.Copy(Genes, result.Genes, Genes.Length);
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            return result;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }

}
