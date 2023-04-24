using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShop.Models
{
    public abstract class CommonAbstract //lớp trừu tượng chung
    {
        public string CreateBy { get; set; } //người tạo
        public DateTime CreateDate { get; set; } 
        public string ModifiedBy { get; set; } //người sửa
        public DateTime ModifiedDate { get; set; }
    }
}