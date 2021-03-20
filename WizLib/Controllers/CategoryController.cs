using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var objList = _context.Categories.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            var obj = new Category();
            if (id == null)
            {
                return View(obj);
            }
            //this for edit
            obj = _context.Categories.FirstOrDefault(c => c.Category_Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Category_Id == 0)
                {
                    // this is create
                    _context.Categories.Add(obj);
                }
                else
                {
                    //this is an update
                    _context.Categories.Update(obj);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }


        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Categories.FirstOrDefault(c => c.Category_Id == id);
            _context.Categories.Remove(objFromDb);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple2()
        {

            var categoryList = new List<Category>();
            for (int i = 1; i <= 2; i++)
            {
                categoryList.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _context.Categories.AddRange(categoryList);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple5()
        {
            var categoryList = new List<Category>();
            for (int i = 1; i <= 5; i++)
            {
                categoryList.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _context.Categories.AddRange(categoryList);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple2()
        {

            var categoryList = _context.Categories.OrderByDescending(c=>c.Category_Id).Take(2).ToList();
           
            _context.Categories.RemoveRange(categoryList);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            var categoryList = _context.Categories.OrderByDescending(c => c.Category_Id).Take(5).ToList();

            _context.Categories.RemoveRange(categoryList);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
