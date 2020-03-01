using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flower_Project.Areas.Admin.Models
{
    public class Category : IEnumerable
    {
        [Key]
        [Display(Name = "Mã danh mục")]
        [Required(ErrorMessage = "Vui lòng nhập mã danh mục cho sản phẩm. ")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Mã danh mục chỉ được phép trong khoảng từ 2 - 10 kí tự.")]
        public string CategoryId { get; set; }
        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục sản phẩm. ")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Tên danh mục chỉ được phép trong khoảng từ 2 - 150 kí tự.")]
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [Display(Name = "Trạng thái")]
        public CategoryStatus Status { get; set; }

        public enum CategoryStatus
        {
            Active = 1,
            Deactivate = 0,
            Deleted = -1
        }

        public bool IsDeleted()
        {
            return this.Status == CategoryStatus.Deleted;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}