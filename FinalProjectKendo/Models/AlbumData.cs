using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectKendo.Models
{
            [Table("mg_albumstore", Schema = "public")]

    public class AlbumData
    {
        [Key]
        public int a_id { get; set; }
        public string al_img { get; set; }
        public string a_artist { get; set; }
        public string a_title { get; set; }
        public int a_price { get; set; }
        public int quantity { get; set; }

      
    }
}