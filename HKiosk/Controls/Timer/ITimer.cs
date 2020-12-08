using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Controls.Timer
{
    public interface ITimer
    {
        void Start(int limit);
        void Stop();
    }
}
