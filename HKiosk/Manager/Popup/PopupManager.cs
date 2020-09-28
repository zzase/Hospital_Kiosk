using HKiosk.Controls.Popup;
using System;
using System.Collections.Generic;

namespace HKiosk.Manager.Popup
{
    public static class PopupManager
    {
        private static readonly Dictionary<PopupElement, IPopup> popup = new Dictionary<PopupElement, IPopup>();
        public static Dictionary<PopupElement, IPopup> Popup { get => popup; }
    }

    public enum PopupElement
    {
        Alert,
        Confirm,
        Loding
    }
}
