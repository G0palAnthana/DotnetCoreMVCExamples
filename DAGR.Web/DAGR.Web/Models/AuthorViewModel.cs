using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAGR.Web.Models
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name ="Total Books")]
        public int TotalBooks { get; set; }
        [Display(Name ="Author Name")]
        public string AuthorName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }
    }
}
