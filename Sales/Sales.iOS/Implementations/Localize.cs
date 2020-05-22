[assembly: Xamarin.Forms.Dependency(typeof(Sales.iOS.Implementations.Localize))]
namespace Sales.iOS.Implementations
{
    using Sales.Interfaces;
    using System.Globalization;
    using Foundation;
    using Helpers;
    using SceneKit;
    using System.Threading;

    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLenguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLenguage = iOSToDotnetLenguage(pref);
            }

            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLenguage);
            }
            catch (CultureNotFoundException e1)
            {
                try
                {
                    var fallback = ToDotnetFallbackLenguage(new PlatformCulture(netLenguage));
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

        string iOSToDotnetLenguage(string iOSLenguage)
        {
            var netLenguage = iOSLenguage;

            switch (iOSLenguage)
            {
                case "ms-My":
                case "ms-SG":
                    netLenguage = "ms";
                    break;
                case "gsw-CH":
                    netLenguage = "de-CH";
                    break;
            }

            return netLenguage;
        }

        string ToDotnetFallbackLenguage(PlatformCulture platCulture)
        {
            var netLenguage = platCulture.LenguageCode;
            switch (platCulture.LenguageCode)
            {
                case "pt":
                    netLenguage = "pt-PT";
                    break;
                case "gsw":
                    netLenguage = "de-CH";
                    break;
            }

            return netLenguage;
        }

    }
}