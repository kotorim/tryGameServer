using Baccarat_Server.Tools;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;
using TouchSocket.Http;
using TouchSocket.Sockets;
using TouchSocket.Http.WebSockets;
using TouchSocket.Core.Plugins;
using TouchSocket.Core.Log;
using TouchSocket.Core.Config;
using TouchSocket.Http.Plugins;
using TouchSocket.Core.Dependency;
using System.Threading.Tasks;
using System.Collections.Generic;
using TouchSocket.Core;

namespace Baccarat_Server
{
    /// <summary>
    /// 网络功能
    /// </summary>
    public static class webHelper
    {
        /// <summary>
        /// 服务端实例
        /// </summary>
        private static HttpService service;
        /// <summary>
        /// 数据库实例
        /// </summary>
        public static MongoClient dbClient { get; private set; }
        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="inner">ip地址</param>
        public static void startService(IPHost[] inner)
        {
            service = new HttpService();
            var config = new TouchSocketConfig();
            config.UsePlugin().SetReceiveType(ReceiveType.Auto).SetListenIPHosts(inner);
            service.Setup(config).Start();
            //service.AddPlugin<WebSocketServerPlugin>()//添加WebSocket功能
            //      .SetWSUrl("/ws")
            //      .SetCallback(WSCallback);//WSCallback回调函数是在WS收到数据时触发回调的。
            //   service.AddPlugin<MyWebSocketPlugin>();
            // service.AddPlugin<MyWSCommandLinePlugin>();
            service.AddPlugin<httpPlugin>();
            config.Container.RegisterTransient<ILog, ConsoleLogger>();
            MessageBox.Show(tools.log("服务器已上线"));
        }
        /// <summary>
        /// 断开服务端
        /// </summary>
        public static void disposeService()
        {
            if (null != service)
            {
                service.Dispose();
                MessageBox.Show("服务器已下线");
            }
        }
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="innerAddress">ip地址</param>
        /// <returns>是否成功连接</returns>
        public static bool connectDatabase(string innerAddress)
        {
            try
            {
                var client = new HttpClient();
                client.Setup(new TouchSocketConfig().UsePlugin().SetRemoteIPHost(new IPHost(innerAddress))).Connect();
                HttpRequest req = new HttpRequest();
                req.SetHost(client.RemoteIPHost.Host).AsGet().InitHeaders();
                var res = client.Request(req);
                string a = res.GetBody();
                if (string.Equals(res.GetBody(), "It looks like you are trying to access MongoDB over HTTP on the native driver port.\r\n"))
                {
                    dbClient = new MongoClient(innerAddress);
                }
                else
                {
                    throw new Exception("无法访问到数据库，请检查链接");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(tools.logError(ex.Message));
                return false;
            }
        }
        /// <summary>
        /// 不管他
        /// </summary>
        /// <param name="client"></param>
        /// <param name="e"></param>
        static void WSCallback(ITcpClientBase client, WSDataFrameEventArgs e)
        {
            switch (e.DataFrame.Opcode)
            {
                case WSDataType.Cont:
                    Console.WriteLine($"收到中间数据，长度为：{e.DataFrame.PayloadLength}");
                    break;
                case WSDataType.Text:
                    Console.WriteLine(e.DataFrame.ToText());
                    ((HttpSocketClient)client).SendWithWS(e.DataFrame.ToText());
                    break;
                case WSDataType.Binary:
                    if (e.DataFrame.FIN)
                    {
                        Console.WriteLine($"收到二进制数据，长度为：{e.DataFrame.PayloadLength}");
                    }
                    else
                    {
                        Console.WriteLine($"收到未结束的二进制数据，长度为：{e.DataFrame.PayloadLength}");
                    }
                    break;
                case WSDataType.Close:
                    {
                        Console.WriteLine("远程请求断开");
                        client.Close("断开");
                    }

                    break;
                case WSDataType.Ping:
                    break;
                case WSDataType.Pong:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 向数据库添加
        /// </summary>
        /// <typeparam name="T">添加的类型</typeparam>
        /// <param name="dataBaseName">数据库名</param>
        /// <param name="collectionName">组名</param>
        /// <param name="target">添加的对象</param>
        public static async void dbAdd<T>(string dataBaseName, string collectionName, T target)
        {
            //获取表
            IMongoCollection<T> collection = dbClient.GetDatabase(dataBaseName).GetCollection<T>(collectionName);
            //插入
            await collection.InsertOneAsync(target);
        }
        /// <summary>
        /// 从数据库搜索
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dataBaseName">数据库名</param>
        /// <param name="collectionName">组名</param>
        /// <param name="func">查询内容</param>
        /// <returns>搜寻结果</returns>
        public static IFindFluent<T, T> dbSearch<T>(string dataBaseName, string collectionName, FilterDefinition<T> func)
        {
            IMongoCollection<T> collection = dbClient.GetDatabase(dataBaseName).GetCollection<T>(collectionName);
            var outer = collection.Find<T>(func);
            return outer;
        }
        /// <summary>
        /// 向数据库更新
        /// </summary>
        /// <typeparam name="T">添加的类型</typeparam>
        /// <param name="dataBaseName">数据库名</param>
        /// <param name="collectionName">组名</param>
        /// <param name="targetFilter">搜索结果</param>
        /// <param name="updateFilter">向目标更新的内容</param>
        public static Task<UpdateResult> dbUpdate<T>(string dataBaseName, string collectionName, FilterDefinition<T> targetFilter, UpdateDefinition<T> updateFilter, bool multy = false)
        {
            IMongoCollection<T> collection = dbClient.GetDatabase(dataBaseName).GetCollection<T>(collectionName);
            return multy ? collection.UpdateManyAsync(targetFilter, updateFilter) : collection.UpdateOneAsync(targetFilter, updateFilter);
        }
        public static Task<ReplaceOneResult> dbSave<T>(string dataBaseName, string collectionName, FilterDefinition<T> func, T newTarget)
        {
            IMongoCollection<T> collection = dbClient.GetDatabase(dataBaseName).GetCollection<T>(collectionName);
            return collection.ReplaceOneAsync(func, newTarget);
        }
        public static Task<long> dbCounter<T>(string dataBaseName, string collectionName, FilterDefinition<T> counterFilter)
        {
            IMongoCollection<T> collection = dbClient.GetDatabase(dataBaseName).GetCollection<T>(collectionName);
            return collection.CountDocumentsAsync(counterFilter);
        }
        public static void tryAddArrayValue(string name, BsonDocument target, BsonValue value)
        {
            if (target.Contains(name))
            {
                if (target.GetValue(name).IsBsonNull)
                {
                    target.Set(name, BsonValue.Create(new BsonArray { value }));

                }
                else
                {
                    target.GetValue(name).AsBsonArray.Add(value);
                }
            }
            else
            {
                Dictionary<string, BsonArray> kv = new Dictionary<string, BsonArray>();
                kv.Add(name, new BsonArray { value });
                target.AddRange(kv);
            }
        }
        public static BsonDocument getBody(HttpContextEventArgs e)
        {
            string type = e.Context.Request.GetHeader(HttpHeaders.ContentType);
            switch (type)
            {
                case "application/json":
                    return BsonDocument.Parse(e.Context.Request.GetBody());
                case "bson":
                    e.Context.Request.TryGetContent(out byte[] content);
                    if (null == content)
                    {
                        Console.WriteLine(tools.logError("无法解析的body类型:" + type));
                        return null;
                    }
                    return MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(content);
                default:
                    Console.WriteLine(tools.logError("无法解析的body类型:" + type));
                    return null;
            }
        }
    }
    public class httpPlugin : HttpPluginBase
    {
        protected override void OnConnected(ITcpClientBase client, TouchSocketEventArgs e)
        {
            eventDispatcher.dispatchEvent(eventType.onConnected, client);
        }
        protected override void OnDisconnected(ITcpClientBase client, ClientDisconnectedEventArgs e)
        {
            eventDispatcher.dispatchEvent(eventType.onDisconnected, client);
        }
        protected override void OnGet(ITcpClientBase client, HttpContextEventArgs e)
        {
            //    if (e.Context.Request.UrlEquals("/success"))
            //    {
            //        e.Context.Response.FromText("Success").Answer();//直接回应
            //        Console.WriteLine("处理完毕");
            //        e.Handled = true;
            //    }
            //    else if (e.Context.Request.UrlEquals("/file"))
            //    {
            //        e.Context.Response
            //            .SetStatus()//必须要有状态
            //            .FromFile(@"D:\System\Windows.iso", e.Context.Request);//直接回应文件。
            //    }
            e.Context.Response.SetHeader("Access-Control-Allow-Origin", "*");
            try
            {
                eventDispatcher.dispatchEvent(eventType.onGet, client, e);
                if (e.Context.Request.Client.Online)
                {
                    if (e.Context.Response.ContentLength <= 0)
                    {
                        e.Context.Response.SetContent(responseDoc.success(-1).ToBson());
                    }
                    e.Context.Response.SetStatus();
                    e.Context.Response.Answer();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(tools.logError(ex.Message));
            }
            base.OnGet(client, e);
        }
        protected override void OnPost(ITcpClientBase client, HttpContextEventArgs e)
        {
            // Console.WriteLine("收到Post请求,派发Post事件");
            e.Context.Response.SetHeader("Access-Control-Allow-Origin", "*");
            // e.Context.Response.SetHeader("Content-Type", "bson");
            try
            {
                eventDispatcher.dispatchEvent(eventType.onPost, client, e, webHelper.getBody(e));
                if (e.Context.Request.Client.Online)
                {
                    if (e.Context.Response.ContentLength <= 0)
                    {
                        e.Context.Response.SetContent(responseDoc.success(-1).ToBson());
                    }
                    e.Context.Response.SetStatus();
                    e.Context.Response.Answer();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(tools.logError(ex.InnerException.Message));
            }
            base.OnPost(client, e);
        }
        protected override void OnReceivedData(ITcpClientBase client, ReceivedDataEventArgs e)
        {
            Console.WriteLine(e);
            base.OnReceivedData(client, e);
        }
        protected override void OnReceivedOtherHttpRequest(ITcpClientBase client, HttpContextEventArgs e)
        {
            Console.WriteLine(e);
            base.OnReceivedOtherHttpRequest(client, e);
        }
    }
    public static class responseDoc
    {
        public static BsonDocument success(int successCode = 0)
        {
            return new BsonDocument(new BsonElement("success", successCode));
        }
    }
}
