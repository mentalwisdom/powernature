using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nature.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using nature.Models;
namespace nature{

    [Route("api/[action]")]
    public class AccountController:Controller{
        public IConfiguration _configuration;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private readonly natureDbContext _db;
        public AccountController(
            IConfiguration configuration,
            natureDbContext db,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager
            ){
            _db = db;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }//ef

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserData model){
            //validate nulltiy
            if(model != null  && model.userName !=null && model.password !=null ){
                return await authenticate(model);
                 
            }//end if
            else{
                //400 code
                return BadRequest();
            }
        }//ef
     

        private async Task<IActionResult> authenticate(UserData model){
              var user = await _userManager.FindByNameAsync(model.userName);  
              
            if (user != null && await _userManager.CheckPasswordAsync(user, model.password))  
            {  
                var userRoles = await _userManager.GetRolesAsync(user);  
   
                var authClaims = new List<Claim>  
                {  
                    new Claim(ClaimTypes.Name, user.UserName),  
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                };  
  
                foreach (var userRole in userRoles)  
                {  
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
                }  
  
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
                var token = new JwtSecurityToken(  
                    issuer: _configuration["JWT:Issuer"],  
                    audience: _configuration["JWT:Audience"],  
                    expires: DateTime.Now.AddHours(1),  
                    claims: authClaims,  
                    signingCredentials: 
                    new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
                    );  
  
                return Ok(new  
                {  
                    roles = userRoles,
                    token = new JwtSecurityTokenHandler().WriteToken(token),  
                    expiration = token.ValidTo  
                });  
            }
            
             return Unauthorized(); 
        }//ef

        [HttpPost]  
        public async Task<IActionResult> Register([FromBody] RegisterData model)  
        {  
            var userExists = await _userManager.FindByNameAsync(model.userName);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, 
                new  { Status = "Error", Message = "User already exists!" });  
  
            AppUser user = new AppUser()  
            {  
                Email = model.email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.userName  
            };  
            var result = await _userManager.CreateAsync(user, model.password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, 
                new { Status = "Error", Message = "User creation failed! Please check user details and try again." });  
  
            return Ok(new  { Status = "Success", Message = "User created successfully!" });  
        }//ef
        
    }//ec
}//en