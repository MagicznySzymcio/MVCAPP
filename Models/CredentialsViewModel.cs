using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPP.Models
{
    public class CredentialsViewModel
    {
        [Required]
        public String Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required, Compare("Password"), Display(Name="Comfirm Password")]
        public String ComfirmPassword { get; set; }
    }
}
