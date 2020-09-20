using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace HKiosk.Util
{
    public static class ClickEffect
    {
        public static FrameworkElement Create(Point touchPos)
        {
            return new Ellipse
            {
                Width = 0,
                Height = 0,
                Fill = System.Windows.Media.Brushes.Red,
                IsHitTestVisible = false,
                Margin = new Thickness((touchPos.X - 540) * 2, (touchPos.Y - 960) * 2, 0, 0),
                Opacity = 0f
            };
        }

        public static async Task<FrameworkElement> Animate(FrameworkElement element)
        {
            while (element.Opacity < 1f)
            {
                element.Width += 20;
                element.Height += 20;
                element.Opacity += 0.2f;
                await Task.Delay(50);
            }

            while (element.Opacity > 0f)
            {
                element.Width -= 20;
                element.Height -= 20;
                element.Opacity -= 0.2f;
                await Task.Delay(50);
            }

            return element;
        }
    }
}
