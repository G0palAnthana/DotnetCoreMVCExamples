using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DAGR.Web.Models
{
    public class BookListingViewModel
    {
        public int Id { get; set; }
        [DisplayName("Book Name")]
        public string BookName { get; set; }
        [DisplayName("Author Name")]
        public string AuthorName { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public List<AuthorViewModel> Authors { get; set; }
    }
}
