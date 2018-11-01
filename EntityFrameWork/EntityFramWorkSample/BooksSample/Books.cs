using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BooksSample
{

    [Table("Books")]
    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }
    }
}
