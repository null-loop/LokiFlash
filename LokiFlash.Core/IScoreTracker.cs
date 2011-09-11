namespace LokiFlash.Core
{
    public interface IScoreTracker
    {
        void RecordGameResult(string type, string word, int attempts);
        void StartSession();
        void EndSession();
    }
}