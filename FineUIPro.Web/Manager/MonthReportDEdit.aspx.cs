using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportDEdit : PageBase
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

        private static DateTime months;

        private static DateTime startTime;

        private static DateTime endTime;

        private static DateTime yearStartTime;

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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.txtReportMan.Text = this.CurrUser.UserName;
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.MonthReportId = Request.Params["monthReportId"];
                if (!string.IsNullOrEmpty(MonthReportId))
                {
                    Model.Manager_MonthReportD monthReport = BLL.MonthReportDService.GetMonthReportByMonthReportId(MonthReportId);
                    if (monthReport != null)
                    {
                        this.ProjectId = monthReport.ProjectId;
                        this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthReportId);
                        if (monthReport.Months != null)
                        {
                            months = Convert.ToDateTime(monthReport.Months);
                            this.txtReportMonths.Text = string.Format("{0:yyyy-MM}", monthReport.Months);
                        }
                        this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        Model.Sys_User reportMan = BLL.UserService.GetUserByUserId(monthReport.ReportMan);
                        if (reportMan != null)
                        {
                            this.txtReportMan.Text = reportMan.UserName;
                        }
                        startTime = Convert.ToDateTime(this.txtReportMonths.Text + "-1").AddMonths(-1).AddDays(25);
                        endTime = Convert.ToDateTime(this.txtReportMonths.Text + "-1").AddDays(24);
                        Model.Manager_SafetyDataD safetyData = BLL.SafetyDataDService.GetSafetyDataDByMonthReportId(this.MonthReportId);
                        if (safetyData != null)  //保存过数据
                        {
                            if (safetyData.ThisUnitPersonNum != null)
                            {
                                this.txtThisUnitPersonNum.Text = safetyData.ThisUnitPersonNum.ToString();
                            }
                            if (safetyData.ThisUnitHSEPersonNum != null)
                            {
                                this.txtThisUnitHSEPersonNum.Text = safetyData.ThisUnitHSEPersonNum.ToString();
                            }
                            if (safetyData.SubUnitPersonNum != null)
                            {
                                this.txtSubUnitPersonNum.Text = safetyData.SubUnitPersonNum.ToString();
                            }
                            if (safetyData.SubUnitHSEPersonNum != null)
                            {
                                this.txtSubUnitHSEPersonNum.Text = safetyData.SubUnitHSEPersonNum.ToString();
                            }
                            if (safetyData.ManHours != null)
                            {
                                this.txtManHours.Text = safetyData.ManHours.ToString();
                            }
                            if (safetyData.HSEManHours != null)
                            {
                                this.txtHSEManHours.Text = safetyData.HSEManHours.ToString();
                            }
                            if (safetyData.LossHours != null)
                            {
                                this.txtLossHours.Text = safetyData.LossHours.ToString();
                            }
                            if (safetyData.LossDay != null)
                            {
                                this.txtLossDay.Text = safetyData.LossDay.ToString();
                            }
                            if (safetyData.DeathNum != null)
                            {
                                this.txtDeathNum.Text = safetyData.DeathNum.ToString();
                            }
                            if (safetyData.DeathPersonNum != null)
                            {
                                this.txtDeathPersonNum.Text = safetyData.DeathPersonNum.ToString();
                            }
                            if (safetyData.SeriousInjuredNum != null)
                            {
                                this.txtSeriousInjuredNum.Text = safetyData.SeriousInjuredNum.ToString();
                            }
                            if (safetyData.SeriousInjuriesPersonNum != null)
                            {
                                this.txtSeriousInjuriesPersonNum.Text = safetyData.SeriousInjuriesPersonNum.ToString();
                            }
                            if (safetyData.SeriousInjuriesLossHour != null)
                            {
                                this.txtSeriousInjuriesLossHour.Text = safetyData.SeriousInjuriesLossHour.ToString();
                            }
                            if (safetyData.MinorInjuredNum != null)
                            {
                                this.txtMinorInjuredNum.Text = safetyData.MinorInjuredNum.ToString();
                            }
                            if (safetyData.MinorAccidentPersonNum != null)
                            {
                                this.txtMinorAccidentPersonNum.Text = safetyData.MinorAccidentPersonNum.ToString();
                            }
                            if (safetyData.MinorAccidentLossHour != null)
                            {
                                this.txtMinorAccidentLossHour.Text = safetyData.MinorAccidentLossHour.ToString();
                            }
                            if (safetyData.OtherNum != null)
                            {
                                this.txtOtherNum.Text = safetyData.OtherNum.ToString();
                            }
                            if (safetyData.OtherAccidentPersonNum != null)
                            {
                                this.txtOtherAccidentPersonNum.Text = safetyData.OtherAccidentPersonNum.ToString();
                            }
                            if (safetyData.OtherAccidentLossHour != null)
                            {
                                this.txtOtherAccidentLossHour.Text = safetyData.OtherAccidentLossHour.ToString();
                            }
                            if (safetyData.MedicalTreatmentNum != null)
                            {
                                this.txtMedicalTreatmentNum.Text = safetyData.MedicalTreatmentNum.ToString();
                            }
                            if (safetyData.MedicalTreatmentLossHour != null)
                            {
                                this.txtMedicalTreatmentLossHour.Text = safetyData.MedicalTreatmentLossHour.ToString();
                            }
                            if (safetyData.WorkLimitNum != null)
                            {
                                this.txtWorkLimitNum.Text = safetyData.WorkLimitNum.ToString();
                            }
                            if (safetyData.RestrictedWorkLossHour != null)
                            {
                                this.txtRestrictedWorkLossHour.Text = safetyData.RestrictedWorkLossHour.ToString();
                            }
                            if (safetyData.FirstAidNum != null)
                            {
                                this.txtFirstAidNum.Text = safetyData.FirstAidNum.ToString();
                            }
                            if (safetyData.OccupationalDiseasesNum != null)
                            {
                                this.txtOccupationalDiseasesNum.Text = safetyData.OccupationalDiseasesNum.ToString();
                            }
                            if (safetyData.AttemptedAccidentNum != null)
                            {
                                this.txtAttemptedAccidentNum.Text = safetyData.AttemptedAccidentNum.ToString();
                            }
                            if (safetyData.PersonInjuredLossMoney != null)
                            {
                                this.txtPersonInjuredLossMoney.Text = safetyData.PersonInjuredLossMoney.ToString();
                            }
                            if (safetyData.FireNum != null)
                            {
                                this.txtFireNum.Text = safetyData.FireNum.ToString();
                            }
                            if (safetyData.ExplosionNum != null)
                            {
                                this.txtExplosionNum.Text = safetyData.ExplosionNum.ToString();
                            }
                            if (safetyData.TrafficNum != null)
                            {
                                this.txtTrafficNum.Text = safetyData.TrafficNum.ToString();
                            }
                            if (safetyData.EquipmentNum != null)
                            {
                                this.txtEquipmentNum.Text = safetyData.EquipmentNum.ToString();
                            }
                            if (safetyData.SiteEnvironmentNum != null)
                            {
                                this.txtSiteEnvironmentNum.Text = safetyData.SiteEnvironmentNum.ToString();
                            }
                            if (safetyData.TheftCaseNum != null)
                            {
                                this.txtTheftCaseNum.Text = safetyData.TheftCaseNum.ToString();
                            }
                            if (safetyData.PropertyLossMoney != null)
                            {
                                this.txtPropertyLossMoney.Text = safetyData.PropertyLossMoney.ToString();
                            }
                            if (safetyData.MainBusinessIncome != null)
                            {
                                this.txtMainBusinessIncome.Text = safetyData.MainBusinessIncome.ToString();
                            }
                            if (safetyData.ConstructionIncome != null)
                            {
                                this.txtConstructionIncome.Text = safetyData.ConstructionIncome.ToString();
                            }
                            if (safetyData.ProjectVolume != null)
                            {
                                this.txtProjectVolume.Text = safetyData.ProjectVolume.ToString();
                            }
                            if (safetyData.PaidForMoney != null)
                            {
                                this.txtPaidForMoney.Text = safetyData.PaidForMoney.ToString();
                            }
                            if (safetyData.ApprovedChargesMoney != null)
                            {
                                this.txtApprovedChargesMoney.Text = safetyData.ApprovedChargesMoney.ToString();
                            }
                            if (safetyData.HasBeenChargedMoney != null)
                            {
                                this.txtHasBeenChargedMoney.Text = safetyData.HasBeenChargedMoney.ToString();
                            }
                            //if (safetyData.WeekMeetingNum != null)
                            //{
                            //    this.txtWeekMeetingNum.Text = safetyData.WeekMeetingNum.ToString();
                            //}
                            //if (safetyData.CommitteeMeetingNum != null)
                            //{
                            //    this.txtCommitteeMeetingNum.Text = safetyData.CommitteeMeetingNum.ToString();
                            //}
                            if (safetyData.TrainPersonNum != null)
                            {
                                this.txtTrainPersonNum.Text = safetyData.TrainPersonNum.ToString();
                            }
                            //if (safetyData.WeekCheckNum != null)
                            //{
                            //    this.txtWeekCheckNum.Text = safetyData.WeekCheckNum.ToString();
                            //}
                            //if (safetyData.HSECheckNum != null)
                            //{
                            //    this.txtHSECheckNum.Text = safetyData.HSECheckNum.ToString();
                            //}
                            //if (safetyData.SpecialCheckNum != null)
                            //{
                            //    this.txtSpecialCheckNum.Text = safetyData.SpecialCheckNum.ToString();
                            //}
                            //if (safetyData.EquipmentHSEInspectionNum != null)
                            //{
                            //    this.txtEquipmentHSEInspectionNum.Text = safetyData.EquipmentHSEInspectionNum.ToString();
                            //}
                            //if (safetyData.LicenseNum != null)
                            //{
                            //    this.txtLicenseNum.Text = safetyData.LicenseNum.ToString();
                            //}
                            //if (safetyData.SolutionNum != null)
                            //{
                            //    this.txtSolutionNum.Text = safetyData.SolutionNum.ToString();
                            //}
                            if (safetyData.ReleaseRectifyNum != null)
                            {
                                this.txtReleaseRectifyNum.Text = safetyData.ReleaseRectifyNum.ToString();
                            }
                            if (safetyData.CloseRectifyNum != null)
                            {
                                this.txtCloseRectifyNum.Text = safetyData.CloseRectifyNum.ToString();
                            }
                            if (safetyData.ReleasePunishNum != null)
                            {
                                this.txtReleasePunishNum.Text = safetyData.ReleasePunishNum.ToString();
                            }
                            if (safetyData.PunishMoney != null)
                            {
                                this.txtPunishMoney.Text = safetyData.PunishMoney.ToString();
                            }
                            if (safetyData.IncentiveMoney != null)
                            {
                                this.txtIncentiveMoney.Text = safetyData.IncentiveMoney.ToString();
                            }
                            if (safetyData.EmergencyDrillNum != null)
                            {
                                this.txtEmergencyDrillNum.Text = safetyData.EmergencyDrillNum.ToString();
                            }
                            if (safetyData.ParticipantsNum != null)
                            {
                                this.txtParticipantsNum.Text = safetyData.ParticipantsNum.ToString();
                            }
                            if (safetyData.DrillInput != null)
                            {
                                this.txtDrillInput.Text = safetyData.DrillInput.ToString();
                            }
                            this.txtDrillTypes.Text = safetyData.DrillTypes.ToString();
                        }
                    }
                }
                else
                {
                    this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthDMenuId, this.ProjectId, this.CurrUser.UnitId);
                    months = Convert.ToDateTime(Request.Params["months"]);
                    this.txtReportMonths.Text = string.Format("{0:yyyy-MM}", months);
                    this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    startTime = months.AddMonths(-1).AddDays(25);
                    endTime = months.AddDays(24);
                    Model.SUBHSSEDB db = Funs.DB;
                    List<Model.Base_Unit> thisUnitLists = BLL.UnitService.GetThisUnitDropDownList();
                    Model.Base_Unit thisUnit = new Model.Base_Unit();
                    if (thisUnitLists.Count > 0)
                    {
                        thisUnit = thisUnitLists[0];
                    }
                    else
                    {
                        thisUnit = BLL.UnitService.GetUnitByProjectIdList(this.ProjectId)[0];
                    }
                    //本公司现场人数取自项目人员信息模块本公司数据
                    this.txtThisUnitPersonNum.Text = BLL.PersonService.GetPersonLitsByprojectIdUnitId(this.ProjectId, thisUnit.UnitId).Count.ToString();
                    //本公司现场HSE管理人数取自项目人员信息模块本公司HSSE管理岗位数据
                    this.txtThisUnitHSEPersonNum.Text = (from x in db.SitePerson_Person
                                                         join y in db.Base_WorkPost
                                                         on x.WorkPostId equals y.WorkPostId
                                                         where x.ProjectId == this.ProjectId && x.UnitId == thisUnit.UnitId && y.IsHsse == true
                                                         select x).Count().ToString();
                    //分包商现场人数取自项目人员信息模块所有分包商数据
                    this.txtSubUnitPersonNum.Text = (from x in db.SitePerson_Person
                                                     join y in db.Project_ProjectUnit
                                                     on x.UnitId equals y.UnitId
                                                     where x.ProjectId == this.ProjectId && y.UnitType == BLL.Const.ProjectUnitType_2
                                                     select x).Count().ToString();
                    //分包商现场人数取自项目人员信息模块所有分包商HSSE管理岗位数据
                    this.txtSubUnitHSEPersonNum.Text = (from x in db.SitePerson_Person
                                                        join y in db.Project_ProjectUnit
                                                        on x.UnitId equals y.UnitId
                                                        join z in db.Base_WorkPost
                                                        on x.WorkPostId equals z.WorkPostId
                                                        where x.ProjectId == this.ProjectId && y.UnitType == BLL.Const.ProjectUnitType_2 && z.IsHsse == true
                                                        select x).Count().ToString();
                    //人工时数取自人工时月报数据
                    decimal sumManHours = 0;
                    var sumList = from x in db.SitePerson_MonthReportDetail
                                  join y in db.SitePerson_MonthReport on x.MonthReportId equals y.MonthReportId
                                  where y.ProjectId == this.CurrUser.LoginProjectId && y.CompileDate == months
                                  select x;
                    if (sumList.Count() > 0)
                    {
                        var sum = sumList.Sum(x => x.PersonWorkTime);
                        if (sum.HasValue)
                        {
                            sumManHours = sum.Value;
                        }
                    }
                    this.txtManHours.Text = sumManHours.ToString();
                    //损失工时数取自事故调查报告模块数据
                    decimal sumWorkingHoursLoss = 0;
                    var accidentList = from x in db.Accident_AccidentReport
                                       where x.ProjectId == this.ProjectId && x.AccidentDate >= startTime && x.AccidentDate < endTime
                                       select x;
                    if (accidentList.Count() > 0)
                    {
                        var sum = accidentList.Sum(x => x.WorkingHoursLoss);
                        if (sum.HasValue)
                        {
                            sumWorkingHoursLoss = sum.Value;
                        }
                    }
                    this.txtLossHours.Text = sumWorkingHoursLoss.ToString();
                    //安全生产人工时数取值为当月人工时数-损失工时数
                    decimal hSEManHours = 0;
                    hSEManHours = sumManHours - sumWorkingHoursLoss;
                    this.txtHSEManHours.Text = hSEManHours.ToString();
                    //损失工作日数取自损失工时数/8
                    int lossDay = 0;
                    decimal lossWorkDay = Convert.ToDecimal(Math.Round(sumWorkingHoursLoss / 8, 2));
                    if (lossWorkDay.ToString().IndexOf(".") > 0 && lossWorkDay.ToString().Substring(lossWorkDay.ToString().IndexOf("."), lossWorkDay.ToString().Length - lossWorkDay.ToString().IndexOf(".")) != ".00")
                    {
                        string intPart = lossWorkDay.ToString().Substring(0, lossWorkDay.ToString().IndexOf("."));
                        string part = lossWorkDay.ToString().Substring(lossWorkDay.ToString().IndexOf(".") + 1, 1);
                        if (Funs.GetNewIntOrZero(part) > 4)
                        {
                            lossDay = Convert.ToInt32(intPart) + 1;
                        }
                        else
                        {
                            lossDay = Convert.ToInt32(intPart);
                        }
                    }
                    else
                    {
                        lossDay = Convert.ToInt32(lossWorkDay);
                    }
                    this.txtLossDay.Text = lossDay.ToString();
                    //各事故数取自事故调查报告及事故调查处理报告模块数据
                    decimal totalLossMoney1 = 0, totalLossMoney2 = 0;
                    this.txtDeathNum.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, "1", this.ProjectId).ToString();
                    this.txtDeathPersonNum.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, "1", this.ProjectId).ToString();
                    totalLossMoney1 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "1", this.ProjectId);
                    this.txtSeriousInjuredNum.Text = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "2").Count.ToString();
                    this.txtSeriousInjuriesPersonNum.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, "2", this.ProjectId).ToString();
                    this.txtSeriousInjuriesLossHour.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, "2", this.ProjectId).ToString();
                    totalLossMoney1 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "2", this.ProjectId);
                    this.txtMinorInjuredNum.Text = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "3").Count.ToString();
                    this.txtMinorAccidentPersonNum.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, "3", this.ProjectId).ToString();
                    this.txtMinorAccidentLossHour.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, "3", this.ProjectId).ToString();
                    totalLossMoney1 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "3", this.ProjectId);
                    this.txtOtherNum.Text = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "11").Count.ToString();
                    this.txtOtherAccidentPersonNum.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, "11", this.ProjectId).ToString();
                    this.txtOtherAccidentLossHour.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, "11", this.ProjectId).ToString();
                    totalLossMoney1 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "11", this.ProjectId);
                    this.txtMedicalTreatmentNum.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, "2", this.ProjectId).ToString();
                    this.txtMedicalTreatmentLossHour.Text = "0";
                    totalLossMoney1 += BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "2", this.ProjectId);
                    this.txtWorkLimitNum.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, "1", this.ProjectId).ToString();
                    this.txtRestrictedWorkLossHour.Text = "0";
                    totalLossMoney1 += BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "1", this.ProjectId);
                    this.txtFirstAidNum.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, "3", this.ProjectId).ToString();
                    totalLossMoney1 += BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "3", this.ProjectId);
                    this.txtOccupationalDiseasesNum.Text = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "9").Count.ToString();
                    totalLossMoney1 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "9", this.ProjectId);
                    this.txtAttemptedAccidentNum.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, "4", this.ProjectId).ToString();
                    totalLossMoney1 += BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "4", this.ProjectId);
                    this.txtPersonInjuredLossMoney.Text = Convert.ToDecimal(Math.Round(totalLossMoney1 / 10000, 2)).ToString();
                    this.txtFireNum.Text = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "4").Count.ToString();
                    totalLossMoney2 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "4", this.ProjectId);
                    this.txtExplosionNum.Text = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "5").Count.ToString();
                    totalLossMoney2 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "5", this.ProjectId);
                    this.txtTrafficNum.Text = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "6").Count.ToString();
                    totalLossMoney2 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "6", this.ProjectId);
                    this.txtEquipmentNum.Text = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "7").Count.ToString();
                    totalLossMoney2 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "7", this.ProjectId);
                    this.txtSiteEnvironmentNum.Text = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "8").Count.ToString();
                    totalLossMoney2 += BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, "8", this.ProjectId);
                    this.txtTheftCaseNum.Text = "0";
                    this.txtPropertyLossMoney.Text = Convert.ToDecimal(Math.Round(totalLossMoney2 / 10000, 2)).ToString();
                    //安全生产费用
                    this.txtMainBusinessIncome.Text = "0";
                    this.txtConstructionIncome.Text = "0";
                    this.txtProjectVolume.Text = "0";
                    this.txtPaidForMoney.Text = "0";
                    this.txtApprovedChargesMoney.Text = "0";
                    this.txtHasBeenChargedMoney.Text = "0";
                    //人员培训
                    this.txtTrainPersonNum.Text = (from x in db.EduTrain_TrainRecordDetail
                                                   join y in db.EduTrain_TrainRecord
                                                   on x.TrainingId equals y.TrainingId
                                                   where y.ProjectId == this.ProjectId && y.TrainStartDate >= startTime && y.TrainStartDate < endTime && y.States == BLL.Const.State_2
                                                   select x).Count().ToString();
                    //安全检查
                    this.txtReleaseRectifyNum.Text = (from x in db.Check_RectifyNotices
                                                      where x.ProjectId == this.ProjectId && x.CheckedDate >= startTime && x.CheckedDate < endTime
                                                      select x).Count().ToString();
                    this.txtCloseRectifyNum.Text = (from x in db.Check_RectifyNotices
                                                    where x.ProjectId == this.ProjectId && x.CheckedDate >= startTime && x.CheckedDate < endTime && x.IsRectify == true
                                                    select x).Count().ToString();
                    var punishList = from x in db.Check_PunishNotice
                                     where x.ProjectId == this.ProjectId && x.PunishNoticeDate >= startTime && x.PunishNoticeDate < endTime && x.States == BLL.Const.State_2
                                     select x.PunishMoney;
                    this.txtReleasePunishNum.Text = punishList.Count().ToString();
                    decimal totalPunishMoney = 0;
                    if (punishList.Count() > 0)
                    {
                        foreach (var item in punishList)
                        {
                            if (item != null)
                            {
                                totalPunishMoney += Funs.GetNewDecimalOrZero(item.ToString());
                            }
                        }
                    }
                    this.txtPunishMoney.Text = totalPunishMoney.ToString();
                    var incentiveList = from x in db.Check_IncentiveNotice
                                        where x.ProjectId == this.ProjectId && x.IncentiveDate >= startTime && x.IncentiveDate < endTime && x.States == BLL.Const.State_2
                                        select x.IncentiveMoney;
                    decimal totalIncentiveMoney = 0;
                    if (incentiveList.Count() > 0)
                    {
                        foreach (var item in incentiveList)
                        {
                            if (item != null)
                            {
                                totalIncentiveMoney += Funs.GetNewDecimalOrZero(item.ToString());
                            }
                        }
                    }
                    this.txtIncentiveMoney.Text = totalIncentiveMoney.ToString();
                    //应急管理
                    List<Model.Emergency_DrillRecordList> drillRecordList = (from x in db.Emergency_DrillRecordList
                                                                             where x.ProjectId == this.ProjectId && x.DrillRecordDate >= startTime && x.DrillRecordDate < endTime && x.States == BLL.Const.State_2
                                                                             select x).ToList();
                    this.txtEmergencyDrillNum.Text = drillRecordList.Count.ToString();
                    int drillRecordPersonNum = 0;
                    var drillRecordPersonList = from x in drillRecordList
                                                select x.JointPersonNum;
                    if (drillRecordPersonList.Count() > 0)
                    {
                        foreach (var item in drillRecordPersonList)
                        {
                            if (item != null)
                            {
                                drillRecordPersonNum += Funs.GetNewIntOrZero(item.ToString());
                            }
                        }
                    }
                    this.txtParticipantsNum.Text = drillRecordPersonNum.ToString();
                    decimal drillCosts = 0;
                    var drillRecordCostList = from x in drillRecordList
                                              select x.DrillCost;
                    if (drillRecordCostList.Count() > 0)
                    {
                        foreach (var item in drillRecordCostList)
                        {
                            if (item != null)
                            {
                                drillCosts += Funs.GetNewDecimalOrZero(item.ToString());
                            }
                        }
                    }
                    this.txtDrillInput.Text = (drillCosts/10000).ToString();
                }
                if (months.Month == 1)   //1月份月报的年度值就是当月值
                {
                    yearStartTime = startTime;
                }
                else
                {
                    yearStartTime = Convert.ToDateTime((months.Year - 1) + "-12-26");
                }
            }
        }
        #endregion

        private string GetIntString(string str)
        {
            if (str.IndexOf(".") > 0)
            {
                return str.Substring(0, str.IndexOf("."));
            }
            else
            {
                return str;
            }
        }

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Manager_MonthReportD oldMonthReport = BLL.MonthReportDService.GetMonthReportByMonths(months, this.CurrUser.LoginProjectId);
            if (oldMonthReport != null)
            {
                this.MonthReportId = oldMonthReport.MonthReportId;
                BLL.SafetyDataDService.DeleteSafetyDataDByMonthReportId(this.MonthReportId);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改安全生产月报", MonthReportId);
            }
            else
            {
                Model.Manager_MonthReportD monthReport = new Model.Manager_MonthReportD();
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MonthReportD));
                monthReport.MonthReportId = newKeyID;
                monthReport.ProjectId = this.CurrUser.LoginProjectId;
                this.MonthReportId = newKeyID;
                monthReport.MonthReportCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthDMenuId, this.ProjectId, this.CurrUser.UnitId);
                monthReport.Months = Funs.GetNewDateTime(Request.Params["months"]);
                monthReport.ReportMan = this.CurrUser.UserId;
                monthReport.MonthReportDate = DateTime.Now;
                BLL.MonthReportDService.AddMonthReport(monthReport);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加安全生产月报", monthReport.MonthReportId);
            }
            SaveData("save");
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Model.Manager_MonthReportD oldMonthReport = BLL.MonthReportDService.GetMonthReportByMonths(months, this.CurrUser.LoginProjectId);
            if (oldMonthReport != null)
            {
                this.MonthReportId = oldMonthReport.MonthReportId;
                oldMonthReport.States = BLL.Const.State_2;
                BLL.MonthReportDService.UpdateMonthReport(oldMonthReport);
                BLL.SafetyDataDService.DeleteSafetyDataDByMonthReportId(this.MonthReportId);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改安全生产月报", MonthReportId);
            }
            else
            {
                Model.Manager_MonthReportD monthReport = new Model.Manager_MonthReportD();
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MonthReportD));
                monthReport.MonthReportId = newKeyID;
                monthReport.ProjectId = this.CurrUser.LoginProjectId;
                this.MonthReportId = newKeyID;
                monthReport.MonthReportCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthDMenuId, this.ProjectId, this.CurrUser.UnitId);
                monthReport.Months = Funs.GetNewDateTime(Request.Params["months"]);
                monthReport.ReportMan = this.CurrUser.UserId;
                monthReport.MonthReportDate = DateTime.Now;
                monthReport.States = BLL.Const.State_2;
                BLL.MonthReportDService.AddMonthReport(monthReport);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加安全生产月报", monthReport.MonthReportId);
            }
            SaveData("submit");

            ////判断单据是否 加入到企业管理资料
            string menuId = BLL.Const.ProjectManagerMonthDMenuId;
            var safeData = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == menuId);
            if (safeData != null)
            {
                BLL.SafetyDataService.AddSafetyData(menuId, this.MonthReportId, this.txtReportMonths.Text + "安全生产月报", "../Manager/MonthReportDView.aspx?MonthReportId={0}", this.ProjectId);
            }

            ShowNotify("提交成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveData(string type)
        {
            Model.Manager_SafetyDataD safetyData = new Model.Manager_SafetyDataD
            {
                MonthReportId = this.MonthReportId,
                ThisUnitPersonNum = Funs.GetNewIntOrZero(this.txtThisUnitPersonNum.Text.Trim()),
                ThisUnitHSEPersonNum = Funs.GetNewIntOrZero(this.txtThisUnitHSEPersonNum.Text.Trim()),
                SubUnitPersonNum = Funs.GetNewIntOrZero(this.txtSubUnitPersonNum.Text.Trim()),
                SubUnitHSEPersonNum = Funs.GetNewIntOrZero(this.txtSubUnitHSEPersonNum.Text.Trim()),
                ManHours = Funs.GetNewIntOrZero(this.txtManHours.Text.Trim()),
                HSEManHours = Funs.GetNewIntOrZero(this.txtHSEManHours.Text.Trim()),
                LossHours = Funs.GetNewIntOrZero(this.txtLossHours.Text.Trim()),
                LossDay = Funs.GetNewIntOrZero(this.txtLossDay.Text.Trim()),
                DeathNum = Funs.GetNewIntOrZero(this.txtDeathNum.Text.Trim()),
                DeathPersonNum = Funs.GetNewIntOrZero(this.txtDeathPersonNum.Text.Trim()),
                SeriousInjuredNum = Funs.GetNewIntOrZero(this.txtSeriousInjuredNum.Text.Trim()),
                SeriousInjuriesPersonNum = Funs.GetNewIntOrZero(this.txtSeriousInjuriesPersonNum.Text.Trim()),
                SeriousInjuriesLossHour = Funs.GetNewIntOrZero(this.txtSeriousInjuriesLossHour.Text.Trim()),
                MinorInjuredNum = Funs.GetNewIntOrZero(this.txtMinorInjuredNum.Text.Trim()),
                MinorAccidentPersonNum = Funs.GetNewIntOrZero(this.txtMinorAccidentPersonNum.Text.Trim()),
                MinorAccidentLossHour = Funs.GetNewIntOrZero(this.txtMinorAccidentLossHour.Text.Trim()),
                OtherNum = Funs.GetNewIntOrZero(this.txtOtherNum.Text.Trim()),
                OtherAccidentPersonNum = Funs.GetNewIntOrZero(this.txtOtherAccidentPersonNum.Text.Trim()),
                OtherAccidentLossHour = Funs.GetNewIntOrZero(this.txtOtherAccidentLossHour.Text.Trim()),
                MedicalTreatmentNum = Funs.GetNewIntOrZero(this.txtMedicalTreatmentNum.Text.Trim()),
                MedicalTreatmentLossHour = Funs.GetNewIntOrZero(this.txtMedicalTreatmentLossHour.Text.Trim()),
                WorkLimitNum = Funs.GetNewIntOrZero(this.txtWorkLimitNum.Text.Trim()),
                RestrictedWorkLossHour = Funs.GetNewIntOrZero(this.txtRestrictedWorkLossHour.Text.Trim()),
                FirstAidNum = Funs.GetNewIntOrZero(this.txtFirstAidNum.Text.Trim()),
                OccupationalDiseasesNum = Funs.GetNewIntOrZero(this.txtOccupationalDiseasesNum.Text.Trim()),
                AttemptedAccidentNum = Funs.GetNewIntOrZero(this.txtAttemptedAccidentNum.Text.Trim()),
                PersonInjuredLossMoney = Funs.GetNewDecimalOrZero(this.txtPersonInjuredLossMoney.Text.Trim()),
                FireNum = Funs.GetNewIntOrZero(this.txtFireNum.Text.Trim()),
                ExplosionNum = Funs.GetNewIntOrZero(this.txtExplosionNum.Text.Trim()),
                TrafficNum = Funs.GetNewIntOrZero(this.txtTrafficNum.Text.Trim()),
                EquipmentNum = Funs.GetNewIntOrZero(this.txtEquipmentNum.Text.Trim()),
                SiteEnvironmentNum = Funs.GetNewIntOrZero(this.txtSiteEnvironmentNum.Text.Trim()),
                TheftCaseNum = Funs.GetNewIntOrZero(this.txtTheftCaseNum.Text.Trim()),
                PropertyLossMoney = Funs.GetNewDecimalOrZero(this.txtPropertyLossMoney.Text.Trim()),
                MainBusinessIncome = Funs.GetNewDecimalOrZero(this.txtMainBusinessIncome.Text.Trim()),
                ConstructionIncome = Funs.GetNewDecimalOrZero(this.txtConstructionIncome.Text.Trim()),
                ProjectVolume = Funs.GetNewDecimalOrZero(this.txtProjectVolume.Text.Trim()),
                PaidForMoney = Funs.GetNewDecimalOrZero(this.txtPaidForMoney.Text.Trim()),
                ApprovedChargesMoney = Funs.GetNewDecimalOrZero(this.txtApprovedChargesMoney.Text.Trim()),
                HasBeenChargedMoney = Funs.GetNewDecimalOrZero(this.txtHasBeenChargedMoney.Text.Trim()),
                //safetyData.WeekMeetingNum = Funs.GetNewIntOrZero(this.txtWeekMeetingNum.Text.Trim());
                //safetyData.CommitteeMeetingNum = Funs.GetNewIntOrZero(this.txtCommitteeMeetingNum.Text.Trim());
                TrainPersonNum = Funs.GetNewIntOrZero(this.txtTrainPersonNum.Text.Trim()),
                //safetyData.WeekCheckNum = Funs.GetNewIntOrZero(this.txtWeekCheckNum.Text.Trim());
                //safetyData.HSECheckNum = Funs.GetNewIntOrZero(this.txtHSECheckNum.Text.Trim());
                //safetyData.SpecialCheckNum = Funs.GetNewIntOrZero(this.txtSpecialCheckNum.Text.Trim());
                //safetyData.EquipmentHSEInspectionNum = Funs.GetNewIntOrZero(this.txtEquipmentHSEInspectionNum.Text.Trim());
                //safetyData.LicenseNum = Funs.GetNewIntOrZero(this.txtLicenseNum.Text.Trim());
                //safetyData.SolutionNum = Funs.GetNewIntOrZero(this.txtSolutionNum.Text.Trim());
                ReleaseRectifyNum = Funs.GetNewIntOrZero(this.txtReleaseRectifyNum.Text.Trim()),
                CloseRectifyNum = Funs.GetNewIntOrZero(this.txtCloseRectifyNum.Text.Trim()),
                ReleasePunishNum = Funs.GetNewIntOrZero(this.txtReleasePunishNum.Text.Trim()),
                PunishMoney = Funs.GetNewDecimalOrZero(this.txtPunishMoney.Text.Trim()),
                IncentiveMoney = Funs.GetNewDecimalOrZero(this.txtIncentiveMoney.Text.Trim()),
                EmergencyDrillNum = Funs.GetNewIntOrZero(this.txtEmergencyDrillNum.Text.Trim()),
                ParticipantsNum = Funs.GetNewIntOrZero(this.txtParticipantsNum.Text.Trim()),
                DrillInput = Funs.GetNewDecimalOrZero(this.txtDrillInput.Text.Trim()),
                DrillTypes = this.txtDrillTypes.Text.Trim()
            };
            BLL.SafetyDataDService.AddSafetyDataD(safetyData);
            if (type == "submit")
            {
                //职工伤亡事故原因分析报
                Model.InformationProject_AccidentCauseReport oldAccidentCauseReport = (from x in Funs.DB.InformationProject_AccidentCauseReport
                                                                                       where x.ProjectId == this.ProjectId && x.Year == months.Year && x.Month == months.Month
                                                                                       select x).FirstOrDefault();
                if (oldAccidentCauseReport != null)
                {
                    BLL.ProjectAccidentCauseReportItemService.DeleteAccidentCauseReportItemByAccidentCauseReportId(oldAccidentCauseReport.AccidentCauseReportId);
                    BLL.ProjectAccidentCauseReportService.DeleteAccidentCauseReportById(oldAccidentCauseReport.AccidentCauseReportId);
                }
                Model.InformationProject_AccidentCauseReport accidentCauseReport = new Model.InformationProject_AccidentCauseReport
                {
                    AccidentCauseReportId = SQLHelper.GetNewID(typeof(Model.InformationProject_AccidentCauseReport)),
                    ProjectId = this.ProjectId,
                    UnitId = this.CurrUser.UnitId,
                    AccidentCauseReportCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectAccidentCauseReportMenuId, this.ProjectId, this.CurrUser.UnitId),
                    Year = months.Year,
                    Month = months.Month,
                    DeathAccident = safetyData.DeathNum,
                    DeathToll = safetyData.DeathPersonNum,
                    InjuredAccident = safetyData.SeriousInjuredNum,
                    InjuredToll = safetyData.SeriousInjuriesPersonNum,
                    MinorWoundAccident = safetyData.MinorInjuredNum,
                    MinorWoundToll = safetyData.MinorAccidentPersonNum,
                    AverageTotalHours = safetyData.ManHours,
                    AverageManHours = safetyData.ThisUnitPersonNum + safetyData.SubUnitPersonNum,  //人数取自本公司及分包人数
                    TotalLossMan = safetyData.LossHours
                };
                var accidentReports5 = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTime(startTime.AddMonths(-1), endTime.AddMonths(-1), this.CurrUser.LoginProjectId);
                accidentCauseReport.LastMonthLossHoursTotal = Funs.GetNewIntOrZero(accidentReports5.Sum(x => x.WorkingHoursLoss ?? 0).ToString());
                accidentCauseReport.KnockOffTotal = safetyData.LossDay;
                //经济损失
                accidentCauseReport.DirectLoss = Funs.GetNewIntOrZero((safetyData.PropertyLossMoney * 10000).ToString());
                accidentCauseReport.IndirectLosses = accidentCauseReport.DirectLoss * 6;
                accidentCauseReport.TotalLoss = accidentCauseReport.DirectLoss + accidentCauseReport.IndirectLosses;
                accidentCauseReport.TotalLossTime = safetyData.HSEManHours;
                accidentCauseReport.CompileMan = this.CurrUser.UserId;
                accidentCauseReport.CompileDate = DateTime.Now;
                accidentCauseReport.States = BLL.Const.State_2;
                BLL.ProjectAccidentCauseReportService.AddAccidentCauseReport(accidentCauseReport);
                //百万工时安全统计月报
                Model.InformationProject_MillionsMonthlyReport oldMillionsMonthlyReport = (from x in Funs.DB.InformationProject_MillionsMonthlyReport
                                                                                           where x.ProjectId == this.ProjectId && x.Year == months.Year && x.Month == months.Month
                                                                                           select x).FirstOrDefault();
                if (oldMillionsMonthlyReport != null)
                {
                    BLL.ProjectMillionsMonthlyReportService.DeleteMillionsMonthlyReportById(oldMillionsMonthlyReport.MillionsMonthlyReportId);
                }
                Model.InformationProject_MillionsMonthlyReport millionsMonthlyReport = new Model.InformationProject_MillionsMonthlyReport
                {
                    MillionsMonthlyReportId = SQLHelper.GetNewID(typeof(Model.InformationProject_MillionsMonthlyReport)),
                    ProjectId = this.ProjectId,
                    Year = months.Year,
                    Month = months.Month,
                    CompileMan = this.CurrUser.UserId,
                    CompileDate = DateTime.Now,
                    States = BLL.Const.State_2,
                    //millionsMonthlyReport.Affiliation = millionsMonthlyReport.Affiliation;
                    //millionsMonthlyReport.Name = millionsMonthlyReport.Name;
                    PostPersonNum = safetyData.ThisUnitPersonNum,   //在岗员工即为本单位员工
                    SnapPersonNum = 0,
                    ContractorNum = safetyData.SubUnitPersonNum,
                    SumPersonNum = safetyData.ThisUnitPersonNum + safetyData.SubUnitPersonNum,
                    TotalWorkNum = decimal.Round(decimal.Round(Convert.ToDecimal(safetyData.ManHours), 4) / 10000, 4),
                    SeriousInjuriesNum = safetyData.SeriousInjuredNum,
                    SeriousInjuriesPersonNum = safetyData.SeriousInjuriesPersonNum,
                    SeriousInjuriesLossHour = safetyData.SeriousInjuriesLossHour,
                    MinorAccidentNum = safetyData.MinorInjuredNum,
                    MinorAccidentPersonNum = safetyData.MinorAccidentPersonNum,
                    MinorAccidentLossHour = safetyData.MinorAccidentLossHour,
                    OtherAccidentNum = safetyData.OtherNum,
                    OtherAccidentPersonNum = safetyData.OtherAccidentPersonNum,
                    OtherAccidentLossHour = safetyData.OtherAccidentLossHour,
                    RestrictedWorkPersonNum = safetyData.WorkLimitNum,
                    RestrictedWorkLossHour = safetyData.RestrictedWorkLossHour,
                    MedicalTreatmentPersonNum = safetyData.MedicalTreatmentNum,
                    MedicalTreatmentLossHour = safetyData.MedicalTreatmentLossHour,
                    FireNum = safetyData.FireNum,
                    ExplosionNum = safetyData.ExplosionNum,
                    TrafficNum = safetyData.TrafficNum,
                    EquipmentNum = safetyData.EquipmentNum,
                    QualityNum = 0,
                    OtherNum = 0,
                    FirstAidDressingsNum = safetyData.FirstAidNum,
                    AttemptedEventNum = safetyData.AttemptedAccidentNum,
                    LossDayNum = safetyData.LossDay
                };
                BLL.ProjectMillionsMonthlyReportService.AddMillionsMonthlyReport(millionsMonthlyReport);
                //安全生产数据季报
                int quarters = 0, month = months.Month;
                if (month == 3)
                {
                    quarters = 1;
                }
                else if (month == 6)
                {
                    quarters = 2;
                }
                else if (month == 9)
                {
                    quarters = 3;
                }
                else if (month == 12)
                {
                    quarters = 4;
                }
                if (quarters > 0)
                {
                    Model.InformationProject_SafetyQuarterlyReport oldSafetyQuarterlyReport = (from x in Funs.DB.InformationProject_SafetyQuarterlyReport
                                                                                               where x.ProjectId == this.ProjectId && x.YearId == months.Year && x.Quarters == quarters
                                                                                               select x).FirstOrDefault();
                    if (oldSafetyQuarterlyReport != null)
                    {
                        BLL.ProjectSafetyQuarterlyReportService.DeleteSafetyQuarterlyReportById(oldSafetyQuarterlyReport.SafetyQuarterlyReportId);
                    }
                    Model.InformationProject_SafetyQuarterlyReport safetyQuarterlyReport = new Model.InformationProject_SafetyQuarterlyReport
                    {
                        SafetyQuarterlyReportId = SQLHelper.GetNewID(typeof(Model.InformationProject_SafetyQuarterlyReport)),
                        ProjectId = this.ProjectId,
                        UnitId = this.CurrUser.UnitId,
                        YearId = months.Year,
                        Quarters = quarters,
                        TotalInWorkHours = safetyData.ManHours,
                        TotalOutWorkHours = safetyData.LossHours
                    };
                    if (safetyData.ManHours.HasValue && safetyData.ManHours != 0)
                    {
                        safetyQuarterlyReport.WorkHoursLossRate = decimal.Round(Convert.ToDecimal(safetyData.LossHours) / Convert.ToDecimal(safetyData.ManHours) * 1000000, 2);
                    }
                    else
                    {
                        safetyQuarterlyReport.WorkHoursLossRate = 0;
                    }
                    safetyQuarterlyReport.WorkHoursAccuracy = 100;
                    safetyQuarterlyReport.MainBusinessIncome = safetyData.MainBusinessIncome;
                    safetyQuarterlyReport.ConstructionRevenue = safetyData.ConstructionIncome;
                    if (safetyData.ManHours.HasValue && safetyData.ManHours != 0)
                    {
                        safetyQuarterlyReport.UnitTimeIncome = decimal.Round(Convert.ToDecimal(safetyData.MainBusinessIncome) * 100000000 / Convert.ToDecimal(safetyData.ManHours), 2);
                    }
                    else
                    {
                        safetyQuarterlyReport.UnitTimeIncome = 0;
                    }
                    if (safetyData.MainBusinessIncome.HasValue && safetyData.MainBusinessIncome != 0)
                    {
                        safetyQuarterlyReport.BillionsOutputMortality = decimal.Round(Convert.ToDecimal(safetyData.DeathPersonNum) / Convert.ToDecimal(safetyData.MainBusinessIncome) * 100000000 * 100, 2);
                    }
                    else
                    {
                        safetyQuarterlyReport.BillionsOutputMortality = 0;
                    }
                    safetyQuarterlyReport.MajorFireAccident = safetyData.FireNum;
                    safetyQuarterlyReport.MajorEquipAccident = safetyData.EquipmentNum;
                    if (safetyData.MainBusinessIncome.HasValue && safetyData.MainBusinessIncome != 0)
                    {
                        safetyQuarterlyReport.AccidentFrequency = decimal.Round(Convert.ToDecimal(safetyData.DeathNum + safetyData.SeriousInjuredNum) / Convert.ToDecimal(safetyData.MainBusinessIncome) * 100000000, 2);
                    }
                    else
                    {
                        safetyQuarterlyReport.AccidentFrequency = 0;
                    }
                    safetyQuarterlyReport.SeriousInjuryAccident = safetyData.DeathNum + safetyData.SeriousInjuredNum;
                    safetyQuarterlyReport.FireAccident = safetyData.FireNum;
                    safetyQuarterlyReport.EquipmentAccident = safetyData.EquipmentNum;
                    safetyQuarterlyReport.PoisoningAndInjuries = safetyData.OccupationalDiseasesNum;
                    safetyQuarterlyReport.ProductionSafetyInTotal = Funs.GetNewIntOrZero(((safetyData.PaidForMoney + safetyData.HasBeenChargedMoney) * 10000).ToString());
                    safetyQuarterlyReport.ProtectionInput = Funs.GetNewDecimalOrZero((safetyQuarterlyReport.ProductionSafetyInTotal * 0.65).ToString());
                    safetyQuarterlyReport.LaboAndHealthIn = Funs.GetNewDecimalOrZero((safetyQuarterlyReport.ProductionSafetyInTotal * 0.2).ToString());
                    safetyQuarterlyReport.TechnologyProgressIn = Funs.GetNewDecimalOrZero((safetyQuarterlyReport.ProductionSafetyInTotal * 0.05).ToString());
                    safetyQuarterlyReport.EducationTrainIn = Funs.GetNewDecimalOrZero((safetyQuarterlyReport.ProductionSafetyInTotal * 0.05).ToString());
                    if (safetyData.MainBusinessIncome.HasValue && safetyData.MainBusinessIncome != 0)
                    {
                        safetyQuarterlyReport.ProjectCostRate = decimal.Round(Convert.ToDecimal(safetyQuarterlyReport.ProductionSafetyInTotal) / (Convert.ToDecimal(safetyData.MainBusinessIncome) * 100000000), 2);
                    }
                    else
                    {
                        safetyQuarterlyReport.ProjectCostRate = 0;
                    }
                    if (safetyData.ManHours.HasValue && safetyData.ManHours != 0)
                    {
                        safetyQuarterlyReport.ProductionInput = decimal.Round(Convert.ToDecimal(safetyQuarterlyReport.ProductionSafetyInTotal) / 10000 / Convert.ToDecimal(safetyData.ManHours) * 1000000, 2);
                    }
                    else
                    {
                        safetyQuarterlyReport.ProductionInput = 0;
                    }
                    if (safetyData.ConstructionIncome.HasValue && safetyData.ConstructionIncome != 0)
                    {
                        safetyQuarterlyReport.Revenue = decimal.Round(Convert.ToDecimal(safetyQuarterlyReport.ProductionSafetyInTotal) / Convert.ToDecimal(safetyData.ConstructionIncome) * 100000000, 2);
                    }
                    else
                    {
                        safetyQuarterlyReport.Revenue = 0;
                    }
                    safetyQuarterlyReport.CorporateDirectorEdu = 0;
                    safetyQuarterlyReport.ProjectLeaderEdu = 0;
                    safetyQuarterlyReport.FullTimeEdu = 0;
                    //safetyQuarterlyReport.ThreeKidsEduRate =
                    //safetyQuarterlyReport.UplinReportRate =
                    //safetyQuarterlyReport.Remarks =
                    //safetyQuarterlyReport.KeyEquipmentTotal =
                    //safetyQuarterlyReport.KeyEquipmentReportCount =
                    //safetyQuarterlyReport.ChemicalAreaProjectCount =
                    //safetyQuarterlyReport.HarmfulMediumCoverCount =
                    //safetyQuarterlyReport.HarmfulMediumCoverRate =
                    safetyQuarterlyReport.CompileMan = this.CurrUser.UserId;
                    safetyQuarterlyReport.CompileDate = DateTime.Now;
                    safetyQuarterlyReport.States = BLL.Const.State_2;
                    BLL.ProjectSafetyQuarterlyReportService.AddSafetyQuarterlyReport(safetyQuarterlyReport);
                    //应急演练开展情况季报
                    Model.InformationProject_DrillConductedQuarterlyReport oldDrillConductedQuarterlyReport = (from x in Funs.DB.InformationProject_DrillConductedQuarterlyReport
                                                                                               where x.ProjectId == this.ProjectId && x.YearId == months.Year && x.Quarter == quarters
                                                                                               select x).FirstOrDefault();
                    if (oldSafetyQuarterlyReport != null)
                    {
                        BLL.ProjectDrillConductedQuarterlyReportItemService.DeleteDrillConductedQuarterlyReportItemList(oldDrillConductedQuarterlyReport.DrillConductedQuarterlyReportId);
                        BLL.ProjectDrillConductedQuarterlyReportService.DeleteDrillConductedQuarterlyReportById(oldDrillConductedQuarterlyReport.DrillConductedQuarterlyReportId);
                    }
                    Model.InformationProject_DrillConductedQuarterlyReport drillConductedQuarterlyReport = new Model.InformationProject_DrillConductedQuarterlyReport
                    {
                        DrillConductedQuarterlyReportId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillConductedQuarterlyReport)),
                        ProjectId = this.ProjectId,
                        UnitId = this.CurrUser.UnitId,
                        YearId = months.Year,
                        Quarter = quarters,
                        CompileMan = this.CurrUser.UserId,
                        CompileDate = DateTime.Now,
                        States = BLL.Const.State_2
                    };
                    BLL.ProjectDrillConductedQuarterlyReportService.AddDrillConductedQuarterlyReport(drillConductedQuarterlyReport);
                    Model.InformationProject_DrillConductedQuarterlyReportItem item = new Model.InformationProject_DrillConductedQuarterlyReportItem
                    {
                        DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_DrillConductedQuarterlyReportItem)),
                        DrillConductedQuarterlyReportId = drillConductedQuarterlyReport.DrillConductedQuarterlyReportId,
                        TotalConductCount = safetyData.EmergencyDrillNum,
                        TotalPeopleCount = safetyData.ParticipantsNum,
                        TotalInvestment = safetyData.DrillInput * 10000
                    };
                    BLL.ProjectDrillConductedQuarterlyReportItemService.AddDrillConductedQuarterlyReportItem(item);
                }
            }
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
                Model.Manager_MonthReportD monthReport = new Model.Manager_MonthReportD
                {
                    MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReportD)),
                    MonthReportCode = this.txtMonthReportCode.Text,
                    ProjectId = this.ProjectId,
                    MonthReportDate = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                    Months = Convert.ToDateTime(this.txtReportMonths.Text + "-1"),
                    ReportMan = this.CurrUser.UserId
                };
                monthReport.MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReport));
                this.MonthReportId = monthReport.MonthReportId;
                BLL.MonthReportDService.AddMonthReport(monthReport);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "增加安全生产月报", monthReport.MonthReportId);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerMonthReport&menuId={1}", this.MonthReportId, BLL.Const.ProjectManagerMonthDMenuId)));
        }
        #endregion
    }
}