using OnBaseTradManager.Models;
using System;
using System.Collections.Generic;
using System.IO;


namespace OnBaseTradManager.Tools
{
    static class ReadTradCSV
    {
        public static List<ObjOnBaseTrad> Read(StreamReader streamReader)
        {
            var listObjOnBase = new List<ObjOnBaseTrad>();
            ObjOnBaseTrad currentObjOnBase;
            var line = streamReader.ReadLine();
            string[] values;

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                values = line!.Split(';'); //doit pas etre null

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
                else { Console.WriteLine("!!! un obj louche !!! "); }
            }
            return listObjOnBase;
        }
    }
}
