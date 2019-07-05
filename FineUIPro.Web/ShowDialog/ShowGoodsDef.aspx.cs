using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ShowDialog
{
    public partial class ShowGoodsDef : PageBase
    {
        /// <summary>
        /// 物资类别
        /// </summary>
        public string GoodsType
        {
            get
            {
                return (string)ViewState["GoodsType"];
            }
            set
            {
                ViewState["GoodsType"] = value;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GoodsSetDataBind();
            }
        }

        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void GoodsSetDataBind()
        {
            this.tvGoodsDef.Nodes.Clear();

            TreeNode rootNode = new TreeNode
            {
                Text = "物资信息",
                NodeID = "0"
            };//定义根节点

            this.tvGoodsDef.Nodes.Add(rootNode);
            this.GetNodes(rootNode.Nodes, null);
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            //TreeNode newNode1 = new TreeNode();
            //newNode1.Text = "药品";
            //newNode1.NodeID = "药品";
            //newNode1.EnableClickEvent = true;
            //nodes.Add(newNode1);
            //TreeNode newNode2 = new TreeNode();
            //newNode2.Text = "仪器";
            //newNode2.NodeID = "仪器";
            //newNode2.EnableClickEvent = true;
            //nodes.Add(newNode2);
            //TreeNode newNode3 = new TreeNode();
            //newNode3.Text = "形象标识";
            //newNode3.NodeID = "形象标识";
            //newNode3.EnableClickEvent = true;
            //nodes.Add(newNode3);
            //TreeNode newNode4 = new TreeNode();
            //newNode4.Text = "电子设备";
            //newNode4.NodeID = "电子设备";
            //newNode4.EnableClickEvent = true;
            //nodes.Add(newNode4);
            //TreeNode newNode5 = new TreeNode();
            //newNode5.Text = "劳保用品";
            //newNode5.NodeID = "劳保用品";
            //newNode5.EnableClickEvent = true;
            //nodes.Add(newNode5);
            //TreeNode newNode6 = new TreeNode();
            //newNode6.Text = "其他";
            //newNode6.NodeID = "其他";
            //newNode6.EnableClickEvent = true;
            //nodes.Add(newNode6);
            var goodsCategorys = GoodsCategoryService.GetGoodsCategoryList();
            foreach (var item in goodsCategorys)
            {
                TreeNode newNode = new TreeNode
                {
                    NodeID = item.GoodsCategoryId,
                    Text = item.GoodsCategoryName,
                    EnableClickEvent = true
                };
                nodes.Add(newNode);
            }
        }
        #endregion

        /// <summary>
        /// 选择节点变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvGoodsDef_NodeCommand(object sender, EventArgs e)
        {
            GoodsType = this.tvGoodsDef.SelectedNode.NodeID;
            this.Grid1.DataSource = BLL.GoodsDefService.GetGoodsDefListByGoodsType(GoodsType);
            this.Grid1.DataBind();
        }

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbGoodsDefId_CheckedChanged(object sender, EventArgs e)
        {
            bool result = true;
            System.Web.UI.WebControls.CheckBox ckbNewGoods = sender as System.Web.UI.WebControls.CheckBox;
            int rowsCount = this.Grid1.Rows.Count;
            int an = 0;
            for (int i = 0; i < rowsCount; i++)
            {
                System.Web.UI.WebControls.CheckBox ckbGoods = (System.Web.UI.WebControls.CheckBox)(this.Grid1.Rows[i].FindControl("ckbGoodsDefId"));
                if (ckbNewGoods.ClientID != ckbGoods.ClientID)
                {
                    if (ckbGoods.Checked == true)
                    {
                        result = false;
                    }
                }
                else
                {
                    an = i;
                }
            }
            if (result == false)
            {
                ((System.Web.UI.WebControls.CheckBox)(this.Grid1.Rows[an].FindControl("ckbGoodsDefId"))).Checked = false;
                Alert.ShowInTop("只能选择一条信息!", MessageBoxIcon.Warning);
                return;
            }
        }



        /// <summary>
        /// 根据物资主键获取该物资当前库存
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        protected string GetNum(object goodsDefId)
        {
            if (goodsDefId != null)
            {
                return BLL.GoodsDefService.GetGoodsNumByGoodsDefId(goodsDefId.ToString(), this.CurrUser.LoginProjectId).ToString();
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            int rowsCount = this.Grid1.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                System.Web.UI.WebControls.CheckBox ckbGoods = (System.Web.UI.WebControls.CheckBox)(this.Grid1.Rows[i].FindControl("ckbGoodsDefId")) as System.Web.UI.WebControls.CheckBox;
                if (ckbGoods.Checked)
                {
                    System.Web.UI.WebControls.Label lblGoodsId = (System.Web.UI.WebControls.Label)(this.Grid1.Rows[i].FindControl("lblGoodsDefId")) as System.Web.UI.WebControls.Label;
                    if (lblGoodsId != null)
                    {
                        Model.Base_GoodsDef goodsDef = BLL.GoodsDefService.GetGoodsDefById(lblGoodsId.Text.Trim());
                        if (goodsDef != null)
                        {
                            result = goodsDef.GoodsDefName + "|" + goodsDef.GoodsDefId;
                        }
                    }
                }
            }
            Session["goodsDefs"] = result;
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 获取物资类别
        /// </summary>
        /// <param name="goodsDefCode"></param>
        /// <returns></returns>
        protected string ConvertGoodsDefCode(object goodsDefCode)
        {
            if (goodsDefCode != null)
            {
                var goodsCategory = BLL.GoodsCategoryService.GetGoodsCategoryById(goodsDefCode.ToString());
                if (goodsCategory != null)
                {
                    return goodsCategory.GoodsCategoryName;
                }
            }
            return null;
        }
    }
}