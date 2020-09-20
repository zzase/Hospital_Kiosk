using HKiosk.Base;
using HKiosk.Manager.Navigation;
using System.Collections.ObjectModel;
using System.Windows;

namespace HKiosk.Controls.NavigationBar
{
    public class NavigationBarViewModel : PropertyChange, INavigationBar
    {
        private ObservableCollection<NaviPart> naviParts = new ObservableCollection<NaviPart>();
        private NaviPartProvider provider = new NaviPartProvider();
        private Thickness marginBetweenElement = new Thickness(0);
        private Visibility visibility;

        public ObservableCollection<NaviPart> NaviParts
        {
            get => naviParts;
            set => SetProperty(ref naviParts, value);
        }

        public Thickness MarginBetweenElement
        {
            get => marginBetweenElement;
            set => SetProperty(ref marginBetweenElement, value); 
        }

        public Visibility Visibility
        {
            get => visibility;
            set => SetProperty(ref visibility, value);
        }

        public NavigationBarViewModel()
        {
            NaviParts = provider.GetNaviParts();
        }

        public void ActivateNaviPart(NaviElement navi)
        {
            for (int itemIndex = 0; itemIndex < NaviParts.Count; itemIndex++)
            {
                if (NaviParts[itemIndex].Navi == navi)
                {
                    NaviParts[itemIndex].IsActivated = true;

                    if (itemIndex > 3)
                    {
                        MarginBetweenElement = new Thickness((itemIndex - 3) * -215, 0, 0, 0);
                    }
                    else
                        MarginBetweenElement = new Thickness(0);
                }
                else
                    NaviParts[itemIndex].IsActivated = false;
            }
        }

        public void SetVisibility(Visibility visibility)
        {
            Visibility = visibility;
        }
    }

    public enum NaviElement
    {
        Main,
        IdentityVerification,
        ConfirmUserInfo,
        SelectCert,
        SelectHistory,
        ConfirmRequestInfo,
        Payment,
        SelectIssuanceMethod,
        Print,
        Fax,
        Mail
    }
}
