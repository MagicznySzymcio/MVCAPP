using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPP.Data
{
    public class CookieUser
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
