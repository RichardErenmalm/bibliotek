using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekhanterings_system_Inlamningsuppgift_3
{
    public class FåUserInput
    {
        
        public static T TaEmotThing<T>(string typAvData)
        {
            while (true)
            {
                Console.WriteLine($"Ange {typAvData}: ");

                string userInput = "";
                try
                {
                    userInput = Console.ReadLine();
                }
                catch
                {
                    ÅteranvändbarText.FörsökIgen("Något gick fel");
                    continue;
                }
               
                T value;
                try
                {
                    value = (T)Convert.ChangeType(userInput, typeof(T));
                    return value;
                }
                catch (FormatException)
                {
                    ÅteranvändbarText.FörsökIgen("Tänk på att använda rätt datatyp");
                }
                catch (Exception ex) 
                {
                    ÅteranvändbarText.FörsökIgen(ex.Message);
                }
            }
        }
        public static int TaEmotInt(string typAvData)
        {
            while (true)
            {
                Console.WriteLine($"Ange {typAvData}: ");

                int id = -1;
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());

                }
                catch
                {
                    ÅteranvändbarText.FörsökIgen("Tänk på att bara använda siffror");
                    continue;
                }
                if (id < 0)
                {
                    ÅteranvändbarText.FörsökIgen($"{typAvData} kan inte innehålla subtraktionstecken");
                    continue;
                }
                return id;
            }
        }

        public static string TaEmotString(string typAvData)
        {
            while (true)
            { 
                Console.WriteLine($"Ange {typAvData}");

                string userInput;

                try
                {
                    userInput = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    ÅteranvändbarText.FörsökIgen(ex.Message);
                    continue;
                }

                return userInput;
            }
        }

        public static int TaEmotPubliceringsår()
        {
            while (true)
            {
                Console.WriteLine($"Ange publicerings år:");

                int userInput;

                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    ÅteranvändbarText.FörsökIgen(ex.Message);
                    continue;
                }

                if (userInput > 2024)
                {
                    ÅteranvändbarText.FörsökIgen("Det är inte möjligt att publicerings året är senare än 2024");
                    continue;
                }

                return userInput;
            }

        }
    }

}
