using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    public interface IOrderingImpl<T> : IEnumerable<T>
    {
        int CompareTo(T left, T right);
        IEnumerable<T> OriginalSource { get; }
    }

    public class CustomOrderedEnumerable<T, TKey> : IOrderingImpl<T>, IEnumerable<T> where TKey : IComparable<TKey>
    {
        private Comparison<T> comparison;
        private IEnumerable<T> source;
        public CustomOrderedEnumerable(IEnumerable<T> source, Func<T, TKey> comparer)
        {
            this.source = source;
            this.comparison = (a, b) => comparer(a).CompareTo(comparer(b));
        }
        public IEnumerable<T> OriginalSource
        {
            get
            {
                return source;
            }
        }

        public int CompareTo(T left, T right)
        {
            return comparison(left, right);
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
