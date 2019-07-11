using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportCEdit4 : PageBase
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
                    else
                    {
                        GetTrainSort();
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
                    else
                    {
                        GetCheckSort();
                    }
                    //检查明细情况
                    checkDetailSorts = (from x in db.Manager_CheckDetailSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (checkDetailSorts.Count > 0)
                    {
                        this.gvCheckDetailSort.DataSource = checkDetailSorts;
                        this.gvCheckDetailSort.DataBind();
                    }
                    else
                    {
                        GetCheckDetailSort();
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
                    else
                    {
                        GetMeetingSort();
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
                    else
                    {
                        GetRewardSort();
                    }
                    //HSE处罚情况
                    punishSorts = (from x in db.Manager_IncentiveSortC where x.MonthReportId == MonthReportId && x.BigType == "2" orderby x.SortIndex select x).ToList();
                    if (punishSorts.Count > 0)
                    {
                        this.gvPunishSort.DataSource = punishSorts;
                        this.gvPunishSort.DataBind();
                    }
                    else
                    {
                        GetPunishSort();
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
                else
                {
                    GetTrainSort();
                    GetCheckSort();
                    GetCheckDetailSort();
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
                    GetMeetingSort();
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
                        }
                        else
                        {
                            this.txtYearComplexEmergencyNum.Text = complexEmergencyNum.ToString();
                        }
                        //专项应急预案
                        if (mr.YearSpecialEmergencyNum != 0)
                        {
                            this.txtYearSpecialEmergencyNum.Text = (mr.YearSpecialEmergencyNum + specialEmergencyNum).ToString();
                        }
                        else
                        {
                            this.txtYearSpecialEmergencyNum.Text = specialEmergencyNum.ToString();
                        }
                        //应急演练活动
                        if (mr.YearDrillRecordNum != 0)
                        {
                            this.txtYearDrillRecordNum.Text = (mr.YearDrillRecordNum + Convert.ToInt32(this.txtDrillRecordNum.Text.Trim())).ToString();
                        }
                        else
                        {
                            this.txtYearDrillRecordNum.Text = this.txtDrillRecordNum.Text;
                        }
                    }
                    else
                    {
                        this.txtYearComplexEmergencyNum.Text = complexEmergencyNum.ToString();
                        this.txtYearSpecialEmergencyNum.Text = specialEmergencyNum.ToString();
                        this.txtYearDrillRecordNum.Text = this.txtDrillRecordNum.Text;
                    }
                    GetRewardSort();
                    GetPunishSort();
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
                        this.txtYearRewardNum.Text = this.txtRewardNum.Text.Trim();
                        this.txtYearRewardMoney.Text = this.txtRewardMoney.Text.Trim();
                        this.txtYearPunishNum.Text = this.txtPunishNum.Text.Trim();
                        this.txtYearPunishMoney.Text = this.txtPunishMoney.Text.Trim();
                    }
                }
            }
        }
        #endregion

        #region 危险源辨识活动情况描述
        /// <summary>
        /// 危险源辨识活动情况描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewHazardSort_Click(object sender, EventArgs e)
        {
            jerqueSaveHazardList();
            Model.Manager_HazardSortC hazardSort = new Model.Manager_HazardSortC
            {
                HazardSortId = SQLHelper.GetNewID(typeof(Model.Manager_HazardSortC))
            };
            hazardSorts.Add(hazardSort);
            this.gvHazardSort.DataSource = hazardSorts;
            this.gvHazardSort.DataBind();
        }

        /// <summary>
        /// 检查并保存危险源集合
        /// </summary>
        private void jerqueSaveHazardList()
        {
            hazardSorts.Clear();
            JArray mergedData = gvHazardSort.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_HazardSortC hazardSort = new Model.Manager_HazardSortC
                {
                    HazardSortId = this.gvHazardSort.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    HazardName = values.Value<string>("HazardName").ToString(),
                    UnitAndArea = values.Value<string>("UnitAndArea").ToString(),
                    StationDef = values.Value<string>("StationDef").ToString(),
                    HandleWay = values.Value<string>("HandleWay").ToString()
                };
                hazardSorts.Add(hazardSort);
            }
        }

        protected void gvHazardSort_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveHazardList();
            string rowID = this.gvHazardSort.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in hazardSorts)
                {
                    if (item.HazardSortId == rowID)
                    {
                        hazardSorts.Remove(item);
                        break;
                    }
                }
                gvHazardSort.DataSource = hazardSorts;
                gvHazardSort.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region HSE培训
        /// <summary>
        /// 显示月报告HSE培训情况
        /// </summary>
        private void GetTrainSort()
        {
            List<Model.EduTrain_TrainRecord> trainings = BLL.EduTrain_TrainRecordService.GetTrainingsByTrainDate(startTime, endTime, this.ProjectId);
            if (trainings.Count > 0)
            {
                int i = 0;
                foreach (Model.EduTrain_TrainRecord t in trainings)
                {
                    Model.Manager_TrainSortC trainSort = new Model.Manager_TrainSortC
                    {
                        TrainSortId = SQLHelper.GetNewID(typeof(Model.Manager_TrainSortC)),
                        SortIndex = i
                    };
                    Model.Base_TrainType trainType = BLL.TrainTypeService.GetTrainTypeById(t.TrainTypeId);
                    if (trainType != null)
                    {
                        trainSort.TrainContent = trainType.TrainTypeName;
                    }
                    trainSort.TeachHour = t.TeachHour;
                    trainSort.TeachMan = t.TeachMan;
                    List<string> unitIds = t.UnitIds.Split(',').ToList();
                    if (unitIds.Count > 0)
                    {
                        string unitName = string.Empty;
                        foreach (string unitId in unitIds)
                        {
                            Model.Base_Unit u = BLL.UnitService.GetUnitByUnitId(unitId);
                            if (u != null)
                            {
                                unitName += u.UnitName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(unitName))
                        {
                            trainSort.UnitName = unitName.Substring(0, unitName.LastIndexOf(","));
                        }
                    }
                    trainSort.PersonNum = BLL.EduTrain_TrainRecordDetailService.GetTrainRecordDetailByTrainingId(t.TrainingId).Count();
                    trainSorts.Add(trainSort);
                    i++;
                }
            }
            this.gvTrainSort.DataSource = trainSorts;
            this.gvTrainSort.DataBind();
        }

        /// <summary>
        /// 增加HSE培训
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewTrainSort_Click(object sender, EventArgs e)
        {
            jerqueSaveTrainList();
            Model.Manager_TrainSortC trainSort = new Model.Manager_TrainSortC
            {
                TrainSortId = SQLHelper.GetNewID(typeof(Model.Manager_TrainSortC))
            };
            trainSorts.Add(trainSort);
            this.gvTrainSort.DataSource = trainSorts;
            this.gvTrainSort.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE培训集合
        /// </summary>
        private void jerqueSaveTrainList()
        {
            trainSorts.Clear();
            JArray mergedData = gvTrainSort.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_TrainSortC trainSort = new Model.Manager_TrainSortC
                {
                    TrainSortId = this.gvTrainSort.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    TrainContent = values.Value<string>("TrainContent").ToString(),
                    TeachHour = Funs.GetNewDecimalOrZero(values.Value<string>("TeachHour").ToString()),
                    TeachMan = values.Value<string>("TeachMan").ToString(),
                    UnitName = values.Value<string>("UnitName").ToString(),
                    PersonNum = Funs.GetNewIntOrZero(values.Value<string>("PersonNum").ToString())
                };
                trainSorts.Add(trainSort);
            }
        }

        protected void gvTrainSort_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveTrainList();
            string rowID = this.gvTrainSort.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in trainSorts)
                {
                    if (item.TrainSortId == rowID)
                    {
                        trainSorts.Remove(item);
                        break;
                    }
                }
                gvTrainSort.DataSource = trainSorts;
                gvTrainSort.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 增加培训活动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewTrainActivitySort_Click(object sender, EventArgs e)
        {
            jerqueSaveTrainActivityList();
            Model.Manager_TrainActivitySortC trainActivitySort = new Model.Manager_TrainActivitySortC
            {
                TrainActivitySortId = SQLHelper.GetNewID(typeof(Model.Manager_TrainActivitySortC))
            };
            trainActivitySorts.Add(trainActivitySort);
            this.gvTrainActivitySort.DataSource = trainActivitySorts;
            this.gvTrainActivitySort.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE培训活动集合
        /// </summary>
        private void jerqueSaveTrainActivityList()
        {
            trainActivitySorts.Clear();
            JArray mergedData = gvTrainActivitySort.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_TrainActivitySortC trainActivitySort = new Model.Manager_TrainActivitySortC
                {
                    TrainActivitySortId = this.gvTrainActivitySort.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    ActivityName = values.Value<string>("ActivityName").ToString(),
                    TrainDate = values.Value<string>("TrainDate").ToString(),
                    TrainEffect = values.Value<string>("TrainEffect").ToString()
                };
                trainActivitySorts.Add(trainActivitySort);
            }
        }

        protected void gvTrainActivitySort_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveTrainActivityList();
            string rowID = this.gvTrainActivitySort.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in trainActivitySorts)
                {
                    if (item.TrainActivitySortId == rowID)
                    {
                        trainActivitySorts.Remove(item);
                        break;
                    }
                }
                gvTrainActivitySort.DataSource = trainActivitySorts;
                gvTrainActivitySort.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region HSE检查
        /// <summary>
        /// 显示月报告HSE检查情况
        /// </summary>
        private void GetCheckSort()
        {
            int i = 0;
            Model.Manager_CheckSortC checkSort1 = new Model.Manager_CheckSortC
            {
                CheckSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckSortC)),
                SortIndex = i,
                CheckType = "日巡检",
                CheckNumber = BLL.Check_CheckDayService.GetCountByCheckTime(startTime, endTime, this.ProjectId),
                YearCheckNum = BLL.Check_CheckDayService.GetCountByCheckTime(yearStartTime, endTime, this.ProjectId),
                TotalCheckNum = BLL.Check_CheckDayService.GetCountByCheckTime(projectStartTime, endTime, this.ProjectId),
                ViolationNumber = BLL.Check_CheckDayService.GetIsOKViolationCountByCheckTime(startTime, endTime, this.ProjectId),
                YearViolationNum = BLL.Check_CheckDayService.GetIsOKViolationCountByCheckTime(yearStartTime, endTime, this.ProjectId)
            };
            checkSorts.Add(checkSort1);
            i++;
            Model.Manager_CheckSortC checkSort2 = new Model.Manager_CheckSortC
            {
                CheckSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckSortC)),
                SortIndex = i,
                CheckType = "联合检查",
                CheckNumber = BLL.Check_CheckColligationService.GetCountByCheckTime(startTime, endTime, this.ProjectId),
                YearCheckNum = BLL.Check_CheckColligationService.GetCountByCheckTime(yearStartTime, endTime, this.ProjectId),
                TotalCheckNum = BLL.Check_CheckColligationService.GetCountByCheckTime(projectStartTime, endTime, this.ProjectId),
                ViolationNumber = BLL.Check_CheckColligationService.GetIsOKViolationCountByCheckTime(startTime, endTime, this.ProjectId),
                YearViolationNum = BLL.Check_CheckColligationService.GetIsOKViolationCountByCheckTime(yearStartTime, endTime, this.ProjectId)
            };
            checkSorts.Add(checkSort2);
            i++;
            Model.Manager_CheckSortC checkSort3 = new Model.Manager_CheckSortC
            {
                CheckSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckSortC)),
                SortIndex = i,
                CheckType = "专项检查",
                CheckNumber = BLL.Check_CheckSpecialService.GetCountByCheckTime(startTime, endTime, this.ProjectId),
                YearCheckNum = BLL.Check_CheckSpecialService.GetCountByCheckTime(yearStartTime, endTime, this.ProjectId),
                TotalCheckNum = BLL.Check_CheckSpecialService.GetCountByCheckTime(projectStartTime, endTime, this.ProjectId),
                ViolationNumber = BLL.Check_CheckSpecialService.GetIsOKViolationCountByCheckTime(startTime, endTime, this.ProjectId),
                YearViolationNum = BLL.Check_CheckSpecialService.GetIsOKViolationCountByCheckTime(yearStartTime, endTime, this.ProjectId)
            };
            checkSorts.Add(checkSort3);
            i++;
            Model.Manager_CheckSortC checkSort4 = new Model.Manager_CheckSortC
            {
                CheckSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckSortC)),
                SortIndex = i,
                CheckType = "开工前检查",
                CheckNumber = BLL.Check_CheckWorkService.GetCountByCheckTime(startTime, endTime, this.ProjectId),
                YearCheckNum = BLL.Check_CheckWorkService.GetCountByCheckTime(yearStartTime, endTime, this.ProjectId),
                TotalCheckNum = BLL.Check_CheckWorkService.GetCountByCheckTime(projectStartTime, endTime, this.ProjectId),
                ViolationNumber = BLL.Check_CheckWorkService.GetIsOKViolationCountByCheckTime(startTime, endTime, this.ProjectId),
                YearViolationNum = BLL.Check_CheckWorkService.GetIsOKViolationCountByCheckTime(yearStartTime, endTime, this.ProjectId)
            };
            checkSorts.Add(checkSort4);
            i++;
            Model.Manager_CheckSortC checkSort5 = new Model.Manager_CheckSortC
            {
                CheckSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckSortC)),
                SortIndex = i,
                CheckType = "季节性/节假日检查",
                CheckNumber = BLL.Check_CheckHolidayService.GetCountByCheckTime(startTime, endTime, this.ProjectId),
                YearCheckNum = BLL.Check_CheckHolidayService.GetCountByCheckTime(yearStartTime, endTime, this.ProjectId),
                TotalCheckNum = BLL.Check_CheckHolidayService.GetCountByCheckTime(projectStartTime, endTime, this.ProjectId),
                ViolationNumber = BLL.Check_CheckHolidayService.GetIsOKViolationCountByCheckTime(startTime, endTime, this.ProjectId),
                YearViolationNum = BLL.Check_CheckHolidayService.GetIsOKViolationCountByCheckTime(yearStartTime, endTime, this.ProjectId)
            };
            checkSorts.Add(checkSort5);
            this.gvCheckSort.DataSource = checkSorts;
            this.gvCheckSort.DataBind();
        }

        /// <summary>
        /// 增加HSE检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewCheckSort_Click(object sender, EventArgs e)
        {
            jerqueSaveCheckList();
            Model.Manager_CheckSortC checkSort = new Model.Manager_CheckSortC
            {
                CheckSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckSortC))
            };
            checkSorts.Add(checkSort);
            this.gvCheckSort.DataSource = checkSorts;
            this.gvCheckSort.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE检查集合
        /// </summary>
        private void jerqueSaveCheckList()
        {
            checkSorts.Clear();
            JArray mergedData = gvCheckSort.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_CheckSortC checkSort = new Model.Manager_CheckSortC
                {
                    CheckSortId = this.gvCheckSort.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    CheckType = values.Value<string>("CheckType").ToString(),
                    CheckNumber = Funs.GetNewIntOrZero(values.Value<string>("CheckNumber").ToString()),
                    YearCheckNum = Funs.GetNewIntOrZero(values.Value<string>("YearCheckNum").ToString()),
                    TotalCheckNum = Funs.GetNewIntOrZero(values.Value<string>("TotalCheckNum").ToString()),
                    ViolationNumber = Funs.GetNewIntOrZero(values.Value<string>("ViolationNumber").ToString()),
                    YearViolationNum = Funs.GetNewIntOrZero(values.Value<string>("YearViolationNum").ToString())
                };
                checkSorts.Add(checkSort);
            }
        }

        protected void gvCheckSort_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveCheckList();
            string rowID = this.gvCheckSort.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in checkSorts)
                {
                    if (item.CheckSortId == rowID)
                    {
                        checkSorts.Remove(item);
                        break;
                    }
                }
                gvCheckSort.DataSource = checkSorts;
                gvCheckSort.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 显示月报告HSE检查明细情况
        /// </summary>
        private void GetCheckDetailSort()
        {
            int i = 0;
            Model.Manager_CheckDetailSortC checkDetailSort1 = new Model.Manager_CheckDetailSortC
            {
                CheckDetailSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckDetailSortC)),
                SortIndex = i,
                CheckType = "日巡检"
            };
            checkDetailSorts.Add(checkDetailSort1);
            i++;
            Model.Manager_CheckDetailSortC checkDetailSort2 = new Model.Manager_CheckDetailSortC
            {
                CheckDetailSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckDetailSortC)),
                SortIndex = i,
                CheckType = "周检/月检"
            };
            checkDetailSorts.Add(checkDetailSort2);
            i++;
            List<Model.Check_CheckSpecial> checkSpecials = BLL.Check_CheckSpecialService.GetListByCheckTime(startTime, endTime, this.ProjectId);
            if (checkSpecials.Count > 0)
            {
                foreach (Model.Check_CheckSpecial checkSpecial in checkSpecials)
                {
                    Model.Manager_CheckDetailSortC checkDetailSort = new Model.Manager_CheckDetailSortC
                    {
                        CheckDetailSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckDetailSortC)),
                        SortIndex = i
                    };
                    List<Model.Check_CheckSpecialDetail> details = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialId(checkSpecial.CheckSpecialId);
                    string str = string.Empty;
                    string checkStation = string.Empty;
                    if (details.Count > 0)
                    {
                        foreach (Model.Check_CheckSpecialDetail detail in details)
                        {
                            str += BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(detail.CheckItem) + ",";
                            checkStation += "隐患：" + detail.Unqualified + ";整改：" + detail.Suggestions + "。";
                        }
                    }
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = str.Substring(0, str.LastIndexOf(','));
                    }
                    checkDetailSort.CheckType = "专项检查(" + str + ")";
                    if (checkSpecial.CheckTime != null)
                    {
                        checkDetailSort.CheckTime = string.Format("{0:yyyy-MM-dd}", checkSpecial.CheckTime);
                    }
                    string unitNames = string.Empty;
                    if (!string.IsNullOrEmpty(checkSpecial.PartInUnits))
                    {
                        string[] strs = checkSpecial.PartInUnits.Split(',');
                        foreach (var item in strs)
                        {
                            Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(item);
                            if (unit != null)
                            {
                                unitNames += unit.UnitName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(unitNames))
                        {
                            unitNames = unitNames.Substring(0, unitNames.LastIndexOf(","));
                        }
                    }
                    checkDetailSort.JoinUnit = unitNames;
                    checkDetailSort.StateDef = checkStation;
                    checkDetailSorts.Add(checkDetailSort);
                    i++;
                }
            }
            this.gvCheckDetailSort.DataSource = checkDetailSorts;
            this.gvCheckDetailSort.DataBind();
        }

        /// <summary>
        /// 增加HSE检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewCheckDetailSort_Click(object sender, EventArgs e)
        {
            jerqueSaveCheckDetailList();
            Model.Manager_CheckDetailSortC checkDetailSort = new Model.Manager_CheckDetailSortC
            {
                CheckDetailSortId = SQLHelper.GetNewID(typeof(Model.Manager_CheckDetailSortC))
            };
            checkDetailSorts.Add(checkDetailSort);
            this.gvCheckDetailSort.DataSource = checkDetailSorts;
            this.gvCheckDetailSort.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE检查集合
        /// </summary>
        private void jerqueSaveCheckDetailList()
        {
            checkDetailSorts.Clear();
            JArray mergedData = gvCheckDetailSort.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_CheckDetailSortC checkDetailSort = new Model.Manager_CheckDetailSortC
                {
                    CheckDetailSortId = this.gvCheckDetailSort.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    CheckType = values.Value<string>("CheckType").ToString(),
                    CheckTime = values.Value<string>("CheckTime").ToString(),
                    JoinUnit = values.Value<string>("JoinUnit").ToString(),
                    StateDef = values.Value<string>("StateDef").ToString()
                };
                checkDetailSorts.Add(checkDetailSort);
            }
        }

        protected void gvCheckDetailSort_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveCheckDetailList();
            string rowID = this.gvCheckDetailSort.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in checkDetailSorts)
                {
                    if (item.CheckDetailSortId == rowID)
                    {
                        checkDetailSorts.Remove(item);
                        break;
                    }
                }
                gvCheckDetailSort.DataSource = checkDetailSorts;
                gvCheckDetailSort.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region HSE会议
        /// <summary>
        /// 显示月报告HSE会议情况
        /// </summary>
        private void GetMeetingSort()
        {
            int i = 0;
            List<Model.Meeting_WeekMeeting> weekMeetings = BLL.WeekMeetingService.GetMeetingListsByDate(startTime, endTime, this.ProjectId);
            if (weekMeetings.Count > 0)
            {
                Model.Manager_MeetingSortC meetingSort = new Model.Manager_MeetingSortC
                {
                    MeetingSortId = SQLHelper.GetNewID(typeof(Model.Manager_MeetingSortC)),
                    SortIndex = i,
                    MeetingType = "HSE周例会",
                    MeetingHours = weekMeetings.First().MeetingHours,
                    MeetingHostMan = weekMeetings.First().MeetingHostMan
                };
                string date = string.Empty;
                foreach (Model.Meeting_WeekMeeting m in weekMeetings)
                {
                    if (m.WeekMeetingDate != null)
                    {
                        DateTime d = Convert.ToDateTime(m.WeekMeetingDate);
                        date += d.Month + "." + d.Day + "/";
                    }
                }
                if (!string.IsNullOrEmpty(date))
                {
                    date = date.Substring(0, date.LastIndexOf('/'));
                }
                meetingSort.MeetingDate = date;
                meetingSort.AttentPerson = weekMeetings.First().AttentPerson;
                meetingSorts.Add(meetingSort);
            }
            i++;
            List<Model.Meeting_MonthMeeting> monthMeetings = BLL.MonthMeetingService.GetMeetingListsByDate(startTime, endTime, this.ProjectId);
            if (monthMeetings.Count > 0)
            {
                Model.Manager_MeetingSortC meetingSort = new Model.Manager_MeetingSortC
                {
                    MeetingSortId = SQLHelper.GetNewID(typeof(Model.Manager_MeetingSortC)),
                    SortIndex = i,
                    MeetingType = "HSE月例会",
                    MeetingHours = monthMeetings.First().MeetingHours,
                    MeetingHostMan = monthMeetings.First().MeetingHostMan
                };
                string date = string.Empty;
                if (monthMeetings.First().MonthMeetingDate != null)
                {
                    DateTime d = Convert.ToDateTime(monthMeetings.First().MonthMeetingDate);
                    date += d.Month + "." + d.Day;
                }
                meetingSort.MeetingDate = date;
                meetingSort.AttentPerson = monthMeetings.First().AttentPerson;
                meetingSorts.Add(meetingSort);
            }
            i++;
            List<Model.Meeting_SpecialMeeting> specialMeetings = BLL.SpecialMeetingService.GetMeetingListsByDate(startTime, endTime, this.ProjectId);
            if (specialMeetings.Count > 0)
            {
                foreach (Model.Meeting_SpecialMeeting item in specialMeetings)
                {
                    Model.Manager_MeetingSortC meetingSort = new Model.Manager_MeetingSortC
                    {
                        MeetingSortId = SQLHelper.GetNewID(typeof(Model.Manager_MeetingSortC)),
                        SortIndex = i,
                        MeetingType = "专题会议",
                        MeetingHours = item.MeetingHours,
                        MeetingHostMan = item.MeetingHostMan
                    };
                    string date = string.Empty;
                    if (item.SpecialMeetingDate != null)
                    {
                        DateTime d = Convert.ToDateTime(item.SpecialMeetingDate);
                        date += d.Month + "." + d.Day;
                    }
                    meetingSort.MeetingDate = date;
                    meetingSort.AttentPerson = item.AttentPerson;
                    meetingSorts.Add(meetingSort);
                    i++;
                }
            }
            List<Model.Meeting_AttendMeeting> attendMeetings = BLL.AttendMeetingService.GetMeetingListsByDate(startTime, endTime, this.ProjectId);
            if (attendMeetings.Count > 0)
            {
                foreach (Model.Meeting_AttendMeeting item in attendMeetings)
                {
                    Model.Manager_MeetingSortC meetingSort = new Model.Manager_MeetingSortC
                    {
                        MeetingSortId = SQLHelper.GetNewID(typeof(Model.Manager_MeetingSortC)),
                        SortIndex = i,
                        MeetingType = item.AttendMeetingName,
                        MeetingHours = item.MeetingHours,
                        MeetingHostMan = item.MeetingHostMan
                    };
                    string date = string.Empty;
                    if (item.AttendMeetingDate != null)
                    {
                        DateTime d = Convert.ToDateTime(item.AttendMeetingDate);
                        date += d.Month + "." + d.Day;
                    }
                    meetingSort.MeetingDate = date;
                    meetingSorts.Add(meetingSort);
                    i++;
                }
            }
            this.gvMeetingSort.DataSource = meetingSorts;
            this.gvMeetingSort.DataBind();
        }

        /// <summary>
        /// 增加HSE会议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewMeetingSort_Click(object sender, EventArgs e)
        {
            jerqueSaveMeetingList();
            Model.Manager_MeetingSortC meetingSort = new Model.Manager_MeetingSortC
            {
                MeetingSortId = SQLHelper.GetNewID(typeof(Model.Manager_MeetingSortC))
            };
            meetingSorts.Add(meetingSort);
            this.gvMeetingSort.DataSource = meetingSorts;
            this.gvMeetingSort.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE会议集合
        /// </summary>
        private void jerqueSaveMeetingList()
        {
            meetingSorts.Clear();
            JArray mergedData = gvMeetingSort.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_MeetingSortC meetingSort = new Model.Manager_MeetingSortC
                {
                    MeetingSortId = this.gvMeetingSort.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    MeetingType = values.Value<string>("MeetingType").ToString(),
                    MeetingHours = Funs.GetNewDecimalOrZero(values.Value<string>("MeetingHours").ToString()),
                    MeetingHostMan = values.Value<string>("MeetingHostMan").ToString(),
                    MeetingDate = values.Value<string>("MeetingDate").ToString(),
                    AttentPerson = values.Value<string>("AttentPerson").ToString(),
                    MainContent = values.Value<string>("MainContent").ToString()
                };
                meetingSorts.Add(meetingSort);
            }
        }

        protected void gvMeetingSort_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMeetingList();
            string rowID = this.gvMeetingSort.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in meetingSorts)
                {
                    if (item.MeetingSortId == rowID)
                    {
                        meetingSorts.Remove(item);
                        break;
                    }
                }
                gvMeetingSort.DataSource = meetingSorts;
                gvMeetingSort.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        #endregion

        #region HSE宣传活动

        private string StripHT(string strHtml)
        {
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            string strOutput = regex.Replace(strHtml, "");
            return strOutput;
        }
        #endregion

        #region HSE奖励
        /// <summary>
        /// 显示月报告HSE奖励情况
        /// </summary>
        private void GetRewardSort()
        {
            List<Model.Check_IncentiveNotice> rewardNoticeList = BLL.IncentiveNoticeService.GetIncentiveNoticeListsByDate(startTime, endTime, this.ProjectId);
            if (rewardNoticeList.Count > 0)
            {
                int i = 0;
                foreach (Model.Check_IncentiveNotice r in rewardNoticeList)
                {
                    Model.Manager_IncentiveSortC rewardSort = new Model.Manager_IncentiveSortC
                    {
                        IncentiveSortId = SQLHelper.GetNewID(typeof(Model.Manager_IncentiveSortC)),
                        SortIndex = i,
                        BigType = "1"
                    };
                    Model.Base_Unit u = BLL.UnitService.GetUnitByUnitId(r.UnitId);
                    if (u != null)
                    {
                        rewardSort.IncentiveUnit = u.UnitName;
                    }
                    rewardSort.IncentiveType = "现金";
                    if (r.IncentiveDate != null)
                    {
                        rewardSort.IncentiveDate = string.Format("{0:yyyy-MM-dd}", r.IncentiveDate);
                    }
                    if (r.IncentiveMoney != null)
                    {
                        rewardSort.IncentiveMoney = Convert.ToInt32(r.IncentiveMoney);
                    }
                    rewardSorts.Add(rewardSort);
                    i++;
                }
            }
            this.gvRewardSort.DataSource = rewardSorts;
            this.gvRewardSort.DataBind();
        }

        /// <summary>
        /// 增加HSE奖励
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewRewardSort_Click(object sender, EventArgs e)
        {
            jerqueSaveRewardList();
            Model.Manager_IncentiveSortC rewardSort = new Model.Manager_IncentiveSortC
            {
                IncentiveSortId = SQLHelper.GetNewID(typeof(Model.Manager_IncentiveSortC))
            };
            rewardSorts.Add(rewardSort);
            this.gvRewardSort.DataSource = rewardSorts;
            this.gvRewardSort.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE奖励集合
        /// </summary>
        private void jerqueSaveRewardList()
        {
            rewardSorts.Clear();
            JArray mergedData = gvRewardSort.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_IncentiveSortC rewardSort = new Model.Manager_IncentiveSortC
                {
                    IncentiveSortId = this.gvRewardSort.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    BigType = "1",
                    IncentiveUnit = values.Value<string>("IncentiveUnit").ToString(),
                    IncentiveType = values.Value<string>("IncentiveType").ToString(),
                    IncentiveDate = values.Value<string>("IncentiveDate").ToString(),
                    IncentiveMoney = Funs.GetNewIntOrZero(values.Value<string>("IncentiveMoney").ToString())
                };
                rewardSorts.Add(rewardSort);
            }
        }

        protected void gvRewardSort_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveRewardList();
            string rowID = this.gvRewardSort.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in rewardSorts)
                {
                    if (item.IncentiveSortId == rowID)
                    {
                        rewardSorts.Remove(item);
                        break;
                    }
                }
                gvRewardSort.DataSource = rewardSorts;
                gvRewardSort.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region HSE处罚
        /// <summary>
        /// 显示月报告HSE处罚情况
        /// </summary>
        private void GetPunishSort()
        {
            List<Model.Check_PunishNotice> punishNoticeList = BLL.PunishNoticeService.GetPunishNoticeListsByDate(startTime, endTime, this.ProjectId);
            if (punishNoticeList.Count > 0)
            {
                int i = 0;
                foreach (Model.Check_PunishNotice p in punishNoticeList)
                {
                    Model.Manager_IncentiveSortC punishSort = new Model.Manager_IncentiveSortC
                    {
                        IncentiveSortId = SQLHelper.GetNewID(typeof(Model.Manager_IncentiveSortC)),
                        SortIndex = i,
                        BigType = "2"
                    };
                    Model.Base_Unit u = BLL.UnitService.GetUnitByUnitId(p.UnitId);
                    if (u != null)
                    {
                        punishSort.IncentiveUnit = u.UnitName;
                    }
                    if (p.PunishNoticeDate != null)
                    {
                        punishSort.IncentiveDate = string.Format("{0:yyyy-MM-dd}", p.PunishNoticeDate);
                    }
                    if (p.PunishMoney != null)
                    {
                        punishSort.IncentiveMoney = Convert.ToInt32(p.PunishMoney);
                    }
                    punishSort.IncentiveReason = p.IncentiveReason;
                    punishSort.IncentiveBasis = p.BasicItem;
                    punishSorts.Add(punishSort);
                    i++;
                }
            }
            this.gvPunishSort.DataSource = punishSorts;
            this.gvPunishSort.DataBind();
        }

        /// <summary>
        /// 增加HSE处罚
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewPunishSort_Click(object sender, EventArgs e)
        {
            jerqueSavePunishList();
            Model.Manager_IncentiveSortC punishSort = new Model.Manager_IncentiveSortC
            {
                IncentiveSortId = SQLHelper.GetNewID(typeof(Model.Manager_IncentiveSortC))
            };
            punishSorts.Add(punishSort);
            this.gvPunishSort.DataSource = punishSorts;
            this.gvPunishSort.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE处罚集合
        /// </summary>
        private void jerqueSavePunishList()
        {
            punishSorts.Clear();
            JArray mergedData = gvPunishSort.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_IncentiveSortC punishSort = new Model.Manager_IncentiveSortC
                {
                    IncentiveSortId = this.gvPunishSort.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    BigType = "2",
                    IncentiveUnit = values.Value<string>("IncentiveUnit").ToString(),
                    IncentiveDate = values.Value<string>("IncentiveDate").ToString(),
                    IncentiveMoney = Funs.GetNewIntOrZero(values.Value<string>("IncentiveMoney").ToString()),
                    IncentiveReason = values.Value<string>("IncentiveReason").ToString(),
                    IncentiveBasis = values.Value<string>("IncentiveBasis").ToString()
                };
                punishSorts.Add(punishSort);
            }
        }

        protected void gvPunishSort_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSavePunishList();
            string rowID = this.gvPunishSort.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in punishSorts)
                {
                    if (item.IncentiveSortId == rowID)
                    {
                        punishSorts.Remove(item);
                        break;
                    }
                }
                gvPunishSort.DataSource = punishSorts;
                gvPunishSort.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 其他HSE管理情况
        /// <summary>
        /// 增加其他HSE管理情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewOtherManagement_Click(object sender, EventArgs e)
        {
            jerqueSaveOtherManagementList();
            Model.Manager_Month_OtherManagementC otherManagementSort = new Model.Manager_Month_OtherManagementC
            {
                OtherManagementId = SQLHelper.GetNewID(typeof(Model.Manager_Month_OtherManagementC))
            };
            otherManagements.Add(otherManagementSort);
            this.gvOtherManagement.DataSource = otherManagements;
            this.gvOtherManagement.DataBind();
        }

        /// <summary>
        /// 检查并保存其他HSE管理情况集合
        /// </summary>
        private void jerqueSaveOtherManagementList()
        {
            otherManagements.Clear();
            JArray mergedData = gvOtherManagement.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_Month_OtherManagementC otherManagementSort = new Model.Manager_Month_OtherManagementC
                {
                    OtherManagementId = this.gvOtherManagement.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    ManagementDes = values.Value<string>("ManagementDes").ToString()
                };
                otherManagements.Add(otherManagementSort);
            }
        }

        protected void gvOtherManagement_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveOtherManagementList();
            string rowID = this.gvOtherManagement.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in otherManagements)
                {
                    if (item.OtherManagementId == rowID)
                    {
                        otherManagements.Remove(item);
                        break;
                    }
                }
                gvOtherManagement.DataSource = otherManagements;
                gvOtherManagement.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Manager_MonthReportC oldMonthReport = BLL.MonthReportCService.GetMonthReportByMonths(Convert.ToDateTime(Request.Params["months"]), this.CurrUser.LoginProjectId);
            if (oldMonthReport != null)
            {
                oldMonthReport.MainActivitiesDef = this.txtMainActivitiesDef.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtMeetingNum.Text.Trim()))
                {
                    oldMonthReport.MeetingNum = Convert.ToInt32(this.txtMeetingNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearMeetingNum.Text.Trim()))
                {
                    oldMonthReport.YearMeetingNum = Convert.ToInt32(this.txtYearMeetingNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtComplexEmergencyNum.Text.Trim()))
                {
                    oldMonthReport.ComplexEmergencyNum = Convert.ToInt32(this.txtComplexEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearComplexEmergencyNum.Text.Trim()))
                {
                    oldMonthReport.YearComplexEmergencyNum = Convert.ToInt32(this.txtYearComplexEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtTotalComplexEmergencyNum.Text.Trim()))
                {
                    oldMonthReport.TotalComplexEmergencyNum = Convert.ToInt32(this.txtTotalComplexEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtSpecialEmergencyNum.Text.Trim()))
                {
                    oldMonthReport.SpecialEmergencyNum = Convert.ToInt32(this.txtSpecialEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearSpecialEmergencyNum.Text.Trim()))
                {
                    oldMonthReport.YearSpecialEmergencyNum = Convert.ToInt32(this.txtYearSpecialEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtTotalSpecialEmergencyNum.Text.Trim()))
                {
                    oldMonthReport.TotalSpecialEmergencyNum = Convert.ToInt32(this.txtTotalSpecialEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtDrillRecordNum.Text.Trim()))
                {
                    oldMonthReport.DrillRecordNum = Convert.ToInt32(this.txtDrillRecordNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearDrillRecordNum.Text.Trim()))
                {
                    oldMonthReport.YearDrillRecordNum = Convert.ToInt32(this.txtYearDrillRecordNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtTotalDrillRecordNum.Text.Trim()))
                {
                    oldMonthReport.TotalDrillRecordNum = Convert.ToInt32(this.txtTotalDrillRecordNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtRewardNum.Text.Trim()))
                {
                    oldMonthReport.RewardNum = Convert.ToInt32(this.txtRewardNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearRewardNum.Text.Trim()))
                {
                    oldMonthReport.YearRewardNum = Convert.ToInt32(this.txtYearRewardNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtRewardMoney.Text.Trim()))
                {
                    oldMonthReport.RewardMoney = Convert.ToInt32(this.txtRewardMoney.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearRewardMoney.Text.Trim()))
                {
                    oldMonthReport.YearRewardMoney = Convert.ToInt32(this.txtYearRewardMoney.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtPunishNum.Text.Trim()))
                {
                    oldMonthReport.PunishNum = Convert.ToInt32(this.txtPunishNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearPunishNum.Text.Trim()))
                {
                    oldMonthReport.YearPunishNum = Convert.ToInt32(this.txtYearPunishNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtPunishMoney.Text.Trim()))
                {
                    oldMonthReport.PunishMoney = Convert.ToInt32(this.txtPunishMoney.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearPunishMoney.Text.Trim()))
                {
                    oldMonthReport.YearPunishMoney = Convert.ToInt32(this.txtYearPunishMoney.Text.Trim());
                }
                oldMonthReport.LicenseRemark = this.txtLicenseRemark.Text.Trim();
                oldMonthReport.EmergencyManagementWorkDef = this.txtEmergencyManagementWorkDef.Text.Trim();
                oldMonthReport.EquipmentRemark = this.txtEquipmentRemark.Text.Trim();
                BLL.MonthReportCService.UpdateMonthReport(oldMonthReport);
                OperateHazardSort(MonthReportId);
                OperateTrainSort(MonthReportId);
                OperateTrainActivitySort(MonthReportId);
                OperateCheckSort(MonthReportId);
                OperateCheckDetailSort(MonthReportId);
                OperateMeetingSort(MonthReportId);
                BLL.IncentiveSortCService.DeleteIncentiveSortsByMonthReportId(MonthReportId);
                OperateRewardSort(MonthReportId);
                OperatePunishSort(MonthReportId);
                OperateOtherManagementSort(MonthReportId);
                BLL.LogService.AddSys_Log(this.CurrUser, oldMonthReport.MonthReportCode, oldMonthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnModify);
            }
            else
            {
                Model.Manager_MonthReportC monthReport = new Model.Manager_MonthReportC();
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MonthReportC));
                monthReport.MonthReportId = newKeyID;
                monthReport.ProjectId = this.CurrUser.LoginProjectId;
                this.MonthReportId = newKeyID;
                monthReport.MonthReportCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthCMenuId, this.ProjectId, this.CurrUser.UnitId);
                monthReport.Months = Funs.GetNewDateTime(Request.Params["months"]);
                monthReport.ReportMan = this.CurrUser.UserId;
                monthReport.MonthReportDate = DateTime.Now;
                monthReport.MainActivitiesDef = this.txtMainActivitiesDef.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtMeetingNum.Text.Trim()))
                {
                    monthReport.MeetingNum = Convert.ToInt32(this.txtMeetingNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearMeetingNum.Text.Trim()))
                {
                    monthReport.YearMeetingNum = Convert.ToInt32(this.txtYearMeetingNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtComplexEmergencyNum.Text.Trim()))
                {
                    monthReport.ComplexEmergencyNum = Convert.ToInt32(this.txtComplexEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearComplexEmergencyNum.Text.Trim()))
                {
                    monthReport.YearComplexEmergencyNum = Convert.ToInt32(this.txtYearComplexEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtTotalComplexEmergencyNum.Text.Trim()))
                {
                    monthReport.TotalComplexEmergencyNum = Convert.ToInt32(this.txtTotalComplexEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtSpecialEmergencyNum.Text.Trim()))
                {
                    monthReport.SpecialEmergencyNum = Convert.ToInt32(this.txtSpecialEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearSpecialEmergencyNum.Text.Trim()))
                {
                    monthReport.YearSpecialEmergencyNum = Convert.ToInt32(this.txtYearSpecialEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtTotalSpecialEmergencyNum.Text.Trim()))
                {
                    monthReport.TotalSpecialEmergencyNum = Convert.ToInt32(this.txtTotalSpecialEmergencyNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtDrillRecordNum.Text.Trim()))
                {
                    monthReport.DrillRecordNum = Convert.ToInt32(this.txtDrillRecordNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearDrillRecordNum.Text.Trim()))
                {
                    monthReport.YearDrillRecordNum = Convert.ToInt32(this.txtYearDrillRecordNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtTotalDrillRecordNum.Text.Trim()))
                {
                    monthReport.TotalDrillRecordNum = Convert.ToInt32(this.txtTotalDrillRecordNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtRewardNum.Text.Trim()))
                {
                    monthReport.RewardNum = Convert.ToInt32(this.txtRewardNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearRewardNum.Text.Trim()))
                {
                    monthReport.YearRewardNum = Convert.ToInt32(this.txtYearRewardNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtRewardMoney.Text.Trim()))
                {
                    monthReport.RewardMoney = Convert.ToInt32(this.txtRewardMoney.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearRewardMoney.Text.Trim()))
                {
                    monthReport.YearRewardMoney = Convert.ToInt32(this.txtYearRewardMoney.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtPunishNum.Text.Trim()))
                {
                    monthReport.PunishNum = Convert.ToInt32(this.txtPunishNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearPunishNum.Text.Trim()))
                {
                    monthReport.YearPunishNum = Convert.ToInt32(this.txtYearPunishNum.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtPunishMoney.Text.Trim()))
                {
                    monthReport.PunishMoney = Convert.ToInt32(this.txtPunishMoney.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtYearPunishMoney.Text.Trim()))
                {
                    monthReport.YearPunishMoney = Convert.ToInt32(this.txtYearPunishMoney.Text.Trim());
                }
                monthReport.EmergencyManagementWorkDef = this.txtEmergencyManagementWorkDef.Text.Trim();
                monthReport.LicenseRemark = this.txtLicenseRemark.Text.Trim();
                monthReport.EquipmentRemark = this.txtEquipmentRemark.Text.Trim();
                BLL.MonthReportCService.AddMonthReport(monthReport);
                OperateHazardSort(newKeyID);
                OperateTrainSort(newKeyID);
                OperateTrainActivitySort(newKeyID);
                OperateCheckSort(newKeyID);
                OperateCheckDetailSort(newKeyID);
                OperateMeetingSort(newKeyID);
                BLL.IncentiveSortCService.DeleteIncentiveSortsByMonthReportId(newKeyID);
                OperateRewardSort(newKeyID);
                OperatePunishSort(newKeyID);
                OperateOtherManagementSort(newKeyID);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnAdd);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 操作月报告危险源情况
        /// </summary>
        private void OperateHazardSort(string monthReportId)
        {
            BLL.HazardSortCService.DeleteHazardSortsByMonthReportId(monthReportId);
            jerqueSaveHazardList();
            foreach (Model.Manager_HazardSortC hazardSort in hazardSorts)
            {
                hazardSort.MonthReportId = monthReportId;
                BLL.HazardSortCService.AddHazardSort(hazardSort);
            }
        }

        /// <summary>
        /// 操作月报告HSE培训情况
        /// </summary>
        private void OperateTrainSort(string monthReportId)
        {
            BLL.TrainSortCService.DeleteTrainSortsByMonthReportId(monthReportId);
            jerqueSaveTrainList();
            foreach (Model.Manager_TrainSortC trainSort in trainSorts)
            {
                trainSort.MonthReportId = monthReportId;
                BLL.TrainSortCService.AddTrainSort(trainSort);
            }
        }

        /// <summary>
        /// 操作月报告HSE培训活动情况
        /// </summary>
        private void OperateTrainActivitySort(string monthReportId)
        {
            BLL.TrainActivitySortCService.DeleteTrainActivitySortsByMonthReportId(monthReportId);
            jerqueSaveTrainActivityList();
            foreach (Model.Manager_TrainActivitySortC trainActivitySort in trainActivitySorts)
            {
                trainActivitySort.MonthReportId = monthReportId;
                BLL.TrainActivitySortCService.AddTrainActivitySort(trainActivitySort);
            }
        }

        /// <summary>
        /// 操作月报告HSE检查情况
        /// </summary>
        private void OperateCheckSort(string monthReportId)
        {
            BLL.CheckSortCService.DeleteCheckSortsByMonthReportId(monthReportId);
            jerqueSaveCheckList();
            foreach (Model.Manager_CheckSortC checkSort in checkSorts)
            {
                checkSort.MonthReportId = monthReportId;
                BLL.CheckSortCService.AddCheckSort(checkSort);
            }
        }

        /// <summary>
        /// 操作月报告HSE检查明细情况
        /// </summary>
        private void OperateCheckDetailSort(string monthReportId)
        {
            BLL.CheckDetailSortCService.DeleteCheckDetailSortsByMonthReportId(monthReportId);
            jerqueSaveCheckDetailList();
            foreach (Model.Manager_CheckDetailSortC checkDetailSort in checkDetailSorts)
            {
                checkDetailSort.MonthReportId = monthReportId;
                BLL.CheckDetailSortCService.AddCheckDetailSort(checkDetailSort);
            }
        }

        /// <summary>
        /// 操作月报告HSE会议情况
        /// </summary>
        private void OperateMeetingSort(string monthReportId)
        {
            BLL.MeetingSortCService.DeleteMeetingSortsByMonthReportId(monthReportId);
            jerqueSaveMeetingList();
            foreach (Model.Manager_MeetingSortC meetingSort in meetingSorts)
            {
                meetingSort.MonthReportId = monthReportId;
                BLL.MeetingSortCService.AddMeetingSort(meetingSort);
            }
        }

        /// <summary>
        /// 操作月报告HSE奖励情况
        /// </summary>
        private void OperateRewardSort(string monthReportId)
        {
            jerqueSaveRewardList();
            foreach (Model.Manager_IncentiveSortC rewardSort in rewardSorts)
            {
                rewardSort.MonthReportId = monthReportId;
                BLL.IncentiveSortCService.AddIncentiveSort(rewardSort);
            }
        }

        /// <summary>
        /// 操作月报告HSE处罚情况
        /// </summary>
        private void OperatePunishSort(string monthReportId)
        {
            jerqueSavePunishList();
            foreach (Model.Manager_IncentiveSortC punishSort in punishSorts)
            {
                punishSort.MonthReportId = monthReportId;
                BLL.IncentiveSortCService.AddIncentiveSort(punishSort);
            }
        }

        /// <summary>
        /// 4.10 HSE现场其他管理情况
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateOtherManagementSort(string monthReportId)
        {
            BLL.OtherManagementCService.DeleteOtherManagementByMonthReportId(monthReportId);
            jerqueSaveOtherManagementList();
            foreach (Model.Manager_Month_OtherManagementC otherManagement in otherManagements)
            {
                otherManagement.MonthReportId = monthReportId;
                BLL.OtherManagementCService.AddOtherManagement(otherManagement);
            }
        }
        #endregion
    }
}