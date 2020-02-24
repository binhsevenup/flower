using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Flower_Project.Areas.Admin.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên đầy đủ.")]
        [Display(Name = "Tên đầy đủ")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Tên bạn chỉ được phép trong khoảng từ 1 - 50 kí tự.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [Display(Name = "Địa chỉ")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Địa chỉ của bạn chỉ được phép trong khoảng từ 5 - 300 kí tự.")]
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
//        [Display(Name = "Trạng thái")]
        public MemberStatus Status { get; set; }

        public enum MemberStatus
        {
            Active = 1,
            Deactivate = 0,
            Deleted = -1
        }
    }
}