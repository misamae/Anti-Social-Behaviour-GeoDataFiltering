using Kent.Boogaart.KBCsv;
using ldn.DataProvider.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ldn.DataProvider.Provider
{
    public class DataProvider
    {

        public IEnumerable<Graffiti> GetGraffitiData()
        {
            var sourceFile = @"C:\ldn\graffiti.csv";

            var reader = new Kent.Boogaart.KBCsv.CsvReader(File.OpenRead(sourceFile));

            reader.ReadHeaderRecord();

            DataRecord row;

            while ((row = reader.ReadDataRecord()) != null)
            {
                yield return new Graffiti
                {
                    EventDate = DateTime.Parse(row["eventdate"]),
                    StreetName = row["street"],
                    Location = new Location
                    {
                        Latitude = double.Parse(row["long"]),
                        Longitude = double.Parse(row["lat"]),
                        X = double.Parse(row["x"]),
                        Y = double.Parse(row["y"])
                    }
                };
            }
        }

        public IEnumerable<AnitSocial> GetAntiSocialData()
        {
            var sourceFile = @"C:\ldn\antisocial.csv";

            var reader = new Kent.Boogaart.KBCsv.CsvReader(File.OpenRead(sourceFile));

            reader.ReadHeaderRecord();

            DataRecord row;

            AnitSocial antiSocial;
            while ((row = reader.ReadDataRecord()) != null)
            {
                if (row.Count != 10) continue;
                
                antiSocial = null;
                try
                {
                    antiSocial = new AnitSocial
                    {
                        EventDate = DateTime.Parse(row["eventdate"]),
                        StreetName = row["street"],
                        Location = new Location
                        {
                            Latitude = double.Parse(row["lat"]),
                            Longitude = double.Parse(row["long"]),
                            X = double.Parse(row["x"]),
                            Y = double.Parse(row["y"])
                        },
                        Vomit = row["vomit"] == "NO" ? false : true,
                        Blood = row["blood"] == "NO" ? false : true,
                        Urine = row["urine"] == "NO" ? false : true,
                        HumanFouling = row["humanfouling"] == "NO" ? false : true,
                    };
                }
                catch
                {
                    continue;
                }

                if(antiSocial != null)
                    yield return antiSocial;
            }
        }

        //Lat.Long.DataPoints.2
        //lat	long	eventdate	street	vomit	blood	urine	humanfouling	dogfouling	graffiti
        public IEnumerable<AnitSocialV1> GetAntiSocialDataV1()
        {
            var sourceFile = @"C:\ldn\Lat.Long.DataPoints.2.csv";

            var reader = new Kent.Boogaart.KBCsv.CsvReader(File.OpenRead(sourceFile));

            reader.ReadHeaderRecord();

            DataRecord row;

            AnitSocialV1 antiSocial;
            while ((row = reader.ReadDataRecord()) != null)
            {
                if (row.Count != 10) continue;

                antiSocial = null;
                try
                {
                    //lat	long	eventdate	street	vomit	blood	urine	humanfouling	dogfouling	graffiti
                    antiSocial = new AnitSocialV1
                    {
                        EventDate = DateTime.Parse(row["eventdate"]),
                        StreetName = row["street"],
                        Location = new Location
                        {
                            Latitude = double.Parse(row["lat"]),
                            Longitude = double.Parse(row["long"]),
                        },
                        Vomit = row["vomit"] == "0" ? false : true,
                        Blood = row["blood"] == "0" ? false : true,
                        Urine = row["urine"] == "0" ? false : true,
                        HumanFouling = row["humanfouling"] == "0" ? false : true,
                        DogFouling = row["dogfouling"] == "0" ? false : true,
                        Graffiti = row["graffiti"] == "0" ? false : true,
                    };
                }
                catch
                {
                    continue;
                }

                if (antiSocial != null)
                    yield return antiSocial;
            }
        }

        //antisocial.top.csv
        public IEnumerable<AnitSocialV2> GetAntiSocialDataV2()
        {
            var sourceFile = @"C:\ldn\antisocial.top.csv";

            var reader = new Kent.Boogaart.KBCsv.CsvReader(File.OpenRead(sourceFile));

            reader.ReadHeaderRecord();

            DataRecord row;

            AnitSocialV2 antiSocial;
            while ((row = reader.ReadDataRecord()) != null)
            {
                if (row.Count != 4) continue;

                antiSocial = null;
                try
                {
                    //lat	long	eventdate	street	vomit	blood	urine	humanfouling	dogfouling	graffiti
                    antiSocial = new AnitSocialV2
                    {
                        Location = new Location
                        {
                            Latitude = double.Parse(row["lat"]),
                            Longitude = double.Parse(row["long"]),
                        },
                        Category = row["Antisocial"],
                        Radius = float.Parse(row["Radius"]),
                    };
                }
                catch
                {
                    continue;
                }

                if (antiSocial != null)
                    yield return antiSocial;
            }
        }

        public IEnumerable<CrowdFlow> GetCrowdFlow(int siteid)
        {
            var source = string.Format("http://opensensors.io/crowddynamics/{0}", siteid);

            var webrequest = WebRequest.Create(source);

            webrequest.Credentials = new NetworkCredential("datascience", "london101");

            var response = webrequest.GetResponse();

            var stream = response.GetResponseStream();

            var reader = new StreamReader(stream);

            string row = string.Empty;

            while (!string.IsNullOrWhiteSpace(row = reader.ReadLine()))
            {
                //total_pedestrains	site_id	borough	hr	year	owner	month	site_name	day
                var items = row.Split(',');

                if (items[0] != "total_pedestrains")
                {
                    var hours = items[3].Split(':');
                    var time = new DateTime(int.Parse(items[4]),
                        int.Parse(items[6]),
                        int.Parse(items[8]),
                        int.Parse(hours[0]),
                        int.Parse(hours[1]),
                        int.Parse(hours[2]));

                    yield return new CrowdFlow
                    {
                        SiteId = int.Parse(items[1]),
                        Borough = items[2],
                        SiteName = items[7],
                        TotalPedestrians = int.Parse(items[0]),
                        Time = time, 
                        Owner = items[5]
                    };
                }
            }
        }

        public IEnumerable<CrowdFlow> GetCrowdFlowByYear(int year)
        {
            throw new NotSupportedException();
            var source = string.Format("http://opensensors.io/crowddynamicsyears/{0}", year);

            var webrequest = WebRequest.Create(source);

            webrequest.Credentials = new NetworkCredential("datascience", "london101");

            var response = webrequest.GetResponse();

            var stream = response.GetResponseStream();

            var reader = new StreamReader(stream);

            string row = string.Empty;

            while (!string.IsNullOrWhiteSpace(row = reader.ReadLine()))
            {
                //total_pedestrains	site_id	borough	hr	year	owner	month	site_name	day
                var items = row.Split(',');

                if (items[0] != "total_pedestrains")
                {
                    var hours = items[3].Split(':');
                    var time = new DateTime(int.Parse(items[4]),
                        int.Parse(items[6]),
                        int.Parse(items[8]),
                        int.Parse(hours[0]),
                        int.Parse(hours[1]),
                        int.Parse(hours[2]));

                    yield return new CrowdFlow
                    {
                        SiteId = int.Parse(items[1]),
                        Borough = items[2],
                        SiteName = items[7],
                        TotalPedestrians = int.Parse(items[0]),
                        Time = time
                    };
                }
            }
        }

        //public IEnumerable<Site> GetSites()
        //{
        //    var sourceFile = @"C:\ldn\sites.csv";

        //    var reader = new Kent.Boogaart.KBCsv.CsvReader(File.OpenRead(sourceFile));

        //    reader.ReadHeaderRecord();

        //    DataRecord row;

        //    Site site;
        //    while ((row = reader.ReadDataRecord()) != null)
        //    {
        //        if (row.Count != 10) continue;

        //        site = null;
        //        try
        //        {
        //            site = new Site
        //            {
        //                EventDate = DateTime.Parse(row["eventdate"]),
        //                StreetName = row["street"],
        //                Location = new Location
        //                {
        //                    Latitude = double.Parse(row["long"]),
        //                    Longitude = double.Parse(row["lat"]),
        //                    X = double.Parse(row["x"]),
        //                    Y = double.Parse(row["y"])
        //                },
        //                Vomit = row["vomit"] == "NO" ? false : true,
        //                Blood = row["blood"] == "NO" ? false : true,
        //                Urine = row["urine"] == "NO" ? false : true,
        //                HumanFouling = row["humanfouling"] == "NO" ? false : true,
        //            };
        //        }
        //        catch
        //        {
        //            continue;
        //        }

        //        if (antiSocial != null)
        //            yield return antiSocial;
        //    }
        //}
    }
}
