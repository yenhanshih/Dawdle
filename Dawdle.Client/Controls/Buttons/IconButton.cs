using System.Windows;
using System.Windows.Controls;

namespace Dawdle.Client.Controls.Buttons
{
    public class IconButton : RadioButton
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(UIElement), typeof(IconButton), new PropertyMetadata(null));

        public UIElement Icon
        {
            get { return (UIElement)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}