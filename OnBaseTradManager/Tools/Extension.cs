using Microsoft.IdentityModel.Tokens;


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
