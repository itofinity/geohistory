using Uk.Co.Itofinity.GeoHistory.Model.People;

namespace Uk.Co.Itofinity.GeoHistory.Model.Role.Military
{
    public abstract class AbstractMilitaryAppointment : IAppointment
    {
        protected AbstractMilitaryAppointment(IOrganisation organisation, Person person)
        {
            Organisation = organisation;
            this.Person = person;
        }

        public IOrganisation Organisation { get; }
        public Person Person { get; }
        public abstract string Name { get; }
        public abstract string ShortName { get; }
        public abstract double ControlFactor { get; }
        public abstract double InfluenceFactor { get; }
    }
}