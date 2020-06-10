using Uk.Co.Itofinity.Geohistory.Model.Time;

namespace Uk.Co.Itofinity.Geohistory.Model.Organisation
{
    public class TemporalChainOfCommand : AbstractTemporal
    {
        public TemporalChainOfCommand(IOrganisation organisation, IFuzzyDateTime startDateTime) : base(startDateTime, null)
        {
            Organisation = organisation;
        }

        public IOrganisation Organisation { get; }
    }
}