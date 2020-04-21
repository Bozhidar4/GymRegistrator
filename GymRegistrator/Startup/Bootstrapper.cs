using Autofac;
using GymRegistrator.Data;
using GymRegistrator.ViewModel;

namespace GymRegistrator.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<GymClientService>().As<IGymClientService>();

            return builder.Build();
        }
    }
}
