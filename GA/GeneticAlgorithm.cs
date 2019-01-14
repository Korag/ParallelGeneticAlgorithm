using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GA.Abstracts;
using GA.BasicTypes;
using GA.Helpers;
using GA.Extensions;
using System.Diagnostics;

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

            // Sekwencyjnie
            //for (int i = 0; i < maxNumberOfGenerations; i++)
            //{
            //    var parents = _selectionOperator.GenerateParentPopulation(_population);

            //    for (int j = 0; j < _numberOfIndividuals - 1; j += 2)
            //    {
            //        if (_random.NextDouble() < CrossoverProbability)
            //        {
            //            _crossOperator.Crossover(parents[j], parents[j + 1]);

            //            _mutationOperator.Mutation(parents[j], MutationProbability);
            //            _mutationOperator.Mutation(parents[j + 1], MutationProbability);
            //        }
            //    }

            //    _population = parents;

            //    UpdateFitness();

            //    if (PrintStatistics)
            //    {
            //        Console.WriteLine($"Generation: {i}");
            //        Console.WriteLine($"The best is: x = {TakeTheBest().Chromosome.DecodedValue}\tf = {TakeTheBest().Fitness}");
            //    }
            //}

            // Parallel
            for (int i = 0; i < maxNumberOfGenerations; i++)
            {
                var parents = _selectionOperator.GenerateParentPopulation(_population);

                Parallel.For(0, _numberOfIndividuals - 1, j =>
                {
                    if (_random.NextDouble() < CrossoverProbability)
                    {
                        _crossOperator.Crossover(parents[j], parents[j + 1]);
                        _mutationOperator.Mutation(parents[j], MutationProbability);
                    }
                });

                _population = parents;

                UpdateFitness();
                if (PrintStatistics)
                {
                    Console.WriteLine($"Generation: {i}");
                    Console.WriteLine($"The best is: x = {TakeTheBest().Chromosome.DecodedValue}\tf = {TakeTheBest().Fitness}");
                }
            }


            // Taski
            //for (int i = 0; i < maxNumberOfGenerations; i++)
            //{
            //    var parents = _selectionOperator.GenerateParentPopulation(_population);

            //    double tasks = (_population.Length / 10000);
            //    int amountOfTask = (int)Math.Ceiling(tasks);
            //    Task[] t = new Task[amountOfTask];
            //    int LeftC = 0;
            //    int RightC = 10000;

            //    for (int k = 0; k < amountOfTask; k++)
            //    {
            //        if (k == amountOfTask - 1)
            //        {
            //            RightC = _population.Length - 1;
            //            t[k] = GeneticOperations(LeftC, RightC, parents);
            //        }
            //        else
            //        {
            //            t[k] = GeneticOperations(LeftC, RightC, parents);
            //            LeftC = RightC;
            //            RightC += 10000;
            //        }
            //    }

            //    for (int k = 0; k < amountOfTask; k++)
            //    {
            //        t[k].Wait();
            //    }

            //    _population = parents;

            //    UpdateFitness();

            //    if (PrintStatistics)
            //    {
            //        Console.WriteLine($"Generation: {i}");
            //        Console.WriteLine($"The best is: x = {TakeTheBest().Chromosome.DecodedValue}\tf = {TakeTheBest().Fitness}");
            //    }
            //}

            return TakeTheBest();
        }

        public Task GeneticOperations(int leftC, int rightC, Individual[] parents)
        {
            var task = new Task(() =>
            {
                for (int j = leftC; j < rightC; j++)
                {
                    if (_random.NextDouble() < CrossoverProbability)
                    {
                        _crossOperator.Crossover(parents[j], parents[j + 1]);

                        _mutationOperator.Mutation(parents[j], MutationProbability);
                        _mutationOperator.Mutation(parents[j + 1], MutationProbability);
                    }
                }
            });
            task.Start();
            return task;
        }

        private Individual TakeTheBest()
        {
            // LINQ
            //StopwatchProvider.StartStopWatch();
            //Console.WriteLine(_population
            //    .OrderByDescending(x => x.Fitness)
            //    .FirstOrDefault() );
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            // Without LINQ
            //StopwatchProvider.StartStopWatch();
            Individual max = new Individual();
            max = _population[1];
            for (int i = 1; i < _population.Length; i++)
            {
                if (max.Fitness < _population[i - 1].Fitness)
                {
                    max = _population[i - 1];
                }
            }
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            return max;
        }

        private void UpdateFitness()
        {
            foreach (var individual in _population)
            {
                individual.UpdateFitness(_fitnessFunction);
            }

            // Parallel
            //Parallel.ForEach(_population, individual =>
            //{
            //    individual.UpdateFitness(_fitnessFunction);
            //});

        }


        private void ResetSimulations()
        {
            _population = new Individual[_numberOfIndividuals];

            if (_population.Length < 50000)
            {
                //StopwatchProvider.StartStopWatch();
                for (int i = 0; i < _numberOfIndividuals; i++)
                {
                    _population[i] = new Individual(_chromosomeSize);
                    _population[i].UpdateFitness(_fitnessFunction);
                }
                //Console.WriteLine(StopwatchProvider.StopWatchTime());
            }
            else
            {
                //StopwatchProvider.StartStopWatch();
                Parallel.For(0, _population.Length, i =>
                {
                    _population[i] = new Individual(_chromosomeSize);
                    _population[i].UpdateFitness(_fitnessFunction);
                });
                //Console.WriteLine(StopwatchProvider.StopWatchTime());
            }
        }
    }
}

