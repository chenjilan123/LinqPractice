using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    public class ICustomOrderedEnumerable<T, TKey> : IEnumerable<T>
        where TKey : IComparable<TKey>
    {
        private Comparison<T> comparison;
        private IEnumerable<T> source;

        public ICustomOrderedEnumerable(IEnumerable<T> source, Func<T, TKey> comparer)
        {
            this.source = source;
            this.comparison = (a, b) => comparer(a).CompareTo(comparer(b));
        }

        public IEnumerator<T> GetEnumerator()
        {
            var sorted = source.ToList();
            sorted.Sort(comparison);
            return sorted.GetEnumerator();
        }

        
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
