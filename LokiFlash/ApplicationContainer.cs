using System.IO;
using System.Linq;
using Caliburn.Micro;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using LokiFlash.Core;
using LokiFlash.wordlists;

namespace LokiFlash
{
    public class ApplicationContainer : WindsorContainer
    {
        public ApplicationContainer()
        {
            var tracker = new XmlScoreTracker();

            Register(Component.For<IScoreTracker>().Instance(tracker));
            Register(Component.For<PictureToWordViewModel>().Instance(new PictureToWordViewModel(BuiltInLists.DefaultPictures, tracker)));
            Register(Component.For<SingleWordListViewModel>().Instance(new SingleWordListViewModel(BuiltInLists.LokiFirst)));

            Register(
                Component.For<IWindowManager>().Instance(new WindowManager()),
                Component.For<IEventAggregator>().Instance(new EventAggregator()),
                Component.For<IShell>().ImplementedBy<ShellViewModel>()
                );

            //RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            Register(AllTypes.FromAssembly(GetType().Assembly)
                         .Where(x => x.Namespace.EndsWith("ViewModels") || x.Name.EndsWith("ViewModel"))
                         .Configure(x => x.LifeStyle.Is(LifestyleType.Singleton)));
        }
    }

}