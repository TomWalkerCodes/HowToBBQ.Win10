using GalaSoft.MvvmLight.Ioc;
using HowToBBQ.Win10.Models;
using HowToBBQ.Win10.Mvvm;
using Mvvm;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace HowToBBQ.Win10.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<BBQRecipe> Recipes { get; set; }
        private INavigationService navigationService;
        public IBBQRecipeRepository BBQRepo = new BBQRecipeRepository();
        private BBQRecipe selectedBBQRecipe;

        public BBQRecipe SelectedBBQRecipe
        {
            get { return selectedBBQRecipe; }
            set { selectedBBQRecipe = value; }
        }

        public bool IsDataLoaded { get; set; }


        public MainViewModel()
        {
            if (!IsDataLoaded)
            {
                Recipes = BBQRepo.GetAll().ToObservableCollection();
                IsDataLoaded = true;
            }

            navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
        }

        public void BBQRecipeTapped(object sender, object parameter)
        {
            var arg = parameter as ItemClickEventArgs;
            var item = arg.ClickedItem as BBQRecipe;
            selectedBBQRecipe = item;
            navigationService.Navigate(typeof(Shell));
        }

        public void ReloadBBQRecipes()
        {
            Recipes = BBQRepo.GetAll().ToObservableCollection();
            IsDataLoaded = true;
        }
    }
}
