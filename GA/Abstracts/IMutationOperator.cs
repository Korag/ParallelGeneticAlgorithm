using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GA.BasicTypes;

namespace GA.Abstracts
{
    public interface IMutationOperator
    {
        void Mutation(Individual individual, double mutationProbability);
    }
}