using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class IMPLIES : Connective
    {
        public IMPLIES()
        {
            fName = "=>";
        }

        public override bool Evaluate(bool aB1, bool aB2)
        {
            return Convert.ToSByte(aB2) >= Convert.ToSByte(aB1);
        }
    }
}
