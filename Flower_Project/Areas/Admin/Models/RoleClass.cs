using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flower_Project.Areas.Admin.Models
{
    public class RoleClass
    {
        [Required(ErrorMessage = "Vui lòng nhập tên quyền.")]
        [Display(Name = "Tên quyền")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tên quyền chỉ được phép trong khoảng từ 2 - 50 kí tự.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mô tả cho quyền.")]
        [Display(Name = "Mô tả")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Mô tả quyền chỉ được phép trong khoảng từ 2 - 50 kí tự.")]
        public string Description { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }
        [Display(Name = "Ngày xóa")]
        public DateTime? DeletedAt { get; set; }
        [Display(Name = "Trạng thái")]
        public RoleStatus Status { get; set; }


        public enum RoleStatus
        {
            Active = 1,
            Deactivate = 0,
            Deleted = -1
        }
    }
}