using GA.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BasicTypes;
using GA.Helpers;
using GA.Extensions;

namespace GA.Implementations
{
    public class ClassicMutationOperator : IMutationOperator
    {
        public void Mutation(Individual individual, double mutationProbability)
        {
            //double[] randomNumbers;
            double[] randomNumbers2;

            //randomNumbers = LINQ(individual, mutationProbability);
            randomNumbers2 = WithoutLINQ(individual, mutationProbability);
        }

        private double[] LINQ(Individual individual, double mutationProbability)
        {
            var random = RandomProvider.Current;

            // LINQ
            //StopwatchProvider.StartStopWatch();
            var randomNumbers = individual
                .Chromosome.Genes
                .Select(x => random.NextDoubleLock())
                .ToArray();
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            return randomNumbers;
        }

        private double[] WithoutLINQ(Individual individual, double mutationProbability)
        {
            var random = RandomProvider.Current;

            // Without LINQ
            //StopwatchProvider.StartStopWatch();
            double[] randomNumbers = new double[individual.Chromosome.Genes.Length];
            for (int i = 0; i < individual.Chromosome.Genes.Length; i++)
            {
                randomNumbers[i] = random.NextDoubleLock();
            }
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            return randomNumbers;
        }
    }
}
