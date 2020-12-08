using HKiosk.Base;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HKiosk.Controls.VideoPlayer
{
    public class VideoPlayerViewModel : PropertyChange
    {
        private ObservableCollection<Video> videos = new ObservableCollection<Video>();
        private Uri source;
        private BitmapImage activationImage;
        private BitmapImage deactivationImage;

        public IMediaService MediaService { get; private set; }

        public ObservableCollection<Video> Videos
        {
            get => videos;
            set => SetProperty(ref videos, value);
        }

        public Uri Source
        {
            get => source;
            set => SetProperty(ref source, value);
        }

        public int VideoIndex { get; set; }

        public ICommand Click { get; }

        public VideoPlayerViewModel()
        {
            Videos = VideoProvider.GetVideos();

            Click = new Command((obj) =>
            {
                var vi = obj as Video;
                var videoIndex = vi.Index;

                ToggleImage(videoIndex);
                ChangeVideo(videoIndex);
            });

            InitVideos();
        }

        public void InitVideos()
        {
            try
            {
                activationImage = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Controls/VideoPlayer/Navi_T.png"));
                deactivationImage = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Controls/VideoPlayer/Navi_F.png"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[VideoPlayerViewModel - InitVideos()] navi 이미지 파싱 실패 : {ex}");
            }

            SetVideoClick();
            ToggleImage(VideoIndex);
        }

        public void SetVideoClick()
        {
            foreach (var video in Videos)
            {
                video.Click = Click;
            }
        }

        public void NextVideo()
        {
            var index = Videos.Count == VideoIndex + 1 ? 0 : VideoIndex + 1;
            ToggleImage(index);
            ChangeVideo(index);
        }

        public void ToggleImage(int index)
        {
            foreach (var video in Videos)
            {
                video.ToggleImage = (index == video.Index) ? activationImage : deactivationImage;
            }
        }

        public void ChangeVideo(int index)
        {
            Source = new Uri(Videos[index].FilePath, UriKind.Relative);
            VideoIndex = index;

            ReStart();
        }

        public void Load(object mediaService)
        {
            this.MediaService = mediaService as IMediaService;
            Source = new Uri(Videos[VideoIndex].FilePath, UriKind.Relative);
        }

        public void Play()
        {
            this.MediaService?.Play();
        }

        public void Stop()
        {
            this.MediaService?.Stop();
        }

        public void ReStart()
        {
            Stop();
            Play();
        }
    }
}
