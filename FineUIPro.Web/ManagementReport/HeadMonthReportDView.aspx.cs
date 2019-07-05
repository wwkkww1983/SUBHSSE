using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ManagementReport
{
    public partial class HeadMonthReportDView : PageBase
    {
        #region 定义项
        private static DateTime months;
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
                string monthStr = Request.Params["months"];
                months = Funs.GetNewDateTimeOrNow(monthStr);
                List<Model.Base_Project> projects = BLL.ProjectService.GetAllProjectDropDownList();
                int totalThisUnitPersonNum = 0, totalThisUnitHSEPersonNum = 0, totalSubUnitPersonNum = 0, totalSubUnitHSEPersonNum = 0, totalManHours = 0,
                    totalHSEManHours = 0, totalLossHours = 0, totalLossDay = 0, totalDeathNum = 0, totalDeathPersonNum = 0, totalSeriousInjuredNum = 0,
                    totalSeriousInjuriesPersonNum = 0, totalSeriousInjuriesLossHour = 0, totalMinorInjuredNum = 0, totalMinorAccidentPersonNum = 0,
                    totalMinorAccidentLossHour = 0, totalOtherNum = 0, totalOtherAccidentPersonNum = 0, totalOtherAccidentLossHour = 0,
                    totalMedicalTreatmentNum = 0, totalMedicalTreatmentLossHour = 0, totalWorkLimitNum = 0, totalRestrictedWorkLossHour = 0,
                    totalFirstAidNum = 0, totalOccupationalDiseasesNum = 0, totalAttemptedAccidentNum = 0, totalFireNum = 0,
                    totalExplosionNum = 0, totalTrafficNum = 0, totalEquipmentNum = 0, totalSiteEnvironmentNum = 0, totalTheftCaseNum = 0,
                    totalTrainPersonNum = 0, totalReleaseRectifyNum = 0, totalCloseRectifyNum = 0, totalReleasePunishNum = 0,
                    totalEmergencyDrillNum = 0, totalParticipantsNum = 0;
                decimal totalPersonInjuredLossMoney = 0, totalPropertyLossMoney = 0, totalMainBusinessIncome = 0, totalConstructionIncome = 0,
                    totalProjectVolume = 0, totalPaidForMoney = 0, totalApprovedChargesMoney = 0, totalHasBeenChargedMoney = 0,
                    totalPunishMoney = 0, totalIncentiveMoney = 0, totalDrillInput = 0;
                string projectNames = string.Empty;
                foreach (var project in projects)
                {
                    Model.Manager_MonthReportD monthReport = BLL.MonthReportDService.GetMonthReportByMonths(months, project.ProjectId);
                    if (monthReport != null)
                    {
                        Model.Manager_SafetyDataD safetyData = BLL.SafetyDataDService.GetSafetyDataDByMonthReportId(monthReport.MonthReportId);
                        if (safetyData != null)
                        {
                            projectNames += project.ProjectName + ",";
                            if (safetyData.ThisUnitPersonNum != null)
                            {
                                totalThisUnitPersonNum += Funs.GetNewIntOrZero(safetyData.ThisUnitPersonNum.ToString());
                            }
                            if (safetyData.ThisUnitHSEPersonNum != null)
                            {
                                totalThisUnitHSEPersonNum += Funs.GetNewIntOrZero(safetyData.ThisUnitHSEPersonNum.ToString());
                            }
                            if (safetyData.SubUnitPersonNum != null)
                            {
                                totalSubUnitPersonNum += Funs.GetNewIntOrZero(safetyData.SubUnitPersonNum.ToString());
                            }
                            if (safetyData.SubUnitHSEPersonNum != null)
                            {
                                totalSubUnitHSEPersonNum += Funs.GetNewIntOrZero(safetyData.SubUnitHSEPersonNum.ToString());
                            }
                            if (safetyData.ManHours != null)
                            {
                                totalManHours += Funs.GetNewIntOrZero(safetyData.ManHours.ToString());
                            }
                            if (safetyData.HSEManHours != null)
                            {
                                totalHSEManHours += Funs.GetNewIntOrZero(safetyData.HSEManHours.ToString());
                            }
                            if (safetyData.LossHours != null)
                            {
                                totalLossHours += Funs.GetNewIntOrZero(safetyData.LossHours.ToString());
                            }
                            if (safetyData.LossDay != null)
                            {
                                totalLossDay += Funs.GetNewIntOrZero(safetyData.LossDay.ToString());
                            }
                            if (safetyData.DeathNum != null)
                            {
                                totalDeathNum += Funs.GetNewIntOrZero(safetyData.DeathNum.ToString());
                            }
                            if (safetyData.DeathPersonNum != null)
                            {
                                totalDeathPersonNum += Funs.GetNewIntOrZero(safetyData.DeathPersonNum.ToString());
                            }
                            if (safetyData.SeriousInjuredNum != null)
                            {
                                totalSeriousInjuredNum += Funs.GetNewIntOrZero(safetyData.SeriousInjuredNum.ToString());
                            }
                            if (safetyData.SeriousInjuriesPersonNum != null)
                            {
                                totalSeriousInjuriesPersonNum += Funs.GetNewIntOrZero(safetyData.SeriousInjuriesPersonNum.ToString());
                            }
                            if (safetyData.SeriousInjuriesLossHour != null)
                            {
                                totalSeriousInjuriesLossHour += Funs.GetNewIntOrZero(safetyData.SeriousInjuriesLossHour.ToString());
                            }
                            if (safetyData.MinorInjuredNum != null)
                            {
                                totalMinorInjuredNum += Funs.GetNewIntOrZero(safetyData.MinorInjuredNum.ToString());
                            }
                            if (safetyData.MinorAccidentPersonNum != null)
                            {
                                totalMinorAccidentPersonNum += Funs.GetNewIntOrZero(safetyData.MinorAccidentPersonNum.ToString());
                            }
                            if (safetyData.MinorAccidentLossHour != null)
                            {
                                totalMinorAccidentLossHour += Funs.GetNewIntOrZero(safetyData.MinorAccidentLossHour.ToString());
                            }
                            if (safetyData.OtherNum != null)
                            {
                                totalOtherNum += Funs.GetNewIntOrZero(safetyData.OtherNum.ToString());
                            }
                            if (safetyData.OtherAccidentPersonNum != null)
                            {
                                totalOtherAccidentPersonNum += Funs.GetNewIntOrZero(safetyData.OtherAccidentPersonNum.ToString());
                            }
                            if (safetyData.OtherAccidentLossHour != null)
                            {
                                totalOtherAccidentLossHour += Funs.GetNewIntOrZero(safetyData.OtherAccidentLossHour.ToString());
                            }
                            if (safetyData.MedicalTreatmentNum != null)
                            {
                                totalMedicalTreatmentNum += Funs.GetNewIntOrZero(safetyData.MedicalTreatmentNum.ToString());
                            }
                            if (safetyData.MedicalTreatmentLossHour != null)
                            {
                                totalMedicalTreatmentLossHour += Funs.GetNewIntOrZero(safetyData.MedicalTreatmentLossHour.ToString());
                            }
                            if (safetyData.WorkLimitNum != null)
                            {
                                totalWorkLimitNum += Funs.GetNewIntOrZero(safetyData.WorkLimitNum.ToString());
                            }
                            if (safetyData.RestrictedWorkLossHour != null)
                            {
                                totalRestrictedWorkLossHour += Funs.GetNewIntOrZero(safetyData.RestrictedWorkLossHour.ToString());
                            }
                            if (safetyData.FirstAidNum != null)
                            {
                                totalFirstAidNum += Funs.GetNewIntOrZero(safetyData.FirstAidNum.ToString());
                            }
                            if (safetyData.OccupationalDiseasesNum != null)
                            {
                                totalOccupationalDiseasesNum += Funs.GetNewIntOrZero(safetyData.OccupationalDiseasesNum.ToString());
                            }
                            if (safetyData.AttemptedAccidentNum != null)
                            {
                                totalAttemptedAccidentNum += Funs.GetNewIntOrZero(safetyData.AttemptedAccidentNum.ToString());
                            }
                            if (safetyData.PersonInjuredLossMoney != null)
                            {
                                totalPersonInjuredLossMoney += Funs.GetNewDecimalOrZero(safetyData.PersonInjuredLossMoney.ToString());
                            }
                            if (safetyData.FireNum != null)
                            {
                                totalFireNum += Funs.GetNewIntOrZero(safetyData.FireNum.ToString());
                            }
                            if (safetyData.ExplosionNum != null)
                            {
                                totalExplosionNum += Funs.GetNewIntOrZero(safetyData.ExplosionNum.ToString());
                            }
                            if (safetyData.TrafficNum != null)
                            {
                                totalTrafficNum += Funs.GetNewIntOrZero(safetyData.TrafficNum.ToString());
                            }
                            if (safetyData.EquipmentNum != null)
                            {
                                totalEquipmentNum += Funs.GetNewIntOrZero(safetyData.EquipmentNum.ToString());
                            }
                            if (safetyData.SiteEnvironmentNum != null)
                            {
                                totalSiteEnvironmentNum += Funs.GetNewIntOrZero(safetyData.SiteEnvironmentNum.ToString());
                            }
                            if (safetyData.TheftCaseNum != null)
                            {
                                totalTheftCaseNum += Funs.GetNewIntOrZero(safetyData.TheftCaseNum.ToString());
                            }
                            if (safetyData.PropertyLossMoney != null)
                            {
                                totalPropertyLossMoney += Funs.GetNewDecimalOrZero(safetyData.PropertyLossMoney.ToString());
                            }
                            if (safetyData.MainBusinessIncome != null)
                            {
                                totalMainBusinessIncome += Funs.GetNewDecimalOrZero(safetyData.MainBusinessIncome.ToString());
                            }
                            if (safetyData.ConstructionIncome != null)
                            {
                                totalConstructionIncome += Funs.GetNewDecimalOrZero(safetyData.ConstructionIncome.ToString());
                            }
                            if (safetyData.ProjectVolume != null)
                            {
                                totalProjectVolume += Funs.GetNewDecimalOrZero(safetyData.ProjectVolume.ToString());
                            }
                            if (safetyData.PaidForMoney != null)
                            {
                                totalPaidForMoney += Funs.GetNewDecimalOrZero(safetyData.PaidForMoney.ToString());
                            }
                            if (safetyData.ApprovedChargesMoney != null)
                            {
                                totalApprovedChargesMoney += Funs.GetNewDecimalOrZero(safetyData.ApprovedChargesMoney.ToString());
                            }
                            if (safetyData.HasBeenChargedMoney != null)
                            {
                                totalHasBeenChargedMoney += Funs.GetNewDecimalOrZero(safetyData.HasBeenChargedMoney.ToString());
                            }
                            if (safetyData.TrainPersonNum != null)
                            {
                                totalTrainPersonNum += Funs.GetNewIntOrZero(safetyData.TrainPersonNum.ToString());
                            }
                            if (safetyData.ReleaseRectifyNum != null)
                            {
                                totalReleaseRectifyNum += Funs.GetNewIntOrZero(safetyData.ReleaseRectifyNum.ToString());
                            }
                            if (safetyData.CloseRectifyNum != null)
                            {
                                totalCloseRectifyNum += Funs.GetNewIntOrZero(safetyData.CloseRectifyNum.ToString());
                            }
                            if (safetyData.ReleasePunishNum != null)
                            {
                                totalReleasePunishNum += Funs.GetNewIntOrZero(safetyData.ReleasePunishNum.ToString());
                            }
                            if (safetyData.PunishMoney != null)
                            {
                                totalPunishMoney += Funs.GetNewDecimalOrZero(safetyData.PunishMoney.ToString());
                            }
                            if (safetyData.IncentiveMoney != null)
                            {
                                totalIncentiveMoney += Funs.GetNewDecimalOrZero(safetyData.IncentiveMoney.ToString());
                            }
                            if (safetyData.EmergencyDrillNum != null)
                            {
                                totalEmergencyDrillNum += Funs.GetNewIntOrZero(safetyData.EmergencyDrillNum.ToString());
                            }
                            if (safetyData.ParticipantsNum != null)
                            {
                                totalParticipantsNum += Funs.GetNewIntOrZero(safetyData.ParticipantsNum.ToString());
                            }
                            if (safetyData.DrillInput != null)
                            {
                                totalDrillInput += Funs.GetNewDecimalOrZero(safetyData.DrillInput.ToString());
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(projectNames))
                {
                    projectNames = projectNames.Substring(0, projectNames.LastIndexOf(","));
                }
                this.txtMonths.Text = string.Format("{0:yyyy-MM-dd}", months);
                this.txtProjectNames.Text = projectNames;
                this.txtThisUnitPersonNum.Text = totalThisUnitPersonNum.ToString();
                this.txtThisUnitHSEPersonNum.Text = totalThisUnitHSEPersonNum.ToString();
                this.txtSubUnitPersonNum.Text = totalSubUnitPersonNum.ToString();
                this.txtSubUnitHSEPersonNum.Text = totalSubUnitHSEPersonNum.ToString();
                this.txtManHours.Text = totalManHours.ToString();
                this.txtHSEManHours.Text = totalHSEManHours.ToString();
                this.txtLossHours.Text = totalLossHours.ToString();
                this.txtLossDay.Text = totalLossDay.ToString();
                this.txtDeathNum.Text = totalDeathNum.ToString();
                this.txtDeathPersonNum.Text = totalDeathPersonNum.ToString();
                this.txtSeriousInjuredNum.Text = totalSeriousInjuredNum.ToString();
                this.txtSeriousInjuriesPersonNum.Text = totalSeriousInjuriesPersonNum.ToString();
                this.txtSeriousInjuriesLossHour.Text = totalSeriousInjuriesLossHour.ToString();
                this.txtMinorInjuredNum.Text = totalMinorInjuredNum.ToString();
                this.txtMinorAccidentPersonNum.Text = totalMinorAccidentPersonNum.ToString();
                this.txtMinorAccidentLossHour.Text = totalMinorAccidentLossHour.ToString();
                this.txtOtherNum.Text = totalOtherNum.ToString();
                this.txtOtherAccidentPersonNum.Text = totalOtherAccidentPersonNum.ToString();
                this.txtOtherAccidentLossHour.Text = totalOtherAccidentLossHour.ToString();
                this.txtMedicalTreatmentNum.Text = totalMedicalTreatmentNum.ToString();
                this.txtMedicalTreatmentLossHour.Text = totalMedicalTreatmentLossHour.ToString();
                this.txtWorkLimitNum.Text = totalWorkLimitNum.ToString();
                this.txtRestrictedWorkLossHour.Text = totalRestrictedWorkLossHour.ToString();
                this.txtFirstAidNum.Text = totalFirstAidNum.ToString();
                this.txtOccupationalDiseasesNum.Text = totalOccupationalDiseasesNum.ToString();
                this.txtAttemptedAccidentNum.Text = totalAttemptedAccidentNum.ToString();
                this.txtPersonInjuredLossMoney.Text = totalPersonInjuredLossMoney.ToString();
                this.txtFireNum.Text = totalFireNum.ToString();
                this.txtExplosionNum.Text = totalExplosionNum.ToString();
                this.txtTrafficNum.Text = totalTrafficNum.ToString();
                this.txtEquipmentNum.Text = totalEquipmentNum.ToString();
                this.txtSiteEnvironmentNum.Text = totalSiteEnvironmentNum.ToString();
                this.txtTheftCaseNum.Text = totalTheftCaseNum.ToString();
                this.txtPropertyLossMoney.Text = totalPropertyLossMoney.ToString();
                this.txtMainBusinessIncome.Text = totalMainBusinessIncome.ToString();
                this.txtConstructionIncome.Text = totalConstructionIncome.ToString();
                this.txtProjectVolume.Text = totalProjectVolume.ToString();
                this.txtPaidForMoney.Text = totalPaidForMoney.ToString();
                this.txtApprovedChargesMoney.Text = totalApprovedChargesMoney.ToString();
                this.txtHasBeenChargedMoney.Text = totalHasBeenChargedMoney.ToString();
                this.txtTrainPersonNum.Text = totalTrainPersonNum.ToString();
                this.txtReleaseRectifyNum.Text = totalReleaseRectifyNum.ToString();
                this.txtCloseRectifyNum.Text = totalCloseRectifyNum.ToString();
                this.txtReleasePunishNum.Text = totalReleasePunishNum.ToString();
                this.txtPunishMoney.Text = totalPunishMoney.ToString();
                this.txtIncentiveMoney.Text = totalIncentiveMoney.ToString();
                this.txtEmergencyDrillNum.Text = totalEmergencyDrillNum.ToString();
                this.txtParticipantsNum.Text = totalParticipantsNum.ToString();
                this.txtDrillInput.Text = totalDrillInput.ToString();
            }
        }
        #endregion
    }
}