/*====================================================================*\
Name ........ : ObjOnBaseTrad.cs
Role ........ : Model to represente a objet traduction from csv
Author ...... : Davide Faga
Date ........ : 28.03.2023
\*====================================================================*/
using OnBaseTradManager.Tools;
using System;

namespace OnBaseTradManager.Models
{
    public class ObjOnBaseTrad
    {
        /*------------------------------------------------------------------*\
        |*							CONSTRUCTOR     						*|
        \*------------------------------------------------------------------*/
        public ObjOnBaseTrad() { }
        public ObjOnBaseTrad(string id) => Id = id;
        public ObjOnBaseTrad(string id, string description) { Id = id; Description = description; }
        public ObjOnBaseTrad(string id, string description, string source) { Id = id; Description = description; Source = source; }
        public ObjOnBaseTrad(string id, string description, string source, string trad, EnumLanguageOnBase enumLang)
        {
            Id = id; Description = description; Source = source;
            switch (enumLang)
            {
                case EnumLanguageOnBase.TRADEN:
                    TradEN = trad;
                    break;
                case EnumLanguageOnBase.TRADFR:
                    TradFR = trad;
                    break;
                case EnumLanguageOnBase.TRADDE:
                    TradDE = trad;
                    break;
            }
        }
        public ObjOnBaseTrad(ObjOnBaseTrad copyObjOnBaseTrad)
        {
            Id = copyObjOnBaseTrad.Id;
            Description = copyObjOnBaseTrad.Description;
            Source = copyObjOnBaseTrad.Source;
            TradEN = copyObjOnBaseTrad.TradEN;
            TradFR = copyObjOnBaseTrad.TradFR;
            TradDE = copyObjOnBaseTrad.TradDE;
        }

        /*------------------------------------------------------------------*\
        |*							VARIABLE                				*|
        \*------------------------------------------------------------------*/
        public string Id { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Source { get; set; } = String.Empty;
        public string TradEN { get; set; } = String.Empty;
        public string TradFR { get; set; } = String.Empty;
        public string TradDE { get; set; } = String.Empty;

        /*------------------------------------------------------------------*\
        |*							FONCTION        						*|
        \*------------------------------------------------------------------*/
        public bool Equals(ObjOnBaseTrad objOnBase)
        {
            if (Id.Equals(objOnBase.Id) &&
                Description.Equals(objOnBase.Description) &&
                Source.Equals(objOnBase.Source))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Id + "; " + Description + "; " + Source + "; " + TradEN + "; " + TradFR + "; " + TradDE;
        }
    }
}
