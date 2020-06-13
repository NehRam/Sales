namespace Sales.ViewModels
{
    using System.Linq;
    using System;
    using System.Windows.Input;
    using Services;
    using Xamarin.Forms;
    using Sales.Helpers;
    using Sales.Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Sales.Views;

    public class ProductItemViewModel : Product
    {

        #region Attributes
        private ApiService apiService;
        #endregion

        #region Constructors
        public ProductItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand DeleteProductCommand 
        {
            get 
            {
                return new RelayCommand(DeleteProduct);
            } 
        }
        public ICommand EditProductCommand
        {
            get
            {
                return new RelayCommand(EditProduct);
            }
        }

        public async void EditProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);

            await Application.Current.MainPage.Navigation.PushAsync(new EditProductPage());
        }

        private async void DeleteProduct()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(
                Lenguages.Confirm,
                Lenguages.DeleteConfirmation,
                Lenguages.Yes,
                Lenguages.No
                );
            if (!answer)
            {
                return;
            }

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Lenguages.Error, connection.Message, Lenguages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProducsController"].ToString();

            var response = await this.apiService.Delete(url, prefix, controller, this.ProducId);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Lenguages.Error, response.Message, Lenguages.Accept);
                return;
            }

            var productsViewModel = ProductsViewModel.GetInstance();
            var deletedProduct = productsViewModel.MyProducts.Where(p => p.ProducId == this.ProducId).FirstOrDefault();
            if (deletedProduct != null)
            {
                productsViewModel.MyProducts.Remove(deletedProduct);
            }
            productsViewModel.RefreshList();
        }
        #endregion
    }
}
 