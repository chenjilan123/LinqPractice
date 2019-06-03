using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    public class BuildDynamicQuery
    {
        public static void RunDemo()
        {
            string[] companies = new[] { "Google", "Apple", "Huawei", "Microsoft" };

            IQueryable<string> queryableData = companies.AsQueryable();

            //Build Expression Tree
            //var query = companies.Where(company => company.ToLower() == "apple" || company.Length > 7).OrderBy(company => company);
            ParameterExpression pe = Expression.Parameter(typeof(string), "company");
            Expression left = Expression.Call(pe, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            Expression right = Expression.Constant("apple", typeof(string));

            Expression e1 = Expression.Equal(left, right);
            //Expression e1 = Expression.MakeBinary(ExpressionType.Equal, left, right);

            left = Expression.Property(pe, typeof(string).GetProperty("Length"));
            right = Expression.Constant(7, typeof(int));

            Expression e2 = Expression.GreaterThan(left, right);
            //Expression e2 = Expression.MakeBinary(ExpressionType.GreaterThan, left, right);

            Expression e3 = Expression.OrElse(e1, e2);
            //Expression e3 = Expression.MakeBinary(ExpressionType.OrElse, e1, e2);

            //(company.ToLower() == "Apple")
            //Console.WriteLine(e1);
            //(company.Length > 7)
            //Console.WriteLine(e2);
            //((company.ToLower() == "Apple") OrElse(company.Length > 7))
            //Console.WriteLine(e3);

            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { queryableData.ElementType },
                queryableData.Expression, //调用者
                Expression.Lambda<Func<string, bool>>(e3, new ParameterExpression[] { pe }) //参数体
                );

            //System.String[]
            //Console.WriteLine(queryableData.Expression);
            //System.String[].Where(company => ((company.ToLower() == "Apple") OrElse(company.Length > 7)))
            //Console.WriteLine(whereCallExpression);

            MethodCallExpression orderByCallExpression = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                new Type[] { queryableData.ElementType, queryableData.ElementType },
                whereCallExpression,
                Expression.Lambda<Func<string, string>>(pe, new[] { pe }));

            //System.String[].Where(company => ((company.ToLower() == "Apple") OrElse(company.Length > 7))).OrderBy(company => company)
            //Console.WriteLine(orderByCallExpression);

            //Execute
            var results = queryableData.Provider.CreateQuery<string>(orderByCallExpression);

            foreach (var company in results)
                Console.WriteLine(company);
        }


    }
}
