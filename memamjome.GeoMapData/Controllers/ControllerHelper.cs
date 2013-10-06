using ldn.DataProvider.Provider;
using memamjome.GeoMapData.Models;
using memamjome.KMeanCluster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace memamjome.GeoMapData.Controllers
{
    public static class ControllerHelper
    {
        public static IEnumerable<Marker> GetClusterMakers(int clusterNumber)
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

            return Clustring.DataPointsPerCluster(rawData, clusterNumber, clusters, 1000)
                    .Select(r => new Marker { lat = r.Item2, lng = r.Item1, r = 100 }).ToList();
        }
    }
}