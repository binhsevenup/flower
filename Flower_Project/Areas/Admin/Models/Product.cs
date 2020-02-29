using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flower_Project.Areas.Admin.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập mã cho sản phẩm. ")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Mã sản phẩm chỉ được phép trong khoảng từ 2 - 10 kí tự.")]
        public string ProductId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm.")]
        public string ProductName { get; set; }
        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng cho sản phẩm. ")]
        [Range(1, maximum: 9999, ErrorMessage = "Số lượng sản phẩm chỉ được phép trong khoảng từ 1 - 9999 kí tự.")]
        public int Quantity { get; set; }
        [Display(Name = "Giá sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập giá cho sản phẩm. ")]
        [Range(1, maximum: 9999999, ErrorMessage = "Giá trị sản phẩm chỉ được phép trong khoảng từ 1 - 9999999vnđ.")]
        public double Price { get; set; }
//        [Display(Name = "Giá sản phẩm (khi giảm giá)")]
//        [Range(0, maximum: 9999999, ErrorMessage = "Giá trị sản phẩm chỉ được phép trong khoảng từ 0 - 9999999vnđ.")]
//        public double? PriceSale { get; set; }
        [Display(Name = "Ảnh")]
        [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
        public string Avatar { get; set; }
        [Display(Name = "Mô tả sản phẩm")]
        [AllowHtml]
        [Required(ErrorMessage = "Vui lòng nhập mô tả cho sản phẩm. ")]
        [StringLength(300, MinimumLength = 2, ErrorMessage = "Mô tả cho sản phẩm chỉ được phép trong khoảng từ 2 - 300 kí tự.")]
        public string Description { get; set; }
        [Display(Name = "Chi tiết sản phẩm")]
        [AllowHtml]
        [Required(ErrorMessage = "Vui lòng nhập mô tả chi tiết cho sản phẩm. ")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Mô tả chi tiết cho sản phẩm chỉ được phép trong khoảng từ 2 - 500 kí tự.")]
        public string Detail { get; set; }
//        [Display(Name = "Quà tặng kèm")]
//        [StringLength(300, MinimumLength = 2, ErrorMessage = "Quà tặng kèm cho sản phẩm chỉ được phép trong khoảng từ 2 - 300 kí tự.")]
//        public string Bonus { get; set; }
//        [Display(Name = "Loại sản phẩm")]
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [Display(Name = "Trạng thái")]
        public ProductStatus Status { get; set; }

        public enum ProductStatus
        {
            Active = 1,
            Deactivate = 0,
            Deleted = -1
        }

        public bool IsDeleted()
        {
            return this.Status == ProductStatus.Deleted;
        }
    }
}