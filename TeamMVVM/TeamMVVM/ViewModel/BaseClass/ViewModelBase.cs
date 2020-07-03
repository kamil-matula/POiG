// Copyrights: Adam Zielonka
using System.ComponentModel;

namespace TeamMVVM.ViewModel.BaseClass
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(params string[] namesOfProperties) //zmiana właściwości
        {
            if (PropertyChanged != null)
            {
                foreach (var prop in namesOfProperties) 
                { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
            }
        }
    }
}