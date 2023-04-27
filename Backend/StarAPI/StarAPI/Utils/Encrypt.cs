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

        public string gen_id(string type)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            var rand_string = new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
            return type+"-"+rand_string;
        }
    }
}
