using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Controls.VideoPlayer
{
    public static class VideoProvider
    {
        public static ObservableCollection<Video> GetVideos()
        {
            var videos = new ObservableCollection<Video>();

            var count = Properties.Settings.Default.VideoCount;
            var titles = Properties.Settings.Default.VideoTitles;
            var paths = Properties.Settings.Default.VideoPaths;

            for (int i = 0; i < count; i++)
            {
                videos.Add(new Video
                {
                    Index = i,
                    Name = titles[i],
                    FilePath = paths[i],
                });
            }

            return videos;
        }
    }
}
