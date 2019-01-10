using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GA.Abstracts;
using GA.BasicTypes;

namespace GA
{
    class GeneticAlgorithm
    {
        private static Random _random = new Random();

        private ICrossOperator _crossOperator;
        private IMutationOperator _mutationOperator;
        private ISelectionOperator _selectionOperator;

        private Individual[] _population;
        private int _numberOfIndividuals;
        private int _chromosomeSize;
        Func<double, double> _fitnessFunction;

        public double CrossoverProbability { get; set; }
        public double MutationProbability { get; set; }
        public bool PrintStatistics { get; set; }

        public GeneticAlgorithm(int numberOfIndividuals, int chromosomeSize,
            ICrossOperator crossOperator,
            IMutationOperator mutationOperator,
            ISelectionOperator selectionOperator,
            Func<double, double> fitnessFunction)
        {
            _numberOfIndividuals = numberOfIndividuals;
            _chromosomeSize = chromosomeSize;
            _crossOperator = crossOperator;
            _mutationOperator = mutationOperator;
            _selectionOperator = selectionOperator;
            _fitnessFunction = fitnessFunction;
            CrossoverProbability = 0.90;
            MutationProbability = 0.05;
            PrintStatistics = false;
        }

        public Individual RunSimulation(int maxNumberOfGenerations)
        {
            ResetSimulations();

            for (int i = 0; i < maxNumberOfGenerations; i++)
            {
                var parents = _selectionOperator.GenerateParentPopulation(_population);

                for (int j = 0; j < _numberOfIndividuals - 1; j += 2)
                {
                    if (_random.NextDouble() < CrossoverProbability)
                    {
                        _crossOperator.Crossover(parents[j], parents[j + 1]);

                        _mutationOperator.Mutation(parents[j], MutationProbability);
                        _mutationOperator.Mutation(parents[j + 1], MutationProbability);
                    }
                }

                _population = parents;

                UpdateFitness();

                if (PrintStatistics)
                {
                    Console.WriteLine($"Generation: {i}");
                    Console.WriteLine($"The best is: x = {TakeTheBest().Chromosome.DecodedValue}\tf = {TakeTheBest().Fitness}");
                }
            }

            return TakeTheBest();
        }

        private Individual TakeTheBest()
        {
            return _population
                .OrderByDescending(x => x.Fitness)
                .FirstOrDefault();
        }

        private void UpdateFitness()
        {
            foreach (var individual in _population)
            {
                individual.UpdateFitness(_fitnessFunction);
            }
        }

        /// ????????? Zmieniamy coœ ?
        private void ResetSimulations()
        {
            _population = new Individual[_numberOfIndividuals];
            for (int i = 0; i < _numberOfIndividuals; i++)
            {
                _population[i] = new Individual(_chromosomeSize);
                _population[i].UpdateFitness(_fitnessFunction);
            }
        }
    }
}

