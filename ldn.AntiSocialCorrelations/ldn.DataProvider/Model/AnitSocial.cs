using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ldn.DataProvider.Model
{
    public class AnitSocial
    {
        public string StreetName { get; set; }

        public Location Location { get; set; }

        public bool Vomit { get; set; }

        public bool Blood { get; set; }

        public bool Urine { get; set; }

        public bool HumanFouling { get; set; }

        public DateTime EventDate { get; set; }

    }
}
