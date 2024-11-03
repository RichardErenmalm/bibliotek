using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekhanterings_system_Inlamningsuppgift_3
{
    public class ÅteranvändbarText
    {
        public static void Header(string header)
        {
            Console.WriteLine(header);

            foreach (char c in header)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void FörsökIgen(string text)
        {
            Console.WriteLine();
            Console.WriteLine("Något blev fel: " + text);
            Console.WriteLine();
            Console.WriteLine("-----------");
            Console.WriteLine("Försök igen");
            Console.WriteLine();
        }

        public static void GåTillbakaTillMeny(string text)
        {
            Console.WriteLine();
            Console.WriteLine(text);
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Klicka enter för att gå tillbaks till menyn");
            Console.ReadLine();

        }
    }
}
