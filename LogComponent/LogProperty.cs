using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComponent
{
    public sealed class LogProperty
    {
        public LogProperty(string name, object value)
        {
            Name = name;
            Value = value;
        }
        internal string Name { get; }
        internal object Value { get; }
    }
}
