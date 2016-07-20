using System.Windows;

namespace Dawdle.Client
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });

            new Views.MainView().Show();
        }
    }
}
