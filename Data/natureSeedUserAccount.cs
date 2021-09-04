using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;
namespace nature.Data{

    public class SeedUserAccount {
        public static async Task go(UserManager<AppUser> _userManager, 
                               RoleManager<AppRole> _roleManager )
        {
            
            //create role here
            if(!await _roleManager.RoleExistsAsync("admin")){
                await _roleManager.CreateAsync(new AppRole("admin"));
            }
            if(!await _roleManager.RoleExistsAsync("manager")){
                await _roleManager.CreateAsync(new AppRole("manager"));
            }
            if(!await _roleManager.RoleExistsAsync("user")){
                await _roleManager.CreateAsync(new AppRole("user"));
            }
            var superUser = new AppUser{
                UserName = "root@localhost.com",
                Email    = "root@localhost.com",
                first_name = "super",
                last_name = "man"
            };
            var user1= new AppUser{
                UserName = "user1@localhost.com",
                Email    = "user1@localhost.com",
                first_name = "user1",
                last_name = "user1"
            };

            //query exisiting user
            if(_userManager.Users.All(u=> u.UserName != superUser.UserName)){
                await _userManager.CreateAsync(superUser,"1234");
                Console.WriteLine("root account has been created.");
            }

            if(_userManager.Users.All(u=> u.UserName != user1.UserName)){
                await _userManager.CreateAsync(user1,"1234");
                Console.WriteLine("user1 account has been created.");
            }
            superUser   = await _userManager.FindByEmailAsync("root@localhost.com");
            user1       = await _userManager.FindByEmailAsync("user1@localhost.com");
           
       
            //insert role for super user  
            if(!await _userManager.IsInRoleAsync(superUser,"admin")){
                await _userManager.AddToRoleAsync(superUser,"admin");
                Console.WriteLine("apply admin role to root");
            }
            else{
                Console.WriteLine("admin user  exist");
            }
            //inser role for user1
            if(!await _userManager.IsInRoleAsync(user1,"user")){
                await _userManager.AddToRoleAsync(user1,"user");
               Console.WriteLine("apply user role to user1");
            }
            else{
                 Console.WriteLine("user1 exist");
            }
            



            

        }//ef
         
    }//ec
}//en