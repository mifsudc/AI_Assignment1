using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class Complex : Sentence
    {
        private List<Sentence> fSentences;
        private Connective fCon;

        public Complex(List<Sentence> aSentences, Connective aCon)
        {
            fSentences = aSentences;
            fCon = aCon;
        }

        public override String Name
        {
            get
            {
                String lResult = "";
                foreach (Sentence lS in fSentences)
                {
                    if (lS.Negation)
                        lResult += "~";
                    lResult += lS.Name + fCon.Name;
                }

                return lResult.TrimEnd( fCon.Name.ToCharArray() );
            }
        }

        public int Count;
    }
}
