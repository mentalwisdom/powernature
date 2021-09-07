using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using nature.Data;
using nature.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
namespace nature
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("p1", builder =>
       {
           builder

                   .AllowAnyOrigin()
                    .AllowAnyMethod()
                   .AllowAnyHeader()
                //        .WithOrigins("http://localhost",
                  //      "https://localhost:1975",
                    //    "http://localhost:1974",
                      //  "https://localhost"
                        //) //Or my flutter web host
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed((host) => true)

                 
                  ;
       }));
            // services.Configure<CookiePolicyOptions>(options =>
            // {
            //     // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //     options.CheckConsentNeeded = context => true;
            //     options.MinimumSameSitePolicy = SameSiteMode.None;
            // });
            services.AddIdentity<AppUser, AppRole>()
             .AddEntityFrameworkStores<natureDbContext>()
             .AddDefaultTokenProviders()
             .AddDefaultUI();

            //use this setup for purely jwt authen no web authen
            // Adding Authentication  
            // services.AddAuthentication(options =>  
            // {  
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
            //     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
            // }) ;

            //use this setup when using web authen along with jwt 
            services.AddAuthentication()
                .AddCookie(cfg => cfg.SlidingExpiration = true) //web authen here
                                                                // Adding Jwt Bearer  here
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:Audience"],
                        ValidIssuer = Configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            });

            //setup db
            services.AddDbContextPool<natureDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        Configuration.GetConnectionString("DefaultConnection"),
                        new MySqlServerVersion(new Version(8, 0, 21)),
                        mySqlOptions => mySqlOptions
                        .CharSetBehavior(CharSetBehavior.NeverAppend)
                    )
                );

            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "project api version1", Version = "v1" });
            });
            //setup mvc and cshtml runtime compliation
            services.AddControllersWithViews()
            .AddRazorRuntimeCompilation()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            //add support for razor page
            services.AddRazorPages();
        }//ef

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, natureDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            context.Database.Migrate();

            //app.UseHttpsRedirection();

            app.UseCors("p1");
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "project api version1"));



            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"products")),
                RequestPath = new PathString("/products")
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), @"coupons")),
                RequestPath = new PathString("/coupons")
            });

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
           {
               endpoints.MapRazorPages();//razor page mapping
               endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
           });

        }//ef





    }//ec
}//en
