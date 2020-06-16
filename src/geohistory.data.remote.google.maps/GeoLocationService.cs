using GoogleMapsApi.Entities.Directions.Request;
using System;
using System.Linq;
using Uk.Co.Itofinity.Geohistory.Model;
using Uk.Co.Itofinity.Geohistory.Model.Citation;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using System.Collections.Generic;
using System.Runtime;
using Uk.Co.Itofinity.Geohistory.Model.Location;

namespace Uk.Co.Itofinity.GeoHistory.Data.Remote.Google.Maps
{
    public class GeoLocationService
    {
        private readonly string apikey;
        private Dictionary<string, object> cache = new Dictionary<string, object>();

        public GeoLocationService(string apikey)
        {
            this.apikey = apikey;
        }

        public LatitudeLongtitude GetLatitudeLongtitude(ILocation location)
        {
            var geocodeResponse = GetGeocodeResponse(location);

            if (geocodeResponse == null)
            {
                return new LatitudeLongtitude();
            }

            var loc = geocodeResponse.Results.ToList()[0].Geometry.Location;
            return new LatitudeLongtitude(loc.Latitude, loc.Longitude);
        }

        public LatitudeLongtitude GetLatitudeLongtitude(Entry entry)
        {
            var geocodeResponse = GetGeocodeResponse(entry);

            if(geocodeResponse == null)
            {
                return new LatitudeLongtitude();
            }

            var loc = geocodeResponse.Results.ToList()[0].Geometry.Location;
            return new LatitudeLongtitude(loc.Latitude, loc.Longitude);
        }


        public IEnumerable<LatitudeLongtitude> GetRoute(Entry from, Entry to)
        {
            var points = new List<LatitudeLongtitude>();

            var directionsResponse = GetDirectionsResponse(from, to);

            if (directionsResponse == null)
            {
                return points;
            }
            
            directionsResponse.Routes.ToList()[0].Legs.ToList()[0].Steps.ToList().ForEach(s =>
            {
                points.Add(new LatitudeLongtitude(s.StartLocation.Latitude, s.StartLocation.Longitude));
                points.Add(new LatitudeLongtitude(s.EndLocation.Latitude, s.EndLocation.Longitude));
            });


            return points;
        }

        private T AddToCache<T>(string cacheKey, T value)
        {
            this.cache[cacheKey] = value;
            return value;
        }

        private T FindInCache<T>(string cacheKey)
        {
            if(!this.cache.Keys.Contains(cacheKey))
            {
                return default(T);
            }

            var val = this.cache[cacheKey];
            if (val is T)
            {
                return (T)this.cache[cacheKey];
            }
            return default(T);
        }

        private void ClearCache()
        {
            this.cache.Clear();
        }

        private GeocodingResponse GetGeocodeResponse(Entry entry)
        {
            return GetGeocodeResponse(entry.Where);
        }
        private GeocodingResponse GetGeocodeResponse(ILocation where)
        { 
            var cacheKey = where.Name.ToString() + "-geocoding";
            var cachedVal = FindInCache<GeocodingResponse>(cacheKey);
            if (cachedVal != null)
            {
                return cachedVal;
            }

            GeocodingRequest geocodeRequest = new GeocodingRequest()
            {
                Address = where.Name,
            };

            geocodeRequest.ApiKey = this.apikey;
            
            var geocodingEngine = GoogleMaps.Geocode;
            
            GeocodingResponse geocode = geocodingEngine.Query(geocodeRequest);
            
            if (geocode.Status == Status.OK)
            {
                return AddToCache(cacheKey, geocode);
            }

            return null;
        }

        private DirectionsResponse GetDirectionsResponse(Entry from, Entry to)
        {
            var cacheKey = from.ToString() + "-" + to.ToString() + "-directions";
            var cachedVal = FindInCache<DirectionsResponse>(cacheKey);
            if (cachedVal != null)
            {
                return cachedVal;
            }

            var directionsRequest = new DirectionsRequest { Origin = from.Where.Name, Destination = to.Where.Name };
            directionsRequest.ApiKey = this.apikey;
            DirectionsResponse directions = GoogleMaps.Directions.Query(directionsRequest);


            if (directions.Status == DirectionsStatusCodes.OK)
            {
                return AddToCache(cacheKey, directions);
            }

            return null;
        }
    }
}
