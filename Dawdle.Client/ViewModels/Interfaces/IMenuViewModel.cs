using Dawdle.Client.Common;

namespace Dawdle.Client.ViewModels.Interfaces
{
    public interface IMenuViewModel
        : IBaseViewModel
    {
        bool IsCurrentContextHome { get; set; }
        bool IsCurrentContextPlay { get; set; }
        RelayCommand SetHomeView { get; }
        RelayCommand SetVideoView { get; }
    }
}