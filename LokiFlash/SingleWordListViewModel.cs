using System.Collections.Generic;
using Caliburn.Micro;
using LokiFlash.Core;

namespace LokiFlash
{
    public class SingleWordListViewModel : Screen
    {
        public IEnumerable<IWord> Source { get; set; }
        public Queue<IWord> Queue { get; set; }

        private WordViewModel _word;
        public WordViewModel Word
        {
            get { return _word; }
            set
            {
                if (value != _word)
                {
                    _word = value;
                    NotifyOfPropertyChange(()=>Word);
                }
            }
        }

        private bool _randomOrder;
        public bool RandomOrder
        {
            get { return _randomOrder; }
            set
            {
                if (value != _randomOrder)
                {
                    _randomOrder = value;
                    NotifyOfPropertyChange(()=>RandomOrder);
                }
            }
        }

        public SingleWordListViewModel(IWordsList list)
        {
            DisplayName = "Single words";
            Source = list.AllItems;
            Next();
            // build list
        }

        //TODO:Measure time to complete, include in scores - plus detail of difficulty...

        public void Next()
        {
            if (Queue==null || Queue.Count == 0)
            {
                Queue = new Queue<IWord>(Source);
            }

            Word = new WordViewModel(Queue.Dequeue());
        }
    }
}