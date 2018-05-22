using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class AND : Connective
    {
        public AND()
        {
            fName = "&";
        }

        public override bool Evaluate(bool aB1, bool aB2)
        {
            return aB1 && aB2;
        }
    }
}
