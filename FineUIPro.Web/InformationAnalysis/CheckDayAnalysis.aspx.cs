using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.InformationAnalysis
{
    public partial class CheckDayAnalysis : PageBase
    {
        #region 定义项
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                BLL.ConstValue.InitConstValueDropDownList(this.drpChartType, ConstValue.Group_ChartType, false);
                this.AnalyseData();
            }
        }
        #endregion

        #region 统计
        /// <summary>
        /// 统计方法
        /// </summary>
        private void AnalyseData()
        {
            string strSql = @"select pd.CheckContent as '检查项',count(*) as '数量' from dbo.Check_CheckDayDetail d
                                left join dbo.Check_CheckDay c on c.CheckDayId=d.CheckDayId
                                left join dbo.Check_ProjectCheckItemDetail pd on pd.CheckItemDetailId=d.CheckItem
                                where c.ProjectId=@ProjectId and pd.CheckContent is not null";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (!string.IsNullOrEmpty(this.txtStarTime.Text))
            {
                strSql += " AND c.CheckTime >= @StarTime";
                listStr.Add(new SqlParameter("@StarTime", this.txtStarTime.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text))
            {
                strSql += " AND c.CheckTime <= @EndTime";
                listStr.Add(new SqlParameter("@EndTime", this.txtEndTime.Text.Trim()));
            }
            strSql += " group by pd.CheckContent";
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            this.gvCheck.DataSource = tb;
            this.gvCheck.DataBind();
            this.ChartCostTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(tb, "日常巡检分析", this.drpChartType.SelectedValue, 1150, 450, this.ckbShow.Checked));
        }
        #endregion

        #region 统计查询
        /// <summary>
        /// 图形变换 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AnalyseData();
        }

        protected void ckbShow_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.AnalyseData();
        }

        /// <summary>
        /// 统计分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            this.AnalyseData();
        }
        #endregion
    }
}