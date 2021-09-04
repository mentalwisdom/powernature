//Author Anan Osothsilp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nature.Data;
using nature.Models;

namespace nature.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly natureDbContext _context;

        public CourseController(natureDbContext context)
        {
            _context = context;
        }//end function

        // GET: Course
        public async Task<IActionResult> Index()
        {
            var result = await _context.Course
                              .Select(x=>new {
									courseId = x.courseId,
									courseName = x.courseName,
									fee = x.fee,
									discount = x.discount,
									createdDate = x.createdDate,
                              })
                              .ToListAsync();
            ViewBag.courses = result;                                              
            return View();
        }//end function
 
        // GET: Course/Create
        public IActionResult Add()
        {
        	//custom queries from FK data table

            return View();
        }//end function

        // POST: Course/Add
        [HttpPost]
        public async Task<IActionResult> Add(Course course)
        {
               try{
                _context.Add(course);
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

        // GET: Course/Edit/1
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

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                 return Json( new {
                              error=1,
                              message = "no",
                              exception= id.ToString() + " not found"
                    });
            }

             ViewBag.course = course;
            return View();
        }//end function

        // POST: Course/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
             try{
                _context.Course.Update(course);
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

        // GET: Course/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.courseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }//end function

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }//end function

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.courseId == id);
        }//end function
    }//end class
}//end namespace
