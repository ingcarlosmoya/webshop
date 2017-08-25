using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Webshop.Web.Utilities;
using System.Web.Mvc;

namespace Webshop.Web.Models
{
    public class ProductViewModel
    {
        public string Serial { get; set; }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price per unit"), DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double UnitPrice { get; set; }

        [Display(Name = "Categories")]
        public List<CategoryViewModel>Categories { get; set; }

    }
}