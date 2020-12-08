using HKiosk.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HKiosk.Pages.SelectHistory 
{
    public class Interval : PropertyChange
    {
        private string name;
        private ImageSource background;
        private Brush foreground;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public ImageSource Background
        {
            get => background;
            set => SetProperty(ref background, value);
        }

        public Brush Foreground
        {
            get => foreground;
            set => SetProperty(ref foreground, value);
        }
    }
}
