using ldn.DataProvider.Provider;
using memamjome.GeoMapData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace memamjome.GeoMapData.Controllers
{
    public class GraffitisController : ApiController
    {
        //public IEnumerable<Marker> GetAllGraffitis()
        //{
        //    var rnd = new Random();
        //    var randomIndexs = Enumerable.Range(0, Common.Constants.NumberOfPoints).Select(r => rnd.Next(6000)).ToList();
        //    var result = new DataProvider()
        //        .GetAntiSocialDataV1()
        //        .Where(a => a.Graffiti)
        //        .Select((a, i) => new
        //        {
        //            index = i,
        //            m = new Marker
        //                {
        //                    lng = a.Location.Longitude,
        //                    lat = a.Location.Latitude,
        //                    r = new Random().Next(100)
        //                }
        //        })
        //         .Where(item => randomIndexs.Contains(item.index))
        //         .Select(item => item.m)
        //         .ToList();

        //    return result;
        //}

        public IEnumerable<Marker> GetAllGraffitis()
        {
            var result = new DataProvider()
                .GetAntiSocialDataV2()
                .Where(a => a.Category == "graffiti")
                .Select((a, i) => new Marker
                {
                    lng = a.Location.Longitude,
                    lat = a.Location.Latitude,
                    r = a.Radius * 100
                }
                )
                 .ToList();

            return result;
        }

    }
}
