using System.Linq;
using System.Text;

namespace tinkerpop.scripts
{
    public class ScriptBuilder
    {
        private StringBuilder _builder = new StringBuilder();

        private ScriptBuilder(string context)
        {
            _builder.Append(context);
        }

        public static ScriptBuilder G
        {
            get
            {
                return g;
            }
        }

        public static ScriptBuilder g
        {
            get
            {
                return new ScriptBuilder("g");
            }
        }

        public ScriptBuilder V()
        {
            _builder.Append(".V()");
            return this;
        }

        public ScriptBuilder V(string label)
        {
            _builder.Append($".V('{label}')");
            return this;
        }

        public ScriptBuilder E()
        {
            _builder.Append(".E()");
            return this;
        }

        public ScriptBuilder E(string label)
        {
            _builder.Append($".E('{label}')");
            return this;
        }

        public ScriptBuilder drop()
        {
            _builder.Append(".drop()");
            return this;
        }

        public ScriptBuilder Drop()
        {
            return drop();
        }

        public string Build()
        {
            return _builder.ToString();
        }

        public ScriptBuilder AddV(string label)
        {
            return addV(label);
        }

        public ScriptBuilder addV(string label)
        {
            _builder.Append($".addV('{label}')");
            return this;
        }

        public ScriptBuilder property(string key, object value)
        {
            var normVal = GetNormalizedValue(value);
            _builder.Append($".property('{key}', {normVal})");
            return this;
        }

        private string GetNormalizedValue(object value)
        {
            if (value is int
                || value is long
                || value is double
                || value is float
                || value is bool)
            {
                return value.ToString();
            }

            return $"'{value}'";
        }

        public override string ToString()
        {
            return Build();
        }

        public ScriptBuilder AddE(string label)
        {
            return addV(label);
        }

        public ScriptBuilder addE(string label)
        {
            _builder.Append($".addE('{label}')");
            return this;
        }

        public ScriptBuilder To(string target)
        {
            return to(target);
        }

        public ScriptBuilder to(string target)
        {
            _builder.Append($".to({target})");
            return this;
        }

        public ScriptBuilder ToID(string id)
        {
            return toID(id);
        }

        public ScriptBuilder toID(string id)
        {
            _builder.Append($".to(g.V('{id}'))");
            return this;
        }

        public ScriptBuilder Count()
        {
            return count();
        }

        public ScriptBuilder count()
        {
            _builder.Append($".count()");
            return this;
        }

        public ScriptBuilder hasLabel(string label)
        {
            _builder.Append($".hasLabel('{label}')");
            return this;
        }

        public ScriptBuilder has(string property, string clause)
        {
            return hasClause(property, clause);
        }

        public ScriptBuilder hasClause(string property, string clause)
        {
            _builder.Append($".{ScriptClauses.hasClause(property, clause)}");
            return this;
        }

        public ScriptBuilder hasValue(string property, string value)
        {
            _builder.Append($".{ScriptClauses.hasValue(property, value)}");
            return this;
        }

        public ScriptBuilder values(params string[] properties)
        {
            var args = properties.ToList().Select(p => $"'{p}'");
            _builder.Append($".values({string.Join(",", args)})");
            return this;
        }

        public ScriptBuilder order()
        {
            _builder.Append($".order()");
            return this;
        }

        public ScriptBuilder @out(string edge)
        {
            _builder.Append($".out('{edge}')");
            return this;
        }

        public ScriptBuilder repeat(string clause)
        {
            _builder.Append($".repeat({clause})");
            return this;
        }

        public ScriptBuilder outE(string edge)
        {
            _builder.Append($".outE('{edge}')");
            return this;
        }

        public ScriptBuilder where(string clause)
        {
            _builder.Append($".where({clause})");
            return this;
        }

        public ScriptBuilder where(ScriptBuilder clause)
        {
            _builder.Append(clause.Build());
            return this;
        }

        public ScriptBuilder by(string property, string clause)
        {
            _builder.Append($".by('{property}', {clause})");
            return this;
        }

        public ScriptBuilder until(string clause)
        {
            _builder.Append($".until({clause})");
            return this;
        }

        public ScriptBuilder path()
        {
            _builder.Append($".path()");
            return this;
        }

        public static ScriptBuilder inV()
        {
            return new ScriptBuilder("inV()");
        }
    }
}
