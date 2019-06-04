using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idocNet.Core.Reflection
{
    public class MemberNotFoundException : Exception
    {
        public MemberNotFoundException(string msg)
            : base(msg)
        {
        }
        public MemberNotFoundException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        public string PropertyName { get; set; }
        public Type Type { get; set; }
    }
}
