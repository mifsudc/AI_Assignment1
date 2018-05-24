using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class BC : Inference
    {
        List<string> fResult = new List<string>();

        public string Execute(KB aKb)
        {
            if (!aKb.fHornForm)
            {
                return "BC requires horn form KB.";
            }

            // instance list setup
            List<Sentence> aTell = aKb.fTell.OrderBy(x => x.Count).ToList();
            List<string> aExclude = new List<string>();

            if ( ChainLevel( aKb.fAsk, aTell, aExclude ) )
            {
                // prepare output
                string lResult = "YES: ";
                foreach ( string lS in fResult )
                    lResult += lS + ", ";
                lResult = lResult.Remove(lResult.Count() - 2, 2);
                return lResult;
            }

            else
                return "NO";

        }

        // infers whether a list of clauses entails a symbol
        private bool ChainLevel( string aFind, List<Sentence> aTell, List<string> aExclude )
        {
            // set aFind as closed
            aExclude.Add(aFind);
            Console.WriteLine("FINDING " + aFind);
            Console.WriteLine();

            // find sentences where aFind appears as implication
            List<Sentence> lClauses = aTell.FindAll( x => ( x.RHS == aFind ) );

            Console.WriteLine("INCLUDING CLAUSES");
            foreach (Sentence s in lClauses)
                Console.WriteLine(s.Name);
            Console.WriteLine();

            // only one true clause needs to be found
            foreach (Sentence lClause in lClauses)
            {
                Console.WriteLine("FINDING DEPENDANCIES FOR " + lClause.Name);

                // obtain dependant literals of clause
                List<string> lDependancies = lClause.Dependancies( aExclude );
                foreach (string s in lDependancies)
                    Console.WriteLine(s);
                Console.WriteLine();

                // determine whether the literals can be entailed
                bool lResult = true;
                foreach (string lLiteral in lDependancies)
                {
                    // all dependancies must be true for root to also be true
                    lResult = lResult && ChainLevel(lLiteral, aTell, aExclude);
                }
                // all dependancies can be entailed
                if (lResult)
                {
                    Console.WriteLine(aFind + " IS ENTAILED");
                    fResult.Add(aFind);
                    return true;
                }
            }

            // no including clauses have been found, or no dependancies can be inferred
            return false;
        }
    }
}
