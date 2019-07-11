using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.ManagementReport
{
    public partial class ReportRemind : PageBase
    {
        #region 定义项
        /// <summary>
        /// 清单主键
        /// </summary>
        public string ReportRemindId
        {
            get
            {
                return (string)ViewState["ReportRemindId"];
            }
            set
            {
                ViewState["ReportRemindId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonth, ConstValue.Group_0009, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, ConstValue.Group_0008, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpQuarter, ConstValue.Group_0011, true);

                List<Model.Base_Project> projects = (from x in Funs.DB.Base_Project
                                                     where x.ProjectState == BLL.Const.ProjectState_1 || x.ProjectState == null
                                                     select x).ToList();
                drpProject.DataValueField = "ProjectId";
                drpProject.DataTextField = "ProjectName";
                var projectlist = projects;
                drpProject.DataSource = projectlist;
                drpProject.DataBind();
                Funs.FineUIPleaseSelect(drpProject);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                LoadData();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 生成数据
        /// </summary>
        private void LoadData()
        {
            DateTime now = DateTime.Now.Date;
            List<Model.Base_Project> projects = (from x in Funs.DB.Base_Project
                                                 where x.ProjectState == BLL.Const.ProjectState_1 || x.ProjectState == null
                                                 select x).ToList();
            foreach (var project in projects)
            {
                if (project.StartDate <= now)  //已开工
                {
                    //处理月报
                    DateTime date = Convert.ToDateTime(now.Year.ToString() + "-" + now.Month.ToString() + "-01").AddDays(-1);  //上月最后一天 
                    if (project.StartDate <= date)  //上月已开工
                    {
                        DateTime month = Convert.ToDateTime(now.Year.ToString() + "-" + now.Month.ToString() + "-01").AddMonths(-1);
                        Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonth(month, project.ProjectId);
                        Model.Manager_MonthReportC monthReportC = BLL.MonthReportCService.GetMonthReportByMonths(month, project.ProjectId);
                        Model.Manager_MonthReportD monthReportD = BLL.MonthReportDService.GetMonthReportByMonths(month, project.ProjectId);
                        if (monthReport == null && monthReportC == null && monthReportD == null && !BLL.MonthReportService.GetMonthReportByDate(month, project.ProjectId))
                        {
                            Model.ManagementReport_ReportRemind oldReportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                                                                                   where x.ProjectId == project.ProjectId && x.Months == month && x.ReportName == "管理月报"
                                                                                   select x).FirstOrDefault();
                            if (oldReportRemind == null)
                            {
                                Model.ManagementReport_ReportRemind reportRemind = new Model.ManagementReport_ReportRemind
                                {
                                    ProjectId = project.ProjectId,
                                    Months = month,
                                    ReportName = "管理月报",
                                    CompileDate = now
                                };
                                BLL.ReportRemindService.AddReportRemind(reportRemind);
                            }
                        }
                        Model.InformationProject_MillionsMonthlyReport millionsMonthlyReport = Funs.DB.InformationProject_MillionsMonthlyReport.FirstOrDefault(x => x.ProjectId == project.ProjectId
                            && x.Year == month.Year && x.Month == month.Month && x.States == BLL.Const.State_2);
                        if (millionsMonthlyReport == null)
                        {
                            Model.ManagementReport_ReportRemind oldReportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                                                                                   where x.ProjectId == project.ProjectId && x.Year == month.Year && x.Month == month.Month && x.ReportName == "百万工时安全统计月报"
                                                                                   select x).FirstOrDefault();
                            if (oldReportRemind == null)
                            {
                                Model.ManagementReport_ReportRemind reportRemind = new Model.ManagementReport_ReportRemind
                                {
                                    ProjectId = project.ProjectId,
                                    Year = month.Year,
                                    Month = month.Month,
                                    ReportName = "百万工时安全统计月报",
                                    CompileDate = now
                                };
                                BLL.ReportRemindService.AddReportRemind(reportRemind);
                            }
                        }
                        Model.InformationProject_AccidentCauseReport accidentCauseReport = Funs.DB.InformationProject_AccidentCauseReport.FirstOrDefault(x => x.ProjectId == project.ProjectId && x.Year == month.Year && x.Month == month.Month);
                        if (accidentCauseReport == null)
                        {
                            Model.ManagementReport_ReportRemind oldReportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                                                                                   where x.ProjectId == project.ProjectId && x.Year == month.Year && x.Month == month.Month && x.ReportName == "职工伤亡事故原因分析报"
                                                                                   select x).FirstOrDefault();
                            if (oldReportRemind == null)
                            {
                                Model.ManagementReport_ReportRemind reportRemind = new Model.ManagementReport_ReportRemind
                                {
                                    ProjectId = project.ProjectId,
                                    Year = month.Year,
                                    Month = month.Month,
                                    ReportName = "职工伤亡事故原因分析报",
                                    CompileDate = now
                                };
                                BLL.ReportRemindService.AddReportRemind(reportRemind);
                            }
                        }
                        if (project.IsForeign == true)   //海外项目
                        {
                            Model.Manager_MonthReportE monthReportE = Funs.DB.Manager_MonthReportE.FirstOrDefault(x => x.ProjectId == project.ProjectId && x.Months.Value.Year == month.Year && x.Months.Value.Month == month.Month);
                            if (monthReportE == null)
                            {
                                Model.ManagementReport_ReportRemind oldReportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                                                                                       where x.ProjectId == project.ProjectId && x.Year == month.Year && x.Month == month.Month && x.ReportName == "海外工程项目月度HSSE统计表"
                                                                                       select x).FirstOrDefault();
                                if (oldReportRemind == null)
                                {
                                    Model.ManagementReport_ReportRemind reportRemind = new Model.ManagementReport_ReportRemind
                                    {
                                        ProjectId = project.ProjectId,
                                        Months = month,
                                        Year = month.Year,
                                        Month = month.Month,
                                        ReportName = "海外工程项目月度HSSE统计表",
                                        CompileDate = now
                                    };
                                    BLL.ReportRemindService.AddReportRemind(reportRemind);
                                }
                            }
                        }
                        //处理季报
                        DateTime showDate = DateTime.Now.AddMonths(-3);
                        int year = showDate.Year;
                        int quarter = Funs.GetNowQuarterlyByTime(showDate);
                        Model.InformationProject_SafetyQuarterlyReport safetyQuarterlyReport = Funs.DB.InformationProject_SafetyQuarterlyReport.FirstOrDefault(x => x.ProjectId == project.ProjectId && x.YearId == year && x.Quarters == quarter);
                        if (safetyQuarterlyReport == null)
                        {
                            //删除未上报月报信息
                            Model.ManagementReport_ReportRemind oldReportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                                                                                   where x.ProjectId == project.ProjectId && x.Year == year && x.Quarterly == quarter && x.ReportName == "安全生产数据季报"
                                                                                   select x).FirstOrDefault();
                            if (oldReportRemind == null)
                            {
                                Model.ManagementReport_ReportRemind reportRemind = new Model.ManagementReport_ReportRemind
                                {
                                    ProjectId = project.ProjectId,
                                    Year = year,
                                    Quarterly = quarter,
                                    ReportName = "安全生产数据季报",
                                    CompileDate = now
                                };
                                BLL.ReportRemindService.AddReportRemind(reportRemind);
                            }
                        }
                        Model.InformationProject_DrillConductedQuarterlyReport drillConductedQuarterlyReport = Funs.DB.InformationProject_DrillConductedQuarterlyReport.FirstOrDefault(x => x.ProjectId == project.ProjectId && x.YearId == year && x.Quarter == quarter);
                        if (drillConductedQuarterlyReport == null)
                        {
                            //删除未上报月报信息
                            Model.ManagementReport_ReportRemind oldReportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                                                                                   where x.ProjectId == project.ProjectId && x.Year == year && x.Quarterly == quarter && x.ReportName == "应急演练开展情况季报"
                                                                                   select x).FirstOrDefault();
                            if (oldReportRemind == null)
                            {
                                Model.ManagementReport_ReportRemind reportRemind = new Model.ManagementReport_ReportRemind
                                {
                                    ProjectId = project.ProjectId,
                                    Year = year,
                                    Quarterly = quarter,
                                    ReportName = "应急演练开展情况季报",
                                    CompileDate = now
                                };
                                BLL.ReportRemindService.AddReportRemind(reportRemind);
                            }
                        }
                        //处理半年报
                        int halfYear = Funs.GetNowHalfYearByTime(now);
                        Model.InformationProject_DrillPlanHalfYearReport drillPlanHalfYearReport = Funs.DB.InformationProject_DrillPlanHalfYearReport.FirstOrDefault(x => x.ProjectId == project.ProjectId && x.YearId == year && x.HalfYearId == halfYear);
                        if (drillPlanHalfYearReport == null)
                        {
                            //删除未上报月报信息
                            Model.ManagementReport_ReportRemind oldReportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                                                                                   where x.ProjectId == project.ProjectId && x.Year == year && x.HalfYear == halfYear && x.ReportName == "应急演练工作计划半年报"
                                                                                   select x).FirstOrDefault();
                            if (oldReportRemind == null)
                            {
                                Model.ManagementReport_ReportRemind reportRemind = new Model.ManagementReport_ReportRemind
                                {
                                    ProjectId = project.ProjectId,
                                    Year = year,
                                    HalfYear = halfYear,
                                    ReportName = "应急演练工作计划半年报",
                                    CompileDate = now
                                };
                                BLL.ReportRemindService.AddReportRemind(reportRemind);
                            }
                        }
                    }
                }
            }
        }

        #region 转换字符串
        /// <summary>
        /// 转换报表名称
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertReportName(object ReportRemindId)
        {
            string reportName = string.Empty;
            if (ReportRemindId != null)
            {
                string quarterly = string.Empty;
                string halfYear = string.Empty;
                Model.ManagementReport_ReportRemind reportRemind = BLL.ReportRemindService.GetReportRemindByReportRemindId(ReportRemindId.ToString());
                if (reportRemind != null)
                {
                    if (reportRemind.Quarterly == 1)
                    {
                        quarterly = "一季度";
                    }
                    else if (reportRemind.Quarterly == 2)
                    {
                        quarterly = "二季度";
                    }
                    else if (reportRemind.Quarterly == 3)
                    {
                        quarterly = "三季度";
                    }
                    else if (reportRemind.Quarterly == 4)
                    {
                        quarterly = "四季度";
                    }
                    if (reportRemind.HalfYear == 1)
                    {
                        halfYear = "上半年";
                    }
                    else if (reportRemind.HalfYear == 2)
                    {
                        halfYear = "下半年";
                    }
                    if (reportRemind.ReportName == "管理月报")
                    {
                        reportName = reportRemind.Months.Value.Year + "年" + reportRemind.Months.Value.Month + "月管理月报未上报";
                    }
                    else if (reportRemind.ReportName == "百万工时安全统计月报")
                    {
                        reportName = reportRemind.Year.ToString() + "年" + reportRemind.Month.ToString() + "月百万工时安全统计月报未上报";
                    }
                    else if (reportRemind.ReportName == "职工伤亡事故原因分析报")
                    {
                        reportName = reportRemind.Year.ToString() + "年" + reportRemind.Month.ToString() + "月职工伤亡事故原因分析报未上报";
                    }
                    else if (reportRemind.ReportName == "安全生产数据季报")
                    {
                        reportName = reportRemind.Year.ToString() + "年" + quarterly + "安全生产数据季报未上报";
                    }
                    else if (reportRemind.ReportName == "应急演练开展情况季报")
                    {
                        reportName = reportRemind.Year.ToString() + "年" + quarterly + "应急演练开展情况季报未上报";
                    }
                    else if (reportRemind.ReportName == "应急演练工作计划半年报")
                    {
                        reportName = reportRemind.Year.ToString() + "年" + halfYear + "应急演练工作计划半年报未上报";
                    }
                    else if (reportRemind.ReportName == "海外工程项目月度HSSE统计表")
                    {
                        reportName = reportRemind.Year.ToString() + "年" + reportRemind.Month.ToString() + "月海外工程项目月度HSSE统计表";
                    }
                }
            }
            return reportName;
        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select ReportRemind.ReportRemindId,ReportRemind.ProjectId,ReportRemind.CompileDate,Project.ProjectCode,Project.ProjectName"
                          + @" from ManagementReport_ReportRemind AS ReportRemind "
                          + @" LEFT JOIN Base_Project AS Project ON ReportRemind.ProjectId=Project.ProjectId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.drpProject.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND ReportRemind.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.drpProject.SelectedValue));
            }
            if (this.drpYear.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND (ReportRemind.Year = @Year OR (ReportRemind.Months IS NOT NULL AND YEAR(ReportRemind.Months)=@YearM))";
                listStr.Add(new SqlParameter("@Year", this.drpYear.SelectedValue));
                listStr.Add(new SqlParameter("@YearM", this.drpYear.SelectedValue));
            }
            if (this.drpMonth.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND (ReportRemind.Month = @Month OR (ReportRemind.Months IS NOT NULL AND Month(ReportRemind.Months)=@MonthM))";
                listStr.Add(new SqlParameter("@Month", this.drpMonth.SelectedValue));
                listStr.Add(new SqlParameter("@MonthM", this.drpMonth.SelectedValue));
            }
            if (this.drpQuarter.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND ReportRemind.Quarterly = @Quarterly";
                listStr.Add(new SqlParameter("@Quarterly", this.drpQuarter.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtReportName.Text.Trim()))
            {
                strSql += " AND ReportRemind.ReportName LIKE @ReportName";
                listStr.Add(new SqlParameter("@ReportName", "%" + this.txtReportName.Text.Trim() + "%"));
            }
            strSql += " order by ReportRemind.CompileDate desc";
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
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

        #region 表头过滤
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
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

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        Model.ManagementReport_ReportRemind reportRemind = BLL.ReportRemindService.GetReportRemindByReportRemindId(rowID);
                        if (reportRemind != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, null, reportRemind.ReportRemindId, BLL.Const.ServerReportRemindMenuId, BLL.Const.BtnDelete);
                            BLL.ReportRemindService.DeleteReportRemindByReportRemind(reportRemind);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
        {
            string content = string.Empty;
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerReportRemindMenuId);
            if (buttonList.Count() > 0)
            {
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("报表上报情况" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
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
                    if (column.ColumnID == "lblReportName")
                    {
                        html = (row.FindControl("lblReportName") as AspNet.Label).Text;
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