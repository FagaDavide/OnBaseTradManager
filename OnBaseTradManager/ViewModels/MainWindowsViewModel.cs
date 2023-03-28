using OnBaseTradManager.Models;
using OnBaseTradManager.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace OnBaseTradManager.ViewModels
{
    public class MainWindowsViewModel : BaseViewModel
    {
        //CONST
        const string pathFileEN = @"C:\test\DEV_EN_OUT_20230327.csv";
        const string pathFileFR = @"C:\test\DEV_FR_OUT_20230327.csv";
        const string pathFileDE = @"C:\test\DEV_DE_OUT_20230327.csv";


        //VAR
        private List<ObjOnBaseTrad> listObjOnBaseEN;
       
        private ObservableCollection<ObjOnBaseTrad> obsObjOnBaseTrad;
        public ObservableCollection<ObjOnBaseTrad> ObsObjOnBaseTrad
        {
            get { return obsObjOnBaseTrad; }
            set 
            {
                obsObjOnBaseTrad = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ObjOnBaseTrad> obsObjOnBaseTradTMP;

        private bool isChecked = false;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        //CONSTRUCTOR
        public MainWindowsViewModel()
        {
            ObsObjOnBaseTrad = new ObservableCollection<ObjOnBaseTrad>();
            isChecked = false;
        }


       


        //FONCTION
        public void loadObs()
        {
            ObsObjOnBaseTrad = obsObjOnBaseTradTMP;
            IsChecked = true;
        }
        public void filterDATA()
        {
            var query = listObjOnBaseEN;//.Where(ob => ob.source
                                       //.Contains("pref-ga", StringComparison.OrdinalIgnoreCase))
                                       //.ToList();

            obsObjOnBaseTradTMP = new ObservableCollection<ObjOnBaseTrad>(query);
            IsChecked = false;
        }

        public void LoadDATA()
        {
            Debug.WriteLine("YOYOYOYOYOYOYOYOYOYOOY");
            var readerEN = new StreamReader(File.OpenRead(pathFileEN));
            var readerFR = new StreamReader(File.OpenRead(pathFileFR));
            var readerDE = new StreamReader(File.OpenRead(pathFileDE));
            //List<ObjOnBaseTrad> listObjOnBaseEN = new List<ObjOnBaseTrad>();
            List<ObjOnBaseTrad> listObjOnBaseFR = new List<ObjOnBaseTrad>();
            List<ObjOnBaseTrad> listObjOnBaseDE = new List<ObjOnBaseTrad>();

            Debug.WriteLine("Lecture fichiers");
            Debug.WriteLine(pathFileEN);
            listObjOnBaseEN = ReadTradCSV.Read(readerEN);
            Debug.WriteLine(pathFileFR);
            listObjOnBaseFR = ReadTradCSV.Read(readerFR);
            Debug.WriteLine(pathFileDE);
            listObjOnBaseDE = ReadTradCSV.Read(readerDE);
            Debug.WriteLine("Fin lecture\n");

            Debug.WriteLine("Verification Count:");
            Debug.WriteLine(listObjOnBaseEN.Count);
            Debug.WriteLine(listObjOnBaseFR.Count);
            Debug.WriteLine(listObjOnBaseDE.Count);
            if (listObjOnBaseEN.Count == listObjOnBaseFR.Count &&
                listObjOnBaseEN.Count == listObjOnBaseDE.Count)
                Debug.WriteLine("Verification OK\n");
            else
            {
                Debug.WriteLine("Verification NOT OK \n Fermeture du programme");
                throw new Exception("Verification NOT OK");
            }

            Debug.WriteLine("Verification de chaque object:");
            bool isListsOK = true;
            for (int i = 0; i < listObjOnBaseEN.Count; i++)
            {
                if (listObjOnBaseEN[i].Equals(listObjOnBaseFR[i]) &&
                    listObjOnBaseEN[i].Equals(listObjOnBaseDE[i]) &&
                    listObjOnBaseFR[i].Equals(listObjOnBaseDE[i]))
                {
                    listObjOnBaseEN[i].tradFR = listObjOnBaseFR[i].tradEN; //a refaire au propre car pour l'instant tout va dans tradEN à la lecture du stream
                    listObjOnBaseEN[i].tradDE = listObjOnBaseDE[i].tradEN;
                }
                else
                {
                    isListsOK = false;
                    break;
                }
            }
            if (isListsOK)
                Debug.WriteLine("L'id, la description et la source sont identiques pour EN-FR-DE");
            else
            {
                Debug.WriteLine("Il y a des objects qui ne sont pas identiques quand on compare\n" +
                    " l'id, la description et la source ");
                throw new Exception("Il y a des objects qui ne sont pas identiques quand on compare\n" +
                    " l'id, la description et la source ");
            }
     
            IsChecked = true;
        }
    }
}
