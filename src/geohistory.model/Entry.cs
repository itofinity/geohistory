using System.Collections.Generic;
using Uk.Co.Itofinity.Geohistory.Model.Audit;
using Uk.Co.Itofinity.Geohistory.Model.Citation;
using Uk.Co.Itofinity.Geohistory.Model.Location;
using Uk.Co.Itofinity.Geohistory.Model.Time;

namespace Uk.Co.Itofinity.Geohistory.Model
{
    public class Entry : Dictionary<string, object>
    {
        private const string what = "what";
        private const string where = "where";
        private const string when = "when";
        private const string citation = "citation";
        private const string audit = "audit";

        public Entry(IOrganisation what, ILocation where, ITemporal when, ICitation citation, IAudit audit) : base()
        {
            this[Entry.what] = what;
            this[Entry.where] = where;
            this[Entry.when] = when;
            this[Entry.citation] = citation;
            this[Entry.audit] = audit;
        }

        public IOrganisation What => this[what] as IOrganisation;

        public ILocation Where => this[where] as ILocation;

        public ITemporal When => this[when] as ITemporal;
    }
}
