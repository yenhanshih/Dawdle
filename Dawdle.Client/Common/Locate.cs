using System;
using System.Windows.Markup;
using Dawdle.Client.ViewModels.Interfaces;
using LightInject;

namespace Dawdle.Client.Common
{
    [MarkupExtensionReturnType(typeof(Locate))]
    public sealed class Locate
        : MarkupExtension
    {
        private static readonly Locate Locator = new Locate();
        private readonly IServiceFactory _container;

        public Locate()
        {
            _container = Ioc.Container;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Locator;
        }

        public IMainViewModel MainViewModel
        {
            get { return _container.GetInstance<IMainViewModel>(); }
        }

        public IHomeViewModel HomeViewModel
        {
            get { return _container.GetInstance<IHomeViewModel>(); }
        }

        public IVideoViewModel VideoViewModel
        {
            get { return _container.GetInstance<IVideoViewModel>(); }
        }

        public IMenuViewModel MenuViewModel
        {
            get { return _container.GetInstance<IMenuViewModel>(); }
        }
    }
}