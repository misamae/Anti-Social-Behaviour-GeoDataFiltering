using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ldn.DataProvider;
using System.IO;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace ldn.AntiSocialCorrelations
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataProvider = new DataProvider.Provider.DataProvider();
            var antisocials = dataProvider.GetAntiSocialData();
            var antisocialsV1 = dataProvider.GetAntiSocialDataV1();
            var graffit = dataProvider.GetGraffitiData().GroupBy(g => g.StreetName).ToDictionary(g => g.Key, g => g);

            //var query = antisocials.Where(a => graffit.ContainsKey(a.StreetName));

            //foreach (var item in query)
            //{
            //    Console.WriteLine(item.StreetName);
            //}
            //var crowdFlow = dataProvider.GetCrowdFlow(4);

            //foreach (var item in antisocials)
            //{
            //    Console.WriteLine(string.Format("{0}, {1}", item.Location.Latitude, item.Location.Latitude));
            //}

            //var mappedJsonDatantisocials= antisocials.Select(s => new Marker{ lat = s.Location.Latitude, lng = s.Location.Longitude});

            //var file = File.OpenWrite(@"C:\ldn\markers");

            //var ser = new DataContractJsonSerializer(typeof(test));

            //ser.WriteObject(file, new test { locations = mappedJsonDatantisocials });

            //file.Flush();

            var mappedJsonDatantisocials = antisocialsV1.Where(a => a.Urine)
                .Select(s => new Marker { lat = s.Location.Latitude, lng = s.Location.Longitude })
                .ToList();

            var file = File.OpenWrite(@"C:\ldn\Urine-markers.json");

            var ser = new DataContractJsonSerializer(typeof(test));

            ser.WriteObject(file, new test { locations = mappedJsonDatantisocials });

            file.Flush();
        }

        [DataContract]
        public class test
        {
            [DataMember]
            public IEnumerable<Marker> locations { get; set; }
        }
    }
}
