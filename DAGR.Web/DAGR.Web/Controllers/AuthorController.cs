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
    public class AuthorController : Controller
    {
        private readonly AuthorMiddlewareBusiness authorBusiness;

        public AuthorController(IRepository<Author> repositoryAuthor, IRepository<Book> repositoryBook)
        {
            this.authorBusiness = new AuthorMiddlewareBusiness(repositoryAuthor, repositoryBook);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(this.authorBusiness.GetAllAuthorDetails().ToList());
        }
        [HttpGet]
        public IActionResult CreateNewAuthor()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewAuthor([FromForm]AuthorViewModel newAuthor)
        {
            this.authorBusiness.CreateNewAuthor(newAuthor);
            return RedirectToAction("Index");
        }

        public IActionResult EditAuthor(int authorId)
        {
            return View(this.authorBusiness.UpdateAuthor(authorId));
        }

        [HttpPost]
        public IActionResult EditAuthor([FromForm]AuthorViewModel existingAuthor)
        {
            this.authorBusiness.UpdateAuthor(existingAuthor);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAuthor(int authorId)
        {
            this.authorBusiness.DeleteAuthor(authorId);
            return RedirectToAction("Index");
        }
    }
}