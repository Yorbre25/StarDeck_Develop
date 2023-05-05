
namespace StarAPI.DTOs
{
    public class InputCard
    {
        public string name { get; set; }
        public int energy { get; set; }
        public int cost { get; set; }
        public int  typeId { get; set; }
        public int raceId { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }


    public class OutputCard
    {
        public string id { get; set; }
        public string name { get; set; }
        public int energy { get; set; }
        public int cost { get; set; }
        public string  type { get; set; }
        public string race { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }
}
