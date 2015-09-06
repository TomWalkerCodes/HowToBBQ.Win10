using HowToBBQ.Win10.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HowToBBQ.Win10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BBQRecipePage : Page
    {

        private BBQRecipeViewModel bbqRecipeViewModel;

        public BBQRecipePage()
        {
            this.InitializeComponent();
            bbqRecipeViewModel = new BBQRecipeViewModel();
            DataContext = App.MainViewModel;
            ButtonPanel.DataContext = bbqRecipeViewModel;
        }

        private async void ButtonFilePick_Click(object sender, RoutedEventArgs e)
        {
            await GetImage(false);
        }


        private async void ButtonCamera_Click(object sender, RoutedEventArgs e)
        {
            await GetImage(true);
        }

        async Task GetImage(bool useCamera)
        {
            try
            {
                BBQImage.Source = await bbqRecipeViewModel.SelectImage(useCamera);
                BBQImage.Visibility = (BBQImage.Source == null) ? Visibility.Visible : Visibility.Visible;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

    }
}
