using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model.Role;

namespace Uk.Co.Itofinity.GeoHistory.Model
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