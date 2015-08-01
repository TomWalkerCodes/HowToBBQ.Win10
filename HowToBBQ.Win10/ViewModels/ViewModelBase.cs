using Mvvm;
using System.Collections.ObjectModel;

namespace HowToBBQ.Win10.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        private static ObservableCollection<MenuItem> menu = new ObservableCollection<MenuItem>();

        public ViewModelBase()
        {}

        public ObservableCollection<MenuItem> Menu {
            get { return menu; }
        }
    }
}
