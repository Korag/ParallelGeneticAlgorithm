using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GA.Abstracts;
using GA.Helpers;

namespace GA.BasicTypes
{
    public class Individual : ICloneable<Individual>
    {
        public ChromosomeType Chromosome { get; set; }
        public double Fitness { get; set; }

        public Individual()
        {
        }

        public Individual(int chromosomeSize)
        {
            Chromosome = new ChromosomeType(chromosomeSize);
        }

        public void UpdateFitness(Func<double, double> fitness)
        {
            Fitness = fitness(Chromosome.DecodedValue);
        }

        public void InsertGenes(int insertIndex, bool[] genes)
        {
            // LINQ
            //StopwatchProvider.StartStopWatch();
            //Chromosome.Genes = Chromosome.Genes
            //    .Select((x, i) => i < insertIndex ? x : genes[i - insertIndex])
            //    .ToArray();
            //Console.WriteLine(StopwatchProvider.StopWatchTime());

            // Without LINQ
            //StopwatchProvider.StartStopWatch();
            for (int i = 0; i < Chromosome.Genes.Length; i++)
            {
                if (i >= insertIndex)
                {
                    Chromosome.Genes[i] = genes[i - insertIndex]; 
                }
            }
            //Console.WriteLine(StopwatchProvider.StopWatchTime());
        }

        public void ReplaceGenes(bool[] genes)
        {
            InsertGenes(0, genes);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("(");
            foreach (var gene in Chromosome.Genes)
            {
                sb.Append($"{(gene ? 1 : 0)} ");
            }
            sb.AppendLine(")");

            sb.AppendLine($"Decoded value: {Chromosome.DecodedValue}");

            return sb.ToString();
        }

        public Individual Clone()
        {
            return new Individual()
            {
                Chromosome = Chromosome.Clone(),
                Fitness = Fitness
            };
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}