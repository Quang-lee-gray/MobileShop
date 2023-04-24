using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobileShop.Models.EF
{
    [Table("tb_Categorys")]
    public class Categorys : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //[StringLength(150)]
        //public string Typecode { get; set; }
        //public string Link { get; set; }
        public string seoTitle { get; set; }
        public string seoDescription { get; set; }
        public string seoKeywords { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}