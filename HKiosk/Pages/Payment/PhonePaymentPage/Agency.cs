using HKiosk.Base;
using System.Windows.Media;

namespace HKiosk.Pages.Payment.PhonePaymentPage
{
    public class Agency : PropertyChange
    {
        private string name;
        private string paramName;
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

        public string ParamName { get; set; }
    }
}
