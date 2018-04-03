using System;

namespace Dawdle.Client.ViewModels.Interfaces
{
    public interface IVideoViewModel
        : IBaseViewModel
    {
        Uri PlayerControlUriInput { get; }
        bool PlayerControlPlayingInput { get; set; }
        bool PlayerControlPlayingOutput { get; set; }
        long PlayerControlTimeInput { get; set; }
        long PlayerControlTimeOutput { get; set; }
        long PlayerControlLengthOutput { get; set; }
        bool PlayerControlInterfacePlayingInput { get; set; }
        bool PlayerControlInterfacePlayingOutput { get; set; }
        long PlayerInterfaceControlTimeInput { get; set; }
        long PlayerInterfaceControlTimeOutput { get; set; }
        long PlayerInterfaceControlLengthInput { get; set; }
    }
}