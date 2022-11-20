using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TouchSocket.Core.XREF.Newtonsoft.Json;
using TouchSocket.Http;
using TouchSocket.Sockets;

namespace Baccarat_Server.webApi
{
    public class userManager : postClass
    {
        protected override void onPost(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            e.Context.Request.Headers.TryGetValue("flag", out string flag);
            if (!string.IsNullOrEmpty(flag))
            {
                switch (flag)
                {
                    case "login":
                        onLogin(client, e, body);
                        break;
                    case "resetpassword":
                        onResetPassword(client, e, body);
                        break;
                    //  case "editmoney":
                    //    editUserMoney(client, e, body);
                    // break;
                    case "getusertree":
                        getUserTree(client, e, body);
                        break;
                    case "setisfrozen":
                        setFrozen(client, e, body);
                        break;
                    case "getusermes":
                        getUserMes(client, e, body);
                        break;
                    case "setactive":
                        setActive(client, e, body);
                        break;
                    case "createuser":
                        createUser(client, e, body);
                        break;
                    case "taguser":
                        tagUser(client, e, body);
                        break;
                    case "userforgetpassword":
                        userForgetPassword(client, e, body);
                        break;
                    default:
                        break;
                }
            }
        }
        protected override void onGet(ITcpClientBase client, HttpContextEventArgs e)
        {
            e.Context.Request.Headers.TryGetValue("flag", out string flag);
            if (!string.IsNullOrEmpty(flag))
            {
                switch (flag)
                {
                    case "getallnametree":
                        getAllNameTree(client, e);
                        break;
                    case "getallmes":
                        getAllMes(client, e);
                        break;
                }
            }
        }
        public static void getUserTree(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            var all = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.In("_id", body.GetValue("message").AsBsonArray)).ToListAsync()).Result;
            var arr = new BsonArray();
            all.ForEach((inner) =>
            {
                // double tmp = inner.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble();
                inner.GetValue("userobjects").AsBsonDocument.Remove("moneyEditRecord");
                inner.GetValue("userobjects").AsBsonDocument.Remove("userCathectics");
                inner.GetValue("userobjects").AsBsonDocument.Remove("freezeLog");
                // inner.Remove("userobjects");
                // inner.Add("userobjects", new BsonDocument(new BsonElement("money", tmp)));
                arr.Add(inner);
            });
            var res = new BsonDocument();
            res.Add("message", arr);
            res.Add("success", 0);
            e.Context.Response.SetContent(res.ToBson());
        }
        public static void getAllNameTree(ITcpClientBase client, HttpContextEventArgs e)
        {
            var all = Task.Run(async () => await webHelper.dbSearch<BsonDocument>("Baccarat", "user", Builders<BsonDocument>.Filter.Ne("_id", new ObjectId("62d8014d24db311a1b282b3c"))).ToListAsync()).Result;
            for (int q = 0; q < all.Count; q++)
            {
                //   if (string.Equals("admin", all[q].GetValue("username").AsString))
                if (all[q].GetValue("_id").AsObjectId == new ObjectId("62cd9353fb515c410d32173e"))
                {
                    nameTree a = new nameTree();
                    a.id = all[q].GetValue("_id").AsObjectId;
                    a.name = all[q].GetValue("username").AsString;
                    getTree(ref a, all, all[q]);
                    //all.RemoveAt(q);
                    //var list = all[q].GetValue("child").AsBsonArray;
                    //a.child = new nameTree[list.Count];
                    //for (int w = 0; w < list.Count; w++)
                    //{
                    //    a.child[w] = new nameTree();
                    //    a.child[w].id = list[w].AsObjectId;
                    //    a.child[w].name = list[w]
                    //}
                    var res = a.ToBsonDocument();
                    res.Add("success", 0);
                    e.Context.Response.SetContent(res.ToBson());
                    break;
                }
            }
        }
        public static void getAllMes(ITcpClientBase client, HttpContextEventArgs e)
        {
            var all = Task.Run(async () => await webHelper.dbSearch<BsonDocument>("Baccarat", "user", Builders<BsonDocument>.Filter.Empty).ToListAsync()).Result;

            for (int q = 0; q < all.Count; q++)
            {
                e.Context.Response.WriteContent(all[q].ToBson());
            }
        }
        public static void getTree(ref nameTree parent, List<BsonDocument> all, BsonDocument target)
        {
            var array = target.GetValue("child").AsBsonArray;
            for (int q = 0; q < array.Count; q++)
            {
                for (int w = 0; w < all.Count; w++)
                {
                    if (array[q].AsObjectId == all[w].GetValue("_id").AsObjectId)
                    {
                        var one = new nameTree();
                        one.name = all[w].GetValue("username").AsString;
                        one.id = all[w].GetValue("_id").AsObjectId;
                        parent.child.Add(one);
                        getTree(ref one, all, all[w]);
                    }
                }
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="client"></param>
        /// <param name="e"></param>
        public static void onLogin(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            //取得banip数据
            blockedip tmpip = getBlockedIP(client.IP);
            if ("defaultUsername" == body.GetValue("username"))
            {
                e.Context.Response.SetContent(responseDoc.success(6).ToBson());
                return;
            }
            //判断banip
            if (tmpip.leftwrongtimes == -1)
            {
                e.Context.Response.SetContent(responseDoc.success(2).ToBson());
                // e.Context.Response.FromJson("{\"success\":2}");
                // Console.WriteLine("IP已经被ban");
                return;
            }
            try
            {
                //查询账号密码
                // user tmpUser = BsonSerializer.Deserialize<user>(body);   // JsonConvert.DeserializeObject<user>(e.Context.Request.GetBody());
                var doc = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("username", body.GetValue("username").AsString)).FirstAsync()).Result;
                if (String.Equals(body.GetValue("password").AsString, doc.GetValue("password").AsString))
                {
                    var result = new BsonDocument();
                    if (body.GetValue("authority").AsInt32 != doc.GetValue("authority").AsInt32)
                    {
                        result.Add("success", 5);
                        e.Context.Response.SetContent(result.ToBson());
                        return;
                    }
                    else if (true == doc.GetValue("isfrozen").AsBoolean)
                    {
                        result.Add("success", 3);
                        e.Context.Response.SetContent(result.ToBson());
                        return;
                    }
                    else if (false == doc.GetValue("active").AsBoolean)
                    {
                        result.Add("success", 4);
                        e.Context.Response.SetContent(result.ToBson());
                        return;
                    }
                    else
                    {
                        doc.Add("success", doc.GetValue("password").AsString == "123456" ? 7 : 0);
                        e.Context.Response.SetContent(doc.ToBson());
                        Console.WriteLine("正确登录");
                        return;
                    }
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                if (-1 == --tmpip.leftwrongtimes)
                {
                    tmpip.servertime = Tools.tools.ToUnixTime(DateTime.UtcNow);
                }
                try
                {
                    if (webHelper.dbUpdate("Baccarat", "blockedip", Builders<BsonDocument>.Filter.Eq("ip", tmpip.ip), Builders<BsonDocument>.Update.Set("leftwrongtimes", tmpip.leftwrongtimes).Set("servertime", tmpip.servertime)).Result.IsAcknowledged)
                    {
                        Console.WriteLine("记录ip信息成功:");
                    }
                }
                catch (Exception ex2)
                {
                    Console.WriteLine(Tools.tools.logError("记录ip信息失败:" + ex2.Message));
                }
                e.Context.Response.SetContent(responseDoc.success(1).ToBson());
                Console.WriteLine("账号或者密码错误，登录失败");
                return;
            }
        }
        /// <summary>
        /// 改密
        /// </summary>
        /// <param name="client"></param>
        /// <param name="e"></param>
        public static void onResetPassword(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            BsonDocument target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("id").AsObjectId)).FirstAsync()).Result;
            target.Set("password", body.GetValue("password"));
            if (target.GetValue("authority").ToInt32() == 3)
            {
                var tmper = target.GetValue("userObjects").AsBsonDocument;
                if (true == tmper.Contains("forgetPassword"))
                {
                    tmper.Set("forgetPassword", false);
                }
            }
            var tmp = Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("id").AsObjectId), target)).Result;
            if (tmp.ModifiedCount == 1)
            {
                e.Context.Response.SetContent(responseDoc.success().ToBson());
            }
            else
            {
                e.Context.Response.SetContent(responseDoc.success(1).ToBson());
            }
        }
        /// <summary>
        /// 查询是否在banip列表，不在就创建一个
        /// </summary>
        /// <param name="innerIP">IP地址</param>
        /// <returns>blockedip:是否在ip列表</returns>
        public static blockedip getBlockedIP(string innerIP)
        {
            long tTime = Tools.tools.ToUnixTime(DateTime.UtcNow);
            try
            {
                var doc = Task.Run(async () => await webHelper.dbSearch("Baccarat", "blockedip", Builders<BsonDocument>.Filter.Eq("ip", innerIP)).FirstAsync()).Result;
                Console.WriteLine("找到banip返回已有条目");
                if (tTime - doc.GetValue("servertime").AsInt64 > 5184000)
                {
                    doc.Set("leftwrongtimes", 10);
                    doc.Set("servertime", tTime);
                    Task.Run(async () => await webHelper.dbUpdate("Baccarat", "blockedip", Builders<BsonDocument>.Filter.Eq("ip", innerIP), Builders<BsonDocument>.Update.Set("servertime", tTime).Set("leftwrongtimes", 10)));
                }
                return BsonSerializer.Deserialize<blockedip>(doc);
            }
            catch (Exception)
            {
                blockedip target = new blockedip();
                target.ip = innerIP;
                target.leftwrongtimes = 10;
                target.servertime = tTime;
                webHelper.dbAdd("Baccarat", "blockedip", target);
                Console.WriteLine("没找到banip返回新建条目");
                return target;
            }
        }


        private void editUserMoney(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            e.Context.Response.SetContent(
                editUserMoney_new(
                    body.GetValue("id").AsObjectId,
                    body.GetValue("editMoney").ToDouble(),
                    body.GetValue("editDetail").AsString,
                    (moneyEditType)body.GetValue("editType").AsInt32)
                .ToBson());
            return;
        }
        public BsonDocument editUserMoney_new(ObjectId id, double money, string editDetail, moneyEditType editType)
        {
            BsonDocument child = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", id)).FirstAsync()).Result;
            if (editType == moneyEditType.add)
            {
                var res = editOneMoney(child.GetValue("parent").AsObjectId,
                    -money, "使子成员增加代币导致自身减少" + Math.Abs(money) + "代币",
                    moneyEditType.sub);
                switch (res.GetValue("success").AsInt32)
                {
                    case 0:
                        return editOneMoney(id, money, editDetail, editType);
                    case 1:
                        return res;
                    case 2:
                        res.Set("success", 5);
                        return res;
                    case 3:
                        res.Set("success", 6);
                        return res;
                    default:
                        return res;
                }
            }
            else if (editType == moneyEditType.sub)
            {
                var res = editOneMoney(id, money, editDetail, editType);
                if (0 == res.GetValue("success").AsInt32)
                {
                    return editOneMoney(child.GetValue("parent").AsObjectId,
                    -money, "使子成员减少代币导致自身增加" + Math.Abs(money) + "代币",
                    moneyEditType.add);
                }
                else
                {
                    return res;
                }
            }
            else
            {
                return editOneMoney(id, money, editDetail, editType);
            }
        }
        private BsonDocument editOneMoney(ObjectId id, double editMoney, string editDetail, moneyEditType editType)
        {
            BsonDocument cur = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", id)).FirstAsync()).Result;
            double curMoney = cur.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble();
            if (0 == editMoney)
            {
                return responseDoc.success(1);
            }
            else if (editMoney + curMoney < 0)
            {
                return responseDoc.success(2);
            }
            else if (3 != cur.GetValue("authority").ToInt32() && editMoney > 0 && cur.GetValue("userobjects").AsBsonDocument.GetValue("moneyMax").ToDouble() <= curMoney)
            {
                return responseDoc.success(3);
            }
            else
            {
                var moneyEdit = new moneyEdit(
                    Tools.tools.ToUnixTime(DateTime.UtcNow),
                    Math.Abs(editMoney),
                    editMoney + curMoney,
                    editDetail,
                    editType);
                cur.GetValue("userobjects").AsBsonDocument.Set("money", editMoney + curMoney);
                webHelper.tryAddArrayValue("moneyEditRecord", cur.GetValue("userobjects").AsBsonDocument, moneyEdit.ToBsonDocument());
                if (1 == Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", id), cur)).Result.ModifiedCount)
                {
                    return new BsonDocument() { new BsonElement("success", 0), new BsonElement("currentMoney", moneyEdit.curMoney) };
                }
                return responseDoc.success(4);
            }
        }

        private void setFrozen(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.In("_id", body.GetValue("names").AsBsonArray)).ToListAsync()).Result;
            var log = new freezeLog(Tools.tools.ToUnixTime(DateTime.UtcNow), body.GetValue("isfrozen").AsBoolean);
            for (int q = 0; q < target.Count; q++)
            {
                webHelper.tryAddArrayValue("freezeLog", target[q].GetValue("userobjects").AsBsonDocument, log.ToBsonDocument());
                target[q].Set("isfrozen", body.GetValue("isfrozen").AsBoolean);
                var result = Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", target[q].GetValue("_id")), target[q])).Result;
                if (result.ModifiedCount <= 0)
                {
                    throw new Exception("错误的id");
                }
            }
            e.Context.Response.SetContent(responseDoc.success().ToBson());
        }
        private void getUserMes(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("id").AsObjectId)).FirstAsync()).Result;
            var doc = target.GetValue("userobjects").AsBsonDocument;
            doc.Add("success", 0);
            e.Context.Response.SetContent(doc.ToBson());
        }
        private void setActive(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.In("_id", body.GetValue("mes").AsBsonArray)).ToListAsync()).Result;
            bool active = body.GetValue("active").AsBoolean;
            target.ForEach((inner) =>
            {
                actTarget(inner, active);
                Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", inner.GetValue("_id").AsObjectId), inner));
            });
            BsonDocument response = new BsonDocument();
            response.Add("mes", new BsonArray(target));
            response.Add("success", 0);
            e.Context.Response.SetContent(response.ToBson());
        }
        private void createUser(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            user tmpUser = new user();
            tmpUser.userobjects = new userobjects();
            int type = body.GetValue("userType").AsInt32;
            if (type < 1 || type > 3)
            {
                e.Context.Response.SetContent(responseDoc.success(1).ToBson());
                return;
            }
            long counter = Task.Run(async () => await webHelper.dbCounter<BsonDocument>("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("authority", type) &
                Builders<BsonDocument>.Filter.Eq("parent", body.GetValue("parentId").AsObjectId))).Result;
            switch (type)
            {
                case 1:
                    if (counter >= 30)
                    {
                        e.Context.Response.SetContent(responseDoc.success(2).ToBson());
                        return;
                    }
                    tmpUser.userobjects.moneyMax = 100000000d;
                    break;
                case 2:
                    if (counter >= 50)
                    {
                        e.Context.Response.SetContent(responseDoc.success(2).ToBson());
                        return;
                    }
                    tmpUser.userobjects.moneyMax = 10000000d;
                    break;
                case 3:
                    if (counter >= 100)
                    {
                        e.Context.Response.SetContent(responseDoc.success(2).ToBson());
                        return;
                    }
                    tmpUser.userobjects.moneyMax = 1000000d;
                    tmpUser.userobjects.cathecticEdge = 100L;
                    break;
            }
            tmpUser.userobjects.money = 0;
            tmpUser._id = ObjectId.GenerateNewId();
            tmpUser.active = false;
            tmpUser.isfrozen = true;
            tmpUser.username = "defaultUsername";
            tmpUser.password = "123456";
            tmpUser.authority = type;
            tmpUser.parent = body.GetValue("parentId").AsObjectId;
            tmpUser.child = new ObjectId[0];
            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("parentId").AsObjectId)).FirstAsync()).Result;
            var tmp = target.GetValue("child").AsBsonArray;
            tmp.Add(tmpUser._id);
            var a = Task.Run(async () => await webHelper.dbUpdate("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("parentId").AsObjectId), Builders<BsonDocument>.Update.Set("child", tmp))).Result;
            if (a.ModifiedCount > 0)
            {
                webHelper.dbAdd("Baccarat", "user", tmpUser);
                var response = responseDoc.success();
                response.Add("id", tmpUser._id);
                e.Context.Response.SetContent(response.ToBson());
            }
            return;
        }
        private void actTarget(BsonDocument target, bool active)
        {
            target.Set("active", active);
            if (false == active)
            {
                target.Set("password", "123456");
                target.Set("username", "defaultUsername");
                target.Set("isfrozen", true);
                var uo = new userobjects();
                switch (target.GetValue("authority").ToInt32())
                {
                    case 1:
                        uo.moneyMax = 100000000d;
                        break;
                    case 2:
                        uo.moneyMax = 10000000d;
                        break;
                    case 3:
                        uo.moneyMax = 1000000d;
                        uo.cathecticEdge = 100L;
                        break;
                }
                target.Set("userobjects", uo.ToBsonDocument());
            }
            else
            {
                target.Set("isfrozen", false);
                target.Set("username", randomUserName());
            }
        }
        private void tagUser(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("id").AsObjectId)).FirstAsync()).Result;
            BsonDocument userObject = target.GetValue("userobjects").AsBsonDocument;
            if (false == userObject.Contains("userTag"))
            {
                userObject.Add("userTag", body.GetValue("userTag").ToInt32());
            }
            else
            {
                userObject.Set("userTag", body.GetValue("userTag").ToInt32());
            }
            var tmp = Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", target.GetValue("_id").AsObjectId), target));
            if (tmp.Result.ModifiedCount > 0)
            {
                e.Context.Response.SetContent(responseDoc.success().ToBson());
            }
        }

        private void userForgetPassword(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            try
            {
                var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("username", body.GetValue("username").ToString())).FirstAsync()).Result;
                BsonDocument userObject = target.GetValue("userobjects").AsBsonDocument;
                if (false == userObject.Contains("forgetPassword"))
                {
                    userObject.Add("forgetPassword", true);
                }
                else
                {
                    userObject.Set("forgetPassword", true);
                }
                var tmp = Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", target.GetValue("_id").AsObjectId), target));
                if (tmp.Result.ModifiedCount > 0)
                {
                    e.Context.Response.SetContent(responseDoc.success().ToBson());
                }
            }
            catch (Exception ex)
            {
                e.Context.Response.SetContent(responseDoc.success(1).ToBson());
            }
        }
        private static string chars = "mb3ayz8j7drckgxesinp5vowfq6lu914th20";
        private string randomUserName()
        {
            long count = (webHelper.dbClient.GetDatabase("Baccarat").GetCollection<BsonDocument>("user").CountDocuments(Builders<BsonDocument>.Filter.Eq("active", true)));
            if (1048575 < count)
            {
                throw new Exception("超过最大用户数量");
            }
            else
            {
                string a = "ca" + IntToi32(count);
                while (a.Length < 6)
                {
                    a += chars[Tools.tools.random.Next(0, 4)];
                }

                return a;
            }
        }
        public static string IntToi32(long xx)
        {
            string a = "";
            while (xx >= 1)
            {
                int index = Convert.ToInt16(xx - (xx / 32) * 32);
                a = chars[index + 4] + a;
                xx = xx / 32;
            }
            return a;
        }
    }
    /// <summary>
    /// 用户拥有的东西
    /// </summary>
    [DataContract]
    public class userobjects
    {
        [DataMember]
        [BsonElement]
        [BsonRepresentation(BsonType.Double)]
        public double money { get; set; }
        [DataMember]
        [BsonElement]
        [BsonRepresentation(BsonType.Double)]
        public double moneyMax { get; set; }
        [DataMember]
        [BsonElement]
        [BsonRepresentation(BsonType.Int64)]
        public long cathecticEdge { get; set; }

        [DataMember]
        [BsonElement]
        //  [BsonRepresentation(BsonType.Document)]
        public moneyEdit[] moneyEditRecord { get; set; }
        [DataMember]
        [BsonElement]
        public freezeLog[] freezeLog { get; set; }
        [DataMember]
        [BsonElement]
        public userCathectics[] userCathectics { get; set; }
    }
    [DataContract]
    [BsonDiscriminator("logMes")]
    public class logMes
    {
        [DataMember]
        [BsonElement("time")]
        [BsonRepresentation(BsonType.Int64)]
        public long time;
        [DataMember]
        [BsonElement("type")]
        [BsonRepresentation(BsonType.Int32)]
        public int type;
        [DataMember]
        [BsonElement("editContent")]
        [BsonRepresentation(BsonType.String)]
        public string editContent;
        public logMes() { }
        public logMes(long iTime, int iType, string iEditContent)
        {
            time = iTime;
            type = iType;
            editContent = iEditContent;
        }
    }
    /// <summary>
    /// 用户数据
    /// </summary>
    [DataContract]
    [BsonDiscriminator("user")]
    public class user
    {

        [DataMember]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [DataMember]
        [BsonElement("active")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool active { set; get; }
        [DataMember]
        [BsonElement("username")]
        [BsonRepresentation(BsonType.String)]
        public string username { get; set; }

        [DataMember]
        [BsonElement("password")]
        [BsonRepresentation(BsonType.String)]
        public string password { get; set; }

        [DataMember]
        [BsonElement("authority")]
        [BsonRepresentation(BsonType.Int32)]
        public int authority { get; set; }

        [DataMember]
        [BsonElement("isfrozen")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool isfrozen { get; set; }

        [DataMember]
        [BsonElement("userobjects")]
        //     [BsonRepresentation(BsonType.Document)]
        public userobjects userobjects { get; set; }

        [DataMember]
        [BsonElement("parent")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId parent { get; set; }

        [DataMember]
        [BsonElement("child")]
        //    [BsonRepresentation(BsonType.Array)]
        public ObjectId[] child { get; set; }
    }
    /// <summary>
    /// 被ban的ip
    /// </summary>
    public class blockedip
    {
        public ObjectId _id { get; set; }
        public string ip { get; set; }
        public int leftwrongtimes { get; set; }
        public long servertime { get; set; }
    }
    [DataContract]
    public class moneyEdit
    {
        [DataMember]
        [BsonElement("editTime")]
        [BsonRepresentation(BsonType.Int64)]
        public long editTime { get; set; }
        [DataMember]
        [BsonElement("editMoney")]
        [BsonRepresentation(BsonType.Double)]
        public double editMoney { get; set; }
        [DataMember]
        [BsonElement("curMoney")]
        [BsonRepresentation(BsonType.Double)]
        public double curMoney { get; set; }
        [DataMember]
        [BsonElement("editDetail")]
        [BsonRepresentation(BsonType.String)]
        public string editDetail { get; set; }
        [DataMember]
        [BsonElement("editType")]
        [BsonRepresentation(BsonType.Int32)]
        public int editType { get; set; }
        public moneyEdit() { }
        public moneyEdit(long eT, double eM, double cM, string eD, moneyEditType eTT)
        {
            this.editMoney = eM;
            this.editTime = eT;
            this.curMoney = cM;
            this.editDetail = eD;
            this.editType = ((int)eTT);
        }
    }
    [DataContract]
    public class freezeLog
    {
        [DataMember]
        [BsonElement("freezeTime")]
        [BsonRepresentation(BsonType.Int64)]
        public long freezeTime { get; set; }
        [DataMember]
        [BsonElement("freezeType")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool freezeType { get; set; }
        public freezeLog() { }
        public freezeLog(long efreezeTime, bool efreezeType)
        {
            this.freezeTime = efreezeTime;
            this.freezeType = efreezeType;
        }
    }
    [DataContract]
    public class userCathectics
    {
        [DataMember]
        [BsonElement("editTime")]
        [BsonRepresentation(BsonType.Int64)]
        public long editTime { get; set; }

        [DataMember]
        [BsonElement("type")]
        [BsonRepresentation(BsonType.String)]
        public string type { get; set; }

        [DataMember]
        [BsonElement("zhuangPair")]
        [BsonRepresentation(BsonType.Int32)]
        public int zhuangPair { get; set; }

        [DataMember]
        [BsonElement("xianPair")]
        [BsonRepresentation(BsonType.Int32)]
        public int xianPair { get; set; }

        [DataMember]
        [BsonElement("xianWin")]
        [BsonRepresentation(BsonType.Int32)]
        public int xianWin { get; set; }


        [DataMember]
        [BsonElement("zhuangWin")]
        [BsonRepresentation(BsonType.Int32)]
        public int zhuangWin { get; set; }


        [DataMember]
        [BsonElement("draw")]
        [BsonRepresentation(BsonType.Int32)]
        public int draw { get; set; }


        [DataMember]
        [BsonElement("curMoney")]
        [BsonRepresentation(BsonType.Double)]
        public int curMoney { get; set; }


    }
    public class nameTree
    {
        public ObjectId id;
        public string name;
        public List<nameTree> child = new List<nameTree>();
    }
}

