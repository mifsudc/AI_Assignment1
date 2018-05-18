using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class TT : Inference
    {
        List<Dictionary<string, bool>> fWorlds  = new List<Dictionary<string, bool>>();

        public string Execute(KB aKb)
        {
            List<String> aSymbols = aKb.fSymbols;

            // create 2^n world models
            for (int i = 0; i < Math.Pow(2, aSymbols.Count); i++)
            {
                fWorlds.Add(new Dictionary<string, bool>());

                for (int j = 0; j < aSymbols.Count; j++)
                {
                    // evil bitwise magic
                    fWorlds[i].Add(aSymbols[j], Convert.ToBoolean((i >> j) & 0b01));
                }
            }

            // Order by symbol count for convenience
            List<Sentence> aSentences = aKb.fSentences.OrderBy( x => x.Count ).ToList();
            // enumerate sentences, evaluate validity of each world
            List<Dictionary<string, bool>> lDelete = new List<Dictionary<string, bool>>();
            foreach (Sentence lS in aSentences)
            {
                foreach (Dictionary<string, bool> lW in fWorlds)
                {
                    if ( !lS.Evaluate(lW) )
                    {
                        lDelete.Add(lW);
                    }
                }

                fWorlds.RemoveAll(x => lDelete.Contains(x));
                lDelete.Clear();

                if (fWorlds.Count == 0)
                {
                    return "NO";
                }
            }

            return "YES: " + fWorlds.Count;
        }
    }
}
