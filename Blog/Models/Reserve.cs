using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Reserve
    {
        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string Text { get; set; }
        public Article Articles { get; set; }
        

    }
}