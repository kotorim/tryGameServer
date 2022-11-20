using Baccarat_Server.Tools;
using MongoDB.Bson;
using System;
using TouchSocket.Http;
using TouchSocket.Sockets;

namespace Baccarat_Server.webApi
{
    /// <summary>
    /// 请求基类
    /// </summary>
    public abstract class postClass
    {
        public postClass()
        {
            eventDispatcher.AddEvent<ITcpClientBase, HttpContextEventArgs, BsonDocument>(eventType.onPost, onPost);
            eventDispatcher.AddEvent<ITcpClientBase, HttpContextEventArgs>(eventType.onGet, onGet);
        }
        /// <summary>
        /// 当接受到post请求时
        /// </summary>
        /// <param name="client">请求的连接</param>
        /// <param name="e">请求的内容</param>
        protected virtual void onPost(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body) { }
        /// <summary>
        /// 当接受到get请求时
        /// </summary>
        /// <param name="client">请求的连接</param>
        /// <param name="e">请求的内容</param>
        protected virtual void onGet(ITcpClientBase client, HttpContextEventArgs e) { }


    }
    /// <summary>
    /// 请求类取得实例
    /// </summary>
    public class postClassNewer
    {
        private static postClassNewer _instance;
        public static postClassNewer getInstance()
        {
            if (null == _instance)
            {
                _instance = new postClassNewer();
            }
            return _instance;
        }
        public userManager login_Instance { get; private set; }
        public videoManager videoManager_Instance { get; private set; }
        public gameManager gameManager_Instance { get; private set; }
        public moneyManager moneyManager_Instance { get; private set; }
        public postClassNewer()
        {
            login_Instance = new userManager();
            videoManager_Instance = new videoManager();
            gameManager_Instance = new gameManager();
            moneyManager_Instance = new moneyManager();
            return;
        }
    }
}
