using System;

namespace Dawdle.Client.ViewModels.Interfaces
{
    public interface IVideoViewModel
        : IBaseViewModel
    {
        Uri ActualUri { get; }
        bool IsPlaying { get; }
    }
}