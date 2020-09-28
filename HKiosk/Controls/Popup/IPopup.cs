using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Controls.Popup
{
    public interface IPopup
    {
        void Show(string msg, Action hideAction = null);
        void Hide();
    }
}
