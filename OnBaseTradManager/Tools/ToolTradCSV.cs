
/*====================================================================*\
Name ........ : ToolTradCSV.cs
Role ........ : Tool to read/write csv
Author ...... : Davide Faga
Date ........ : 28.03.2023
\*====================================================================*/
using OnBaseTradManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace OnBaseTradManager.Tools
{
    static class ToolTradCSV
    {
        /// <summary>
        /// Read traduction file csv OnBase
        /// </summary>
        /// <param name="pathFile"></param>
        /// <returns>List of ObjOnBaseTrad</returns>
        public static List<ObjOnBaseTrad> Read(string pathFile)
        {
            var listObjOnBase = new List<ObjOnBaseTrad>();
            var streamReader = new StreamReader(File.OpenRead(pathFile));
            ObjOnBaseTrad currentObjOnBase;
            string[] values;

            _ = streamReader.ReadLine(); //Read useless headers
            while (!streamReader.EndOfStream)
            {
                string? line = streamReader.ReadLine();
                values = line!.Split(';'); //must not be null

                if (values.Length > 0)
                {
                    if (values.Length == 1)
                        currentObjOnBase = new ObjOnBaseTrad
                        {
                            id = values[0]
                        };

                    else if (values.Length == 2)
                        currentObjOnBase = new ObjOnBaseTrad
                        {
                            id = values[0],
                            description = values[1]
                        };

                    else if (values.Length == 3)
                        currentObjOnBase = new ObjOnBaseTrad
                        {
                            id = values[0],
                            description = values[1],
                            source = values[2]
                        };

                    else
                        currentObjOnBase = new ObjOnBaseTrad
                        {
                            id = values[0],
                            description = values[1],
                            source = values[2],
                            tradEN = values[3]
                        };
                    listObjOnBase.Add(currentObjOnBase);
                }
                else { Debug.WriteLine("!!! un obj louche !!! "); }
            }
            return listObjOnBase;
        }
    }
}
