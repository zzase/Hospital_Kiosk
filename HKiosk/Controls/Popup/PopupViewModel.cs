using HKiosk.Base;
using HKiosk.Util;
using System;
using System.Windows;
using System.Windows.Input;

namespace HKiosk.Controls.Popup
{
    public class PopupViewModel : PropertyChange
    {
        private Visibility visibility = Visibility.Hidden;
        private string message = string.Empty;
        private Action hideAction;

        public Visibility Visibility
        {
            get => visibility;
            set => SetProperty(ref visibility, value);
        }

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public ICommand HidePopupCommand { get; }

        public PopupViewModel()
        {
            HidePopupCommand = new Command((obj) => 
            {
                bool? shouldExecuteHideAction = obj as bool?;
                if (shouldExecuteHideAction ?? false)
                    hideAction?.Invoke();

                Hide(); 
            });
        }

        public void Show(string msg, Action hideAction = null)
        {
            Visibility = Visibility.Visible;
            Message = msg;
            this.hideAction = hideAction;
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
