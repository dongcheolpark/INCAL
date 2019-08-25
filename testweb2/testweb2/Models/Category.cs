using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace testweb2.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string CatAttribute { get; set; }
    }

    public class SelectedCategory
    {
        [Key]
        public int CatUId { get; set; }
        public int CatUName { get; set; }
        public int CatUSelect { get; set; }
    }

    public class CategoryDBcontext : DbContext
    {
        public DbSet<Category> Category { get; set; }
    }

    public class UserCategoriesDBcontext : DbContext
    {
        public DbSet<SelectedCategory> Categories { get; set; }
    }

}