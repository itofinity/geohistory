﻿using System.Collections.Generic;
using System.Drawing;
using Uk.Co.Itofinity.GeoHistory.Model.Location;

namespace Uk.Co.Itofinity.GeoHistory.Model
{
    public class RenderEntry
    {
        public RenderEntry(Entry entry) : this(entry, Color.Yellow, null)
        {
        }

        public RenderEntry(Entry e, Color color, IEnumerable<LatitudeLongtitude> routePoints)
        {
            this.Entry = e;
            this.RouteTo = routePoints;
            this.Color = color;
        }

        public Entry Entry { get; }
        public IEnumerable<LatitudeLongtitude> RouteTo { get; }
        public Color Color { get; }
    }
}
