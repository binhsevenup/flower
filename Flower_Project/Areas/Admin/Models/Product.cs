using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flower_Project.Areas.Admin.Models
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double PriceSale { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Bonus { get; set; }
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public ProductStatus Status { get; set; }

        public enum ProductStatus
        {
            Active = 1,
            Deactivate = 0,
            Deleted = -1
        }
    }
}