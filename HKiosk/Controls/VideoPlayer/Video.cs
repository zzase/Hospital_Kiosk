using HKiosk.Base;
using System.Windows.Input;
using System.Windows.Media;

namespace HKiosk.Controls.VideoPlayer
{
    public class Video : PropertyChange
    {
        private int index;
        private string name;
        private string filePath;
        private ImageSource toggleImage;

        public int Index
        {
            get => index;
            set => SetProperty(ref index, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string FilePath
        {
            get => filePath;
            set => SetProperty(ref filePath, value);
        }

        public ImageSource ToggleImage
        {
            get => toggleImage;
            set 
                {
                SetProperty(ref toggleImage, value);

                System.Console.WriteLine(value);

                    }
        }

        public ICommand Click { get; set; }
    }
}
