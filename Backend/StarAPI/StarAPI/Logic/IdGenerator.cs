namespace StarAPI.Logic
{
    public class IdGenerator
    {
        private static int idLenght = 12;
        public string GenerateId(string idPrefix)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, idLenght).Select(s => s[random.Next(s.Length)]).ToArray());
            return idPrefix+"-"+randomString;
        }
    }
}