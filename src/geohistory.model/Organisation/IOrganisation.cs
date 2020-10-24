using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Role;

namespace UK.CO.Itofinity.GeoHistory.Model
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