/*====================================================================*\
Name ........ : MainWindow.cs
Role ........ : Provide all fonction to manage cvs traduction
Author ...... : Davide Faga
Date ........ : 28.03.2023
\*====================================================================*/
using OnBaseTradManager.Models;
using OnBaseTradManager.Tools;
using OnBaseTradManager.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;
using OnBaseTradManager.Views;

namespace OnBaseTradManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /*------------------------------------------------------------------*\
        |*							VARIABLE                				*|
        \*------------------------------------------------------------------*/
        private MainWindowsViewModel mwvm = new MainWindowsViewModel();
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();


        /*------------------------------------------------------------------*\
        |*							CONSTRUCTOR     						*|
        \*------------------------------------------------------------------*/
        public MainWindow()
        {
            initDialogs();
            DataContext = mwvm;
            InitializeComponent();
        }

        /*------------------------------------------------------------------*\
        |*							ON EVENT         						*|
        \*------------------------------------------------------------------*/
        private void OnClickLoadData(object sender, EventArgs e)
        {
            mwvm.ObsObjOnBaseTrad.Clear();
            Task.Run(() =>
            {
                try
                {
                    mwvm.ReadDATA();
                    mwvm.FilterDATA();
                }
                catch (Exception e) { MessageBox.Show(e.Message, "Error Load DATA", MessageBoxButton.OK, MessageBoxImage.Error); }
            });
        }
        private void OnClickFilter(object sender, EventArgs e)
        {
            Task.Run(() => mwvm.FilterDATA());
        }

        private void OnClickLoadObs(object sender, EventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke(() => mwvm.LoadObs());
        }
        private void OnTextChangedFilter(object sender, EventArgs e)
        {
            mwvm.IsBtnLoadObsClickable = false;
        }
        private void OnClickSaveCSV(object sender, EventArgs e)
        {
            Task.Run(() => mwvm.WriteDATA());
        }
        private void OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var currentObjOnBaseTrad = (ObjOnBaseTrad)e.Row.Item;
                var columnHeader = e.Column.Header.ToString();
                var cellValue = ((TextBox)e.EditingElement).Text;
                Task.Run(() => mwvm.AddObjOnBaseTradModifiedToDico(currentObjOnBaseTrad, columnHeader, cellValue));
                ((TextBox)e.EditingElement).Text = cellValue.AddDoubleQuoteOnlyStartEnd();
            }
        }
        private void OnClickBtnLangEnglish(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                mwvm.PathFileEN = openFileDialog.FileName;
                Properties.Settings.Default["LastDirectory"] = Path.GetDirectoryName(openFileDialog.FileName);
                Properties.Settings.Default.Save();
            }
        }
        private void OnClickBtnLangFrench(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                mwvm.PathFileFR = openFileDialog.FileName;
                Properties.Settings.Default["LastDirectory"] = Path.GetDirectoryName(openFileDialog.FileName);
                Properties.Settings.Default.Save();
            }
        }
        private void OnClickBtnLangGerman(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                mwvm.PathFileDE = openFileDialog.FileName;
                Properties.Settings.Default["LastDirectory"] = Path.GetDirectoryName(openFileDialog.FileName);
                Properties.Settings.Default.Save();
            }
        }
        private void OnBtnClickPathSaveDir(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mwvm.PathSave = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default["SaveDirectory"] = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
        } 
        private void OnBtnClickHelp(object sender, EventArgs e)
        {
            HelpView helpView = new();
            helpView.ResizeMode = ResizeMode.NoResize;
            helpView.ShowDialog();
        }

        /*------------------------------------------------------------------*\
        |*							FUNCTION         						*|
        \*------------------------------------------------------------------*/
        private void initDialogs()
        {
            openFileDialog.Filter = "CSV documents |*.csv";
            openFileDialog.Title = "Load Traduction File OnBase";
            openFileDialog.InitialDirectory = Properties.Settings.Default["LastDirectory"].ToString();
            folderBrowserDialog.Description = "Select folder where save traductions files";
            folderBrowserDialog.UseDescriptionForTitle = true;
            folderBrowserDialog.SelectedPath = Properties.Settings.Default["SaveDirectory"].ToString();
            mwvm.PathSave = Properties.Settings.Default["SaveDirectory"].ToString()!;
        }
    }
}
