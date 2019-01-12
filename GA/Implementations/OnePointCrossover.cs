using System;
using GA.Abstracts;
using GA.BasicTypes;
using System.Linq;
using GA.Helpers;

namespace GA.Implementations
{
    class OnePointCrossover : ICrossOperator
    {
        public void Crossover(Individual parent1, Individual parent2)
        {
            var random = RandomProvider.Current;

            int crossPoint = random.Next(1, parent1.Chromosome.Size - 1);

            //Console.WriteLine($"Cross point: {crossPoint}");

            // LINQ
            //StopwatchProvider.StartStopWatch();
            //var parent1Genome = parent1.Chromosome.Genes
            //    .SkipWhile((x, i) => i < crossPoint)
            //    .ToArray();
       
            //var parent2Genome = parent2.Chromosome.Genes
            //    .SkipWhile((x, i) => i < crossPoint)
            //    .ToArray();
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            // Without LINQ
            bool[] parent1Genome2 = new bool[parent1.Chromosome.Genes.Length];
            bool[] parent2Genome2 = new bool[parent2.Chromosome.Genes.Length];

            //StopwatchProvider.StartStopWatch();
            int iterator = 0;
            for (int i = 0; i < parent1.Chromosome.Genes.Length; i++)
            {
                if (i >= crossPoint)
                {
                    parent1Genome2[iterator] = parent1.Chromosome.Genes[i];
                    parent2Genome2[iterator] = parent1.Chromosome.Genes[i];
                    iterator++;
                }
            }
            iterator = 0;
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            parent1.InsertGenes(crossPoint, parent2Genome2);
            parent2.InsertGenes(crossPoint, parent1Genome2);
        }
    }
}