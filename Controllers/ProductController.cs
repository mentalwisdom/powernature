//Author Anan Osothsilp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nature.Data;
using nature.Models;
 
 

namespace nature.Controllers
{
    public class ProductController : Controller
    {
        private readonly natureDbContext _context;

        public ProductController(natureDbContext context)
        {
            _context = context;
        }//end function

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var result = await _context.Product
                              .Select(x=>new {
									productId = x.productId,
									name = x.name,
									qty = x.qty,
									price = x.price,
									productGroup = x.productGroup.productGroupName,
                                    image = x.image
                              })
                              .ToListAsync();
            ViewBag.products = result;                                              
            return View();
        }//end function
 
        // GET: Product/Create
        public IActionResult Add()
        {
        	//custom queries from FK data table
            ViewBag.productGroups = _context.ProductGroup.Select(x=> new {x.productGroupId,value=x.productGroupName});
            //ViewBag.select_productGroup = ViewBag.productGroups[0];

            return View();
        }//end function

        // POST: Product/Add
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            try{
                 var data = product.image.Split(',')[1];
                product.image = "";
                _context.Add(product);
                await _context.SaveChangesAsync();
                string fileName = product.productId.ToString() + ".png";
                string filePath = Path.Combine($"{Directory.GetCurrentDirectory()}/products/{fileName}");
                var bytess = Convert.FromBase64String(data);
                using (var imageFile = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }
                return Json( new {
                              error=-1,
                              message = "yes"
                });
               }//end try
               catch(Exception ex){
                    return Json( new {
                              error=1,
                              message = "yes",
                              exception = ex.Message
                    });
               }  //end Exception
             
        }//end function

        // GET: Product/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Json( new {
                              error=1,
                              message = "no",
                              exception= " please define id"
                    });
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                 return Json( new {
                              error=1,
                              message = "no",
                              exception= id.ToString() + " not found"
                    });
            }
            var productGroups = _context.ProductGroup.Select(x=> new {x.productGroupId,value=x.productGroupName});
            ViewBag.productGroups = productGroups;
            ViewBag.select_productGroup = await productGroups.FirstOrDefaultAsync(x=>x.productGroupId == product.productGroupId);
            
           

             ViewBag.product = product;
            return View();
        }//end function

        // POST: Product/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
              try{
                string data = product.image;  
                product.image = "";
                _context.Update(product);

                await _context.SaveChangesAsync();
                if(data.Contains("base64"))
                 {
                    data = data.Split(',')[1];
                    string fileName = product.productId.ToString() + ".png";
                    string filePath = Path.Combine($"{Directory.GetCurrentDirectory()}/products/{fileName}");
                    var bytess = Convert.FromBase64String(data);
                    using (var imageFile = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.Write(bytess, 0, bytess.Length);
                        imageFile.Flush();
                    }
                     
                 }
          
                return Json( new {
                              error=-1,
                              message = "yes"
                });
               }//end try
               catch(Exception ex){
                    return Json( new {
                              error=1,
                              message = "yes",
                              exception = ex.Message
                    });
               }  //end Exception
        }//end function

        // GET: Product/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.productId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }//end function

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }//end function

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.productId == id);
        }//end function
    }//end class
}//end namespace
