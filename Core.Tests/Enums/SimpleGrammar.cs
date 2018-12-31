namespace Core.Tests.Enums
{
    public static class SimpleGrammar
    {
        public enum NonTerminals
        {
            E,
            T
        }

        public enum Terminals
        {
            Id,
            Plus,
            Epsilon,
            Eof
        }
    }
}