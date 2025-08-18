using System;
using System.Linq;
using System.Web.Mvc;
using Erp_V1.DAL.DAL;

namespace Erp.WebApp.Controllers
{
    [CustomAuthorize(Roles = "Admin,Seller")]
    public class ProductsController : Controller
    {
        private readonly erp_v2Entities _dbContext = new erp_v2Entities();

        // GET: /Products
        public ActionResult Index()
        {
            var products = _dbContext.PRODUCT
                .Where(p => !p.isDeleted)
                .OrderBy(p => p.ProductName)
                .ToList();

            return View(products);
        }

        // GET: /Products/ProductAnalytics
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult ProductAnalytics()
        {
            return View();
        }
    }
}