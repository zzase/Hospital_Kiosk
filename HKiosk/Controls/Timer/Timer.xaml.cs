using HKiosk.Manager.Timer;
using System.Windows.Controls;

namespace HKiosk.Controls.Timer
{
    /// <summary>
    /// timer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Timer : UserControl
    {
        public Timer()
        {
            InitializeComponent();

            this.DataContext = TimerManager.Timer as TimerViewModel;
        }
    }
}
