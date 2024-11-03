using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Bibliotekhanterings_system_Inlamningsuppgift_3
{
    public class Author
    {
        public int AuthorId { get; set; }    
        public string Namn {  get; set; }
        public string Land {  get; set; }

        public Author(int authorId, string namn, string land)
        {
            AuthorId = authorId;
            Namn = namn;
            Land = land;
        }
    }
}
