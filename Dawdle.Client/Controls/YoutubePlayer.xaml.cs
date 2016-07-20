using System;
using System.Windows;

namespace Dawdle.Client.Controls
{
    public partial class YoutubePlayer
    {
        private static bool _isMediaLoaded;

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
    }
}
