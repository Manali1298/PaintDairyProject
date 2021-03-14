using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectKendo.Models
{
    public class CartData
    {
        public int cart_id { get; set; }
        public int u_id { get; set; }
        public int a_id { get; set; }

        public string email { get; set; }
        public int item_qua { get; set; }
        public int cart_itemqua { get; set; }
        public int a_price { get; set; }


         public string al_img { get; set; }
      
        public string a_title { get; set; }

        public string Color { get; set; }
    }
}