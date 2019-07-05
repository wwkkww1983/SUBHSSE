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
    public partial class MonthReportDView : PageBase
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

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerMonthReport&menuId={1}&type=-1", this.MonthReportId, BLL.Const.ProjectManagerMonthDMenuId)));
        }
        #endregion
    }
}