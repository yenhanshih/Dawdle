using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Dawdle.Client.Views
{
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void MainVew_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation(0, 0.8, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            MenuView.BeginAnimation(OpacityProperty, animation);
        }

        private void MainView_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation(0.8, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            MenuView.BeginAnimation(OpacityProperty, animation);
        }

        private void MainView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
