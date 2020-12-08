using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HKiosk.Controls.VideoPlayer
{
    /// <summary>
    /// VideoPlayer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VideoPlayer : UserControl, IMediaService
    {
        public VideoPlayer()
        {
            InitializeComponent();

            Loaded += (object sender, RoutedEventArgs e) =>
            {
                vm.Load(this);
                this.mediaPlayer.MediaEnded += NextVideo;

                Play();
            };
        }

        public void Play()
        {
            this.mediaPlayer.Play();
        }

        public void Stop()
        {
            this.mediaPlayer.Stop();
        }

        private void NextVideo(object sender, RoutedEventArgs e)
        {
            vm.NextVideo();
        }
    }
}
