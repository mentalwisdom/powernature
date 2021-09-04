//Author Anan Osothsilp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nature.Data;
using nature.Models;

namespace nature.Controllers
{
    public class ProductGroupController : Controller
    {
        private readonly natureDbContext _context;

        public ProductGroupController(natureDbContext context)
        {
            _context = context;
        }//end function

        // GET: ProductGroup
        public async Task<IActionResult> Index()
        {
            var result = await _context.ProductGroup
                              .Select(x=>new {
									productGroupId = x.productGroupId,
									productGroupName = x.productGroupName,
                              })
                              .ToListAsync();
            ViewBag.productGroups = result;                                              
            return View();
        }//end function
 
        // GET: ProductGroup/Create
        public IActionResult Add()
        {
        	//custom queries from FK data table

            return View();
        }//end function

        // POST: ProductGroup/Add
        [HttpPost]
        public async Task<IActionResult> Add(ProductGroup productGroup)
        {
               try{
                _context.Add(productGroup);
                await _context.SaveChangesAsync();
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

        // GET: ProductGroup/Edit/1
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

            var productGroup = await _context.ProductGroup.FindAsync(id);
            if (productGroup == null)
            {
                 return Json( new {
                              error=1,
                              message = "no",
                              exception= id.ToString() + " not found"
                    });
            }

             ViewBag.productGroup = productGroup;
            return View();
        }//end function

        // POST: ProductGroup/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(ProductGroup productGroup)
        {
             try{
                _context.ProductGroup.Update(productGroup);
                await _context.SaveChangesAsync();
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

        // GET: ProductGroup/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroup
                .FirstOrDefaultAsync(m => m.productGroupId == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }//end function

        // POST: ProductGroup/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productGroup = await _context.ProductGroup.FindAsync(id);
            _context.ProductGroup.Remove(productGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }//end function

        private bool ProductGroupExists(int id)
        {
            return _context.ProductGroup.Any(e => e.productGroupId == id);
        }//end function
    }//end class
}//end namespace
