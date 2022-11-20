using Baccarat_Client_Manager.Tools;
using Baccarat_Server.Tools;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TouchSocket.Core.XREF.Newtonsoft.Json;
using TouchSocket.Http;

namespace Baccarat_Client_Manager.Forms
{
    public partial class mainManagerSuper : Form
    {
        public mainManagerSuper()
        {
            InitializeComponent();
        }

        private void resetAdminPassword_Click(object sender, EventArgs e)
        {
            panel_ResetAdminPassword.Visible = true;
            panel_ResetAdminPassword.BringToFront();
        }

        private void btn_ResetAdminPassword_Click(object sender, EventArgs e)
        {
            if (!string.Equals(newPassword.Text, reNewPassword.Text))
            {
                MessageBox.Show("两次输入的新密码不一致");
                return;
            }
            requestStaff req = webHelper.postRequest(
                   new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "resetpassword") },
                   new BsonDocument() { new BsonElement("id", Baccarat_Client_Manager.userMessage.GetValue("child").AsBsonArray[0].AsObjectId), new BsonElement("password", newPassword.Text) }.ToBson()
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
}
