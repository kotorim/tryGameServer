using Baccarat_Client_Manager.Tools;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TouchSocket.Http;

namespace Baccarat_Client_Manager.Forms
{
    public partial class mainManagerAdmin : Form
    {
        public mainManagerAdmin()
        {
            InitializeComponent();

        }
        private void initConfig()
        {
            requestStaff rStaff = webHelper.getRequest(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "getconfig") });
            rStaff.onResponse += (BsonDocument doc) =>
            {
                //this.serverConfig.Invoke(new Action<DataGridView>((inner) =>
                //{

                //}), this.serverConfig);
            };
            rStaff.send();
        }
        private void 视频管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileUpload = new fileUpload();
            fileUpload.FormClosed += new FormClosedEventHandler((a, b) =>
            {
                this.Show();
            });
            fileUpload.Show();
            this.Hide();
        }
        private void 账户监管ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var userManager = new userManager();
            userManager.FormClosed += new FormClosedEventHandler((a, b) =>
            {
                this.Show();
            });
            userManager.Show();
            this.Hide();
        }

        private void 数据备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("备份数据可能需要一定时间，并且备份期间新增数据无法保存"
                   , "确认备份"
                      , MessageBoxButtons.OKCancel
                      , MessageBoxIcon.Question)
                      == DialogResult.OK)
            {
                MessageBox.Show("请选择备份路径");
                if (backUpFileDialog.ShowDialog() == DialogResult.OK)
                {
                    requestStaff rStaff = webHelper.getRequest(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "getallmes") });
                    rStaff.onResponse += (BsonDocument res) =>
                    {
                        if (null != res)
                        {
                            File.WriteAllBytes(backUpFileDialog.SelectedPath + "\\backUp" + DateTime.Now.ToString("yyyy.MM.dd") + ".dat", res.ToBson());
                        }
                        MessageBox.Show("备份成功");
                    };
                    rStaff.send();
                }
            }
        }

        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  this.serverConfig.Visible = true;
        }

        private void 账户配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fmManager = new fmManager();
            fmManager.FormClosed += new FormClosedEventHandler((a, b) =>
            {
                this.Show();
            });
            fmManager.Show();
            this.Hide();
        }
    }
}
