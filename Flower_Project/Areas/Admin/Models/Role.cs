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


        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public RoleStatus Status { get; set; }

        public enum RoleStatus
        {
            Active = 1,
            Deactivate = 0,
            Deleted = -1
        }
    }
}