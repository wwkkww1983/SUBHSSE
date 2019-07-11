using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.SiteConstruction
{
    public partial class MonthPlan : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("MonthPlanEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                } 
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.InitTreeMenu();
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.trMonthPlan.Nodes.Clear();
            var monthPlanLists = BLL.MonthPlanService.GetMonthPlanListByProjectId(this.ProjectId);
            if (monthPlanLists.Count() > 0)
            {
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    monthPlanLists = monthPlanLists.Where(x => x.UnitId == this.CurrUser.UnitId).OrderBy(x => x.Months).ToList();
                }

                var yearsList = monthPlanLists.Select(x => x.CompileDate.Value.Year).Distinct();
                if (yearsList.Count() > 0)
                {
                    foreach (var item in yearsList)
                    {
                        TreeNode rootNode = new TreeNode
                        {
                            Text = item.ToString() + "年",
                            NodeID = item.ToString(),
                            Expanded = true
                        };
                        this.trMonthPlan.Nodes.Add(rootNode);
                        this.BoundTree(rootNode.Nodes, rootNode, monthPlanLists, "0");
                    }
                }
            }
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, TreeNode node, List<Model.SiteConstruction_MonthPlan> monthPlanLists, string type)
        {
            if (type == "0")
            {
                var monthList = monthPlanLists.Where(x => x.Months.Value.Year.ToString() == node.NodeID).OrderByDescending(x => x.Months).Select(x => x.Months.Value.Month).Distinct();
                if (monthList.Count() > 0)
                {
                    foreach (var item in monthList)
                    {
                        TreeNode rootNode = new TreeNode
                        {
                            Text = item.ToString() + "月",
                            NodeID = node.NodeID.ToString() + "-" + item.ToString() + "-01",
                            EnableClickEvent = true
                        };
                        nodes.Add(rootNode);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trMonthPlan_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
        }

        #region 绑定GV 数据
        /// <summary>
        /// 绑定GV 数据
        /// </summary>
        private void BindGrid()
        {
            string dateSelect = string.Empty;
            if (this.trMonthPlan.SelectedNode != null)
            {
                dateSelect = this.trMonthPlan.SelectedNode.NodeID;
            }
            string strSql = @"SELECT C.MonthPlanId,C.ProjectId,C.UnitId,C.CompileMan,C.CompileDate,C.AttachUrl,C.SeeFile,Unit.UnitName,CompileUser.UserName as CompileManName,OperateUser2.UserName as AuditManName,"
                        + @" C.JobContent,(CASE WHEN LEN(C.JobContent) > 50 THEN SUBSTRING(C.JobContent,0,50) ELSE C.JobContent END) AS ShortJobContent "
                        + @" ,(CASE WHEN C.States = " + BLL.Const.State_0 + " OR C.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN C.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                        + @" FROM dbo.SiteConstruction_MonthPlan AS C "
                        + @" LEFT JOIN dbo.Base_Unit AS Unit ON C.UnitId=Unit.UnitId "
                        + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON C.MonthPlanId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                        + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId "
                        + @" LEFT JOIN Sys_User AS CompileUser ON CompileUser.UserId=C.CompileMan "
                        + @" LEFT JOIN Sys_FlowOperate AS FlowOperate2 ON C.MonthPlanId=FlowOperate2.DataId AND FlowOperate2.IsClosed = 1 AND FlowOperate2.State='2'"
                        + @" LEFT JOIN Sys_User AS OperateUser2 ON FlowOperate2.OperaterId=OperateUser2.UserId "
                        + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND C.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }

            strSql += " AND C.Months = @Months";
            listStr.Add(new SqlParameter("@Months", Funs.GetNewDateTime(dateSelect)));
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND C.UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击事件
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
            string id = Grid1.SelectedRowID;
            var monthPlan = BLL.MonthPlanService.GetMonthPlanById(id);
            if (monthPlan != null)
            {
                if (this.btnMenuEdit.Hidden || this.CurrUser.UnitId != monthPlan.UnitId)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthPlanView.aspx?MonthPlanId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthPlanEdit.aspx?MonthPlanId={0}", id, "编辑 - ")));
                }
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var monthPlan = BLL.MonthPlanService.GetMonthPlanById(rowID);
                    if (monthPlan != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, monthPlan.Months.ToString(), monthPlan.MonthPlanId, BLL.Const.ProjectMonthPlanMenuId, BLL.Const.BtnDelete);
                        BLL.MonthPlanService.DeleteMonthPlanById(rowID);
                    }
                }
                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 获取按钮权限
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectMonthPlanMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("月度计划" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 5000;
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
    }
}