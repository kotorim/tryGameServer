using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TouchSocket.Http;
using TouchSocket.Sockets;

namespace Baccarat_Server.webApi
{
    public class gameManager : postClass
    {
        protected override void onPost(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            e.Context.Request.Headers.TryGetValue("flag", out string flag);
            if (!string.IsNullOrEmpty(flag))
            {
                switch (flag)
                {
                    case "usercathectic":
                        userCathectic(client, e, body);
                        break;
                    case "editcathecticedge":
                        editCathecticEdge(client, e, body);
                        break;
                    default: break;
                }
            }
        }
        private void userCathectic(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            var target = Task.Run(async () => await webHelper.dbSearch<BsonDocument>("Baccarat", "videomessage", Builders<BsonDocument>.Filter.Eq("name", body.GetValue("videoName").AsString)).FirstAsync()).Result;
            var user = Task.Run(async () => await webHelper.dbSearch<BsonDocument>("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("_id"))).FirstAsync()).Result;
            timeStamps stamp = BsonSerializer.Deserialize<timeStamps>(target.GetValue("timeStamps").AsBsonArray[body.GetValue("curTimeStamp").AsInt32].AsBsonDocument);
            double userGetMoney = 0d;
            BsonDocument response = new BsonDocument();
            BsonDocument cathecticLog = new BsonDocument();
            cathecticLog.Add("type", "下注");
            bool zhuangPair = checkPair(stamp.ZhuangCard);
            bool xianPair = checkPair(stamp.XianCard);
            int isZhuangWin = checkWin(stamp);
            response.Add("zhuangPair", zhuangPair);
            response.Add("xianPair", xianPair);
            response.Add("isZhuangWin", isZhuangWin);
            cathecticLog.Add("isZhuangPair", zhuangPair);
            cathecticLog.Add("isXianPair", xianPair);
            cathecticLog.Add("isZhuangWin", isZhuangWin);
            if (false == valueIsNull(body.GetValue("cZhuangPair")))
            {
                cathecticLog.Add("zhuangPair", body.GetValue("cZhuangPair").AsInt32);
                userGetMoney += (zhuangPair ? body.GetValue("cZhuangPair").AsInt32 * 11 : -body.GetValue("cZhuangPair").AsInt32);
            }
            else
            {
                cathecticLog.Add("zhuangPair", 0);
            }
            if (false == valueIsNull(body.GetValue("cXianPair")))
            {
                cathecticLog.Add("xianPair", body.GetValue("cXianPair").AsInt32);
                userGetMoney += (xianPair ? body.GetValue("cXianPair").AsInt32 * 11 : -body.GetValue("cXianPair").AsInt32);
            }
            else
            {
                cathecticLog.Add("xianPair", 0);
            }
            if (isZhuangWin == 0)
            {
                if (false == valueIsNull(body.GetValue("cDraw")))
                {
                    cathecticLog.Add("draw", body.GetValue("cDraw").AsInt32);
                    userGetMoney += (isZhuangWin == 0 ? body.GetValue("cDraw").AsInt32 * 8 : -body.GetValue("cDraw").AsInt32);
                }
                else
                {
                    cathecticLog.Add("draw", 0);
                }
                cathecticLog.Add("zhuangWin", 0);
                cathecticLog.Add("xianWin", 0);
            }
            else
            {
                if (false == valueIsNull(body.GetValue("cZhuangWin")))
                {
                    cathecticLog.Add("zhuangWin", body.GetValue("cZhuangWin").AsInt32);
                    if (isZhuangWin > 0)
                    {
                        userGetMoney += (0.95d * body.GetValue("cZhuangWin").AsInt32);
                    }
                    else
                    {
                        userGetMoney -= body.GetValue("cZhuangWin").AsInt32;
                    }
                }
                else
                {
                    cathecticLog.Add("zhuangWin", 0);
                }

                if (false == valueIsNull(body.GetValue("cXianWin")))
                {
                    cathecticLog.Add("xianWin", body.GetValue("cXianWin").AsInt32);
                    userGetMoney += (isZhuangWin < 0 ? body.GetValue("cXianWin").AsInt32 : -body.GetValue("cXianWin").AsInt32);
                }
                else
                {
                    cathecticLog.Add("xianWin", 0);
                }
                if (false == valueIsNull(body.GetValue("cDraw")))
                {
                    cathecticLog.Add("draw", body.GetValue("cDraw").AsInt32);
                    userGetMoney -= body.GetValue("cDraw").AsInt32;
                }
                else
                {
                    cathecticLog.Add("draw", 0);
                }
            }
            cathecticLog.Add("editTime", Tools.tools.ToUnixTime(DateTime.UtcNow));
            cathecticLog.Add("userGetMoney", userGetMoney);
            //  cathecticLog.Add("curMoney", user.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble() + userGetMoney);
            response.Add("userGetMoney", userGetMoney);
            response.Add("curMoney", user.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble() + userGetMoney);
            webHelper.tryAddArrayValue("userCathectics", user.GetValue("userobjects").AsBsonDocument, cathecticLog);
            var regResult = Task.Run(async () => await webHelper.dbSave<BsonDocument>("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", user.GetValue("_id").AsObjectId), user)).Result;
            postClassNewer.getInstance().moneyManager_Instance.editMoney(
             user.GetValue("_id").AsObjectId,
             moneyEditType.cathectic,
             userGetMoney,
             "于房间 " + body.GetValue("roomId").AsInt32.ToString() + "投注" + (userGetMoney >= 0 ? "收益" : "损失") + Math.Abs(userGetMoney) + "代币"
                );

            //postClassNewer.getInstance().login_Instance.editUserMoney_new(
            //    user.GetValue("_id").AsObjectId, userGetMoney,
            //    "于房间 " + body.GetValue("roomId").AsInt32.ToString() + "投注" + (userGetMoney >= 0 ? "收益" : "损失") + Math.Abs(userGetMoney) + "代币",
            //    moneyEditType.cathectic);
            // (userGetMoney >= 0 ? moneyEditType.add : moneyEditType.sub));
            response.Add("success", 0);
            e.Context.Response.SetContent(response.ToBson());
        }
        private void editCathecticEdge(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            var user = Task.Run(async () => await webHelper.dbSearch<BsonDocument>("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("id"))).FirstAsync()).Result;
            BsonDocument response = new BsonDocument();
            if (user.GetValue("authority").AsInt32 == 3)
            {
                long num = body.GetValue("cathecticEdge").ToInt64();
                if (num < 100)
                {
                    num = 100;
                    response.Add("success", 2);
                }
                else if (num > 100000)
                {
                    num = 100000;
                    response.Add("success", 3);
                }
                else
                {
                    response.Add("success", 0);
                }
                (user.GetValue("userobjects").AsBsonDocument).Set("cathecticEdge", num);
                Task.Run(async () => await webHelper.dbSave<BsonDocument>("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("id")), user));
                e.Context.Response.SetContent(response.ToBson());
            }
            else
            {
                response.Add("success", 1);
            }
        }
        private bool checkPair(cardMes[] inner)
        {
            if (null != inner && inner.Length >= 2 && inner[0].CardNumberInJason == inner[1].CardNumberInJason)
            {
                return true;
            }
            //for (int q = 0; q < 2; q++)
            //{
            //    for (int w = q; w < inner.Length; w++)
            //    {
            //        if (q != w && inner[q].CardNumberInJason == inner[w].CardNumberInJason)
            //        {
            //            return true;
            //        }
            //    }
            //}
            return false;
        }
        private int checkWin(timeStamps inner)
        {
            int zhuangPoint = 0;
            int xianPoint = 0;
            for (int q = 0; q < inner.ZhuangCard.Length; q++)
            {
                if (int.Parse(inner.ZhuangCard[q].CardNumberInJason) < 10)
                    zhuangPoint += int.Parse(inner.ZhuangCard[q].CardNumberInJason);
            }
            for (int w = 0; w < inner.XianCard.Length; w++)
            {
                if (int.Parse(inner.XianCard[w].CardNumberInJason) < 10)
                    xianPoint += int.Parse(inner.XianCard[w].CardNumberInJason);
            }
            zhuangPoint %= 10;
            xianPoint %= 10;
            return (zhuangPoint - xianPoint) > 0 ? 1 : ((zhuangPoint - xianPoint == 0) ? 0 : -1);
        }
        private bool valueIsNull(BsonValue value)
        {
            if (false == value.IsBsonNull && value.AsInt32 > 0)
            {
                return false;
            }
            return true;
        }
    }
    [DataContract]
    public class timeStamps
    {
        [DataMember]
        [BsonElement]
        //[BsonRepresentation(BsonType.Double)]
        public double VideoTimeInJason;
        [DataMember]
        [BsonElement]
        //[BsonRepresentation(BsonType.Document)]
        public cardMes[] ZhuangCard;
        [DataMember]
        [BsonElement]
        //[BsonRepresentation(BsonType.Document)]
        public cardMes[] XianCard;
    }
    [DataContract]
    public class cardMes
    {
        [DataMember]
        [BsonElement]
        // [BsonRepresentation(BsonType.String)]
        public string CardSuitInJason;
        [DataMember]
        [BsonElement]
        // [BsonRepresentation(BsonType.String)]
        public string CardNumberInJason;
    }
}
