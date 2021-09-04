using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nature.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using nature.Models;

using System;

namespace nature {
    [Authorize(AuthenticationSchemes = 
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[action]")]
    public class ApiController: Controller{
        private natureDbContext _db;

        public ApiController(natureDbContext db){
                _db = db;
            }
            [HttpGet]
            public IActionResult Data(){
                return Json(new string[]{"a","b","c"});
            }

            [HttpGet]
            public async Task<IActionResult> CryptoByName(string name){
                var result = await _db.Crypto
                .Where(x=>x.Name ==name)
                .Select(x=> new {
                     open = x.Open,
                     low = x.Low,
                     high = x.High,
                     close = x.Close,
                     volumeto= x.Volume
                }).ToListAsync();
                return Ok(result);
            }//ef

            [HttpPost]
            public async Task<IActionResult> PressOrder([FromBody] Order order){
                if(order ==null){
                    return BadRequest(new{error="no object"});
                }
                 //order.orderId =1;
                order.orderDate = DateTime.Now;

                 _db.Order.Add(order);
                 await _db.SaveChangesAsync();
                return Ok(order);
            }//ef

            [HttpGet]
            public  IActionResult Products (){
                return Ok(
                _db.Product
                .Select(x=> new {
                    x.productId,
                    x.name,
                    x.price,
                    qty = 0,
                    image = x.image,
                    
                })
                .OrderByDescending(x=>x.productId)
                .ToList());
            }
    }//ef
}//en

// {
//   "orderId": 0,
//   "customerId": 1,
//   "orderItems": [
//     {
//       "productId": 1,
//       "qty": 10,
//       "discount": 10,
//     },
// {
//       "productId": 2,
//       "qty": 20,
//       "discount": 20,
//     }
//   ],
 
// }