using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobileShop.Models.EF
{
    [Table("tb_Product")]
    public class Product : CommonAbstract // Sản phẩm
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
            this.OrderDetails = new HashSet<OrderDetails>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }//Giá cả
        public decimal PriceSale { get; set; } //Giá bán
        public int Quantity { get; set; }
        public string ProductCode { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; } //Tính năng
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        public int ProductCategoryId { get; set; }//Khóa ngoại của bảng loại sản phẩm(ProductCategory)
        public string seoTitle { get; set; }
        public string seoDescription { get; set; }
        public string seoKeywords { get; set; }
        public int Position { get; set; }
        public virtual ProductCategory ProductCategorys { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}