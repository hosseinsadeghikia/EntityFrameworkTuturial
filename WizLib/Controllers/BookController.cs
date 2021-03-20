using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;
using WizLib_Model.ViewModels;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

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
            var objList = _context.Books.Include(x => x.Publisher).ToList();
            foreach (var obj in objList)
            {
                //Least Efficient
                //obj.Publisher = _context.Publishers.FirstOrDefault(p => p.Publisher_Id == obj.Publisher_Id);

                //Explicit Loading More Efficient
                //_context.Entry(obj).Reference(p=>p.Publisher).Load();
            }
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            var obj = new BookVM
            {
                PublisherList = _context.Publishers.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Publisher_Id.ToString()
                })
            };

            if (id == null)
            {
                return View(obj);
            }

            //this for edit
            obj.Book = _context.Books.FirstOrDefault(a => a.Book_Id == id);

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM obj)
        {

            if (obj.Book.Book_Id == 0)
            {
                // this is create
                _context.Books.Add(obj.Book);
            }
            else
            {
                //this is an update
                _context.Books.Update(obj.Book);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            var obj = new BookVM();

            if (id == null)
            {
                return View(obj);
            }

            //this for edit
            obj.Book = _context.Books.Include(x => x.BookDetail).
                FirstOrDefault(a => a.Book_Id == id);

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookVM obj)
        {

            if (obj.Book.BookDetail.BookDetail_Id == 0)
            {
                // this is create
                _context.BookDetails.Add(obj.Book.BookDetail);
                _context.SaveChanges();

                var bookFromDb = _context.Books.FirstOrDefault(b => b.Book_Id == obj.Book.Book_Id);
                if (bookFromDb != null) bookFromDb.BookDetail_Id = obj.Book.BookDetail.BookDetail_Id;
                _context.SaveChanges();
            }
            else
            {
                //this is an update
                _context.BookDetails.Update(obj.Book.BookDetail);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Books.FirstOrDefault(c => c.Book_Id == id);
            _context.Books.Remove(objFromDb ?? throw new InvalidOperationException());
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PlayGround()
        {
            //var bookTemp = _context.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _context.Books;
            //double totalPrice = 0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _context.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _context.Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _context.Books.Count();

            IEnumerable<Book> bookList1 = _context.Books;
            var filterBook1 = bookList1.Where(b => b.Price > 500).ToList();

            IQueryable<Book> bookList2 = _context.Books;
            var filterBook2 = bookList2.Where(b => b.Price > 500);

            //var category = _context.Categories.FirstOrDefault();
            //_context.Entry(category).State = EntityState.Modified;
            //_context.SaveChanges();

            //Updating Related Data
            var bookTemp1 = _context.Books.Include(x => x.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            if (bookTemp1 != null) bookTemp1.BookDetail.NumberOfChapters = 150;
            _context.Books.Update(bookTemp1 ?? throw new InvalidOperationException());
            _context.SaveChanges();

            var bookTemp2 = _context.Books.Include(x => x.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            if (bookTemp2 != null) bookTemp2.BookDetail.NumberOfChapters = 210;
            _context.Books.Attach(bookTemp2 ?? throw new InvalidOperationException());
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
