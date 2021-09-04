using System;
using System.Threading.Tasks;
using nature;
using nature.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ex1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            //create scope to use services
            using(var scope = host.Services.CreateScope()){
                var services = scope.ServiceProvider;//get service instance
                var _db = services.GetRequiredService<natureDbContext>();
                try{

                    _db.Database.Migrate();
                    var _userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var _roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                    await SeedUserAccount.go(_userManager,_roleManager);
                }
                catch(Exception ex){
                     
                    Console.WriteLine("seeding error {0}",ex.Message);

                }
            }//end using

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
