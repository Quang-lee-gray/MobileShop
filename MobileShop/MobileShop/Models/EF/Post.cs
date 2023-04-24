using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobileShop.Models.EF
{
    [Table("tb_Post")]
    public class Post : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public string seoTitle { get; set; }
        public string seoDescription { get; set; }
        public string seoKeywords { get; set; }
        public bool IsActive { get; set; }
        public virtual Categorys Categorys { get; set; }
    }
}