using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using TouchSocket.Core.Config;
using TouchSocket.Core.XREF.Newtonsoft.Json;
using TouchSocket.Http;
using TouchSocket.Sockets;

namespace Baccarat_Client_Manager.Tools
{
    public class requestStaff
    {
        public HttpRequest request;
        public Action<BsonDocument> onResponse;
        public requestStaff()
        {
            request = new HttpRequest();
        }
        public void send()
        {
            webHelper.requestQueue.Enqueue(this);
        }
    }
    public class webHelper
    {
        private static host host = JsonConvert.DeserializeObject<host>(new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\config.dat", Encoding.Default).ReadToEnd());
        public static HttpClient client;
        public static ConcurrentQueue<requestStaff> requestQueue = new ConcurrentQueue<requestStaff>();
        public static void requestThread()
        {
            requestStaff rStaff;
            while (true)
            {
                rStaff = null;
                requestQueue.TryDequeue(out rStaff);
                if (null != rStaff)
                {
                    if (false == client.Online)
                    {
                        if (false == connectServer())
                        {
                            MessageBox.Show("无法链接服务器");
                            Application.Exit();
                        }
                    }
                    else
                    {
                        var response = client.Request(rStaff.request, false, -1);
                        response.TryGetContent(out byte[] data);
                        rStaff.onResponse.Invoke(BsonSerializer.Deserialize<BsonDocument>(data));
                    }

                }
            }
        }
        public static bool connectServer()
        {
            try
            {
                client = new HttpClient();
                client.Setup(new TouchSocketConfig().UsePlugin().SetRemoteIPHost(new IPHost(IPAddress.Parse(host.IP), host.port)));
                client.Connect();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("\b无法连接到服务器\n\b 请与管理员联系");
                return false;
            }
        }
        private static void fastConnectServer()
        {
            if (false == client.Online)
            {
                if (false == connectServer())
                {
                    MessageBox.Show("无法连接服务器，程序即将自动退出");
                    Application.Exit();
                }
            }
        }
        public static requestStaff postRequest(KeyValuePair<string, string>[] header, string content)
        {
            fastConnectServer();
            requestStaff rStaff = new requestStaff();
            rStaff.request.SetHost(client.RemoteIPHost.Host).AsPost().SetContent(content).InitHeaders().SetHeader(HttpHeaders.ContentType, "application/json");
            for (int q = 0; q < header.Length; q++)
            {
                rStaff.request.SetHeader(header[q].Key, header[q].Value);
            }
            return rStaff;
        }
        public static requestStaff postRequest(KeyValuePair<string, string>[] header, byte[] content)
        {
            fastConnectServer();
            requestStaff rStaff = new requestStaff();
            rStaff.request.SetHost(client.RemoteIPHost.Host).AsPost().InitHeaders().SetHeader(HttpHeaders.ContentType, "bson");
            rStaff.request.SetContent(content);
            for (int q = 0; q < header.Length; q++)
            {
                rStaff.request.SetHeader(header[q].Key, header[q].Value);
            }
            return rStaff;
        }
        public static requestStaff getRequest(KeyValuePair<string, string>[] header, KeyValuePair<string, string>[] param = null)
        {
            fastConnectServer();
            requestStaff rStaff = new requestStaff();
            rStaff.request.SetHost(client.RemoteIPHost.Host).AsGet().InitHeaders();
            for (int q = 0; q < header.Length; q++)
            {
                rStaff.request.SetHeader(header[q].Key, header[q].Value);
            }
            if (null != param)
            {
                StringBuilder paramString = new StringBuilder();
                paramString.Append("/?");
                for (int w = 0; w < param.Length; w++)
                {
                    paramString.Append(param[w].Key + '=' + param[w].Value + '&');
                }
                paramString.Remove(paramString.Length - 1, 1);
                rStaff.request.SetUrl(paramString.ToString());
            }
            return rStaff;
        }
        public static DateTime FromUnixTime(long unixTime, bool isUTC = false)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            epoch = epoch.AddMilliseconds(unixTime);
            if (true == isUTC)
            {
                epoch = epoch.AddHours(8d);
            }
            return epoch;
        }
        public static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalMilliseconds);
        }
        //public static HttpResponse postRequest(KeyValuePair<string, string>[] header, string content)
        //{
        //    if (false == client.Online)
        //    {
        //        if (false == connectServer())
        //        {
        //            MessageBox.Show("无法连接服务器，程序即将自动退出");
        //            Application.Exit();
        //        }
        //    }
        //    HttpRequest request = new HttpRequest();
        //    request.SetHost(client.RemoteIPHost.Host).AsPost().SetContent(content).InitHeaders().SetHeader(HttpHeaders.ContentType, "application/json");
        //    for (int q = 0; q < header.Length; q++)
        //    {
        //        request.SetHeader(header[q].Key, header[q].Value);
        //    }
        //    var response = client.Request(request, timeout: 100000);
        //    return response;
        //}
        //public static HttpResponse postRequest(KeyValuePair<string, string>[] header, byte[] content)
        //{
        //    if (false == client.Online)
        //    {
        //        if (false == connectServer())
        //        {
        //            Application.Exit();
        //        }
        //    }
        //    HttpRequest request = new HttpRequest();
        //    request.SetHost(client.RemoteIPHost.Host).AsPost().InitHeaders().SetContent(content);
        //    for (int q = 0; q < header.Length; q++)
        //    {
        //        request.SetHeader(header[q].Key, header[q].Value);
        //    }
        //    var response = client.Request(request, timeout: 100000);
        //    return response;
        //}
        //public static HttpResponse getRequest(KeyValuePair<string, string>[] header, KeyValuePair<string, string>[] param)
        //{
        //    if (false == client.Online)
        //    {
        //        if (false == connectServer())
        //        {
        //            Application.Exit();
        //        }
        //    }
        //    HttpRequest request = new HttpRequest();
        //    StringBuilder paramString = new StringBuilder();
        //    paramString.Append("/?");
        //    for (int w = 0; w < param.Length; w++)
        //    {
        //        paramString.Append(param[w].Key + '=' + param[w].Value + '&');
        //    }
        //    paramString.Remove(paramString.Length - 1, 1);
        //    request.SetUrl(paramString.ToString());
        //    request.SetHost(client.RemoteIPHost.Host).AsGet().InitHeaders();
        //    for (int q = 0; q < header.Length; q++)
        //    {
        //        request.SetHeader(header[q].Key, header[q].Value);
        //    }
        //    var response = client.Request(request, timeout: 100000);
        //    return response;
        //}
    }
    public class host
    {
        public string IP { set; get; }
        public int port { set; get; }
    }
}
