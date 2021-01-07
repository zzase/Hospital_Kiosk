using HKiosk.Manager.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Util.Server
{
    public static class RequestAPI
    {
        internal static string operationURL = Properties.Settings.Default.APIURL;

        internal static List<T> JArrayToList<T>(JArray jArray)
        {
            try
            {
                return (List<T>)Convert.ChangeType(JsonConvert.DeserializeObject<List<T>>(jArray?.ToString()), typeof(List<T>));
            }
            catch (Exception ex)
            {
                Log.Write($"JArrayToList Error {ex}");
                return null;
            }
        }

        internal static T ConvertJson<T>(JObject jObject) where T : new()
        {
            try
            {
                return (T)Convert.ChangeType(JsonConvert.DeserializeObject<T>(jObject.ToString()), typeof(T));
            }
            catch (Exception ex)
            {
                Log.Write($"ConvertJson Error {ex}");
                return default;
            }
        }

        #region XML
        /// <summary>
        /// 키오스크 세팅 XML 가져오기
        /// </summary>
        /// <returns></returns>
        internal static async Task<string> GetKioskSettingInfoXml()
        {
            return await WebServer.Get($"{operationURL}/wm1.0/xml/KIOSK_SETTING_INFO.xml");
        }

        /// <summary>
        /// 증명서 정보 XML 가져오기
        /// </summary>
        /// <returns></returns>
        internal static async Task<string> GetKioskCertiInfoXml()
        {
            return await WebServer.Get($"{operationURL}/wm1.0/xml/KIOSK_CERTI_INFO.xml");
        }

        /// <summary>
        /// 지역화 XML 가져오기
        /// </summary>
        /// <returns></returns>
        internal static async Task<string> GetKioskLocalizationInfoXml()
        {
            return await WebServer.Get($"{operationURL}/wm1.0/xml/KIOSK_LOCALIZATION.xml");
        }
        #endregion

        #region PatientPage
        /// <summary>
        /// 환자정보 조회 API
        /// </summary>
        /// <param name="uName">성명</param>
        /// <param name="uJumin">주민번호</param>
        /// <returns></returns>
        internal static async Task<JObject> PatNoRequest(string uName, string uJumin)
        {
            var giwanNo = Properties.Settings.Default.GiwanNo;

            var paramData = new JObject
            {
                { "command", "patNoReq" },
                { "giwanNo", giwanNo },
                { "uName", uName },
                { "uJumin", uJumin }
            };
            
            var postdata = new JObject
            {
                {"reqData", KISA_SEED_CBC_DLL_Importer.Encrypt(paramData.ToString())},
                {"requester", "BBMC"}
            };

            return await WebServer.RequestJsonAsync(postdata.ToString(), $"{operationURL}/ModuleT.do", "post");
        }
        static readonly byte[] key = Encoding.UTF8.GetBytes("BBMC!!@*5218998h");
        static readonly byte[] iv = Encoding.UTF8.GetBytes("4421673257160032");

        internal static string ByteEncryptTest(string _data)
        {
            byte[] data = Encoding.UTF8.GetBytes(_data);

            int size = (int)Math.Ceiling((data.Length + 1) / 16f) * 16;

            byte[] result = new byte[size];

            int v = KISA_SEED_CBC_DLL_Importer.SEED_CBC_Encrypt(key, iv, data, data.Length, result);

            return Convert.ToBase64String(result);
        }

        internal static string ByteDecryptTest(string _data)
        {
            byte[] data;

            data = Convert.FromBase64String(_data);

            int size = data.Length;

            byte[] result = new byte[size];

            KISA_SEED_CBC_DLL_Importer.SEED_CBC_Decrypt(key, iv, data, data.Length, result);

            return Encoding.UTF8.GetString(result);
        }

        internal static async Task<JObject> PatNoRequest_Test(string uName, string uJumin)
        {
            JObject paramData;

            switch (uName)
            {
                case "1":
                    paramData = new JObject
                    {
                        { "resultCode", "200" },
                        { "uPatNo", "2020123456" }
                    };
                    break;
                case "2":
                    paramData = new JObject
                    {
                        { "resultCode", "400" },
                        { "resultMessage", "등록된 환자정보가 없습니다." }
                    };
                    break;
                case "3":
                    paramData = new JObject
                    {
                        { "resultCode", "400" },
                        { "resultMessage", "발급에 필요한 필수데이터가 누락되었습니다." }
                    };
                    break;
                case "4":
                    paramData = new JObject
                    {
                        { "resultCode", "400" },
                        { "resultMessage", "서버가 다운되었거나 시스템오류가 발생하였습니다." }
                    };
                    break;
                default:
                    paramData = new JObject
                    {
                        { "resultCode", "" }
                    };
                    break;
            }

            await Task.Delay(1000);

            return paramData;
        }
        #endregion

        #region CertPage
        /// <summary>
        /// 발급가능 증명서 조회
        /// </summary>
        /// <returns></returns>
        internal static async Task<JObject> JobListRequest()
        {
            var giwanNo = Properties.Settings.Default.GiwanNo;
            var uName = DataManager.Instance.PatientInfo.Name;
            var uBirth = DataManager.Instance.PatientInfo.Birth;
            var uPatNo = DataManager.Instance.PatientInfo.PatientNo;

            var paramData = new JObject
            {
                { "command", "jobList" },
                { "giwanNo", giwanNo },
                { "uName", uName },
                { "uBirth", uBirth },
                { "uPatNo", uPatNo }
            };

            var postdata = new JObject
            {
                {"reqData", KISA_SEED_CBC_DLL_Importer.Encrypt(paramData.ToString())},
                {"requester", "BBMC"}
            };

            return await WebServer.RequestJsonAsync(postdata.ToString(), $"{operationURL}/ModuleC.do", "post");
        }

        internal static async Task<JObject> JobListRequest_Test(int testCase)
        {
            JObject paramData;

            switch (testCase)
            {
                case 1:
                {
                    var json1 = new JObject
                    {
                        { "certCd", "10101" },
                        { "certNe", "진단서 사본" },
                        { "hostCertCd", "CA006" },
                        { "price", "1,000원" },
                        { "korYN", "Y" }
                    };

                    var json2 = new JObject
                    {
                        { "certCd", "10102" },
                        { "certNe", "소견서 사본" },
                        { "hostCertCd", "CA006" },
                        { "price", "1,000원" },
                        { "korYN", "Y" }
                    };

                    var json3 = new JObject
                    {
                        { "certCd", "10103" },
                        { "certNe", "입퇴원확인서" },
                        { "hostCertCd", "CA006" },
                        { "price", "1,000원" },
                        { "korYN", "Y" }
                    };

                    var json4 = new JObject
                    {
                        { "certCd", "10104" },
                        { "certNe", "세부내역서(입원,외래,응급)" },
                        { "hostCertCd", "CA006" },
                        { "price", "1,000원" },
                        { "korYN", "Y" }
                    };

                    var json5 = new JObject
                    {
                        { "certCd", "10105" },
                        { "certNe", "진료비 영수증(입원,외래,응급)" },
                        { "hostCertCd", "CA006" },
                        { "price", "1,000원" },
                        { "korYN", "Y" }
                    };

                    var json6 = new JObject
                    {
                        { "certCd", "10106" },
                        { "certNe", "납입증명서(연말정산용)" },
                        { "hostCertCd", "CA006" },
                        { "price", "1,000원" },
                        { "korYN", "Y" }
                    };

                    var json7 = new JObject
                    {
                        { "certCd", "10106" },
                        { "certNe", "납입증명서(연말정산용)" },
                        { "hostCertCd", "CA006" },
                        { "price", "1,000원" },
                        { "korYN", "Y" }
                    };

                    JArray jArray = new JArray
                    {
                        json1,
                        json2,
                        json3,
                        json4,
                        json5,
                        json6
                    };

                    paramData = new JObject
                    {
                        { "resultCode", "200" },
                        { "giwanNo", "K00001" },
                        { "gubun", "환자" },
                        { "uPatNo", "2020123456" },
                        { "list", jArray }
                    };
                    break;
                }
                case 2:
                    paramData = new JObject
                    {
                        { "resultCode", "3000" },
                        { "resultMessage", "발급에 필요한 필수데이터가 누락되었습니다." }
                    };
                    break;
                case 3:
                    paramData = new JObject
                    {
                        { "resultCode", "3001" },
                        { "resultMessage", "등록된 환자정보가 없습니다." }
                    };
                    break;
                case 4:
                    paramData = new JObject
                    {
                        { "resultCode", "3002" },
                        { "resultMessage", "발급 가능한 증명서가 없습니다." }
                    };
                    break;
                case 5:
                    paramData = new JObject
                    {
                        { "resultCode", "400" },
                        { "resultMessage", "서버가 다운되었거나 시스템오류가 발생하였습니다." }
                    };
                    break;
                default:
                    paramData = new JObject
                    {
                        { "resultCode", "" }
                    };
                    break;
            }

            await Task.Delay(3000);

            return paramData;
        }
        /// <summary>
        /// 기간 조회 필요없는 수진이력 조회 
        /// </summary>
        /// <returns></returns>
        internal static async Task<JObject> CertSujinRequest()
        {
            var giwanNo = Properties.Settings.Default.GiwanNo;
            var uPatNo = DataManager.Instance.PatientInfo.PatientNo;
            var uName = DataManager.Instance.PatientInfo.Name;
            var uBirth = DataManager.Instance.PatientInfo.Birth;
            var certCd = DataManager.Instance.SelectedJob.CertCd;
            var certNe = DataManager.Instance.SelectedJob.CertNe;
            var hosCertCd = DataManager.Instance.SelectedJob.HosCertCd;

            var paramData = new JObject
            {
                { "command", "certSujinReq" },
                { "giwanNo", giwanNo },
                { "uPatNo", uPatNo },
                { "uName", uName },
                { "uBirth", uBirth },
                { "certCd", certCd },
                { "certNe", certNe },
                { "hosCertCd", hosCertCd },

            };

            var postdata = new JObject
            {
                {"reqData", KISA_SEED_CBC_DLL_Importer.Encrypt(paramData.ToString())},
                {"requester", "BBMC"}
            };

            return await WebServer.RequestJsonAsync(postdata.ToString(), $"{operationURL}/ModuleC.do", "post");
        }

        /// <summary>
        /// 수진이력 조회
        /// </summary>
        /// <param name="fromDate">조회 일자(시작일 yyyyMMdd)</param>
        /// <param name="toDate">조회 일자(종료일 yyyyMMdd)</param>
        /// <returns></returns>
        internal static async Task<JObject> CertSujinRequest(string fromDate, string toDate)
        {
            var giwanNo = Properties.Settings.Default.GiwanNo;
            var uPatNo = DataManager.Instance.PatientInfo.PatientNo;
            var uName = DataManager.Instance.PatientInfo.Name;
            var uBirth = DataManager.Instance.PatientInfo.Birth;
            var certCd = DataManager.Instance.SelectedJob.CertCd;
            var certNe = DataManager.Instance.SelectedJob.CertNe;
            var hosCertCd = DataManager.Instance.SelectedJob.HosCertCd;

            var paramData = new JObject
            {
                { "command", "certSujinReq" },
                { "giwanNo", giwanNo },
                { "uPatNo", uPatNo },
                { "uName", uName },
                { "uBirth", uBirth },
                { "certCd", certCd },
                { "certNe", certNe },
                { "hosCertCd", hosCertCd },
                { "fromDate", fromDate },
                { "toDate", toDate },
            };

            var postdata = new JObject
            {
                {"reqData", KISA_SEED_CBC_DLL_Importer.Encrypt(paramData.ToString())},
                {"requester", "BBMC"}
            };

            return await WebServer.RequestJsonAsync(postdata.ToString(), $"{operationURL}/ModuleC.do", "post");
        }

        /// <summary>
        /// 증명서 신청
        /// </summary>
        internal static async Task<JObject> CertRequest()
        {
            var giwanNo = Properties.Settings.Default.GiwanNo;
            var uPatNo = DataManager.Instance.PatientInfo.PatientNo;
            var uName = DataManager.Instance.PatientInfo.Name;
            var uBirth = DataManager.Instance.PatientInfo.Birth;
            var orderNo = DataManager.Instance.PaymentInfo.OrderNo;


            JArray certRequests = new JArray();

            for ( int i = 0; i < DataManager.Instance.CertRequestInfos.Count; i++)
            {
                if (!string.IsNullOrEmpty(DataManager.Instance.CertRequestInfos[i].StateCode))
                    continue;

                var certCd = DataManager.Instance.CertRequestInfos[i].Job.CertCd;
                var certNe = DataManager.Instance.CertRequestInfos[i].Job.CertNe;
                var hosCertCd = DataManager.Instance.CertRequestInfos[i].Job.HosCertCd;
                var certCnt = DataManager.Instance.CertRequestInfos[i].SujinHistroy.Count;
                var sujinKey = DataManager.Instance.CertRequestInfos[i].SujinHistroy.SujinKey;
                var deptNo = DataManager.Instance.CertRequestInfos[i].SujinHistroy.DeptNo;
                var deptNe = DataManager.Instance.CertRequestInfos[i].SujinHistroy.DeptNe;
                var certDate = DataManager.Instance.CertRequestInfos[i].SujinHistroy.CertDate;
                var inDate = DataManager.Instance.CertRequestInfos[i].SujinHistroy.InDate;
                var outDate = DataManager.Instance.CertRequestInfos[i].SujinHistroy.OutDate;

                var certRequestJson = new JObject
                {
                    { "reqSeq", i.ToString() },
                    { "certCd", certCd },
                    { "certNe", certNe },
                    { "hostCertCd", hosCertCd },
                    { "certCnt", certCnt },
                    { "sujinKey", sujinKey },
                    { "deptNo", deptNo },
                    { "deptNe", deptNe },
                    { "certDate", certDate },
                    { "inDate", inDate },
                    { "outDate", outDate }
                };

                certRequests.Add(certRequestJson);
            }

            var paramData = new JObject
            {
                { "command", "certReq" },
                { "giwanNo", giwanNo },
                { "uName", uName },
                { "uBirth", uBirth },
                { "uPatNo", uPatNo },
                { "orderNo", orderNo },
                { "list", certRequests},
            };

            var postdata = new JObject
            {
                {"reqData", KISA_SEED_CBC_DLL_Importer.Encrypt(paramData.ToString())},
                {"requester", "BBMC"}
            };

            Console.WriteLine(postdata.ToString());

            return await WebServer.RequestJsonAsync(postdata.ToString(), $"{operationURL}/ModuleC.do", "post");
        }

        internal static async Task<JObject> CertStateRequest(string certNo)
        {
            var giwanNo = Properties.Settings.Default.GiwanNo;

            var paramData = new JObject
            {
                { "command", "certState" },
                { "giwanNo", giwanNo },
                { "certNo", certNo }
            };

            var postdata = new JObject
            {
                {"reqData", KISA_SEED_CBC_DLL_Importer.Encrypt(paramData.ToString())},
                {"requester", "BBMC"}
            };

            return await WebServer.RequestJsonAsync(postdata.ToString(), $"{operationURL}/ModuleC.do", "post");
        }

        internal static async Task<JObject> PayRequest()
        {
            var giwanNo = Properties.Settings.Default.GiwanNo;
            var buyerName = DataManager.Instance.PaymentInfo.BuyerName;
            var buyerTel = DataManager.Instance.PaymentInfo.BuyerTel;
            var buyerEmail = DataManager.Instance.PaymentInfo.BuyerEmail;
            var orderNo = DataManager.Instance.PaymentInfo.OrderNo;
            var payMethod = DataManager.Instance.PaymentInfo.PayMethod;
            var resultCode = DataManager.Instance.PaymentInfo.ResultCode;
            var goodsName = DataManager.Instance.PaymentInfo.GoodsName;
            var amt = DataManager.Instance.PaymentInfo.Amt;

            Console.WriteLine($"orderNo {orderNo}");

            var paramData = new JObject
            {
                { "command", "certRes" },
                { "giwanNo", giwanNo },
                { "buyerName", buyerName },
                { "buyerTel", buyerTel },
                { "buyerEmail", buyerEmail },
                { "orderNo", orderNo },
                { "payMethod", payMethod },
                { "resultCode", resultCode },
                { "goodsName", goodsName },
                { "amt", amt }
            };

            var postdata = new JObject
            {
                {"reqData", KISA_SEED_CBC_DLL_Importer.Encrypt(paramData.ToString())},
                {"requester", "BBMC"}
            };

            return await WebServer.RequestJsonAsync(postdata.ToString(), $"{operationURL}/ModuleC.do", "post");
        }

        internal static async Task<JObject> CheckSujinHistory(string giwanNo,string certCd)
        {
            giwanNo = Properties.Settings.Default.GiwanNo;
            certCd = DataManager.Instance.SelectedJob.CertCd;
            var paramData = new JObject
            {
                { "command", "certForm" },
                { "giwanNo", giwanNo },
                { "certCd", certCd }

            };

            var postdata = new JObject
            {
                {"reqData", KISA_SEED_CBC_DLL_Importer.Encrypt(paramData.ToString())},
                {"requester", "BBMC"}
            };

            return await WebServer.RequestJsonAsync(postdata.ToString(), $"{operationURL}/ModuleC.do", "post");
        }

        internal static async Task<bool> PrintRequest(string tpid, string minno, string cnt, string jobNe)
        {
            var giwanNo = Properties.Settings.Default.GiwanNo;

            var urlCheck = "www.medcerti.com/hp1.0/jsp/report/senddocno.jsp";
            var urlFile = "www.medcerti.com/hp1.0/jsp/report/sendfile.jsp";
            var urlPost = "www.medcerti.com/hp1.0/jsp/report/printcomplete.jsp";

            var callurl = "http://127.0.0.1:65432/SSO";
            var areaurl = "http://127.0.0.1/SHOWREPORT_PRINTAUTO?" +
                      "URLCheck=" + urlCheck + "|" +
                      "URLFile=" + urlFile + "|" +
                      "URLPost=" + urlPost + "|" +
                      "TPID=" + tpid + "|" +
                      "MINNO=" + minno + "|" +
                      "Printable=Y|" +
                      "Copies=" + cnt + "|" +
                      "Printed=0|" +
                      "JOB_NE=" + jobNe + "|" +
                      "RECEIVE_TYPE=WEB|" +
                      "RECEIVE_TARGET=|" +
                      "KOR_YN=Y|" +
                      "ADV_YN=|" +
                      "GIWAN_NO=" + giwanNo + "|" +
                      "Param_1=|" +
                      "Param_2=|" +
                      "Param_3=|" +
                      "Param_4=|" +
                      "Param_5=|" +
                      "DisableInfoBox=Y|" +
                      "LEFT=-4000|" +
                      "TOP=207|" +
                      "WIDTH=600|" +
                      "HEIGHT=1000|" +
                      "ZOOM=85|" +
                      "Valid_Time=|" +
                      "Receiver=|" +
                      "Use=|" +
                      "DisableMsgBox=Y|" +
                      "UserAgent=Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";

            string ariadata = await KioskAgent.UseEncAria("10001", areaurl);

            var postData = $"PARAM=dzreportx:{ariadata}";

            var result = await WebServer.RequestStringAsync(postData, callurl, "post");

            if (string.IsNullOrWhiteSpace(result))
                return false;
            else
                return true;
        }
        #endregion
    }
}
