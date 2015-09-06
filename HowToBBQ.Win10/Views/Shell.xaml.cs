using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using HowToBBQ.Win10.ViewModels;
using HowToBBQ.Win10.Models;
using Windows.UI.ViewManagement;
using Windows.UI;

namespace HowToBBQ.Win10
{
    public sealed partial class Shell : Page
    {
        public Shell()
        {
            this.InitializeComponent();

            // Navigate to the first page (optionally)
            var type = (DataContext as ShellViewModel).Menu[1].NavigationDestination;

            // Navigate to the BBQRecipePage if there is selected BBQRecipe
            if (!String.IsNullOrEmpty(App.MainViewModel.SelectedBBQRecipe.Id))
            {
                type = (DataContext as ShellViewModel).Menu[0].NavigationDestination;
            }
            Loaded += Shell_Loaded;
            SplitViewFrame.Navigate(type);
        }

        private void Shell_Loaded(object sender, RoutedEventArgs e)
        {
//            App.SetTitleTheme(App.Current);
            //ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            //titleBar.BackgroundColor = Colors.Blue;///(Color)Resources["MenuTitleColorBlue"];
            //titleBar.ForegroundColor = Colors.YellowGreen;
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var menuItem = e.AddedItems.First() as MenuItem;
                if (menuItem.IsNavigation)
                {
                    App.MainViewModel.SelectedBBQRecipe = new BBQRecipe();
                    SplitViewFrame.Navigate(menuItem.NavigationDestination);
                }
                else
                {
                    menuItem.Command.Execute(null);
                }
            }
        }

        private void SplitViewOpener_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X > 50)
            {
                MySplitView.IsPaneOpen = true;
            }
        }

        private void SplitViewPane_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X < -50)
            {
                MySplitView.IsPaneOpen = false;
  
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

            if(MySplitView.IsPaneOpen)
            {
                Logo.Visibility = Visibility.Visible;
            }
            else
            {
                Logo.Visibility = Visibility.Collapsed;
            }
        }

        private void MySplitView_PaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args)
        {

        }
    }
}
