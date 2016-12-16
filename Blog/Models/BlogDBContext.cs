﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.Models
{
   
        public class BlogDbContext : IdentityDbContext<ApplicationUser>
        {
            public BlogDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
            }

        public virtual IDbSet<Article> Articles { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }
        public static BlogDbContext Create()
            {
                return new BlogDbContext();
            }
        }
    }
