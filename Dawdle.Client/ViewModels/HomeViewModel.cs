using System.Collections.Generic;
using System.Linq;
using Dawdle.Client.Common;
using Dawdle.Client.Models.Enums;
using Dawdle.Client.ViewModels.Interfaces;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using MyToolkit.Multimedia;

namespace Dawdle.Client.ViewModels
{
    public class HomeViewModel
        : NotifyPropertyChanged
        , IHomeViewModel
    {
        private readonly IMainViewModel _parentMainViewModel;

        private List<SearchResult> _videos;
        public List<SearchResult> Videos
        {
            get { return _videos; }
            private set
            {
                _videos = value;
                OnPropertyChanged();
            }
        }  

        public HomeViewModel(IMainViewModel parentMainViewModel)
        {
            _parentMainViewModel = parentMainViewModel;

            GetYoutubeVideoTitles("Google");
        }

        private RelayCommand _playVideo;
        public RelayCommand PlayVideo
        {
            get
            {
                return _playVideo ?? (_playVideo = new RelayCommand(async id =>
                {
                    var resourceId = (ResourceId)id;
                    var youtube = await YouTube.GetUrisAsync(resourceId.VideoId.ToString());
                    _parentMainViewModel.ChangeContext(Context.Play);
                    _parentMainViewModel.ReceivedPlaySignal.OnNext(youtube.First().Uri);
                }));
            }
        }

        private async void GetYoutubeVideoTitles(string searchString)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDsDJMSZsQAS4-MTiPzXtG_KK4KXiRx9E8",
                ApplicationName = GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchString;
            searchListRequest.MaxResults = 50;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            Videos = searchListResponse.Items.Where(video => video.Id.Kind == "youtube#video").ToList();
        }
    }
}