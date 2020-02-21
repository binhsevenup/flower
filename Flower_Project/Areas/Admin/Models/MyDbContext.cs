using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Flower_Project.Areas.Admin.Models
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext() : base("name=MyDbContext")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public static MyDbContext Create()
        {
            return new MyDbContext();
        }

        public System.Data.Entity.DbSet<Flower_Project.Areas.Admin.Models.Role> IdentityRoles { get; set; }
    }
}