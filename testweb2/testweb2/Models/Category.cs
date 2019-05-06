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
    public class UserCategory
    {
        [Key]
        public int CatUId { get; set; }
        public string CatUName { get; set; }
        public string CatUSelect { get; set; }
    }

    public class CategoryDBcontext : DbContext
    {
        public DbSet<Category> Category { get; set; }
    }
    public class UserCategoryDBcontext : DbContext
    { 
        public DbSet<UserCategory> UserCategory { get; set; }
    }
}