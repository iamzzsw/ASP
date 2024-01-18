using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Genre { get; set; }

        public decimal Price { get; set; }

    }

}
