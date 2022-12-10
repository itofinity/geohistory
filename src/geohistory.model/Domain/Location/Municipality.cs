﻿namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Location
{
    public class Municipality : AbstractLocation
    {
        public const string Label = "municipality";

        public Municipality(string name, string citationId, string auditSessionId) : base(name, Label, citationId, auditSessionId)
        {
        }
    }
}