using System.ComponentModel.DataAnnotations;

namespace nature.Models{
    public class Product {
        [Key]
        public int productId {get;set;} //pk
        public string name {get;set;} //label="name"
        public int qty {get;set;} //label="qty"
        public double price {get;set;} //label="price"
		public int productGroupId {get;set;} //fk=label="product group" show="productGroupName"
		public ProductGroup productGroup {get;set;} //np select="productGroupName"
		public string image {get;set;}//file label="company logo" hide dir="products" 

    }//ef
}//en