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
        private int fCount;

        public Complex(List<Sentence> aSentences, Connective aCon)
        {
            fSentences = aSentences;
            fCon = aCon;
            // only root sentence count is required after initialization
            fCount = 0;
            foreach ( Sentence lSentence in fSentences )
            {
                fCount += lSentence.Count;
            }
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

        public override int Count { get => fCount; set => fCount = value; }

        public override bool Evaluate(List<string> aModel)
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

        public override string FindUnknown(List<string> aLiterals)
        {
            if ( fCount > 1 )
                throw new Exception("Cannot find more than one unknown");

            string lResult = "";
            foreach ( Sentence lS in fSentences )
            {
                // if a subsentence evaluates to false it contains the unknown
                if ( !lS.Evaluate(aLiterals) )      // fails on negation, but hf contains none
                {
                    lResult = lS.FindUnknown(aLiterals);
                    break;
                }
            }

            return lResult;
        }

        public override bool Query( string aLiteral )
        {
            if ( fSentences.Exists( x => x.Query(aLiteral) ) )
            {
                return true;
            }

            return false;
        }
    }
}
