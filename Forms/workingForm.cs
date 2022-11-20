using Baccarat_Server.Tools;
using Baccarat_Server.webApi;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TouchSocket.Http;
using TouchSocket.Sockets;

namespace Baccarat_Server.Forms
{
    public partial class workingForm : Form
    {
        private DateTime serverActiveTime;
        public workingForm()
        {
            InitializeComponent();
            channelList.TopLeftHeaderCell.Value = "频道列表";
            serverActiveTime = DateTime.Now;
            eventDispatcher.AddEvent(eventType.onConnected, new Action<ITcpClientBase>((inner) =>
            {
                this.dataGridView1.Invoke(new Action<DataGridView>((innerr) =>
                {
                    innerr.Rows.Add(
                  inner.IP
                  );
                }), this.dataGridView1);
            }));
            eventDispatcher.AddEvent(eventType.onDisconnected, new Action<ITcpClientBase>((inner) =>
            {
                this.dataGridView1.Invoke(new Action<DataGridView>((innerr) =>
                {
                    for (int q = 0; q < innerr.Rows.Count; q++)
                    {
                        if (string.Equals(innerr.Rows[q].Cells[0].Value.ToString(), inner.IP))
                        {
                            innerr.Rows.RemoveAt(q);
                            break;
                        }
                    }
                }), this.dataGridView1);
            }));
            eventDispatcher.AddEvent(eventType.onPost, new Action<ITcpClientBase, HttpContextEventArgs, BsonDocument>((client, e, body) =>
            {
                this.dataGridView1.Invoke(new Action<DataGridView>((innerr) =>
                {
                    for (int q = 0; q < innerr.Rows.Count; q++)
                    {
                        if (string.Equals(innerr.Rows[q].Cells[0].Value.ToString(), client.IP))
                        {
                            innerr.Rows[q].Cells[1].Value = e.Context.Request.GetHeader("flag");
                            break;
                        }
                    }
                }), this.dataGridView1);
            }));
            eventDispatcher.AddEvent(eventType.onGet, new Action<ITcpClientBase, HttpContextEventArgs>((client, e) =>
            {
                this.dataGridView1.Invoke(new Action<DataGridView>((innerr) =>
                {
                    for (int q = 0; q < innerr.Rows.Count; q++)
                    {
                        if (string.Equals(innerr.Rows[q].Cells[0].Value.ToString(), client.IP))
                        {
                            innerr.Rows[q].Cells[1].Value = e.Context.Request.GetHeader("flag");
                            break;
                        }
                    }
                }), this.dataGridView1);
            }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            eventDispatcher.dispatchEvent(eventType.onTimeTick);
            this.refreshChannelList(postClassNewer.getInstance().videoManager_Instance.channels);
            var tmp = DateTime.Now - serverActiveTime;
            this.status.Text = "当前时间: " + DateTime.Now.ToString("G") + " 本次服务器运行总时长: " + tmp.Days + "天" + tmp.Hours + "小时";
        }
        public void refreshChannelList(List<videoChannel> inner)
        {
            if (channelList.RowCount < inner.Count)
            {
                for (int w = channelList.RowCount; w < inner.Count; w++)
                {
                    channelList.Rows.Add();
                }
            }
            for (int q = 0; q < inner.Count; q++)
            {
                if (null != inner[q].playingVideo)
                {
                    channelList.Rows[q].Cells[0].Value = q + 1;
                    channelList.Rows[q].Cells[1].Value = inner[q].playingVideo.GetValue("name").AsString;
                    channelList.Rows[q].Cells[2].Value = inner[q].isMain ? "间歇" : "主要";
                    channelList.Rows[q].Cells[3].Value = inner[q].timeStep.ToString() + "/" + inner[q].playingVideo.GetValue("videoDuration").ToString();
                }
            }
        }

        private void 重置全部userObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(async () => { await webHelper.dbUpdate("Baccarat", "user", new BsonDocument(), Builders<BsonDocument>.Update.Set("userobjects", new BsonDocument(new BsonElement("money", 1000d))), true); });
        }

        private void 全体新增kvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(async () => { await webHelper.dbUpdate("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("authority", 3), Builders<BsonDocument>.Update.Set("userobjects", new BsonDocument() { new BsonElement("money", 1000d), new BsonElement("cathecticEdge", 200L) }), true); });
        }
    }
}
