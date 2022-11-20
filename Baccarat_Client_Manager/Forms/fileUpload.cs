using Baccarat_Client_Manager.Tools;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Buffers.Text;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using TouchSocket.Http;
using Xabe.FFmpeg;

namespace Baccarat_Client_Manager.Forms
{
    public partial class fileUpload : Form
    {
        private int startIndexMain = 0;
        private int startIndexNap = 0;
        private int number = 10;
        private ConcurrentQueue<KeyValuePair<string, string>> uploadList;
        private int listCounter = 0;
        private int failCounter = 0;
        public fileUpload()
        {
            InitializeComponent();
            this.mainVideoGrid.TopLeftHeaderCell.Value = "对局视频";
            this.napVideoGrid.TopLeftHeaderCell.Value = "间歇视频";
            this.showEach.SelectedIndex = 0;
            this.prevMain.Enabled = false;
        }
        public void refreshMainVideoList(fileUpload target, int startIndexx, int numberr)
        {

            requestStaff rStaff = webHelper.getRequest(
              new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "getvideolist") },
              new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("startIndex",startIndexx.ToString()),
                    new KeyValuePair<string, string>("number",numberr.ToString()),
                    new KeyValuePair<string, string>("isMain","true")
              });
            rStaff.onResponse += (BsonDocument doc) =>
              {
                  target.Invoke(new Action<fileUpload>((inner) =>
                  {
                      inner.prevMain.Enabled = doc.GetValue("couldPrev").AsBoolean;
                      inner.nextMain.Enabled = doc.GetValue("couldNext").AsBoolean;
                      inner.startIndexMain = doc.GetValue("indexNum").AsInt32;
                      BsonArray nameArray = doc.GetValue("nameArray").AsBsonArray;
                      inner.mainVideoGrid.Rows.Clear();
                      for (int q = 0; q < nameArray.Count; q++)
                      {
                          inner.mainVideoGrid.Rows.Add(nameArray[q].AsString);
                      }
                  }), target);
              };
            rStaff.send();
        }
        public void refreshNapVideoList(fileUpload target, int startIndexx, int numberr)
        {

            requestStaff rStaff = webHelper.getRequest(
              new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "getvideolist") },
              new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("startIndex",startIndexx.ToString()),
              new KeyValuePair<string, string>("number",numberr.ToString()),new KeyValuePair<string, string>("isMain","false")});
            rStaff.onResponse += (BsonDocument doc) =>
            {
                BsonArray nameArray = doc.GetValue("nameArray").AsBsonArray;
                target.Invoke(new Action<fileUpload>((inner) =>
                {
                    inner.prevNap.Enabled = doc.GetValue("couldPrev").AsBoolean;
                    inner.nextNap.Enabled = doc.GetValue("couldNext").AsBoolean;
                    inner.startIndexNap = doc.GetValue("indexNum").AsInt32;
                    inner.napVideoGrid.Rows.Clear();
                    for (int q = 0; q < nameArray.Count; q++)
                    {
                        inner.napVideoGrid.Rows.Add(nameArray[q].AsString);
                    }
                }), target);

            };
            rStaff.send();
        }

        private void showEach_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.showEach.SelectedIndex)
            {
                case 0:
                    number = 10;
                    break;
                case 1:
                    number = 20;
                    break;
                case 2:
                    number = 50;
                    break;
                case 3:
                    number = 100;
                    break;
                default:
                    number = 10;
                    break;
            }
            startIndexMain = 0;
            startIndexNap = 0;
            this.refreshMainVideoList(this, startIndexMain, this.number);
            this.refreshNapVideoList(this, startIndexNap, this.number);
        }
        private void prevPage_Click(object sender, EventArgs e)
        {
            this.refreshMainVideoList(this, startIndexMain - 2 * number, this.number);
        }
        private void nextPage_Click(object sender, EventArgs e)
        {
            this.refreshMainVideoList(this, startIndexMain, this.number);
        }
        private void uploadButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请分别选择需要上传的视频和json文件,对应名字相同");
            MessageBox.Show("请选择视频文件");
            if (this.videoFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("请选择对应json文件");
                if (this.jsonFile.ShowDialog() == DialogResult.OK)
                {
                    if (this.jsonFile.FileNames.Length != this.videoFile.FileNames.Length)
                    {
                        MessageBox.Show("json文件和视频文件数量不匹配,请从视频文件开始重新选择");
                        return;
                    }
                    List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                    for (int q = 0; q < this.videoFile.SafeFileNames.Length; q++)
                    {
                        string[] split = this.videoFile.SafeFileNames[q].Split('.');

                        for (int w = 0; w < this.jsonFile.SafeFileNames.Length; w++)
                        {
                            if (string.Equals(split[0], this.jsonFile.SafeFileNames[w].Split('.')[0]))
                            {
                                list.Add(new KeyValuePair<string, string>(this.jsonFile.FileNames[w], this.videoFile.FileNames[q]));
                            }
                        }
                    }
                    if (MessageBox.Show("共选中" + this.videoFile.FileNames.Length + "个文件,共完成" + list.Count + "个json文件和视频文件的匹配"
                        , "确认上传"
                        , MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Question)
                        == DialogResult.OK)
                    {
                        if (null == uploadList)
                        {
                            uploadList = new ConcurrentQueue<KeyValuePair<string, string>>();
                            for (int q = 0; q < list.Count; q++)
                            {
                                uploadList.Enqueue(list[q]);
                            }
                            uploadVideo();
                        }
                        else
                        {
                            for (int q = 0; q < list.Count; q++)
                            {
                                uploadList.Enqueue(list[q]);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("json文件选择失败,请从视频文件开始重新选择");
                }
            }
            else
            {
                MessageBox.Show("视频文件选择失败");
            }
        }
        private void uploadNapBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请选择视频文件");
            if (this.videoFile.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("共选中" + this.videoFile.FileNames.Length + "个文件"
                    , "确认上传"
                       , MessageBoxButtons.OKCancel
                       , MessageBoxIcon.Question)
                       == DialogResult.OK)
                {
                    if (null == uploadList)
                    {
                        uploadList = new ConcurrentQueue<KeyValuePair<string, string>>();
                        for (int q = 0; q < this.videoFile.FileNames.Length; q++)
                        {
                            uploadList.Enqueue(new KeyValuePair<string, string>(null, this.videoFile.FileNames[q]));
                        }
                        uploadVideo();
                    }
                    else
                    {
                        for (int q = 0; q < this.videoFile.FileNames.Length; q++)
                        {
                            uploadList.Enqueue(new KeyValuePair<string, string>(null, this.videoFile.FileNames[q]));
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("视频文件选择失败");
            }
        }
        private void onClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("视频上传中，请勿退出");
            //DialogResult result;
            //result = MessageBox.Show("确定退出吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (result == DialogResult.OK)
            //{

            //    Application.ExitThread();
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }
        private async void uploadVideo()
        {
            if (listCounter == 0)
            {
                this.message.Items[0].Text = "已上传" + listCounter + "个，还剩" + uploadList.Count + "个";
                this.uploadNapBtn.Enabled = false;
                this.uploadMainBtn.Enabled = false;
                this.FormClosing += onClosing;
            }
            if (uploadList.Count > 0)
            {

                if (uploadList.TryDequeue(out KeyValuePair<string, string> value))
                {
                    listCounter++;
                    BsonDocument doc = new BsonDocument();
                    string[] split = value.Value.Split(new char[] { '\\', '\\' });
                    IMediaInfo info = await FFmpeg.GetMediaInfo(value.Value);
                    doc.Add("videoDuration", (int)info.Duration.TotalSeconds);
                    doc.Add("name", split[split.Length - 1]);
                    if (false == value.Key.IsNullOrEmpty())
                    {
                        string json = "{\"timeStamps\":" + File.ReadAllText(value.Key) + "}";
                        doc.AddRange(BsonDocument.Parse(json));
                    }
                    doc.Add("File", File.ReadAllBytes(value.Value));
                    requestStaff rStaff = webHelper.postRequest(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "uploadvideo") }, doc.ToBson());
                    rStaff.onResponse += (BsonDocument res) =>
                    {
                        if (this.InvokeRequired)
                        {
                            switch (res.GetValue("success").AsInt32)
                            {
                                case 0:
                                    this.message.Invoke(new Action<StatusStrip>((inner) =>
                                    {
                                        inner.Items[0].Text = "已上传" + listCounter + "个，还剩" + uploadList.Count + "个";
                                    }), this.message);
                                    uploadVideo();
                                    break;
                                case 1:
                                    this.message.Invoke(new Action<StatusStrip>((inner) =>
                                    {
                                        inner.Items[0].Text = "已上传" + listCounter + "个，还剩" + uploadList.Count + "个";
                                    }), this.message);
                                    this.failCounter++;
                                    uploadVideo();
                                    break;
                                case -1:
                                    this.failCounter = 0;
                                    this.listCounter = 0;
                                    MessageBox.Show("服务器出现内部错误，停止传输");
                                    this.Invoke(new Action<fileUpload>((inner) =>
                                    {
                                        inner.message.Items[0].Text = " ";
                                        inner.uploadNapBtn.Enabled = true;
                                        inner.uploadMainBtn.Enabled = true;
                                    }));
                                    this.FormClosing -= onClosing;
                                    return;
                            }
                        }
                        else
                        {
                            switch (res.GetValue("success").AsInt32)
                            {
                                case 0:
                                    this.message.Items[0].Text = "已上传" + listCounter + 1 + "个，还剩" + uploadList.Count + "个";
                                    uploadVideo();
                                    break;
                                case 1:
                                    this.message.Items[0].Text = "已上传" + listCounter + 1 + "个，还剩" + uploadList.Count + "个";
                                    this.failCounter++;
                                    uploadVideo();
                                    break;
                                case -1:
                                    this.failCounter = 0;
                                    this.listCounter = 0;
                                    MessageBox.Show("服务器出现内部错误，停止传输");
                                    this.message.Items[0].Text = " ";
                                    this.uploadNapBtn.Enabled = true;
                                    this.uploadMainBtn.Enabled = true;
                                    this.FormClosing -= onClosing;
                                    return;
                            }
                        }
                    };
                    webHelper.requestQueue.Enqueue(rStaff);
                }
                else
                {
                    uploadVideo();
                }
                //HttpResponse response = webHelper.postRequest(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "uploadvideo") }, doc.ToJson());
            }
            else
            {
                if (this.failCounter > 0)
                {
                    MessageBox.Show("上传完毕,因文件名重复失败" + this.failCounter + "个");
                }
                else
                {
                    MessageBox.Show("上传完毕");
                }
                this.uploadList = null;
                this.FormClosing -= onClosing;
                startIndexMain = 0;
                this.failCounter = 0;
                this.listCounter = 0;
                startIndexNap = 0;
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<fileUpload>((inner) =>
                    {
                        inner.message.Items[0].Text = " ";
                        inner.uploadNapBtn.Enabled = true;
                        inner.uploadMainBtn.Enabled = true;
                        inner.refreshMainVideoList(inner, startIndexMain, this.number);
                        inner.refreshNapVideoList(inner, startIndexNap, this.number);
                    }), this);
                }
                else
                {
                    this.message.Items[0].Text = " ";
                    this.uploadNapBtn.Enabled = true;
                    this.uploadMainBtn.Enabled = true;
                    this.refreshMainVideoList(this, startIndexMain, this.number);
                    this.refreshNapVideoList(this, startIndexNap, this.number);
                }
            }
        }

        private void prevNap_Click(object sender, EventArgs e)
        {
            this.refreshNapVideoList(this, startIndexNap - 2 * number, this.number);

        }
        private void nextNap_Click(object sender, EventArgs e)
        {
            this.refreshNapVideoList(this, startIndexNap, this.number);

        }
    }
    [DataContract]
    public class timeStamps
    {
        [DataMember]
        [BsonElement]
        [BsonRepresentation(BsonType.Double)]
        public double VideoTimeInJason;
        [DataMember]
        [BsonElement]
        [BsonRepresentation(BsonType.Document)]
        public cardMes[] ZhuangCard;
        [DataMember]
        [BsonElement]
        [BsonRepresentation(BsonType.Document)]
        public cardMes[] XianCard;
    }
    [DataContract]
    public class cardMes
    {
        [DataMember]
        [BsonElement]
        [BsonRepresentation(BsonType.Int32)]
        public int CardSuitInJason;
        [DataMember]
        [BsonElement]
        [BsonRepresentation(BsonType.Int32)]
        public int CardNumberInJason;
    }
}
