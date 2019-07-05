using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.ManagementReport
{
    public partial class ServerSafeReport : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            if (!IsPostBack && this.CurrUser != null)
            {
                this.GetButtonPower();  ////得到权限
                this.InitTreeMenu(); ////初始化树节点
            }
        }
        #endregion

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trSafeReport.Nodes.Clear();            
            trSafeReport.EnableIcons = true;
            trSafeReport.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "项目安全文件",
                NodeID = "0",
                Expanded = true
            };
            this.trSafeReport.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        /// <summary>
        /// 绑定树 数据
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt =BLL.SafeReportService.GetSafeReportBySupItem(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.SafeReportName,
                        NodeID = dr.SafeReportId,
                        ToolTip = "[" + dr.SafeReportCode + "]" + dr.SafeReportName,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);
                    if (dr.States == BLL.Const.State_0)
                    {
                        tn.Text = "<font color='#54FF9F'>" + tn.Text + "</font>";                        
                    }
                    if (!BLL.SafeReportService.IsUpLoadSafeReport(dr.SafeReportId))
                    {
                        tn.Text = "<font color='#FF7575'>" + tn.Text + "</font>";
                        this.SetNodeColor(tn);
                    }
                    BoundTree(tn.Nodes, dr.SafeReportId);
                }
            }
        }

        /// <summary>
        ///  设置父级节点颜色
        /// </summary>
        private void SetNodeColor(TreeNode tn)
        {
            if (tn.NodeID != "0")
            {
                tn.Text = "<font color='#FF7575'>" + tn.Text + "</font>";
                this.SetNodeColor(tn.ParentNode);
            }
        }

        #endregion

        #region 增加
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.trSafeReport.SelectedNode != null)
            {
                Model.Manager_SafeReport SafeReport = BLL.SafeReportService.GetSafeReportBySafeReportId(this.trSafeReport.SelectedNode.NodeID);
                if ((SafeReport != null && SafeReport.IsEndLever == false) || this.trSafeReport.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ServerSafeReportSave.aspx?SupSafeReportId={0}", this.trSafeReport.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("选择的项已是末级！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.trSafeReport.SelectedNode != null)
            {
                if (this.trSafeReport.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ServerSafeReportSave.aspx?SafeReportId={0}", this.trSafeReport.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("根节点无法编辑！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
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
            if (this.trSafeReport.SelectedNode != null)
            {
                var q = BLL.SafeReportService.GetSafeReportBySafeReportId(this.trSafeReport.SelectedNode.NodeID);
                if (q != null && BLL.SafeReportService.IsDeleteSafeReport(this.trSafeReport.SelectedNode.NodeID))
                {
                    BLL.SafeReportService.DeleteSafeReportBySafeReportId(this.trSafeReport.SelectedNode.NodeID);
                    this.InitTreeMenu();
                }
                else
                {
                    ShowNotify("存在下级菜单或已增加资源数据，不允许删除！",MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择删除项！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Tree选择事件
        /// <summary>
        /// Tree选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trSafeReport_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region BindGrid
        /// <summary>
        /// 绑定明细
        /// </summary>
        private void BindGrid()
        {
            if (this.trSafeReport.SelectedNode != null)
            {
                string strSql = @"SELECT item.SafeReportItemId,item.SafeReportId,item.ProjectId,p.ProjectName,item.ReportContent,item.ReportManId,item.ReportTime,s.RequestTime,s.SafeReportName,item.UpReportTime,item.States"
                            + @" ,(CASE WHEN item.States = 2 THEN '已上报' ELSE '未上报' END) AS StatesName"
                            + @" FROM Manager_SafeReportItem AS item"
                            + @" LEFT JOIN Manager_SafeReport AS S ON item.SafeReportId = S.SafeReportId"
                            + @" LEFT JOIN Base_Project AS p ON item.ProjectId = p.ProjectId"
                            + @" WHERE item.SafeReportId=@SafeReportId ";              
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@SafeReportId", this.trSafeReport.SelectedNode.NodeID));
                if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
                {
                    strSql += " AND (p.ProjectName LIKE @name OR s.SafeReportName LIKE @name )";
                    listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();

                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    var SafeReport = BLL.SafeReportItemService.GetSafeReportItemBySafeReportItemId(Grid1.Rows[i].DataKeys[0].ToString());
                    if (SafeReport != null)
                    {
                        if (SafeReport.States == BLL.Const.State_2)
                        {
                            Grid1.Rows[i].RowCssClass = "blue";
                        }
                        else
                        {
                            Grid1.Rows[i].RowCssClass = "red";
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 弹出框关闭事件 
        /// <summary>
        /// 树节点弹出框关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            InitTreeMenu();
        }

        /// <summary>
        /// 明细弹出框关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 明细表页码及排序
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 明细编辑事件
        /// <summary>
        /// 双击编辑明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            string SafeReportItemId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SafeReportItemSave.aspx?SafeReportItemId={0}", SafeReportItemId, "编辑 - ")));
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerSafeReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;                    
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;                    
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}