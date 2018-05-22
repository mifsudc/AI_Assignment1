using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment2
{
    class Parser
    {
        private Dictionary<String, Atomic> fLiterals;
        private Dictionary<String, Connective> fCons;

        public Parser()
        {
            fLiterals = new Dictionary<string, Atomic>();
            fCons = new Dictionary<string, Connective>();

            // *** create connectives dict ***
            fCons.Add( "=>", new IMPLIES() );
            fCons.Add( "&", new AND() );
            fCons.Add( "\\/", new OR() );
            fCons.Add( "<=>", new BICOND() );
        }

        public KB Parse()
        {
            StreamReader lReader = new StreamReader("../../test.txt");

            KB lResult = new KB();

            List<String> lStrings = new List<String>();

            while (!lReader.EndOfStream)
            {
                // read file to end
                if (lReader.ReadLine() == "TELL")
                {
                    Console.WriteLine("READING SENTENCES");

                    // read TELL string:sentences
                    String lRaw = lReader.ReadLine();
                    Console.WriteLine("RAW: " + lRaw);
                    Console.WriteLine();
                    // strip whitespace
                    lRaw = String.Join("", lRaw.Split(' '));

                    if (lRaw.Contains(";"))
                    {
                        // split into array of string:sentences
                        lStrings = lRaw.Split( new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                    else
                    {
                        // single string:sentence in TELL
                        lStrings.Add(lRaw);
                    }

                    foreach (String lString in lStrings)
                    {
                        Console.WriteLine("SPLIT: " + lString);

                        // create actual sentences from string:sentences
                        lResult.fTell.Add( ConvertSentence( lString ) );
                    }
                }
                else
                {
                    // read ASK sentence
                    lResult.fAsk = lReader.ReadLine();
                }
            }

            lReader.Close();

            // Write sentences
            Console.WriteLine("SENTENCES READ:");
            foreach (Sentence lS in lResult.fTell)
            {
                Console.WriteLine("SENTENCE: " + lS.Name);
            }
            Console.WriteLine("LITERALS ADDED:");
            foreach (Atomic A in fLiterals.Values)
            {
                Console.WriteLine("LITERAL: " + A.Name);
            }
            lResult.fLiterals = fLiterals.Keys.ToList();

            return lResult;
        }

        private Sentence ConvertSentence(String lInput) // TODO add biconditionals & disjunctions
        {

            if (lInput.Contains("=>"))
            {
                // split implications =>
                string[] lTemp = lInput.Split( new char[] { '=', '>' } , StringSplitOptions.RemoveEmptyEntries);

                List<Sentence> lSentences = new List<Sentence>();
                foreach (String lSS in lTemp)
                {
                    lSentences.Add( ConvertSentence(lSS) );
                }

                return new Complex(lSentences, fCons["=>"]);
            }
            // split conjunctions &
            if (lInput.Contains("&"))
            {
                // split implications =>
                string[] lTemp = lInput.Split('&');

                List<Sentence> lSentences = new List<Sentence>();
                foreach (String lSS in lTemp)
                {
                    lSentences.Add(ConvertSentence(lSS));
                }

                return new Complex(lSentences, fCons["&"]);
            }
            // split negations ~
            if (lInput.Contains("~"))
            {
                lInput = lInput.Substring(1);
                List<Sentence> lSentences = new List<Sentence>();
                lSentences.Add( ConvertSentence(lInput) );
                return new Complex( lSentences, fCons["~"] );
            }

            if (!fLiterals.ContainsKey(lInput))
            {
                fLiterals.Add( lInput, new Atomic(lInput) );
            }

            return fLiterals[lInput];
        }
    }
}