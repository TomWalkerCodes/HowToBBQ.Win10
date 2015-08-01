using HowToBBQ.Win10.Views;

namespace HowToBBQ.Win10.ViewModels
{
    class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menu
            Menu.Clear();
            Menu.Add(new MenuItem() { Glyph = "", Text = "BBQ Recipes", NavigationDestination = typeof(MainPage) });
            Menu.Add(new MenuItem() { Glyph = "", Text = "Add Recipe", NavigationDestination = typeof(BBQRecipePage) });
            
        }
    }
}
