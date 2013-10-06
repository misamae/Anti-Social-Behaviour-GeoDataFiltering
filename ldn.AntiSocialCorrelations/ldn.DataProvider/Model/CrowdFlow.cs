using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ldn.DataProvider.Model
{
    public class CrowdFlow
    {
        public int TotalPedestrians { get; set; }

        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public string Borough { get; set; }

        public DateTime Time { get; set; }

        public string Owner { get; set; }
    }
}
