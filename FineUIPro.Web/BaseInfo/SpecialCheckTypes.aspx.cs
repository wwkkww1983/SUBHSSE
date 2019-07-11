using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Linq;

namespace FineUIPro.Web.BaseInfo
{
    public partial class SpecialCheckTypes : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetButtonPower();  //获取按钮权限
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                CheckItemSetDataBind();//加载树
            }
        }

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void CheckItemSetDataBind()
        {
            this.tvCheckItemSet.Nodes.Clear();
            this.tvCheckItemSet.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode
            {
                Text = "专项检查项",
                Expanded = true,
                NodeID = "0"
            };//定义根节点

            this.tvCheckItemSet.Nodes.Add(rootNode);
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
            List<Model.HSSE_Check_CheckItemSet> checkItemSet = null;
            if (parentId == null)
            {
                checkItemSet = (from x in BLL.Funs.DB.HSSE_Check_CheckItemSet where x.SupCheckItem == "0" orderby x.SortIndex select x).ToList();
            }
            else
            {
                checkItemSet = (from x in BLL.Funs.DB.HSSE_Check_CheckItemSet where x.SupCheckItem == parentId orderby x.SortIndex select x).ToList();
            }

            foreach (var q in checkItemSet)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = q.CheckItemName,
                    NodeID = q.CheckItemSetId
                };
                //if (q.IsEndLever == true)
                //{
                    newNode.EnableClickEvent = true;
                //}
                nodes.Add(newNode);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].Nodes, nodes[i].NodeID);
            }
        }
        #endregion
        #endregion
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvCheckItemSet_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            if (this.tvCheckItemSet.SelectedNodeID != "0" && this.tvCheckItemSet.SelectedNode != null)
            {
                this.Grid1.DataSource = null;
                this.Grid1.DataBind();
                BindGrid();
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            string strSql = @"select * from HSSE_Check_CheckItemDetail where CheckItemSetId=@CheckItemSetId";

            SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@CheckItemSetId",this.tvCheckItemSet.SelectedNodeID)
                    };

            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 右键增加、修改、删除检查项方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            if (this.tvCheckItemSet.SelectedNode != null)
            {
                var q = BLL.CheckItemSetService.GetCheckItemSetById(this.tvCheckItemSet.SelectedNodeID);
                if (q == null)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SpecialCheckTypesEdit.aspx?supCheckItem=0", "新建 - ")));
                }
                else if (q.IsEndLever != true)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SpecialCheckTypesEdit.aspx?supCheckItem={0}", q.CheckItemSetId, "新建 - ")));
                }
                else
                {
                    Alert.ShowInTop("选择的项已是末级！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 右键修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (this.tvCheckItemSet.SelectedNode != null && this.tvCheckItemSet.SelectedNodeID != "0")
            {
                this.hdSelectId.Text = this.tvCheckItemSet.SelectedNode.NodeID;
                var q = BLL.CheckItemSetService.GetCheckItemSetById(this.tvCheckItemSet.SelectedNodeID);
                if (q != null)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SpecialCheckTypesEdit.aspx?supCheckItem={0}&checkItemSetId={1}", q.SupCheckItem, q.CheckItemSetId, "编辑 - ")));
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (this.tvCheckItemSet.SelectedNode != null)
            {
                string checkItemSetId = this.tvCheckItemSet.SelectedNodeID;
                var q = BLL.CheckItemSetService.GetCheckItemSetById(checkItemSetId);
                if (q != null && BLL.CheckItemSetService.IsDeleteCheckItemSet(checkItemSetId))
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, q.MapCode, q.CheckItemSetId, BLL.Const.SpecialCheckTypesMenuId, BLL.Const.BtnDelete);
                    BLL.CheckItemSetService.DeleteCheckItemSetById(checkItemSetId);

                    CheckItemSetDataBind();
                    Alert.ShowInTop("删除成功！");
                }
                else
                {
                    Alert.ShowInTop("存在下级菜单或已增加检查项明细，不允许删除！");
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 增加、修改、删除检查项明细事件
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (tvCheckItemSet.SelectedNode != null)
            {
                var checkItem = BLL.CheckItemSetService.GetCheckItemSetById(tvCheckItemSet.SelectedNodeID);
                if (checkItem != null && checkItem.IsEndLever == true)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SpecialCheckTypesDetailEdit.aspx?checkItemSetId={0}", tvCheckItemSet.SelectedNodeID, "新增 - ")));
                }
                else
                {
                    Alert.ShowInTop("请选择最后一级节点添加！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                Alert.ShowInTop("请选择检查项！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModifyDetail_Click(object sender, EventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SpecialCheckTypesDetailEdit.aspx?checkItemSetId={0}&checkItemDetailId={1}", tvCheckItemSet.SelectedNodeID, Id, "修改 - ")));
        }

        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// 删除明细方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelDetail_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getV = BLL.CheckItemDetailService.GetCheckItemDetailById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.SortIndex.ToString(), getV.CheckItemSetId, BLL.Const.SpecialCheckTypesMenuId, Const.BtnDelete);
                        BLL.CheckItemDetailService.DeleteCheckItemDetailById(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            CheckItemSetDataBind();
        }
        #endregion

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpecialCheckTypesMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnMenuNew.Hidden = false;
                    this.btnNewDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                    this.btnMenuModifyDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                    this.btnMenuDelDetail.Hidden = false;
                    this.btnDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var checkItems = from x in Funs.DB.HSSE_Check_CheckItemSet select x;
            if (checkItems.Count() > 0)
            {
                foreach (var item in checkItems)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, item.MapCode, item.CheckItemSetId, BLL.Const.SpecialCheckTypesMenuId, Const.BtnDelete);
                    DeleteCheckItemSet(item.CheckItemSetId);
                }
            }
            this.CheckItemSetDataBind();//加载树
            this.BindGrid();
        }

        /// <summary>
        /// 删除检查项方法
        /// </summary>
        /// <param name="supCheckItem"></param>
        private void DeleteCheckItemSet(string supCheckItem)
        {
            var checkItemSet = from x in Funs.DB.HSSE_Check_CheckItemSet where x.SupCheckItem == supCheckItem select x;
            if (checkItemSet.Count() > 0)
            {
                foreach (var item in checkItemSet)
                {
                    DeleteCheckItemSet(item.CheckItemSetId);
                }

                this.CheckItemSetDataBind();//加载树
                this.BindGrid();
            }
            else
            {
                var checkItemSetEnd = Funs.DB.HSSE_Check_CheckItemSet.FirstOrDefault(x => x.CheckItemSetId == supCheckItem);
                if (checkItemSetEnd != null)
                {
                    ///删除详细项
                    BLL.CheckItemDetailService.DeleteCheckItemDetailByCheckItemSetId(checkItemSetEnd.CheckItemSetId);
                    ////删除检查项
                    BLL.CheckItemSetService.DeleteCheckItemSetById(checkItemSetEnd.CheckItemSetId);
                    ////删除检查项
                    BLL.CheckItemSetService.DeleteCheckItemSetById(checkItemSetEnd.SupCheckItem);
                }
            }
        }
        #endregion       
    }
}