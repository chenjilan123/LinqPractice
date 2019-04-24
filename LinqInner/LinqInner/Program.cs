using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    class Program
    {
        static void Main(string[] args)
        {
            //SimpleExpressionTressWithParameter();
            //CalculateFactorialOfaNumber();
            //ParsingExpressionTree();
            //CompileExpressionTree();

            //ExecuteExpressionTree();

            ModifyExpressionTree();
            Console.ReadKey();
        }

        #region Intial
        private static void SimpleExpressionTree()
        {
            Expression<Func<int, bool>> lambda = num => num < 5;
        }

        private static void SimpleExpressionTressWithParameter()
        {
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression five = Expression.Constant(5, typeof(int));
            BinaryExpression numLessThenFive = Expression.LessThan(numParam, five);

            Expression<Func<int, bool>> lambda =
                Expression.Lambda<Func<int, bool>>(
                    numLessThenFive,
                    new ParameterExpression[] { numParam });
            Console.WriteLine(lambda.ToString());
        }

        /// <summary>
        /// ******************************************************
        /// </summary>
        private static void CalculateFactorialOfaNumber()
        {
            ParameterExpression value = Expression.Parameter(typeof(int), "value");
            ParameterExpression result = Expression.Parameter(typeof(int), "result");

            LabelTarget label = Expression.Label(typeof(int));

            BlockExpression block = Expression.Block(
                new[] { result },
                Expression.Assign(result, Expression.Constant(1)),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.GreaterThan(value, Expression.Constant(1)),
                            Expression.MultiplyAssign(result,
                                Expression.PostDecrementAssign(value)),
                            Expression.Break(label, result)
                            ),
                        label
                        ));
            var expr = Expression.Lambda<Func<int, int>>(block, value);
            Console.WriteLine(expr.ToString());
            int factorial = expr.Compile()(5);
            Console.WriteLine(factorial);
        }

        private static void ParsingExpressionTree()
        {
            Expression<Func<int, bool>> exprTree = num => num >= 5;

            ParameterExpression param = (ParameterExpression)exprTree.Parameters[0];
            BinaryExpression operation = (BinaryExpression)exprTree.Body;
            ParameterExpression left = (ParameterExpression)operation.Left;
            ConstantExpression right = (ConstantExpression)operation.Right;

            Console.WriteLine($"Decomposed expression: {param.Name} => {left.Name} {operation.NodeType} {right.Value}");
            //Decomposed expression: num => num GreaterThanOrEqual 5
        }

        private static void CompileExpressionTree()
        {
            Expression<Func<int, bool>> expr = num => num > 5;

            var func = expr.Compile();
            Console.WriteLine(func);
            Console.WriteLine(func(5));
            Console.WriteLine(expr.Compile()(5));

        }

        static IEnumerable<string> GetSequenceFromConsole()
        {
            var input = string.Empty;
            input = Console.ReadLine();
            while (input != "end!")
            {
                yield return input;
                input = Console.ReadLine();
            }
        }
        #endregion

        #region ExecuteExpressionTree
        private static void ExecuteExpressionTree()
        {
            BinaryExpression be = Expression.Power(Expression.Constant(2D), Expression.Constant(3D));

            Expression<Func<double>> le = Expression.Lambda<Func<double>>(be);

            Func<double> compiledExpression = le.Compile();

            var result = compiledExpression();
            Console.WriteLine(result);
        }
        #endregion

        #region ModifyExpressionTree
        private static void ModifyExpressionTree()
        {
            Expression<Func<int, int>> expr = num => num + 5;

            Console.WriteLine(expr.ToString());
        }
        #endregion
    }
}
