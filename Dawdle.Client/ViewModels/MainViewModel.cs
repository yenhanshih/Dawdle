using System;
using System.Reactive.Subjects;
using Dawdle.Client.Common;
using Dawdle.Client.Models.Enums;
using Dawdle.Client.ViewModels.Interfaces;

namespace Dawdle.Client.ViewModels
{
    public class MainViewModel
        : NotifyPropertyChanged
        , IMainViewModel
    {
        private readonly ISubject<Context> _currentViewChanged;
        public ISubject<Context> CurrentViewChanged
        {
            get { return _currentViewChanged; }
        }

        private readonly ISubject<Uri> _receivedPlaySignal; 
        public ISubject<Uri> ReceivedPlaySignal
        {
            get { return _receivedPlaySignal; }
        }

        private bool _isHomeViewVisible;
        public bool IsHomeViewVisible
        {
            get { return _isHomeViewVisible; }
            set
            {
                _isHomeViewVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isVideoViewVisible;
        public bool IsVideoViewVisible
        {
            get { return _isVideoViewVisible; }
            set
            {
                _isVideoViewVisible = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _currentViewChanged = new Subject<Context>();
            _receivedPlaySignal = new Subject<Uri>();
        }

        public void ChangeContext(Context context)
        {
            switch (context)
            {
                case Context.Home:
                    ResetViewVisibility();
                    IsHomeViewVisible = true;
                    CurrentViewChanged.OnNext(Context.Home);
                    break;
                case Context.Play:
                    ResetViewVisibility();
                    IsVideoViewVisible = true;
                    CurrentViewChanged.OnNext(Context.Play);
                    break;
            }
        }

        private void ResetViewVisibility()
        {
            IsHomeViewVisible = false;
            IsVideoViewVisible = false;
        }
    }
}