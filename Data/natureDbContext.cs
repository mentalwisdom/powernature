using nature.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using nature;

namespace nature.Data
{
     public class natureDbContext : IdentityDbContext<AppUser,AppRole,int>{
         public natureDbContext(DbContextOptions<natureDbContext> options):base(options){
            
         }//ef
         protected override void OnModelCreating(ModelBuilder builder)
        {
 
            base.OnModelCreating(builder);
        }
        public DbSet<AppUser> AppUsers {get;set;}
        public DbSet<Course> Course {get;set;}
        public DbSet<Crypto> Crypto {get;set;}
        public DbSet<Product>  Product {get;set;}
        public DbSet<Order> Order {get;set;}
        public DbSet<OrderItem>  OrderItem {get;set;}
        public DbSet<Customer> Customer {get;set;}
        public DbSet<ProductGroup> ProductGroup {get;set;}
    
     }//ec
}//en