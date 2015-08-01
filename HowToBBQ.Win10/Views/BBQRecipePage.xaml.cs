using HowToBBQ.Win10.Models;
using HowToBBQ.Win10.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HowToBBQ.Win10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BBQRecipePage : Page
    {
        public MediaCapture captureManager;
        public bool isPreviewing = false;
        public bool isAutoFocus = false;


        private WriteableBitmap bitmap;

        public BBQRecipePage()
        {
            this.InitializeComponent();
            DataContext = new BBQRecipeViewModel();
        }

        private async void Loadphoto(string filename)
        {
            //load saved image
            StorageFolder pictureLibrary = KnownFolders.SavedPictures;
            //filename = "ms-appdata:///local/Pictures/Test.jpeg";
            StorageFile savedPicture = await pictureLibrary.GetFileAsync(filename);
            ImageProperties imgProp = await savedPicture.Properties.GetImagePropertiesAsync();
            var savedPictureStream = await savedPicture.OpenAsync(FileAccessMode.Read);

            //set image properties and show the taken photo
            bitmap = new WriteableBitmap((int)imgProp.Width, (int)imgProp.Height);
            await bitmap.SetSourceAsync(savedPictureStream);
            BBQImage.Source = bitmap;
            BBQImage.Visibility = Visibility.Visible;
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
                // Application now has read/write access to the picked file
                Loadphoto(file.Path);
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

                if (savedFile != null)
                {
                    await photo.MoveAndReplaceAsync(savedFile);
                }
            }
        }
    }
}
