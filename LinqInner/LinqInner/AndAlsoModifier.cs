using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    public class AndAlsoModifier : ExpressionVisitor
    {

        public static void RunDemo()
        {
            Expression<Func<bool, bool, bool>> expr = (left, right) => left | false && right && true;
            Console.WriteLine(expr);

            var modifier = new AndAlsoModifier();
            var exprNew = modifier.Modify(expr);
            Console.WriteLine(exprNew);
        }

        public Expression Modify(Expression expression)
        {
            return Visit(expression);
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.AndAlso)
            {
                Expression left = this.Visit(b.Left);
                Expression right = this.Visit(b.Right);
                return Expression.MakeBinary(ExpressionType.OrElse, left, right, b.IsLiftedToNull, b.Method);
            }
            return base.VisitBinary(b);
        }
    }
}
