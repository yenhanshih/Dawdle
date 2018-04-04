using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Dawdle.Client.Views
{
    public partial class VideoView
    {
        public VideoView()
        {
            InitializeComponent();
        }

        private void VideoView_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            PlayerInterfaceControl.BeginAnimation(OpacityProperty, animation);
        }

        private void VideoView_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation(1, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            PlayerInterfaceControl.BeginAnimation(OpacityProperty, animation);
        }

        private void VideoView_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
