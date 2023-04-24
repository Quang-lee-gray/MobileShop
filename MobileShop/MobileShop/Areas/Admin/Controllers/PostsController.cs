using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Services.Description;
using MobileShop.Models;
using MobileShop.Models.EF;
using PagedList;
namespace MobileShop.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Posts
        public ActionResult Index(string Search, int? page)
        {
            if (Search != null)
            {
                page = 1;
            }
            var model = from c in db.Posts.ToList() select c;
            //Tìm kiếm
            if (!string.IsNullOrEmpty(Search))
            {
                model = model.Where(c => c.Title.ToLower().Contains(Search.ToLower()) || c.seoTitle.ToLower().Contains(Search.ToLower()));
            }
            //Phân trang
            var pageSize = 3;
            var pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber,pageSize));
        }
        //public ActionResult Details(int? id)
        //{
        //    var model = db.Posts.Find(id);
        //    return View(model);
        //}
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Post posts)
        {
            if (ModelState.IsValid)
            {
                posts.CreateDate = DateTime.Now;
                posts.ModifiedDate = DateTime.Now;
                db.Posts.Add(posts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(posts);
        }
        public ActionResult Edit(int? id)
        {
            var model = db.Posts.Find(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(model);
                var item = db.Posts.SingleOrDefault(m => m.Id == model.Id);
                model.ModifiedDate = DateTime.Now;
                db.Entry(model).Property(m => m.Image).IsModified = true;
                db.Entry(model).Property(m => m.Title).IsModified = true;
                db.Entry(model).Property(m => m.Description).IsModified = true;
                db.Entry(model).Property(m => m.seoTitle).IsModified = true;
                db.Entry(model).Property(m => m.IsActive).IsModified = true;
                db.Entry(model).Property(m => m.seoDescription).IsModified = true;
                db.Entry(model).Property(m => m.seoKeywords).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Posts.Find(Convert.ToInt64(item));
                        db.Posts.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
