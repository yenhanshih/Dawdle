using System;
using Dawdle.Client.Common;
using Dawdle.Client.ViewModels.Interfaces;

namespace Dawdle.Client.ViewModels
{
    public class VideoViewModel
        : NotifyPropertyChanged
        , IVideoViewModel
    {
        private readonly IMainViewModel _parentMainViewModel;

        private Uri _actualUri;
        public Uri ActualUri
        {
            get { return _actualUri; }
            private set
            {
                _actualUri = value;
                OnPropertyChanged();
            }
        }

        private bool _isPlaying;
        public bool IsPlaying
        {
            get { return _isPlaying; }
            private set
            {
                _isPlaying = value;
                OnPropertyChanged();
            }
        }

        public VideoViewModel(IMainViewModel parentMainViewModel)
        {
            _parentMainViewModel = parentMainViewModel;

            _parentMainViewModel.ReceivedPlaySignal.Subscribe(uri =>
            {
                IsPlaying = false;
                ActualUri = uri;
                IsPlaying = true;
            });
        }
    }
}