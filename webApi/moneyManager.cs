using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TouchSocket.Http;
using TouchSocket.Sockets;

namespace Baccarat_Server.webApi
{
    public class moneyManager : postClass
    {
        protected override void onPost(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            e.Context.Request.Headers.TryGetValue("flag", out string flag);
            if (!string.IsNullOrEmpty(flag))
            {
                switch (flag)
                {
                    case "editmoney":
                        editUserMoney(client, e, body);
                        break;
                    case "getusermoney":
                        getUserMoney(client, e, body);
                        break;
                    default: break;
                }
            }
        }
        private void editUserMoney(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            double curMoney = 0;
            BsonDocument response;
            int editor = editMoney(body.GetValue("id").AsObjectId, (moneyEditType)body.GetValue("editType").AsInt32, body.GetValue("editMoney").ToDouble(), ref curMoney, body.Contains("editDetail") ? body.GetValue("editDetail").AsString : null);
            switch (editor)
            {
                case -1:
                    throw new Exception("代币编辑内部错误");
                case 0:
                    response = responseDoc.success(0);//成功
                    response.Add("currentMoney", curMoney);
                    break;
                default:
                    response = responseDoc.success(editor);
                    break;
            }
            e.Context.Response.SetContent(response.ToBson());
        }
        public int editMoney(ObjectId id, moneyEditType editType, double editMoney, ref double currentMoney, string editDetail = null)
        {
            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", id)).FirstAsync()).Result;
            var parent = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", target.GetValue("parent").AsObjectId)).FirstAsync()).Result;

            switch (editType)
            {
                case moneyEditType.add:
                case moneyEditType.sub:
                    switch (checkCouldEditMoney(target, editMoney))
                    {
                        case 0:
                            switch (checkCouldEditMoney(parent, -editMoney))
                            {
                                case 0:
                                    if (0 == directEditMoney(target, editMoney, (editMoney > 0 ? moneyEditType.add : moneyEditType.sub), "管理员" + (editMoney > 0 ? "增加" : "减少") + Math.Abs(editMoney).ToString() + "代币") &&
                                        0 == directEditMoney(parent, -editMoney, (editMoney > 0 ? moneyEditType.managerAdd : moneyEditType.managerSub), "给子成员" + (editMoney > 0 ? "增加" : "减少") + Math.Abs(editMoney).ToString() + "代币"))
                                    {
                                        currentMoney = target.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble();
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                case 1:
                                    return 3;
                                case -1:
                                    return 4;
                            }
                            break;
                        case 1:
                            return 1;
                        case -1:
                            return 2;
                    }
                    break;
                case moneyEditType.rebate:
                    directEditMoney(target, editMoney, moneyEditType.rebate, "来自子用户的返利" + editMoney.ToString() + "代币");
                    currentMoney = target.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble();
                    return 0;
                case moneyEditType.cathectic:
                    directEditMoney(target, editMoney, moneyEditType.cathectic, (null == editDetail ? "投注" + (editMoney > 0 ? "收益" : "损失") + Math.Abs(editMoney).ToString() + "代币" : editDetail));
                    currentMoney = target.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble();
                    return 0;
                default:
                    return -1;
            }
            return -1;
        }
        public int editMoney(ObjectId id, moneyEditType editType, double editMoney, string editDetail = null)
        {
            var target = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", id)).FirstAsync()).Result;
            var parent = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", target.GetValue("parent").AsObjectId)).FirstAsync()).Result;

            switch (editType)
            {
                case moneyEditType.add:
                case moneyEditType.sub:
                    switch (checkCouldEditMoney(target, editMoney))
                    {
                        case 0:
                            switch (checkCouldEditMoney(parent, -editMoney))
                            {
                                case 0:
                                    if (0 == directEditMoney(target, editMoney, (editMoney > 0 ? moneyEditType.add : moneyEditType.sub), "管理员" + (editMoney > 0 ? "增加" : "减少") + Math.Abs(editMoney).ToString() + "代币") &&
                                        0 == directEditMoney(parent, -editMoney, (editMoney > 0 ? moneyEditType.managerAdd : moneyEditType.managerSub), "使子成员" + (editMoney > 0 ? "增加" : "减少") + Math.Abs(editMoney).ToString() + "代币"))
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                case 1:
                                    return 3;
                                case -1:
                                    return 4;
                            }
                            break;
                        case 1:
                            return 1;
                        case -1:
                            return 2;
                        case 2: return 5;
                    }
                    break;
                case moneyEditType.rebate:
                    directEditMoney(target, editMoney, moneyEditType.rebate, "来自子用户的返利" + editMoney.ToString() + "代币");
                    return 0;
                case moneyEditType.cathectic:
                    directEditMoney(target, editMoney, moneyEditType.cathectic, (null == editDetail ? "投注" + (editMoney > 0 ? "收益" : "损失") + Math.Abs(editMoney).ToString() + "代币" : editDetail));
                    return 0;
                default:
                    return -1;
            }
            return -1;
        }

        private int checkCouldEditMoney(BsonDocument target, double editMoney)
        {
            BsonDocument uo = target.GetValue("userobjects").AsBsonDocument;
            double curMoney = uo.GetValue("money").ToDouble();
            double moneyMax = uo.GetValue("moneyMax").ToDouble();
            if (curMoney + editMoney > moneyMax)
            {
                return 1;
            }
            else if (curMoney + editMoney < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        private int directEditMoney(BsonDocument target, double editMoney, moneyEditType editType, string editDetail)
        {
            var moneyEdit = new moneyEdit(
                Tools.tools.ToUnixTime(DateTime.UtcNow),
                Math.Abs(editMoney),
                editMoney + target.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble(),
                editDetail,
                editType);
            target.GetValue("userobjects").AsBsonDocument.Set("money", moneyEdit.curMoney);
            webHelper.tryAddArrayValue("moneyEditRecord", target.GetValue("userobjects").AsBsonDocument, moneyEdit.ToBsonDocument());
            if (1 == Task.Run(async () => await webHelper.dbSave("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", target.GetValue("_id")), target)).Result.ModifiedCount)
            {
                return 0;
            }
            return 1;
        }
        private void getUserMoney(ITcpClientBase client, HttpContextEventArgs e, BsonDocument body)
        {
            BsonDocument docRes = Task.Run(async () => await webHelper.dbSearch("Baccarat", "user", Builders<BsonDocument>.Filter.Eq("_id", body.GetValue("_id").AsObjectId)).FirstAsync()).Result;
            var doc = new BsonDocument();
            doc.Add("success", 0);
            doc.Add("money", docRes.GetValue("userobjects").AsBsonDocument.GetValue("money").ToDouble());
            e.Context.Response.SetContent(doc.ToBson());
        }
    }
    public enum moneyEditType
    {
        add = 0,
        sub = 1,
        rebate = 2,
        cathectic = 3,
        managerAdd = 4,
        managerSub = 5
    }
}
