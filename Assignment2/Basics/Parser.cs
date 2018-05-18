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
        private Dictionary<String, Symbol> fSymbols;

        public Parser()
        {
            fLiterals = new Dictionary<string, Atomic>();
            fSymbols = new Dictionary<string, Symbol>();

            // *** create connectives dict ***
            fSymbols.Add( "=>", new Connective("=>") );
            fSymbols.Add( "&", new Symbol("&") );
        }

        public void Parse()
        {
            StreamReader lReader = new StreamReader("../../test.txt");

            String lAsk;
            List<String> lStrings = new List<String>();
            List<Sentence> lTell = new List<Sentence>();

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
                        lTell.Add( ConvertSentence( lString ) );
                    }
                }
                else
                {
                    // read ASK sentence
                    lAsk = lReader.ReadLine();
                }
            }

            lReader.Close();

            // Write sentences
            Console.WriteLine("SENTENCES READ:");
            foreach (Sentence lS in lTell)
            {
                Console.WriteLine("SENTENCE: " + lS.Name);
            }
            Console.WriteLine("LITERALS ADDED:");
            foreach (Atomic A in fLiterals.Values)
            {
                Console.WriteLine("LITERAL: " + A.Name);
            }
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

                return new Complex(lSentences, fSymbols["=>"]);
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

                return new Complex(lSentences, fSymbols["&"]);
            }
            // split negations ~
            if (lInput.Contains("~"))
            {
                lInput = lInput.Substring(1);
                List<Sentence> lSentences = new List<Sentence>();
                lSentences.Add( ConvertSentence(lInput) );
                return new Complex( lSentences, fSymbols["~"] );
            }

            if (!fLiterals.ContainsKey(lInput))
            {
                fLiterals.Add( lInput, new Atomic(lInput) );
            }

            return fLiterals[lInput];
        }
    }
}