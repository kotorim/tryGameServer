//using MongoDB.Bson;
//using MongoDB.Bson.Serialization;
//using MongoDB.Bson.Serialization.Attributes;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using TouchSocket.Core.XREF.Newtonsoft.Json;
//using TouchSocket.Http;
//using TouchSocket.Sockets;

//namespace Baccarat_Server.webApi
//{
//    public class userManager : postClass
//    {
//        private static Regex match = new Regex("(ObjectId\\(\"|NumberLong\\(\"|NumberLong\\(|\"\\)|\\))");
//        protected override void onPost(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
//        {
//            e.Context.Request.Headers.TryGetValue("flag", out string flag);
//            if (!string.IsNullOrEmpty(flag))
//            {
//                switch (flag)
//                {
//                    case "login":
//                        onLogin(client, e, body);
//                        break;
//                    case "resetpassword":
//                        onResetPassword(client, e, body);
//                        break;
//                    case "editmoney":
//                        editUserMoney(client, e, body);
//                        break;
//                    case "getusertree":
//                        getUserTree(client, e, body);
//                        break;
//                    case "setisfrozen":
//                        setFrozen(client, e, body);
//                        break;
//                    case "getusermes":
//                        getUserMes(client, e, body);
//                        break;
//                    case "clearuser":
//                        clearUser(client, e, body);
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }
//        protected override void onGet(ITcpClientBase client, HttpContextEventArgs e)
//        {
//            e.Context.Request.Headers.TryGetValue("flag", out string flag);
//            if (!string.IsNullOrEmpty(flag))
//            {
//                switch (flag)
//                {
//                    case "getallnametree":
//                        getAllNameTree(client, e);
//                        break;
//                    case "getallmes":
//                        getAllMes(client, e);
//                        break;
//                }
//            }
//        }
//        public static void getUserTree(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
//        {
//            var all = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.In("username", body.GetValue("message").AsBsonArray)).ToListAsync()).Result;
//            var arr = new BsonArray();
//            all.ForEach((inner) =>
//            {
//                double tmp = inner.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble();
//                inner.Remove("userobjects");
//                inner.Add("userobjects", new BsonDocument(new BsonElement("money", tmp)));
//                arr.Add(inner);
//            });
//            var res = new BsonDocument();
//            res.Add("message", arr);
//            res.Add("success", 0);
//            e.Context.Response.FromJson(res.ToJson());
//        }
//        public static void getAllNameTree(ITcpClientBase client, HttpContextEventArgs e)
//        {
//            var all = Task.Run(async () => await webHelper.dbSearch<BsonDocument>("Baccarat", "user", Builders<BsonDocument>.Filter.Ne("username", "super")).ToListAsync()).Result;
//            for (int q = 0; q < all.Count; q++)
//            {
//                if (string.Equals("admin", all[q].GetValue("username").AsString))
//                {
//                    nameTree a = new nameTree();
//                    a.name = all[q].GetValue("username").AsString;
//                    var list = all[q].GetValue("child").AsBsonArray;
//                    a.child = new nameTree[list.Count];
//                    for (int w = 0; w < list.Count; w++)
//                    {
//                        a.child[w] = new nameTree();
//                        a.child[w].name = list[w].AsString;
//                    }
//                    all.RemoveAt(q);
//                    getTree(ref a, all);
//                    var outer = a.ToBsonDocument();
//                    outer.Add("success", 0);
//                    e.Context.Response.FromJson(outer.ToJson());
//                    break;
//                }
//            }
//        }
//        public static void getAllMes(ITcpClientBase client, HttpContextEventArgs e)
//        {
//            var all = Task.Run(async () => await webHelper.dbSearch<BsonDocument>("Baccarat", "user", Builders<BsonDocument>.Filter.Empty).ToListAsync()).Result;
//            for (int q = 0; q < all.Count; q++)
//            {
//                e.Context.Response.WriteContent(all[q].ToBson());
//            }
//            e.Context.Response.Answer();
//        }
//        public static void getTree(ref nameTree parent, List<BsonDocument> all)
//        {
//            for (int q = 0; q < parent.child.Length; q++)
//            {
//                for (int w = 0; w < all.Count; w++)
//                {
//                    if (string.Equals(parent.child[q].name, all[w].GetValue("username").AsString))
//                    {
//                        var tmp = all[w].GetValue("child").AsBsonArray;
//                        if (tmp.Count > 0)
//                        {
//                            nameTree[] child = new nameTree[tmp.Count];
//                            for (int e = 0; e < tmp.Count; e++)
//                            {
//                                child[e] = new nameTree();
//                                child[e].name = tmp[e].AsString;
//                            }
//                            parent.child[q].child = child;
//                            getTree(ref parent.child[q], all);
//                        }
//                        break;
//                    }
//                }
//            }
//        }
//        /// <summary>
//        /// 登录
//        /// </summary>
//        /// <param name="client"></param>
//        /// <param name="e"></param>
//        public static void onLogin(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
//        {
//            //取得banip数据
//            blockedip tmpip = getBlockedIP(client.IP);
//            //判断banip
//            if (tmpip.leftwrongtimes == -1)
//            {
//                e.Context.Response.FromJson("{\"success\":2}");
//                Console.WriteLine("IP已经被ban");
//                return;
//            }
//            try
//            {
//                //查询账号密码
//                // user tmpUser = BsonSerializer.Deserialize<user>(body);   // JsonConvert.DeserializeObject<user>(e.Context.Request.GetBody());
//                var result = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("username", body.GetValue("username").AsString)).FirstAsync()).Result;
//                if (String.Equals(body.GetValue("password").AsString, result.GetValue("password").AsString))
//                {
//                    result.Add("success", result.GetValue("isfrozen").AsBoolean ? 3 : 0);
//                    result.Set("_id", result.GetValue("_id").AsObjectId.ToString());
//                    string tmpo = result.ToJson();
//                    e.Context.Response.FromJson(match.Replace(tmpo, ""));
//                    Console.WriteLine("正确登录");
//                    return;
//                }
//            }
//            catch (Exception ex)
//            {
//                if (-1 == --tmpip.leftwrongtimes)
//                {
//                    tmpip.servertime = Tools.tools.ToUnixTime(DateTime.UtcNow);
//                }
//                try
//                {
//                    if (webHelper.dbUpdate("Baccarat", "blockedip", Builders<BsonDocument>.Filter.Eq("ip", tmpip.ip), Builders<BsonDocument>.Update.Set("leftwrongtimes", tmpip.leftwrongtimes).Set("servertime", tmpip.servertime)).Result.IsAcknowledged)
//                    {
//                        Console.WriteLine("记录ip信息成功:");
//                    }
//                }
//                catch (Exception ex2)
//                {
//                    Console.WriteLine(Tools.tools.logError("记录ip信息失败:" + ex2.Message));
//                }
//                e.Context.Response.FromJson("{\"success\":1}");
//                Console.WriteLine("账号或者密码错误，登录失败");
//                return;
//            }
//        }
//        /// <summary>
//        /// 改密
//        /// </summary>
//        /// <param name="client"></param>
//        /// <param name="e"></param>
//        public static void onResetPassword(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
//        {
//            var tmp = Task.Run(async () => await webHelper.dbUpdate("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(body.GetValue("id").AsString)), Builders<BsonDocument>.Update.Set("password", body.GetValue("password").AsString))).Result;
//            if (tmp.ModifiedCount == 1)
//            {
//                e.Context.Response.FromJson("{\"success\":0}");
//            }
//            else
//            {
//                e.Context.Response.FromJson("{\"success\":1}");
//            }
//        }
//        /// <summary>
//        /// 查询是否在banip列表，不在就创建一个
//        /// </summary>
//        /// <param name="innerIP">IP地址</param>
//        /// <returns>blockedip:是否在ip列表</returns>
//        public static blockedip getBlockedIP(string innerIP)
//        {
//            long tTime = Tools.tools.ToUnixTime(DateTime.UtcNow);
//            try
//            {
//                var doc = Task.Run(async () => await webHelper.dbSearch("Baccarat", "blockedip", Builders<BsonDocument>.Filter.Eq("ip", innerIP)).FirstAsync()).Result;
//                Console.WriteLine("找到banip返回已有条目");
//                if (tTime - doc.GetValue("servertime").AsInt64 > 5184000)
//                {
//                    doc.Set("leftwrongtimes", 10);
//                    doc.Set("servertime", tTime);
//                    Task.Run(async () => await webHelper.dbUpdate("Baccarat", "blockedip", Builders<BsonDocument>.Filter.Eq("ip", innerIP), Builders<BsonDocument>.Update.Set("servertime", tTime).Set("leftwrongtimes", 10)));
//                }
//                return BsonSerializer.Deserialize<blockedip>(doc);
//            }
//            catch (Exception)
//            {
//                blockedip target = new blockedip();
//                target.ip = innerIP;
//                target.leftwrongtimes = 10;
//                target.servertime = tTime;
//                webHelper.dbAdd("Baccarat", "blockedip", target);
//                Console.WriteLine("没找到banip返回新建条目");
//                return target;
//            }
//        }
//        public BsonDocument editUserMoney(ObjectId id, double money)
//        {
//            BsonDocument docRes = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", id)).FirstAsync()).Result;
//            var moneyEdit = new moneyEdit(Tools.tools.ToUnixTime(DateTime.UtcNow), money, money + docRes.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble());
//            docRes.GetValue("userobjects").AsBsonDocument.Set("money", moneyEdit.curMoney);
//            webHelper.tryAddArrayValue("moneyEditRecord", docRes.GetValue("userobjects").AsBsonDocument, moneyEdit.ToBsonDocument());
//            if (1 == Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", docRes.GetValue("_id")), docRes)).Result.ModifiedCount)
//            {
//                return docRes;
//            }
//            throw new Exception("editUserMoney:没有找到对应的用户");
//        }
//        private void editUserMoney(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
//        {
//            BsonDocument docRes = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(body.GetValue("id").AsString))).FirstAsync()).Result;
//            var moneyEdit = new moneyEdit(Tools.tools.ToUnixTime(DateTime.UtcNow), body.GetValue("editMoney").AsInt32, body.GetValue("editMoney").AsInt32 + docRes.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble());
//            docRes.GetValue("userobjects").AsBsonDocument.Set("money", moneyEdit.curMoney);
//            webHelper.tryAddArrayValue("moneyEditRecord", docRes.GetValue("userobjects").AsBsonDocument, moneyEdit.ToBsonDocument());
//            if (1 == Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", docRes.GetValue("_id")), docRes)).Result.ModifiedCount)
//            {
//                e.Context.Response.FromJson("{\"success\":0,\"currentMoney\":" + moneyEdit.curMoney + "}");
//                return;
//            }
//            throw new Exception("editUserMoney:没有找到对应的用户");
//        }
//        private void setFrozen(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
//        {
//            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.In("username", body.GetValue("names").AsBsonArray)).ToListAsync()).Result;
//            var log = new freezeLog(Tools.tools.ToUnixTime(DateTime.UtcNow), body.GetValue("isfrozen").AsBoolean);
//            for (int q = 0; q < target.Count; q++)
//            {
//                if (false == target[q].GetValue("userobjects").AsBsonDocument.Contains("freezeLog"))
//                {
//                    Dictionary<string, BsonArray> kv = new Dictionary<string, BsonArray>();
//                    kv.Add("freezeLog", new BsonArray { log.ToBsonDocument().AsBsonValue });
//                    target[q].GetValue("userobjects").AsBsonDocument.AddRange(kv);
//                }
//                else
//                {
//                    target[q].GetValue("userobjects").AsBsonDocument.GetValue("freezeLog").AsBsonArray.Add(log.ToBsonDocument());
//                }
//                target[q].Set("isfrozen", body.GetValue("isfrozen").AsBoolean);
//                var result = Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", target[q].GetValue("_id")), target[q])).Result;
//                if (result.ModifiedCount <= 0)
//                {
//                    throw new Exception("错误的id");
//                }
//            }
//            e.Context.Response.FromJson("{\"success\":0}");
//        }
//        private void getUserMes(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
//        {
//            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(body.GetValue("id").AsString))).FirstAsync()).Result;
//            var doc = target.GetValue("userobjects").AsBsonDocument;
//            doc.Add("success", 0);
//            e.Context.Response.FromJson(doc.ToJson());
//            //var dic = new Dictionary<string, List<logMes>>();
//            //dic.Add("mes", list);
//            //var response = new BsonDocument();
//            //response.AddRange(dic);
//            //response.Add("success", 0);
//            //e.Context.Response.FromJson(response.ToJson());

//            //List<logMes> list = new List<logMes>();
//            //if (target.GetValue("userobjects").AsBsonDocument.Contains("moneyEditRecord"))
//            //{
//            //    var moneyEdit = target.GetValue("userobjects").AsBsonDocument.GetValue("moneyEditRecord").AsBsonArray;
//            //    for (int q = 0; q < moneyEdit.Count; q++)
//            //    {
//            //        list.Add(new logMes(moneyEdit[q].AsBsonDocument.GetValue("editTime").AsInt64, 1, (moneyEdit[q].AsBsonDocument.GetValue("editMoney").ToDouble()).ToString()));
//            //    }
//            //}
//            //if (target.GetValue("userobjects").AsBsonDocument.Contains("freezeLog"))
//            //{
//            //    var freezeLog = target.GetValue("userobjects").AsBsonDocument.GetValue("freezeLog").AsBsonArray;
//            //    for (int w = 0; w < freezeLog.Count; w++)
//            //    {
//            //        list.Add(new logMes(freezeLog[w].AsBsonDocument.GetValue("freezeTime").AsInt64, 0, freezeLog[w].AsBsonDocument.GetValue("freezeType").AsBoolean ? "关户" : "开户"));
//            //    }
//            //}
//            //if (list.Count > 0)
//            //{
//            //    list.Sort(new Comparison<logMes>((a, b) => { return (int)(a.time - b.time); }));
//            //}
//            //var doc = new BsonDocument();
//            //var arr = new BsonArray();
//            //doc.Add("mes", arr);
//            //for (int r = 0; r < list.Count; r++)
//            //{
//            //    arr.Add(list[r].ToBsonDocument());
//            //}
//            //doc.Add("money", target.GetValue("userobjects").AsBsonDocument.GetValue("money"));
//        }
//        private void clearUser(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
//        {
//            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(body.GetValue("id").AsString))).FirstAsync()).Result;
//            target.Set("isfrozen", true);
//            target.Set("password", "defaultPassword");
//        }
//    }
//    /// <summary>
//    /// 用户拥有的东西
//    /// </summary>
//    [DataContract]
//    public class userobjects
//    {
//        [DataMember]
//        [BsonElement]
//        [BsonRepresentation(BsonType.Double)]
//        public double money { get; set; }
//        [DataMember]
//        [BsonElement]
//        //  [BsonRepresentation(BsonType.Document)]
//        public moneyEdit[] moneyEditRecord { get; set; }
//        [DataMember]
//        [BsonElement]
//        public freezeLog[] freezeLog { get; set; }
//    }
//    [DataContract]
//    [BsonDiscriminator("logMes")]
//    public class logMes
//    {
//        [DataMember]
//        [BsonElement("time")]
//        [BsonRepresentation(BsonType.Int64)]
//        public long time;
//        [DataMember]
//        [BsonElement("type")]
//        [BsonRepresentation(BsonType.Int32)]
//        public int type;
//        [DataMember]
//        [BsonElement("editContent")]
//        [BsonRepresentation(BsonType.String)]
//        public string editContent;
//        public logMes() { }
//        public logMes(long iTime, int iType, string iEditContent)
//        {
//            time = iTime;
//            type = iType;
//            editContent = iEditContent;
//        }
//    }
//    /// <summary>
//    /// 用户数据
//    /// </summary>
//    [DataContract]
//    [BsonDiscriminator("user")]
//    public class user
//    {

//        [DataMember]
//        [BsonElement("_id")]
//        [BsonRepresentation(BsonType.ObjectId)]
//        public ObjectId _id { get; set; }

//        [DataMember]
//        [BsonElement("username")]
//        [BsonRepresentation(BsonType.String)]
//        public string username { get; set; }

//        [DataMember]
//        [BsonElement("password")]
//        [BsonRepresentation(BsonType.String)]
//        public string password { get; set; }

//        [DataMember]
//        [BsonElement("authority")]
//        [BsonRepresentation(BsonType.Int32)]
//        public int authority { get; set; }

//        [DataMember]
//        [BsonElement("isfrozen")]
//        [BsonRepresentation(BsonType.Boolean)]
//        public bool isfrozen { get; set; }

//        [DataMember]
//        [BsonElement("userobjects")]
//        //     [BsonRepresentation(BsonType.Document)]
//        public userobjects userobjects { get; set; }

//        [DataMember]
//        [BsonElement("parent")]
//        [BsonRepresentation(BsonType.String)]
//        public string parent { get; set; }

//        [DataMember]
//        [BsonElement("child")]
//        //    [BsonRepresentation(BsonType.Array)]
//        public string[] child { get; set; }
//    }
//    /// <summary>
//    /// 被ban的ip
//    /// </summary>
//    public class blockedip
//    {
//        public ObjectId _id { get; set; }
//        public string ip { get; set; }
//        public int leftwrongtimes { get; set; }
//        public long servertime { get; set; }
//    }
//    [DataContract]
//    public class moneyEdit
//    {
//        [DataMember]
//        [BsonElement("editTime")]
//        [BsonRepresentation(BsonType.Int64)]
//        public long editTime { get; set; }
//        [DataMember]
//        [BsonElement("editMoney")]
//        [BsonRepresentation(BsonType.Double)]
//        public double editMoney { get; set; }
//        [DataMember]
//        [BsonElement("curMoney")]
//        [BsonRepresentation(BsonType.Double)]
//        public double curMoney { get; set; }
//        public moneyEdit() { }
//        public moneyEdit(long eT, double eM, double cM)
//        {
//            this.editMoney = eM;
//            this.editTime = eT;
//            this.curMoney = cM;
//        }
//    }
//    [DataContract]
//    public class freezeLog
//    {
//        [DataMember]
//        [BsonElement("freezeTime")]
//        [BsonRepresentation(BsonType.Int64)]
//        public long freezeTime { get; set; }
//        [DataMember]
//        [BsonElement("freezeType")]
//        [BsonRepresentation(BsonType.Boolean)]
//        public bool freezeType { get; set; }
//        public freezeLog() { }
//        public freezeLog(long efreezeTime, bool efreezeType)
//        {
//            this.freezeTime = efreezeTime;
//            this.freezeType = efreezeType;
//        }
//    }
//    public class nameTree
//    {
//        public string name;
//        public nameTree[] child;
//    }
//}

