using HowToBBQ.Win10.Models;
using Mvvm;
using System;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;

namespace HowToBBQ.Win10.ViewModels
{
    public class BBQRecipeViewModel: ViewModelBase
    {
        public BBQRecipe CurrentBBQRecipe { get; set; }

        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand DeleteCommand { get; private set; }

        public DelegateCommand ShareCommand { get; private set; }

        private int id;
        private string name;
        private string shortDesc;
        private string ingredients;
        public string directions;
        public int prepTime;
        public int totalTime;
        public int serves;
        public string imageSource;
        private StorageFile task;

        public BBQRecipeViewModel()
        {

            if (App.MainViewModel.SelectedBBQRecipe != null)
            {
                CurrentBBQRecipe = App.MainViewModel.SelectedBBQRecipe;
            }
            else
            {
                CurrentBBQRecipe = new BBQRecipe();
                CurrentBBQRecipe.Id = 0;
            }

            id = CurrentBBQRecipe.Id;
            name = CurrentBBQRecipe.Name;
            shortDesc = CurrentBBQRecipe.ShortDesc;
            ingredients = CurrentBBQRecipe.Ingredients;
            directions = CurrentBBQRecipe.Directions;
            prepTime = CurrentBBQRecipe.PrepTime;
            totalTime = CurrentBBQRecipe.TotalTime;
            serves = CurrentBBQRecipe.Serves;
            imageSource = CurrentBBQRecipe.ImageSource;

            SaveCommand = new DelegateCommand(SaveBBQRecipe);
            DeleteCommand = new DelegateCommand(DeleteBBQRecipe);
            ShareCommand = new DelegateCommand(ShareBBQRecipe);

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;


        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                SetProperty(ref id, value);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }

        public string ShortDesc
        {
            get
            {
                return shortDesc;
            }
            set
            {
                SetProperty(ref shortDesc, value);
            }
        }

        public string Ingredients
        {
            get
            {
                return ingredients;
            }
            set
            {
                SetProperty(ref ingredients, value);
            }
        }

        public string Directions
        {
            get
            {
                return directions;
            }
            set
            {
                SetProperty(ref directions, value);
            }
        }

        public int PrepTime
        {
            get
            {
                return prepTime;
            }
            set
            {
                SetProperty(ref prepTime, value);
            }
        }

        public int TotalTime
        {
            get
            {
                return totalTime;
            }
            set
            {
                SetProperty(ref totalTime, value);
            }
        }

        public int Serves
        {
            get
            {
                return serves;
            }
            set
            {
                SetProperty(ref serves, value);
            }
        }

        public string ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                SetProperty(ref imageSource, value);
            }
        }


        public void SaveBBQRecipe()
        {
            CurrentBBQRecipe.Id = id;
            CurrentBBQRecipe.Name = name;
            CurrentBBQRecipe.ShortDesc = shortDesc;
            CurrentBBQRecipe.Ingredients= ingredients;
            CurrentBBQRecipe.Directions = directions;
            CurrentBBQRecipe.PrepTime = prepTime;
            CurrentBBQRecipe.TotalTime = totalTime;
            CurrentBBQRecipe.Serves = serves;
            CurrentBBQRecipe.ImageSource = imageSource;


            if (id == 0)
            {
                App.MainViewModel.BBQRepo.Add(CurrentBBQRecipe);
                App.MainViewModel.IsDataLoaded = false;
            }
            else
            {
                App.MainViewModel.BBQRepo.Update(CurrentBBQRecipe);
            }
        }

        public void DeleteBBQRecipe()
        {

            if (id > 0)
            {
                App.MainViewModel.BBQRepo.Remove(this.id);

                Id = 0;
                Name = "";
                ShortDesc = "";
                Ingredients ="";
                Directions = "";
                PrepTime = 0;
                TotalTime = 0;
                Serves = 0;
                ImageSource = "";

                App.MainViewModel.IsDataLoaded = false;

            }
           
        }

        public void ShareBBQRecipe()
        {

            if (id > 0)
            {
                DataTransferManager.ShowShareUI();

            }

        }


        private async void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequestDeferral deferral = args.Request.GetDeferral();
            args.Request.Data.Properties.Title = Name;
            args.Request.Data.SetText(shortDesc);
            var uri = new Uri(imageSource, UriKind.Absolute);
            StorageFile storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);

            args.Request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(storageFile));
            deferral.Complete();
        }


    }
}
