﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.Common
{
    public static class StringExtensions
    {
        public static string ExtractUsernameFromMail(this string text)
        {
            text = text.Substring(0, text.IndexOf("@"));
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(text[0].ToString().ToUpper());
            stringBuilder.Append(text.Substring(1));
            return stringBuilder.ToString();
        }
    }
}