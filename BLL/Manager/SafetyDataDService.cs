using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SafetyDataDService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取安全生产数据在线月报
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static Model.Manager_SafetyDataD GetSafetyDataDByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_SafetyDataD where x.MonthReportId == monthReportId select x).FirstOrDefault();
        }

        /// <summary>
        /// 增加安全生产数据在线月报
        /// </summary>
        /// <param name="safetyData">安全生产数据在线月报实体</param>
        public static void AddSafetyDataD(Model.Manager_SafetyDataD safetyData)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_SafetyDataD));
            Model.Manager_SafetyDataD newSafetyDataD = new Model.Manager_SafetyDataD
            {
                SafetyDataId = newKeyID,
                MonthReportId = safetyData.MonthReportId,
                ThisUnitPersonNum = safetyData.ThisUnitPersonNum,
                ThisUnitHSEPersonNum = safetyData.ThisUnitHSEPersonNum,
                SubUnitPersonNum = safetyData.SubUnitPersonNum,
                SubUnitHSEPersonNum = safetyData.SubUnitHSEPersonNum,
                ManHours = safetyData.ManHours,
                HSEManHours = safetyData.HSEManHours,
                LossHours = safetyData.LossHours,
                LossDay = safetyData.LossDay,
                DeathNum = safetyData.DeathNum,
                DeathPersonNum = safetyData.DeathPersonNum,
                SeriousInjuredNum = safetyData.SeriousInjuredNum,
                SeriousInjuriesPersonNum = safetyData.SeriousInjuriesPersonNum,
                SeriousInjuriesLossHour = safetyData.SeriousInjuriesLossHour,
                MinorInjuredNum = safetyData.MinorInjuredNum,
                MinorAccidentPersonNum = safetyData.MinorAccidentPersonNum,
                MinorAccidentLossHour = safetyData.MinorAccidentLossHour,
                OtherNum = safetyData.OtherNum,
                OtherAccidentPersonNum = safetyData.OtherAccidentPersonNum,
                OtherAccidentLossHour = safetyData.OtherAccidentLossHour,
                MedicalTreatmentNum = safetyData.MedicalTreatmentNum,
                MedicalTreatmentLossHour = safetyData.MedicalTreatmentLossHour,
                WorkLimitNum = safetyData.WorkLimitNum,
                RestrictedWorkLossHour = safetyData.RestrictedWorkLossHour,
                FirstAidNum = safetyData.FirstAidNum,
                OccupationalDiseasesNum = safetyData.OccupationalDiseasesNum,
                AttemptedAccidentNum = safetyData.AttemptedAccidentNum,
                PersonInjuredLossMoney = safetyData.PersonInjuredLossMoney,
                FireNum = safetyData.FireNum,
                ExplosionNum = safetyData.ExplosionNum,
                TrafficNum = safetyData.TrafficNum,
                EquipmentNum = safetyData.EquipmentNum,
                SiteEnvironmentNum = safetyData.SiteEnvironmentNum,
                TheftCaseNum = safetyData.TheftCaseNum,
                PropertyLossMoney = safetyData.PropertyLossMoney,
                MainBusinessIncome = safetyData.MainBusinessIncome,
                ConstructionIncome = safetyData.ConstructionIncome,
                ProjectVolume = safetyData.ProjectVolume,
                PaidForMoney = safetyData.PaidForMoney,
                ApprovedChargesMoney = safetyData.ApprovedChargesMoney,
                HasBeenChargedMoney = safetyData.HasBeenChargedMoney,
                WeekMeetingNum = safetyData.WeekMeetingNum,
                CommitteeMeetingNum = safetyData.CommitteeMeetingNum,
                TrainPersonNum = safetyData.TrainPersonNum,
                WeekCheckNum = safetyData.WeekCheckNum,
                HSECheckNum = safetyData.HSECheckNum,
                SpecialCheckNum = safetyData.SpecialCheckNum,
                EquipmentHSEInspectionNum = safetyData.EquipmentHSEInspectionNum,
                LicenseNum = safetyData.LicenseNum,
                SolutionNum = safetyData.SolutionNum,
                ReleaseRectifyNum = safetyData.ReleaseRectifyNum,
                CloseRectifyNum = safetyData.CloseRectifyNum,
                ReleasePunishNum = safetyData.ReleasePunishNum,
                PunishMoney = safetyData.PunishMoney,
                IncentiveMoney = safetyData.IncentiveMoney,
                EmergencyDrillNum = safetyData.EmergencyDrillNum,
                ParticipantsNum = safetyData.ParticipantsNum,
                DrillInput = safetyData.DrillInput,
                DrillTypes = safetyData.DrillTypes
            };

            db.Manager_SafetyDataD.InsertOnSubmit(newSafetyDataD);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除安全生产数据在线月报
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteSafetyDataDByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_SafetyDataD where x.MonthReportId == monthReportId select x).FirstOrDefault();
            if (q != null)
            {
                db.Manager_SafetyDataD.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
