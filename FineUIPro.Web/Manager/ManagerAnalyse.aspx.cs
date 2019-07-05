using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerAnalyse : PageBase
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
            SqlParameter[] values = new SqlParameter[]
                {
                    new SqlParameter("@ProjectId", this.ProjectId),
                    new SqlParameter("@StartTime", "2000-01-01"),
                    new SqlParameter("@EndTime", "3000-01-01"),
                };
            DataTable dt = BLL.SQLHelper.GetDataTableRunProc("sp_CostAnalyse", values);
            this.gvAnalyse.DataSource = dt;
            this.gvAnalyse.DataBind();
            this.ChartCostTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dt, "人工时和投入安全费用比率分析", this.drpChartType.SelectedValue, 1150, 450, this.ckbShow.Checked));
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