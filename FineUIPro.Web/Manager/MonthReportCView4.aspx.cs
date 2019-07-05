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
    public partial class MonthReportCView4 : PageBase
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
        /// 危险源情况集合
        /// </summary>
        private static List<Model.Manager_HazardSortC> hazardSorts = new List<Model.Manager_HazardSortC>();

        /// <summary>
        /// 培训情况集合
        /// </summary>
        private static List<Model.Manager_TrainSortC> trainSorts = new List<Model.Manager_TrainSortC>();

        /// <summary>
        /// 培训活动情况集合
        /// </summary>
        private static List<Model.Manager_TrainActivitySortC> trainActivitySorts = new List<Model.Manager_TrainActivitySortC>();

        /// <summary>
        /// 检查情况集合
        /// </summary>
        private static List<Model.Manager_CheckSortC> checkSorts = new List<Model.Manager_CheckSortC>();

        /// <summary>
        /// 检查明细情况集合
        /// </summary>
        private static List<Model.Manager_CheckDetailSortC> checkDetailSorts = new List<Model.Manager_CheckDetailSortC>();

        /// <summary>
        /// 会议情况集合
        /// </summary>
        private static List<Model.Manager_MeetingSortC> meetingSorts = new List<Model.Manager_MeetingSortC>();

        /// <summary>
        /// HSE宣传活动集合
        /// </summary>
        private static List<Model.Manager_PromotionalActiviteSortC> promotionalActiviteSorts = new List<Model.Manager_PromotionalActiviteSortC>();

        /// <summary>
        /// HSE应急预案集合
        /// </summary>
        private static List<Model.Manager_EmergencySortC> emergencySorts = new List<Model.Manager_EmergencySortC>();

        /// <summary>
        /// HSE应急演练集合
        /// </summary>
        private static List<Model.Manager_DrillSortC> drillSorts = new List<Model.Manager_DrillSortC>();

        /// <summary>
        /// HSE奖励集合
        /// </summary>
        private static List<Model.Manager_IncentiveSortC> rewardSorts = new List<Model.Manager_IncentiveSortC>();

        /// <summary>
        /// HSE处罚集合
        /// </summary>
        private static List<Model.Manager_IncentiveSortC> punishSorts = new List<Model.Manager_IncentiveSortC>();

        /// <summary>
        /// 其他HSE管理活动
        /// </summary>
        private static List<Model.Manager_OtherActiveSortC> otherActiveSorts = new List<Model.Manager_OtherActiveSortC>();

        /// <summary>
        /// 4.9.2 活动情况说明集合
        /// </summary>
        private static List<Model.Manager_Month_ActivityDesC> activityDess = new List<Model.Manager_Month_ActivityDesC>();

        /// <summary>
        /// 4.10 HSE现场其他管理情况集合
        /// </summary>
        private static List<Model.Manager_Month_OtherManagementC> otherManagements = new List<Model.Manager_Month_OtherManagementC>();

        #endregion

        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hazardSorts.Clear();
                trainSorts.Clear();
                trainActivitySorts.Clear();
                checkSorts.Clear();
                checkDetailSorts.Clear();
                meetingSorts.Clear();
                emergencySorts.Clear();
                promotionalActiviteSorts.Clear();
                drillSorts.Clear();
                rewardSorts.Clear();
                punishSorts.Clear();
                otherActiveSorts.Clear();
                activityDess.Clear();
                otherManagements.Clear();
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
                    this.txtHazardNum.Text = (monthReport.HazardNum ?? 0).ToString();
                    this.txtYearHazardNum.Text = (monthReport.YearHazardNum ?? 0).ToString();
                    //危险源情况
                    hazardSorts = (from x in db.Manager_HazardSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (hazardSorts.Count > 0)   //保存过数据
                    {
                        this.gvHazardSort.DataSource = hazardSorts;
                        this.gvHazardSort.DataBind();
                    }
                    //培训情况
                    trainSorts = (from x in db.Manager_TrainSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (trainSorts.Count > 0)
                    {
                        this.gvTrainSort.DataSource = trainSorts;
                        this.gvTrainSort.DataBind();
                    }
                    //培训活动情况
                    trainActivitySorts = (from x in db.Manager_TrainActivitySortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (trainActivitySorts.Count > 0)
                    {
                        this.gvTrainActivitySort.DataSource = trainActivitySorts;
                        this.gvTrainActivitySort.DataBind();
                    }
                    //检查情况
                    checkSorts = (from x in db.Manager_CheckSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (checkSorts.Count > 0)
                    {
                        this.gvCheckSort.DataSource = checkSorts;
                        this.gvCheckSort.DataBind();
                    }
                    //检查明细情况
                    checkDetailSorts = (from x in db.Manager_CheckDetailSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (checkDetailSorts.Count > 0)
                    {
                        this.gvCheckDetailSort.DataSource = checkDetailSorts;
                        this.gvCheckDetailSort.DataBind();
                    }
                    this.txtMeetingNum.Text = (monthReport.MeetingNum ?? 0).ToString();
                    this.txtYearMeetingNum.Text = (monthReport.YearMeetingNum ?? 0).ToString();
                    //会议情况
                    meetingSorts = (from x in db.Manager_MeetingSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (meetingSorts.Count > 0)
                    {
                        this.gvMeetingSort.DataSource = meetingSorts;
                        this.gvMeetingSort.DataBind();
                    }
                    this.txtPromotionalActiviteNum.Text = (monthReport.PromotionalActiviteNum ?? 0).ToString();
                    this.txtYearPromotionalActiviteNum.Text = (monthReport.YearPromotionalActiviteNum ?? 0).ToString();
                    //HSE宣传活动情况
                    promotionalActiviteSorts = (from x in db.Manager_PromotionalActiviteSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (promotionalActiviteSorts.Count > 0)
                    {
                        this.gvPromotionalActiviteSort.DataSource = promotionalActiviteSorts;
                        this.gvPromotionalActiviteSort.DataBind();
                    }
                    this.txtComplexEmergencyNum.Text = (monthReport.ComplexEmergencyNum ?? 0).ToString();
                    this.txtYearComplexEmergencyNum.Text = (monthReport.YearComplexEmergencyNum ?? 0).ToString();
                    this.txtSpecialEmergencyNum.Text = (monthReport.SpecialEmergencyNum ?? 0).ToString();
                    this.txtYearSpecialEmergencyNum.Text = (monthReport.YearSpecialEmergencyNum ?? 0).ToString();
                    this.txtDrillRecordNum.Text = (monthReport.DrillRecordNum ?? 0).ToString();
                    this.txtYearDrillRecordNum.Text = (monthReport.YearDrillRecordNum ?? 0).ToString();
                    //HSE应急预案情况
                    emergencySorts = (from x in db.Manager_EmergencySortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (emergencySorts.Count > 0)
                    {
                        this.gvEmergencySort.DataSource = emergencySorts;
                        this.gvEmergencySort.DataBind();
                    }
                    //HSE应急演练情况
                    drillSorts = (from x in db.Manager_DrillSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (drillSorts.Count > 0)
                    {
                        this.gvDrillSort.DataSource = drillSorts;
                        this.gvDrillSort.DataBind();
                    }
                    this.txtLicenseNum.Text = (monthReport.LicenseNum ?? 0).ToString();
                    this.txtYearLicenseNum.Text = (monthReport.YearLicenseNum ?? 0).ToString();
                    this.txtEquipmentNum.Text = (monthReport.EquipmentNum ?? 0).ToString();
                    this.txtYearEquipmentNum.Text = (monthReport.YearEquipmentNum ?? 0).ToString();
                    this.txtLicenseRemark.Text = monthReport.LicenseRemark;
                    this.txtEquipmentRemark.Text = monthReport.EquipmentRemark;
                    //HSE奖励情况
                    rewardSorts = (from x in db.Manager_IncentiveSortC where x.MonthReportId == MonthReportId && x.BigType == "1" orderby x.SortIndex select x).ToList();
                    if (rewardSorts.Count > 0)
                    {
                        this.gvRewardSort.DataSource = rewardSorts;
                        this.gvRewardSort.DataBind();
                    }
                    //HSE处罚情况
                    punishSorts = (from x in db.Manager_IncentiveSortC where x.MonthReportId == MonthReportId && x.BigType == "2" orderby x.SortIndex select x).ToList();
                    if (punishSorts.Count > 0)
                    {
                        this.gvPunishSort.DataSource = punishSorts;
                        this.gvPunishSort.DataBind();
                    }
                    this.txtRewardNum.Text = (monthReport.RewardNum ?? 0).ToString();
                    this.txtYearRewardNum.Text = (monthReport.YearRewardNum ?? 0).ToString();
                    this.txtRewardMoney.Text = (monthReport.RewardMoney ?? 0).ToString();
                    this.txtYearRewardMoney.Text = (monthReport.YearRewardMoney ?? 0).ToString();
                    this.txtPunishNum.Text = (monthReport.PunishNum ?? 0).ToString();
                    this.txtYearPunishNum.Text = (monthReport.YearPunishNum ?? 0).ToString();
                    this.txtPunishMoney.Text = (monthReport.PunishMoney ?? 0).ToString();
                    this.txtYearPunishMoney.Text = (monthReport.YearPunishMoney ?? 0).ToString();
                    //其他HSE管理活动 
                    otherActiveSorts = (from x in db.Manager_OtherActiveSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (otherActiveSorts.Count > 0)
                    {
                        this.gvOtherActiveSort.DataSource = otherActiveSorts;
                        this.gvOtherActiveSort.DataBind();
                    }
                    //4.9.2 活动说明情况
                    activityDess = (from x in db.Manager_Month_ActivityDesC where x.MonthReportId == MonthReportId select x).ToList();
                    if (activityDess.Count > 0)
                    {
                        this.gvActivityDes.DataSource = activityDess;
                        this.gvActivityDes.DataBind();
                    }
                    //4.10 HSE现场其他管理情况
                    otherManagements = (from x in db.Manager_Month_OtherManagementC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (otherManagements.Count > 0)
                    {
                        this.gvOtherManagement.DataSource = otherManagements;
                        this.gvOtherManagement.DataBind();
                    }
                }
            }
        }
        #endregion
    }
}