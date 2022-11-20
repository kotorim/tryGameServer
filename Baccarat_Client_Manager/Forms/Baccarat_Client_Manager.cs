using Baccarat_Client_Manager.Forms;
using Baccarat_Client_Manager.Tools;
using Baccarat_Server.Tools;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TouchSocket.Core.XREF.Newtonsoft.Json;
using TouchSocket.Http;

namespace Baccarat_Client_Manager
{

    public partial class Baccarat_Client_Manager : Form
    {
        public static BsonDocument userMessage;
        public Baccarat_Client_Manager()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(password.Text))
            {
                MessageBox.Show("账号或密码不能为空");
                return;
            }
            BsonDocument doc = new BsonDocument();
            doc.Add("username", username.Text);
            doc.Add("password", password.Text);
            doc.Add("authority", "super" == username.Text ? -1 : 0);
            // "{\"username\":\"" + username.Text + "\",\"password\":\"" + password.Text + "\"}"
            requestStaff rStaff = webHelper.postRequest(
                new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "login") },
                doc.ToBson()
                ); ;
            rStaff.onResponse += (BsonDocument response) =>
            {
                try
                {
                    userMessage = response;
                    switch (userMessage.GetValue("success").AsInt32)
                    {
                        case -1:
                            throw new Exception("服务器无法处理的消息");
                        case 0:
                        case 7:
                            if (0 >= userMessage.GetValue("authority").AsInt32)
                            {
                                this.Invoke(new Action<Form>((wind) =>
                                {
                                    Form window;
                                    if (-1 == userMessage.GetValue("authority").AsInt32)
                                    {
                                        window = new mainManagerSuper();
                                    }
                                    else
                                    {
                                        window = new mainManagerAdmin();
                                    }
                                    window.Show();
                                    window.FormClosed += new FormClosedEventHandler((aa, bb) => { this.Close(); });
                                    wind.Hide();
                                }), this);
                            }
                            else
                            {
                                throw new Exception("管理端无法登录非管理端账号");
                            }
                            break;
                        case 1:
                            throw new Exception("账号或者密码错误");
                        case 2:
                            throw new Exception("登录错误次数达到上限，ip地址被冻结");
                        case 5:
                            throw new Exception("登录用户权限非本端用户权限，请检查账号权限");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            rStaff.send();
        }
    }
    //public class userobjects
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int money { get; set; }
    //}

    //public class user
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string username { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string password { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int authority { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string isfrozen { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string parent { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<string> child { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public userobjects userobjects { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int success { get; set; }
    //}
}

