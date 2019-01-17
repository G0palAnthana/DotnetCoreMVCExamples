using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAGR.Data.Models
{
    [Table("Author")]
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string AuthorFullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }
        public virtual ICollection<Book> Books { get; set; }
    }
}
