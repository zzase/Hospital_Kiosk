using HKiosk.Manager.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HKiosk.Controls.NavigationBar
{
    public class NaviPartProvider
    {
        private readonly Dictionary<NaviElement, string> NaviType1 = new Dictionary<NaviElement, string>()
        {
            {NaviElement.IdentityVerification, "본인인증"},
            {NaviElement.ConfirmUserInfo, "사용자 정보 확인"},
            {NaviElement.SelectCert, "증명서 선택"},
            {NaviElement.SelectHistory, "수진이력 선택"},
            {NaviElement.ConfirmRequestInfo, "신청내용 확인"},
            {NaviElement.Payment, "수수료 결제"},
            {NaviElement.SelectIssuanceMethod, "발급방법 선택"},
            {NaviElement.Print, "증명서 출력"},
        };

        public ObservableCollection<NaviPart> GetNaviParts()
        {
            ObservableCollection<NaviPart> naviParts = new ObservableCollection<NaviPart>();

            var ActivationNaviFirstImage = new BitmapImage
                (new Uri(@"/Resources/Controls/NavigationBar/Navi_1_T.png", UriKind.Relative));
            var DeactivationNaviFirstImage = new BitmapImage
                (new Uri(@"/Resources/Controls/NavigationBar/Navi_1_F.png", UriKind.Relative));
            var ActivationNaviImage = new BitmapImage
                (new Uri(@"/Resources/Controls/NavigationBar/Navi_2_T.png", UriKind.Relative));
            var DeactivationNaviImage = new BitmapImage
                (new Uri(@"/Resources/Controls/NavigationBar/Navi_2_F.png", UriKind.Relative));


            var gridMargin = new Thickness(0);
            bool isFirst = true;

            foreach (var navi in NaviType1)
            {
                naviParts.Add(new NaviPart
                {
                    GridMargin = gridMargin,
                    TextBlockMargin = isFirst ? new Thickness(0, 0, 20, 0) : new Thickness(0),
                    ActivatedImage = isFirst ? ActivationNaviFirstImage : ActivationNaviImage,
                    DeactivatedImage = isFirst ? DeactivationNaviFirstImage : DeactivationNaviImage,
                    Text = navi.Value,
                    Navi = navi.Key
                });

                gridMargin.Left -= 25;
                gridMargin.Right += 25;
                isFirst = false;
            }

            return naviParts;
        }
    }
}
