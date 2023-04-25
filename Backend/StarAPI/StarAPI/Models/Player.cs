using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Player
    {
        [Key]
        public string id {get;set;}
        public string f_name {get;set;}
        public string p_hash {get;set;}
        public int lvl {get;set;}
        [ForeignKey("Country")]
        public string country {get;set;}


    }
}
