using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekhanterings_system_Inlamningsuppgift_3
{
    public static class Meny
    {
        public static void HuvudMeny()
        {

           Bibliotek bibliotek = new Bibliotek();

            string header = "Huvud meny";

            bool keepGoing = true;
            while (keepGoing)
            {
                ÅteranvändbarText.Header(header);

                Console.WriteLine("1. Lägg till ny bok");
                Console.WriteLine("2. Lägg till ny författare");
                Console.WriteLine("3. Uppdatera bok detaljer");
                Console.WriteLine("4. Uppdatera författardetaljer");
                Console.WriteLine("5. Ta bort bok");
                Console.WriteLine("6. Lista alla böcker och författare");
                Console.WriteLine("7. Ge en recension");
                Console.WriteLine("8. Visa alla medelrecensioner över ett visst nummer");
                Console.WriteLine("9. Visa alla medelrecensioner");
                Console.WriteLine("10. Filtrera böcker enligt genre");
                Console.WriteLine("11. Sortera böcker efter författare");
                Console.WriteLine("12. Detaljerad författar information");
                Console.WriteLine("13. Avsluta");

                int userInput = -1;
                try 
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                catch 
                { 
                }

                Console.WriteLine();

                switch (userInput)
                {
                    case 1:
                        bibliotek.LäggTillBok();
                        break;
                    case 2:
                        bibliotek.UppdateraBokdetaljer();
                        break;
                    case 3:
                        bibliotek.UppdateraFörfattardetaljer();
                        break;
                    case 4:
                        bibliotek.TaBortEnBok();
                        break;                      
                    case 5:
                        bibliotek.TaBortEnFörfattare();
                        break;
                    case 6:
                        bibliotek.VisaAllaBöckerOchFörfattare();
                        break;
                    case 7:
                        bibliotek.GeRecension();
                        break;
                    case 8:
                        bibliotek.VisaMedelRecensionÖverValtNummer();
                        break;
                    case 9:
                        bibliotek.VisaMedelRecension();
                        break;
                    case 10:
                        bibliotek.FiltreraBöckerEnligtGenre();                      
                        break;
                    case 11:
                        bibliotek.SorteraBöckerEnligtFörfattare();
                        break;
                    case 12:
                        bibliotek.DetaljeradFörfattarInformation();
                        break;
                    case 13: 
                        keepGoing = false;
                        break;
                    default:
                        ÅteranvändbarText.FörsökIgen("Vänligen välj mellan tillgängliga alternativ");
                        break;
                }
            }
        }
    }
}
