using System;
using System.CommandLine;
using System.Linq;

namespace UK.CO.Geohistory.Client.Commands.Api
{
    public class HierarchicalOption<T> : Option<T>, IHierarchicalOption
    {
        public HierarchicalOption(string parent, string alias, string description = null) : this(parent, alias, () => { return default(T); }, description)
        {;
        }

        public HierarchicalOption(string parent, string[] aliases, string description = null) : this(parent, aliases, () => { return default(T); }, description)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public HierarchicalOption(string parent, string alias, Func<T> getDefaultValue, string description = null) : base(alias, getDefaultValue, description)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public HierarchicalOption(string parent, string[] aliases, Func<T> getDefaultValue, string description = null) : base(aliases, getDefaultValue, description)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public T Precedent 
        { 
            get 
            {
                var envVarvalue = System.Environment.GetEnvironmentVariable(GetEnvName());
                if (!string.IsNullOrWhiteSpace(envVarvalue))
                {
                    return (T)Convert.ChangeType(envVarvalue, typeof(T));
                }

                // TODO check config file.

                return default(T);
            } 
        }

        public T GetPrecedent(T current)
        {
            if (current != null)
            {
                return current;
            }

            var precedent = Precedent;
            if (precedent != null)
            {
                return precedent;
            }
            

            throw new Exception($"{Aliases.FirstOrDefault()} is undefined");
        }

        public string Parent { get; }

        private string GetEnvName()
        {
            var primaryAlias = this.Aliases.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(primaryAlias))
            {
                throw new Exception("ëh?");
            }
            var key = $"{Parent}_{primaryAlias}".ToUpper();
            return key;
        }
    }
}
