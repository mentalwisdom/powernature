using System.ComponentModel.DataAnnotations;
using System;
namespace nature.Models{

    public class Crypto {

        [Key]
        public int No {get;set;}
        public string Name {get;set;}
        public double High {get;set;}
        public double Low {get;set;}
        public double Open {get;set;}
        public double Close {get;set;}
        public double Volume {get;set;}
        public double AdjClose {get;set;}
        public DateTime   Date {get;set;}


        
    }//ec
}//en