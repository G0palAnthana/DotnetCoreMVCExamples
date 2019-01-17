using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAGR.Data.Data.Repository;
using DAGR.Data.Models;
using DAGR.Web.Business;
using DAGR.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DAGR.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly BooksMiddlewareBusiness booksBusiness;

        public BookController(IRepository<Author> repositoryAuthor, IRepository<Book> repositoryBook)
        {
            this.booksBusiness = new BooksMiddlewareBusiness(repositoryAuthor, repositoryBook);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(this.booksBusiness.GetBookDetailsList());
        }
        [HttpGet]
        public IActionResult CreateNewBook()
        {
            return View(this.booksBusiness.GetNewBookDetails());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewBook([FromForm]BookListingViewModel newbook)
        {
            this.booksBusiness.NewBook(newbook);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditBookDetails(int bookId)
        {
            return View(this.booksBusiness.GetBookDetailsById(bookId));
        }

        [HttpPost]
        public IActionResult EditBookDetails([FromForm]BookListingViewModel book)
        {
            this.booksBusiness.UpdateBookDetails(book.Id, book);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int bookId)
        {
            this.booksBusiness.DeleteBookDetails(bookId);
            return RedirectToAction("Index");
        }
    }
}