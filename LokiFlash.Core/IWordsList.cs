using System.Collections.Generic;

namespace LokiFlash.Core
{
    public interface IWordsList
    {
        string Name { get; }
        IEnumerable<IWord> AllItems { get; }
    }
}