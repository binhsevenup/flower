using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flower_Project.Areas.Admin.Models
{
    public class UserClass
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên đầy đủ.")]
        [Display(Name = "Tên đầy đủ")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Tên bạn chỉ được phép trong khoảng từ 1 - 50 kí tự.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [Display(Name = "Địa chỉ")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Địa chỉ của bạn chỉ được phép trong khoảng từ 5 - 300 kí tự.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tài khoản.")]
        [Display(Name = "Tài khoản")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tài khoản chỉ được phép trong khoảng từ 5 - 50 kí tự.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Mật khẩu chỉ được phép trong khoảng từ 5 - 50 kí tự.")]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email.")]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }
        [Display(Name = "số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Ngày cập nhật")]

        public DateTime? UpdatedAt { get; set; }
        [Display(Name = "Ngày xóa")]

        public DateTime? DeletedAt { get; set; }
        [Display(Name = "Trạng thái")]
        public MemberStatus Status { get; set; }

        public enum MemberStatus
        {
            Active = 1,
            Deactivate = 0,
            Deleted = -1
        }
    }
}