using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace testweb2.Models
{
    public class User
    {
        public int UserNo { get; set; }
        public string UserName { get; set; }
        public string UserClass { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
    }

    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}