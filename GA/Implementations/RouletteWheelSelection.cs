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
    public class RouletteWheelSelection : ISelectionOperator
    {
        public void CalculateDistribuance(Individual[] currentPopulation, out List<double> distribuance)
        {
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

            //Console.WriteLine($"Individual: {currentIndex}");

            return currentPopulation[currentIndex].Clone();
        }

        public Individual[] GenerateParentPopulation(Individual[] currentPopulation)
        {
            List<double> distribuance;
            CalculateDistribuance(currentPopulation, out distribuance);

            // zmienna randomowa z generatora dla 2 i dalszych generacji przyjmuje zawsze wartosc 0 przez co wynik jest zły
            // naprawiłem to dodajac do generatora funkcje z lockiem i w kazdej innej klasie zadzialalo
            // tutaj niestety dziala tylko dla 1 generacji a potem sie psuje

            return currentPopulation
                .Select(x => SelectCurrentParent(RandomProvider.Current.NextDoubleLock(), currentPopulation, distribuance))
                .ToArray();
        }
    }
}
