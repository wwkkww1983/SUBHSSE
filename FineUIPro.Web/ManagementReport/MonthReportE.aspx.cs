using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ManagementReport
{
    public partial class MonthReportE : PageBase
    {
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
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonth, ConstValue.Group_0009, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, ConstValue.Group_0008, true);


                List<Model.Base_Project> projects = (from x in Funs.DB.Base_Project
                                                     where x.IsForeign == true
                                                     select x).ToList();    //海外项目
                drpProject.DataValueField = "ProjectId";
                drpProject.DataTextField = "ProjectName";
                var projectlist = projects;
                drpProject.DataSource = projectlist;
                drpProject.DataBind();
                Funs.FineUIPleaseSelect(drpProject);
                // 绑定表格
                //BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT MonthReport.*,Project.ProjectName,Project.ProjectCode"
                                 + @" FROM Manager_MonthReportE AS MonthReport "
                                 + @" LEFT JOIN Base_Project AS Project ON Project.ProjectId=MonthReport.ProjectId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.drpProject.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND MonthReport.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.drpProject.SelectedValue));
            }
            if (this.drpYear.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND YEAR(MonthReport.Months)=@Year";
                listStr.Add(new SqlParameter("@Year", this.drpYear.SelectedValue));
            }
            if (this.drpMonth.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND Month(MonthReport.Months)=@Month";
                listStr.Add(new SqlParameter("@Month", this.drpMonth.SelectedValue));
            }
            strSql += " order by Project.ProjectCode desc";
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            SummaryData();
        }

        /// <summary>
        /// 合计
        /// </summary>
        private void SummaryData()
        {
            decimal sumContractAmount = 0, sumThisIncome = 0, sumYearIncome = 0, sumTotalIncome=0,sumThisInvestment=0,sumYearInvestment=0,
                sumTotalInvestment=0,sumThisReward=0,sumYearReward=0,sumTotalReward=0,sumThisPunish=0,sumYearPunish=0,sumTotalPunish=0;
            int sumThisPersonNum = 0, sumYearPersonNum = 0, sumTotalPersonNum = 0, sumThisForeignPersonNum = 0, sumYearForeignPersonNum=0,
                sumTotalForeignPersonNum = 0, sumThisTrainPersonNum = 0, sumYearTrainPersonNum = 0, sumTotalTrainPersonNum=0,
                sumThisCheckNum = 0, sumYearCheckNum = 0, sumTotalCheckNum = 0, sumThisViolationNum = 0, sumYearViolationNum=0,
                sumTotalViolationNum = 0, sumThisEmergencyDrillNum = 0, sumYearEmergencyDrillNum = 0, sumTotalEmergencyDrillNum=0,
                sumThisHSEManhours = 0, sumYearHSEManhours = 0, sumTotalHSEManhours = 0, sumThisRecordEvent = 0, sumYearRecordEvent=0,
                sumTotalRecordEvent = 0, sumThisNoRecordEvent = 0, sumYearNoRecordEvent = 0, sumTotalNoRecordEvent=0;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                if (values["ContractAmount"].ToString() != "")
                {
                    sumContractAmount += values.Value<decimal>("ContractAmount");
                }
                if (values["ThisIncome"].ToString() != "")
                {
                    sumThisIncome += values.Value<decimal>("ThisIncome");
                }
                if (values["YearIncome"].ToString() != "")
                {
                    sumYearIncome += values.Value<decimal>("YearIncome");
                }
                if (values["TotalIncome"].ToString() != "")
                {
                    sumTotalIncome += values.Value<decimal>("TotalIncome");
                }
                if (values["ThisPersonNum"].ToString() != "")
                {
                    sumThisPersonNum += values.Value<int>("ThisPersonNum");
                }
                if (values["YearPersonNum"].ToString() != "")
                {
                    sumYearPersonNum += values.Value<int>("YearPersonNum");
                }
                if (values["TotalPersonNum"].ToString() != "")
                {
                    sumTotalPersonNum += values.Value<int>("TotalPersonNum");
                }
                if (values["ThisForeignPersonNum"].ToString() != "")
                {
                    sumThisForeignPersonNum += values.Value<int>("ThisForeignPersonNum");
                }
                if (values["YearForeignPersonNum"].ToString() != "")
                {
                    sumYearForeignPersonNum += values.Value<int>("YearForeignPersonNum");
                }
                if (values["TotalForeignPersonNum"].ToString() != "")
                {
                    sumTotalForeignPersonNum += values.Value<int>("TotalForeignPersonNum");
                }
                if (values["ThisTrainPersonNum"].ToString() != "")
                {
                    sumThisTrainPersonNum += values.Value<int>("ThisTrainPersonNum");
                }
                if (values["YearTrainPersonNum"].ToString() != "")
                {
                    sumYearTrainPersonNum += values.Value<int>("YearTrainPersonNum");
                }
                if (values["TotalTrainPersonNum"].ToString() != "")
                {
                    sumTotalTrainPersonNum += values.Value<int>("TotalTrainPersonNum");
                }
                if (values["ThisCheckNum"].ToString() != "")
                {
                    sumThisCheckNum += values.Value<int>("ThisCheckNum");
                }
                if (values["YearCheckNum"].ToString() != "")
                {
                    sumYearCheckNum += values.Value<int>("YearCheckNum");
                }
                if (values["TotalCheckNum"].ToString() != "")
                {
                    sumTotalCheckNum += values.Value<int>("TotalCheckNum");
                }
                if (values["ThisViolationNum"].ToString() != "")
                {
                    sumThisViolationNum += values.Value<int>("ThisViolationNum");
                }
                if (values["YearViolationNum"].ToString() != "")
                {
                    sumYearViolationNum += values.Value<int>("YearViolationNum");
                }
                if (values["TotalViolationNum"].ToString() != "")
                {
                    sumTotalViolationNum += values.Value<int>("TotalViolationNum");
                }
                if (values["ThisInvestment"].ToString() != "")
                {
                    sumThisInvestment += values.Value<decimal>("ThisInvestment");
                }
                if (values["YearInvestment"].ToString() != "")
                {
                    sumYearInvestment += values.Value<decimal>("YearInvestment");
                }
                if (values["TotalInvestment"].ToString() != "")
                {
                    sumTotalInvestment += values.Value<decimal>("TotalInvestment");
                }
                if (values["ThisReward"].ToString() != "")
                {
                    sumThisReward += values.Value<decimal>("ThisReward");
                }
                if (values["YearReward"].ToString() != "")
                {
                    sumYearReward += values.Value<decimal>("YearReward");
                }
                if (values["TotalReward"].ToString() != "")
                {
                    sumTotalReward += values.Value<decimal>("TotalReward");
                }
                if (values["ThisPunish"].ToString() != "")
                {
                    sumThisPunish += values.Value<decimal>("ThisPunish");
                }
                if (values["YearPunish"].ToString() != "")
                {
                    sumYearPunish += values.Value<decimal>("YearPunish");
                }
                if (values["TotalPunish"].ToString() != "")
                {
                    sumTotalPunish += values.Value<decimal>("TotalPunish");
                }
                if (values["ThisEmergencyDrillNum"].ToString() != "")
                {
                    sumThisEmergencyDrillNum += values.Value<int>("ThisEmergencyDrillNum");
                }
                if (values["YearEmergencyDrillNum"].ToString() != "")
                {
                    sumYearEmergencyDrillNum += values.Value<int>("YearEmergencyDrillNum");
                }
                if (values["TotalEmergencyDrillNum"].ToString() != "")
                {
                    sumTotalEmergencyDrillNum += values.Value<int>("TotalEmergencyDrillNum");
                }
                if (values["ThisHSEManhours"].ToString() != "")
                {
                    sumThisHSEManhours += values.Value<int>("ThisHSEManhours");
                }
                if (values["YearHSEManhours"].ToString() != "")
                {
                    sumYearHSEManhours += values.Value<int>("YearHSEManhours");
                }
                if (values["TotalHSEManhours"].ToString() != "")
                {
                    sumTotalHSEManhours += values.Value<int>("TotalHSEManhours");
                }
                if (values["ThisRecordEvent"].ToString() != "")
                {
                    sumThisRecordEvent += values.Value<int>("ThisRecordEvent");
                }
                if (values["YearRecordEvent"].ToString() != "")
                {
                    sumYearRecordEvent += values.Value<int>("YearRecordEvent");
                }
                if (values["TotalRecordEvent"].ToString() != "")
                {
                    sumTotalRecordEvent += values.Value<int>("TotalRecordEvent");
                }
                if (values["ThisNoRecordEvent"].ToString() != "")
                {
                    sumThisNoRecordEvent += values.Value<int>("ThisNoRecordEvent");
                }
                if (values["YearNoRecordEvent"].ToString() != "")
                {
                    sumYearNoRecordEvent += values.Value<int>("YearNoRecordEvent");
                }
                if (values["TotalNoRecordEvent"].ToString() != "")
                {
                    sumTotalNoRecordEvent += values.Value<int>("TotalNoRecordEvent");
                }
            }

            if (this.Grid1.Rows.Count > 0)
            {
                JObject summary = new JObject();
                summary.Add("ProjectName", "合计：");
                summary.Add("ContractAmount", sumContractAmount);
                summary.Add("ThisIncome", sumThisIncome);
                summary.Add("YearIncome", sumYearIncome);
                summary.Add("TotalIncome", sumTotalIncome);
                summary.Add("ThisPersonNum", sumThisPersonNum);
                summary.Add("YearPersonNum", sumYearPersonNum);
                summary.Add("TotalPersonNum", sumTotalPersonNum);
                summary.Add("ThisForeignPersonNum", sumThisForeignPersonNum);
                summary.Add("YearForeignPersonNum", sumYearForeignPersonNum);
                summary.Add("TotalForeignPersonNum", sumTotalForeignPersonNum);
                summary.Add("ThisTrainPersonNum", sumThisTrainPersonNum);
                summary.Add("YearTrainPersonNum", sumYearTrainPersonNum);
                summary.Add("TotalTrainPersonNum", sumTotalTrainPersonNum);
                summary.Add("ThisCheckNum", sumThisCheckNum);
                summary.Add("YearCheckNum", sumYearCheckNum);
                summary.Add("TotalCheckNum", sumTotalCheckNum);
                summary.Add("ThisViolationNum", sumThisViolationNum);
                summary.Add("YearViolationNum", sumYearViolationNum);
                summary.Add("TotalViolationNum", sumTotalViolationNum);
                summary.Add("ThisInvestment", sumThisInvestment);
                summary.Add("YearInvestment", sumYearInvestment);
                summary.Add("TotalInvestment", sumTotalInvestment);
                summary.Add("ThisReward", sumThisReward);
                summary.Add("YearReward", sumYearReward);
                summary.Add("TotalReward", sumTotalReward);
                summary.Add("ThisPunish", sumThisPunish);
                summary.Add("YearPunish", sumYearPunish);
                summary.Add("TotalPunish", sumTotalPunish);
                summary.Add("ThisEmergencyDrillNum", sumThisEmergencyDrillNum);
                summary.Add("YearEmergencyDrillNum", sumYearEmergencyDrillNum);
                summary.Add("TotalEmergencyDrillNum", sumTotalEmergencyDrillNum);
                summary.Add("ThisHSEManhours", sumThisHSEManhours);
                summary.Add("YearHSEManhours", sumYearHSEManhours);
                summary.Add("TotalHSEManhours", sumTotalHSEManhours);
                summary.Add("ThisRecordEvent", sumThisRecordEvent);
                summary.Add("YearRecordEvent", sumYearRecordEvent);
                summary.Add("TotalRecordEvent", sumTotalRecordEvent);
                summary.Add("ThisNoRecordEvent", sumThisNoRecordEvent);
                summary.Add("YearNoRecordEvent", sumYearNoRecordEvent);
                summary.Add("TotalNoRecordEvent", sumTotalNoRecordEvent);
                Grid1.SummaryData = summary;
            }
            else
            {
                Grid1.SummaryData = null;
            }
        }
        #endregion

        #region 统计按钮事件
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 表头过滤
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("海外工程项目月度HSSE统计表" + filename, System.Text.Encoding.UTF8) + ".xls");
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