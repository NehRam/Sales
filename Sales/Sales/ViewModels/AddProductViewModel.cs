namespace Sales.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Sales.Common.Models;
    using Services;
    using Xamarin.Forms;
    public class AddProductViewModel : BaseViewModel
    {
        #region Attributes
        private MediaFile file;
        private ImageSource imageSource;
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;

        #endregion

        #region Properties
        public string Description { get; set; }
        public string Price { get; set; }
        public string Remarks { get; set; }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        #endregion

        #region Constructors
        public AddProductViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.ImageSource = "noproduct";
        }
        #endregion

        #region Commands
        public ICommand SaveCommand {
            get {
                return new RelayCommand(Save);
            }
        }
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Lenguages.ImageSource,
                Lenguages.Cancel,
                null,
                Lenguages.FromGallery,
                Lenguages.NewPicture);

            if (source==Lenguages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == Lenguages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,

                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }
            if (this.file !=null)
            {
                this.imageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Lenguages.Error, 
                    Lenguages.DescriptionError, 
                    Lenguages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Lenguages.Error,
                    Lenguages.PriceError,
                    Lenguages.Accept);
                return;
            }

            var price = decimal.Parse(this.Price);

            if (price <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                   Lenguages.Error,
                   Lenguages.PriceError,
                   Lenguages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Lenguages.Error, 
                    connection.Message, 
                    Lenguages.Accept);
                return;
            }

            var product = new Product
            {
                Description = this.Description,
                Price = price,
                Remarks = this.Remarks
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProducsController"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, product);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Lenguages.Error,
                    response.Message,
                    Lenguages.Accept);
                return;
            }

            var newProduct = (Product)response.Result;
            var viewModel = ProductsViewModel.GetInstance();
            viewModel.Products.Add(newProduct);

            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
