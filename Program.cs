using Baccarat_Server.Tools;
using Baccarat_Server.webApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TouchSocket.Http;
using TouchSocket.Sockets;

namespace Baccarat_Server
{

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            tools.initFS();
            postClassNewer.getInstance();
            //  eventDispatcher.AddEvent(eventType.onPost, new Action<ITcpClientBase, HttpContextEventArgs>(login.onLogin));
            // eventDispatcher.AddEvent(eventType.onPost, new Action<ITcpClientBase, HttpContextEventArgs>(login.onResetPassword));
            // eventDispatcher.AddEvent(eventType.onPost, new Action<ITcpClientBase, HttpContextEventArgs>(videoManager.onGetVideo));
            Application.Run(new Baccarat_Server());
        }
    }
}
