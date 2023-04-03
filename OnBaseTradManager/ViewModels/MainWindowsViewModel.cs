/*====================================================================*\
Name ........ : MainWindowsViewModel.cs
Role ........ : Main ViewModel manage all datas
Author ...... : Davide Faga
Date ........ : 28.03.2023
\*====================================================================*/
using Microsoft.IdentityModel.Tokens;
using OnBaseTradManager.Models;
using OnBaseTradManager.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace OnBaseTradManager.ViewModels
{
    public class MainWindowsViewModel : BaseViewModel
    {
        /*------------------------------------------------------------------*\
        |*							CONSTANTE                				*|
        \*------------------------------------------------------------------*/
        private const string FILENAME_SAVE_EN = "TraductionOnBaseEN";
        private const string FILENAME_SAVE_FR = "TraductionOnBaseFR";
        private const string FILENAME_SAVE_DE = "TraductionOnBaseDE";
        private const string FILENAME_SAVE_EXTENTION = ".csv";

        /*------------------------------------------------------------------*\
        |*							VARIABLE                				*|
        \*------------------------------------------------------------------*/
        #region VARIABLE
        private List<ObjOnBaseTrad> listObjOnBaseREF = new();
        private Dictionary<string, ObjOnBaseTrad> dicoObjOnBaseModifiedEN = new();
        private Dictionary<string, ObjOnBaseTrad> dicoObjOnBaseModifiedFR = new();
        private Dictionary<string, ObjOnBaseTrad> dicoObjOnBaseModifiedDE = new();
        private ObservableCollection<ObjOnBaseTrad> obsObjOnBaseTradTMP = new();
        private ObservableCollection<ObjOnBaseTrad> obsObjOnBaseTrad = new();
        private bool isBtnLoadObsClickable = false;
        private bool isBtnLoadDataClickable = true;
        private bool isBtnFilterClickable = false;
        private bool isBtnSaveClickable = false;
        private string filter = String.Empty;
        private string pathFileEN = String.Empty;
        private string pathFileFR = String.Empty; 
        private string pathFileDE = String.Empty;
        private string pathSave = String.Empty;
        #endregion
        /*------------------------------------------------------------------*\
        |*							PROPERTIE                				*|
        \*------------------------------------------------------------------*/
        #region PROPERTIE
        public ObservableCollection<ObjOnBaseTrad> ObsObjOnBaseTrad
        {
            get { return obsObjOnBaseTrad; }
            set
            {
                obsObjOnBaseTrad = value;
                OnPropertyChanged();
            }
        }

        public bool IsBtnLoadDataClickable
        {
            get { return isBtnLoadDataClickable; }
            set
            {
                isBtnLoadDataClickable = value;
                OnPropertyChanged();
            }
        }

        public bool IsBtnLoadObsClickable
        {
            get { return isBtnLoadObsClickable; }
            set
            {
                isBtnLoadObsClickable = value;
                OnPropertyChanged();
            }
        }

        public bool IsBtnFilterClickable
        {
            get { return isBtnFilterClickable; }
            set
            {
                isBtnFilterClickable = value;
                OnPropertyChanged();
            }
        }

        public bool IsBtnSaveClickable
        {
            get { return isBtnSaveClickable; }
            set
            {
                isBtnSaveClickable = value;
                OnPropertyChanged();
            }
        }

        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnPropertyChanged();
            }
        }
        public string PathFileEN
        {
            get { return pathFileEN; }
            set
            {
                pathFileEN = value;
                OnPropertyChanged();
            }
        }
        public string PathFileFR
        {
            get { return pathFileFR; }
            set
            {
                pathFileFR = value;
                OnPropertyChanged();
            }
        }
        public string PathFileDE
        {
            get { return pathFileDE; }
            set
            {
                pathFileDE = value;
                OnPropertyChanged();
            }
        }
        public string PathSave
        {
            get { return pathSave; }
            set
            {
                pathSave = value;
                OnPropertyChanged();
            }
        }
        #endregion
        /*------------------------------------------------------------------*\
        |*							CONSTRUCTOR     						*|
        \*------------------------------------------------------------------*/
        public MainWindowsViewModel() { }

        /*------------------------------------------------------------------*\
        |*							FUNCTION        						*|
        \*------------------------------------------------------------------*/
        #region FONCTION
        public void AddObjOnBaseTradModifiedToDico(ObjOnBaseTrad objOnBaseTradModified, string? strLang, string? newValue)
        {
            if (strLang == null || newValue == null)
                return;

            //needed to be ok with standard OnBase
            newValue = newValue.AddDoubleQuoteOnlyStartEnd();

            EnumLanguageOnBase enumLang = (EnumLanguageOnBase)Enum.Parse(typeof(EnumLanguageOnBase), strLang.ToUpper());

            switch (enumLang)
            {
                case EnumLanguageOnBase.TRADEN:
                    objOnBaseTradModified.TradEN = newValue;
                    if (dicoObjOnBaseModifiedEN.ContainsKey(objOnBaseTradModified.Id))
                        dicoObjOnBaseModifiedEN[objOnBaseTradModified.Id] = objOnBaseTradModified;
                    else
                        dicoObjOnBaseModifiedEN.Add(objOnBaseTradModified.Id, objOnBaseTradModified);
                    break;
                case EnumLanguageOnBase.TRADFR:
                    objOnBaseTradModified.TradFR = newValue;
                    if (dicoObjOnBaseModifiedFR.ContainsKey(objOnBaseTradModified.Id))
                        dicoObjOnBaseModifiedFR[objOnBaseTradModified.Id] = objOnBaseTradModified;
                    else
                        dicoObjOnBaseModifiedFR.Add(objOnBaseTradModified.Id, objOnBaseTradModified);
                    break;
                case EnumLanguageOnBase.TRADDE:
                    objOnBaseTradModified.TradDE = newValue;
                    if (dicoObjOnBaseModifiedDE.ContainsKey(objOnBaseTradModified.Id))
                        dicoObjOnBaseModifiedDE[objOnBaseTradModified.Id] = objOnBaseTradModified;
                    else
                        dicoObjOnBaseModifiedDE.Add(objOnBaseTradModified.Id, objOnBaseTradModified);
                    break;
                default:
                    throw new Exception("The Lang Value IS NOT TradEN or TradFR or TradDE");
            }

            IsBtnSaveClickable = dicoObjOnBaseModifiedEN.Count > 0 ||
                                 dicoObjOnBaseModifiedFR.Count > 0 ||
                                 dicoObjOnBaseModifiedDE.Count > 0;
        }
        public void LoadObs()
        {
            ObsObjOnBaseTrad = obsObjOnBaseTradTMP;
        }

        public void WriteDATA()
        {
            string date = "_" + DateTime.Today.ToString("yyyy-MM-dd");
            ToolTradCSV.Write(PathSave + "\\" + FILENAME_SAVE_EN + date + FILENAME_SAVE_EXTENTION, dicoObjOnBaseModifiedEN.Values.ToList(), EnumLanguageOnBase.TRADEN);
            ToolTradCSV.Write(PathSave + "\\" + FILENAME_SAVE_FR + date + FILENAME_SAVE_EXTENTION, dicoObjOnBaseModifiedFR.Values.ToList(), EnumLanguageOnBase.TRADFR);
            ToolTradCSV.Write(PathSave + "\\" + FILENAME_SAVE_DE + date + FILENAME_SAVE_EXTENTION, dicoObjOnBaseModifiedDE.Values.ToList(), EnumLanguageOnBase.TRADDE);
        }

        /// <summary>
        /// Filter Data by source
        /// </summary>
        /// <param name="filter"></param>
        public void FilterDATA()
        {
            IsBtnLoadObsClickable = false;
            IsBtnFilterClickable = false;
            var query = listObjOnBaseREF.Where(ob => ob.Source
                                       .Contains(filter, StringComparison.OrdinalIgnoreCase))
                                       .ToList();

            obsObjOnBaseTradTMP = new ObservableCollection<ObjOnBaseTrad>(query);
            IsBtnLoadObsClickable = true;
            IsBtnFilterClickable = true;
        }
        /// <summary>
        /// Load data reading csv EN-FR-DE
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ReadDATA()
        {
            dicoObjOnBaseModifiedEN.Clear();
            dicoObjOnBaseModifiedFR.Clear();
            dicoObjOnBaseModifiedDE.Clear();
            listObjOnBaseREF.Clear();
            IsBtnFilterClickable = false;
            IsBtnLoadObsClickable = false;
            Filter = String.Empty;
            List<ObjOnBaseTrad> listObjOnBaseEN = new();
            List<ObjOnBaseTrad> listObjOnBaseFR = new();
            List<ObjOnBaseTrad> listObjOnBaseDE = new();
            Dictionary<EnumLanguageOnBase, List<ObjOnBaseTrad>> listVerification = new();

            if (!pathFileEN.IsNullOrEmpty())
            {
                listObjOnBaseEN = ToolTradCSV.Read(PathFileEN, EnumLanguageOnBase.TRADEN);
                listVerification.Add(EnumLanguageOnBase.TRADEN, listObjOnBaseEN);
            }
            if (!pathFileFR.IsNullOrEmpty())
            {
                listObjOnBaseFR = ToolTradCSV.Read(PathFileFR, EnumLanguageOnBase.TRADFR);
                listVerification.Add(EnumLanguageOnBase.TRADFR, listObjOnBaseFR);
            }
            if (!pathFileDE.IsNullOrEmpty())
            {
                listObjOnBaseDE = ToolTradCSV.Read(PathFileDE, EnumLanguageOnBase.TRADDE);
                listVerification.Add(EnumLanguageOnBase.TRADDE, listObjOnBaseDE);
            }

            if (listVerification.Count == 0)
                return;


            for (int i = 0; i < listVerification.Count; i++)
            {
                if (listVerification.ElementAt(i).Value.Count != listVerification.ElementAt((i + 1) % listVerification.Count).Value.Count)
                {
                    throw new Exception("The number of records (lines) of the different language files are not identical." +
                     " This implies that some OnBase attributes are in one file but not in the other. \n\nSolution :" +
                     " Export from OnBase up-to-date translation files for each language");
                }
            }

            var listTMP = listVerification.ElementAt(0).Value;
            for (int i = 0; i < listTMP.Count; i++)
                listObjOnBaseREF.Add(new ObjOnBaseTrad(listTMP[i].Id, listTMP[i].Description, listTMP[i].Source));

            for (int i = 0; i < listVerification.Count; i++)
            {
                EnumLanguageOnBase enumLang = listVerification.ElementAt(i).Key;
                for (int j = 0; j < listVerification.ElementAt(0).Value.Count; j++)
                {
                    var currentTrad = listVerification.ElementAt(i).Value[j];
                    var otherTrad = listVerification.ElementAt((i + 1) % listVerification.Count).Value[j];
                    if (listVerification.ElementAt(i).Value[j].Equals(listVerification.ElementAt((i + 1) % listVerification.Count).Value[j]))
                    {
                        switch (enumLang)
                        {
                            case EnumLanguageOnBase.TRADEN:
                                listObjOnBaseREF[j].TradEN = currentTrad.TradEN;
                                break;
                            case EnumLanguageOnBase.TRADFR:
                                listObjOnBaseREF[j].TradFR = currentTrad.TradFR;
                                break;
                            case EnumLanguageOnBase.TRADDE:
                                listObjOnBaseREF[j].TradDE = currentTrad.TradDE;
                                break;
                        }
                    }
                    else
                    {
                        listObjOnBaseREF.Clear();
                        IsBtnLoadDataClickable = true;
                        Debug.WriteLine("Each Object Verification NOT OK (id, description and source)");
                        throw new Exception("Every attribute in one language must be in the other with the same matches for ID, Description and Source" +
                             "\n\nSolution: Export from OnBase up-to-date translation files for each language");
                    }
                }
            }

            IsBtnFilterClickable = true;
        }
        #endregion
    }
}
