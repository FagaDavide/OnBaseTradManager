using OnBaseTradManager.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;


namespace OnBaseTradManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowsViewModel mwvm = new MainWindowsViewModel();
        public MainWindow()
        {
            DataContext = mwvm;
            InitializeComponent();
        }

        private void OnClickLoadData(object sender, EventArgs args)
        {
            Task.Run(() => mwvm.LoadDATA());
        }
        private void OnClickFilter(object sender, EventArgs args)
        {
            Task.Run(() => mwvm.filterDATA());
        }

        private void OnClickLoadObs(object sender, EventArgs args)
        {
            App.Current.Dispatcher.BeginInvoke(() => mwvm.loadObs());
        }
    }
}
