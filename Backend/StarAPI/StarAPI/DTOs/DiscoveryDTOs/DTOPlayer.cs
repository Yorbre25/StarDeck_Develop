using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.DTO.Discovery
{
    public class InputPlayer
    {
        public string email {get;set;}
        public string firstName {get;set;}
        public string lastName { get; set; }
        public string username {get; set;}
        public string password {get;set;}
        public int countryId {get;set;}
        public string avatar { get; set; }
    }

    public class OutputPlayer
    {
        public string id {get;set;}
        public string email {get;set;}
        public string firstName {get;set;}
        public string lastName { get; set; }
        public string username {get; set;}
        public string pHash {get;set;}
        public int xp {get;set;}
        public int ranking { get; set; }
        public string country {get;set;}
        public int coins { get; set; }
        public string avatar { get; set; }
    }
}
