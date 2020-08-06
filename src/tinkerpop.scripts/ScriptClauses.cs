namespace tinkerpop.scripts
{
    public static class ScriptClauses
    {
        public static string gt(int threshold) => $"gt({threshold})";
        public static string gt(long threshold) => $"gt({threshold})";

        public static string lt(int threshold) => $"lt({threshold})";
        public static string lt(long threshold) => $"lt({threshold})";

        public static string @out()
        {
            return "out()";
        }

        public static string hasClause(string property, string clause) => $"has('{property}', {clause})";

        public static string hasValue(string property, string value) => $"has('{property}', '{value}')";

        public static string has(string property, string value) => hasValue(property, value);

        public static string decr => "decr";
    }
}
