using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Dawdle.Client.ViewModels;

namespace Dawdle.Client.Views
{
    public partial class MenuView
    {
        private bool Expanded { get; set; }

        public MenuView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (Expanded)
            {
                var animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
                animation.Completed += (s, _) => Expanded = false;
                SearchTextBox.BeginAnimation(WidthProperty, animation);
            }
            else
            {
                var animation = new DoubleAnimation(400, TimeSpan.FromSeconds(0.3));
                animation.Completed += (s, _) => Expanded = true;
                SearchTextBox.BeginAnimation(WidthProperty, animation);
            }

            SearchTextBox.Focus();
        }

        private void SearchTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonBase_OnClick(sender, e);

                var viewModel = (MenuViewModel)DataContext;
                if (viewModel.Search.CanExecute(SearchTextBox.Text))
                {
                    viewModel.Search.Execute(SearchTextBox.Text);
                }
            }
        }
    }
}
