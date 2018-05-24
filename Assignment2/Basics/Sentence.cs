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
        public abstract string Name { get; }
        // denotes the number of unknowns in a sentence
        public abstract int Count { get; set; }
        // given a complete world model, evaluates whether the world entails the sentence
        public abstract bool Evaluate(List<string> aModel);
        // solves for the last unknown literal
        public abstract string FindUnknown(List<string> aLiterals);

        public abstract string RHS { get; }

        public abstract List<string> Dependancies( List<string> aExclude );
        // queries whether the sentence contains the given literal
        public abstract bool Query(string aLiteral);
    }
}
