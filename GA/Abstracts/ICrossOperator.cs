using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GA.BasicTypes;

namespace GA.Abstracts
{

    interface ICrossOperator
    {
        void Crossover(Individual parent1, Individual parent2);
    }

}