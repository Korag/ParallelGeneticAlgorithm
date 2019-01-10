using GA.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BasicTypes;
using GA.Helpers;

namespace GA.Implementations
{
    public class ClassicMutationOperator : IMutationOperator
    {
        public void Mutation(Individual individual, double mutationProbability)
        {
            var random = RandomProvider.Current;

            var randomNumbers = individual
                .Chromosome.Genes
                .Select(x => random.NextDouble())
                .ToArray();

            individual.ReplaceGenes(individual
                .Chromosome.Genes
                .Zip(randomNumbers, (gene, prob) => prob <= mutationProbability ? !gene : gene)
                .ToArray());
        }
    }
}
