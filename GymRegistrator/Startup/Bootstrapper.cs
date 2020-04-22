using Autofac;
using GymRegistrator.DataAccess;
using GymRegistrator.UI.Data;
using GymRegistrator.UI.ViewModel;

namespace GymRegistrator.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GymRegistratorDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<GymClientService>().As<IGymClientService>();

            return builder.Build();
        }
    }
}
