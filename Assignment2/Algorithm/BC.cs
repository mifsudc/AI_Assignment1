using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class BC : Inference
    {
        public string Execute(KB aKb)
        {
            if (!aKb.fHornForm)
            {
                return "BC requires horn form KB.";
            }



            return "";
        }
    }
}
