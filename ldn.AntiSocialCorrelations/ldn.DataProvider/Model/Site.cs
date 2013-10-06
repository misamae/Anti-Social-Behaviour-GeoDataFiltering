using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ldn.DataProvider.Model
{
    public class Site
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Borough { get; set; }

        public double Northing { get; set; }

        public double Easting { get; set; }

        public string Owner { get; set; }
    }
}
