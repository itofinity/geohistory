using System;
using System.Collections.Generic;
using System.Web;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Role
{
    public abstract class AbstractPersonalRole : IPersonalRole
    {
        public AbstractPersonalRole(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public string ShortName => Name; // TODO is this right?

        public string Id => HttpUtility.UrlEncode(Name);
    }
}
