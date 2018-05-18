using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class Symbol
    {
        private String fName;
        private bool fEval;

        public static bool UNK;

        public Symbol(String aName)
        {
            fName = aName;
            fEval = UNK;
        }
    }
}
