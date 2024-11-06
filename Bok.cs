using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bibliotekhanterings_system_Inlamningsuppgift_3
{
    public interface IIdentifiable
    {
        int Id { get; set; }
    }

    public class Bok : IIdentifiable
    {
        public int Id { get; set; } 
        public string Titel { get; set; }
        public string Author{ get; set; }
        public string Genre { get; set; }
        public int PublishingYear {  get; set; }
        public int Isbn { get; set; }
        public List<int> Resensioner { get; set; }

        [JsonConstructor]
        public Bok(int id, string titel, string author, string genre, int publishingYear, int isbn) 
        { 
            Id = id;
            Titel = titel;
            Author = author;
            Genre = genre;
            PublishingYear = publishingYear; 
            Isbn = isbn;

            Resensioner = new List<int>();
        }

    }
}
