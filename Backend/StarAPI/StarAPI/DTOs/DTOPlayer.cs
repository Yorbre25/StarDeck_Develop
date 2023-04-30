using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.DTOs
{
    public class OutputPlayer
    {
        public string id {get;set;}
        public string email {get;set;}
        public string firstName {get;set;}
        public string lastName { get; set; }
        public string username {get; set;}
        public string pHash {get;set;}
        public int level {get;set;}
        public string ranking { get; set; }
        public bool activatedAccount { get; set; }
        public string country {get;set;}
        public int coins { get; set; }
        public string avatar { get; set; }



    }
}
