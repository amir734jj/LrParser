namespace Core.Tests.Enums
{
    public static class NumericEnums
    {
        public enum NonTerminals
        {
            Root,
            E
        }

        public enum Terminals
        {
            Number,
            Plus,
            Multiply,
            Epsilon,
            Eof
        }
    }
}