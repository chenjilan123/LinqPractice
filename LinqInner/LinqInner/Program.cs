using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = GetSequenceFromConsole();
            var ordered = sequence.OrderBy(s => s.Length);

            foreach (var item in ordered)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }

    static class LinqExtension
    {
        public static IEnumerable<T> OrderBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> comparer)
            where TKey : IComparable<TKey>
        {
            //return new ICustomOrderedEnumerable<T, TKey>(source, comparer);
            return new CustomOrderedEnumerable<T, TKey>(source, comparer);
        }
        public static IEnumerable<T> ThenBy<T, TKey>(this IOrderingImpl<T> source, Func<T, TKey> comparer)
            where TKey : IComparable<TKey>
        {
            return new CustomOrderedEnumerable<T, TKey>(source, comparer);
        }
    }

}
