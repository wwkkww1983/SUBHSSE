using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class CheckItemSet : PageBase
    {
        #region  定义项
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                this.GetButtonPower();  //获取按钮权限
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();                
                CheckItemSetDataBind();//加载树
            }
        }

        #region 绑定GV 明细
        /// <summary>
        /// 绑定GV 明细
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            string strSql = @"select * from Check_ProjectCheckItemDetail where CheckItemSetId=@CheckItemSetId";

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
                Text = "检查项",
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
            List<Model.Check_ProjectCheckItemSet> checkItemSet = null;
            string checkItemName = string.Empty;
            if (!string.IsNullOrEmpty(this.txtCheckItemSet.Text.Trim()))
            {
                checkItemName = this.txtCheckItemSet.Text.Trim();
            }
            if (parentId == null)
            {
                checkItemSet = (from x in BLL.Funs.DB.Check_ProjectCheckItemSet where x.SupCheckItem == "0" && x.CheckType == this.ckType.SelectedValue && x.ProjectId == this.ProjectId && x.CheckItemName.Contains(checkItemName) orderby x.SortIndex select x).ToList();
            }
            else
            {
                checkItemSet = (from x in BLL.Funs.DB.Check_ProjectCheckItemSet where x.SupCheckItem == parentId && x.CheckType == this.ckType.SelectedValue && x.ProjectId == this.ProjectId orderby x.SortIndex select x).ToList();
            }
            foreach (var q in checkItemSet)
            {
                var isEnd = BLL.Check_ProjectCheckItemSetService.IsEndLevel(q.CheckItemSetId);
                TreeNode newNode = new TreeNode
                {
                    Text = q.CheckItemName,
                    NodeID = q.CheckItemSetId
                };
                if (isEnd)
                {
                    newNode.EnableClickEvent = true;
                }
                nodes.Add(newNode);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].Nodes, nodes[i].NodeID);
            }
        }
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
                var q = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(this.tvCheckItemSet.SelectedNodeID);
                if (q == null)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CheckItemEdit.aspx?supCheckItem=0&checkType={0}", this.ckType.SelectedValue, "新建 - ")));
                }
                else if (q.IsEndLever != true)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CheckItemEdit.aspx?supCheckItem={0}&checkType={1}", q.CheckItemSetId, this.ckType.SelectedValue, "新建 - ")));
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
                var q = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(this.tvCheckItemSet.SelectedNodeID);
                if (q != null)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CheckItemEdit.aspx?supCheckItem={0}&checkItemSetId={1}&checkType={2}", q.SupCheckItem, q.CheckItemSetId, this.ckType.SelectedValue, "编辑 - ")));
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
                if (BLL.Check_ProjectCheckItemSetService.IsDeleteCheckItemSet(checkItemSetId))
                {
                    var getV = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(checkItemSetId);
                    if(getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.MapCode, getV.CheckItemSetId, BLL.Const.ProjectCheckItemSetMenuId, BLL.Const.BtnDelete);
                        BLL.Check_ProjectCheckItemSetService.DeleteCheckItemSet(checkItemSetId, this.ProjectId);
                        
                        CheckItemSetDataBind();
                        Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
                    }
                  
                }
                else
                {
                    Alert.ShowInTop("存在下级菜单或已增加检查项明细，不允许删除！", MessageBoxIcon.Warning);
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
                var checkItem = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(tvCheckItemSet.SelectedNodeID);
                if (checkItem != null && checkItem.IsEndLever == true)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckItemDetailEdit.aspx?checkItemSetId={0}", tvCheckItemSet.SelectedNodeID, "新增 - ")));
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckItemDetailEdit.aspx?checkItemSetId={0}&checkItemDetailId={1}", tvCheckItemSet.SelectedNodeID, Id, "修改 - ")));
        }

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
                    var getV = BLL.Check_ProjectCheckItemDetailService.GetCheckItemDetailById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.SortIndex.ToString(), getV.CheckItemDetailId, BLL.Const.ProjectCheckItemSetMenuId, BLL.Const.BtnDelete);
                       
                        BLL.Check_ProjectCheckItemDetailService.DeleteCheckItemDetail(rowID);
                    }               
                }

                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
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

        protected void ckType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckItemSetDataBind();//加载树
            BindGrid();
        }

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectCheckItemSetMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnMenuNew.Hidden = false;
                    this.btnNewDetail.Hidden = false;
                    this.btnCheckItemExtract.Hidden = false;
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

        #region 抽取公共资源检查项内容
        /// <summary>
        /// 抽取公共资源检查项内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckItemExtract_Click(object sender, EventArgs e)
        {
            string checkType = this.ckType.SelectedValue;
            var q = Funs.DB.Check_ProjectCheckItemSet.FirstOrDefault(x => x.ProjectId == this.ProjectId && x.CheckType == checkType);
            if (q != null)
            {
                Alert.ShowInTop("该项目已存在" + this.ckType.SelectedItem.Text + "检查项内容，无法抽取！");
                return;
            }
            var checkItems = from x in Funs.DB.Technique_CheckItemSet where x.SupCheckItem == "0" && x.CheckType == checkType orderby x.SortIndex select x;
            if (checkItems.Count() == 0)
            {
                Alert.ShowInTop(this.ckType.SelectedItem.Text + "检查项内容为空，请在本页面或到资源库中添加！");
                return;
            }
            this.InsertProjectCheckItemSet("0", checkType);
            this.UpdateNullSup(checkType);   ///修正上级菜单为NULL的数据
            BLL.LogService.AddSys_Log(this.CurrUser, "抽取公共资源检查项内容", null, BLL.Const.ProjectCheckItemSetMenuId, BLL.Const.BtnDownload);
            ShowNotify("抽取公共资源检查项内容成功！", MessageBoxIcon.Success);
            CheckItemSetDataBind();
        }
        #endregion

        #region 提取方法
        /// <summary>
        ///   提取方法
        /// </summary>
        /// <param name="supCheckItem"></param>
        private void InsertProjectCheckItemSet(string supCheckItem, string checkType)
        {
            var checkItems = from x in Funs.DB.Technique_CheckItemSet where x.SupCheckItem == supCheckItem && x.CheckType == checkType orderby x.SortIndex select x;
            if (checkItems.Count() > 0)
            {
                foreach (var item in checkItems)
                {
                    Model.Check_ProjectCheckItemSet checkItemSet = new Model.Check_ProjectCheckItemSet
                    {
                        CheckItemSetId = SQLHelper.GetNewID(typeof(Model.Check_ProjectCheckItemSet)),
                        ProjectId = this.ProjectId,
                        CheckItemName = item.CheckItemName
                    };

                    //获取项目表中对应上级检查项的Id
                    if (item.SupCheckItem != "0")
                    {
                        var cpItemSet = Funs.DB.Check_ProjectCheckItemSet.FirstOrDefault(x => x.OldCheckItemSetId == item.SupCheckItem && x.ProjectId == this.ProjectId);
                        if (cpItemSet != null)
                        {
                            checkItemSet.SupCheckItem = cpItemSet.CheckItemSetId;
                        }
                        else
                        {
                            checkItemSet.SupCheckItem = null;
                        }
                    }
                    else   //根级检查项
                    {
                        checkItemSet.SupCheckItem = "0";
                    }

                    checkItemSet.CheckType = item.CheckType;
                    checkItemSet.MapCode = item.MapCode;
                    checkItemSet.IsEndLever = item.IsEndLever;
                    checkItemSet.SortIndex = item.SortIndex;
                    checkItemSet.IsBuiltIn = item.IsBuiltIn;
                    checkItemSet.OldCheckItemSetId = item.CheckItemSetId;
                    Funs.DB.Check_ProjectCheckItemSet.InsertOnSubmit(checkItemSet);
                    Funs.DB.SubmitChanges();

                    ///如果是末级菜单则查找明细表
                    if (checkItemSet.IsEndLever == true)
                    {
                        var details = from x in Funs.DB.Technique_CheckItemDetail where x.CheckItemSetId == item.CheckItemSetId select x;
                        foreach (var d in details)
                        {
                            Model.Check_ProjectCheckItemDetail detail = new Model.Check_ProjectCheckItemDetail
                            {
                                CheckItemDetailId = SQLHelper.GetNewID(typeof(Model.Check_ProjectCheckItemDetail)),
                                CheckItemSetId = checkItemSet.CheckItemSetId,
                                CheckContent = d.CheckContent,
                                SortIndex = d.SortIndex,
                                IsBuiltIn = d.IsBuiltIn
                            };
                            Funs.DB.Check_ProjectCheckItemDetail.InsertOnSubmit(detail);
                            Funs.DB.SubmitChanges();
                        }
                    }
                    else
                    {
                        InsertProjectCheckItemSet(checkItemSet.OldCheckItemSetId, checkType);
                    }
                }
            }
        }
        #endregion

        #region 提取方法
        /// <summary>
        /// 修正上级菜单为NULL的数据
        /// </summary>
        private void UpdateNullSup(string checkType)
        {
            ///修正上级
            var supIDNULL = from x in Funs.DB.Check_ProjectCheckItemSet
                            where x.ProjectId == this.ProjectId && x.CheckType == checkType && x.SupCheckItem == null
                            select x;
            if (supIDNULL.Count() > 0)
            {
                foreach (var item in supIDNULL)
                {
                    var tChekcItemset = Funs.DB.Technique_CheckItemSet.FirstOrDefault(x => x.CheckItemSetId == item.OldCheckItemSetId);
                    if (tChekcItemset != null)
                    {
                        var tsupChekcItemset = Funs.DB.Check_ProjectCheckItemSet.FirstOrDefault(x => x.OldCheckItemSetId == tChekcItemset.SupCheckItem);
                        if (tsupChekcItemset != null)
                        {
                            item.SupCheckItem = tsupChekcItemset.CheckItemSetId;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("检查项目设置" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion        

        #region 删除
        /// <summary>
        ///   删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string checkType = this.ckType.SelectedValue;
            var checkItems = from x in Funs.DB.Check_ProjectCheckItemSet where x.CheckType == checkType && x.ProjectId == this.ProjectId select x;
            if (checkItems.Count() > 0)
            {
                foreach (var item in checkItems)
                {
                    DeleteCheckItemSet(item.CheckItemSetId);
                }
            }
            this.CheckItemSetDataBind();//加载树
            this.BindGrid();
            BLL.LogService.AddSys_Log(this.CurrUser, "批量删除项目现场检查项信息", null, BLL.Const.ProjectCheckItemSetMenuId, BLL.Const.BtnDelete);
            ShowNotify("删除成功！", MessageBoxIcon.Success);
        }

        /// <summary>
        /// 删除检查项方法
        /// </summary>
        /// <param name="supCheckItem"></param>
        private void DeleteCheckItemSet(string supCheckItem)
        {
            var checkItemSet = from x in Funs.DB.Check_ProjectCheckItemSet where x.SupCheckItem == supCheckItem && x.ProjectId == this.ProjectId select x;
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
                var checkItemSetEnd = Funs.DB.Check_ProjectCheckItemSet.FirstOrDefault(x => x.CheckItemSetId == supCheckItem && x.ProjectId == this.ProjectId);
                if (checkItemSetEnd != null)
                {
                    ///删除详细项
                    BLL.Check_ProjectCheckItemDetailService.DeleteCheckItemDetailByCheckItemSetId(checkItemSetEnd.CheckItemSetId);
                    BLL.Check_ProjectCheckItemSetService.DeleteCheckItemSet(checkItemSetEnd.CheckItemSetId, this.ProjectId);
                    ////删除检查项
                    BLL.Check_ProjectCheckItemSetService.DeleteCheckItemSet(checkItemSetEnd.SupCheckItem, this.ProjectId);
                }
            }
        }
        #endregion

        /// <summary>
        /// 查询检查项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Text_TextChanged(object sender, EventArgs e)
        {
            CheckItemSetDataBind();
        }
    }
}