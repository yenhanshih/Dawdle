using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Meta.Vlc.Wpf;

namespace Dawdle.Client.Controls
{
    public partial class YoutubePlayer
    {
        private static bool _isMediaLoaded;
        private static bool _isDraggingSlider;

        public static readonly DependencyProperty VideoUriProperty = DependencyProperty.Register("VideoUri", typeof(Uri), typeof(YoutubePlayer), new PropertyMetadata(VideoUriChanged));

        public Uri VideoUri
        {
            get { return (Uri)GetValue(VideoUriProperty); }
            set { SetValue(VideoUriProperty, value); }
        }

        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register("IsPlaying", typeof(bool), typeof(YoutubePlayer), new PropertyMetadata(IsPlayingChanged));

        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        public YoutubePlayer()
        {
            InitializeComponent();

            VlcPlayer.TimeChanged += VlcPlayerOnTimeChanged;
            VlcPlayer.LengthChanged += VlcPlayerOnLengthChanged;
        }

        private void VlcPlayerOnTimeChanged(object sender, EventArgs e)
        {
            if (!_isDraggingSlider)
            {
                var time = ((VlcPlayer)sender).Time;
                CurrentTime.Text = time.ToString("hh\\:mm\\:ss");
                PlayerProgressBar.Value = time.TotalSeconds;
            }
        }

        private void VlcPlayerOnLengthChanged(object sender, EventArgs e)
        {
            var length = ((VlcPlayer)sender).Length;
            CurrentLength.Text = length.ToString("hh\\:mm\\:ss");
            PlayerProgressBar.Minimum = 0;
            PlayerProgressBar.Maximum = length.TotalSeconds;
        }

        private static void VideoUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = (YoutubePlayer)d;
            if (_isMediaLoaded)
            {
                player.VlcPlayer.Stop();
                _isMediaLoaded = false;
            }
            player.VlcPlayer.LoadMedia((Uri)e.NewValue);
            _isMediaLoaded = true;
        }

        private static void IsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = (YoutubePlayer)d;
            if ((bool) e.NewValue)
            {
                player.VlcPlayer.Play();
            }
            else
            {
                player.VlcPlayer.Pause();
            }
        }
        private void YoutubePlayer_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation(0, 0.8, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            PlayerControls.BeginAnimation(OpacityProperty, animation);
        }

        private void YoutubePlayer_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation(0.8, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            PlayerControls.BeginAnimation(OpacityProperty, animation);
        }

        private void PlayerProgressBar_OnDragStarted(object sender, DragStartedEventArgs e)
        {
            _isDraggingSlider = true;
        }

        private void PlayerProgressBar_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            CurrentTime.Text = TimeSpan.FromSeconds(PlayerProgressBar.Value).ToString("hh\\:mm\\:ss");
        }

        private void PlayerProgressBar_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            VlcPlayer.Time = TimeSpan.FromSeconds(PlayerProgressBar.Value);
            _isDraggingSlider = false;
        }
    }
}
