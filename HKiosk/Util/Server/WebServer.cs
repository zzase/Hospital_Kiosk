using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Util.Server
{
    public static class WebServer
    {
        /// <summary>
        /// Http 요청 (동기)
        /// </summary>
        /// <param name="data">요청 데이터</param>
        /// <param name="url">URL</param>
        /// <param name="method">HTTP 요청 방법</param>
        /// <returns></returns>
        public static JObject Request(string data, string url, string method)
        {
            byte[] bytes = Encoding.Default.GetBytes(data);
            string response = string.Empty;
            JObject result = null;

            try
            {
                // Post
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = method;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.AllowWriteStreamBuffering = false;

                using (Stream reqStream = httpWebRequest.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                }

                using (WebResponse resp = httpWebRequest.GetResponse())
                {
                    using (Stream respStream = resp.GetResponseStream())
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        response = sr.ReadToEnd();
                    }
                }

                result = JObject.Parse(response);
            }
            catch (Exception e)
            {
                Log.Write($"Http 동기요청 예외발생 : {e.ToString()} URL:{url} DATA:{data}");
            }

            return result;
        }

        /// <summary>
        /// Http 요청 (비동기)
        /// </summary>
        /// <param name="data">요청 데이터</param>
        /// <param name="url">URL</param>
        /// <param name="method">HTTP 요청 방법</param>
        /// <param name="usearia">암호화 모듈 사용</param>
        /// <returns></returns>
        public async static Task<JObject> RequestAsync(string data, string url, string method, bool usearia)
        {
            byte[] bytes = Encoding.Default.GetBytes(data);
            string response = string.Empty;
            JObject result = null;

            try
            {
                // Post
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = method;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.AllowWriteStreamBuffering = false;

                Stream reqStream = await httpWebRequest.GetRequestStreamAsync();
                await reqStream.WriteAsync(bytes, 0, bytes.Length);
                reqStream.Dispose();

                using (HttpWebResponse resp = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
                {
                    using (Stream respStream = resp.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(respStream))
                        {
                            response = sr.ReadToEnd();
                        }
                    }
                }

                response = string.IsNullOrEmpty(response) ? "" : response.Trim();

                //암호화 모듈
                if (!string.IsNullOrEmpty(response))
                {
                    if (!response.Contains("{"))
                    {
                        if (usearia)
                        {
                            string dec = await KioskAgent.UseDecAria("10001", response);
                            response = dec.Replace("˝", "\"");
                        }
                    }
                }

                result = JObject.Parse(response);
            }
            catch (Exception e)
            {
                Log.Write($"#### RequestAsync : [{data}], [{url}], [{method}], [{usearia}], [{response}]");
                Log.Write($"Http 비동기요청 예외발생 : {e.ToString()} URL:{url} DATA:{data}");
            }

            return result;
        }

        public static async Task<string> Get(string url)
        {
            string response = string.Empty;

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "get";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.AllowWriteStreamBuffering = false;

                using (WebResponse resp = await httpWebRequest.GetResponseAsync())
                {
                    using (Stream respStream = resp.GetResponseStream())
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        response = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write($"WebServer Get 예외발생 : {e.ToString()} URL:{url}");
            }

            return response;
        }
    }
}
