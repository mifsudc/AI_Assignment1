using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    abstract class Sentence
    {
        // denotes whether the sentence is negated
        public bool Negation;
        // denotes the actual representation on the literal
        public virtual String Name { get; }
        // denotes the number of unknowns in a sentence
        public int Count;
        // given a complete world model, evaluates whether the world entails the sentence
        public abstract bool Evaluate(Dictionary<string, bool> aModel);
        // solves for the last unknown literal
        //public abstract bool Query(string aLiteral);
    }
}
