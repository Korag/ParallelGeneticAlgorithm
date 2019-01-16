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
        public static Random r =  new Random();


        public Task<double> SumOfFitness(int leftC, int rightC, Individual[] currentPopulation)
        {
            var task = new Task<double>(() =>
            {
                double sum = 0;

                for (int j = leftC; j < rightC; j++)
                {
                    sum += currentPopulation[j].Fitness;
                }
                return sum;
            });
            task.Start();
            return task;
        }

        //public Task<double[]> SumDivideOfFitness(int leftC, int rightC, Individual[] currentPopulation, double sumOfFitness)
        //{
        //    var task = new Task<double[]>(() =>
        //    {
        //        double[] arrayOfFitness = new double[currentPopulation.Length];
        //        double sum = 0;

        //            if (leftC > 0)
        //            {
        //            lock (this)
        //                 {
        //                     sum = arrayOfFitness[leftC - 1];
        //                 }
        //            }
         
          
        //        for (int j = leftC; j < rightC; j++)
        //        {
        //            arrayOfFitness[j] += sum+(currentPopulation[j].Fitness/sumOfFitness);
        //            sum = arrayOfFitness[j];
        //        }
        //        return arrayOfFitness;
        //    });
        //    task.Start();
        //    return task;
        //}

        public List<double> CalculateDistribuance(Individual[] currentPopulation)
        {
            var sumOfFitness2 = currentPopulation
                .Sum(x => x.Fitness);

            double sumOfFitness = 0;

            double seed = 10000;
            double tasks = (currentPopulation.Length / seed);
            int amountOfTask = (int)Math.Ceiling(tasks);
            Task<double>[] t = new Task<double>[amountOfTask];
            int LeftC = 0;
            int RightC = (int)seed;

            for (int k = 0; k < amountOfTask; k++)
            {
                if (k == amountOfTask - 1)
                {
                    RightC = currentPopulation.Length;
                    t[k] = SumOfFitness(LeftC, RightC, currentPopulation);
                }
                else
                {
                    t[k] = SumOfFitness(LeftC, RightC, currentPopulation);
                    LeftC = RightC;
                    RightC += (int)seed;
                }
            }

            for (int k = 0; k < amountOfTask; k++)
            {
                t[k].Wait();
                sumOfFitness += t[k].Result;
            }

            //double cummulativeFitness = 0;
            //List<double> distribuance2 = currentPopulation
            //    .Select(x =>
            //    {
            //        cummulativeFitness += x.Fitness / sumOfFitness;
            //        return cummulativeFitness;
            //    })
            //    .ToList();

            List<double> distribuance = new List<double>(); 

            double sum = 0;
            for (int i = 0; i < currentPopulation.Length; i++)
            {
                double tmp = sum + (currentPopulation[i].Fitness / sumOfFitness);
                distribuance.Add(tmp);
                sum = tmp;
            }

            return distribuance;

        }

        private Individual SelectCurrentParent(double random, Individual[] currentPopulation, List<double> distribuance)
        {

            //var currentIndex2 = distribuance
            //    .Select((x, i) => new { Index = i, Value = x })
            //    .FirstOrDefault(x => x.Value > random).Index;

            int currentIndex = 0;
            for (int i = 0; i < distribuance.Count; i++)
            {
                if (distribuance[i] > random)
                {
                    currentIndex = i;
                    break;
                }
            }

            //Console.WriteLine($"Individual: {currentIndex}");

            return currentPopulation[currentIndex].Clone();
        }

        public Individual[] GenerateParentPopulation(Individual[] currentPopulation)
        {
            //List<double> distribuance;
            //CalculateDistribuance(currentPopulation, out distribuance);

            List<double> distribuance;
            distribuance = CalculateDistribuance(currentPopulation);

            // zmienna randomowa z generatora dla 2 i dalszych generacji przyjmuje zawsze wartosc 0 przez co wynik jest zły
            // naprawiłem to dodajac do generatora funkcje z lockiem i w kazdej innej klasie zadzialalo
            // tutaj niestety dziala tylko dla 1 generacji a potem sie psuje

            //currentPopulation
            //    .Select(x => SelectCurrentParent(r.NextDouble(), currentPopulation, distribuance))
            //    .ToArray();

            Parallel.For(0, currentPopulation.Length, i =>
            {
                currentPopulation[i] = SelectCurrentParent(r.NextDouble(), currentPopulation, distribuance);
            });

            return currentPopulation;
        }
    }
}
