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

        public override bool Evaluate(Dictionary<string, bool> aModel)
        {
            bool lResult = fSentences[0].Evaluate(aModel);

            // skip first element already stored in lResult
            foreach (Sentence lS in fSentences.Skip(1))
            {
                // enumerate subsentences and evaluate to bool
                lResult = fCon.Evaluate(lResult, lS.Evaluate(aModel));
            }

            return lResult;
        }
    }
}
