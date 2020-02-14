using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flower_Project.Areas.Admin.Models
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public CategoryStatus Status { get; set; }

        public enum CategoryStatus
        {
            Active = 1,
            Deactivate = 0,
            Deleted = -1
        }
    }
}