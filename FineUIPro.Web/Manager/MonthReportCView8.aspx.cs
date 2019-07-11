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
    public partial class MonthReportCView8 : PageBase
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
        /// 9.1 危险源动态识别及控制集合
        /// </summary>
        private static List<Model.Manager_Month_HazardC> hazards = new List<Model.Manager_Month_HazardC>();

        /// <summary>
        /// 9.2 HSSE培训集合
        /// </summary>
        private static List<Model.Manager_Month_TrainC> trains = new List<Model.Manager_Month_TrainC>();

        /// <summary>
        /// 9.3 HSSE检查集合
        /// </summary>
        private static List<Model.Manager_Month_CheckC> checks = new List<Model.Manager_Month_CheckC>();

        /// <summary>
        /// 9.4 HSSE会议集合
        /// </summary>
        private static List<Model.Manager_Month_MeetingC> meetings = new List<Model.Manager_Month_MeetingC>();

        /// <summary>
        /// 9.5 HSSE活动集合
        /// </summary>
        private static List<Model.Manager_Month_ActivitiesC> activitiess = new List<Model.Manager_Month_ActivitiesC>();

        /// <summary>
        /// 9.6.1 应急预案修编集合
        /// </summary>
        private static List<Model.Manager_Month_EmergencyPlanC> emergencyPlans = new List<Model.Manager_Month_EmergencyPlanC>();

        /// <summary>
        /// 9.6.2 应急演练活动集合
        /// </summary>
        private static List<Model.Manager_Month_EmergencyExercisesC> emergencyExercisess = new List<Model.Manager_Month_EmergencyExercisesC>();

        /// <summary>
        /// 9.7 HSE费用投入计划集合
        /// </summary>
        private static List<Model.Manager_Month_CostInvestmentPlanC> costInvestmentPlans = new List<Model.Manager_Month_CostInvestmentPlanC>();

        /// <summary>
        /// 9.8 HSE管理文件/方案修编计划集合
        /// </summary>
        private static List<Model.Manager_Month_ManageDocPlanC> manageDocPlans = new List<Model.Manager_Month_ManageDocPlanC>();

        /// <summary>
        /// 9.9 其他HSE工作计划
        /// </summary>
        private static List<Model.Manager_Month_OtherWorkPlanC> otherWorkPlans = new List<Model.Manager_Month_OtherWorkPlanC>();

        #endregion

        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                    months = Convert.ToDateTime(monthReport.Months);
                    this.txtNextEmergencyResponse.Text = monthReport.NextEmergencyResponse;
                    Model.SUBHSSEDB db = Funs.DB;
                    //9.1 危险源动态识别及控制
                    hazards = (from x in db.Manager_Month_HazardC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (hazards.Count > 0)
                    {
                        this.gvHazard.DataSource = hazards;
                        this.gvHazard.DataBind();
                    }
                    //9.3 HSSE检查
                    checks = (from x in db.Manager_Month_CheckC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (checks.Count > 0)
                    {
                        this.gvCheck.DataSource = checks;
                        this.gvCheck.DataBind();
                    }
                    //9.8 HSE管理文件/方案修编计划
                    manageDocPlans = (from x in db.Manager_Month_ManageDocPlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (manageDocPlans.Count > 0)
                    {
                        this.gvManageDocPlan.DataSource = manageDocPlans;
                        this.gvManageDocPlan.DataBind();
                    }
                    //9.9其他HSE工作计划
                    otherWorkPlans = (from x in db.Manager_Month_OtherWorkPlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (otherWorkPlans.Count > 0)
                    {
                        this.gvOtherWorkPlan.DataSource = otherWorkPlans;
                        this.gvOtherWorkPlan.DataBind();
                    }
                }
            }
        }
        #endregion
    }
}