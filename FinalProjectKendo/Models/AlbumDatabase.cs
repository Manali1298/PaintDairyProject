using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectKendo.Models
{
    public class AlbumDatabase
    {
        public int a_id { get; set; }
        public string al_img { get; set; }
        public string a_artist { get; set; }
        public string a_title { get; set; }
        public int a_price { get; set; }
        public int quantity { get; set; }
        public string email { get; set; }
        public int sellqua { get; set; }
        public int cart_itemqua { get; set; }
    }
}