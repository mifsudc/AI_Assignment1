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

        public override string RHS { get => fName; }

        public override int Count { get => 1; set { } }

        public override List<string> Dependancies(List<string> aExclude)
        {
            List<string> lResult = new List<string>();
            if ( !aExclude.Contains( fName ) )
                lResult.Add(fName);
            return lResult;
        }

        public override bool Evaluate(List<string> aModel) => aModel.Contains(fName);

        public override string FindUnknown(List<string> aLiterals) => fName;

        public override bool Query(string aLiteral) => aLiteral == fName;
    }
}
