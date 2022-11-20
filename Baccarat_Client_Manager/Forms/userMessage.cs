using Baccarat_Client_Manager.Tools;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Forms;
using TouchSocket.Http;

namespace Baccarat_Client_Manager.Forms
{
    public partial class userMessage : Form
    {
        public userMessage(ObjectId _id, string name)
        {
            InitializeComponent();
            this.Name = name;
            var rStaff = webHelper.postRequest(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "getusermes") },
                         new BsonDocument(new BsonElement("id", _id)).ToBson());
            rStaff.onResponse += (BsonDocument body) =>
            {
                double num = 0;
                switch (body.GetValue("success").AsInt32)
                {
                    case 0:
                        this.money.Invoke(new Action<Label>((inner) =>
                        {
                            inner.Text = tools.moneyBuilder(body.GetValue("money").ToDouble().ToString());
                        }), this.money);
                        this.grid.Invoke(new Action<DataGridView>((inner) =>
                        {
                            if (body.Contains("moneyEditRecord") && !body.GetValue("moneyEditRecord").IsBsonNull)
                            {
                                var moneyEdit = body.GetValue("moneyEditRecord").AsBsonArray;
                                for (int q = 0; q < moneyEdit.Count; q++)
                                {
                                    double tmp = moneyEdit[q].AsBsonDocument.GetValue("editMoney").ToDouble();
                                    string type = "";
                                    switch (moneyEdit[q].AsBsonDocument.GetValue("editType").AsInt32)
                                    {
                                        case 0:
                                            type = "增加";
                                            break;
                                        case 1:
                                            type = "减少";
                                            num += tmp;
                                            break;
                                        case 2:
                                            type = "分红";
                                            break;
                                        case 3:
                                            type = "投注";
                                            break;
                                        case 4:
                                            type = "管理员增加";
                                            break;
                                        case 5:
                                            type = "管理员减少";
                                            break;
                                    }
                                    inner.Rows.Add(
                                                 webHelper.FromUnixTime(moneyEdit[q].AsBsonDocument.GetValue("editTime").AsInt64, true),
                                                 type,
                                                 moneyEdit[q].AsBsonDocument.GetValue("editDetail").AsString);
                                }
                            }
                            if (body.Contains("freezeLog") && !body.GetValue("freezeLog").IsBsonNull)
                            {
                                var freezeLog = body.GetValue("freezeLog").AsBsonArray;
                                for (int w = 0; w < freezeLog.Count; w++)
                                {
                                    inner.Rows.Add(
                                                 webHelper.FromUnixTime(freezeLog[w].AsBsonDocument.GetValue("freezeTime").AsInt64, true),
                                                 "账户控制",
                                   freezeLog[w].AsBsonDocument.GetValue("freezeType").AsBoolean ? "关户" : "开户"
                                    );
                                }
                            }
                            inner.Sort(inner.Columns[0], System.ComponentModel.ListSortDirection.Descending);
                            subedMoney.Text = tools.moneyBuilder(num.ToString());
                        }), this.grid);
                        break;
                    case -1:
                        MessageBox.Show("信息查询失败,请联系管理员.");
                        break;
                }
            };
            rStaff.send();
        }
        private bool isFilterManagerSub = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (false == isFilterManagerSub)
            {
                isFilterManagerSub = true;
                button1.Text = "总明细";
                for (int q = 0; q < this.grid.Rows.Count; q++)
                {
                    if (this.grid.Rows[q].Cells[1].Value.ToString() != "减少")
                    {
                        this.grid.Rows[q].Visible = false;
                    }
                }
            }
            else
            {
                isFilterManagerSub = false;
                button1.Text = "纯管理员扣分明细";
                for (int q = 0; q < this.grid.Rows.Count; q++)
                {
                    this.grid.Rows[q].Visible = true;
                }
            }
        }
    }
}
