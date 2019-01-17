using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAGR.Data.Models
{
    [Table("Book")]
    public class Book : BaseEntity
    {
        public int AuthorId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(10)]
        public string ISBN { get; set; }
        [StringLength(50)]
        public string Publisher { get; set; }
        public virtual Author Author { get; set; }
    }
}