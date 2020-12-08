using HKiosk.Base;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using System;
using System.Windows;
using System.Windows.Threading;

namespace HKiosk.Controls.Timer
{
    public class TimerViewModel : PropertyChange, ITimer
    {
        private readonly DispatcherTimer timer;
        private int limit;
        private string timerText;
        private Visibility visibility;

        public string TimerText
        {
            get => timerText;
            set => SetProperty(ref timerText, value);
        }

        public Visibility Visibility
        {
            get => visibility;
            set => SetProperty(ref visibility, value);
        }

        private void DtTicker(object sender, EventArgs e)
        {
            if (--limit < 0)
            {
                Stop();
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(limit);
            TimerText = $"{timeSpan.Minutes:D2} : {timeSpan.Seconds:D2}";
        }

        public TimerViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(this.DtTicker);
        }

        public void Start(int limit)
        {
            var timeSpan = TimeSpan.FromSeconds(limit);
            this.limit = limit;

            TimerText = $"{timeSpan.Minutes:D2} : {timeSpan.Seconds:D2}";
            Visibility = Visibility.Visible;

            if (!timer.IsEnabled)
                timer.Start();
        }

        public void Stop()
        {
            Visibility = Visibility.Hidden;

            if (timer.IsEnabled)
                timer.Stop();
        }
    }
}
