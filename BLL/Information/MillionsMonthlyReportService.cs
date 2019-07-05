using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class MillionsMonthlyReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 百万工时安全统计月报表
        /// </summary>
        /// <param name="MillionsMonthlyReportId">百万工时安全统计月报表Id</param>
        /// <returns>百万工时安全统计月报表</returns>
        public static Model.Information_MillionsMonthlyReport GetMillionsMonthlyReportByMillionsMonthlyReportId(string MillionsMonthlyReportId)
        {
            return Funs.DB.Information_MillionsMonthlyReport.FirstOrDefault(e => e.MillionsMonthlyReportId == MillionsMonthlyReportId);
        }

        /// <summary>
        /// 百万工时安全统计月报表
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <param name="year">年度</param>
        /// <param name="month">月份</param>
        /// <returns>百万工时安全统计月报表</returns>
        public static Model.Information_MillionsMonthlyReport GetMillionsMonthlyReportByUnitIdAndYearAndMonth(string unitId, int year, int month)
        {
            return Funs.DB.Information_MillionsMonthlyReport.FirstOrDefault(e => e.UnitId == unitId && e.Month == month && e.Year == year);
        }

        /// <summary>
        /// 根据单位Id获取百万工时安全统计月报表集合
        /// </summary>
        /// <param name="UnitId">单位Id</param>
        /// <returns>百万工时安全统计月报表集合</returns>
        public static List<Model.View_Information_MillionsMonthlyReport> GetMillionsMonthlyReportsByUnitId(string UnitId)
        {
            return (from x in Funs.DB.View_Information_MillionsMonthlyReport where x.UnitId == UnitId orderby x.FillingDate descending select x).ToList();
        }

        /// <summary>
        /// 增加百万工时安全统计月报表
        /// </summary>
        /// <param name="MillionsMonthlyReport">百万工时安全统计月报表实体</param>
        public static void AddMillionsMonthlyReport(Model.Information_MillionsMonthlyReport MillionsMonthlyReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_MillionsMonthlyReport newMillionsMonthlyReport = new Model.Information_MillionsMonthlyReport
            {
                MillionsMonthlyReportId = MillionsMonthlyReport.MillionsMonthlyReportId,
                Year = MillionsMonthlyReport.Year,
                Month = MillionsMonthlyReport.Month,
                UnitId = MillionsMonthlyReport.UnitId,
                FillingDate = MillionsMonthlyReport.FillingDate,
                DutyPerson = MillionsMonthlyReport.DutyPerson,
                RecordableIncidentRate = MillionsMonthlyReport.RecordableIncidentRate,
                LostTimeRate = MillionsMonthlyReport.LostTimeRate,
                LostTimeInjuryRate = MillionsMonthlyReport.LostTimeInjuryRate,
                DeathAccidentFrequency = MillionsMonthlyReport.DeathAccidentFrequency,
                AccidentMortality = MillionsMonthlyReport.AccidentMortality,
                FillingMan = MillionsMonthlyReport.FillingMan,
                UpState = MillionsMonthlyReport.UpState,
                HandleState = MillionsMonthlyReport.HandleState,
                HandleMan = MillionsMonthlyReport.HandleMan
            };

            db.Information_MillionsMonthlyReport.InsertOnSubmit(newMillionsMonthlyReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改百万工时安全统计月报表
        /// </summary>
        /// <param name="MillionsMonthlyReport">百万工时安全统计月报表实体</param>
        public static void UpdateMillionsMonthlyReport(Model.Information_MillionsMonthlyReport MillionsMonthlyReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_MillionsMonthlyReport newMillionsMonthlyReport = db.Information_MillionsMonthlyReport.FirstOrDefault(e => e.MillionsMonthlyReportId == MillionsMonthlyReport.MillionsMonthlyReportId);
            if (newMillionsMonthlyReport != null)
            {
                newMillionsMonthlyReport.Year = MillionsMonthlyReport.Year;
                newMillionsMonthlyReport.Month = MillionsMonthlyReport.Month;
                newMillionsMonthlyReport.UnitId = MillionsMonthlyReport.UnitId;
                newMillionsMonthlyReport.FillingDate = MillionsMonthlyReport.FillingDate;
                newMillionsMonthlyReport.DutyPerson = MillionsMonthlyReport.DutyPerson;
                newMillionsMonthlyReport.RecordableIncidentRate = MillionsMonthlyReport.RecordableIncidentRate;
                newMillionsMonthlyReport.LostTimeRate = MillionsMonthlyReport.LostTimeRate;
                newMillionsMonthlyReport.LostTimeInjuryRate = MillionsMonthlyReport.LostTimeInjuryRate;
                newMillionsMonthlyReport.DeathAccidentFrequency = MillionsMonthlyReport.DeathAccidentFrequency;
                newMillionsMonthlyReport.AccidentMortality = MillionsMonthlyReport.AccidentMortality;
                newMillionsMonthlyReport.UpState = MillionsMonthlyReport.UpState;
                newMillionsMonthlyReport.HandleState = MillionsMonthlyReport.HandleState;
                newMillionsMonthlyReport.HandleMan = MillionsMonthlyReport.HandleMan;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据Id获取数据
        /// </summary>
        /// <param name="MillionsMonthlyReportId"></param>
        public static void DeleteMillionsMonthlyReportByMillionsMonthlyReportId(string MillionsMonthlyReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_MillionsMonthlyReport newMillionsMonthlyReport = db.Information_MillionsMonthlyReport.FirstOrDefault(e => e.MillionsMonthlyReportId == MillionsMonthlyReportId);
            if (newMillionsMonthlyReport != null)
            {
                db.Information_MillionsMonthlyReport.DeleteOnSubmit(newMillionsMonthlyReport);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据报表单位，报表时间判断是否存在
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        public static Model.Information_MillionsMonthlyReport GetMillionsMonthlyReportByUnitIdDate(string unitId, int year, int Month)
        {
            return Funs.DB.Information_MillionsMonthlyReport.FirstOrDefault(e => e.UnitId == unitId && e.Year == year && e.Month == Month);
        }
    }
}
