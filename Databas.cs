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
        public List<Bok> AllBooksFromDb{  get; set; }

        [JsonPropertyName("authors")]
        public List<Author> AllAuthorsFromDb { get; set; }

        public Databas()
        {
            AllBooksFromDb = new List<Bok>();
            AllAuthorsFromDb = new List<Author>();
        }
    }
}
