using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Dawdle.Client.Controls
{
    public partial class PlayerInterfaceControl
    {
        private static bool _draggingSlider;
        private static bool _playing;

        public static readonly DependencyProperty PlayingInputProperty = DependencyProperty.Register("PlayingInput", typeof(bool), typeof(PlayerInterfaceControl), new PropertyMetadata(false, null, PlayingInputted));

        public bool PlayingInput
        {
            set => SetValue(PlayingInputProperty, value);
        }

        public static readonly DependencyProperty PlayingOutputProperty = DependencyProperty.Register("PlayingOutput", typeof(bool), typeof(PlayerInterfaceControl));

        public bool PlayingOutput
        {
            get => (bool)GetValue(PlayingOutputProperty);
            set => SetValue(PlayingOutputProperty, value);
        }

        public static readonly DependencyProperty TimeInputProperty = DependencyProperty.Register("TimeInput", typeof(long), typeof(PlayerInterfaceControl), new PropertyMetadata((long)0, null, TimeInputted));

        public long TimeInput
        {
            set => SetValue(TimeInputProperty, value);
        }

        public static readonly DependencyProperty TimeOutputProperty = DependencyProperty.Register("TimeOutput", typeof(long), typeof(PlayerInterfaceControl));

        public long TimeOutput
        {
            get => (long)GetValue(TimeOutputProperty);
            set => SetValue(TimeOutputProperty, value);
        }

        public static readonly DependencyProperty LengthInputProperty = DependencyProperty.Register("LengthInput", typeof(long), typeof(PlayerInterfaceControl), new PropertyMetadata((long)0, null, LengthInputted));

        public long LengthInput
        {
            set => SetValue(LengthInputProperty, value);
        }

        public static readonly DependencyProperty EndReachedInputProperty = DependencyProperty.Register("EndReachedInput", typeof(long), typeof(PlayerInterfaceControl), new PropertyMetadata((long)0, null, EndReachedInputtedCoerced));

        public long EndReachedInput
        {
            set => SetValue(EndReachedInputProperty, value);
        }

        public PlayerInterfaceControl()
        {
            InitializeComponent();
        }

        private void ProgressBar_OnDragStarted(object sender, DragStartedEventArgs e)
        {
            _draggingSlider = true;
        }

        private void ProgressBar_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            TimeOutput = Convert.ToInt64(ProgressBar.Value);
            _draggingSlider = false;
        }

        private void ProgressBar_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            CurrentTime.Text = TimeSpan.FromMilliseconds(ProgressBar.Value).ToString("hh\\:mm\\:ss");
        }

        private void PlayPauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_playing)
            {
                _playing = false;
                PlayPauseButton.Icon = (UIElement)FindResource("PlayIcon");
                PlayingOutput = false;
            }
            else
            {
                _playing = true;
                PlayPauseButton.Icon = (UIElement)FindResource("PauseIcon");
                PlayingOutput = true;
            }
        }

        private static object PlayingInputted(DependencyObject d, object baseValue)
        {
            _playing = (bool)baseValue;

            return baseValue;
        }

        private static object TimeInputted(DependencyObject d, object baseValue)
        {
            var playerInterfaceControl = (PlayerInterfaceControl)d;

            if (!_draggingSlider)
            {
                playerInterfaceControl.ProgressBar.Value = (long)baseValue;
                playerInterfaceControl.CurrentTime.Text = TimeSpan.FromMilliseconds((long)baseValue).ToString("hh\\:mm\\:ss");
            }

            return baseValue;
        }

        private static object LengthInputted(DependencyObject d, object baseValue)
        {
            var playerInterfaceControl = (PlayerInterfaceControl)d;
            playerInterfaceControl.ProgressBar.Maximum = (long)baseValue;
            playerInterfaceControl.CurrentLength.Text = TimeSpan.FromMilliseconds((long)baseValue).ToString("hh\\:mm\\:ss");

            return baseValue;
        }

        private static object EndReachedInputtedCoerced(DependencyObject d, object basevalue)
        {
            var playerInterfaceControl = (PlayerInterfaceControl)d;
            playerInterfaceControl.ProgressBar.Value = (long)basevalue;
            playerInterfaceControl.CurrentTime.Text = TimeSpan.FromMilliseconds((long)basevalue).ToString("hh\\:mm\\:ss");

            return basevalue;
        }
    }
}
