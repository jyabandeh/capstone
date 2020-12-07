using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfoWpfApp
{
    public abstract class PeopleList<T>
    {
        private List<T> items;

        public PeopleList(int itemsCapacity)
        {
            items = new List<T>(itemsCapacity);
        }

        public T this[int index]
        {
            get { return items[index]; }
            set { items.Add(value); }
        }
    }
}
