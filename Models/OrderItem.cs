using System.ComponentModel.DataAnnotations;

namespace nature.Models{
    public class OrderItem {
        [Key]
        public int orderItemId {get;set;}
        public int productId {get;set;}
        public Product product {get;set;}

        public int qty {get;set;}
        public double discount {get;set;}

        public int orderId {get;set;}
        public Order order {get;set;}
    }//ec
}//en