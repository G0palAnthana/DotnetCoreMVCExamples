using DAGR.Data.Data.Repository;
using DAGR.Data.Models;
using DAGR.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAGR.Web.Business
{
    public class AuthorMiddlewareBusiness
    {
        private readonly IRepository<Author> authorRepository;
        private readonly IRepository<Book> bookRepository;

        public AuthorMiddlewareBusiness(IRepository<Author> repositoryAuthor, IRepository<Book> repositoryBook)
        {
            this.authorRepository = repositoryAuthor;
            this.bookRepository = repositoryBook;
        }

        #region Autor CRUD Methods
        //Select
        public IEnumerable<AuthorViewModel> GetAllAuthorDetails()
        {
            return from author in this.authorRepository.GetAll()
                   select new AuthorViewModel
                   {
                       AuthorId = author.Id,
                       FirstName = author.FirstName,
                       LastName = author.LastName,
                       Email = author.Email,
                       TotalBooks = this.GetBooksCountByAuthorId(author.Id)
                   };
        }

        //Create
        public bool CreateNewAuthor(AuthorViewModel newAuthorDetails)
        {
            try
            {
                Author newauthor = new Author
                {
                    FirstName = newAuthorDetails.FirstName,
                    LastName = newAuthorDetails.LastName,
                    Email = newAuthorDetails.Email,
                    AddedDate = DateTime.Now,
                    ModifiedDate  = DateTime.Now
                };
                authorRepository.Insert(newauthor);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //Update 
        public AuthorViewModel UpdateAuthor(int authorId)
        {
            var author = this.authorRepository.Get(authorId);
            return new AuthorViewModel
            {
                AuthorId = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                TotalBooks = this.GetBooksCountByAuthorId(authorId)
            };
        }

        public bool UpdateAuthor(AuthorViewModel existingAuthorDetails)
        {
            try
            {
                var existingauthor = this.authorRepository.Get(existingAuthorDetails.AuthorId);
                existingauthor.FirstName = existingAuthorDetails.FirstName;
                existingauthor.LastName = existingAuthorDetails.LastName;
                existingauthor.Email = existingAuthorDetails.Email;

                this.authorRepository.Update(existingauthor);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //Delete 
        public bool DeleteAuthor(int authorId)
        {
            try
            {
                var author = this.authorRepository.Get(authorId);
                this.authorRepository.Delete(author);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion

        #region Private Methods
        private int GetBooksCountByAuthorId(int authorId)
        {
            return bookRepository.GetAll().Count(q => q.AuthorId == authorId);
        }
        #endregion
    }
}
