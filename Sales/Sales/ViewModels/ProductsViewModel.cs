namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Helpers;
    using Services;
    using Xamarin.Forms;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private DataService dataService;
        private bool isRefreshing;
        private ObservableCollection<ProductItemViewModel> products;
        private string filter;
        #endregion

        #region Properties

        public Category Category { get; set; }
        public List<Product> MyProducts { get; set; }
        public ObservableCollection<ProductItemViewModel> Products {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set 
            { 
                this.filter = value;
                this.RefreshList();
            }
        }
        #endregion

        #region Construtors

        public ProductsViewModel(Category category)
        {
            instance = this;
            this.Category = category;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadProducts();
        }
        #endregion

        #region Singleton
        private static ProductsViewModel instance;
        

        public static ProductsViewModel GetInstance()
        {
            return instance;
        }
    
        #endregion

        #region Methods
        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(Lenguages.Error, connection.Message, Lenguages.Accept);
                return;
            }

            var answer = await this.LoadProductsFromAPI();

            if (answer)
            {
                this.RefreshList();
            }

            this.IsRefreshing = false;
        }

        private async Task LoadProductsFromDB()
        {
            this.MyProducts = await this.dataService.GetAllProducts();
        }

        private async Task SaveProductsToDB()
        {
            await this.dataService.DeleteAllProducts();
            dataService.Insert(this.MyProducts);
        }

        private async Task<bool> LoadProductsFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProducsController"].ToString();

            var response = await this.apiService.GetList<Product>(url, prefix, controller, this.Category.CategoryId, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                return false;
            }

            this.MyProducts = (List<Product>)response.Result;

            return true;
        }

        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var ProductItemViewModel = this.MyProducts.Select(p => new ProductItemViewModel
                {
                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                    IsAvailable = p.IsAvailable,
                    Price = p.Price,
                    ProducId = p.ProducId,
                    PublishOn = p.PublishOn,
                    Remarks = p.Remarks,
                    CategoryId = p.CategoryId,
                    UserId = p.UserId
                });

                this.Products = new ObservableCollection<ProductItemViewModel>(
                    ProductItemViewModel.OrderBy(p => p.Description));
            }
            else
            {
                var ProductItemViewModel = this.MyProducts.Select(p => new ProductItemViewModel
                {
                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                    IsAvailable = p.IsAvailable,
                    Price = p.Price,
                    ProducId = p.ProducId,
                    PublishOn = p.PublishOn,
                    Remarks = p.Remarks,
                    CategoryId = p.CategoryId,
                    UserId = p.UserId
                }).Where(p => p.Description.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Products = new ObservableCollection<ProductItemViewModel>(
                    ProductItemViewModel.OrderBy(p => p.Description));
            }
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        } 
        #endregion
    }
}
