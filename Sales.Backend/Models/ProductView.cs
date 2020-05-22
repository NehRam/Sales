namespace Sales.Backend.Models
{
    using System.Web;
    using Sales.Common.Models;
    
    public class ProductView : Product
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}