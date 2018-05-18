using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    abstract class Sentence
    {
        public bool Negation;
        public virtual String Name { get; }
        public int Count;
        public abstract bool Evaluate(Dictionary<string, bool> aModel);
        public abstract bool Query(string aLiteral);
    }
}
