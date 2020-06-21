
using System;
using System.Collections.Generic;
using System.Text;


namespace Sales.Helpers
{
    using Xamarin.Forms;
    using Sales.Interfaces;
    using Resources;
    using System.Runtime.Versioning;

    public static class Lenguages
    {
        static Lenguages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }
        public static string Products
        {
            get { return Resource.Products; }
        }
        public static string TurnOnInternet
        {
            get { return Resource.TurnOnInternet; }
        }

        public static string AddProduct
        {
            get { return Resource.AddProduct; }
        }

        public static string Description
        {
            get { return Resource.Description; }
        }
        public static string Price
        {
            get { return Resource.Price; }
        }
        public static string DescriptionPlaceHolder
        {
            get { return Resource.DescriptionPlaceHolder; }
        }
        public static string PricePlaceHolder
        {
            get { return Resource.PricePlaceHolder; }
        }
        public static string Save
        {
            get { return Resource.Save; }
        }
        public static string Remarks
        {
            get { return Resource.Remarks; }
        }
        public static string ChangeImage
        {
            get { return Resource.ChangeImage; }
        }
        public static string DescriptionError
        {
            get { return Resource.DescriptionError; }
        }
        public static string PriceError
        {
            get { return Resource.PriceError; }
        }

        public static string Cancel
        {
            get { return Resource.Cancel; }
        }
        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }
        public static string NewPicture
        {
            get { return Resource.NewPicture; }
        }
        public static string ImageSource
        {
            get { return Resource.ImageSource; }
        }
        public static string Delete
        {
            get { return Resource.Delete; }
        }
        public static string Edit
        {
            get { return Resource.Edit; }
        }
        public static string DeleteConfirmation
        {
            get { return Resource.DeleteConfirmation; }
        }
        public static string Yes
        {
            get { return Resource.Yes; }
        }
        public static string No
        {
            get { return Resource.No; }
        }
        public static string Confirm
        {
            get { return Resource.Confirm; }
        }
        public static string EditProduct
        {
            get { return Resource.EditProduct; }
        }
        public static string IsAvailable
        {
            get { return Resource.IsAvailable; }
        }
        public static string Search
        {
            get { return Resource.Search; }
        }
        public static string Login
        {
            get { return Resource.Login; }
        }
        public static string EMail
        {
            get { return Resource.EMail; }
        }
        public static string EMailPlaceHolder
        {
            get { return Resource.EMailPlaceHolder; }
        }
        public static string Password
        {
            get { return Resource.Password; }
        }
        public static string PasswordPlaceHolder
        {
            get { return Resource.PasswordPlaceHolder; }
        }
        public static string RememberMe
        {
            get { return Resource.RememberMe; }
        }
        public static string Forgot
        {
            get { return Resource.Forgot; }
        }
        public static string Register
        {
            get { return Resource.Register; }
        }
        public static string EMailValidation
        {
            get { return Resource.EMailValidation; }
        }
        public static string PasswordValidation
        {
            get { return Resource.PasswordValidation; }
        }
        public static string SomethingWrong
        {
            get { return Resource.SomethingWrong; }
        }
        public static string Menu
        {
            get { return Resource.Menu; }
        }
        public static string About
        {
            get { return Resource.About; }
        }
        public static string Exit
        {
            get { return Resource.Exit; }
        }
        public static string Setup
        {
            get { return Resource.Setup; }
        }
        public static string NoProductsMessage
        {
            get { return Resource.NoProductsMessage; }
        }
        public static string FirstName
        {
            get { return Resource.FirstName; }
        }
        public static string LastName
        {
            get { return Resource.LastName; }
        }
        public static string Phone
        {
            get { return Resource.Phone; }
        }
        public static string Address
        {
            get { return Resource.Address; }
        }
        public static string FirstNamePlaceholder
        {
            get { return Resource.FirstNamePlaceholder; }
        }
        public static string LastNamePlaceholder
        {
            get { return Resource.LastNamePlaceholder; }
        }
        public static string PhonePlaceholder
        {
            get { return Resource.PhonePlaceholder; }
        }
        public static string AddressPlaceholder
        {
            get { return Resource.AddressPlaceholder; }
        }
        public static string PasswordConfirm
        {
            get { return Resource.PasswordConfirm; }
        }
        public static string PasswordConfirmPlaceholder
        {
            get { return Resource.PasswordConfirmPlaceholder; }
        }
        public static string FirstNameError
        {
            get { return Resource.FirstNameError; }
        }
        public static string LastNameError
        {
            get { return Resource.LastNameError; }
        }
        public static string PhoneError
        {
            get { return Resource.PhoneError; }
        }
        public static string EMailError
        {
            get { return Resource.EMailError; }
        }
        public static string PasswordError
        {
            get { return Resource.PasswordError; }
        }
        public static string PasswordConfirmError
        {
            get { return Resource.PasswordConfirmError; }
        }
        public static string PasswordsNoMatch
        {
            get { return Resource.PasswordsNoMatch; }
        }
        public static string RegisterConfirmation
        {
            get { return Resource.RegisterConfirmation; }
        }
    }
}
