using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Webshop.Models
{
    public class ProductViewModel
    {
        public int Serial { get; set; }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price per unit")]
        public decimal UnitPrice { get; set; }

    }
}