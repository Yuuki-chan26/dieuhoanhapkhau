using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz
{
    [DataContract]
    public class Setting
    {
        [DataMember]
        public string PasswordStrength { get; set; }

        [DataMember]
        public string PasswordInvalidMessage { get; set; }

        [DataMember]
        public bool EnableLockout { get; set; }

        [DataMember]
        public int FailedPasswordAttemptCount { get; set; }

        [DataMember]
        public int MinutesToUnlock { get; set; }





        private static Setting _current;
        public static Setting Current
        {
            get
            {

                if (_current == null)
                {
                    _current = new Setting()
                    {
                        EnableLockout = true,
                        FailedPasswordAttemptCount = 5,
                        MinutesToUnlock = 15,
                        PasswordInvalidMessage = "The password is invalid.",
                        PasswordStrength = ".{5,}"
                    };

                }

                return _current;
            }
        }

    }
}
