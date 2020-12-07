using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfoWpfApp
{
    public class CheckoutLogList<T> where T : class, IComparable<T>, IEnumerable<T>
    {
        private List<T> items;

        public CheckoutLogList()
        {
            items = new List<T>(1000);
        }

        public T this[int index]
        {
            get { return items[index]; }
            set { items.Add(value); }
        }
    }
}
