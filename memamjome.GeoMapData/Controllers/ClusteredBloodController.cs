using ldn.DataProvider.Provider;
using memamjome.GeoMapData.Models;
using memamjome.KMeanCluster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace memamjome.GeoMapData.Controllers
{
    public class ClusteredBloodsController : ApiController
    {
        public IEnumerable<Marker> GetAllClusteredBloods()
        {
            var result = new DataProvider()
                .GetAntiSocialDataV1()
                .Select(a =>
                    new Marker
                    {
                        lng = a.Location.Longitude,
                        lat = a.Location.Latitude,
                        r = 1 * 100
                    })
                 .ToList();


            var rawData = result.Select(r => new double[] { r.lng, r.lat }).ToArray();

            var clusters = Clustring.KMean(rawData, 5, 30);

            var clustersGrouped = clusters.GroupBy(a => a);

            var itemsInClusters = new List<Marker>();

            var index = 0;

            foreach (var item in clusters)
            {
                var newResult = Clustring.DataPointsPerCluster(rawData, index, clusters, 50).ToList();

                foreach (var r in newResult)
                {
                    itemsInClusters.Add(new Marker { lat = r.Item2, lng = r.Item1, r = 100 });
                }

                index++;
            }

            return itemsInClusters;
        }

        public IEnumerable<Marker> GetClusteredBloodsByClusterNumber(int clusterNumber)
        {
            var result = new DataProvider()
                .GetAntiSocialDataV1()
                .Select(a =>
                    new Marker
                    {
                        lng = a.Location.Longitude,
                        lat = a.Location.Latitude,
                        r = 1 * 100
                    })
                 .ToList();


            var rawData = result.Select(r => new double[] { r.lng, r.lat }).ToArray();

            var clusters = Clustring.KMean(rawData, 5, 1000);

            var clustersGrouped = clusters.GroupBy(a => a);

            var itemsInClusters = new List<Marker>();

            return Clustring.DataPointsPerCluster(rawData, clusterNumber, clusters, 100)
                    .Select(r => new Marker { lat = r.Item2, lng = r.Item1, r = 100 }).ToList();
        }
    }
}
