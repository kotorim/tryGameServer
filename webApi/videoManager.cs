using Baccarat_Server.Tools;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TouchSocket.Core.XREF.Newtonsoft.Json;
using TouchSocket.Http;
using TouchSocket.Sockets;

namespace Baccarat_Server.webApi
{
    /// <summary>
    /// 视频管理
    /// </summary>
    public class videoManager : postClass
    {
        /// <summary>
        /// 频道
        /// </summary>
        public List<videoChannel> channels { get; private set; } = new List<videoChannel>();
        /// <summary>
        /// 主要视频池
        /// </summary>
        public List<string> mainVideos { get; private set; }
        /// <summary>
        /// 间隙视频池
        /// </summary>
        public List<string> napVideos { get; private set; }
        public videoManager()
        {
            refreshMainVideo();
            refreshNapVideo();
            for (int q = 0; q < 6; q++)
            {
                channels.Add(new videoChannel());
            }
        }
        protected override void onGet(ITcpClientBase client, HttpContextEventArgs e)
        {
            e.Context.Request.Headers.TryGetValue("flag", out string flag);
            if (!string.IsNullOrEmpty(flag))
            {
                switch (flag)
                {
                    case "getchannelnum":
                        getChannelnum(client, e);
                        break;
                    case "getchannelmessage":
                        getChannelMessage(client, e);
                        break;
                    case "getvideolist":
                        getVideoList(client, e);
                        break;
                    default: break;
                }
            }
            else
            {
                if (string.Equals(e.Context.Request.RelativeURL, "/files"))
                {
                    getFile(client, e);
                }
                //   e.Context.Request.
                //string[] url = e.Context.Request.URL.Split('/');
                //if (0 < url.Length)
                //{
                //    string[] url1 = url[1].Split('?');
                //    if (0 < url)
                //        switch ([0])
                //        {
                //            case "files":
                //                getFile(client, e);
                //                break;
                //        }
                //}
            }
            //e.Context.Response.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Files\\napVideo\\1.mp4", null);
        }
        protected override void onPost(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            e.Context.Request.Headers.TryGetValue("flag", out string flag);
            if (!string.IsNullOrEmpty(flag))
            {
                switch (flag)
                {
                    case "uploadvideo":
                        uploadVideo(client, e, body);
                        break;
                    default: break;
                }
            }
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="client"></param>
        /// <param name="e"></param>
        private void getFile(ITcpClientBase client, HttpContextEventArgs e)
        {
            try
            {
                e.Context.Request.TryGetQuery("type", out string type);
                e.Context.Request.TryGetQuery("name", out string name);
                if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(name))
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Files\\" + type + "\\" + name))
                    {
                        e.Context.Response.SetStatus().FromFile(AppDomain.CurrentDomain.BaseDirectory + "Files\\" + type + "\\" + name, e.Context.Request);
                    }
                    else
                    {
                        tools.logError("取得文件失败,检查是否存在相应的文件：Files\\" + type + "\\" + name);
                    }
                }
                else
                {
                    tools.logError("取得文件失败,访问形式错误：type:" + type + " name:" + name);
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 取得频道总数
        /// </summary>
        /// <param name="client"></param>
        /// <param name="e"></param>
        private void getChannelnum(ITcpClientBase client, HttpContextEventArgs e)
        {
            e.Context.Response.SetContent("{\"success\":0,\"channelnum\":" + channels.Count + "}".ToBson());
        }
        /// <summary>
        /// 取得频道信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="e"></param>
        private void getChannelMessage(ITcpClientBase client, HttpContextEventArgs e)
        {
            e.Context.Request.TryGetQuery("id", out string id);
            BsonDocument document = new BsonDocument();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    document = this.channels[int.Parse(id) - 1].playingVideo.Clone().AsBsonDocument;
                    document.Add("currentTime", this.channels[int.Parse(id) - 1].timeStep);
                    document.Add("success", 0);
                    e.Context.Response.SetContent(document.ToBson());
                    return;
                }
            }

            catch (Exception ex)
            {
                throw new Exception("取得频道失败,检查是否存在该频道:" + id + " 错误描述:" + ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 取得视频名字列表
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="e"></param>
        private void getVideoList(ITcpClientBase client, HttpContextEventArgs e)
        {
            e.Context.Request.TryGetQuery("isMain", out string isMain);
            BsonDocument doc = new BsonDocument();
            if ("true" == isMain)
            {
                if (null != this.mainVideos)
                {
                    e.Context.Request.TryGetQuery("startIndex", out string startIndex_S);
                    e.Context.Request.TryGetQuery("number", out string number_S);
                    int startIndex = int.Parse(startIndex_S);
                    int number = int.Parse(number_S);
                    doc.Add("couldPrev", (startIndex - number) < 0 ? false : true);
                    doc.Add("couldNext", (startIndex + number) > this.mainVideos.Count ? false : true);
                    // doc.Add("maxValue", this.mainVideos.Count);
                    BsonArray nameArray = new BsonArray();
                    int length = doc.GetValue("couldNext").AsBoolean ? startIndex + number : this.mainVideos.Count;
                    int q = startIndex;
                    for (; q < length; q++)
                    {
                        nameArray.Add(this.mainVideos[q]);
                    }
                    doc.Add("indexNum", q > this.mainVideos.Count - 1 ? this.mainVideos.Count - 1 + number : q);
                    doc.Add("nameArray", nameArray);
                    e.Context.Response.SetContent(doc.ToBson());
                }
                throw new Exception("没有主要视频列表");
            }
            else
            {
                if (null != this.napVideos)
                {
                    e.Context.Request.TryGetQuery("startIndex", out string startIndex_S);
                    e.Context.Request.TryGetQuery("number", out string number_S);
                    int startIndex = int.Parse(startIndex_S);
                    int number = int.Parse(number_S);
                    doc.Add("couldPrev", (startIndex - number) < 0 ? false : true);
                    doc.Add("couldNext", (startIndex + number) > this.napVideos.Count ? false : true);
                    // doc.Add("maxValue", this.mainVideos.Count);
                    BsonArray nameArray = new BsonArray();
                    int length = doc.GetValue("couldNext").AsBoolean ? startIndex + number : this.napVideos.Count;
                    int q = startIndex;
                    for (; q < length; q++)
                    {
                        nameArray.Add(this.napVideos[q]);
                    }
                    doc.Add("indexNum", q > this.napVideos.Count - 1 ? this.napVideos.Count - 1 + number : q);
                    doc.Add("nameArray", nameArray);
                    e.Context.Response.SetContent(doc.ToBson());
                }
                throw new Exception("没有间歇视频列表");
            }
        }
        /// <summary>
        /// 上传视频
        /// </summary>
        /// <param name="client"></param>
        /// <param name="e"></param>
        private void uploadVideo(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            body.TryGetValue("name", out BsonValue value);
            try
            {
                var res = webHelper.dbSearch("Baccarat", "videomessage", Builders<BsonDocument>.Filter.Eq("name", value)).FirstAsync().Result;
                //   e.Context.Response.SetContent("{\"success\":1}".ToBson());
                e.Context.Response.SetContent(responseDoc.success(1).ToBson());
            }
            catch (Exception)
            {
                try
                {
                    body.TryGetValue("timeStamps", out BsonValue timeStamps);
                    if (null == timeStamps)
                    {
                        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "Files\\napVideo\\" + value, body.GetValue("File").AsByteArray);
                        this.napVideos.Add(value.AsString);
                        body.Remove("File");

                    }
                    else
                    {
                        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "Files\\mainVideo\\" + value, body.GetValue("File").AsByteArray);
                        this.mainVideos.Add(value.AsString);
                        body.Remove("File");

                    }
                    webHelper.dbAdd("Baccarat", "videomessage", body);
                    e.Context.Response.SetContent(responseDoc.success(0).ToBson());
                }
                catch (Exception ex)
                {
                    throw ex;
                    //MessageBox.Show(tools.logError(ex.InnerException.Message));
                }
            }
        }
        /// <summary>
        /// 刷新主视频池
        /// </summary>
        private void refreshMainVideo()
        {
            string[] allName = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Files\\mainVideo");
            if (allName.Length <= 0)
            {
                MessageBox.Show(tools.logError("路径文件丢失"));
                Application.Exit();
            }
            mainVideos = new List<string>();
            for (int q = 0; q < allName.Length; q++)
            {
                string[] split = allName[q].Split(new char[] { '\\', '\\' });
                mainVideos.Add(split[split.Length - 1]);
            }
        }
        /// <summary>
        /// 刷新休息时间视频池
        /// </summary>
        private void refreshNapVideo()
        {
            string[] allName = Directory.GetFileSystemEntries(AppDomain.CurrentDomain.BaseDirectory + "Files\\napVideo\\");
            if (allName.Length <= 0)
            {
                MessageBox.Show(tools.logError("路径文件丢失"));
                Application.Exit();
            }
            this.napVideos = new List<string>();
            for (int q = 0; q < allName.Length; q++)
            {
                string[] split = allName[q].Split(new char[] { '\\', '\\' });
                napVideos.Add(split[split.Length - 1]);
            }
        }
        /// <summary>
        /// 播放视频（取得视频名字)
        /// </summary>
        /// <param name="inner">标记号</param>
        /// <returns>视频序号</returns>
        public string getMainVideo(int inner)
        {
            return mainVideos[inner];
        }
        public string getNapVideo()
        {
            return napVideos[tools.random.Next(0, napVideos.Count)];
        }
        /// <summary>
        /// 检查该序号视频是否为重复的
        /// </summary>
        /// <param name="inner"></param>
        /// <returns></returns>
        public bool checkRecurring(int inner)
        {
            for (int q = 0; q < channels.Count; q++)
            {
                if (channels[q].playingVideoId == inner)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class videoChannel
    {
        public BsonDocument playingVideo { get; private set; } = null;
        public bool isMain { get; private set; } = true;
        public int timeStep { get; private set; } = 0;
        public int playingVideoId { get; private set; }
        public int[] randomNameArray;
        public int nameArrayCounter = 0;
        public videoChannel()
        {
            eventDispatcher.AddEvent(eventType.onTimeTick, this.tick);
        }
        /// <summary>
        /// 刷新随机视频池
        /// </summary>
        private void refreshRandomArray()
        {
            this.nameArrayCounter = 0;
            this.randomNameArray = new int[postClassNewer.getInstance().videoManager_Instance.mainVideos.Count];
            for (int q = 0; q < randomNameArray.Length; q++)
            {
                randomNameArray[q] = q;
                int w = tools.random.Next(0, q);
                randomNameArray[w] = randomNameArray[w] ^ randomNameArray[q];
                randomNameArray[q] = randomNameArray[w] ^ randomNameArray[q];
                randomNameArray[w] = randomNameArray[w] ^ randomNameArray[q];
            }
        }
        /// <summary>
        /// 每秒一次的时间跳动
        /// </summary>
        private void tick()
        {
            if (null == playingVideo)
            {
                nextVideo();
            }
            else if (timeStep >= playingVideo.GetValue("videoDuration").AsInt32)
            {
                nextVideo();
            }
            else
            {
                timeStep++;
            }
        }
        /// <summary>
        /// 切换视频
        /// </summary>
        private void nextVideo()
        {
            if (null == randomNameArray)
            {
                refreshRandomArray();
            }
            string name;
            if (true == isMain)
            {
                for (; nameArrayCounter < randomNameArray.Length - 1; nameArrayCounter++)
                {
                    if (nameArrayCounter == randomNameArray.Length - 1)
                    {
                        refreshRandomArray();
                    }
                    else if (false == postClassNewer.getInstance().videoManager_Instance.checkRecurring(randomNameArray[nameArrayCounter]))
                    {
                        break;
                    }
                }
                this.playingVideoId = randomNameArray[nameArrayCounter];
                name = postClassNewer.getInstance().videoManager_Instance.getMainVideo(randomNameArray[nameArrayCounter]);
            }
            else
            {
                name = postClassNewer.getInstance().videoManager_Instance.getNapVideo();
            }
            try
            {
                var result = webHelper.dbSearch("Baccarat", "videomessage", Builders<BsonDocument>.Filter.Eq("name", name));
                playingVideo = result.FirstAsync().Result;
                isMain = !isMain;
                timeStep = 0;
                // timeStep = playingVideo.GetValue("videoDuration").AsInt32 - 20;
            }
            catch (Exception ex)
            {
                tools.logError("取得视频信息失败,检查是否存在该段视频:" + name + " 错误描述:" + ex.InnerException.Message);
                throw ex;
            }
        }
    }
    //public class videoMes
    //{
    //    public string name;
    //    public int videoDuration;
    //    public timeStamp[] timeStamps;
    //}
    //public class timeStamp
    //{
    //    public double VideoTimeInJason;
    //    public cardMes[] ZhuangCard;
    //    public cardMes[] XianCard;
    //}
    //public class cardMes
    //{
    //    public int CardSuitInJason;
    //    public int CardNumberInJason;
    //}
}
