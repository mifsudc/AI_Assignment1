using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    abstract class Connective
    {
        protected string fName;

        public string Name { get => fName; }
        public abstract bool Evaluate(bool aB1, bool aB2);
    }
}
