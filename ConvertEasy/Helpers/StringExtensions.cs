using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConvertEasy.Helpers
{
    public class StringExtensions
    {
        public static string ToFriendlyUrl(string urlToEncode)
        {
            string engChars = urlToEncode.Trim().ToLower();
            string[] oldValues = new string[] { "Ç", "ç", "Ğ", "ğ", "İ", "ı", "I", "Ö", "ö", "Ş", "ş", "Ü", "ü", "!", "?", ",", ":", "`", "'", " " };
            string[] newValues = new string[] { "c", "c", "g", "g", "i", "i", "i", "o", "o", "s", "s", "u", "u", "", "", "", "", "", "", "_" };
            for (int i = 0; i < oldValues.Length; i++)
                engChars = engChars.Replace(oldValues[i], newValues[i]);
            engChars = engChars.Trim();
            return engChars;
        }
    }
}