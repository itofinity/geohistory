using Uk.Co.Itofinity.GeoHistory.Model.Audit;
using Uk.Co.Itofinity.GeoHistory.Model.Citation;
using Uk.Co.Itofinity.GeoHistory.Model.Time;

namespace Uk.Co.Itofinity.GeoHistory.Model.Organisation
{
    public class TemporalChainOfCommand : AbstractTemporal
    {

        public TemporalChainOfCommand(IOrganisation superior, IOrganisation inferior, FuzzyDateRange fuzzyDateRange, ICitation citation, IAudit audit) : base(fuzzyDateRange.StartDateTime, fuzzyDateRange.EndDateTime, citation, audit)
        {
            this.Superior = superior;
            this.Inferior = inferior;
        }

        public IOrganisation Superior { get; }
        public IOrganisation Inferior { get; }

        public override string Name => ToString();

        public override string ShortName => $"{Superior.ShortName}-{Inferior.ShortName}/{StartDateTime.DateTime.ToString(StartDateTime.Format)}";


        public override string ToString()
        {
            return $"{Superior.Name}-{Inferior.Name}/{StartDateTime.DateTime.ToString(StartDateTime.Format)}";
        }
    }
}