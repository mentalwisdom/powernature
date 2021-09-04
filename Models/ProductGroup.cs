using System.ComponentModel.DataAnnotations;

namespace  nature.Models
{
    public class ProductGroup {
        [Key]
        public int productGroupId           {get;set;} //pk label="no"
        public string productGroupName     {get;set;} //label="productgroup"
   

    }//ef
}//en