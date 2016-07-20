using Dawdle.Client.ViewModels;
using Dawdle.Client.ViewModels.Interfaces;
using LightInject;

namespace Dawdle.Client.Common
{
    public static class Ioc
    {
        public static IServiceFactory Container { get; private set; }

        static Ioc()
        {
            Container = RegisterTypes();
        }

        private static ServiceContainer RegisterTypes()
        {
            var container = new ServiceContainer();

            container.Register<IHomeViewModel, HomeViewModel>(new PerContainerLifetime());
            container.Register<IVideoViewModel, VideoViewModel>(new PerContainerLifetime());
            container.Register<IMenuViewModel, MenuViewModel>(new PerContainerLifetime());
            container.Register<IMainViewModel, MainViewModel>(new PerContainerLifetime());

            return container;
        }
    }
}