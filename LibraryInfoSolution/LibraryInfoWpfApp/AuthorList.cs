using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfoWpfApp
{
    public class AuthorList<T> : PeopleList<T>
    {
        private List<T> items;

        public AuthorList() : base(1000)
        {
            items = new List<T>(1000);
        }
    }
}
