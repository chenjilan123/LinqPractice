using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner.LINQtoSQL
{
    /// <summary>
    /// Mapping Metadata
    /// </summary>
    [Table]
    public class Airline
    {
        [Column(CanBeNull = true, DbType = "")]
        public string Line { get; set; }
    }
}
