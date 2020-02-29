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
        public string FullName { get; set; }
       
        public string Address { get; set; }
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