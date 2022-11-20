using Baccarat_Client_Manager.Tools;
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
using TouchSocket.Http;

namespace Baccarat_Client_Manager.Forms
{
    public partial class userManager : Form
    {
        public userManager()
        {
            InitializeComponent();
            getNameTree();
        }
        // private List<BsonDocument> savedDoc = new List<BsonDocument>();
        private void getCurNameList(TreeNode curDoc, ref StringBuilder needGetDoc)
        {
            needGetDoc.Append("ObjectId(\"");
            needGetDoc.Append(curDoc.Tag);
            needGetDoc.Append("\")");
            needGetDoc.Append(',');
            if (curDoc.IsExpanded)
            {
                for (int q = 0; q < curDoc.GetNodeCount(false); q++)
                {
                    getCurNameList(curDoc.Nodes[q], ref needGetDoc);
                }
            }
        }
        private void reFreshGrid()
        {
            this.nameGrid.Invoke(new Action<DataGridView>((inner) =>
            {
                StringBuilder body = new StringBuilder();
                body.Append("{\"message\":[");
                getCurNameList(this.nameTree.Nodes[0].Nodes[0], ref body);
                body.Remove(body.Length - 1, 1);
                body.Append("]}");
                requestStaff rStaff = webHelper.postRequest(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "getusertree") }, body.ToString());
                rStaff.onResponse += (BsonDocument response) =>
                {
                    switch (response.GetValue("success").AsInt32)
                    {
                        case 0:
                            var list = response.GetValue("message").AsBsonArray;
                            this.nameGrid.Invoke(new Action<DataGridView>((inner2) =>
                            {
                                inner2.Rows.Clear();
                                for (int q = 0; q < list.Count; q++)
                                {
                                    var isfrozen = list[q].AsBsonDocument.GetValue("isfrozen").AsBoolean;
                                    inner2.Rows.Add(list[q].AsBsonDocument.GetValue("username").AsString,
                                         isfrozen ? "冻结中" : "正常");
                                    inner2.Rows[q].Cells[2].Value = isfrozen ? "恢复" : "冻结";
                                    inner2.Rows[q].Cells[3].Value = "删除";
                                    inner2.Rows[q].Cells[4].Value = "查询";
                                    inner2.Rows[q].Cells[0].Tag = list[q].AsBsonDocument.GetValue("_id").AsObjectId;
                                }
                            }), this.nameGrid);
                            break;
                        default:
                            MessageBox.Show("grid获取失败");
                            break;
                    }
                };
                rStaff.send();
            }), this.nameGrid);
        }
        private void getNameTree()
        {
            requestStaff rStaff = webHelper.getRequest(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "getallnametree") });
            rStaff.onResponse += (BsonDocument body) =>
            {
                if (0 == body.GetValue("success").AsInt32)
                {
                    body.Remove("success");
                    this.nameTree.Invoke(new Action<TreeView>((a) =>
                    {
                        a.Nodes[0].Nodes.Clear();
                        foreachTree(body, nameTree.Nodes[0]);
                    }), this.nameTree);
                }
                else
                {
                    MessageBox.Show("树形图获取失败，请联系服务端");
                }
            };
            rStaff.send();
        }

        private void foreachTree(BsonDocument nodeValue, TreeNode node)
        {

            this.nameTree.Invoke(new Action<TreeView>((a) =>
            {
                var myNode = node.Nodes.Add(nodeValue.GetValue("name").AsString);
                myNode.Tag = nodeValue.GetValue("_id").AsObjectId;
                if (false == nodeValue.GetValue("child").IsBsonNull)
                {
                    var tmp = nodeValue.GetValue("child").AsBsonArray;
                    for (int q = 0; q < tmp.Count; q++)
                    {
                        foreachTree(tmp[q].AsBsonDocument, myNode);
                    }
                }
            }), this.nameTree);
        }

        private void nameTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {

            reFreshGrid();
        }

        private void nameTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            reFreshGrid();
        }

        private void nameTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.nameGrid.ClearSelection();
            for (int q = 0; q < nameGrid.Rows.Count; q++)
            {
                if (string.Equals(e.Node.Tag, nameGrid.Rows[q].Cells[0].Tag))
                {
                    this.nameGrid.Rows[q].Selected = true;
                    this.nameGrid.CurrentCell = this.nameGrid.Rows[q].Cells[0];
                    break;
                }
            }
        }

        private void nameGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DialogResult res;
            switch (e.ColumnIndex)
            {
                case 2:
                    if ((ObjectId)this.nameGrid.Rows[e.RowIndex].Cells[0].Tag != Baccarat_Client_Manager.userMessage.GetValue("_id"))
                    {
                        res = MessageBox.Show("确定要冻结/恢复该用户以及该用户的子用户吗", "Tips", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (res == DialogResult.OK)
                        {
                            bool isfrozen = (this.nameGrid.Rows[e.RowIndex].Cells[2].Value.ToString() == "恢复");
                            var doc = new BsonDocument();
                            var bsonArr = new BsonArray();
                            TreeNode node = null;
                            var list = new List<ObjectId>();
                            this.forEachTree(nameTree.Nodes[0], (inner) =>
                            {
                                if (string.Equals(inner.Text, this.nameGrid.Rows[e.RowIndex].Cells[0].Value.ToString()))
                                {
                                    node = inner;
                                }
                            });
                            this.forEachTree(node, (inner) =>
                            {
                                list.Add((ObjectId)inner.Tag);
                            });
                            for (int q = 0; q < list.Count; q++)
                            {
                                bsonArr.Add(list[q]);
                            }
                            doc.Add("names", bsonArr);
                            doc.Add("isfrozen", (isfrozen ? false : true));
                            //var json = "{\"id\":\"[" + this.nameGrid.Rows[e.RowIndex].Cells[0].Tag.ToString() + "\"],\"isfrozen\":" + (isfrozen ? "false" : "true") + "}";
                            var rStaff = webHelper.postRequest(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "setisfrozen") }, doc.ToBson());
                            rStaff.onResponse += (BsonDocument body) =>
                            {
                                switch (body.GetValue("success").AsInt32)
                                {
                                    case 0:
                                        MessageBox.Show("操作成功.");
                                        reFreshGrid();
                                        //  this.nameGrid.Rows[e.RowIndex].Cells[2].Value = (isfrozen ? "冻结" : "恢复");
                                        //this.nameGrid.Rows[e.RowIndex].Cells[1].Value = (isfrozen ? "正常" : "冻结中");
                                        break;
                                }
                            };
                            rStaff.send();
                        }
                    }
                    break;
                case 3:
                    if ((ObjectId)this.nameGrid.Rows[e.RowIndex].Cells[0].Tag != Baccarat_Client_Manager.userMessage.GetValue("_id"))
                    {
                        res = MessageBox.Show("重要信息:确定要清空该用户吗", "Tips", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (res == DialogResult.OK)
                        {
                            BsonDocument reqDoc = new BsonDocument();
                            reqDoc.Add("mes", new BsonArray() { (ObjectId)this.nameGrid.Rows[e.RowIndex].Cells[0].Tag });
                            reqDoc.Add("active", false);
                            requestStaff req = webHelper.postRequest(
                              new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("flag", "setactive") },
                              reqDoc.ToBson()
                              );
                            req.onResponse += (BsonDocument response) =>
                            {
                                if (response.GetValue("success").AsInt32 == 0)
                                {
                                    MessageBox.Show("操作成功.");
                                    getNameTree();
                                    reFreshGrid();
                                }
                                else
                                {
                                    MessageBox.Show("操作失败，请联系管理人员.");
                                }
                            };
                            req.send();
                        }
                    }
                    break;
                case 4:
                    var mes = new userMessage((ObjectId)this.nameGrid.Rows[e.RowIndex].Cells[0].Tag, this.nameGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    mes.Show();
                    break;

            }
        }
        //private void getAllChilds(ref List<string> ids, TreeNode curDoc)
        //{
        //    ids.Add(curDoc.Text);
        //    if (curDoc.Nodes.Count > 0)
        //    {
        //        for (int q = 0; q < curDoc.Nodes.Count; q++)
        //        {
        //            getAllChilds(ref ids, curDoc.Nodes[q]);
        //        }
        //    }
        //}
        private void forEachTree(TreeNode tree, Action<TreeNode> func)
        {
            func(tree);
            if (tree.Nodes.Count > 0)
            {
                for (int q = 0; q < tree.Nodes.Count; q++)
                {
                    forEachTree(tree.Nodes[q], func);
                }
            }
        }
    }
}
