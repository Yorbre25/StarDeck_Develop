using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Player
    {
        [Key]
        public string id {get;set;}
        public string email {get;set;}
        public string f_name {get;set;}
        public string l_name { get; set; }
        public string nickname {get; set;}
        public string p_hash {get;set;}
        public int lvl {get;set;}
        public string ranking { get; set; }
        public bool in_game { get; set; }
        public bool active { get; set; }
        [ForeignKey("Country")]
        public string country {get;set;}
        public int coins { get; set; }
        public string avatar { get; set; }



    }
}
