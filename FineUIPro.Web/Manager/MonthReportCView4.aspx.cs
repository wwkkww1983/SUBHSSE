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
                Model.Manager_MonthReportC mr = BLL.MonthReportCService.GetLastMonthReportByDate(endTime, this.ProjectId);
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
                    Model.SUBHSSEDB db = Funs.DB;
                    this.txtMainActivitiesDef.Text = monthReport.MainActivitiesDef;
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
                    if (monthReport.MeetingNum != null)
                    {
                        this.txtMeetingNum.Text = (monthReport.MeetingNum ?? 0).ToString();
                        this.txtYearMeetingNum.Text = (monthReport.YearMeetingNum ?? 0).ToString();
                    }
                    else
                    {
                        //会议数量
                        int meetingNum1 = BLL.WeekMeetingService.GetCountByTime(startTime, endTime, this.ProjectId);
                        int meetingNum2 = BLL.MonthMeetingService.GetCountByTime(startTime, endTime, this.ProjectId);
                        int meetingNum3 = BLL.SpecialMeetingService.GetCountByTime(startTime, endTime, this.ProjectId);
                        int meetingNum4 = BLL.AttendMeetingService.GetCountByTime(startTime, endTime, this.ProjectId);
                        this.txtMeetingNum.Text = (meetingNum1 + meetingNum2 + meetingNum3 + meetingNum4).ToString();
                        if (mr != null)
                        {
                            if (mr.YearMeetingNum != 0)
                            {
                                this.txtYearMeetingNum.Text = (mr.YearMeetingNum + Convert.ToInt32(this.txtMeetingNum.Text.Trim())).ToString();
                            }
                            else
                            {
                                this.txtYearMeetingNum.Text = txtMeetingNum.Text.Trim();
                            }
                        }
                        else
                        {
                            this.txtYearMeetingNum.Text = txtMeetingNum.Text.Trim();
                        }
                    }
                    //会议情况
                    meetingSorts = (from x in db.Manager_MeetingSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (meetingSorts.Count > 0)
                    {
                        this.gvMeetingSort.DataSource = meetingSorts;
                        this.gvMeetingSort.DataBind();
                    }
                    if (monthReport.ComplexEmergencyNum != null)
                    {
                        this.txtComplexEmergencyNum.Text = (monthReport.ComplexEmergencyNum ?? 0).ToString();
                        this.txtYearComplexEmergencyNum.Text = (monthReport.YearComplexEmergencyNum ?? 0).ToString();
                        this.txtTotalComplexEmergencyNum.Text = (monthReport.TotalComplexEmergencyNum ?? 0).ToString();
                        this.txtSpecialEmergencyNum.Text = (monthReport.SpecialEmergencyNum ?? 0).ToString();
                        this.txtYearSpecialEmergencyNum.Text = (monthReport.YearSpecialEmergencyNum ?? 0).ToString();
                        this.txtTotalSpecialEmergencyNum.Text = (monthReport.TotalSpecialEmergencyNum ?? 0).ToString();
                        this.txtDrillRecordNum.Text = (monthReport.DrillRecordNum ?? 0).ToString();
                        this.txtYearDrillRecordNum.Text = (monthReport.YearDrillRecordNum ?? 0).ToString();
                        this.txtTotalDrillRecordNum.Text = (monthReport.TotalDrillRecordNum ?? 0).ToString();
                    }
                    else
                    {
                        //应急管理
                        List<Model.Emergency_EmergencyList> complexEmergencys = BLL.EmergencyListService.GetEmergencyListsByEmergencyType(BLL.Const.ComplexEmergencyName, this.ProjectId, startTime, endTime);  //综合应急预案集合
                        int complexEmergencyNum = complexEmergencys.Count;
                        this.txtComplexEmergencyNum.Text = complexEmergencyNum.ToString();
                        List<Model.Emergency_EmergencyList> specialEmergencys = BLL.EmergencyListService.GetOtherEmergencyListsByEmergencyType(BLL.Const.ComplexEmergencyName, this.ProjectId, startTime, endTime);  //专项应急预案集合
                        int specialEmergencyNum = specialEmergencys.Count;
                        this.txtSpecialEmergencyNum.Text = specialEmergencyNum.ToString();
                        this.txtDrillRecordNum.Text = BLL.DrillRecordListService.GetCountByDate(startTime, endTime, this.ProjectId).ToString();
                        if (mr != null)
                        {
                            //综合应急预案
                            if (mr.YearComplexEmergencyNum != 0)
                            {
                                this.txtYearComplexEmergencyNum.Text = (mr.YearComplexEmergencyNum + complexEmergencyNum).ToString();
                                this.txtTotalComplexEmergencyNum.Text = (mr.TotalComplexEmergencyNum + complexEmergencyNum).ToString();
                            }
                            else
                            {
                                this.txtYearComplexEmergencyNum.Text = complexEmergencyNum.ToString();
                                this.txtTotalComplexEmergencyNum.Text = complexEmergencyNum.ToString();
                            }
                            //专项应急预案
                            if (mr.YearSpecialEmergencyNum != 0)
                            {
                                this.txtYearSpecialEmergencyNum.Text = (mr.YearSpecialEmergencyNum + specialEmergencyNum).ToString();
                                this.txtTotalSpecialEmergencyNum.Text = (mr.TotalSpecialEmergencyNum + specialEmergencyNum).ToString();
                            }
                            else
                            {
                                this.txtYearSpecialEmergencyNum.Text = specialEmergencyNum.ToString();
                                this.txtTotalSpecialEmergencyNum.Text = specialEmergencyNum.ToString();
                            }
                            //应急演练活动
                            if (mr.YearDrillRecordNum != 0)
                            {
                                this.txtYearDrillRecordNum.Text = (mr.YearDrillRecordNum + Convert.ToInt32(this.txtDrillRecordNum.Text.Trim())).ToString();
                                this.txtTotalDrillRecordNum.Text = (mr.TotalDrillRecordNum + Convert.ToInt32(this.txtDrillRecordNum.Text.Trim())).ToString();
                            }
                            else
                            {
                                this.txtYearDrillRecordNum.Text = this.txtDrillRecordNum.Text;
                                this.txtTotalDrillRecordNum.Text = this.txtDrillRecordNum.Text;
                            }
                        }
                        else
                        {
                            this.txtYearComplexEmergencyNum.Text = complexEmergencyNum.ToString();
                            this.txtTotalComplexEmergencyNum.Text = complexEmergencyNum.ToString();
                            this.txtYearSpecialEmergencyNum.Text = specialEmergencyNum.ToString();
                            this.txtTotalSpecialEmergencyNum.Text = specialEmergencyNum.ToString();
                            this.txtYearDrillRecordNum.Text = this.txtDrillRecordNum.Text;
                            this.txtTotalDrillRecordNum.Text = this.txtDrillRecordNum.Text;
                        }
                    }
                    this.txtEmergencyManagementWorkDef.Text = monthReport.EmergencyManagementWorkDef;
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
                    if (monthReport.RewardNum != null)
                    {
                        this.txtRewardNum.Text = (monthReport.RewardNum ?? 0).ToString();
                        this.txtYearRewardNum.Text = (monthReport.YearRewardNum ?? 0).ToString();
                        this.txtRewardMoney.Text = (monthReport.RewardMoney ?? 0).ToString();
                        this.txtYearRewardMoney.Text = (monthReport.YearRewardMoney ?? 0).ToString();
                        this.txtPunishNum.Text = (monthReport.PunishNum ?? 0).ToString();
                        this.txtYearPunishNum.Text = (monthReport.YearPunishNum ?? 0).ToString();
                        this.txtPunishMoney.Text = (monthReport.PunishMoney ?? 0).ToString();
                        this.txtYearPunishMoney.Text = (monthReport.YearPunishMoney ?? 0).ToString();
                    }
                    else
                    {
                        this.txtRewardNum.Text = BLL.IncentiveNoticeService.GetCountByDate(startTime, endTime, this.ProjectId).ToString();
                        this.txtRewardMoney.Text = Convert.ToInt32(BLL.IncentiveNoticeService.GetSumMoneyByDate(startTime, endTime, this.ProjectId)).ToString();
                        this.txtPunishNum.Text = BLL.PunishNoticeService.GetCountByDate(startTime, endTime, this.ProjectId).ToString();
                        this.txtPunishMoney.Text = Convert.ToInt32(BLL.PunishNoticeService.GetSumMoneyByDate(startTime, endTime, this.ProjectId)).ToString();
                        if (mr != null)
                        {
                            if (mr.YearRewardNum != 0)
                            {
                                this.txtYearRewardNum.Text = (mr.YearRewardNum + Convert.ToInt32(this.txtRewardNum.Text.Trim())).ToString();
                            }
                            else
                            {
                                this.txtYearRewardNum.Text = this.txtRewardNum.Text.Trim();
                            }
                            if (mr.YearRewardMoney != 0)
                            {
                                this.txtYearRewardMoney.Text = (mr.YearRewardMoney + Convert.ToInt32(this.txtRewardMoney.Text.Trim())).ToString();
                            }
                            else
                            {
                                this.txtYearRewardMoney.Text = this.txtRewardMoney.Text.Trim();
                            }
                            if (mr.YearPunishNum != 0)
                            {
                                this.txtYearPunishNum.Text = (mr.YearPunishNum + Convert.ToInt32(this.txtPunishNum.Text.Trim())).ToString();
                            }
                            else
                            {
                                this.txtYearPunishNum.Text = this.txtPunishNum.Text.Trim();
                            }
                            if (mr.YearPunishMoney != 0)
                            {
                                this.txtYearPunishMoney.Text = (mr.YearPunishMoney + Convert.ToInt32(this.txtPunishMoney.Text.Trim())).ToString();
                            }
                            else
                            {
                                this.txtYearPunishMoney.Text = this.txtPunishMoney.Text.Trim();
                            }
                        }
                        else
                        {
                            this.txtYearRewardNum.Text = this.txtYearRewardNum.Text.Trim();
                            this.txtYearRewardMoney.Text = this.txtRewardMoney.Text.Trim();
                            this.txtYearPunishNum.Text = this.txtPunishNum.Text.Trim();
                            this.txtYearPunishMoney.Text = this.txtPunishMoney.Text.Trim();
                        }
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