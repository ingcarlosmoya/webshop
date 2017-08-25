using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Model
{
    public class Product
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Serial { get; set; }

        public List<Category> Categories { get; set; }
    }
}
