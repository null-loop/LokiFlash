using System;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using LokiFlash.Core;

namespace LokiFlash
{
    public class WordViewModel : PropertyChangedBase
    {
        public IWord Word { get; set; }
        public string Text { get { return Word.Display; } }
        public ImageSource Picture { get { return new BitmapImage(new Uri(Word.PicturePath.FullName, UriKind.Absolute)); } }

        public WordViewModel(IWord word)
        {
            Word = word;
        }
    }

    public class MatchingWordViewModel : PropertyChangedBase
    {
        public IWord Word { get; set; }
        public IMatch<IWord> Parent { get; set; }

        public MatchingWordViewModel(IWord word, IMatch<IWord> parent)
        {
            Word = word;
            Parent = parent;
        }
    }
}
