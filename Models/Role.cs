﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPP.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
