using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flower_Project.Areas.Admin.Models;

namespace Flower_Project.Utility
{
    public class CategoryUtility
    {
        private static MyDbContext db = new MyDbContext();
        private static List<Category> _listCategories;

        public static List<Category> GetCategories()
        {
            if (_listCategories == null)
            {
                _listCategories = db.Categories.ToList();
            }

            return _listCategories;
        }

        public static List<SelectListItem> GetCategoriesAsDropDown()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (_listCategories == null)
            {
                _listCategories = db.Categories.ToList();
            }

            foreach (var category in _listCategories)
            {
                list.Add(new SelectListItem { Text = category.CategoryName, Value = category.CategoryId });

            }

            return list;

        }
    }
}