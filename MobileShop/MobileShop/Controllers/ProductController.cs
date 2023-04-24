using MobileShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace MobileShop.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index(int? id,int ? page,string fSearchPrice, string lSearchPrice)
        {
            var items = from c in db.Products.ToList() select c;
            if (id != null)
            {
                items = items.Where(m => m.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.id = id;
            if (fSearchPrice != null && lSearchPrice != null)
            {
                page = 1;
            }
            if (fSearchPrice != null && lSearchPrice != null)
            {
                items = items.Where(c => c.PriceSale >= Int32.Parse(fSearchPrice) && c.PriceSale <= Int32.Parse(lSearchPrice));
            }
            //Phân trang
            var pageSize = 12;
            var pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DetailsProduct(string seoTitle,int id)
        {
            var item = db.Products.Find(id);
            return View(item);
        }
        public ActionResult ProductCategory(string seoTitle,int id, int? page, string fSearchPrice, string lSearchPrice)
        {
            var items = from c in db.Products.ToList() select c;
            if (id > 0)
            {
                items = items.Where(m => m.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if(cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.id = id;
            if (fSearchPrice != null && lSearchPrice != null)
            {
                page = 1;
            }
            if (fSearchPrice != null && lSearchPrice != null)
            {
                items = items.Where(c => c.PriceSale >= Int32.Parse(fSearchPrice) && c.PriceSale <= Int32.Parse(lSearchPrice));
            }
            //Phân trang
            var pageSize = 12;
            var pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(m => m.IsHome == true).Take(10).ToList();
            return PartialView("", items);
        }
        public ActionResult Partial_ProductSale()
        {
            var items = db.Products.Where(m => m.IsSale == true).Take(10).ToList();
            return PartialView("", items);
        }
    }
}