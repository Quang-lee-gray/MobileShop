using MobileShop.Models;
using MobileShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        //Hiển thị sản phẩm trong giỏ hàng
        public ActionResult Partial_Thanhtoan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }
        //Giỏ hàng
        public ActionResult Partial_ItemCart()
        {
            //Session là một đối tượng được sử dụng để lưu trữ các thông tin có liên quan đến hành vi và hoạt động của người dùng trên trang web
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return View(cart.items);
            }
            return View();
        }
        //Đếm số lượng sản phẩm trong giỏ
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }
        //Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(m => m.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategorys.Title,
                    seoTitle = checkProduct.seoTitle,
                    Quantity = quantity
                };
                if (checkProduct.ProductImages.FirstOrDefault(m => m.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImages.FirstOrDefault(m => m.IsDefault).Image;
                }
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                else
                {
                    item.Price = (decimal)checkProduct.Price;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công!", code = 1, Count = cart.items.Count };
            }
            return Json(code);
        }
        //Cập nhật giỏ hàng
        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        //Xóa sản phẩm trong giỏ hàng 
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var check = cart.items.FirstOrDefault(m => m.ProductId == id);
                if (check != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.items.Count };
                }
            }
            return Json(code);
        }
        //Thanh toán
        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                ViewBag.Checkcart = cart;
            }
            return View();
        }
        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel order)
        {
            var code = new { Success = false, Code = -1 };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    Order od = new Order();
                    od.CustomerName = order.CustomerName;
                    od.Phone = order.Phone;
                    od.Address = order.Address;
                    cart.items.ForEach(m => od.OrderDetails.Add(new OrderDetails
                    {
                        ProductID = m.ProductId,
                        Quantity = m.Quantity,
                        Price = m.Price
                    }));
                    od.TotalAmount = cart.items.Sum(m => (m.Price * m.Quantity));
                    od.TypePayment = order.TypePayment;
                    od.CreateDate = DateTime.Now;
                    od.ModifiedDate = DateTime.Now;
                    od.CreateBy = order.Phone;
                    Random rd = new Random();
                    od.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Orders.Add(od);
                    db.SaveChanges();
                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess");
                }
            }
            return Json( order);
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
    }
}
