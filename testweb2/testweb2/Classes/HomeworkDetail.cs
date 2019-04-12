using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testweb2.Classes
{
    public class HomeworkDetail
    {
        public Homework Homework { get; set; }
        public IEnumerable<NoteCat> Category { get; set; }
        public HomeworkDetail(Homework a, IEnumerable<NoteCat> b)
        {
            Homework = a;
            Category = b;
        }
    }
}