using System.Windows.Input;

namespace Dawdle.Client.Views
{
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void MainView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }
    }
}
