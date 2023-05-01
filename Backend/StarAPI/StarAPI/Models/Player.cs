using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Player
    {
        [Key]
        public string id {get;set;}
        public string email {get;set;}
        public string firstName {get;set;}
        public string lastName { get; set; }
        public string username {get; set;}
        public string pHash {get;set;}
        public int level {get;set;}
        public int ranking { get; set; }
        public bool inGame { get; set; }
        public bool activatedAccount { get; set; }
        [ForeignKey("Country")]
        public int countryId {get;set;}
        public int coins { get; set; }
        public int avatarId { get; set; }



    }
}
