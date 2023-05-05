using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.DTOs
{
    public class InputPlanet
    {
        public string name { get; set; }
        public int  typeId { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }

        public class OutputPlanet
    {
        public string id { get; set; }
        public string name { get; set; }
        public string  type { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }
}