using HKiosk.Manager.Navigation;
using System.Windows;

namespace HKiosk.Controls.NavigationBar
{
    public interface INavigationBar
    {
        void ActivateNaviPart(NaviElement naviElementName);
        void SetVisibility(Visibility visibility);
    }
}
