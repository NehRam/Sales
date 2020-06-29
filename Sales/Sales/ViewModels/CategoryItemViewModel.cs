namespace Sales.ViewModels
{
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Sales.Views;
    using System.Windows.Input;

    public class CategoryItemViewModel : Category
    {
        #region Commands
        public ICommand GotoCategoryCommand 
        { 
            get 
            {
                return new RelayCommand(GotoCategory);
            } 
        }

        private async void GotoCategory()
        {
            MainViewModel.GetInstance().Products = new ProductsViewModel(this);
            await App.Navigator.PushAsync(new ProductsPage());
        }
        #endregion
    }
}
