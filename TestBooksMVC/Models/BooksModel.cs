using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestBooksMVC.Models
{
    public class BooksModel
    {
        public int BooksId { get; set; }
        [DisplayName("Title Book")]
        public string Title { get; set; }
        public string Author{ get; set; }
        public DateTime DatePublished { get; set; }
        public int Pages { get; set; }
        public string Type { get; set; }
    }
}