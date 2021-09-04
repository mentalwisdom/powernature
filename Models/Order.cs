using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nature.Models{
    public class Order{
        [Key]
        public int orderId {get;set;}
        public int customerId {get;set;}
        public Customer customer {get;set;}
         public List<OrderItem> orderItems {get;set;}

        public DateTime orderDate {get;set;}
        
        


    }//ec
}//en