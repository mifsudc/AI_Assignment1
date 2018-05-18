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
            if (!aKb.fHornForm)
            {
                return "FC requires horn form KB.";
            }

            List<Sentence> aClauses = aKb.fSentences.OrderBy(x => x.Count).ToList();

            List<Sentence> lAgenda = new List<Sentence>();
            lAgenda.AddRange(aClauses.Where(x => x.Count == 1));

            Dictionary<string, bool> lInferred = new Dictionary<string, bool>();
            while ( !lInferred.ContainsKey( aKb.fTarget.Name ) )
            {
                if ( lAgenda.Count == 0 )
                    return "NO";

                // pop first symbol from agenda
                Sentence aS = lAgenda[0];
                lAgenda.Remove(aS);

                if ( lInferred.ContainsKey(aS.Name) )
                    continue;
                lInferred.Add(aS.Name, true); // move dis shit somewhere

                // enumerate rules
                foreach ( Sentence aClause in aClauses )
                {
                    // clause contains current focus symbol
                    if ( aClause.Query(aS.Name) )
                    {
                        // clause contains 1 unknown -> can be entailed
                        if (--aClause.Count == 1)
                        {

                        }
                    }

                    
                }
            }

            return "";
        }
    }
}
