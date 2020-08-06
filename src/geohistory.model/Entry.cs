using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model.Audit;
using Uk.Co.Itofinity.GeoHistory.Model.Citation;
using Uk.Co.Itofinity.GeoHistory.Model.Location;
using Uk.Co.Itofinity.GeoHistory.Model.Time;

namespace Uk.Co.Itofinity.GeoHistory.Model
{
    public class Entry : Dictionary<string, object>
    {
        private const string what = "what";
        private const string where = "where";
        private const string when = "when";
        private const string citation = "citation";
        private const string audit = "audit";

        public Entry(IEntity what, ICitation citation, IAudit audit) : this(what, null, null, citation, audit)
        {

        }

        public Entry(IEntity what, ILocation where, IFuzzyDateTimeRange when, ICitation citation, IAudit audit) : base()
        {
            this[Entry.what] = what;
            this[Entry.where] = where;
            this[Entry.when] = when;
            this[Entry.citation] = citation;
            this[Entry.audit] = audit;
        }

        public IEntity What => this[what] as IEntity;

        public ILocation Where => this[where] as ILocation;

        public IFuzzyDateTimeRange When => this[when] as IFuzzyDateTimeRange;

        public ICitation Citation => this[citation] as ICitation;

        public IAudit Audit => this[audit] as IAudit;

        public override string ToString()
        {
            return $"{this.What.ShortName}:{this.Where.ShortName}:{this.When.ShortName}";
        }
    }
}
