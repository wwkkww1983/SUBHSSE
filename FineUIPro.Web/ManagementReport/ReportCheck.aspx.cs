using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BLL;
using System.IO;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.ManagementReport
{
    public partial class ReportCheck : PageBase
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
            //FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
                this.txtReportDate.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                // 绑定表格
                //BindGrid();
            }
        }

        private bool IsShow(string projectId)
        {
            DateTime startTime = Funs.GetNewDateTimeOrNow(this.txtReportDate.Text + "-01");
            DateTime endTime = startTime.AddMonths(1);
            bool b = false;
            Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportsByMonthsAndProjectId(startTime, projectId);
            if (monthReport != null)
            {
                b = true;
            }
            else    //项目当月未生成月报且完工，但有数据 
            {
                Model.Base_ProjectSate projectSate = (from x in Funs.DB.Base_ProjectSate
                                                      where x.ProjectId == projectId
                                                      orderby x.CompileDate descending
                                                      select x).FirstOrDefault();
                if (projectSate != null)
                {
                    if (projectSate.CompileDate >= startTime && projectSate.CompileDate < endTime)
                    {
                        b = true;
                    }
                }
            }
            return b;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            DateTime months = Convert.ToDateTime(this.txtReportDate.Text + "-01");
            //项目信息集合
            var projects = from x in Funs.DB.Base_Project orderby x.ProjectCode descending select x;
            //施工中项目集合
            List<Model.Base_Project> dataProjects = BLL.ProjectService.GetProjectDropDownListByState("1");
            List<string> dataProjectIds = dataProjects.Select(x => x.ProjectId).ToList();
            List<string> showProjectIds = new List<string>();
            foreach (Model.Base_Project project in projects)
            {
                if (dataProjectIds.Contains(project.ProjectId) || IsShow(project.ProjectId))
                {
                    showProjectIds.Add(project.ProjectId);
                }
            }
            //项目月报记录集合
            var monthReports = from x in Funs.DB.Manager_MonthReportB
                               join y in Funs.DB.Base_Project
                               on x.ProjectId equals y.ProjectId
                               where x.Months == months
                               orderby y.ProjectCode descending
                               select x;
            //本部百万工时记录
            var millionsMonthlyReportItems = from x in Funs.DB.Information_MillionsMonthlyReportItem
                                             join y in Funs.DB.Information_MillionsMonthlyReport
                                             on x.MillionsMonthlyReportId equals y.MillionsMonthlyReportId
                                             where y.Year == months.Year && y.Month == months.Month && x.Affiliation != "本月合计"
                                             select x;
            //项目百万工时记录集合
            var projectMillionsMonthlyReports = from x in Funs.DB.InformationProject_MillionsMonthlyReport
                                                where x.Year == months.Year && x.Month == months.Month
                                                && x.States == BLL.Const.State_2
                                                select x;
            //本部职工伤亡记录
            var accidentCauseReport = (from x in Funs.DB.Information_AccidentCauseReport
                                       where x.Year == months.Year && x.Month == months.Month
                                       select x).FirstOrDefault();
            //项目职工伤亡记录集合
            var projectAccidentCauseReports = from x in Funs.DB.InformationProject_AccidentCauseReport
                                              where x.Year == months.Year && x.Month == months.Month
                                              && x.States == BLL.Const.State_2
                                              select x;
            string accidentDef = string.Empty;
            //项目事故记录集合
            List<Model.Manager_AccidentDetailSortB> details = new List<Model.Manager_AccidentDetailSortB>();
            foreach (var item in monthReports)
            {
                details.AddRange(BLL.AccidentDetailSortBService.GetAccidentDetailSortsByMonthReportId(item.MonthReportId));
            }
            foreach (var detail in details)
            {
                accidentDef += detail.AccidentType + ",";
            }
            if (!string.IsNullOrEmpty(accidentDef))
            {
                accidentDef = accidentDef.Substring(0, accidentDef.LastIndexOf(","));
            }
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("ProjectCode", typeof(string));
            table.Columns.Add("ProjectName", typeof(string));
            table.Columns.Add("ManHours1", typeof(string));
            table.Columns.Add("SafeManHours1", typeof(string));
            table.Columns.Add("Check1", typeof(string));
            table.Columns.Add("ManCount1", typeof(string));
            table.Columns.Add("ManHours2", typeof(string));
            table.Columns.Add("Check2", typeof(string));
            table.Columns.Add("ManCount2", typeof(string));
            table.Columns.Add("Check3", typeof(string));
            table.Columns.Add("ManHours3", typeof(string));
            table.Columns.Add("Check4", typeof(string));
            table.Columns.Add("ManCount3", typeof(string));
            table.Columns.Add("Check5", typeof(string));
            table.Columns.Add("AccidentDef", typeof(string));
            table.Columns.Add("View", typeof(string));
            DataRow row = table.NewRow();
            row["Id"] = 0;
            row["ProjectCode"] = "本部管理";
            row["ProjectName"] = "各项目综合";
            row["ManHours1"] = monthReports.Sum(x => x.Manhours) ?? 0;
            row["SafeManHours1"] = monthReports.Sum(x => x.HseManhours) ?? 0;
            row["Check1"] = Funs.GetNewIntOrZero(row["ManHours1"].ToString()) == Funs.GetNewIntOrZero(row["SafeManHours1"].ToString()) ? "相同" : "不同";
            row["ManCount1"] = monthReports.Sum(x => x.TotalManNum) ?? 0;
            decimal totalWorkNum = 0;
            foreach (var millionsMonthlyReportItem in millionsMonthlyReportItems)
            {
                if (millionsMonthlyReportItem != null && millionsMonthlyReportItem.TotalWorkNum != null)
                {
                    totalWorkNum += Funs.GetNewDecimalOrZero(millionsMonthlyReportItem.TotalWorkNum.ToString());
                }
            }
            row["ManHours2"] = (totalWorkNum * 10000).ToString("0.##");
            row["Check2"] = Funs.GetNewIntOrZero(row["ManHours1"].ToString()) == Funs.GetNewDecimalOrZero(row["ManHours2"].ToString()) ? "相同" : "不同";
            int sumPersonNum = 0;
            foreach (var millionsMonthlyReportItem in millionsMonthlyReportItems)
            {
                if (millionsMonthlyReportItem != null && millionsMonthlyReportItem.SumPersonNum != null)
                {
                    sumPersonNum += Funs.GetNewIntOrZero(millionsMonthlyReportItem.SumPersonNum.ToString());
                }
            }
            row["ManCount2"] = sumPersonNum;
            row["Check3"] = Funs.GetNewIntOrZero(row["ManCount1"].ToString()) == Funs.GetNewIntOrZero(row["ManCount2"].ToString()) ? "相同" : "不同";
            row["ManHours3"] = (accidentCauseReport != null ? (accidentCauseReport.AverageTotalHours ?? 0) : 0).ToString("0.##");
            row["Check4"] = Funs.GetNewIntOrZero(row["ManHours1"].ToString()) == Funs.GetNewDecimalOrZero(row["ManHours3"].ToString()) ? "相同" : "不同";
            row["ManCount3"] = accidentCauseReport != null ? (accidentCauseReport.AverageManHours ?? 0) : 0;
            row["Check5"] = Funs.GetNewIntOrZero(row["ManCount1"].ToString()) == Funs.GetNewIntOrZero(row["ManCount3"].ToString()) ? "相同" : "不同";
            row["AccidentDef"] = accidentDef;
            table.Rows.Add(row);
            int i = 1;
            foreach (var showProjectId in showProjectIds)
            {
                Model.Manager_MonthReportB monthReport = monthReports.FirstOrDefault(x => x.ProjectId == showProjectId);
                DataRow rowItem = table.NewRow();
                Model.Base_Project project = projects.FirstOrDefault(x => x.ProjectId == showProjectId);
                rowItem["Id"] = i;
                rowItem["ProjectCode"] = project != null ? project.ProjectCode : "";
                rowItem["ProjectName"] = project != null ? project.ProjectName : "";
                rowItem["ManHours1"] = monthReport != null ? (monthReport.Manhours ?? 0) : 0;
                rowItem["SafeManHours1"] = monthReport != null ? (monthReport.HseManhours ?? 0) : 0;
                rowItem["Check1"] = Funs.GetNewIntOrZero(rowItem["ManHours1"].ToString()) == Funs.GetNewIntOrZero(rowItem["SafeManHours1"].ToString()) ? "相同" : "不同";
                rowItem["ManCount1"] = monthReport != null ? (monthReport.TotalManNum ?? 0) : 0;
                var projectMillionsMonthlyReport = projectMillionsMonthlyReports.FirstOrDefault(x => x.ProjectId == showProjectId);
                rowItem["ManHours2"] = ((projectMillionsMonthlyReport != null ? (projectMillionsMonthlyReport.TotalWorkNum ?? 0) : 0) * 10000).ToString("0.##");
                rowItem["Check2"] = Funs.GetNewIntOrZero(rowItem["ManHours1"].ToString()) == Funs.GetNewDecimalOrZero(rowItem["ManHours2"].ToString()) ? "相同" : "不同";
                rowItem["ManCount2"] = projectMillionsMonthlyReport != null ? (projectMillionsMonthlyReport.SumPersonNum ?? 0) : 0;
                rowItem["Check3"] = Funs.GetNewIntOrZero(rowItem["ManCount1"].ToString()) == Funs.GetNewIntOrZero(rowItem["ManCount2"].ToString()) ? "相同" : "不同";
                var projectAccidentCauseReport = projectAccidentCauseReports.FirstOrDefault(x => x.ProjectId == showProjectId);
                rowItem["ManHours3"] = (projectAccidentCauseReport != null ? (projectAccidentCauseReport.AverageTotalHours ?? 0) : 0).ToString("0.##");
                rowItem["Check4"] = Funs.GetNewIntOrZero(rowItem["ManHours1"].ToString()) == Funs.GetNewDecimalOrZero(rowItem["ManHours3"].ToString()) ? "相同" : "不同";
                rowItem["ManCount3"] = projectAccidentCauseReport != null ? (projectAccidentCauseReport.AverageManHours ?? 0) : 0;
                rowItem["Check5"] = Funs.GetNewIntOrZero(rowItem["ManCount1"].ToString()) == Funs.GetNewIntOrZero(rowItem["ManCount3"].ToString()) ? "相同" : "不同";
                string itemAccidentDef = string.Empty;
                //项目事故记录集合
                List<Model.Manager_AccidentDetailSortB> itemDetails = new List<Model.Manager_AccidentDetailSortB>();
                itemDetails.AddRange(BLL.AccidentDetailSortBService.GetAccidentDetailSortsByMonthReportId(showProjectId));
                foreach (var detail in itemDetails)
                {
                    itemAccidentDef += detail.AccidentType + ",";
                }
                if (!string.IsNullOrEmpty(itemAccidentDef))
                {
                    itemAccidentDef = itemAccidentDef.Substring(0, itemAccidentDef.LastIndexOf(","));
                }
                rowItem["AccidentDef"] = itemAccidentDef;
                rowItem["View"] = "查看月报";
                table.Rows.Add(rowItem);
                i++;
            }

            Grid1.RecordCount = table.Rows.Count;
            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int j = 0; j < this.Grid1.Rows.Count; j++)
            {
                if (this.Grid1.Rows[j].Values[6].ToString() == "不同")
                {
                    this.Grid1.Rows[j].CellCssClasses[6] = "color";
                }
                if (this.Grid1.Rows[j].Values[10].ToString() == "不同")
                {
                    this.Grid1.Rows[j].CellCssClasses[10] = "color";
                }
                if (this.Grid1.Rows[j].Values[12].ToString() == "不同")
                {
                    this.Grid1.Rows[j].CellCssClasses[12] = "color";
                }
                if (this.Grid1.Rows[j].Values[15].ToString() == "不同")
                {
                    this.Grid1.Rows[j].CellCssClasses[15] = "color";
                }
                if (this.Grid1.Rows[j].Values[17].ToString() == "不同")
                {
                    this.Grid1.Rows[j].CellCssClasses[17] = "color";
                }
            }
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
        #endregion

        #region 校对
        /// <summary>
        /// 校对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtReportDate.Text.Trim()))
            {
                BindGrid();
            }
            else
            {
                Alert.ShowInTop("请选择月份！", MessageBoxIcon.Warning);
                return;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("月报与集团报表校对" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “ReportCheck.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ReportCheck.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfSee")
                    {
                        html = (row.FindControl("lbtnSee") as AspNet.LinkButton).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region Grid点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string projectCode = this.Grid1.Rows[e.RowIndex].DataKeys[1].ToString();
            if (e.CommandName == "View")
            {
                if (string.IsNullOrEmpty(this.txtReportDate.Text.Trim()))
                {
                    ShowNotify("请选择月份！", MessageBoxIcon.Warning);
                    return;
                }
                Model.Base_Project project = (from x in Funs.DB.Base_Project where x.ProjectCode == projectCode select x).FirstOrDefault();
                if (project == null)
                {
                    ShowNotify("该项目信息不存在！", MessageBoxIcon.Warning);
                    return;
                }
                Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonth(Convert.ToDateTime(this.txtReportDate.Text.Trim() + "-01"), project.ProjectId);
                if (monthReport == null)
                {
                    ShowNotify("该项目当前月报不存在！", MessageBoxIcon.Warning);
                    return;
                }
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Manager/MonthReportBView.aspx?MonthReportId={0}", monthReport.MonthReportId, "查看 - ")));
            }
        }
        #endregion
    }
}