using Baccarat_Client_Manager.Tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TouchSocket.Http;
using Xabe.FFmpeg;

namespace Baccarat_Client_Manager
{
    public static class Program
    {
        public static Baccarat_Client_Manager mainWindow;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FFmpeg.Conversions.New();
            if (webHelper.connectServer())
            {
                Thread rt = new Thread(new ThreadStart(webHelper.requestThread));
                rt.IsBackground = true;
                rt.Start();
                mainWindow = new Baccarat_Client_Manager();
                Application.Run(mainWindow);
            }
        }
    }
}
