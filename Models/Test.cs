using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPP.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string costam { get; set; }
    }
}
