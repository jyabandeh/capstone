using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfoWpfApp
{
    public class CardholderList<T> : PeopleList<T>
    {
        private List<T> items;

        public CardholderList() : base(2000)
        {
            items = new List<T>(2000);
        }
    }
}
