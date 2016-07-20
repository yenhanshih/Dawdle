using System.Collections.Generic;
using Dawdle.Client.Common;
using Google.Apis.YouTube.v3.Data;

namespace Dawdle.Client.ViewModels.Interfaces
{
    public interface IHomeViewModel
        : IBaseViewModel
    {
        List<SearchResult> Videos { get; }
        RelayCommand PlayVideo { get; }
    }
}