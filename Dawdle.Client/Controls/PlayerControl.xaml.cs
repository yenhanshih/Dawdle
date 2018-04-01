using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Vlc.DotNet.Core;

namespace Dawdle.Client.Controls
{
    public partial class PlayerControl
    {
        private static bool _isDraggingSlider;

        public static readonly DependencyProperty VideoUriProperty = DependencyProperty.Register("VideoUri", typeof(Uri), typeof(PlayerControl), new PropertyMetadata(VideoUriChanged));

        public Uri VideoUri
        {
            get => (Uri)GetValue(VideoUriProperty);
            set => SetValue(VideoUriProperty, value);
        }

        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register("IsPlaying", typeof(bool), typeof(PlayerControl), new PropertyMetadata(IsPlayingChanged));

        public bool IsPlaying
        {
            get => (bool)GetValue(IsPlayingProperty);
            set
            {
                Dispatcher.Invoke(() =>
                {
                    PlayPauseButton.Icon = value ? (UIElement)FindResource("PauseIcon") : (UIElement)FindResource("PlayIcon");
                    SetValue(IsPlayingProperty, value);
                });
            }
        }

        public PlayerControl()
        {
            InitializeComponent();

            // Choose x86 or x64 library
            var vlcDirectoryPath = Path.Combine(Environment.CurrentDirectory, IntPtr.Size == 4 ? "../../../packages/VideoLAN.LibVLC.Windows.3.0.0-alpha/build/x86" : "../../../packages/VideoLAN.LibVLC.Windows.3.0.0-alpha/build/x64");
            var vlcLibDirectory = new DirectoryInfo(vlcDirectoryPath);
            VlcControl.SourceProvider.CreatePlayer(vlcLibDirectory);

            VlcControl.SourceProvider.MediaPlayer.TimeChanged += VlcPlayerOnTimeChanged;
            VlcControl.SourceProvider.MediaPlayer.LengthChanged += VlcPlayerOnLengthChanged;
        }

        private static void VideoUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = (PlayerControl)d;

            ThreadPool.QueueUserWorkItem(i =>
            {
                player.VlcControl.SourceProvider.MediaPlayer.Play((Uri)e.NewValue);
            });
        }

        private static void IsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = (PlayerControl)d;

            if ((bool)e.NewValue)
            {
                ThreadPool.QueueUserWorkItem(i =>
                {
                    player.VlcControl.SourceProvider.MediaPlayer.Play();
                });
            }
            else
            {
                player.VlcControl.SourceProvider.MediaPlayer.Pause();
            }
        }

        private void VlcPlayerOnTimeChanged(object sender, EventArgs e)
        {
            if (!_isDraggingSlider)
            {
                var time = ((VlcMediaPlayer)sender).Time;

                Dispatcher.Invoke(() =>
                {
                    CurrentTime.Text = TimeSpan.FromMilliseconds(time).ToString("hh\\:mm\\:ss");
                    PlayerProgressBar.Value = time;
                });
            }
        }

        private void VlcPlayerOnLengthChanged(object sender, EventArgs e)
        {
            var length = ((VlcMediaPlayer)sender).Length;

            Dispatcher.Invoke(() =>
            {
                CurrentLength.Text = TimeSpan.FromMilliseconds(length).ToString("hh\\:mm\\:ss");
                PlayerProgressBar.Minimum = 0;
                PlayerProgressBar.Maximum = length;
            });
        }

        private void PlayerControl_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation(0, 0.8, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            PlayerControlInterface.BeginAnimation(OpacityProperty, animation);
        }

        private void PlayerControl_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation(0.8, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            PlayerControlInterface.BeginAnimation(OpacityProperty, animation);
        }

        private void PlayerProgressBar_OnDragStarted(object sender, DragStartedEventArgs e)
        {
            _isDraggingSlider = true;
        }

        private void PlayerProgressBar_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            CurrentTime.Text = TimeSpan.FromMilliseconds(PlayerProgressBar.Value).ToString("hh\\:mm\\:ss");
        }

        private void PlayerProgressBar_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            VlcControl.SourceProvider.MediaPlayer.Time = Convert.ToInt64(PlayerProgressBar.Value);

            _isDraggingSlider = false;
        }

        private void PlayPauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (VlcControl.SourceProvider.MediaPlayer.IsPlaying())
            {
                IsPlaying = false;
            }

            if (!VlcControl.SourceProvider.MediaPlayer.IsPlaying())
            {
                IsPlaying = true;
            }
        }
    }
}
