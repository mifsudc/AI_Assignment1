using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class Atomic : Sentence
    {
        private String fName;

        public Atomic(String aName)
        {
            fName = aName;
        }

        public override string Name { get => fName; }

        public override int Count { get => 1; set { } }

        public override bool Evaluate(List<string> aModel)
        {
            return aModel.Contains(fName);
        }

        public override string FindUnknown(List<string> aLiterals)
        {
            return fName;
        }

        public override bool Query(string aLiteral)
        {
            return aLiteral == fName;
        }
    }
}
