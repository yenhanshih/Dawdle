using System;
using Dawdle.Client.Common;
using Dawdle.Client.ViewModels.Interfaces;

namespace Dawdle.Client.ViewModels
{
    public class VideoViewModel
        : NotifyPropertyChanged
        , IVideoViewModel
    {
        private Uri _playerControlUriInput;
        public Uri PlayerControlUriInput
        {
            get => _playerControlUriInput;
            private set
            {
                _playerControlUriInput = value;
                OnPropertyChanged();
            }
        }

        private bool _playerControlPlayingInput;
        public bool PlayerControlPlayingInput
        {
            get => _playerControlPlayingInput;
            set
            {
                _playerControlPlayingInput = value;
                OnPropertyChanged();
            }
        }

        private bool _playerControlPlayingOutput;
        public bool PlayerControlPlayingOutput
        {
            get => _playerControlPlayingOutput;
            set
            {
                _playerControlPlayingOutput = value;
                PlayerControlInterfacePlayingInput = value;
                OnPropertyChanged();
            }
        }

        private long _playerControlTimeInput;
        public long PlayerControlTimeInput
        {
            get => _playerControlTimeInput;
            set
            {
                _playerControlTimeInput = value;
                OnPropertyChanged();
            }
        }

        private long _playerControlTimeOutput;
        public long PlayerControlTimeOutput
        {
            get => _playerControlTimeOutput;
            set
            {
                _playerControlTimeOutput = value;
                PlayerInterfaceControlTimeInput = value;
                OnPropertyChanged();
            }
        }

        private long _playerControlLengthOutput;
        public long PlayerControlLengthOutput
        {
            get => _playerControlLengthOutput;
            set
            {
                _playerControlLengthOutput = value;
                PlayerInterfaceControlLengthInput = value;
                OnPropertyChanged();
            }
        }

        private bool _playerControlInterfacePlayingInput;
        public bool PlayerControlInterfacePlayingInput
        {
            get => _playerControlInterfacePlayingInput;
            set
            {
                _playerControlInterfacePlayingInput = value;
                OnPropertyChanged();
            }
        }

        private bool _playerControlInterfacePlayingOutput;
        public bool PlayerControlInterfacePlayingOutput
        {
            get => _playerControlInterfacePlayingOutput;
            set
            {
                _playerControlInterfacePlayingOutput = value;
                PlayerControlPlayingInput = value;
                OnPropertyChanged();
            }
        }

        private long _playerInterfaceControlTimeInput;
        public long PlayerInterfaceControlTimeInput
        {
            get => _playerInterfaceControlTimeInput;
            set
            {
                _playerInterfaceControlTimeInput = value;
                OnPropertyChanged();
            }
        }

        private long _playerInterfaceControlTimeOutput;
        public long PlayerInterfaceControlTimeOutput
        {
            get => _playerInterfaceControlTimeOutput;
            set
            {
                _playerInterfaceControlTimeOutput = value;
                PlayerControlTimeInput = value;
                OnPropertyChanged();
            }
        }

        private long _playerInterfaceControlLengthInput;
        public long PlayerInterfaceControlLengthInput
        {
            get => _playerInterfaceControlLengthInput;
            set
            {
                _playerInterfaceControlLengthInput = value;
                OnPropertyChanged();
            }
        }

        public VideoViewModel(IMainViewModel parentMainViewModel)
        {
            parentMainViewModel.ReceivedPlaySignal.Subscribe(uri =>
            {
                PlayerControlPlayingInput = false;
                PlayerControlUriInput = uri;
                PlayerControlPlayingInput = true;
            });
        }
    }
}