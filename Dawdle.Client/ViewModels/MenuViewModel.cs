using System;
using Dawdle.Client.Common;
using Dawdle.Client.Models.Enums;
using Dawdle.Client.ViewModels.Interfaces;

namespace Dawdle.Client.ViewModels
{
    public class MenuViewModel
        : NotifyPropertyChanged
        , IMenuViewModel
    {
        private readonly IMainViewModel _parentMainViewModel;

        private bool _isCurrentContextHome;
        public bool IsCurrentContextHome
        {
            get { return _isCurrentContextHome; }
            set
            {
                _isCurrentContextHome = value;
                OnPropertyChanged();
            }
        }

        private bool _isCurrentContextPlay;
        public bool IsCurrentContextPlay
        {
            get { return _isCurrentContextPlay; }
            set
            {
                _isCurrentContextPlay = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel(IMainViewModel mainViewModel)
        {
            _parentMainViewModel = mainViewModel;
            _parentMainViewModel.CurrentViewChanged.Subscribe(context =>
            {
                ResetCurrentContext();

                if (context == Context.Home)
                {
                    IsCurrentContextHome = true;
                }
                if (context == Context.Play)
                {
                    IsCurrentContextPlay = true;
                }
            });
            _parentMainViewModel.ChangeContext(Context.Home);
        }

        private RelayCommand _setHomeView;
        public RelayCommand SetHomeView
        {
            get
            {
                return _setHomeView ?? (_setHomeView = new RelayCommand(_ =>
                {
                    _parentMainViewModel.ChangeContext(Context.Home);
                }));
            }
        }

        private RelayCommand _setVideoView;
        public RelayCommand SetVideoView
        {
            get
            {
                return _setVideoView ?? (_setVideoView = new RelayCommand(_ =>
                {
                    _parentMainViewModel.ChangeContext(Context.Play);
                }));
            }
        }

        private void ResetCurrentContext()
        {
            IsCurrentContextHome = false;
            IsCurrentContextPlay = false;
        }
    }
}