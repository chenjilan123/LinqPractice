using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner.LINQtoSQL
{
    public class DatabaseDataContext
    {
        public int MyProperty { get; set; }
        public DatabaseDataContext(DbConnection conn)
        {

        }

        public DatabaseTable<TEntity> GetTable<TEntity>()
        {
            throw new NotImplementedException();
            //CheckDispose();
            //DatabaseMetaTable metaTable = _metaModel.GetTable(typeof(TEntity));
            //if (metaTable == null)
            //    throw new Exception(
            //      string.Format("{0} is not decorated with the TableAttribute.",
            //      typeof(TEntity).Name));
            //IDatabaseTable table = GetTable(metaTable);
            //if (table.ElementType != typeof(TEntity))
            //    throw new Exception(
            //      string.Format("It was not possible to find a table for type {0}",
            //      typeof(TEntity).Name));
            //return (DatabaseTable<TEntity>)table;
        }
        //private IDatabaseTable GetTable(DatabaseMetaTable metaTable)
        //{
        //    IDatabaseTable table;
        //    if (!_tables.TryGetValue(metaTable, out table))
        //    {
        //        table = (IDatabaseTable)Activator.CreateInstance(
        //          typeof(DatabaseTable<>).MakeGenericType(
        //            new Type[] { metaTable.EntityType }),
        //            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
        //            new object[] { this, metaTable }, null);
        //        _tables.Add(metaTable, table);
        //    }
        //    return table;
        //}
    }
}
