/*====================================================================*\
Name ........ : ToolTradCSV.cs
Role ........ : Tool to read/write csv
Author ...... : Davide Faga
Date ........ : 28.03.2023
\*====================================================================*/
using OnBaseTradManager.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OnBaseTradManager.Tools
{
    static class ToolTradCSV
    {
        /// <summary>
        /// Read traduction file csv OnBase
        /// </summary>
        /// <param name="pathFile"></param>
        /// <returns>List of ObjOnBaseTrad</returns>
        public static List<ObjOnBaseTrad> Read(string pathFile, EnumLanguageOnBase enumLang)
        {
            var listObjOnBaseTrad = new List<ObjOnBaseTrad>();
            var streamReader = new StreamReader(File.OpenRead(pathFile));
            ObjOnBaseTrad currentObjOnBaseTrad;
            string[] values;

            _ = streamReader.ReadLine(); //Read useless headers
            while (!streamReader.EndOfStream)
            {
                string? line = streamReader.ReadLine();
                values = line!.Split(';'); //must not be null

                if (values.Length > 0)
                {
                    if (values.Length == 1)
                        currentObjOnBaseTrad = new ObjOnBaseTrad(values[0]);
                    else if (values.Length == 2)
                        currentObjOnBaseTrad = new ObjOnBaseTrad(values[0], values[1]);
                    else if (values.Length == 3)
                        currentObjOnBaseTrad = new ObjOnBaseTrad(values[0], values[1], values[2]);
                    else
                        currentObjOnBaseTrad = new ObjOnBaseTrad(values[0], values[1], values[2], values[3], enumLang);
                    listObjOnBaseTrad.Add(currentObjOnBaseTrad);
                }
                else { Debug.WriteLine("!!! un obj louche !!! "); }
            }
            return listObjOnBaseTrad;
        }

        public static void Write(string pathFile, List<ObjOnBaseTrad> listObjOnBaseTrad, EnumLanguageOnBase enumLang)
        {
            if (listObjOnBaseTrad == null)
                return;
            if (listObjOnBaseTrad.Count == 0)
                return;
            if (pathFile.IsNullOrEmpty())
                return;

            string separator = ";";
            StringBuilder output = new();

            switch (enumLang)
            {
                case EnumLanguageOnBase.TRADEN:
                    string englishHeader = $"\"Id\";\"Description\";\"Source\";\"Translation (English)\"";
                    output.AppendLine(englishHeader);
                    listObjOnBaseTrad.ForEach(obj =>
                    {
                        string newLine = $"{obj.Id}{separator}{obj.Description}{separator}{obj.Source}{separator}{obj.TradEN}";
                        output.AppendLine(newLine);
                    });
                    break;

                case EnumLanguageOnBase.TRADFR:
                    string frenchHeader = $"\"Id\";\"Description\";\"Source\";\"Translation (French)\"";
                    output.AppendLine(frenchHeader);
                    listObjOnBaseTrad.ForEach(obj =>
                    {
                        string newLine = $"{obj.Id}{separator}{obj.Description}{separator}{obj.Source}{separator}{obj.TradFR}";
                        output.AppendLine(newLine);
                    });
                    break;

                case EnumLanguageOnBase.TRADDE:
                    string germanHeader = $"\"Id\";\"Description\";\"Source\";\"Translation (German)\"";
                    output.AppendLine(germanHeader);
                    listObjOnBaseTrad.ForEach(obj =>
                    {
                        string newLine = $"{obj.Id}{separator}{obj.Description}{separator}{obj.Source}{separator}{obj.TradDE}";
                        output.AppendLine(newLine);
                    });
                    break;
            }

            using StreamWriter writer = new(pathFile, false, Encoding.UTF8);
            writer.Write(output);
        }
    }
}
