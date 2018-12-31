using System.Collections.Immutable;

namespace Core.Models.Interfaces
{
    public interface ISlrParser
    {
        ImmutableList<IState> BuildStates();
    }
}