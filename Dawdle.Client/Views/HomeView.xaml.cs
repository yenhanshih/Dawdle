using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using MyToolkit.UI;

namespace Dawdle.Client.Views
{
    public partial class HomeView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void HomeView_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var scrollBar = VideoListView.GetVisualDescendants().First(d => d.GetType() == typeof(ScrollBar)) as ScrollBar;
            var animation = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            scrollBar?.BeginAnimation(OpacityProperty, animation);
        }

        private void HomeView_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var scrollBar = VideoListView.GetVisualDescendants().First(d => d.GetType() == typeof(ScrollBar)) as ScrollBar;
            var animation = new DoubleAnimation(1, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            scrollBar?.BeginAnimation(OpacityProperty, animation);
        }
    }
}
