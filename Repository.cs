using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekhanterings_system_Inlamningsuppgift_3
{
    public  class Repository<T> where T : class, IIdentifiable
    {
        private List<T> list = new List<T>();

        public void LäggTill(T item)
        {
            list.Add(item);
        }

        public List<T> Hämta()
        {
            return list;
        }

        public void Ändra(T item)
        {
            var index = list.FindIndex(i => (i as IIdentifiable).Id == (item as IIdentifiable).Id);
            if (index >= 0)
            {
                list[index] = item;
            }
        }

        public void TaBort(T item)
        {
            list.Remove(item);
        }
    }
}
