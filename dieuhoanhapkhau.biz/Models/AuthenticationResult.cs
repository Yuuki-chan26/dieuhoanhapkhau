using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace dieuhoanhapkhau.biz.Models
{
    public enum LoginResults
    {
        SUCCESS = 0,
        INCORRECT = 1,
        LOCKOUT = 2,
        NOT_ACTIVATED,
        INACTIVE = 3
    }

    [DataContract]
    public class AuthenticationResult
    {
        [DataMember]
        public LoginResults LoginResult { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }



    }
}
