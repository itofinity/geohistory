using System;

namespace Uk.Co.Itofinity.GeoHistory.Model
{
    public abstract class AbstractNamedEntity : INamed, ITyped
    {
        public AbstractNamedEntity(string name, string type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public string Name { get; }

        public string ShortName => Name;    // TODO is this correct?

        public string Type { get; }
    }
}
