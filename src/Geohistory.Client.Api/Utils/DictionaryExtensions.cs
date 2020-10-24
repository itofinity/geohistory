using System;
using System.Collections.Generic;
using System.Text;

namespace UK.CO.Itofinity.GeoHistory.Client.Api.Utils
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// <see cref="https://stackoverflow.com/a/3982554/1297975"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="source"></param>
        /// <param name="collection"></param>
        public static void AddRange<T, S>(this Dictionary<T, S> source, Dictionary<T, S> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("Collection is null");
            }

            foreach (var item in collection)
            {
                if (!source.ContainsKey(item.Key))
                {
                    source.Add(item.Key, item.Value);
                }
                else
                {
                    // handle duplicate key issue here
                }
            }
        }   
    }
}
