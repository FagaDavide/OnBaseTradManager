/*====================================================================*\
Name ........ : MainWindow.cs
Role ........ : Provide all fonction to manage cvs traduction
Author ...... : Davide Faga
Date ........ : 28.03.2023
\*====================================================================*/
using OnBaseTradManager.Models;
using OnBaseTradManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

        /*------------------------------------------------------------------*\
        |*							CONSTRUCTOR     						*|
        \*------------------------------------------------------------------*/
        public MainWindow()
        {
            DataContext = mwvm;
            InitializeComponent();
        }

        /*------------------------------------------------------------------*\
        |*							ON EVENT         						*|
        \*------------------------------------------------------------------*/
        private void onClickLoadData(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                try { mwvm.LoadDATA(); }
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
        private void OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
           
            if (e.EditAction == DataGridEditAction.Commit)
            {
               
                MessageBox.Show(e.Row.Item.ToString());
                MessageBox.Show(e.Column.Header.ToString());
                MessageBox.Show(e.EditingElement.ToString());
            }

            

        }

    }
}
