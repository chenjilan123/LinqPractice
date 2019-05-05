using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicExpressionTree();
            return;

            //Console.WriteLine(System.Type.FilterAttribute.ToString());
            //Console.WriteLine(System.Type.FilterName);
            //Console.WriteLine(System.Type.FilterNameIgnoreCase);
            //Console.WriteLine(System.Type.Missing);
            //Console.WriteLine(System.Type.Delimiter);
            //Console.WriteLine(System.Type.EmptyTypes.Length);
            //return;

            //SimpleExpressionTressWithParameter();
            //CalculateFactorialOfaNumber();
            //ParsingExpressionTree();
            //CompileExpressionTree();

            //ExecuteExpressionTree();

            //ModifyExpressionTree();

            //AndAlsoModifier.RunDemo();

            //BuildDynamicQuery.RunDemo();
            //Console.ReadKey();
        }

        #region ExpressionTreeType
        private static void ExpressionTreeType()
        {
            var type = typeof(Func<int>);
            Expression<Func<int>> expr = () => 5;
        }
        #endregion

        #region ExpressionTree
        private static void ExpressionTree()
        {
            // 1
            Expression<Action<int>> expr = i => Console.WriteLine(i);
            //Wrong
            //Expression<Action<int>> expr = i => { Console.WriteLine(i); };
            Action<int> action1 = i => { Console.WriteLine(i); };
            Action<int> action2 = i => Console.WriteLine(i);
            Console.WriteLine(expr);
            expr.Compile()(5);

            // 2
            ParameterExpression param1 = Expression.Parameter(typeof(int), "i");
            ParameterExpression param2 = Expression.Parameter(typeof(int), "i");
            MethodCallExpression methodCall1 = Expression.Call(
                typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }),
                param1);
            var expr2 = Expression.Lambda<Action<int>>(methodCall1, param1);
            //Wrong
            //var expr2 = Expression.Lambda<Action<int>>(methodCall, param2);
            expr2.Compile()(2);

            // 3.Block
            MethodCallExpression methodCall2 = Expression.Call(
                typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }),
                Expression.Constant(500));
            var block = Expression.Block(methodCall1, methodCall2, methodCall2);
            var expr3 = Expression.Lambda<Action<int>>(
                block,
                new ParameterExpression[] { param1 });
            expr3.Compile()(50);

        }
        #endregion

        #region DynamicExpressionTree
        private static void DynamicExpressionTree()
        {
            var func1 = ETFact();
            var i = func1(5);
            Console.WriteLine(i);

            var func2 = ILFact();
            var j = func2(6);
            Console.WriteLine(j);
        }

        //ExpressionTree°æ±¾
        private static Func<int, int> ETFact()
        {
            var value = Expression.Parameter(typeof(int), "value");
            var result = Expression.Variable(typeof(int), "result");
            var label = Expression.Label(typeof(int));
            var block = Expression.Block(
                new[] { result }
                , Expression.Assign(result, Expression.Constant(1))
                , Expression.Loop(Expression.IfThenElse(
                    Expression.GreaterThan(value, Expression.Constant(1))
                    , Expression.MultiplyAssign(result, Expression.PostDecrementAssign(value))
                    , Expression.Break(label, result))
                    , label)
                );
            return Expression.Lambda<Func<int, int>>(block, value).Compile();
        }

        //MSIL°æ±¾
        static Func<int, int> ILFact()

        {

            var method = new DynamicMethod(

      "factorial", typeof(int),

            new[] { typeof(int) }

            );

            var il = method.GetILGenerator();

            var result = il.DeclareLocal(typeof(int));

            var startWhile = il.DefineLabel();

            var returnResult = il.DefineLabel();




            // result = 1

            il.Emit(OpCodes.Ldc_I4_1);

            il.Emit(OpCodes.Stloc, result);




            // if (value <= 1) branch end

            il.MarkLabel(startWhile);

            il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Ldc_I4_1);

            il.Emit(OpCodes.Ble_S, returnResult);




            // result *= (value¨C)

            il.Emit(OpCodes.Ldloc, result);

            il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Dup);

            il.Emit(OpCodes.Ldc_I4_1);

            il.Emit(OpCodes.Sub);

            il.Emit(OpCodes.Starg_S, 0);

            il.Emit(OpCodes.Mul);

            il.Emit(OpCodes.Stloc, result);




            // end while

            il.Emit(OpCodes.Br_S, startWhile);




            // return result

            il.MarkLabel(returnResult);

            il.Emit(OpCodes.Ldloc, result);

            il.Emit(OpCodes.Ret);




            return (Func<int, int>)method.CreateDelegate(typeof(Func<int, int>));

        }

        private static int CSharpFact(int value)
        {
            int result = 1;
            while (value > 1)
            {
                result *= value--;
            }
            return result;
        }
        #endregion

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
