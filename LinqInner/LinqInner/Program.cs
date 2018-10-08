using System;
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
            SelectManyExpression();
            Console.ReadKey();
        }
        
        public static void SelectManyExpression()
        {
            var oddNumbers = new int[] { 1, 3, 5, 7, 9 };
            var evenNumbers = new int[] { 2, 4, 6, 8, 10 };
            //���Ը���ά��, ����from=>��ά���ĸ�from=>��ά��
            //��������������ʱ����ʱSelectMany�����ã�ѡ���游���Ӽ����Ӽ����Ӽ����游���������Ӽ����б�(flatten)��
            //�൱��ö���Ӽ���ö���Ӽ����Ӽ��ȵ�...
#if !MethodExpression
            var query = from odd in oddNumbers
                        from even in evenNumbers
#if OnlyOddIsBigger
                        where odd > even
#endif
                        select new
                        {
                            OddNumber = odd,
                            EvenNumber = even,
                            Sum = odd + even,
                            Product = odd * even,
                        };
#else
            var query = oddNumbers.SelectMany(odd => evenNumbers,
#if OnlyOddIsBigger
                            (oddNumber, evenNumber) =>
                            new
                            {
                                OddNumber = oddNumber,
                                EvenNumber = evenNumber,
                            })
                            .Where(pair => pair.OddNumber > pair.EvenNumber)
                            .Select(pair =>
                            new
                            {
                                OddNumber = pair.OddNumber,
                                EvenNumber = pair.EvenNumber,
                                Sum = pair.OddNumber + pair.EvenNumber,
                                Product = pair.OddNumber * pair.EvenNumber,
                            });
#else
                            (oddNumber, evenNumber) =>
                            new
                            {
                                OddNumber = oddNumber,
                                EvenNumber = evenNumber,
                                Sum = oddNumber + evenNumber,
                                Product = oddNumber * evenNumber,
                            });
#endif

#endif
            foreach (var item in query)
            {
                Console.WriteLine(
                    $" Odd : {item.OddNumber}\t" +
                    $"Even : {item.EvenNumber}\t" +
                    $" Sum : {item.Sum}\t" +
                    $"Product: {item.Product}");
            }
        }
    }
}
