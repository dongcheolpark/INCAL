using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace MvcMovie.Models
{
    public class Homework
    {
        [Key]
        public int NoteNo { get; set; }
        public string Subject { get; set; }
        public string T_Name { get; set; }
        [AllowHtml]
        public string Contents { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
    public class NoteCat
    {
        [Key]
        public int NoteCatId { get; set; }
        public int NoteNo { get; set; }
        public string CatAttribute { get; set; }
    }

    public class HomeworkDBContext : DbContext
    {
        public DbSet<Homework> Homework { get; set; }
    }

    public class NoteCatDBContext : DbContext
    {
        public DbSet<NoteCat> NoteCat { get; set; }
    }
}