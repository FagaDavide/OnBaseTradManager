using System;

namespace OnBaseTradManager.Models
{
    public class ObjOnBaseTrad
    {
        //CONSTRUCTOR
        public ObjOnBaseTrad() { }

        //VAR
        public string id { get; set; } = String.Empty;
        public string description { get; set; } = String.Empty;
        public string source { get; set; } = String.Empty;
        public string tradEN { get; set; } = String.Empty;
        public string tradFR { get; set; } = String.Empty;
        public string tradDE { get; set; } = String.Empty;

        //FONCTION
        public bool Equals(ObjOnBaseTrad objOnBase)
        {
            if (id.Equals(objOnBase.id) &&
                description.Equals(objOnBase.description) &&
                source.Equals(objOnBase.source))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return id + ", " + description + ", " + source + ", " + tradEN + ", " + tradFR + ", " + tradDE;
        }
    }
}
