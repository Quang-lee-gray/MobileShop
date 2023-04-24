using MobileShop.Models;
using MobileShop.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class ProductCategorysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductCategorys
        public ActionResult Index(string Search, int? page)
        {
            if (Search != null)
            {
                page = 1;
            }
            var model = from c in db.ProductCategories.ToList() select c;
            //Tìm kiếm
            if (!string.IsNullOrEmpty(Search))
            {
                model = model.Where(c => c.Title.ToLower().Contains(Search.ToLower()) || c.seoTitle.ToLower().Contains(Search.ToLower()));
            }
            //Phân trang
            var pageSize = 3;
            var pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }      
        // GET: Admin/ProductCategorys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductCategorys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory model)
        {
            try
            {
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                var items = db.ProductCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/ProductCategorys/Edit/5
        public ActionResult Edit(int id)
        {
            var model = db.ProductCategories.Find(id);
            return View(model);
        }

        // POST: Admin/ProductCategorys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            try
            {
                db.ProductCategories.Attach(model);
                var item = db.ProductCategories.SingleOrDefault(m => m.Id == model.Id);
                model.ModifiedDate = DateTime.Now;
                db.Entry(model).Property(m => m.Title).IsModified = true;
                db.Entry(model).Property(m => m.Description).IsModified = true;
                db.Entry(model).Property(m => m.Icon).IsModified = true;
                db.Entry(model).Property(m => m.seoTitle).IsModified = true;
                db.Entry(model).Property(m => m.seoDescription).IsModified = true;
                db.Entry(model).Property(m => m.seoKeywords).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var model = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
