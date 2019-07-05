using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全生产数据季报
    /// </summary>
    public static class SafetyQuarterlyReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全生产数据季报
        /// </summary>
        /// <param name="safetyQuarterlyReportId"></param>
        /// <returns></returns>
        public static Model.Information_SafetyQuarterlyReport GetSafetyQuarterlyReportById(string safetyQuarterlyReportId)
        {
            return Funs.DB.Information_SafetyQuarterlyReport.FirstOrDefault(e => e.SafetyQuarterlyReportId == safetyQuarterlyReportId);
        }

        /// <summary>
        /// 安全生产数据季报
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <param name="year">年度</param>
        /// <param name="quarters">季度</param>
        /// <returns>安全生产数据季报</returns>
        public static Model.Information_SafetyQuarterlyReport GetSafetyQuarterlyReportByUnitIdAndYearAndQuarters(string unitId, int year, int quarters)
        {
            return Funs.DB.Information_SafetyQuarterlyReport.FirstOrDefault(e => e.UnitId == unitId && e.Quarters == quarters && e.YearId == year);
        }

        /// <summary>
        /// 根据单位Id获取安全生产数据季报集合
        /// </summary>
        /// <param name="UnitId">单位Id</param>
        /// <returns>安全生产数据季报集合</returns>
        public static List<Model.View_Information_SafetyQuarterlyReport> GetSafetyQuarterlyReportsByUnitId(string UnitId)
        {
            return (from x in Funs.DB.View_Information_SafetyQuarterlyReport where x.UnitId == UnitId orderby x.FillingDate descending select x).ToList();
        }

        /// <summary>
        /// 根据单位、年度、季度获取安全生产数据季报
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="yearId"></param>
        /// <param name="quarters"></param>
        /// <returns></returns>
        public static Model.Information_SafetyQuarterlyReport GetSafetyQuarterlyReportByUnitYearQuarters(string unitId, int yearId, int quarters)
        {
            return Funs.DB.Information_SafetyQuarterlyReport.FirstOrDefault(e => e.UnitId == unitId && e.YearId == yearId && e.Quarters == quarters);
        }

        /// <summary>
        /// 增加安全生产数据季报
        /// </summary>
        /// <param name="safetyQuarterlyReport"></param>
        public static void AddSafetyQuarterlyReport(Model.Information_SafetyQuarterlyReport safetyQuarterlyReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_SafetyQuarterlyReport newSafetyQuarterlyReport = new Model.Information_SafetyQuarterlyReport
            {
                SafetyQuarterlyReportId = safetyQuarterlyReport.SafetyQuarterlyReportId,
                UnitId = safetyQuarterlyReport.UnitId,
                YearId = safetyQuarterlyReport.YearId,
                FillingDate = DateTime.Now,
                Quarters = safetyQuarterlyReport.Quarters,
                TotalInWorkHours = safetyQuarterlyReport.TotalInWorkHours,
                TotalInWorkHoursRemark = safetyQuarterlyReport.TotalInWorkHoursRemark,
                TotalOutWorkHours = safetyQuarterlyReport.TotalOutWorkHours,
                TotalOutWorkHoursRemark = safetyQuarterlyReport.TotalOutWorkHoursRemark,
                WorkHoursLossRate = safetyQuarterlyReport.WorkHoursLossRate,
                WorkHoursLossRateRemark = safetyQuarterlyReport.WorkHoursLossRateRemark,
                WorkHoursAccuracy = safetyQuarterlyReport.WorkHoursAccuracy,
                WorkHoursAccuracyRemark = safetyQuarterlyReport.WorkHoursAccuracyRemark,
                MainBusinessIncome = safetyQuarterlyReport.MainBusinessIncome,
                MainBusinessIncomeRemark = safetyQuarterlyReport.MainBusinessIncomeRemark,
                ConstructionRevenue = safetyQuarterlyReport.ConstructionRevenue,
                ConstructionRevenueRemark = safetyQuarterlyReport.ConstructionRevenueRemark,
                UnitTimeIncome = safetyQuarterlyReport.UnitTimeIncome,
                UnitTimeIncomeRemark = safetyQuarterlyReport.UnitTimeIncomeRemark,
                BillionsOutputMortality = safetyQuarterlyReport.BillionsOutputMortality,
                BillionsOutputMortalityRemark = safetyQuarterlyReport.BillionsOutputMortalityRemark,
                MajorFireAccident = safetyQuarterlyReport.MajorFireAccident,
                MajorFireAccidentRemark = safetyQuarterlyReport.MajorFireAccidentRemark,
                MajorEquipAccident = safetyQuarterlyReport.MajorEquipAccident,
                MajorEquipAccidentRemark = safetyQuarterlyReport.MajorEquipAccidentRemark,
                AccidentFrequency = safetyQuarterlyReport.AccidentFrequency,
                AccidentFrequencyRemark = safetyQuarterlyReport.AccidentFrequencyRemark,
                SeriousInjuryAccident = safetyQuarterlyReport.SeriousInjuryAccident,
                SeriousInjuryAccidentRemark = safetyQuarterlyReport.SeriousInjuryAccidentRemark,
                FireAccident = safetyQuarterlyReport.FireAccident,
                FireAccidentRemark = safetyQuarterlyReport.FireAccidentRemark,
                EquipmentAccident = safetyQuarterlyReport.EquipmentAccident,
                EquipmentAccidentRemark = safetyQuarterlyReport.EquipmentAccidentRemark,
                PoisoningAndInjuries = safetyQuarterlyReport.PoisoningAndInjuries,
                PoisoningAndInjuriesRemark = safetyQuarterlyReport.PoisoningAndInjuriesRemark,
                ProductionSafetyInTotal = safetyQuarterlyReport.ProductionSafetyInTotal,
                ProductionSafetyInTotalRemark = safetyQuarterlyReport.ProductionSafetyInTotalRemark,
                ProtectionInput = safetyQuarterlyReport.ProtectionInput,
                ProtectionInputRemark = safetyQuarterlyReport.ProtectionInputRemark,
                LaboAndHealthIn = safetyQuarterlyReport.LaboAndHealthIn,
                LaborAndHealthInRemark = safetyQuarterlyReport.LaborAndHealthInRemark,
                TechnologyProgressIn = safetyQuarterlyReport.TechnologyProgressIn,
                TechnologyProgressInRemark = safetyQuarterlyReport.TechnologyProgressInRemark,
                EducationTrainIn = safetyQuarterlyReport.EducationTrainIn,
                EducationTrainInRemark = safetyQuarterlyReport.EducationTrainInRemark,
                ProjectCostRate = safetyQuarterlyReport.ProjectCostRate,
                ProjectCostRateRemark = safetyQuarterlyReport.ProjectCostRateRemark,
                ProductionInput = safetyQuarterlyReport.ProductionInput,
                ProductionInputRemark = safetyQuarterlyReport.ProductionInputRemark,
                Revenue = safetyQuarterlyReport.Revenue,
                RevenueRemark = safetyQuarterlyReport.RevenueRemark,
                FullTimeMan = safetyQuarterlyReport.FullTimeMan,
                FullTimeManRemark = safetyQuarterlyReport.FullTimeManRemark,
                FullTimeManAttachUrl = safetyQuarterlyReport.FullTimeManAttachUrl,
                PMMan = safetyQuarterlyReport.PMMan,
                PMManRemark = safetyQuarterlyReport.PMManRemark,
                PMManAttachUrl = safetyQuarterlyReport.PMManAttachUrl,
                CorporateDirectorEdu = safetyQuarterlyReport.CorporateDirectorEdu,
                CorporateDirectorEduRemark = safetyQuarterlyReport.CorporateDirectorEduRemark,
                ProjectLeaderEdu = safetyQuarterlyReport.ProjectLeaderEdu,
                ProjectLeaderEduRemark = safetyQuarterlyReport.ProjectLeaderEduRemark,
                FullTimeEdu = safetyQuarterlyReport.FullTimeEdu,
                FullTimeEduRemark = safetyQuarterlyReport.FullTimeEduRemark,
                ThreeKidsEduRate = safetyQuarterlyReport.ThreeKidsEduRate,
                ThreeKidsEduRateRemark = safetyQuarterlyReport.ThreeKidsEduRateRemark,
                UplinReportRate = safetyQuarterlyReport.UplinReportRate,
                UplinReportRateRemark = safetyQuarterlyReport.UplinReportRateRemark,
                Remarks = safetyQuarterlyReport.Remarks,
                CompileMan = safetyQuarterlyReport.CompileMan,
                UpState = safetyQuarterlyReport.UpState,
                HandleState = safetyQuarterlyReport.HandleState,
                HandleMan = safetyQuarterlyReport.HandleMan,
                KeyEquipmentTotal = safetyQuarterlyReport.KeyEquipmentTotal,
                KeyEquipmentTotalRemark = safetyQuarterlyReport.KeyEquipmentTotalRemark,
                KeyEquipmentReportCount = safetyQuarterlyReport.KeyEquipmentReportCount,
                KeyEquipmentReportCountRemark = safetyQuarterlyReport.KeyEquipmentReportCountRemark,
                ChemicalAreaProjectCount = safetyQuarterlyReport.ChemicalAreaProjectCount,
                ChemicalAreaProjectCountRemark = safetyQuarterlyReport.ChemicalAreaProjectCountRemark,
                HarmfulMediumCoverCount = safetyQuarterlyReport.HarmfulMediumCoverCount,
                HarmfulMediumCoverCountRemark = safetyQuarterlyReport.HarmfulMediumCoverCountRemark,
                HarmfulMediumCoverRate = safetyQuarterlyReport.HarmfulMediumCoverRate,
                HarmfulMediumCoverRateRemark = safetyQuarterlyReport.HarmfulMediumCoverRateRemark
            };
            db.Information_SafetyQuarterlyReport.InsertOnSubmit(newSafetyQuarterlyReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全生产数据季报
        /// </summary>
        /// <param name="safetyQuarterlyReport"></param>
        public static void UpdateSafetyQuarterlyReport(Model.Information_SafetyQuarterlyReport safetyQuarterlyReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_SafetyQuarterlyReport newSafetyQuarterlyReport = db.Information_SafetyQuarterlyReport.FirstOrDefault(e => e.SafetyQuarterlyReportId == safetyQuarterlyReport.SafetyQuarterlyReportId);
            if (newSafetyQuarterlyReport != null)
            {
                newSafetyQuarterlyReport.UnitId = safetyQuarterlyReport.UnitId;
                newSafetyQuarterlyReport.YearId = safetyQuarterlyReport.YearId;
                newSafetyQuarterlyReport.Quarters = safetyQuarterlyReport.Quarters;
                newSafetyQuarterlyReport.TotalInWorkHours = safetyQuarterlyReport.TotalInWorkHours;
                newSafetyQuarterlyReport.TotalInWorkHoursRemark = safetyQuarterlyReport.TotalInWorkHoursRemark;
                newSafetyQuarterlyReport.TotalOutWorkHours = safetyQuarterlyReport.TotalOutWorkHours;
                newSafetyQuarterlyReport.TotalOutWorkHoursRemark = safetyQuarterlyReport.TotalOutWorkHoursRemark;
                newSafetyQuarterlyReport.WorkHoursLossRate = safetyQuarterlyReport.WorkHoursLossRate;
                newSafetyQuarterlyReport.WorkHoursLossRateRemark = safetyQuarterlyReport.WorkHoursLossRateRemark;
                newSafetyQuarterlyReport.WorkHoursAccuracy = safetyQuarterlyReport.WorkHoursAccuracy;
                newSafetyQuarterlyReport.WorkHoursAccuracyRemark = safetyQuarterlyReport.WorkHoursAccuracyRemark;
                newSafetyQuarterlyReport.MainBusinessIncome = safetyQuarterlyReport.MainBusinessIncome;
                newSafetyQuarterlyReport.MainBusinessIncomeRemark = safetyQuarterlyReport.MainBusinessIncomeRemark;
                newSafetyQuarterlyReport.ConstructionRevenue = safetyQuarterlyReport.ConstructionRevenue;
                newSafetyQuarterlyReport.ConstructionRevenueRemark = safetyQuarterlyReport.ConstructionRevenueRemark;
                newSafetyQuarterlyReport.UnitTimeIncome = safetyQuarterlyReport.UnitTimeIncome;
                newSafetyQuarterlyReport.UnitTimeIncomeRemark = safetyQuarterlyReport.UnitTimeIncomeRemark;
                newSafetyQuarterlyReport.BillionsOutputMortality = safetyQuarterlyReport.BillionsOutputMortality;
                newSafetyQuarterlyReport.BillionsOutputMortalityRemark = safetyQuarterlyReport.BillionsOutputMortalityRemark;
                newSafetyQuarterlyReport.MajorFireAccident = safetyQuarterlyReport.MajorFireAccident;
                newSafetyQuarterlyReport.MajorFireAccidentRemark = safetyQuarterlyReport.MajorFireAccidentRemark;
                newSafetyQuarterlyReport.MajorEquipAccident = safetyQuarterlyReport.MajorEquipAccident;
                newSafetyQuarterlyReport.MajorEquipAccidentRemark = safetyQuarterlyReport.MajorEquipAccidentRemark;
                newSafetyQuarterlyReport.AccidentFrequency = safetyQuarterlyReport.AccidentFrequency;
                newSafetyQuarterlyReport.AccidentFrequencyRemark = safetyQuarterlyReport.AccidentFrequencyRemark;
                newSafetyQuarterlyReport.SeriousInjuryAccident = safetyQuarterlyReport.SeriousInjuryAccident;
                newSafetyQuarterlyReport.SeriousInjuryAccidentRemark = safetyQuarterlyReport.SeriousInjuryAccidentRemark;
                newSafetyQuarterlyReport.FireAccident = safetyQuarterlyReport.FireAccident;
                newSafetyQuarterlyReport.FireAccidentRemark = safetyQuarterlyReport.FireAccidentRemark;
                newSafetyQuarterlyReport.EquipmentAccident = safetyQuarterlyReport.EquipmentAccident;
                newSafetyQuarterlyReport.EquipmentAccidentRemark = safetyQuarterlyReport.EquipmentAccidentRemark;
                newSafetyQuarterlyReport.PoisoningAndInjuries = safetyQuarterlyReport.PoisoningAndInjuries;
                newSafetyQuarterlyReport.PoisoningAndInjuriesRemark = safetyQuarterlyReport.PoisoningAndInjuriesRemark;
                newSafetyQuarterlyReport.ProductionSafetyInTotal = safetyQuarterlyReport.ProductionSafetyInTotal;
                newSafetyQuarterlyReport.ProductionSafetyInTotalRemark = safetyQuarterlyReport.ProductionSafetyInTotalRemark;
                newSafetyQuarterlyReport.ProtectionInput = safetyQuarterlyReport.ProtectionInput;
                newSafetyQuarterlyReport.ProtectionInputRemark = safetyQuarterlyReport.ProtectionInputRemark;
                newSafetyQuarterlyReport.LaboAndHealthIn = safetyQuarterlyReport.LaboAndHealthIn;
                newSafetyQuarterlyReport.LaborAndHealthInRemark = safetyQuarterlyReport.LaborAndHealthInRemark;
                newSafetyQuarterlyReport.TechnologyProgressIn = safetyQuarterlyReport.TechnologyProgressIn;
                newSafetyQuarterlyReport.TechnologyProgressInRemark = safetyQuarterlyReport.TechnologyProgressInRemark;
                newSafetyQuarterlyReport.EducationTrainIn = safetyQuarterlyReport.EducationTrainIn;
                newSafetyQuarterlyReport.EducationTrainInRemark = safetyQuarterlyReport.EducationTrainInRemark;
                newSafetyQuarterlyReport.ProjectCostRate = safetyQuarterlyReport.ProjectCostRate;
                newSafetyQuarterlyReport.ProjectCostRateRemark = safetyQuarterlyReport.ProjectCostRateRemark;
                newSafetyQuarterlyReport.ProductionInput = safetyQuarterlyReport.ProductionInput;
                newSafetyQuarterlyReport.ProductionInputRemark = safetyQuarterlyReport.ProductionInputRemark;
                newSafetyQuarterlyReport.Revenue = safetyQuarterlyReport.Revenue;
                newSafetyQuarterlyReport.RevenueRemark = safetyQuarterlyReport.RevenueRemark;
                newSafetyQuarterlyReport.FullTimeMan = safetyQuarterlyReport.FullTimeMan;
                newSafetyQuarterlyReport.FullTimeManRemark = safetyQuarterlyReport.FullTimeManRemark;
                newSafetyQuarterlyReport.FullTimeManAttachUrl = safetyQuarterlyReport.FullTimeManAttachUrl;
                newSafetyQuarterlyReport.PMMan = safetyQuarterlyReport.PMMan;
                newSafetyQuarterlyReport.PMManRemark = safetyQuarterlyReport.PMManRemark;
                newSafetyQuarterlyReport.PMManAttachUrl = safetyQuarterlyReport.PMManAttachUrl;
                newSafetyQuarterlyReport.CorporateDirectorEdu = safetyQuarterlyReport.CorporateDirectorEdu;
                newSafetyQuarterlyReport.CorporateDirectorEduRemark = safetyQuarterlyReport.CorporateDirectorEduRemark;
                newSafetyQuarterlyReport.ProjectLeaderEdu = safetyQuarterlyReport.ProjectLeaderEdu;
                newSafetyQuarterlyReport.ProjectLeaderEduRemark = safetyQuarterlyReport.ProjectLeaderEduRemark;
                newSafetyQuarterlyReport.FullTimeEdu = safetyQuarterlyReport.FullTimeEdu;
                newSafetyQuarterlyReport.FullTimeEduRemark = safetyQuarterlyReport.FullTimeEduRemark;
                newSafetyQuarterlyReport.ThreeKidsEduRate = safetyQuarterlyReport.ThreeKidsEduRate;
                newSafetyQuarterlyReport.ThreeKidsEduRateRemark = safetyQuarterlyReport.ThreeKidsEduRateRemark;
                newSafetyQuarterlyReport.UplinReportRate = safetyQuarterlyReport.UplinReportRate;
                newSafetyQuarterlyReport.UplinReportRateRemark = safetyQuarterlyReport.UplinReportRateRemark;
                newSafetyQuarterlyReport.Remarks = safetyQuarterlyReport.Remarks;
                newSafetyQuarterlyReport.UpState = safetyQuarterlyReport.UpState;
                newSafetyQuarterlyReport.HandleState = safetyQuarterlyReport.HandleState;
                newSafetyQuarterlyReport.HandleMan = safetyQuarterlyReport.HandleMan;
                newSafetyQuarterlyReport.KeyEquipmentTotal = safetyQuarterlyReport.KeyEquipmentTotal;
                newSafetyQuarterlyReport.KeyEquipmentTotalRemark = safetyQuarterlyReport.KeyEquipmentTotalRemark;
                newSafetyQuarterlyReport.KeyEquipmentReportCount = safetyQuarterlyReport.KeyEquipmentReportCount;
                newSafetyQuarterlyReport.KeyEquipmentReportCountRemark = safetyQuarterlyReport.KeyEquipmentReportCountRemark;
                newSafetyQuarterlyReport.ChemicalAreaProjectCount = safetyQuarterlyReport.ChemicalAreaProjectCount;
                newSafetyQuarterlyReport.ChemicalAreaProjectCountRemark = safetyQuarterlyReport.ChemicalAreaProjectCountRemark;
                newSafetyQuarterlyReport.HarmfulMediumCoverCount = safetyQuarterlyReport.HarmfulMediumCoverCount;
                newSafetyQuarterlyReport.HarmfulMediumCoverCountRemark = safetyQuarterlyReport.HarmfulMediumCoverCountRemark;
                newSafetyQuarterlyReport.HarmfulMediumCoverRate = safetyQuarterlyReport.HarmfulMediumCoverRate;
                newSafetyQuarterlyReport.HarmfulMediumCoverRateRemark = safetyQuarterlyReport.HarmfulMediumCoverRateRemark;
                db.SubmitChanges();
            }
        }        

        /// <summary>
        /// 根据主键删除安全生产数据季报
        /// </summary>
        /// <param name="safetyQuarterlyReportId"></param>
        public static void DeleteSafetyQuarterlyReportById(string safetyQuarterlyReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_SafetyQuarterlyReport safetyQuarterlyReport = db.Information_SafetyQuarterlyReport.FirstOrDefault(e => e.SafetyQuarterlyReportId == safetyQuarterlyReportId);
            if (safetyQuarterlyReport!=null)
            {
                if (!string.IsNullOrEmpty(safetyQuarterlyReport.FullTimeManAttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, safetyQuarterlyReport.FullTimeManAttachUrl);
                }
                if (!string.IsNullOrEmpty(safetyQuarterlyReport.PMManAttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, safetyQuarterlyReport.PMManAttachUrl);
                }
                db.Information_SafetyQuarterlyReport.DeleteOnSubmit(safetyQuarterlyReport);
                db.SubmitChanges();
            }
        }
    }
}
