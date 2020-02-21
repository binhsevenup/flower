using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Flower_Project.Areas.Admin.Models
{
    public class Role : IdentityRole
    {
        [Display(Name = "Mô tả")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Mô tả quyền chỉ được phép trong khoảng từ 1 - 50 kí tự.")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
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