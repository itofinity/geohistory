using System.Collections.Generic;
using System.Globalization;
using Uk.Co.Itofinity.Geohistory.Model.Organisation;
using Uk.Co.Itofinity.Geohistory.Model.People;
using Uk.Co.Itofinity.Geohistory.Model.Role;

namespace Uk.Co.Itofinity.Geohistory.Model
{
    public interface IOrganisation : IEntity
    {
        string Size { get; }

        List<TemporalRole> Personel { get; }

        void AddPersonel(TemporalRole appointment);

        List<TemporalRole> Purposes { get; }

        void AddPurpose(TemporalRole appointment);
    }
}