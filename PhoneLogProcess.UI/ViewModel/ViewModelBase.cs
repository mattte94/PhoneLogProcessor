using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneLogProcess.UI.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            InitializeCommands();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected abstract void InitializeCommands();
    }
}
