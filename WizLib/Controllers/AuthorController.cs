using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var objList = _context.Authors.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            var obj = new Author();
            if (id == null)
            {
                return View(obj);
            }
            //this for edit
            obj = _context.Authors.FirstOrDefault(a => a.Author_Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Author_Id == 0)
                {
                    // this is create
                    _context.Authors.Add(obj);
                }
                else
                {
                    //this is an update
                    _context.Authors.Update(obj);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }


        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Authors.FirstOrDefault(c => c.Author_Id == id);
            _context.Authors.Remove(objFromDb);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
