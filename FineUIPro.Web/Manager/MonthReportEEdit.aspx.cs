using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportEEdit : PageBase
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
        /// 月报告查主键
        /// </summary>
        public string LastMonthReportId
        {
            get
            {
                return (string)ViewState["LastMonthReportId"];
            }
            set
            {
                ViewState["LastMonthReportId"] = value;
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
        #endregion

        private static DateTime startTime;

        private static DateTime endTime;

        private static DateTime yearStartTime;

        private static DateTime yearEndTime;

        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.MonthReportId = Request.Params["monthReportId"];
                this.txtReportMan.Text = this.CurrUser.UserName;
                this.ProjectId = this.CurrUser.LoginProjectId;
                //1.项目情况
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    this.txtProjectName.Text = project.ProjectName;
                    string startDate = string.Empty, endDate = string.Empty;
                    if (project.StartDate != null)
                    {
                        startDate = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                    }
                    if (project.EndDate != null)
                    {
                        endDate = string.Format("{0:yyyy-MM-dd}", project.EndDate);
                    }
                    this.txtStartEndDate.Text = startDate + "/" + endDate;
                    ///项目经理
                    var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == this.ProjectId && x.RoleId == BLL.Const.ProjectManager);
                    if (m != null)
                    {
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(m.UserId);
                        if (user != null)
                        {
                            this.txtProjectManagerName.Text = user.UserName;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(this.MonthReportId))
                {
                    Model.Manager_MonthReportE monthReport = BLL.MonthReportEService.GetMonthReportByMonthReportId(MonthReportId);
                    if (monthReport != null)
                    {
                        startTime = Convert.ToDateTime(monthReport.Months);
                        endTime = startTime.AddMonths(1);
                        this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthReportId);
                        this.txtMonthReportCode.Text = monthReport.MonthReportCode;
                        if (monthReport.Months != null)
                        {
                            this.txtMonths.Text = string.Format("{0:yyyy-MM}", monthReport.Months);
                        }
                        this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        this.txtCountryCities.Text = monthReport.CountryCities;
                        this.txtContractType.Text = monthReport.ContractType;
                        if (monthReport.ContractAmount != null)
                        {
                            this.txtContractAmount.Text = monthReport.ContractAmount.ToString();
                        }
                        this.txtThisMajorWork.Text = monthReport.ThisMajorWork;
                        this.txtNextMajorWork.Text = monthReport.NextMajorWork;
                        if (monthReport.ThisIncome != null)
                        {
                            this.txtThisIncome.Text = monthReport.ThisIncome.ToString();
                        }
                        if (monthReport.YearIncome != null)
                        {
                            this.txtYearIncome.Text = monthReport.YearIncome.ToString();
                        }
                        if (monthReport.TotalIncome != null)
                        {
                            this.txtTotalIncome.Text = monthReport.TotalIncome.ToString();
                        }
                        this.txtThisImageProgress.Text = monthReport.ThisImageProgress;
                        this.txtYearImageProgress.Text = monthReport.YearImageProgress;
                        this.txtTotalImageProgress.Text = monthReport.TotalImageProgress;
                        if (monthReport.YearPersonNum != null)
                        {
                            this.txtYearPersonNum.Text = monthReport.YearPersonNum.ToString();
                        }
                        if (monthReport.TotalPersonNum != null)
                        {
                            this.txtTotalPersonNum.Text = monthReport.TotalPersonNum.ToString();
                        }
                        if (monthReport.ThisForeignPersonNum != null)
                        {
                            this.txtThisForeignPersonNum.Text = monthReport.ThisForeignPersonNum.ToString();
                        }
                        if (monthReport.YearForeignPersonNum != null)
                        {
                            this.txtYearForeignPersonNum.Text = monthReport.YearForeignPersonNum.ToString();
                        }
                        if (monthReport.TotalForeignPersonNum != null)
                        {
                            this.txtTotalForeignPersonNum.Text = monthReport.TotalForeignPersonNum.ToString();
                        }
                        this.txtProjectManagerPhone.Text = monthReport.ProjectManagerPhone;
                        this.txtHSEManagerName.Text = monthReport.HSEManagerName;
                        this.txtHSEManagerPhone.Text = monthReport.HSEManagerPhone;
                    }
                }
                else
                {
                    this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthEMenuId, this.ProjectId, this.CurrUser.UnitId);
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    int day = DateTime.Now.Day;
                    if (day > 5)
                    {
                        this.txtMonths.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
                        this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        startTime = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "1");
                        endTime = startTime.AddMonths(1);
                    }
                    else
                    {
                        if (month == 1)
                        {
                            this.txtMonths.Text = (DateTime.Now.Year - 1).ToString() + "-12"; ;
                        }
                        else
                        {
                            this.txtMonths.Text = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Month - 1).ToString();
                        }

                        this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        startTime = Convert.ToDateTime(this.txtMonths.Text + "-" + "1");
                        endTime = startTime.AddMonths(1);
                    }
                }
                yearStartTime = Convert.ToDateTime(startTime.Year.ToString() + "-1-1");
                yearEndTime = yearStartTime.AddYears(1);
                //本月现场员工总数
                this.txtThisPersonNum.Text = BLL.PersonService.GetPersonList(this.ProjectId).Count.ToString();
                //本月HSSE教育培训（人/次）
                var monthTrainings = from x in Funs.DB.EduTrain_TrainRecord where x.ProjectId == this.CurrUser.LoginProjectId && x.TrainStartDate >= startTime && x.TrainStartDate < endTime && x.States == BLL.Const.State_2 select x;
                this.txtThisTrainPersonNum.Text = (from x in monthTrainings
                                                   join y in Funs.DB.EduTrain_TrainRecordDetail
                                                   on x.TrainingId equals y.TrainingId
                                                   select y).Count().ToString();
                //本年累计HSSE教育培训（人/次）
                var yearTrainings = from x in Funs.DB.EduTrain_TrainRecord where x.ProjectId == this.CurrUser.LoginProjectId && x.TrainStartDate >= yearStartTime && x.TrainStartDate < yearEndTime && x.States == BLL.Const.State_2 select x;
                this.txtYearTrainPersonNum.Text = (from x in yearTrainings
                                                   join y in Funs.DB.EduTrain_TrainRecordDetail
                                                   on x.TrainingId equals y.TrainingId
                                                   select y).Count().ToString();
                //自开工累计HSSE教育培训（人/次）
                var totalTrainings = from x in Funs.DB.EduTrain_TrainRecord where x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2 select x;
                this.txtTotalTrainPersonNum.Text = (from x in totalTrainings
                                                    join y in Funs.DB.EduTrain_TrainRecordDetail
                                                    on x.TrainingId equals y.TrainingId
                                                    select y).Count().ToString();
                //本月HSSE检查（次）
                int monthCheck1 = BLL.Check_CheckDayService.GetCountByCheckTime(startTime, endTime, this.CurrUser.LoginProjectId);
                int monthCheck2 = BLL.Check_CheckColligationService.GetCountByCheckTimeAndCheckType(startTime, endTime, this.CurrUser.LoginProjectId, "0");
                int monthCheck3 = BLL.Check_CheckColligationService.GetCountByCheckTimeAndCheckType(startTime, endTime, this.CurrUser.LoginProjectId, "1");
                int monthCheck4 = BLL.Check_CheckSpecialService.GetCountByCheckTime(startTime, endTime, this.CurrUser.LoginProjectId);
                this.txtThisCheckNum.Text = (monthCheck1 + monthCheck2 + monthCheck3 + monthCheck4).ToString();
                //本年累计HSSE检查（次）
                int yearCheck1 = BLL.Check_CheckDayService.GetCountByCheckTime(yearStartTime, yearEndTime, this.CurrUser.LoginProjectId);
                int yearCheck2 = BLL.Check_CheckColligationService.GetCountByCheckTimeAndCheckType(yearStartTime, yearEndTime, this.CurrUser.LoginProjectId, "0");
                int yearCheck3 = BLL.Check_CheckColligationService.GetCountByCheckTimeAndCheckType(yearStartTime, yearEndTime, this.CurrUser.LoginProjectId, "1");
                int yearCheck4 = BLL.Check_CheckSpecialService.GetCountByCheckTime(yearStartTime, yearEndTime, this.CurrUser.LoginProjectId);
                this.txtYearCheckNum.Text = (yearCheck1 + yearCheck2 + yearCheck3 + yearCheck4).ToString();
                //自开工累计HSSE检查（次）
                int totalCheck1 = (from x in Funs.DB.Check_CheckDay where x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2 select x).Count();
                int totalCheck2 = (from x in Funs.DB.Check_CheckColligation where x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2 select x).Count();
                int totalCheck3 = (from x in Funs.DB.Check_CheckSpecial where x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2 select x).Count();
                this.txtTotalCheckNum.Text = (totalCheck1 + totalCheck2 + totalCheck3).ToString();
                //本月HSSE隐患排查治理（项）
                int monthViolationNumber1 = (from x in Funs.DB.Check_CheckDay
                                             join y in Funs.DB.Check_CheckDayDetail
                                             on x.CheckDayId equals y.CheckDayId
                                             where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                             select y).Count();
                int monthViolationNumber2 = (from x in Funs.DB.Check_CheckColligation
                                             join y in Funs.DB.Check_CheckColligationDetail
                                             on x.CheckColligationId equals y.CheckColligationId
                                             where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                             select y).Count();
                int monthViolationNumber3 = (from x in Funs.DB.Check_CheckSpecial
                                             join y in Funs.DB.Check_CheckSpecialDetail
                                             on x.CheckSpecialId equals y.CheckSpecialId
                                             where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                             select y).Count();
                this.txtThisViolationNum.Text = (monthViolationNumber1 + monthViolationNumber2 + monthViolationNumber3).ToString();
                //本年累计HSSE隐患排查治理（项）
                int yearViolationNumber1 = (from x in Funs.DB.Check_CheckDay
                                            join y in Funs.DB.Check_CheckDayDetail
                                            on x.CheckDayId equals y.CheckDayId
                                            where x.CheckTime >= yearStartTime && x.CheckTime < yearEndTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                            select y).Count();
                int yearViolationNumber2 = (from x in Funs.DB.Check_CheckColligation
                                            join y in Funs.DB.Check_CheckColligationDetail
                                            on x.CheckColligationId equals y.CheckColligationId
                                            where x.CheckTime >= yearStartTime && x.CheckTime < yearEndTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                            select y).Count();
                int yearViolationNumber3 = (from x in Funs.DB.Check_CheckSpecial
                                            join y in Funs.DB.Check_CheckSpecialDetail
                                            on x.CheckSpecialId equals y.CheckSpecialId
                                            where x.CheckTime >= yearStartTime && x.CheckTime < yearEndTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                            select y).Count();
                this.txtYearViolationNum.Text = (yearViolationNumber1 + yearViolationNumber2 + yearViolationNumber3).ToString();
                //自开工累计HSSE隐患排查治理（项）
                int totalViolationNumber1 = (from x in Funs.DB.Check_CheckDay
                                             join y in Funs.DB.Check_CheckDayDetail
                                             on x.CheckDayId equals y.CheckDayId
                                             where x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                             select y).Count();
                int totalViolationNumber2 = (from x in Funs.DB.Check_CheckColligation
                                             join y in Funs.DB.Check_CheckColligationDetail
                                             on x.CheckColligationId equals y.CheckColligationId
                                             where x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                             select y).Count();
                int totalViolationNumber3 = (from x in Funs.DB.Check_CheckSpecial
                                             join y in Funs.DB.Check_CheckSpecialDetail
                                             on x.CheckSpecialId equals y.CheckSpecialId
                                             where x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                             select y).Count();
                this.txtTotalViolationNum.Text = (totalViolationNumber1 + totalViolationNumber2 + totalViolationNumber3).ToString();
                //本月HSSE投入
                decimal monthRealCostA = BLL.CostSmallDetailItemService.GetCostDetailsByCostType(this.CurrUser.LoginProjectId, startTime, endTime, "A");
                decimal monthRealCostB = BLL.CostSmallDetailItemService.GetCostDetailsByCostType(this.CurrUser.LoginProjectId, startTime, endTime, "B");
                this.txtThisInvestment.Text = (monthRealCostA + monthRealCostB).ToString();
                //本年累计HSSE投入
                decimal yearRealCostA = BLL.CostSmallDetailItemService.GetCostDetailsByCostType(this.CurrUser.LoginProjectId, yearStartTime, yearEndTime, "A");
                decimal yearRealCostB = BLL.CostSmallDetailItemService.GetCostDetailsByCostType(this.CurrUser.LoginProjectId, yearStartTime, yearEndTime, "B");
                this.txtYearInvestment.Text = (yearRealCostA + yearRealCostB).ToString();
                //自开工累计HSSE投入
                decimal totalRealCostA = BLL.CostSmallDetailItemService.GetTotalCostDetailsByCostType(this.CurrUser.LoginProjectId, "A");
                decimal totalRealCostB = BLL.CostSmallDetailItemService.GetTotalCostDetailsByCostType(this.CurrUser.LoginProjectId, "B");
                this.txtTotalInvestment.Text = (totalRealCostA + totalRealCostB).ToString();
                //本月HSSE奖励
                this.txtThisReward.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoneyByTime(startTime, endTime, this.CurrUser.LoginProjectId));
                //本年累计HSSE奖励
                this.txtYearReward.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoneyByTime(yearStartTime, yearEndTime, this.CurrUser.LoginProjectId));
                //自开工累计HSSE奖励
                this.txtTotalReward.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoney(this.CurrUser.LoginProjectId));
                //本月HSSE处罚
                this.txtThisPunish.Text = string.Format("{0:N2}", BLL.PunishNoticeService.GetSumMoneyByTime(startTime, endTime, this.CurrUser.LoginProjectId));
                //本年累计HSSE处罚
                this.txtYearPunish.Text = string.Format("{0:N2}", BLL.PunishNoticeService.GetSumMoneyByTime(yearStartTime, yearEndTime, this.CurrUser.LoginProjectId));
                //自开工累计HSSE处罚
                this.txtTotalPunish.Text = string.Format("{0:N2}", BLL.PunishNoticeService.GetSumMoney(this.CurrUser.LoginProjectId));
                //本月HSSE应急演练（次）
                this.txtThisEmergencyDrillNum.Text = BLL.DrillRecordListService.GetCountByDate2(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
                //本年累计HSSE应急演练（次）
                this.txtYearEmergencyDrillNum.Text = BLL.DrillRecordListService.GetCountByDate2(yearStartTime, yearEndTime, this.CurrUser.LoginProjectId).ToString();
                //自开工累计HSSE应急演练（次）
                this.txtTotalEmergencyDrillNum.Text = BLL.DrillRecordListService.GetCount(this.CurrUser.LoginProjectId).ToString();
                //HSE工时
                int? monthHours = 0, yearHours = 0, totalHours = 0;
                List<Model.SitePerson_DayReport> dayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(startTime, endTime, this.CurrUser.LoginProjectId);
                if (dayReports.Count > 0)
                {
                    foreach (var dayReport in dayReports)
                    {
                        monthHours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                    }
                }
                else
                {
                    monthHours = 0;
                }
                if (monthHours == null)
                {
                    monthHours = 0;
                }
                List<Model.SitePerson_DayReport> yearDayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(yearStartTime, yearEndTime, this.CurrUser.LoginProjectId);
                if (yearDayReports.Count > 0)
                {
                    foreach (var yearDayReport in yearDayReports)
                    {
                        yearHours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == yearDayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                    }
                }
                else
                {
                    yearHours = 0;
                }
                if (yearHours == null)
                {
                    yearHours = 0;
                }
                List<Model.SitePerson_DayReport> totalDayReports = BLL.SitePerson_DayReportService.GetDayReports(this.CurrUser.LoginProjectId);
                if (totalDayReports.Count > 0)
                {
                    foreach (var totalDayReport in totalDayReports)
                    {
                        totalHours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == totalDayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                    }
                }
                else
                {
                    totalHours = 0;
                }
                if (yearHours == null)
                {
                    totalHours = 0;
                }
                //安全人工时 
                //轻重死（含待定）事故的最近时间
                Model.Accident_AccidentReport maxAccident = BLL.AccidentReport2Service.GetMaxAccidentTimeReport(endTime, this.CurrUser.LoginProjectId);
                DateTime? maxAccidentTime = null;
                if (maxAccident != null)
                {
                    maxAccidentTime = maxAccident.AccidentDate;
                }
                if (maxAccidentTime != null)
                {
                    DateTime newTime = Convert.ToDateTime(maxAccidentTime);
                    if (startTime >= newTime)  //事故时间在月报开始时间之前
                    {
                        this.txtThisHSEManhours.Text = (monthHours ?? 0).ToString("N0");

                    }
                    else    //事故时间在月报开始时间之后
                    {
                        int? sumHseManhours2 = 0;
                        List<Model.SitePerson_DayReport> newDayReports2 = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(newTime.AddDays(1), endTime, this.CurrUser.LoginProjectId);
                        if (newDayReports2.Count > 0)
                        {
                            foreach (var dayReport in newDayReports2)
                            {
                                sumHseManhours2 += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                            }
                        }
                        else
                        {
                            sumHseManhours2 = 0;
                        }
                        this.txtThisHSEManhours.Text = (sumHseManhours2 ?? 0).ToString("N0");
                    }
                    //本年累计安全工时
                    List<Model.SitePerson_DayReport> yearDayReports2 = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(newTime, yearEndTime, this.CurrUser.LoginProjectId);
                    if (yearDayReports2.Count > 0)
                    {
                        yearHours = 0;
                        foreach (var yearDayReport2 in yearDayReports2)
                        {
                            yearHours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == yearDayReport2.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                        }
                    }
                    else
                    {
                        yearHours = 0;
                    }
                    this.txtYearHSEManhours.Text = (yearHours ?? 0).ToString("N0");
                    //项目累计安全工时
                    List<Model.SitePerson_DayReport> totalDayReports2 = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate2(newTime, this.CurrUser.LoginProjectId);
                    if (totalDayReports2.Count > 0)
                    {
                        totalHours = 0;
                        foreach (var totalDayReport2 in totalDayReports2)
                        {
                            totalHours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == totalDayReport2.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                        }
                    }
                    else
                    {
                        totalHours = 0;
                    }
                    this.txtTotalHSEManhours.Text = (totalHours ?? 0).ToString("N0");
                }
                else
                {
                    this.txtThisHSEManhours.Text = (monthHours ?? 0).ToString("N0");
                    this.txtYearHSEManhours.Text = (yearHours ?? 0).ToString("N0");
                    this.txtTotalHSEManhours.Text = (totalHours ?? 0).ToString("N0");
                }
                //本月可记录HSE事件
                int monthRecordEvent1 = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, "1", this.CurrUser.LoginProjectId);
                int monthRecordEvent2 = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, "2", this.CurrUser.LoginProjectId);
                int monthRecordEvent3 = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, "3", this.CurrUser.LoginProjectId);
                this.txtThisRecordEvent.Text = (monthRecordEvent1 + monthRecordEvent2 + monthRecordEvent3).ToString();
                //本年累计可记录HSE事件
                int yearRecordEvent1 = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(yearStartTime, yearEndTime, "1", this.CurrUser.LoginProjectId);
                int yearRecordEvent2 = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(yearStartTime, yearEndTime, "2", this.CurrUser.LoginProjectId);
                int yearRecordEvent3 = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(yearStartTime, yearEndTime, "3", this.CurrUser.LoginProjectId);
                this.txtYearRecordEvent.Text = (yearRecordEvent1 + yearRecordEvent2 + yearRecordEvent3).ToString();
                //自开工累计可记录HSE事件
                int totalRecordEvent1 = BLL.AccidentReport2Service.GetCountByAccidentType("1", this.CurrUser.LoginProjectId);
                int totalRecordEvent2 = BLL.AccidentReport2Service.GetCountByAccidentType("2", this.CurrUser.LoginProjectId);
                int totalRecordEvent3 = BLL.AccidentReport2Service.GetCountByAccidentType("3", this.CurrUser.LoginProjectId);
                this.txtTotalRecordEvent.Text = (totalRecordEvent1 + totalRecordEvent2 + totalRecordEvent3).ToString();
                //本月不可记录HSE事件
                int monthNoRecordEvent1 = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, "2", this.CurrUser.LoginProjectId);
                int monthNoRecordEvent2 = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, "3", this.CurrUser.LoginProjectId);
                int monthNoRecordEvent3 = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, "4", this.CurrUser.LoginProjectId);
                this.txtThisNoRecordEvent.Text = (monthNoRecordEvent1 + monthNoRecordEvent2 + monthNoRecordEvent3).ToString();
                //本年累计不可记录HSE事件
                int yearNoRecordEvent1 = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(yearStartTime, yearEndTime, "2", this.CurrUser.LoginProjectId);
                int yearNoRecordEvent2 = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(yearStartTime, yearEndTime, "3", this.CurrUser.LoginProjectId);
                int yearNoRecordEvent3 = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(yearStartTime, yearEndTime, "4", this.CurrUser.LoginProjectId);
                this.txtYearNoRecordEvent.Text = (yearNoRecordEvent1 + yearNoRecordEvent2 + yearNoRecordEvent3).ToString();
                //自开工累计不可记录HSE事件
                int totalNoRecordEvent1 = BLL.AccidentReportOtherService.GetCountByAccidentType("2", this.CurrUser.LoginProjectId);
                int totalNoRecordEvent2 = BLL.AccidentReportOtherService.GetCountByAccidentType("3", this.CurrUser.LoginProjectId);
                int totalNoRecordEvent3 = BLL.AccidentReportOtherService.GetCountByAccidentType("4", this.CurrUser.LoginProjectId);
                this.txtTotalNoRecordEvent.Text = (totalNoRecordEvent1 + totalNoRecordEvent2 + totalNoRecordEvent3).ToString();

            }
        }

        #region 保存按钮
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MonthReportId))
            {
                int freezeDay = 5;
                Model.Manager_MonthReportE lastMonthReport = BLL.MonthReportEService.GetLastMonthReportByDate(DateTime.Now, freezeDay, this.CurrUser.LoginProjectId);
                if (lastMonthReport != null)
                {
                    DateTime months = Convert.ToDateTime(lastMonthReport.Months);
                    string date1 = months.Date.Year.ToString() + "-" + months.Date.Month.ToString();
                    string date2 = this.txtMonths.Text.Trim();
                    if (date1 == date2)
                    {
                        Alert.ShowInTop("该月份月报已经存在，不能重复添加！", MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            Model.Manager_MonthReportE monthReport = new Model.Manager_MonthReportE
            {
                MonthReportCode = this.txtMonthReportCode.Text,
                ProjectId = this.ProjectId,
                Months = Funs.GetNewDateTime(this.txtMonths.Text + "-1"),
                MonthReportDate = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                ReportMan = this.CurrUser.UserId
            };
            monthReport.CountryCities = this.txtCountryCities.Text.Trim();
            monthReport.StartEndDate = this.txtStartEndDate.Text.Trim();
            monthReport.ContractType = this.txtContractType.Text.Trim();
            monthReport.ContractAmount = Funs.GetNewDecimalOrZero(this.txtContractAmount.Text.Trim());
            monthReport.ThisMajorWork = this.txtThisMajorWork.Text.Trim();
            monthReport.NextMajorWork = this.txtNextMajorWork.Text.Trim();
            monthReport.ThisIncome = Funs.GetNewDecimalOrZero(this.txtThisIncome.Text.Trim());
            monthReport.YearIncome = Funs.GetNewDecimalOrZero(this.txtYearIncome.Text.Trim());
            monthReport.TotalIncome = Funs.GetNewDecimalOrZero(this.txtTotalIncome.Text.Trim());
            monthReport.ThisImageProgress = this.txtThisImageProgress.Text.Trim();
            monthReport.YearImageProgress = this.txtYearImageProgress.Text.Trim();
            monthReport.TotalImageProgress = this.txtTotalImageProgress.Text.Trim();
            monthReport.ThisPersonNum = Funs.GetNewIntOrZero(this.txtThisPersonNum.Text.Trim());
            monthReport.YearPersonNum = Funs.GetNewIntOrZero(this.txtYearPersonNum.Text.Trim());
            monthReport.TotalPersonNum = Funs.GetNewIntOrZero(this.txtTotalPersonNum.Text.Trim());
            monthReport.ThisForeignPersonNum = Funs.GetNewIntOrZero(this.txtThisForeignPersonNum.Text.Trim());
            monthReport.YearForeignPersonNum = Funs.GetNewIntOrZero(this.txtYearForeignPersonNum.Text.Trim());
            monthReport.TotalForeignPersonNum = Funs.GetNewIntOrZero(this.txtTotalForeignPersonNum.Text.Trim());
            monthReport.ThisTrainPersonNum = Funs.GetNewIntOrZero(this.txtThisTrainPersonNum.Text.Trim());
            monthReport.YearTrainPersonNum = Funs.GetNewIntOrZero(this.txtYearTrainPersonNum.Text.Trim());
            monthReport.TotalTrainPersonNum = Funs.GetNewIntOrZero(this.txtTotalTrainPersonNum.Text.Trim());
            monthReport.ThisCheckNum = Funs.GetNewIntOrZero(this.txtThisCheckNum.Text.Trim());
            monthReport.YearCheckNum = Funs.GetNewIntOrZero(this.txtYearCheckNum.Text.Trim());
            monthReport.TotalCheckNum = Funs.GetNewIntOrZero(this.txtTotalCheckNum.Text.Trim());
            monthReport.ThisViolationNum = Funs.GetNewIntOrZero(this.txtThisViolationNum.Text.Trim());
            monthReport.YearViolationNum = Funs.GetNewIntOrZero(this.txtYearViolationNum.Text.Trim());
            monthReport.TotalViolationNum = Funs.GetNewIntOrZero(this.txtTotalViolationNum.Text.Trim());
            monthReport.ThisInvestment = Funs.GetNewDecimalOrZero(this.txtThisInvestment.Text.Trim());
            monthReport.YearInvestment = Funs.GetNewDecimalOrZero(this.txtYearInvestment.Text.Trim());
            monthReport.TotalInvestment = Funs.GetNewDecimalOrZero(this.txtTotalInvestment.Text.Trim());
            monthReport.ThisReward = Funs.GetNewDecimalOrZero(this.txtThisReward.Text.Trim());
            monthReport.YearReward = Funs.GetNewDecimalOrZero(this.txtYearReward.Text.Trim());
            monthReport.TotalReward = Funs.GetNewDecimalOrZero(this.txtTotalReward.Text.Trim());
            monthReport.ThisPunish = Funs.GetNewDecimalOrZero(this.txtThisPunish.Text.Trim());
            monthReport.YearPunish = Funs.GetNewDecimalOrZero(this.txtYearPunish.Text.Trim());
            monthReport.TotalPunish = Funs.GetNewDecimalOrZero(this.txtTotalPunish.Text.Trim());
            monthReport.ThisEmergencyDrillNum = Funs.GetNewIntOrZero(this.txtThisEmergencyDrillNum.Text.Trim());
            monthReport.YearEmergencyDrillNum = Funs.GetNewIntOrZero(this.txtYearEmergencyDrillNum.Text.Trim());
            monthReport.TotalEmergencyDrillNum = Funs.GetNewIntOrZero(this.txtTotalEmergencyDrillNum.Text.Trim());
            monthReport.ThisHSEManhours = Funs.GetNewIntOrZero(this.txtThisHSEManhours.Text.Trim());
            monthReport.YearHSEManhours = Funs.GetNewIntOrZero(this.txtYearHSEManhours.Text.Trim());
            monthReport.TotalHSEManhours = Funs.GetNewIntOrZero(this.txtTotalHSEManhours.Text.Replace(",", "").Trim());
            monthReport.ThisRecordEvent = Funs.GetNewIntOrZero(this.txtThisRecordEvent.Text.Trim());
            monthReport.YearRecordEvent = Funs.GetNewIntOrZero(this.txtYearRecordEvent.Text.Trim());
            monthReport.TotalRecordEvent = Funs.GetNewIntOrZero(this.txtTotalRecordEvent.Text.Trim());
            monthReport.ThisNoRecordEvent = Funs.GetNewIntOrZero(this.txtThisNoRecordEvent.Text.Trim());
            monthReport.YearNoRecordEvent = Funs.GetNewIntOrZero(this.txtYearNoRecordEvent.Text.Trim());
            monthReport.TotalNoRecordEvent = Funs.GetNewIntOrZero(this.txtTotalNoRecordEvent.Text.Trim());
            monthReport.ProjectManagerName = this.txtProjectManagerName.Text.Trim();
            monthReport.ProjectManagerPhone = this.txtProjectManagerPhone.Text.Trim();
            monthReport.HSEManagerName = this.txtHSEManagerName.Text.Trim();
            monthReport.HSEManagerPhone = this.txtHSEManagerPhone.Text.Trim();
            if (!string.IsNullOrEmpty(this.MonthReportId))
            {
                monthReport.MonthReportId = this.MonthReportId;
                BLL.MonthReportEService.UpdateMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthEMenuId, BLL.Const.BtnModify);
            }
            else
            {
                monthReport.MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReport));
                this.MonthReportId = monthReport.MonthReportId;
                BLL.MonthReportEService.AddMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthEMenuId, BLL.Const.BtnAdd);

                //删除未上报月报信息
                Model.ManagementReport_ReportRemind reportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                                                                    where x.ProjectId == this.ProjectId && x.Months == monthReport.Months && x.ReportName == "海外工程项目月度HSSE统计表"
                                                                    select x).FirstOrDefault();
                if (reportRemind != null)
                {
                    BLL.ReportRemindService.DeleteReportRemindByReportRemind(reportRemind);
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.MonthReportId))
            {
                // 冻结时间
                int freezeDay = 5;
                Model.Manager_MonthReportE lastMonthReport = BLL.MonthReportEService.GetLastMonthReportByDate(DateTime.Now, freezeDay, this.CurrUser.LoginProjectId);
                if (lastMonthReport != null)
                {
                    DateTime months = Convert.ToDateTime(lastMonthReport.Months);
                    string date1 = months.Date.Year.ToString() + "-" + months.Date.Month.ToString();
                    string date2 = this.txtMonths.Text.Trim();
                    if (date1 == date2)
                    {
                        Alert.ShowInTop("该月份海外工程项目月度HSSE统计表已经存在，不能重复添加！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                Model.Manager_MonthReportE monthReport = new Model.Manager_MonthReportE
                {
                    MonthReportCode = this.txtMonthReportCode.Text,
                    ProjectId = this.ProjectId,
                    Months = Funs.GetNewDateTime(this.txtMonths.Text + "-1"),
                    MonthReportDate = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                    ReportMan = this.CurrUser.UserId,
                    MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReport))
                };
                this.MonthReportId = monthReport.MonthReportId;
                BLL.MonthReportEService.AddMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthEMenuId, BLL.Const.BtnAdd);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerMonthReport&menuId={1}", this.MonthReportId, BLL.Const.ProjectManagerMonthEMenuId)));
        }
        #endregion
    }
}