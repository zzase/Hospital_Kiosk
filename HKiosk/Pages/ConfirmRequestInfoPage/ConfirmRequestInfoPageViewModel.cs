﻿using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using HKiosk.Manager.Data;
using System.Globalization;
using HKiosk.Manager.Popup;
using HKiosk.Util.Server;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    public class ConfirmRequestInfoPageViewModel : PropertyChange
    {
        private string finalPrice;
        private ObservableCollection<CertRequestInfo> certRequestInfos;
        private ICommand isCancelCommand;
        private Visibility certRequestButtonVisibility;
        private Visibility paymentButtonVisibility;
        private Visibility printButtonVisibility;

        public ICommand MainPageCommand { get; }
        public ICommand PlusCertCommand { get; }
        public ICommand CertRequesetCommnad { get; }
        public ICommand PaymentCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand IsCancelCommand
        {
            get { return (this.isCancelCommand) ?? (this.isCancelCommand = new Command((obj) => { CheckProcess(); })); }
        }

        public ObservableCollection<CertRequestInfo> CertRequestInfos
        {
            get => certRequestInfos;
            set => SetProperty(ref certRequestInfos, value);
        }

        public string FinalPrice
        {
            get => finalPrice;
            set => SetProperty(ref finalPrice, value);
        }

        public Visibility CertRequestButtonVisibility
        {
            get => certRequestButtonVisibility;
            set => SetProperty(ref certRequestButtonVisibility, value);
        }

        public Visibility PaymentButtonVisibility
        {
            get => paymentButtonVisibility;
            set => SetProperty(ref paymentButtonVisibility, value);
        }

        public Visibility PrintButtonVisibility
        {
            get => printButtonVisibility;
            set => SetProperty(ref printButtonVisibility, value);
        }
        
        public ConfirmRequestInfoPageViewModel()
        {
            CertRequestInfos = DataManager.Instance.CertRequestInfos;

            CalcFinalPrice();

            SetButtonVisibility();

            MainPageCommand = new Command((obj) =>
            {
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            });

            PlusCertCommand = new Command((obj) =>
            {
                for (int i = 0; i < CertRequestInfos.Count; i++)
                {
                    CertRequestInfos[i].SujinHistroy.IsChecked = false;
                }
                NavigationManager.Navigate(PageElement.SelectCert);
            });

            CertRequesetCommnad = new Command(async (obj) =>
            {
                if (DataManager.Instance.CertRequestInfos.Count == 0)
                {
                    PopupManager.Instance[PopupElement.Alert]?.Show("발급 가능한 증명서 목록이 없습니다.\n증명서를 선택해주세요");
                    return;
                }
                else
                {
                    PopupManager.Instance[PopupElement.Loding]?.Show("증명서 신청중입니다. \n 잠시만 기다려주세요.");

                    var certRequestJson = await RequestAPI.CertRequest();

                    if (certRequestJson == null)
                    {
                        PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.");
                        PopupManager.Instance[PopupElement.Loding]?.Hide();
                        return;
                    }

                    if (certRequestJson["resultCode"]?.ToString() == "200")
                    {
                        DataManager.Instance.PaymentInfo.OrderNo = certRequestJson["orderNo"]?.ToString();

                        try
                        {
                            var list = certRequestJson["list"].Value<JArray>();

                            foreach (var jobject in list)
                            {
                                int index = int.Parse(jobject["reqSeq"]?.ToString());

                                if (jobject["retCd"].ToString() == "200")
                                {
                                    CertRequestInfos[index].CertNo = jobject["certNo"].ToString();
                                }
                                else
                                {
                                    CertRequestInfos[index].State = "반송";
                                    CertRequestInfos[index].StateCode = "31";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.");
                            PopupManager.Instance[PopupElement.Loding]?.Hide();
                            Log.Write($"[ConfirmRequestInfoPageViewModel] CertRequesetCommnad exception : {ex}");
                            return;
                        }
                    }
                    else
                    {
                        var resultMessage = certRequestJson["resultMessage"]?.ToString();

                        if (string.IsNullOrWhiteSpace(resultMessage))
                        {
                            resultMessage = "서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.";
                        }

                        PopupManager.Instance[PopupElement.Alert].Show(resultMessage);
                        PopupManager.Instance[PopupElement.Loding]?.Hide();
                        return;
                    }

                    for (int i = 0; i < CertRequestInfos.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(DataManager.Instance.CertRequestInfos[i].StateCode) ||
                            string.IsNullOrWhiteSpace(DataManager.Instance.CertRequestInfos[i].CertNo))
                            continue;

                        CertRequestInfos[i].StateCode = "10";
                        CertRequestInfos[i].State = "발급중";
                        CheckCertState(CertRequestInfos[i], DataManager.Instance.CancellationTokenSource.Token);
                    }

                    SetButtonVisibility();
                    PopupManager.Instance[PopupElement.Loding]?.Hide();
                }
            });

            PaymentCommand = new Command((obj) =>
            {
                DataManager.Instance.FinalPrice = FinalPrice;

                if (DataManager.Instance.FinalPrice.Equals("0원"))
                    NavigationManager.Navigate(PageElement.Print);

                else
                    NavigationManager.Navigate(PageElement.SelectPayment);
            });

            PrintCommand = new Command((obj) =>
            {
                NavigationManager.Navigate(PageElement.Print);
            });
        }

        public async void CheckCertState(CertRequestInfo certRequestInfo, CancellationToken cancelToken)
        {
            while (certRequestInfo.StateCode != "30" && certRequestInfo.StateCode != "31")
            {
                if (cancelToken.IsCancellationRequested)
                    return;

                var stateJson = await RequestAPI.CertStateRequest(certRequestInfo.CertNo);

                if (stateJson == null)
                {
                    await Task.Delay(1000);
                    continue;
                }

                Console.WriteLine(stateJson);

                if (stateJson["resultCode"]?.ToString() == "200")
                {
                    certRequestInfo.StateCode = stateJson["stateCd"]?.ToString();

                    switch (certRequestInfo.StateCode)
                    {
                        case "10":
                        case "20":
                            certRequestInfo.State = "발급중";
                            break;
                        case "30":
                            certRequestInfo.State = "발급완료";
                            break;
                        case "31":
                            certRequestInfo.State = "반송";
                            break;
                    }
                }

                await Task.Delay(1000);
            }

            if (certRequestInfo.StateCode == "31")
                CalcFinalPrice();
        }

        private void CheckProcess()
        {
            for (int i = 0; i < DataManager.Instance.CertRequestInfos.Count;)
            {
                if (DataManager.Instance.CertRequestInfos[i].IsCheckedForCancel)
                {
                    DataManager.Instance.CertRequestInfos.RemoveAt(i);
                }
                else i++;
            }
            CalcFinalPrice();
        }

        private void CalcFinalPrice()
        {
            int finalPrice = 0;

            for (int i = 0; i < DataManager.Instance.CertRequestInfos.Count; i++)
            {
                if (DataManager.Instance.CertRequestInfos[i].StateCode == "31")
                    continue;

                string[] PriceArray = DataManager.Instance.CertRequestInfos[i].Job.Price.Split('원');

                finalPrice += Int32.Parse(PriceArray[0], NumberStyles.AllowThousands) * Int32.Parse(DataManager.Instance.CertRequestInfos[i].Count ?? "0");
            }

            FinalPrice = String.Format("{0:#,0}", finalPrice) + "원";
        }

        private void SetButtonVisibility()
        {
            bool noPayment = Mode.Equals(Mode.ModeFlag.NO_PAYMENT);

            CertRequestButtonVisibility = Visibility.Collapsed;
            PaymentButtonVisibility = noPayment ? Visibility.Collapsed : Visibility.Visible;
            PrintButtonVisibility = noPayment ? Visibility.Visible : Visibility.Collapsed;

            for (int i = 0; i < DataManager.Instance.CertRequestInfos.Count; i++)
            {
                if (string.IsNullOrEmpty(DataManager.Instance.CertRequestInfos[i].StateCode))
                {
                    CertRequestButtonVisibility = Visibility.Visible;
                    PaymentButtonVisibility = Visibility.Collapsed;
                    PrintButtonVisibility = Visibility.Collapsed;
                    break;
                }
            }
        }
    }
}
