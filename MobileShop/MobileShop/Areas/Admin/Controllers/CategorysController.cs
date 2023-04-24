using MobileShop.Models;
using MobileShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
namespace MobileShop.Areas.Admin.Controllers
{
    public class CategorysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Categorys
        public ActionResult Index()
        {
            var models = db.Categories;
            return View(models);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(Categorys model)
        {
            if (ModelState.IsValid)
            {
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Categorys model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(model);
                var item = db.Categories.SingleOrDefault(m => m.Id == model.Id);
                model.ModifiedDate = DateTime.Now;
                db.Entry(model).Property(m => m.Title).IsModified = true;
                db.Entry(model).Property(m => m.Description).IsModified = true;
                db.Entry(model).Property(m => m.seoTitle).IsModified = true;
                db.Entry(model).Property(m => m.IsActive).IsModified = true;
                db.Entry(model).Property(m => m.seoDescription).IsModified = true;
                db.Entry(model).Property(m => m.seoKeywords).IsModified = true;
                db.Entry(model).Property(m => m.Position).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var model = db.Categories.SingleOrDefault(m => m.Id == id);
            db.Categories.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if(items!=null && items.Any())
                {
                    foreach(var item in items)
                    {
                        var obj = db.Categories.Find(Convert.ToInt64(item));
                        db.Categories.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}