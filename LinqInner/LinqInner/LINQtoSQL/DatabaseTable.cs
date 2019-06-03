using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner.LINQtoSQL
{
    public sealed class DatabaseTable<TEntity> : IQueryable<TEntity>
    {
        public IQueryProvider Provider => throw new NotImplementedException();

        public IEnumerator<TEntity> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public Type ElementType
        {
            get { return typeof(TEntity); }
        }

        public Expression Expression
        {
            get { return Expression.Constant(this); }
        }

        //public IQueryProvider Provider
        //{
        //    get { return this; }
        //}

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
