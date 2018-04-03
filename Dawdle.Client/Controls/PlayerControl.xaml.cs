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
        public static readonly DependencyProperty UriInputProperty = DependencyProperty.Register("UriInput", typeof(Uri), typeof(PlayerControl), new PropertyMetadata(UriInputted));

        public Uri UriInput
        {
            set => SetValue(UriInputProperty, value);
        }

        public static readonly DependencyProperty PlayingInputProperty = DependencyProperty.Register("PlayingInput", typeof(bool), typeof(PlayerControl), new PropertyMetadata(PlayingInputted));

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

        public static readonly DependencyProperty TimeInputProperty = DependencyProperty.Register("TimeInput", typeof(long), typeof(PlayerControl), new PropertyMetadata(TimeInputted));

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

        public PlayerControl()
        {
            InitializeComponent();

            // Choose x86 or x64 library
            var vlcDirectoryPath = Path.Combine(Environment.CurrentDirectory, IntPtr.Size == 4 ? "../../../packages/VideoLAN.LibVLC.Windows.3.0.0-alpha2/build/x86" : "../../../packages/VideoLAN.LibVLC.Windows.3.0.0-alpha2/build/x64");
            VlcControl.SourceProvider.CreatePlayer(new DirectoryInfo(vlcDirectoryPath));

            VlcControl.SourceProvider.MediaPlayer.Playing += MediaPlayerOnPlaying;
            VlcControl.SourceProvider.MediaPlayer.TimeChanged += MediaPlayerOnTimeChanged;
            VlcControl.SourceProvider.MediaPlayer.LengthChanged += MediaPlayerOnLengthChanged;
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

        private static void UriInputted(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = (PlayerControl)d;
            player.VlcControl.SourceProvider.MediaPlayer.SetMedia((Uri)e.NewValue);
        }

        private static void PlayingInputted(DependencyObject d, DependencyPropertyChangedEventArgs e)
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
                ThreadPool.QueueUserWorkItem(i =>
                {
                    player.VlcControl.SourceProvider.MediaPlayer.Pause();
                });
            }
        }

        private static void TimeInputted(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = (PlayerControl)d;
            player.VlcControl.SourceProvider.MediaPlayer.Time = (long)e.NewValue;
        }
    }
}
