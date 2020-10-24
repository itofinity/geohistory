using System;

namespace UK.CO.Itofinity.GeoHistory.Model.Audit
{
    public class SimpleAudit : IAudit
    {
        private DateTime? _when;
        public SimpleAudit(string who, DateTime when)
        {
            Who = who;
            _when = when;
        }

        public SimpleAudit(string who)
        {
            Who = who;
        }

        public string Who { get; }

        public DateTime When { get { return _when.HasValue ? _when.Value : DateTime.Now; } }
    }
}
