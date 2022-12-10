using System;

namespace Uk.Co.Itofinity.GeoHistory.Model
{
    public abstract class AbstractEntity : ITyped
    {
        public AbstractEntity(string type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public string Type { get; }
    }
}
