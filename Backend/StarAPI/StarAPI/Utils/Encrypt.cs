using System;
using System.Security.Cryptography;
using System.Text;

namespace StarAPI.Utils
{
    public class Encrypt
    {
        public string Sha256(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha256Hash.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
