using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using LokiFlash.Core;

namespace LokiFlash
{
    public class PictureToWordViewModel : Screen, IMatch<IWord>
    {
        public IWordsList WordList { get; set; }
        public IScoreTracker ScoreTracker { get; set; }

        private WordViewModel _target;
        public WordViewModel Target
        {
            get { return _target; }
            set
            {
                if (value != _target)
                {
                    _target = value;
                    NotifyOfPropertyChange(()=>Target);
                }
            }
        }

        private IEnumerable<MatchingWordAndPictureViewModel> _possibleAnswers;
        public IEnumerable<MatchingWordAndPictureViewModel> PossibleAnswers
        {
            get { return _possibleAnswers; }
            set
            {
                if (value != _possibleAnswers)
                {
                    _possibleAnswers = value;
                    NotifyOfPropertyChange(()=>PossibleAnswers);
                }
            }
        }

        private int _maximumWords = 3;
        public int MaximumWords
        {
            get { return _maximumWords; }
            set
            {
                if (value != _maximumWords)
                {
                    _maximumWords = value;
                    NotifyOfPropertyChange(() => MaximumWords);
                    Next();
                }
            }
        }

        private bool _matchFirstLetter;
        public bool MatchFirstLetter
        {
            get { return _matchFirstLetter; }
            set
            {
                if (value != _matchFirstLetter)
                {
                    _matchFirstLetter = value;
                    NotifyOfPropertyChange(()=>MatchFirstLetter);
                    Next();
                }
            }
        }

        private string _lastWord = string.Empty;
        private List<string> _wordHistory = new List<string>();

        private Random _random = new Random();

        public PictureToWordViewModel(IWordsList source, IScoreTracker scoreTracker)
        {
            DisplayName = "Picture To Word";
            WordList = source;
            ScoreTracker = scoreTracker;

            ChangeState();
        }

        public void Next()
        {
            ChangeState();
        }

        private void ChangeState()
        {
            if (_wordHistory.Count() == WordList.AllItems.Count())
                _wordHistory.Clear();

            // pick the picture and word to show
            var possibleAnswers = new List<IWord>();

            var allUnused = WordList.AllItems.Where(i => !_wordHistory.Contains(i.Display) && i.Display!=_lastWord).ToList();
            var picked = allUnused.ElementAt(_random.Next(allUnused.Count()));

            // pick extra words
            var needCount = MaximumWords - 1;

            allUnused = WordList.AllItems.ToList();
            allUnused.Remove(picked);
            possibleAnswers.Add(picked);

            while(needCount != 0)
            {
                var thisSource = allUnused.ToArray();

                if (MatchFirstLetter)
                {
                    thisSource = thisSource.Where(u => u.Display.StartsWith(picked.Display[0].ToString())).ToArray();

                    if (thisSource.Count() == 0)
                    {
                        thisSource = allUnused.ToArray();
                    }
                }

                var p = thisSource.ElementAt(_random.Next(thisSource.Count()));

                possibleAnswers.Add(p);
                allUnused.Remove(p);

                needCount--;
            }

            possibleAnswers = ShuffleWords(possibleAnswers);

            // setup state
            _lastWord = picked.Display;
            _wordHistory.Add(_lastWord);

            Target = new WordViewModel(picked);
            PossibleAnswers = possibleAnswers.Select(w => new MatchingWordAndPictureViewModel(w, this));
        }

        private List<IWord> ShuffleWords(List<IWord> words)
        {
            // generate new random order

            var newList = new List<IWord>();

            while(words.Count != 0)
            {
                var n = words.ElementAt(_random.Next(words.Count));
                newList.Add(n);
                words.Remove(n);
            }

            return newList;
        }

        private int _attempts = 0;

        public bool IsMatch(IWord wordAndPicture)
        {
            var m = wordAndPicture.Display == Target.Word.Display;

            _attempts++;

            if (m)
            {
                ScoreTracker.RecordGameResult("PictureToWord", Target.Word.Display, _attempts);
                _attempts = 0;
                Next();
            }

            return m;
        }
    }
}