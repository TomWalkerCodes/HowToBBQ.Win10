using GalaSoft.MvvmLight.Ioc;
using HowToBBQ.Win10.Common;
using HowToBBQ.Win10.Models;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace HowToBBQ.Win10.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<BBQRecipe> Recipes { get; set; }
        public INavigationService navigationService;
        public IBBQRecipeRepository BBQRepo;
        private BBQRecipe selectedBBQRecipe;

        public BBQRecipe SelectedBBQRecipe
        {
            get { return selectedBBQRecipe; }
            set { selectedBBQRecipe = value; }
        }

        public bool IsDataLoaded { get; set; }


        public MainViewModel()
        {
            BBQRepo = new BBQRecipeRepository();
            selectedBBQRecipe = new BBQRecipe();

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
