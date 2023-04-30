namespace StarAPI.Logic.Utils
{
    public class KeyGenerator
    {
        public string gen_id(string type)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            var rand_string = new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
            return type + "-" + rand_string;
        }
    }
}
