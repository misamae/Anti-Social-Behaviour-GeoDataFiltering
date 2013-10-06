using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ldn.DataProvider.Model
{
    public class AnitSocialV1
    {
        public string StreetName { get; set; }

        public Location Location { get; set; }

        public bool Vomit { get; set; }

        public bool Blood { get; set; }

        public bool Urine { get; set; }

        public bool HumanFouling { get; set; }

        public bool DogFouling { get; set; }

        public bool Graffiti { get; set; }

        public DateTime EventDate { get; set; }

    }

    public class AnitSocialV2
    {
        public Location Location { get; set; }

        public float Radius { get; set; }

        public string Category { get; set; }
    }
}
