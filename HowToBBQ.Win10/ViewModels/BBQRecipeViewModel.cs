using HowToBBQ.Win10.Common;
using HowToBBQ.Win10.Models;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace HowToBBQ.Win10.ViewModels
{
    public class BBQRecipeViewModel : ViewModelBase
    {

        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand DeleteCommand { get; private set; }

        public DelegateCommand ShareCommand { get; private set; }


        public BBQRecipeViewModel()
        {

            SaveCommand = new DelegateCommand(SaveBBQRecipe);
            DeleteCommand = new DelegateCommand(DeleteBBQRecipe);
            ShareCommand = new DelegateCommand(ShareBBQRecipe);

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

        }

        #region Page Functions
        private WriteableBitmap bitmap;

        async Task<WriteableBitmap> SelectImageFromPicker()
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {

                ImageProperties imgProp = await file.Properties.GetImagePropertiesAsync();
                var savedPictureStream = await file.OpenAsync(FileAccessMode.Read);

                //set image properties and show the taken photo
                bitmap = new WriteableBitmap((int)imgProp.Width, (int)imgProp.Height);
                await bitmap.SetSourceAsync(savedPictureStream);
                bitmap.Invalidate();

                SaveImageAsync(file);

                return bitmap;
            }
            else return null;
        }

        private async void SaveImageAsync(StorageFile file)
        {

            if (file != null)
            {
                StorageFile newImageFile = await file.CopyAsync(ApplicationData.Current.LocalFolder, Guid.NewGuid().ToString());

                App.MainViewModel.SelectedBBQRecipe.ImagePath = newImageFile.Path;
            }
        }

        async Task<WriteableBitmap> TakePicture()
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(600, 600);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo != null)
            {

                WriteableBitmap bitmap = new WriteableBitmap(600, 600);
                IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
                bitmap.SetSource(stream);

                SaveImageAsync(photo);

                return bitmap;
            }

            return null;
        }
        public async Task<WriteableBitmap> SelectImage(bool useCamera)
        {
            if (useCamera) return await TakePicture();
            else return await SelectImageFromPicker();
        }
        #endregion Page Functions


        public void SaveBBQRecipe()
        {

            if (App.MainViewModel.SelectedBBQRecipe.IsChanged)
            {
                App.MainViewModel.BBQRepo.Update(App.MainViewModel.SelectedBBQRecipe);
                ShowMessage("Record has been saved");
            }
        }

        public void DeleteBBQRecipe()
        {

            if (!String.IsNullOrEmpty(App.MainViewModel.SelectedBBQRecipe.Id))
            {
                App.MainViewModel.BBQRepo.Remove(App.MainViewModel.SelectedBBQRecipe.Id);
                ShowMessage("Record has been deleted", true);

            }

        }

        public void ShareBBQRecipe()
        {

            if (!String.IsNullOrEmpty(App.MainViewModel.SelectedBBQRecipe.Id))
            {
                DataTransferManager.ShowShareUI();

            }

        }

        private async Task ShowMessage(string message, bool goBack=false)
        {
            var messageDialog = new MessageDialog(message);

            await messageDialog.ShowAsync();
            if (goBack) App.MainViewModel.navigationService.Navigate(typeof(Shell));
        }

        private async void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequestDeferral deferral = args.Request.GetDeferral();
            args.Request.Data.Properties.Title = App.MainViewModel.SelectedBBQRecipe.Name;
            args.Request.Data.SetText(App.MainViewModel.SelectedBBQRecipe.ShortDesc);

            StorageFile storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(App.MainViewModel.SelectedBBQRecipe.ImageUri);

            args.Request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(storageFile));
            deferral.Complete();
        }
    }
}
