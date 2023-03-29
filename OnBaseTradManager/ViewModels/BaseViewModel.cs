/*====================================================================*\
Name ........ : BaseViewModel.cs
Role ........ : Base of all ViewModels manage the property change
Author ...... : Davide Faga
Date ........ : 28.03.2023
\*====================================================================*/
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OnBaseTradManager.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
