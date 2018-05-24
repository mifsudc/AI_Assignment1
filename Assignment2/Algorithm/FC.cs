using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class FC : Inference
    {
        public string Execute(KB aKb)
        {
            // FC requires horn form
            if (!aKb.fHornForm)
            {
                return "FC requires horn form KB.";
            }
            
            // instance list setup
            List<Sentence> aTell = aKb.fTell.OrderBy( x => x.Count ).ToList();
            List<Sentence> lComplete = new List<Sentence>();
            List<string> lInferred = new List<string>();

            // forward chaining process
            List<Sentence> lClauses = aTell.FindAll( x => !lComplete.Contains(x) );
            for (int i = 0; i < aTell.Count; i++)
            {
                // find a sentence that can be used to infer a literal
                Sentence lTempSent = lClauses.Find( x => x.Count == 1 );

                // continue if sentence is found
                if ( lTempSent != null )
                {
                    // infer the literal
                    string lClause = lTempSent.FindUnknown( lInferred );
                    lInferred.Add( lClause );
                    if ( lClause == aKb.fAsk )
                        break;
                    Console.WriteLine(lClause);

                    // add to used sentences list
                    lComplete.Add( lTempSent );
                    lClauses = aTell.FindAll( x => !lComplete.Contains(x) );

                    // enumerate through sentences
                    foreach (Sentence lSentence in lClauses)
                    {
                        // update if sentence contains new inferred literal
                        if ( lSentence.Query(lClause) )
                            lSentence.Count--;
                    }
                }
                else
                    // no further implications can be deduced
                    return "NO";
            }

            // prepare output
            string lResult = "YES: ";
            foreach (var lString in lInferred)
                lResult += lString + ", ";
            lResult = lResult.Remove( lResult.Count() - 2, 2 );

            return lResult;
        }
    }
}
