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

        public string Name { get => fName; }
    }
}
