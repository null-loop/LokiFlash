using Caliburn.Micro;
using LokiFlash.Core;

namespace LokiFlash
{
    public class WordToPictureViewModel : Screen
    {
        public IWordsList WordList { get; set; }

        public WordToPictureViewModel(IWordsList list)
        {
            WordList = list;
        }
    }
}