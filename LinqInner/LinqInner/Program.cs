using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    public static class ExtensionMethod
    {
        //Collection of Extension Methods.
        public static bool HasNumberOfType<T>(this IEnumerable<T> sequence
            , int number
            , Func<T, bool> condition)
        {
            int found = 0;
            while(sequence.Any() && found < number) //Wrong?
            {
                sequence = sequence
                    .SkipWhile(condition);
                found++;
                sequence.Skip(1);
            }
            //Slower
            return sequence.Any();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //LINQ to Amazon, LINQ to Azure
            var sequence = Enumerable.Range(0, 100);
            sequence = sequence.SkipWhile(n => n > 10);
            foreach (var item in sequence)
            {
                Console.WriteLine(item);
            }            

            //HasNumberOfType();
            //Performance();
            Console.ReadKey();
        }
        
        public static void HasNumberOfType()
        {
            var sequence = Enumerable.Range(0, 10000);
            var exists = sequence.HasNumberOfType(100, n => n < 10);
            Console.WriteLine(exists);
        }

        public static void Performance()
        {
            var sequence = Enumerable.Range(0, 1000000000);

            //当数量小于10时，使用Count()，否则使用Skip等方法。

            Console.WriteLine("Let's see if there are any number");
#if !Right
            bool exists = sequence.Count() != 0;
#else
            bool exists = sequence.Any();
#endif
            Console.WriteLine("done!");

            Console.WriteLine("Let's see if there are more than 3 numbers");
#if !Right
            bool more = sequence.Count() > 3;
#else
            bool more = sequence.Skip(3).Any(); // If there are more than 3 numbers;
#endif
            Console.WriteLine("done!");
            
            //Console.WriteLine("Let's see if there are any number bigger than 3");
#if !Right
            //bool bigger = sequence.Where(n => n >= 3).Any();
#else
            //bool bigger = sequence.SkipWhile(n => n < 3).Any(); 
            //SkipWhile and TakeWhile is not quite the same as we assume.
#endif

            //Return the next recommendation item(object)
            //Example: Find the next item after the first 6 in a list;
            var numbers = new int[] { 1, 2, 3, 4, 102, 404, 20, 30, 45, 6, 10, 100, 39 };
            var item = numbers.SkipWhile(n => n != 6).Skip(1).FirstOrDefault();
            Console.WriteLine($"The first item after 6 is : {item}");

            Console.WriteLine("done!");
        }
    }
}
