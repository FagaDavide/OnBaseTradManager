using Microsoft.Graph.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBaseTradManager.Tools
{
    public static class Extension
    {
        public static string RemoveDoubleQuote(this string str)
        {
            return str.Replace("\"", "");
        }

        public static string AddDoubleQuoteOnlyStartEnd(this string str)
        {
            if(str.IsNullOrEmpty()) return string.Empty;
            return "\"" + str.Replace("\"", "") + "\"";
        }
    }
}
