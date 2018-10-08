using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    public class Task
    {
        private Project parent;

        public Project Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public int Chapter { get; set; }
        public string Name { get; set; }
    }
}
