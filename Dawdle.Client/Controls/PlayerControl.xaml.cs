using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Vlc.DotNet.Core;

namespace Dawdle.Client.Controls
{
    public partial class PlayerControl
    {
        public static readonly DependencyProperty UriInputProperty = DependencyProperty.Register("UriInput", typeof(Uri), typeof(PlayerControl), new PropertyMetadata(null, null, UriInputted));

        public Uri UriInput
        {
            set => SetValue(UriInputProperty, value);
        }

        public static readonly DependencyProperty PlayingInputProperty = DependencyProperty.Register("PlayingInput", typeof(bool), typeof(PlayerControl), new PropertyMetadata(false, null, PlayingInputted));

        public bool PlayingInput
        {
            set => SetValue(PlayingInputProperty, value);
        }

        public static readonly DependencyProperty PlayingOutputProperty = DependencyProperty.Register("PlayingOutput", typeof(bool), typeof(PlayerControl));

        public bool PlayingOutput
        {
            get => (bool)GetValue(PlayingOutputProperty);
            set => SetValue(PlayingOutputProperty, value);
        }

        public static readonly DependencyProperty TimeInputProperty = DependencyProperty.Register("TimeInput", typeof(long), typeof(PlayerControl), new PropertyMetadata((long)0, null, TimeInputted));

        public long TimeInput
        {
            set => SetValue(TimeInputProperty, value);
        }

        public static readonly DependencyProperty TimeOutputProperty = DependencyProperty.Register("TimeOutput", typeof(long), typeof(PlayerControl));

        public long TimeOutput
        {
            get => (long)GetValue(TimeOutputProperty);
            set => SetValue(TimeOutputProperty, value);
        }

        public static readonly DependencyProperty LengthOutputProperty = DependencyProperty.Register("LengthOutput", typeof(long), typeof(PlayerControl));

        public long LengthOutput
        {
            get => (long)GetValue(LengthOutputProperty);
            set => SetValue(LengthOutputProperty, value);
        }

        public static readonly DependencyProperty EndReachedOutputProperty = DependencyProperty.Register("EndReachedOutput", typeof(long), typeof(PlayerControl));

        public long EndReachedOutput
        {
            get => (long)GetValue(EndReachedOutputProperty);
            set => SetValue(EndReachedOutputProperty, value);
        }

        public PlayerControl()
        {
            InitializeComponent();

            // Choose x86 or x64 library
            var vlcDirectoryPath = Path.Combine(Environment.CurrentDirectory, IntPtr.Size == 4 ? "../../../packages/VideoLAN.LibVLC.Windows.3.0.5/build/x86" : "../../../packages/VideoLAN.LibVLC.Windows.3.0.5/build/x64");
            VlcControl.SourceProvider.CreatePlayer(new DirectoryInfo(vlcDirectoryPath));

            VlcControl.SourceProvider.MediaPlayer.Playing += MediaPlayerOnPlaying;
            VlcControl.SourceProvider.MediaPlayer.TimeChanged += MediaPlayerOnTimeChanged;
            VlcControl.SourceProvider.MediaPlayer.LengthChanged += MediaPlayerOnLengthChanged;
            VlcControl.SourceProvider.MediaPlayer.EndReached += MediaPlayerOnEndReached;
        }

        private async void MediaPlayerOnPlaying(object sender, VlcMediaPlayerPlayingEventArgs vlcMediaPlayerPlayingEventArgs)
        {
            await Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    PlayingOutput = VlcControl.SourceProvider.MediaPlayer.IsPlaying();
                });
            });
        }

        private async void MediaPlayerOnTimeChanged(object sender, VlcMediaPlayerTimeChangedEventArgs vlcMediaPlayerTimeChangedEventArgs)
        {
            await Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    TimeOutput = vlcMediaPlayerTimeChangedEventArgs.NewTime;
                });
            });
        }

        private async void MediaPlayerOnLengthChanged(object sender, VlcMediaPlayerLengthChangedEventArgs vlcMediaPlayerLengthChangedEventArgs)
        {
            await Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    LengthOutput = VlcControl.SourceProvider.MediaPlayer.Length;
                });
            });
        }

        private async void MediaPlayerOnEndReached(object sender, VlcMediaPlayerEndReachedEventArgs vlcMediaPlayerEndReachedEventArgs)
        {
            await Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    EndReachedOutput = VlcControl.SourceProvider.MediaPlayer.Length;
                });
            });
        }

        private static object UriInputted(DependencyObject d, object baseValue)
        {
            var player = (PlayerControl)d;
            if ((Uri)baseValue != null)
            {
                player.VlcControl.SourceProvider.MediaPlayer.SetMedia((Uri)baseValue);
            }

            return baseValue;
        }

        private static object PlayingInputted(DependencyObject d, object baseValue)
        {
            var player = (PlayerControl)d;
            if ((bool)baseValue)
            {
                ThreadPool.QueueUserWorkItem(i =>
                {
                    player.VlcControl.SourceProvider.MediaPlayer.Play();
                });
            }
            else
            {
                ThreadPool.QueueUserWorkItem(i =>
                {
                    player.VlcControl.SourceProvider.MediaPlayer.Pause();
                });
            }

            return baseValue;
        }

        private static object TimeInputted(DependencyObject d, object baseValue)
        {
            var player = (PlayerControl)d;
            player.VlcControl.SourceProvider.MediaPlayer.Time = (long)baseValue;

            return baseValue;
        }
    }
}
