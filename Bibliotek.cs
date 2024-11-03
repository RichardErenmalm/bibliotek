using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace Bibliotekhanterings_system_Inlamningsuppgift_3
{
    public  class Bibliotek
    {
        //skapar en privat instans av Databas som bara kan nås från biblioteket
        private Databas databas = new Databas();

        public Bibliotek() 
        {

            try
            {
                //skapar en string med path till json filen
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LibraryData.json");
                //Läser JSON.txt filen och lägger in det i jsonData string
                string jsonData = File.ReadAllText(filePath);

                if (File.Exists(filePath) && !string.IsNullOrEmpty(jsonData))
                {
                    //tar datan från jsonData och omvandlar till objekt i databas klassen
                    databas = JsonSerializer.Deserialize<Databas>(jsonData) ?? new Databas();
                }
                else
                {
                    databas = new Databas();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Något blev fel: " + ex.Message);
            }
          
        }

        public void SparaTillDataBas()
        {

            try
            {
                string json = JsonSerializer.Serialize(databas, new JsonSerializerOptions {WriteIndented = true });
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LibraryData.json"), json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Något blev fell: " + ex.Message);
            }
           
        }

        public  void LäggTillBok()
        {
            //ta emot användarinformation
            int bokId = FåUserInput.TaEmotInt("ID");

            string titel = FåUserInput.TaEmotString("titel");

            string författare = FåUserInput.TaEmotString("författare");

            string genre = FåUserInput.TaEmotString("genre");

            int publiceringsÅr = FåUserInput.TaEmotPubliceringsår();

            int isbn = FåUserInput.TaEmotInt("ISBN");


            //skapa bok, lägg in i databas och spara till Json fil
            Bok bok = new Bok(bokId, titel, författare, genre, publiceringsÅr, isbn);
            databas.AllBooksFromDb.Add(bok);
            SparaTillDataBas();
           

            //om författaren inte finns, lägg till författaren till listan
            if(!CheckIfAuthorExist(författare))
            {
                Console.WriteLine("Boken är den första boken i biblioteket av författaren " + författare);
                Console.WriteLine("Du kommer nu behöva ange lite information om författaren");
                Console.WriteLine("");
                LäggTillFörfattare(författare);
            }

            SparaTillDataBas();
            Console.WriteLine("Boken är tillagd i biblioteket");
            Console.WriteLine();
        }


        public void LäggTillFörfattare(string namn)
        {
            //ta emot användar information
            int id = FåUserInput.TaEmotInt("författarens id");
            string land = FåUserInput.TaEmotString("land");


            //skapa författare, lägg in i json fil och spara ändringar
            Author author = new Author(id, namn, land);
            databas.AllAuthorsFromDb.Add(author);
            SparaTillDataBas();

            Console.WriteLine();
            Console.WriteLine("Författaren är tillagd i biblioteket");
        }


        public void UppdateraBokdetaljer()
        {
            //ta emot bok id och gå tillbaka till meny om det int finns
            int bokID = FåUserInput.TaEmotInt("bok ID");
            if (!CheckIfBookIdExist(bokID))
            {
                ÅteranvändbarText.GåTillbakaTillMeny($"Bok ID:{bokID}, matchar inte någon bok i biblioteket");
            }

            while (true)
            {

                //låt användare välja vilken egenskap de vill ändra
                int val = FåUserInput.TaEmotInt("Vilken information du vill ändra \n\n1. ID\n2. Titel\n3. Författare\n4. Genre\n5. Publiserings år\n6. ISBN");

                //skapa två böcker som är referens till boken som ska ändras från author listan och bok listan
                Bok bokIBokListan = databas.AllBooksFromDb.First(bok => bok.BokId == bokID);

                int ändratVärdeInt;
                string ändratVärdeString;
                //ändra valda egenskap och spara sedan till json fil
                switch(val)
                {
                    case 1:
                        ändratVärdeInt = FåUserInput.TaEmotInt("nytt ID");
                        bokIBokListan.BokId = ändratVärdeInt;                   
                        break;
                    case 2:
                        ändratVärdeString = FåUserInput.TaEmotString("ny titel");
                        bokIBokListan.Titel = ändratVärdeString;
                        break;
                    case 3:
                        ändratVärdeString = FåUserInput.TaEmotString("ny författare");
                        bokIBokListan.Author = ändratVärdeString;
                        break;
                    case 4:
                        ändratVärdeString = FåUserInput.TaEmotString("ny genre");
                        bokIBokListan.Genre = ändratVärdeString;
                        break;
                    case 5:
                        ändratVärdeInt = FåUserInput.TaEmotInt("nytt publiserings år");
                        bokIBokListan.PublishingYear = ändratVärdeInt;
                        break;
                    case 6:
                        ändratVärdeInt = FåUserInput.TaEmotInt("nytt ISBN");
                        bokIBokListan.Isbn = ändratVärdeInt;
                        break;
                    default:
                        ÅteranvändbarText.FörsökIgen("Välj mellan tillgängliga alternativ");
                        continue;
                          
                }
                SparaTillDataBas();

                ÅteranvändbarText.GåTillbakaTillMeny("Ändringarna sparades");
                break;
              
            }
           
        }

        public void UppdateraFörfattardetaljer()
        {
            //ta emot bok id och gå tillbaka till meny om det int finns
            int författarID = FåUserInput.TaEmotInt("författar ID");
            if (!CheckIfAuthorIdExist(författarID))
            {
                ÅteranvändbarText.GåTillbakaTillMeny($"Författar ID:{författarID}, matchar inte någon bok i biblioteket");
                return;
            }

            //skapa en author som är referens till author i författarlistan som matchar id
            Author author = databas.AllAuthorsFromDb.First(author => author.AuthorId == författarID);

            while (true)
            {
                //låt användaren välja vilken egenskap de vill ändra
                int val = FåUserInput.TaEmotInt("Vilken information du vill ändra \n\n1. ID\n2. Namn\n3. Land");

                int ändratVärdeInt;
                string ändratVärdeString;

                //ändra valda egenskap till det nya vädet, spara sedan till json filen
                switch (val)
                {
                    case 1:
                        ändratVärdeInt = FåUserInput.TaEmotInt("nytt ID");
                        author.AuthorId = ändratVärdeInt;
                        break;
                    case 2:
                        ändratVärdeString = FåUserInput.TaEmotString("nytt namn");

                        string gamaltNamn = author.Namn;
                        databas.AllBooksFromDb.ForEach(bok =>
                        {
                            if (bok.Author == gamaltNamn)
                            {
                                bok.Author = ändratVärdeString;
                            }
                        });
                        author.Namn = ändratVärdeString;
                        break;
                    case 3:
                        ändratVärdeString = FåUserInput.TaEmotString("nytt land");
                        author.Land = ändratVärdeString;
                        break;
                    default:
                        ÅteranvändbarText.FörsökIgen("Välj mellan tillgängliga alternativ");
                        continue;
                }
                SparaTillDataBas();

                ÅteranvändbarText.GåTillbakaTillMeny("Ändringarna sparades");
                break;
            }
        }
        public void TaBortEnBok()
        {
            //ta emot bok id och gå tillbaka till meny om det int finns
            int id = FåUserInput.TaEmotInt("bok id");
            if (!CheckIfBookIdExist(id))
            {
                ÅteranvändbarText.GåTillbakaTillMeny($"Bok ID:{id}, matchar inte någon bok i biblioteket");
                return;
            }

           

            Bok bokAttTaBort = databas.AllBooksFromDb.First(bok => bok.BokId == id);

            //ta bort författare om boken är den sista i hans lista
            if (databas.AllBooksFromDb.Exists(bok => bok.Author == bokAttTaBort.Author))
            {

                Author author = databas.AllAuthorsFromDb.First(author => author.Namn == bokAttTaBort.Author);
                databas.AllAuthorsFromDb.Remove(author);
                ÅteranvändbarText.GåTillbakaTillMeny($"Boken är borttagen från biblioteket. (Författaren {author.Namn} togs också bort eftersom bibliotekt inte längre har några böcker av honom/henne)");
            }
            else
            {
                ÅteranvändbarText.GåTillbakaTillMeny($"Boken är borttagen från biblioteket");
            }

            databas.AllBooksFromDb.Remove(bokAttTaBort);
            SparaTillDataBas();
        }
        
        public void TaBortEnFörfattare()
        {
            //ta emot författar id och gå tillbaka till meny om det int finns
            int id = FåUserInput.TaEmotInt("författar id");
            if (!CheckIfAuthorIdExist(id))
            {
                ÅteranvändbarText.GåTillbakaTillMeny($"Författar ID:{id}, matchar inte någon författare i biblioteket");
                return;
            }


            //tar bort författaren från författar listan och alla böcker av författaren från boklistan
            Author author = databas.AllAuthorsFromDb.First(författare => författare.AuthorId == id);
            ÅteranvändbarText.GåTillbakaTillMeny($"Författaren {author.Namn} och bok/böcker hen har skrivit har tagits bort från biblioteket");
            databas.AllBooksFromDb.RemoveAll(bok => bok.Author == author.Namn);
            databas.AllAuthorsFromDb.Remove(author);
            SparaTillDataBas();
        }

        public void VisaAllaBöckerOchFörfattare()
        {
            //skriver ut alla författare i författar listan
            Console.WriteLine("Alla författare: ");
            databas.AllAuthorsFromDb.ForEach(author =>
            {
                Console.WriteLine($"Namn: {author.Namn}, ID: {author.AuthorId}, Land: {author.Land}");
            });
            //skriv ut alla böcker i boklistan
            Console.WriteLine();
            Console.WriteLine("Alla böcker: ");
            databas.AllBooksFromDb.ForEach(bok =>
            {
               Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Author}, Genre: {bok.Genre}, id:{bok.BokId}, publiseringsår: {bok.PublishingYear}, isbn: {bok.Isbn}  ");
            });
            ÅteranvändbarText.GåTillbakaTillMeny("");
        }
    
        public void GeRecension()
        {
            //ta emotboktitel och gå tillbaka till meny om det inte finns
            string bokTitel = FåUserInput.TaEmotString("bokens titel");
            if (!CheckIfBookExist(bokTitel))
            {
                ÅteranvändbarText.GåTillbakaTillMeny($"Titel:{bokTitel}, matchar inte någon bok i biblioteket");
                return;
            }
            
            while(true)
            {
                //ta emot användares recension
                int recension = FåUserInput.TaEmotInt("en recension mellan 1 - 5");

                //om recensionen är mellan 1 och 5: lägg till recensionen. annars: be användaren att skriva recension mellan 1 och 5 och starta om
                if (recension <= 5 && recension >= 1)
                {
                    Bok bok = databas.AllBooksFromDb.First(bok => bok.Titel == bokTitel);
                    bok.Resensioner.Add(recension);

                    SparaTillDataBas();
                    ÅteranvändbarText.GåTillbakaTillMeny("Recensionen har lagts till");
                }
                else
                {
                    ÅteranvändbarText.FörsökIgen("Du kan bara ge en recension mellan 1 - 5");
                    continue;
                }
                break;
            }
            

        }
       

        public void VisaMedelRecensionÖverValtNummer()
        {
            int recensionVärde = -1;
            while (true)
            {
                //ta emot recension om den är mellan 1 och 5, annars gör om
                recensionVärde = FåUserInput.TaEmotInt("recension värde du vill se recensioner över eller lika med");
                if (recensionVärde > 5 && recensionVärde < 1)
                {

                    ÅteranvändbarText.FörsökIgen("Du kan bara ge en recension mellan 1 - 5");
                    continue;
                }
                else
                {
                    break;
                }
            }

            //Skriv ut alla böcker med recensions medelvärde över eller lika med användares angivna nummer, annars: skriv att inga böcker har medelvärde så högt
            bool finnsBokÖverInskrivnaVärdet = false;
            databas.AllBooksFromDb.ForEach(bok =>
            {

                if (bok.Resensioner.Average() >= recensionVärde)
                {
                    finnsBokÖverInskrivnaVärdet = true;
                    Console.WriteLine($"{bok.Titel} : {bok.Resensioner.Average()}");
                }
                
               
            });
            if (!finnsBokÖverInskrivnaVärdet)
            {
                ÅteranvändbarText.GåTillbakaTillMeny("Det finns inga böcker med medel recension lika med eller över: " + recensionVärde);
            }
            else
            {
                ÅteranvändbarText.GåTillbakaTillMeny("");
            }   
        }


        public void VisaMedelRecension()
        {
            bool finnsRecensioner = false;

            databas.AllBooksFromDb.ForEach(bok =>
            {
                if (bok.Resensioner.Count != 0)
                {
                    finnsRecensioner = true;
                    Console.WriteLine($"{bok.Titel} : {bok.Resensioner.Average()}");
                }
               
 
            });
            if (!finnsRecensioner)
            {
                ÅteranvändbarText.GåTillbakaTillMeny("Det finns inga recensioner till några böcker i biblioteket för tillfället");
            }
            else
            {
                ÅteranvändbarText.GåTillbakaTillMeny("");
            }
        }

        public void FiltreraBöckerEnligtGenre()
        {
            //ta emot genre
            string genre = FåUserInput.TaEmotString("genre du vill se");
            Console.WriteLine();

            //visa upp alla böcker i rätt genre, eller skriv ut att inga böcker i genre finns
            List<Bok> filtreraEfterGenre = databas.AllBooksFromDb.Where(bok => bok.Genre == genre).ToList();
            if (filtreraEfterGenre.Count == 0)
            {
                Console.WriteLine("Det finns ingen bok i biblioteket inom genren " + genre);
            }
            else
            {
                Console.WriteLine($"Alla {genre} böcker:");
                filtreraEfterGenre.ForEach(bok =>
                {
                    Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Author},  id:{bok.BokId}, publiseringsår: {bok.PublishingYear}, isbn: {bok.Isbn}  ");
                });
            }

            ÅteranvändbarText.GåTillbakaTillMeny("");
        }


       
        public void SorteraBöckerEnligtFörfattare()
        {
            //skapa en ny lista som sorterar böckerna efter författares namn ordning
            List<Bok> SorteraEnligtFörfattare = databas.AllBooksFromDb.OrderBy(bok => bok.Author).ToList();

            string författare = "";
            //skriv ut alla böckers egenskaper i listan. När den kommer till nästa författare: skriv ut författarens namn
            SorteraEnligtFörfattare.ForEach(bok =>
            {
                if (bok.Author != författare)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{bok.Author}");
                    författare = bok.Author;
                }
                Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Author}, id:{bok.BokId}, publiseringsår: {bok.PublishingYear}, isbn: {bok.Isbn}  ");
             
            });
            ÅteranvändbarText.GåTillbakaTillMeny("");

        }

        public void DetaljeradFörfattarInformation()
        {
            ÅteranvändbarText.Header("Alla författare:");

            if (databas.AllAuthorsFromDb.Count != 0)
            {
                databas.AllAuthorsFromDb.ForEach(författare =>
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Namn: {författare.Namn}, Land: {författare.AuthorId}, ID: {författare.AuthorId}");
                    Console.WriteLine("Böcker: ");
                    databas.AllBooksFromDb.ForEach(bok =>
                    {

                        if (bok.Author == författare.Namn)
                        {
                           
                            Console.Write($"Titel: {bok.Titel}, Id:{bok.BokId}, Publiseringsår: {bok.PublishingYear}, ISBN: {bok.Isbn},  Medelrecension: ");
                           
                            if (bok.Resensioner.Count != 0)
                            {
                                Console.WriteLine(bok.Resensioner.Average());
                            }
                            else
                            {
                                Console.WriteLine("Inga resensioner hittils");
                            }
                        }
                       
                    });

                }); 
            }
            else
            {
                Console.WriteLine("Inga författare finns ännu i biblioteket");
            }
            ÅteranvändbarText.GåTillbakaTillMeny("");
        }

       public bool CheckIfAuthorExist(string checkThisAuthor)
       {
            if (databas.AllAuthorsFromDb.Exists(author => author.Namn == checkThisAuthor))
            {
                return true;
            }
            else 
            {
                return false;
            }   
       }

        public bool CheckIfAuthorIdExist(int id)
        {
            if (databas.AllAuthorsFromDb.Exists(author => author.AuthorId == id))
            {
                return true;
            }
            else { return false; }
        }
        public bool CheckIfBookExist(string checkThisTitle)
        {
            if (databas.AllBooksFromDb.Exists(bok => bok.Titel == checkThisTitle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool CheckIfBookIdExist(int id)
        {
            if (databas.AllBooksFromDb.Exists(book => book.BokId == id))
            {
                return true;
            }
            else { return false; }
        }
    }
}
