using System.Windows;
using System.Windows.Controls;

namespace Dawdle.Client.Controls.Buttons
{
    public class IconRadioButton : RadioButton
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(UIElement), typeof(IconRadioButton), new PropertyMetadata(null));

        public UIElement Icon
        {
            get => (UIElement)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
    }
}