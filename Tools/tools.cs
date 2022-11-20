using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baccarat_Server.Tools
{
    public static class tools
    {
        public static Random random = new Random(DateTime.Now.Day);
        private static FileStream fs;
        private static ConcurrentQueue<string> fsQueue = new ConcurrentQueue<string>();
        public static void initFS()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Files\\logs\\" + "log" + DateTime.UtcNow.ToString("yyyy.MM") + ".dat";
            if (!File.Exists(path))
            {
                fs = File.OpenWrite(path);
                fs.Write(new byte[] { 0xef, 0xbb, 0xbf }, 0, 3);
            }
            else
            {
                fs = File.OpenWrite(path);
            }
            fs.Seek(fs.Length, SeekOrigin.Current);
            eventDispatcher.AddEvent(eventType.onTimeTick, writeLog);
            Application.ApplicationExit += new EventHandler((a, b) =>
            {

                StringBuilder outer = new StringBuilder();
                log("服务器已下线");
                string[] tmp = fsQueue.ToArray();
                for (int q = 0; q < tmp.Length; q++)
                {
                    outer.Append(tmp[q]);
                }
                outer.Append("\n\n-----------------------------------\n");
                byte[] tmpbyte = Encoding.UTF8.GetBytes(outer.ToString());
                //fs = File.OpenWrite(path);
                //  fs.Seek(fs.Length, SeekOrigin.Current);
                fs.Write(tmpbyte, 0, tmpbyte.Length);
                fs.Close();
            });
            // fs.Close();

        }
        private static void writeLog()
        {
            if (!fsQueue.IsEmpty)
            {
                fsQueue.TryDequeue(out string result);
                if (!string.IsNullOrEmpty(result))
                {
                    byte[] tmp = Encoding.UTF8.GetBytes(result);
                    fs.WriteAsync(tmp, 0, tmp.Length);
                    Console.WriteLine("log:" + result);
                }
            }
        }
        /// <summary>
        /// 记录信息到文件（Files/logs)
        /// </summary>
        /// <param name="inner">一行信息</param>
        /// <returns>返回本身信息(用于串联其他输出)</returns>
        public static string log(string inner)
        {
            fsQueue.Enqueue("\n[" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "]" + inner);
            return inner;
        }
        /// <summary>
        /// 记录信息到文件（Files/logs)
        /// </summary>
        /// <param name="inner">一行信息</param>
        /// <returns>返回本身信息(用于串联其他输出)</returns>
        public static string logError(string inner)
        {
            fsQueue.Enqueue("\n[" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "][ERROR]" + inner);
            return inner;
        }
        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTime);
        }

        public static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalMilliseconds);
        }
    }
}
