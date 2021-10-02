using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPP.Data
{
    public class CookieUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
