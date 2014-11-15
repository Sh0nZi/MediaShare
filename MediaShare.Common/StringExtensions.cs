using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.Common
{
    public static class StringExtensions
    {
        public static string ExtractUsernameFromMail(this string email)
        {
            if (!email.Contains("@"))
            {
                throw new ArgumentException("E-mail must contain '@' symbol","email");
            }
            email = email.Substring(0, email.IndexOf("@"));
            var stringBuilder = new StringBuilder(email);            
            return stringBuilder.ToString();
        }
    }
}