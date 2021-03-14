using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectKendo.Models
{
    public class CheckOutData
    {
        public int check_id { get; set; }
        [Required]
        [Display(Name = "First Name:")]
        public string firstnm { get; set; }
        [Required]
        [Display(Name = "Last Name:")]
        public string lastnm { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Address:")]
        public string address { get; set; }
        [Required]
        [Display(Name = "State:")]
        public string state { get; set; }
        [DataType(DataType.PostalCode)]
        [Required]
        [Display(Name = "Pin Code:")]
        public int postalcode { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number")]
        public string mobile { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ship Date")]
        public string shipdate { get; set; }
        public int totalprice { get; set; }
        public int u_id { get; set; }
        public int cart_id { get; set; }
        public int order_num { get; set; }
    }
}