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

            var parent1Genome = parent1.Chromosome.Genes
                .SkipWhile((x, i) => i < crossPoint)
                .ToArray();

            var parent2Genome = parent2.Chromosome.Genes
                .SkipWhile((x, i) => i < crossPoint)
                .ToArray();

            parent1.InsertGenes(crossPoint, parent2Genome);
            parent2.InsertGenes(crossPoint, parent1Genome);
        }
    }
}