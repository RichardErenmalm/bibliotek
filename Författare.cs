using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Bibliotekhanterings_system_Inlamningsuppgift_3
{
    public class Author : IIdentifiable
    {
        public int Id { get; set; }    
        public string Namn {  get; set; }
        public string Land {  get; set; }

        public Author(int id, string namn, string land)
        {
            Id = id;
            Namn = namn;
            Land = land;
        }
    }
}
