using Autofac;
using GymRegistrator.DataAccess;
using GymRegistrator.UI.Data.Lookups;
using GymRegistrator.UI.Data.Repositories;
using GymRegistrator.UI.View.Services;
using GymRegistrator.UI.ViewModel;
using Prism.Events;

namespace GymRegistrator.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<GymRegistratorDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<GymClientDetailViewModel>().As<IGymClientDetailViewModel>();
            
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<GymClientRepository>().As<IGymClientRepository>();

            return builder.Build();
        }
    }
}
