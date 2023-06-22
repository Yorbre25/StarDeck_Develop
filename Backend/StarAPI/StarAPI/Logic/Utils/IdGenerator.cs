namespace StarAPI.Logic.Utils
{
    public class IdGenerator
    {
         Random random = new Random();
        private static int idLenght = 12;
        public string GenerateId(string idPrefix)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var rand_string = new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
            return idPrefix + "-" + rand_string;
        }
    }
}
