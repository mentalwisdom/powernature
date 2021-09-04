using System.ComponentModel.DataAnnotations;

namespace nature.Models{
    public class Customer {
        [Key]
        public int customerId {get;set;} //pk
        public string firstName {get;set;}
        public string lastName {get;set;}

    }//ec
}//en