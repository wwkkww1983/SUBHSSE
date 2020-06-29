using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerMonthC : PageBase
    {
        /// <summary>
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

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            //FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                this.txtReportDate.Text = string.Format("{0:yyyy-MM}",DateTime.Now);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT MonthReport.MonthReportId,MonthReport.ProjectId,MonthReport.Months,CodeRecords.Code AS MonthReportCode,Users.UserName as ReportManName"
                          + @" FROM Manager_MonthReportC AS MonthReport "
                          + @" LEFT JOIN Sys_User AS Users ON MonthReport.ReportMan=Users.UserId "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON MonthReport.MonthReportId=CodeRecords.DataId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND MonthReport.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (!string.IsNullOrEmpty(this.txtMonthReportCode.Text.Trim()))
            {
                strSql += " AND CodeRecords.Code LIKE @MonthReportCode";
                listStr.Add(new SqlParameter("@MonthReportCode", "%" + this.txtMonthReportCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();

        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 过滤表头、排序、分页、关闭窗口
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

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

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(null, null);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string MonthReportId = Grid1.SelectedRowID;
            var monthReport = BLL.MonthReportCService.GetMonthReportByMonthReportId(MonthReportId);
            int n = 6;  //月报冻结时间
            var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
            if (sysSet != null)
            {
                n = Convert.ToInt32(sysSet.ConstValue);
            }
            if (monthReport != null)
            {
                int d = Convert.ToInt32(DateTime.Now.Day);
                if ((monthReport.Months.Value.Year == DateTime.Now.Year && monthReport.Months.Value.Month == DateTime.Now.Month) ||
                    ((monthReport.Months.Value.AddMonths(1).Month == DateTime.Now.Month) && d < n + 1))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportCEdit.aspx?MonthReportId={0}", MonthReportId, "编辑 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportCView.aspx?MonthReportId={0}", MonthReportId, "查看 - ")));
                }
            }
        }
        #endregion

        #region 导出
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string MonthReportId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportCOut.aspx?MonthReportId={0}", MonthReportId, "编辑 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var mont = BLL.MonthReportCService.GetMonthReportByMonthReportId(rowID);
                    if (mont != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, mont.MonthReportCode, mont.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnDelete);
                        BLL.PersonSortCService.DeletePersonSortsByMonthReportId(rowID);
                        BLL.HazardSortCService.DeleteHazardSortsByMonthReportId(rowID);
                        BLL.AccidentSortCService.DeleteAccidentSortsByMonthReportId(rowID);
                        BLL.HseCostCService.DeleteHseCostsByMonthReportId(rowID);
                        BLL.TrainSortCService.DeleteTrainSortsByMonthReportId(rowID);
                        BLL.TrainActivitySortCService.DeleteTrainActivitySortsByMonthReportId(rowID);
                        BLL.MeetingSortCService.DeleteMeetingSortsByMonthReportId(rowID);
                        BLL.CheckSortCService.DeleteCheckSortsByMonthReportId(rowID);
                        BLL.CheckDetailSortCService.DeleteCheckDetailSortsByMonthReportId(rowID);
                        BLL.PromotionalActiviteSortCService.DeletePromotionalActiviteSortsByMonthReportId(rowID);
                        BLL.EmergencySortCService.DeleteEmergencySortsByMonthReportId(rowID);
                        BLL.DrillSortCService.DeleteDrillSortsByMonthReportId(rowID);
                        BLL.IncentiveSortCService.DeleteIncentiveSortsByMonthReportId(rowID);
                        BLL.OtherActiveSortCService.DeleteOtherActiveSortsByMonthReportId(rowID);
                        BLL.ActivityDesCService.DeleteActivityDesByMonthReportId(rowID);
                        BLL.OtherManagementCService.DeleteOtherManagementByMonthReportId(rowID);
                        BLL.PlanCService.DeletePlanByMonthReportId(rowID);
                        BLL.ReviewRecordCService.DeleteReviewRecordByMonthReportId(rowID);
                        BLL.FileManageCService.DeleteFileManageByMonthReportId(rowID);
                        BLL.FiveExpenseCService.DeleteFiveExpenseByMonthReportId(rowID);
                        BLL.SubExpenseCService.DeleteSubExpenseByMonthReportId(rowID);
                        BLL.AccidentDesciptionItemCService.DeleteAccidentDesciptionItemByMonthReportId(rowID);
                        BLL.AccidentDesciptionCService.DeleteAccidentDesciptionByMonthReportId(rowID);
                        BLL.OtherWorkCService.DeleteOtherWorkByMonthReportId(rowID);
                        BLL.HazardCService.DeleteHazardByMonthReportId(rowID);
                        BLL.TrainCService.DeleteTrainByMonthReportId(rowID);
                        BLL.CheckCService.DeleteCheckByMonthReportId(rowID);
                        BLL.MeetingCService.DeleteMeetingByMonthReportId(rowID);
                        BLL.ActivitiesCService.DeleteActivitiesByMonthReportId(rowID);
                        BLL.EmergencyPlanCService.DeleteEmergencyPlanByMonthReportId(rowID);
                        BLL.EmergencyExercisesCService.DeleteEmergencyExercisesByMonthReportId(rowID);
                        BLL.CostInvestmentPlanCService.DeleteCostInvestmentPlanByMonthReportId(rowID);
                        BLL.ManageDocPlanCService.DeleteManageDocPlanByMonthReportId(rowID);
                        BLL.OtherWorkPlanCService.DeleteOtherWorkPlanByMonthReportId(rowID);
                        BLL.MonthReportCService.DeleteMonthReportByMonthReportId(rowID);
                    }                   
                }

                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）");
            }
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
            if (project.StartDate != null)
            {
                if (!string.IsNullOrEmpty(this.txtReportDate.Text.Trim()))
                {
                    DateTime months = Convert.ToDateTime(this.txtReportDate.Text.Trim() + "-01");
                    Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonths(months, this.CurrUser.LoginProjectId);
                    if (monthReport != null)
                    {
                        Alert.ShowInTop("当前月份的月报已存在！", MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportCEdit.aspx?months={0}", string.Format("{0:yyyy-MM-dd}", months), "编辑 - ")));
                    }
                }
                else
                {
                    Alert.ShowInTop("请选择月份！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("请先设置项目开工时间！", MessageBoxIcon.Warning);
                return;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectManagerMonthCMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管理月报B" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “ManagerMonthC.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ManagerMonthC.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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