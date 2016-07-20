using System;
using Dawdle.Client.Common;

namespace Dawdle.Client.ViewModels.Interfaces
{
    public interface IVideoViewModel
        : IBaseViewModel
    {
        Uri ActualUri { get; }
        bool IsPlaying { get; }
        RelayCommand PlayPause { get; }
    }
}