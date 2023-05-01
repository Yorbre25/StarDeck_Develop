using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class Country
    {
        [Key]
        public int id { get; set; }
        public string countryName { get; set; }

    }
}
