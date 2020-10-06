using HKiosk.Controls.Popup;
using HKiosk.Util;
using System;
using System.Collections.Generic;

namespace HKiosk.Manager.Popup
{
    public class PopupManager : Singleton<PopupManager>
    {
        private static readonly Dictionary<PopupElement, IPopup> popup = new Dictionary<PopupElement, IPopup>();

        public IPopup this[PopupElement popupElement]
        {
            get => popup.ContainsKey(popupElement) ? popup[popupElement] : null;
        }

        public void Add(PopupElement popupElement, IPopup popup)
        {
            if (PopupManager.popup.ContainsKey(popupElement))
                return;

            PopupManager.popup.Add(popupElement, popup);
        }
    }

    public enum PopupElement
    {
        Alert,
        Confirm,
        Loding
    }
}
