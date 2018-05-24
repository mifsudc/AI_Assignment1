using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser lParser = new Parser();
            KB kb = lParser.Parse();

            Inference Alg = new BC();
            Console.WriteLine( Alg.Execute(kb) );

            Console.ReadLine();
        }
    }
}
