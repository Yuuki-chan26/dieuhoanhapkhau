using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace dieuhoanhapkhau.web.Helpers
{
    public class HelperConvertText
    {
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            string lowerStr = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
            //return Regex.Replace(lowerStr, "[^a-zA-Z0-9_.-]", "-");
            return lowerStr;
        }
        public static string mahoa_sha1(string _str)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(_str);
            bs = sha1.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x1"));
            }
            _str = s.ToString();
            return _str;
        }
    }
}