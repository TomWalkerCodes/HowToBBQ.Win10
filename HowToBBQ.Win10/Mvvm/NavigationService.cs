using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HowToBBQ.Win10.Mvvm
{
    public class NavigationService : INavigationService
    {
        private Frame _frame;
        public event NavigatingCancelEventHandler Navigating;

        public NavigationService(Frame frame)
        {
            _frame = frame;
        }

        public void Navigate(Type type)
        {
            _frame.Navigate(type);
        }

        public void Navigate(Type type, object parameter)
        {
            _frame.Navigate(type, parameter);
        }

        public void Navigate(string type, object parameter)
        {
            _frame.Navigate(Type.GetType(type), parameter);
        }

        public void Navigate(string type)
        {
            _frame.Navigate(Type.GetType(type));
        }

        public void GoBack()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }
    }
}
