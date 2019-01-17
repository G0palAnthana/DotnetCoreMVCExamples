using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAGR.Data.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string IPAddress { get; set; }
    }
}
