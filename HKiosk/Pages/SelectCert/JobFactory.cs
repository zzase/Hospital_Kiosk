using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using HKiosk.Util;
using HKiosk.Manager.Navigation;

namespace HKiosk.Pages.SelectCert
{
    class JobFactory
    {
        private void ParseJson(String json)
        {
            var jsonArrayString = MakeJArray().ToString();
            try
            {
                JArray array = JArray.Parse(jsonArrayString);

                for (int i = 0; i < array.Count; i++)
                {
                    dynamic data = JObject.Parse(array[i].ToString());
                    string finalCertNe = "";

                    //if (data["certNe"].ToString().Contains("("))
                    //{
                    //    string[] editCertNe = data["certNe"].ToString().Split('(');
                    //    finalCertNe = editCertNe[0] + "\r\n(" + editCertNe[1];
                    //}

                    //else
                    //{
                        finalCertNe = data["certNe"].ToString();
                    //}
 
                    Certs.Add(new Job()
                    {
                        CertCd = data["certCd"].ToString(),
                        CertNe = finalCertNe,
                        HostCertCd = data["hostCertCd"].ToString(),
                        Price = data["price"].ToString(),
                        KorYN = data["korYN"].ToString(),
                        SelectCommand = new Command(
                            delegate(Object obj) 
                            { 
                                NavigationManager.Navigate(PageElement.SelectHistory); 
                            }
                            )
                            
                });

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error");
            }
        }

        //public async Task<string> RequestJson()
        //{
        //    string url = "http://www.objgen.com/json/models/ReCB3";
        //    HttpClient client = new HttpClient();
        //    Task<string> getStringTask = client.GetStringAsync(url);
        //    json = await getStringTask;

        //    return json;
        //}

        private JArray MakeJArray()
        {
            JArray list = new JArray();

            var json1 = new JObject();
            json1.Add("certCd", "10101");
            json1.Add("certNe", "진단서 사본");
            json1.Add("hostCertCd", "CA006");
            json1.Add("price", "1,000원");
            json1.Add("korYN", "Y");

            var json2 = new JObject();
            json2.Add("certCd", "10102");
            json2.Add("certNe", "소견서 사본");
            json2.Add("hostCertCd", "CA006");
            json2.Add("price", "1,000원");
            json2.Add("korYN", "Y");

            var json3 = new JObject();
            json3.Add("certCd", "10103");
            json3.Add("certNe", "입퇴원확인서");
            json3.Add("hostCertCd", "CA006");
            json3.Add("price", "1,000원");
            json3.Add("korYN", "Y");

            var json4 = new JObject();
            json4.Add("certCd", "10104");
            json4.Add("certNe", "세부내역서(입원,외래,응급)");
            json4.Add("hostCertCd", "CA006");
            json4.Add("price", "1,000원");
            json4.Add("korYN", "Y");

            var json5 = new JObject();
            json5.Add("certCd", "10105");
            json5.Add("certNe", "진료비 영수증(입원,외래,응급)");
            json5.Add("hostCertCd", "CA006");
            json5.Add("price", "1,000원");
            json5.Add("korYN", "Y");

            var json6 = new JObject();
            json6.Add("certCd", "10106");
            json6.Add("certNe", "납입증명서(연말정산용)");
            json6.Add("hostCertCd", "CA006");
            json6.Add("price", "1,000원");
            json6.Add("korYN", "Y");

            var json7 = new JObject();
            json7.Add("certCd", "10106");
            json7.Add("certNe", "납입증명서(연말정산용)");
            json7.Add("hostCertCd", "CA006");
            json7.Add("price", "1,000원");
            json7.Add("korYN", "Y");

            list.Add(json1);
            list.Add(json2);
            list.Add(json3);
            list.Add(json4);
            list.Add(json5);
            list.Add(json6);
            //list.Add(json7);


            return list;
        }

       

        public string Json
        {
            get;
            private set;
        }

        public List<Job> Certs
        {
            get;
            private set;
        }

        public JobFactory()
        {
            Certs = new List<Job>();
            string json = MakeJArray().ToString();
            ParseJson(json);
        }

    }
}
