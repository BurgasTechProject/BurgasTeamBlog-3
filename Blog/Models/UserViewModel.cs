﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class UserViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }

        public string Gender { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public List<Role> Roles { get; set; }
    }
}