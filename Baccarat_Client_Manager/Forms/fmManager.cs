using Baccarat_Client_Manager.Tools;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Baccarat_Client_Manager.Forms
{
    public partial class fmManager : Form
    {
        private int curSelectedRow;
        public fmManager()
        {
            InitializeComponent();
        }
        public void setPanel1BackGround()
        {

            //获取当前屏幕的图像
            Bitmap b = new Bitmap(panel3.Width, panel3.Height);
            panel3.DrawToBitmap(b, new Rectangle(0, 0, panel3.Width, panel3.Height));
            for (int q = 0; q < b.Height; q++)
            {
                for (int w = q % 2; w < b.Width; w += 2)
                {
                    b.SetPixel(w, q, Color.DarkGray);
                }
            }
            panel1.BackgroundImage = b;
        }
        // public 
        private void refreshGrid()
        {
            var tmp = Baccarat_Client_Manager.userMessage.GetValue("child").AsBsonArray;
            if (tmp.Count > 0)
            {
                StringBuilder body = new StringBuilder();
                body.Append("{\"message\":[");
                for (int q = 0; q < tmp.Count; q++)
                {
                    body.Append("ObjectId(\"");
                    body.Append(tmp[q].AsObjectId);
                    body.Append("\"),");
                }
                body.Remove(body.Length - 1, 1);
                body.Append("]}");
                requestStaff rStaff = webHelper.postRequest(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "getusertree") }, body.ToString());
                rStaff.onResponse += (BsonDocument response) =>
                {
                    switch (response.GetValue("success").AsInt32)
                    {
                        case 0:
                            var list = response.GetValue("message").AsBsonArray;
                            this.userGrid.Invoke(new Action<DataGridView>((inner2) =>
                            {
                                inner2.Rows.Clear();
                                for (int q = 0; q < list.Count; q++)
                                {
                                    inner2.Rows.Add();
                                    inner2.Rows[q].Cells[0].Tag = list[q].AsBsonDocument.GetValue("_id").AsObjectId;
                                    inner2.Rows[q].Cells[0].Value = q + 1;
                                    inner2.Rows[q].Cells[1].Value = list[q].AsBsonDocument.GetValue("username").AsString;
                                    bool active = list[q].AsBsonDocument.GetValue("active").AsBoolean;
                                    inner2.Rows[q].Cells[2].Value = active ? tools.moneyBuilder(list[q].AsBsonDocument.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble().ToString()) : "未启用";
                                    inner2.Rows[q].Cells[3].Value = active ? "代币操作" : "未启用";
                                    inner2.Rows[q].Cells[4].Value = active ? "停用" : "启用";
                                    inner2.Rows[q].Cells[5].Value = active ? (list[q].AsBsonDocument.GetValue("password").AsString == "123456" ? "当前为初始密码" : "初始密码") : "未启用";
                                    inner2.Rows[q].Cells[6].Value = active ? "查询" : "未启用";
                                }
                            }), this.userGrid);
                            break;
                    }
                };
                rStaff.send();
            }
        }

        private void activeEdit(ObjectId id, bool active)
        {
            if (DialogResult.OK == MessageBox.Show("即将" + (active ? "启用" : "停用") + "该账户,请确认", "确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                BsonDocument reqDoc = new BsonDocument();
                reqDoc.Add("mes", new BsonArray() { id });
                reqDoc.Add("active", active);
                requestStaff req = webHelper.postRequest(
                  new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "setactive") },
                  reqDoc.ToBson()
                  );
                req.onResponse += (BsonDocument response) =>
                {
                    if (response.GetValue("success").AsInt32 == 0)
                    {
                        var doc = response.GetValue("mes").AsBsonArray[0].AsBsonDocument;
                        MessageBox.Show("操作成功.");
                        this.Invoke(new Action<fmManager>((fmManager inner) =>
                        {
                            inner.userGrid.Rows[inner.curSelectedRow].Cells[1].Value = doc.GetValue("username").AsString;
                            inner.userGrid.Rows[inner.curSelectedRow].Cells[2].Value = active ? "0" : "未启用";
                            inner.userGrid.Rows[inner.curSelectedRow].Cells[3].Value = active ? "代币操作" : "未启用";
                            inner.userGrid.Rows[inner.curSelectedRow].Cells[4].Value = active ? "停用" : "启用";
                            inner.userGrid.Rows[inner.curSelectedRow].Cells[5].Value = active && doc.GetValue("password").AsString != "123456" ? "初始密码" : "未启用";
                            inner.userGrid.Rows[inner.curSelectedRow].Cells[6].Value = active ? "查询" : "未启用";
                        }), this);
                    }
                    else
                    {
                        MessageBox.Show("操作失败，请联系管理人员.");
                    }
                };
                req.send();
            }
        }
        private void moneyEdit(ObjectId id, double num)
        {
            if (DialogResult.OK == MessageBox.Show((num >= 0 ? "即将增加" : "即将减少") + Math.Abs(num) + "个代币", "确认操作", MessageBoxButtons.OKCancel))
            {
                BsonDocument reqDoc = new BsonDocument();
                reqDoc.Add("id", id);
                reqDoc.Add("editMoney", num);
                reqDoc.Add("editDetail", "管理员" + (num >= 0 ? "增加" : "减少") + Math.Abs(num).ToString() + "代币");
                reqDoc.Add("editType", num >= 0 ? 0 : 1);
                requestStaff req = webHelper.postRequest(
                    new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "editmoney") },
                    reqDoc.ToBson()
                    );
                req.onResponse += (BsonDocument response) =>
                {
                    switch (response.GetValue("success").AsInt32)
                    {
                        case 0:
                            MessageBox.Show("操作成功,当前用户代币数量为:" + response.GetValue("currentMoney").ToDouble() + ".");
                            this.Invoke(new Action<fmManager>((fmManager inner) =>
                            {
                                inner.curMoneyText.Text = tools.moneyBuilder(response.GetValue("currentMoney").ToDouble().ToString());
                                inner.userGrid.Rows[inner.curSelectedRow].Cells[2].Value = tools.moneyBuilder(response.GetValue("currentMoney").ToDouble().ToString());
                            }), this);
                            break;
                        case 1:
                            MessageBox.Show("操作失败,用户代币达到上限");
                            break;
                        case 2:
                            MessageBox.Show("操作失败,用户代币达到下限");
                            break;
                        case 3:
                            MessageBox.Show("操作失败,父用户代币达到上限");
                            break;
                        case 4:
                            MessageBox.Show("操作失败,父用户代币达到下限");
                            break;
                        case -1:
                            MessageBox.Show("操作失败,请联系管理人员.");
                            break;
                    }
                };
                req.send();
            }
        }
        private void resetPassword(ObjectId id)
        {
            if (DialogResult.OK == MessageBox.Show("即将设置该用户的密码为初始密码(123456).", "确认操作", MessageBoxButtons.OKCancel))
            {
                requestStaff req = webHelper.postRequest(
                    new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "resetpassword") },
                    new BsonDocument() { new BsonElement("id", id), new BsonElement("password", "123456") }.ToBson()
                    );
                req.onResponse += (BsonDocument response) =>
                {
                    int success = response.GetValue("success").AsInt32;
                    if (success == 0)
                    {
                        MessageBox.Show("操作成功.");
                    }
                    else if (success == 1)
                    {
                        MessageBox.Show("操作失败,没有找到该用户.");
                    }
                    else if (success == -1)
                    {
                        MessageBox.Show("操作失败,请联系管理人员.");
                    }
                };
                req.send();
            }
        }
        private void getMessage(ObjectId id)
        {
            new userMessage(id, userGrid.Rows[curSelectedRow].Cells[1].Value.ToString()).Show();
        }

        private void userGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.curSelectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                switch (e.ColumnIndex)
                {
                    case 3:
                        if (userGrid.Rows[curSelectedRow].Cells[3].Value.ToString() == "代币操作")
                        {
                            curMoneyText.Text = userGrid.Rows[curSelectedRow].Cells[2].Value.ToString();
                            panel1.Visible = true;                        //     this.onClickOpenMoneyControlPanel((ObjectId)userGrid.Rows[e.RowIndex].Cells[0].Tag);
                        }
                        else
                        {
                            MessageBox.Show("未启用，无法操作其代币.");
                        }
                        break;
                    case 4:
                        activeEdit((ObjectId)userGrid.Rows[curSelectedRow].Cells[0].Tag, userGrid.Rows[curSelectedRow].Cells[4].Value.ToString() == "停用" ? false : true);
                        break;
                    case 5:
                        if (userGrid.Rows[curSelectedRow].Cells[5].Value.ToString() == "初始密码")
                        {
                            resetPassword((ObjectId)userGrid.Rows[curSelectedRow].Cells[0].Tag);
                        }
                        else if (userGrid.Rows[curSelectedRow].Cells[5].Value.ToString() == "未启用")
                        {
                            MessageBox.Show("未启用，无法重置密码.");
                        }
                        else
                        {
                            MessageBox.Show("当前已为初始密码，无需重置密码.");
                        }
                        break;
                    case 6:
                        if (userGrid.Rows[curSelectedRow].Cells[6].Value.ToString() == "查询")
                        {
                            getMessage((ObjectId)userGrid.Rows[curSelectedRow].Cells[0].Tag);
                        }
                        else
                        {
                            MessageBox.Show("未启用，无法查询信息.");
                        }
                        break;
                }
            }
        }

        private void fmManager_Shown(object sender, EventArgs e)
        {
            this.userGrid.Enabled = true;
            refreshGrid();
        }
        private void panel1_VisibleChanged(object sender, EventArgs e)
        {
            if (true == panel1.Visible)
            {
                this.setPanel1BackGround();
            }
        }
        private void addMoneyBtn_Click(object sender, EventArgs e)
        {
            this.moneyEdit((ObjectId)userGrid.Rows[curSelectedRow].Cells[0].Tag, double.Parse(moneyEditTB.Text));
        }
        private void moneyEditTB_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(moneyEditTB.Text) && false == uint.TryParse(moneyEditTB.Text, out uint res))
            {
                moneyEditTB.Text = "100";
                MessageBox.Show("请输入一个正整数");
            }
        }
        private void subMoneyBtn_Click(object sender, EventArgs e)
        {
            this.moneyEdit((ObjectId)userGrid.Rows[curSelectedRow].Cells[0].Tag, -(double.Parse(moneyEditTB.Text)));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void addUserBtn_Click(object sender, EventArgs e)
        {
            BsonDocument doc = new BsonDocument();
            doc.Add("parentId", Baccarat_Client_Manager.userMessage.GetValue("_id"));
            doc.Add("userType", 1);

            requestStaff req = webHelper.postRequest(
            new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "createuser") },
            doc.ToBson()
            );
            req.onResponse += (BsonDocument response) =>
            {
                switch (response.GetValue("success").AsInt32)
                {
                    case 0:
                        Baccarat_Client_Manager.userMessage.GetValue("child").AsBsonArray.Add(response.GetValue("id").AsObjectId);
                        MessageBox.Show("操作成功.");
                        this.refreshGrid();
                        break;
                    case 1:
                        MessageBox.Show("操作失败，用户权限不符.");
                        break;
                    case 2:
                        MessageBox.Show("操作失败，超过最大子用户数量.");
                        break;
                    default:
                        MessageBox.Show("操作失败，请联系管理员.");
                        break;
                }
            };
            req.send();
        }

    }
}

