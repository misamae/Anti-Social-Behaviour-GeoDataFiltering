using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ldn.DataProvider.Model
{
    public class Graffiti
    {
        public DateTime EventDate { get; set; }

        public string StreetName { get; set; }

        public Location Location { get; set; }
    }
}
