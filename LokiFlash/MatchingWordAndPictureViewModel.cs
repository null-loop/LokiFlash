using LokiFlash.Core;

namespace LokiFlash
{
    public class MatchingWordAndPictureViewModel : WordViewModel
    {
        public IMatch<IWord> Parent { get; set; }

        public MatchingWordAndPictureViewModel(IWord wordAndPicture, IMatch<IWord> parent)
            : base(wordAndPicture)
        {
            Parent = parent;
        }

        public void Match()
        {
            if (!Parent.IsMatch(Word)) IsEnabled = false;
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    NotifyOfPropertyChange(() => IsEnabled);
                }
            }
        }
    }

    public interface IMatch<T>
    {
        bool IsMatch(T wordAndPicture);
    }
}