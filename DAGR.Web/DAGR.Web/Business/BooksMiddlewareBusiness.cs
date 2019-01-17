using DAGR.Data.Data.Repository;
using DAGR.Data.Models;
using DAGR.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAGR.Web.Business
{
    public class BooksMiddlewareBusiness
    {
        private readonly IRepository<Author> authorRepository;
        private readonly IRepository<Book> bookRepository;

        public BooksMiddlewareBusiness(IRepository<Author> repositoryAuthor, IRepository<Book> repositoryBook)
        {
            this.authorRepository = repositoryAuthor;
            this.bookRepository = repositoryBook;
        }

        public IEnumerable<BookListingViewModel> GetBookDetailsList()
        {
            return from book in bookRepository.GetAll()
                   select new BookListingViewModel
                   {
                       Id = book.Id,
                       BookName = book.Name,
                       Publisher = book.Publisher,
                       ISBN = book.ISBN,
                       AuthorName = GetAuthorFullName(book.AuthorId)
                   };
        }

        public BookListingViewModel GetNewBookDetails()
        {
            var authors = from author in this.authorRepository.GetAll()
                          select new AuthorViewModel
                          {
                              AuthorId = author.Id,
                              FirstName = author.FirstName,
                              LastName = author.LastName,
                              Email = author.Email
                          };
            return new BookListingViewModel { Authors = authors.ToList() };
        }
        public bool NewBook(BookListingViewModel bookListingViewModel)
        {
            Book newbook = new Book
            {
                Name = bookListingViewModel.BookName,
                Publisher = bookListingViewModel.Publisher,
                ISBN = bookListingViewModel.ISBN,
                AddedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                AuthorId = GetAuthorIdByName(bookListingViewModel.AuthorName).Value
            };
            try
            {
                bookRepository.Insert(newbook);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public BookListingViewModel GetBookDetailsById(int bookId)
        {
            var bookDetails = this.bookRepository.Get(bookId);
            return new BookListingViewModel
            {
                BookName = bookDetails.Name,
                AuthorName = GetAuthorFullName(bookDetails.AuthorId),
                ISBN = bookDetails.ISBN,
                Publisher = bookDetails.Publisher,
                Id = bookDetails.Id
            };
        }

        public bool UpdateBookDetails(int bookId, BookListingViewModel bookDetails)
        {
            try
            {
                Book existingBook = this.bookRepository.Get(bookId);
                existingBook.Name = bookDetails.BookName;
                existingBook.ISBN = bookDetails.ISBN;
                existingBook.Publisher = bookDetails.Publisher;
                existingBook.AuthorId = this.GetAuthorIdByName(bookDetails.AuthorName).Value;
                this.bookRepository.Update(existingBook);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteBookDetails(int bookId)
        {
            var bookDetails = this.bookRepository.Get(bookId);
            try
            {
                this.bookRepository.Delete(bookDetails);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #region Private Methods
        private string GetAuthorFullName(int authorId)
        {
            Author author = authorRepository.Get(authorId);
            return author != null ? string.Format("{0} {1}", author.FirstName, author.LastName): string.Empty;
        }
        private Nullable<int> GetAuthorIdByName(string authorName)
        {
            Author author = authorRepository.GetAll().Where(x => x.AuthorFullName.ToLower() == authorName.ToLower()).FirstOrDefault();
            return author?.Id;
        }
        #endregion
    }
}
