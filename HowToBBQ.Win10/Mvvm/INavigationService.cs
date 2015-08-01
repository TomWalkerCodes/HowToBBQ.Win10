using System;
using Windows.UI.Xaml.Navigation;

namespace HowToBBQ.Win10.Mvvm
{
    public interface INavigationService
    {
        event NavigatingCancelEventHandler Navigating;
        void Navigate(Type type);
        void Navigate(Type type, object parameter);
        void Navigate(string type);
        void Navigate(string type, object parameter);
        void GoBack();
    }
}
