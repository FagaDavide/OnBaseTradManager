/*====================================================================*\
Name ........ : MainWindowsViewModel.cs
Role ........ : Main ViewModel manage all datas
Author ...... : Davide Faga
Date ........ : 28.03.2023
\*====================================================================*/
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
        |*							CONSTANTE        						*|
        \*------------------------------------------------------------------*/
        const string pathFileEN = @"C:\test\DEV_EN_OUT_20230327.csv";
        const string pathFileFR = @"C:\test\DEV_FR_OUT_20230327.csv";
        const string pathFileDE = @"C:\test\DEV_DE_OUT_20230327.csv";

        /*------------------------------------------------------------------*\
        |*							VARIABLE                				*|
        \*------------------------------------------------------------------*/
        private List<ObjOnBaseTrad> listObjOnBaseEN = new();
        private Dictionary<string, ObjOnBaseTrad> dicoObjOnBaseModified = new();
        private ObservableCollection<ObjOnBaseTrad> obsObjOnBaseTradTMP = new();
        private ObservableCollection<ObjOnBaseTrad> obsObjOnBaseTrad = new();
        private bool isBtnLoadObsClickable = false;
        private bool isBtnLoadDataClickable = true;
        private bool isBtnFilterClickable = false;
        private string filter = String.Empty;

        /*------------------------------------------------------------------*\
        |*							PROPERTIE                				*|
        \*------------------------------------------------------------------*/
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

        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnPropertyChanged();
            }
        }

        /*------------------------------------------------------------------*\
        |*							CONSTRUCTOR     						*|
        \*------------------------------------------------------------------*/
        public MainWindowsViewModel() { }

        /*------------------------------------------------------------------*\
        |*							FONCTION        						*|
        \*------------------------------------------------------------------*/
        public void AddObjOnBaseTradToDico(ObjOnBaseTrad objOnBaseTradModified)
        {
            if (dicoObjOnBaseModified.ContainsKey(objOnBaseTradModified.id))
                dicoObjOnBaseModified[objOnBaseTradModified.id] = objOnBaseTradModified;
            else
                dicoObjOnBaseModified.Add(objOnBaseTradModified.id, objOnBaseTradModified);
        }
        public void LoadObs()
        {
            ObsObjOnBaseTrad = obsObjOnBaseTradTMP;
        }

        /// <summary>
        /// Filter Data by source
        /// </summary>
        /// <param name="filter"></param>
        public void FilterDATA()
        {
            IsBtnLoadObsClickable = false;
            IsBtnFilterClickable = false;
            var query = listObjOnBaseEN.Where(ob => ob.source
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
        public void LoadDATA()
        {
            IsBtnFilterClickable = false;
            IsBtnLoadDataClickable = false;
            IsBtnLoadObsClickable = false;
            Filter = String.Empty;
            List<ObjOnBaseTrad> listObjOnBaseFR;
            List<ObjOnBaseTrad> listObjOnBaseDE;

            listObjOnBaseEN = ToolTradCSV.Read(pathFileEN);
            listObjOnBaseFR = ToolTradCSV.Read(pathFileFR);
            listObjOnBaseDE = ToolTradCSV.Read(pathFileDE);

            Debug.WriteLine("Count Verification:");
            if (listObjOnBaseEN.Count == listObjOnBaseFR.Count &&
                listObjOnBaseEN.Count == listObjOnBaseDE.Count)
                Debug.WriteLine("Verification OK\n");
            else
            {
                listObjOnBaseEN.Clear();
                IsBtnLoadDataClickable = true;
                Debug.WriteLine("Count Verification NOT OK");
                throw new Exception("Count Verification NOT OK");
            }

            Debug.WriteLine("Each Object Verification:");
            for (int i = 0; i < listObjOnBaseEN.Count; i++)
            {
                if (listObjOnBaseEN[i].Equals(listObjOnBaseFR[i]) &&
                    listObjOnBaseEN[i].Equals(listObjOnBaseDE[i]) &&
                    listObjOnBaseFR[i].Equals(listObjOnBaseDE[i]))
                {
                    //everything goes in tradEN when reading the stream
                    listObjOnBaseEN[i].tradFR = listObjOnBaseFR[i].tradEN;
                    listObjOnBaseEN[i].tradDE = listObjOnBaseDE[i].tradEN;
                }
                else
                {
                    listObjOnBaseEN.Clear();
                    IsBtnLoadDataClickable = true;
                    Debug.WriteLine("Each Object Verification NOT OK (id, description and source)");
                    throw new Exception("Each Object Verification NOT OK (id, description and source)");
                }
            }
            IsBtnFilterClickable = true;
            IsBtnLoadDataClickable = true;
        }
    }
}
