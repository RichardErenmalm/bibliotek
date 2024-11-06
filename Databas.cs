using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading.Tasks;

namespace Bibliotekhanterings_system_Inlamningsuppgift_3
{
    public class Databas
    {
        [JsonPropertyName("books")]
        public Repository<Bok> AllBooksFromDb{  get; set; }

        [JsonPropertyName("authors")]
        public Repository<Author> AllAuthorsFromDb { get; set; }

        public Databas()
        {
            AllBooksFromDb = new Repository<Bok>();
            AllAuthorsFromDb = new Repository<Author>();
        }
    }
}
