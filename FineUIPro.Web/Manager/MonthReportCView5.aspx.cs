using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportCView5 : PageBase
    {
        #region 定义项
        /// <summary>
        /// 月报告查主键
        /// </summary>
        public string MonthReportId
        {
            get
            {
                return (string)ViewState["MonthReportId"];
            }
            set
            {
                ViewState["MonthReportId"] = value;
            }
        }

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

        private static DateTime startTime;

        private static DateTime endTime;

        private static DateTime yearStartTime;

        private static DateTime projectStartTime;

        #region 定义集合
        /// <summary>
        /// 5.1.2 本月文件、方案修编情况说明集合
        /// </summary>
        private static List<Model.Manager_Month_PlanC> plans = new List<Model.Manager_Month_PlanC>();

        /// <summary>
        /// 5.2.2 详细审查记录集合
        /// </summary>
        private static List<Model.Manager_Month_ReviewRecordC> reviewRecords = new List<Model.Manager_Month_ReviewRecordC>();

        /// <summary>
        /// 5.3 HSSE文件管理集合
        /// </summary>
        private static List<Model.Manager_Month_FileManageC> fileManages = new List<Model.Manager_Month_FileManageC>();

        #endregion

        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                plans.Clear();
                reviewRecords.Clear();
                fileManages.Clear();
                this.MonthReportId = Request.Params["monthReportId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                DateTime months = Convert.ToDateTime(Request.Params["months"]);
                startTime = Convert.ToDateTime(Request.Params["startTime"]);
                endTime = Convert.ToDateTime(Request.Params["endTime"]);
                yearStartTime = Convert.ToDateTime(Request.Params["yearStartTime"]);
                Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonths(months, this.CurrUser.LoginProjectId);
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(ProjectId);
                if (project.StartDate != null)
                {
                    projectStartTime = Convert.ToDateTime(project.StartDate);
                }
                if (monthReport != null)
                {
                    this.MonthReportId = monthReport.MonthReportId;
                    this.ProjectId = monthReport.ProjectId;
                    Model.SUBHSSEDB db = Funs.DB;
                    this.txtActionPlanNum.Text = (monthReport.ActionPlanNum ?? 0).ToString();
                    this.txtYearActionPlanNum.Text = (monthReport.YearActionPlanNum ?? 0).ToString();
                    this.txtMonthSolutionNum.Text = (monthReport.MonthSolutionNum ?? 0).ToString();
                    this.txtYearSolutionNum.Text = (monthReport.YearSolutionNum ?? 0).ToString();
                    //5.1.2 本月文件、方案修编情况说明
                    plans = (from x in db.Manager_Month_PlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (plans.Count > 0)
                    {
                        this.gvMonthPlan.DataSource = plans;
                        this.gvMonthPlan.DataBind();
                    }
                    //5.2.2 详细审查记录
                    reviewRecords = (from x in db.Manager_Month_ReviewRecordC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (reviewRecords.Count > 0)
                    {
                        this.gvReviewRecord.DataSource = reviewRecords;
                        this.gvReviewRecord.DataBind();
                    }
                    //5.3 HSE文件管理
                    fileManages = (from x in db.Manager_Month_FileManageC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (fileManages.Count > 0)
                    {
                        this.gvFileManage.DataSource = fileManages;
                        this.gvFileManage.DataBind();
                    }
                }
            }
        }
        #endregion
    }
}