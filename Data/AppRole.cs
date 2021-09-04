using Microsoft.AspNetCore.Identity;

namespace nature.Data{

    public class AppRole:IdentityRole<int>{
 
        public AppRole(string Name):base(Name){}
        
    }
}