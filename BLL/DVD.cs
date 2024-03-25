using System;
namespace LibraryManager.BLL
{
    public class DVD : Item
    {
        public String director { get; set; }
        public string Authornationality { get; set; }
        public string Authorybirthday { get; set; }

        public DVD(String title, int barcode, String director,string Authornationality,string Authorbirthday) : base(title, barcode)
        {
            this.director = director;
            this.Authornationality = Authornationality;
            this.Authorybirthday = Authorbirthday;

        }
    }
}
