using System;
using System.Reactive.Subjects;
using Dawdle.Client.Models.Enums;

namespace Dawdle.Client.ViewModels.Interfaces
{
    public interface IMainViewModel
        : IBaseViewModel
    {
        ISubject<Context> CurrentViewChanged { get; }
        ISubject<Uri> ReceivedPlaySignal { get; }
        bool IsHomeViewVisible { get; }
        bool IsVideoViewVisible { get; }
        void ChangeContext(Context context);
    }
}