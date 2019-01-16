using System;
using System.Collections.Generic;
using System.Linq;
using Dawdle.Client.Common;
using Dawdle.Client.Models.Enums;
using Dawdle.Client.ViewModels.Interfaces;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace Dawdle.Client.ViewModels
{
    public class HomeViewModel
        : NotifyPropertyChanged
        , IHomeViewModel
    {
        private readonly IYoutubeClient _youtubeClient;
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

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel(IYoutubeClient youtubeClient, IMainViewModel parentMainViewModel)
        {
            _youtubeClient = youtubeClient;
            _parentMainViewModel = parentMainViewModel;

            SearchAndReloadVideoList("Google");
        }

        private RelayCommand _playVideo;
        public RelayCommand PlayVideo
        {
            get
            {
                return _playVideo ?? (_playVideo = new RelayCommand(async id =>
                {
                    var resourceId = (ResourceId)id;
                    var videoMediaStreamInfo = await _youtubeClient.GetVideoMediaStreamInfosAsync(resourceId.VideoId);
                    var muxedStreamInfo = videoMediaStreamInfo.Muxed.WithHighestVideoQuality();
                    _parentMainViewModel.ChangeContext(Context.Play);
                    _parentMainViewModel.ReceivedPlaySignal.OnNext(new Uri(muxedStreamInfo.Url));
                }));
            }
        }

        public async void SearchAndReloadVideoList(string query)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "AIzaSyCL1Ux_Zu3A4H30538nqqVyF-fVPCTVOfo",
                ApplicationName = GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query;
            searchListRequest.MaxResults = 50;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            Videos = searchListResponse.Items.Where(video => video.Id.Kind == "youtube#video").ToList();
        }
    }
}