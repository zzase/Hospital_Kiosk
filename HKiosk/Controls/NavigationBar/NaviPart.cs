using HKiosk.Base;
using HKiosk.Manager.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HKiosk.Controls.NavigationBar
{
    public class NaviPart : PropertyChange
    {
        private Thickness gridMargin;
        private Thickness textBlockMargin;
        private string text;
        private NaviElement naviElement;
        private bool isActivated;
        private Brush foregroundColor;
        private ImageSource naviImage;
        private ImageSource activatedImage;
        private ImageSource deactivatedImage;

        public Thickness GridMargin
        {
            get => gridMargin;
            set => SetProperty(ref gridMargin, value);
        }

        public Thickness TextBlockMargin
        {
            get => textBlockMargin;
            set => SetProperty(ref textBlockMargin, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public NaviElement Navi
        {
            get => naviElement;
            set => SetProperty(ref naviElement, value);
        }

        public Brush ForegroundColor
        {
            get => foregroundColor;
            set => SetProperty(ref foregroundColor, value);
        }

        public ImageSource NaviImage
        {
            get => naviImage;
            set => SetProperty(ref naviImage, value);
        }

        public bool IsActivated
        {
            get => isActivated;
            set
            {
                isActivated = value;

                ForegroundColor = new SolidColorBrush(IsActivated ? Colors.White : Color.FromRgb(0x67, 0x67, 0x67));
                NaviImage = IsActivated ? ActivatedImage : DeactivatedImage;
            }
        }

        public ImageSource ActivatedImage
        {
            get => activatedImage;
            set => SetProperty(ref activatedImage, value);
        }

        public ImageSource DeactivatedImage
        {
            get => deactivatedImage;
            set => SetProperty(ref deactivatedImage, value);
        }

    }
}
