using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    public static class LinqExtension
    {
        public static IOrderingImpl<T> OrderBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> comparer)
            where TKey : IComparable<TKey>
        {
            //return new ICustomOrderedEnumerable<T, TKey>(source, comparer);
            return new CustomOrderedEnumerable<T, TKey>(source, comparer);
        }
        public static IOrderingImpl<T> OrderByDescending<T, TKey>(this IEnumerable<T> source, Func<T, TKey> comparer)
            where TKey : IComparable<TKey>
        {
            Comparison<T> descendingComparer = (left, right) => comparer(right).CompareTo(comparer(left));
            return new CustomOrderedEnumerable<T, TKey>(source, descendingComparer);
        }
        public static IOrderingImpl<T> ThenBy<T, TKey>(this IOrderingImpl<T> source, Func<T, TKey> comparer)
            where TKey : IComparable<TKey>
        {
            return new CustomOrderedEnumerable<T, TKey>(source, comparer);
        }
    }
}
