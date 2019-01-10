using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.Abstracts
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}
