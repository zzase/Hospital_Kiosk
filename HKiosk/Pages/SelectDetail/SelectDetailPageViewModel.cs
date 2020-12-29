using HKiosk.Base;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using HKiosk.Manager.Popup;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKiosk.Pages.SelectDetail
{
    public class SelectDetailPageViewModel : PropertyChange
    {
        private ObservableCollection<SujinHistroy> sujinHistroies;
        private string deptNe;
        private string inDateNe;
        private double deptNeWidth;
        private double inDateWidth;
        private double outDateWidth;
        public ICommand MainPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public string CertNe
        {
            get => DataManager.Instance.SelectedJob.CertNe;
        }
        public string DeptNe
        {
            get => deptNe;
            set => SetProperty(ref deptNe, value);
        }

        public ObservableCollection<SujinHistroy> SujinHistories
        {
            get => sujinHistroies;
            set => SetProperty(ref sujinHistroies, value);
        }
        public double DeptNeWidth
        {
            get => deptNeWidth;
            set => SetProperty(ref deptNeWidth, value);
        }
        public string InDateNe
        {
            get => inDateNe;
            set => SetProperty(ref inDateNe, value);
        }
        public double InDateWidth
        {
            get => inDateWidth;
            set => SetProperty(ref inDateWidth, value);
        }
        public double OutDateWidth
        {
            get => outDateWidth;
            set => SetProperty(ref outDateWidth, value);
        }


        public SelectDetailPageViewModel()
        {
            SujinHistories = DataManager.Instance.SujinHistroys;

            for(int i=0; i<SujinHistories.Count; i++)
            {
                if (SujinHistories[i].DeptNe.Equals(""))
                {
                    DeptNeWidth = 0;
                }
                else
                {
                    DeptNeWidth = 300;
                }
                if (!SujinHistories[i].OutDate.Equals(""))
                {
                    InDateNe = "입원일자";
                    InDateWidth = 200;
                    OutDateWidth = 200;
                }
                else
                {
                    InDateNe = "수납일자";
                    InDateWidth = 300;
                    OutDateWidth = 0;
                }
            }

            MainPageCommand = new Command((obj) => NavigateMainPage());

            NextPageCommand = new Command((obj) =>
            {
                if (SujinHistories == null)
                {
                    PopupManager.Instance[PopupElement.Alert]?.Show("수진이력을 검색해주세요.");
                    return;
                }
                else
                {
                    SelectHistories();
                    NavigationManager.Navigate(PageElement.ConfirmRequestInfo);
                }
            });
            PreviousPageCommand = new Command((obj) =>
            {
                NavigationManager.Navigate(PageElement.SelectHistory);
            });
        }
        private void NavigateMainPage()
        {
            DataManager.Instance.InitData();
            NavigationManager.Navigate(PageElement.Main);
        }

        public void SelectHistories()
        {
            string requestDetail;

            for (int i = 0; i < SujinHistories.Count; i++)
            {
                if (!SujinHistories[i].OutDate.Equals("") && !SujinHistories[i].DeptNe.Equals(""))
                {
                    requestDetail = SujinHistories[i].DeptNe + "\n" + SujinHistories[i].InDate + "~" + SujinHistories[i].OutDate;
                }
                else if (SujinHistories[i].OutDate.Equals("") && !SujinHistories[i].DeptNe.Equals(""))
                {
                    requestDetail = SujinHistories[i].DeptNe + "\n" + SujinHistories[i].InDate;
                }
                else
                    requestDetail = SujinHistories[i].InDate;

                if (!SujinHistories[i].Count.Equals("0"))
                {
                    CertRequestInfo certRequestInfo = new CertRequestInfo
                    {
                        Job = DataManager.Instance.SelectedJob,
                        Count = SujinHistories[i].Count,
                        SujinHistroy = SujinHistories[i],
                        IsCheckedForCancel = false,
                        RequestDetail = requestDetail
                    };
                    DataManager.Instance.CertRequestInfos.Add(certRequestInfo);
                }
            }
        }
    }
}
