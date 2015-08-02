using HowToBBQ.Win10.ViewModels;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HowToBBQ.Win10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BBQRecipePage : Page
    {

        private WriteableBitmap bitmap;

        public BBQRecipePage()
        {
            this.InitializeComponent();
            DataContext = new BBQRecipeViewModel();
        }

        private async void ButtonFilePick_Click(object sender, RoutedEventArgs e)
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
                BBQImage.Source = bitmap;
                BBQImage.Visibility = Visibility.Visible;

                (this.DataContext as BBQRecipeViewModel).imageSource = file.Path;
            }
        }



        private async void ButtonCamera_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(600, 600);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo != null)
            {
                BitmapImage bmp = new BitmapImage();
                IRandomAccessStream stream = await photo.
                                                   OpenAsync(FileAccessMode.Read);
                bmp.SetSource(stream);
                BBQImage.Source = bmp;

                FileSavePicker savePicker = new FileSavePicker();
                savePicker.FileTypeChoices.Add
                                      ("jpeg image", new List<string>() { ".jpeg" });

                savePicker.SuggestedFileName = "New picture";

                StorageFile savedFile = await savePicker.PickSaveFileAsync();

                (this.DataContext as BBQRecipeViewModel).imageSource = savedFile.Path;

                if (savedFile != null)
                {
                    await photo.MoveAndReplaceAsync(savedFile);
                }
            }
        }
    }
}
