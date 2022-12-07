using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model;
using Uk.Co.Itofinity.GeoHistory.Model.Location;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Uk.Co.Itofinity.GeoHistory.Data.Remote.Google.Maps
{
    public class CachingGeoLocationService : IGeoLocationService
    {
        private IGeoLocationService coreService;
        private string root = "./cache";
        public CachingGeoLocationService(string apikey)
        {
            coreService = new GeoLocationService(apikey);
            if(!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
        }

        public LatitudeLongtitude GetLatitudeLongtitude(ILocation location)
        {
            if(CacheContains(location))
            {
                return Cached(location);
            }
            var latLong = coreService.GetLatitudeLongtitude(location);
            Cache(location, latLong);
            return latLong;
        }

        public LatitudeLongtitude GetLatitudeLongtitude(Entry entry)
        {
            if(CacheContains(entry))
            {
                return Cached(entry);
            }
            var latLong = coreService.GetLatitudeLongtitude(entry);
            Cache(entry, latLong);
            return latLong;
        }

        public IEnumerable<LatitudeLongtitude> GetRoute(Entry from, Entry to)
        {
            if(CacheContains(from, to))
            {
                return Cached(from, to);
            }
            var route = coreService.GetRoute(from, to);
            Cache(from, to, route);
            return route;
        }

        private void Cache(Entry from, Entry to, IEnumerable<LatitudeLongtitude> route)
        {
            File.WriteAllText(CacheKey(from, to), JsonConvert.SerializeObject(route, Formatting.Indented));
        }

        private void Cache(ILocation location, LatitudeLongtitude latLong)
        {
            File.WriteAllText(CacheKey(location), JsonConvert.SerializeObject(latLong, Formatting.Indented));
        }
        
        private void Cache(Entry entry, LatitudeLongtitude latLong)
        {
            File.WriteAllText(CacheKey(entry), JsonConvert.SerializeObject(latLong, Formatting.Indented));
        }

        private string CacheKey(Entry from, Entry to)
        {
            return Path.Combine(root, $@"route-{GetBase64(from.ToString())}-{GetBase64(to.ToString())}.json");
        }

        private string CacheKey(ILocation location)
        {
            return Path.Combine(root, $@"location-{GetBase64(location.ToString())}.json");
        }

        private string CacheKey(Entry entry)
        {
            return Path.Combine(root, $@"entry-{GetBase64(entry.ToString())}.json");
        }
        
        private string GetBase64(string text)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(text));
        }
        private LatitudeLongtitude Cached(Entry entry)
        {
            return JsonConvert.DeserializeObject<LatitudeLongtitude>(File.ReadAllText(CacheKey(entry)));
        }
        
        private LatitudeLongtitude Cached(ILocation location)
        {
            return JsonConvert.DeserializeObject<LatitudeLongtitude>(File.ReadAllText(CacheKey(location)));
        }

        private IEnumerable<LatitudeLongtitude> Cached(Entry from, Entry to)
        {
            return JsonConvert.DeserializeObject<IEnumerable<LatitudeLongtitude>>(File.ReadAllText(CacheKey(from, to)));
        }

        private bool CacheContains(ILocation location)
        {
            return Directory.GetFiles(root).Contains(CacheKey(location));
        }
        
        private bool CacheContains(Entry entry)
        {
            return Directory.GetFiles(root).Contains(CacheKey(entry));
        }

        private bool CacheContains(Entry from, Entry to)
        {
            return Directory.GetFiles(root).Contains(CacheKey(from, to));
        }
    }
}