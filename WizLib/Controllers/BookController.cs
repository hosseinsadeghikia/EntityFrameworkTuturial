using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var objList = _context.Books.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            var obj = new Book();
            if (id == null)
            {
                return View(obj);
            }
            //this for edit
            obj = _context.Books.FirstOrDefault(a => a.Book_Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Book obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Book_Id == 0)
                {
                    // this is create
                    _context.Books.Add(obj);
                }
                else
                {
                    //this is an update
                    _context.Books.Update(obj);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }


        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Books.FirstOrDefault(c => c.Book_Id == id);
            _context.Books.Remove(objFromDb);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
