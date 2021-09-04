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
    public class CustomerController : Controller
    {
        private readonly natureDbContext _context;

        public CustomerController(natureDbContext context)
        {
            _context = context;
        }//end function

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var result = await _context.Customer
                              .Select(x=>new {
									customerId = x.customerId,
									firstName = x.firstName,
									lastName = x.lastName,
                              })
                              .ToListAsync();
            ViewBag.customers = result;                                              
            return View();
        }//end function
 
        // GET: Customer/Create
        public IActionResult Add()
        {
        	//custom queries from FK data table

            return View();
        }//end function

        // POST: Customer/Add
        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
               try{
                _context.Add(customer);
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

        // GET: Customer/Edit/1
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

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                 return Json( new {
                              error=1,
                              message = "no",
                              exception= id.ToString() + " not found"
                    });
            }

             ViewBag.customer = customer;
            return View();
        }//end function

        // POST: Customer/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
             try{
                _context.Customer.Update(customer);
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

        // GET: Customer/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.customerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }//end function

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }//end function

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.customerId == id);
        }//end function
    }//end class
}//end namespace
