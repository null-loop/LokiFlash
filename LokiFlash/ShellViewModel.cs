using Caliburn.Micro;

namespace LokiFlash {

    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell 
    {
        public ShellViewModel(PictureToWordViewModel wordToPictureViewModel, SingleWordListViewModel singleWordListViewModel)
        {
            Items.Add(wordToPictureViewModel);
            Items.Add(singleWordListViewModel);

            ActivateItem(wordToPictureViewModel);
        }
    }
}
