using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfoWpfApp
{
    public class LibrarianList<T> : PeopleList<T>
    {
        private List<T> items;

        public LibrarianList() : base(20)
        {
            items = new List<T>(20);
        }
    }
}
