using System;
namespace LibraryManager.BLL
{
    public class Book : Item
    {
        public String author { get; set; }
        public String ISBN { get; set; }
        public string Authornationality { get; set; }
        public string Authorybirthday { get; set; }

        public Book(String title, int barcode, String ISBN, String author, string Authornationality, string Authorbirthday) : base(title, barcode)
        {
            this.ISBN = ISBN;
            this.author = author;
            this.Authornationality = Authornationality;
            this.Authorybirthday = Authorbirthday;

        }
    }
}
