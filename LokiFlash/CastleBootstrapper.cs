using LokiFlash.Core;

namespace LokiFlash
{
	using System;
	using System.Collections.Generic;
	using Caliburn.Micro;

    public class CastleBootstrapper : Bootstrapper<IShell>
    {
        private ApplicationContainer container;

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            // start scoring sessions
            container.Resolve<IScoreTracker>().StartSession();

            base.OnStartup(sender, e);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            // stop scoring sessions
            container.Resolve<IScoreTracker>().EndSession();

            base.OnExit(sender, e);
        }

        protected override void Configure()
        {
            container = new ApplicationContainer();
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key) ? container.Resolve(service) : container.Resolve(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return (IEnumerable<object>) container.ResolveAll(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}


