using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class Country
    {
        [Key]
        public string id { get; set; }
        public string c_name { get; set; }

    }
}
