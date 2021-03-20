using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublisherController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var objList = _context.Publishers.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            var obj = new Publisher();
            if (id == null)
            {
                return View(obj);
            }
            //this for edit
            obj = _context.Publishers.FirstOrDefault(c => c.Publisher_Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Publisher_Id == 0)
                {
                    // this is create
                    _context.Publishers.Add(obj);
                }
                else
                {
                    //this is an update
                    _context.Publishers.Update(obj);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }


        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Publishers.FirstOrDefault(c => c.Publisher_Id == id);
            _context.Publishers.Remove(objFromDb);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
