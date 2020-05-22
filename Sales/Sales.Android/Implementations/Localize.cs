[assembly: Xamarin.Forms.Dependency(typeof(Sales.Droid.Implementations.Localize))]
namespace Sales.Droid.Implementations
{
    //using Java.Lang;
    using Sales.Helpers;
    using Sales.Interfaces;
    using System.Globalization;
    using System.Threading;
    public class Localize:ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLenguage = "en";
            var androidLocale = Java.Util.Locale.Default;
            netLenguage = AndroidToDotnetLenguage(androidLocale.ToString().Replace("_", "-"));
            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLenguage);
            }
            catch (CultureNotFoundException e1)
            {

                try
                {
                    var fallback = ToDotnetFallbackLenguage (new PlatformCulture(netLenguage));
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    ci = new System.Globalization.CultureInfo("en");
                }
            }
            return ci;
        }
        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

        }
        string AndroidToDotnetLenguage(string androidLenguage)
        {
            var netLenguage = androidLenguage;
            switch (androidLenguage)
            {
                case "ms-BN":
                case "ms-MY":
                case "ms-BG":
                    netLenguage = "ms";
                    break;
                case "in-ID":
                    netLenguage = "id-ID";
                    break;
                case "gsw-CH":
                    netLenguage = "de-CH";
                    break;
                //case "es-US":
                  //  netLenguage = "es";
                    //break;
            }
            return netLenguage;
        }
        string ToDotnetFallbackLenguage (PlatformCulture platCulture)
        {
            var netLenguage = platCulture.LenguageCode;
            switch (platCulture.LenguageCode)
            {
                case "gsw":
                    netLenguage = "de-CH";
                    break;
            }
            return netLenguage;
        }
    }
}