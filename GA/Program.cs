using GA.BasicTypes;
using GA.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.Helpers;

namespace GA
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> fitness = x => 2 * x + 1;
            var ga = new GeneticAlgorithm(3000, 10,
                new OnePointCrossover(),
                new ClassicMutationOperator(),
                new RouletteWheelSelection(),
                fitness);
            ga.PrintStatistics = true;

            StopwatchProvider.StartStopWatch();
            var result = ga.RunSimulation(5);
            Console.WriteLine(StopwatchProvider.StopWatchTime());

            Console.WriteLine($"x = {result.Chromosome.DecodedValue}, f = {result.Fitness}");

            //var ind = new Individual(10);
            //ind.ReplaceGenes(new bool[] { true, true, true, true, true, true, true, true, true, true });
            //Console.WriteLine($"x = {ind.Chromosome.DecodedValue}, f = {fitness(ind.Chromosome.DecodedValue)}");

            Console.ReadLine();

            return;

            //var ind1 = new Individual(10);
            //var ind2 = new Individual(10);

            //Console.WriteLine("Individual 1: ");
            //Console.WriteLine(ind1);
            //Console.WriteLine("Individual 2: ");
            //Console.WriteLine(ind2);

            //var crossover = new OnePointCrossover();

            //crossover.Crossover(ind1, ind2);

            //Console.WriteLine("Individual 1: ");
            //Console.WriteLine(ind1);
            //Console.WriteLine("Individual 2: ");
            //Console.WriteLine(ind2);

            //var mutation = new ClassicMutationOperator();


            //Console.WriteLine("--Before mutation: ");
            //Console.WriteLine("Individual 1: ");
            //Console.WriteLine(ind1);

            //mutation.Mutation(ind1, 0.1);

            //Console.WriteLine("--After mutation: ");
            //Console.WriteLine("Individual 1: ");
            //Console.WriteLine(ind1);
        }
    }
}


