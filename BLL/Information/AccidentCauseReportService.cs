using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class AccidentCauseReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 职工伤亡事故原因分析报表
        /// </summary>
        /// <param name="AccidentCauseReportId">职工伤亡事故原因分析报表Id</param>
        /// <returns>职工伤亡事故原因分析报表</returns>
        public static Model.Information_AccidentCauseReport GetAccidentCauseReportByAccidentCauseReportId(string AccidentCauseReportId)
        {
            return Funs.DB.Information_AccidentCauseReport.FirstOrDefault(e => e.AccidentCauseReportId == AccidentCauseReportId);
        }

        /// <summary>
        /// 职工伤亡事故原因分析报表
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <param name="year">年度</param>
        /// <param name="month">月份</param>
        /// <returns>职工伤亡事故原因分析报表</returns>
        public static Model.Information_AccidentCauseReport GetAccidentCauseReportByUnitIdAndYearAndMonth(string unitId, int year, int month)
        {
            return Funs.DB.Information_AccidentCauseReport.FirstOrDefault(e => e.UnitId == unitId && e.Month == month && e.Year == year);
        }

        /// <summary>
        /// 根据单位Id获取职工伤亡事故原因分析报表集合
        /// </summary>
        /// <param name="UnitId">单位Id</param>
        /// <returns>职工伤亡事故原因分析报表集合</returns>
        public static List<Model.View_Information_AccidentCauseReport> GetAccidentCauseReportsByUnitId(string UnitId)
        {
            return (from x in Funs.DB.View_Information_AccidentCauseReport where x.UnitId == UnitId orderby x.FillingDate descending select x).ToList();
        }


        /// <summary>
        /// 增加职工伤亡事故原因分析报表
        /// </summary>
        /// <param name="AccidentCauseReport">职工伤亡事故原因分析报表实体</param>
        public static void AddAccidentCauseReport(Model.Information_AccidentCauseReport AccidentCauseReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_AccidentCauseReport newAccidentCauseReport = new Model.Information_AccidentCauseReport
            {
                AccidentCauseReportId = AccidentCauseReport.AccidentCauseReportId,
                AccidentCauseReportCode = AccidentCauseReport.AccidentCauseReportCode,
                Year = AccidentCauseReport.Year,
                Month = AccidentCauseReport.Month,
                UnitId = AccidentCauseReport.UnitId,
                DeathAccident = AccidentCauseReport.DeathAccident,
                DeathToll = AccidentCauseReport.DeathToll,
                InjuredAccident = AccidentCauseReport.InjuredAccident,
                InjuredToll = AccidentCauseReport.InjuredToll,
                MinorWoundAccident = AccidentCauseReport.MinorWoundAccident,
                MinorWoundToll = AccidentCauseReport.MinorWoundToll,
                AverageTotalHours = AccidentCauseReport.AverageTotalHours,
                AverageManHours = AccidentCauseReport.AverageManHours,
                TotalLossMan = AccidentCauseReport.TotalLossMan,
                LastMonthLossHoursTotal = AccidentCauseReport.LastMonthLossHoursTotal,
                KnockOffTotal = AccidentCauseReport.KnockOffTotal,
                DirectLoss = AccidentCauseReport.DirectLoss,
                IndirectLosses = AccidentCauseReport.IndirectLosses,
                TotalLoss = AccidentCauseReport.TotalLoss,
                TotalLossTime = AccidentCauseReport.TotalLossTime,
                FillCompanyPersonCharge = AccidentCauseReport.FillCompanyPersonCharge,
                TabPeople = AccidentCauseReport.TabPeople,
                FillingDate = AccidentCauseReport.FillingDate,
                AuditPerson = AccidentCauseReport.AuditPerson,
                UpState = AccidentCauseReport.UpState,
                HandleState = AccidentCauseReport.HandleState,
                HandleMan = AccidentCauseReport.HandleMan
            };

            db.Information_AccidentCauseReport.InsertOnSubmit(newAccidentCauseReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改职工伤亡事故原因分析报表
        /// </summary>
        /// <param name="AccidentCauseReport">职工伤亡事故原因分析报表实体</param>
        public static void UpdateAccidentCauseReport(Model.Information_AccidentCauseReport AccidentCauseReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_AccidentCauseReport newAccidentCauseReport = db.Information_AccidentCauseReport.FirstOrDefault(e => e.AccidentCauseReportId == AccidentCauseReport.AccidentCauseReportId);
            if (newAccidentCauseReport != null)
            {
                newAccidentCauseReport.AccidentCauseReportCode = AccidentCauseReport.AccidentCauseReportCode;
                newAccidentCauseReport.Year = AccidentCauseReport.Year;
                newAccidentCauseReport.Month = AccidentCauseReport.Month;
                newAccidentCauseReport.UnitId = AccidentCauseReport.UnitId;
                newAccidentCauseReport.DeathAccident = AccidentCauseReport.DeathAccident;
                newAccidentCauseReport.DeathToll = AccidentCauseReport.DeathToll;
                newAccidentCauseReport.InjuredAccident = AccidentCauseReport.InjuredAccident;
                newAccidentCauseReport.InjuredToll = AccidentCauseReport.InjuredToll;
                newAccidentCauseReport.MinorWoundAccident = AccidentCauseReport.MinorWoundAccident;
                newAccidentCauseReport.MinorWoundToll = AccidentCauseReport.MinorWoundToll;
                newAccidentCauseReport.AverageTotalHours = AccidentCauseReport.AverageTotalHours;
                newAccidentCauseReport.AverageManHours = AccidentCauseReport.AverageManHours;
                newAccidentCauseReport.TotalLossMan = AccidentCauseReport.TotalLossMan;
                newAccidentCauseReport.LastMonthLossHoursTotal = AccidentCauseReport.LastMonthLossHoursTotal;
                newAccidentCauseReport.KnockOffTotal = AccidentCauseReport.KnockOffTotal;
                newAccidentCauseReport.DirectLoss = AccidentCauseReport.DirectLoss;
                newAccidentCauseReport.IndirectLosses = AccidentCauseReport.IndirectLosses;
                newAccidentCauseReport.TotalLoss = AccidentCauseReport.TotalLoss;
                newAccidentCauseReport.TotalLossTime = AccidentCauseReport.TotalLossTime;
                newAccidentCauseReport.FillCompanyPersonCharge = AccidentCauseReport.FillCompanyPersonCharge;
                newAccidentCauseReport.TabPeople = AccidentCauseReport.TabPeople;
                newAccidentCauseReport.AuditPerson = AccidentCauseReport.AuditPerson;
                newAccidentCauseReport.FillCompanyPersonCharge = AccidentCauseReport.FillCompanyPersonCharge;
                newAccidentCauseReport.TabPeople = AccidentCauseReport.TabPeople;
                newAccidentCauseReport.AuditPerson = AccidentCauseReport.AuditPerson;
                newAccidentCauseReport.FillingDate = AccidentCauseReport.FillingDate;
                newAccidentCauseReport.UpState = AccidentCauseReport.UpState;
                newAccidentCauseReport.HandleState = AccidentCauseReport.HandleState;
                newAccidentCauseReport.HandleMan = AccidentCauseReport.HandleMan;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据Id获取数据
        /// </summary>
        /// <param name="AccidentCauseReportId"></param>
        public static void DeleteAccidentCauseReportByAccidentCauseReportId(string AccidentCauseReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_AccidentCauseReport newAccidentCauseReport = db.Information_AccidentCauseReport.FirstOrDefault(e => e.AccidentCauseReportId == AccidentCauseReportId);
            if (newAccidentCauseReport != null)
            {
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(AccidentCauseReportId);
                db.Information_AccidentCauseReport.DeleteOnSubmit(newAccidentCauseReport);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据报表单位，报表时间判断是否存在
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        public static Model.Information_AccidentCauseReport GetAccidentCauseReportByUnitIdDate(string unitId, int year, int Month)
        {
            return Funs.DB.Information_AccidentCauseReport.FirstOrDefault(e => e.UnitId == unitId && e.Year == year && e.Month == Month);
        }
    }
}
