using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idocNet.Core.Reflection
{
    public class MemberException : Exception
    {
        public MemberException(string msg)
            : base(msg)
        {
        }
        public MemberException(string msg, Exception inner)
            : base(msg, inner)
        {
        }
    }
}
