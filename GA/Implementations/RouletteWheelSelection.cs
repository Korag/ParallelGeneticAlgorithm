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
    public class RouletteWheelSelection : ISelectionOperator
    {
        public void CalculateDistribuance(Individual[] currentPopulation, out List<double> distribuance)
        {
            var random = RandomProvider.Current;
            var sumOfFitness = currentPopulation
                .Sum(x => x.Fitness);

            double cummulativeFitness = 0;
            distribuance = currentPopulation
                .Select(x =>
                {
                    cummulativeFitness += x.Fitness / sumOfFitness;
                    return cummulativeFitness;
                })
                .ToList();
        }

        private Individual SelectCurrentParent(double random, Individual[] currentPopulation, List<double> distribuance)
        {

            var currentIndex = distribuance
                .Select((x, i) => new { Index = i, Value = x })
                .FirstOrDefault(x => x.Value > random).Index;

            Console.WriteLine($"Individual: {currentIndex}");

            return currentPopulation[currentIndex].Clone();
        }

        public Individual[] GenerateParentPopulation(Individual[] currentPopulation)
        {
            var random = RandomProvider.Current;

            List<double> distribuance;
            CalculateDistribuance(currentPopulation, out distribuance);

            return currentPopulation
                .Select(x => SelectCurrentParent(random.NextDouble(), currentPopulation, distribuance))
                .ToArray();
        }
    }
}
