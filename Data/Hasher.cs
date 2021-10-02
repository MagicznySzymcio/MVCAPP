using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVCAPP.Data
{
    public class Hasher
    {
        public static string GenerateSalt(int size = 16)
        {
            byte[] salt;
            using RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt = new byte[size]);
            return Convert.ToBase64String(salt);
        }

        public static string GenerateHash(string pass, string salt)
        {
            using var alg = new HMACSHA256(GetBytes(salt));
            var result = alg.ComputeHash(GetBytes(pass));
            return Convert.ToBase64String(result);
        }

        private static byte[] GetBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

    }
}
