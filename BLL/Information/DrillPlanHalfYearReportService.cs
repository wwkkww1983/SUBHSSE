using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急演练工作计划半年报主表
    /// </summary>
    public static class DrillPlanHalfYearReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应急演练工作计划半年报
        /// </summary>
        /// <param name="drillPlanHalfYearReportId"></param>
        /// <returns></returns>
        public static Model.Information_DrillPlanHalfYearReport GetDrillPlanHalfYearReportById(string drillPlanHalfYearReportId)
        {
            return Funs.DB.Information_DrillPlanHalfYearReport.FirstOrDefault(e => e.DrillPlanHalfYearReportId == drillPlanHalfYearReportId);
        }

        /// <summary>
        /// 应急演练工作计划半年报
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <param name="year">年度</param>
        /// <param name="quarters">半年Id</param>
        /// <returns>应急演练工作计划半年报</returns>
        public static Model.Information_DrillPlanHalfYearReport GetDrillPlanHalfYearReportByUnitIdAndYearAndHalfYear(string unitId, int year, int halfYearId)
        {
            return Funs.DB.Information_DrillPlanHalfYearReport.FirstOrDefault(e => e.UnitId == unitId && e.HalfYearId == halfYearId && e.YearId == year);
        }

        /// <summary>
        /// 根据单位Id获取应急演练工作计划半年报集合
        /// </summary>
        /// <param name="UnitId">单位Id</param>
        /// <returns>应急演练工作计划半年报集合</returns>
        public static List<Model.View_Information_DrillPlanHalfYearReport> GetDrillPlanHalfYearReportsByUnitId(string UnitId)
        {
            return (from x in Funs.DB.View_Information_DrillPlanHalfYearReport where x.UnitId == UnitId orderby x.Years descending select x).ToList();
        }

        /// <summary>
        /// 根据单位、年数时间获取信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="yearId"></param>
        /// <returns></returns>
        public static Model.Information_DrillPlanHalfYearReport GetDrillPlanHalfYearReportByUnitIdDate(string unitId, int yearId, int halfYearId)
        {
            return Funs.DB.Information_DrillPlanHalfYearReport.FirstOrDefault(e => e.UnitId == unitId && e.YearId == yearId && e.HalfYearId == halfYearId);
        }

        /// <summary>
        /// 添加应急演练工作计划半年报
        /// </summary>
        /// <param name="drillPlanHalfYearReport"></param>
        public static void AddDrillPlanHalfYearReport(Model.Information_DrillPlanHalfYearReport drillPlanHalfYearReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_DrillPlanHalfYearReport newDrillPlanHalfYearReport = new Model.Information_DrillPlanHalfYearReport
            {
                DrillPlanHalfYearReportId = drillPlanHalfYearReport.DrillPlanHalfYearReportId,
                UnitId = drillPlanHalfYearReport.UnitId,
                CompileMan = drillPlanHalfYearReport.CompileMan,
                CompileDate = drillPlanHalfYearReport.CompileDate,
                YearId = drillPlanHalfYearReport.YearId,
                HalfYearId = drillPlanHalfYearReport.HalfYearId,
                Telephone = drillPlanHalfYearReport.Telephone,
                UpState = drillPlanHalfYearReport.UpState,
                HandleState = drillPlanHalfYearReport.HandleState,
                HandleMan = drillPlanHalfYearReport.HandleMan
            };
            db.Information_DrillPlanHalfYearReport.InsertOnSubmit(newDrillPlanHalfYearReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改应急演练工作计划半年报
        /// </summary>
        /// <param name="drillPlanHalfYearReport"></param>
        public static void UpdateDrillPlanHalfYearReport(Model.Information_DrillPlanHalfYearReport drillPlanHalfYearReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_DrillPlanHalfYearReport newDrillPlanHalfYearReport = db.Information_DrillPlanHalfYearReport.FirstOrDefault(e => e.DrillPlanHalfYearReportId == drillPlanHalfYearReport.DrillPlanHalfYearReportId);
            if (newDrillPlanHalfYearReport != null)
            {
                newDrillPlanHalfYearReport.UnitId = drillPlanHalfYearReport.UnitId;
                newDrillPlanHalfYearReport.CompileMan = drillPlanHalfYearReport.CompileMan;
                newDrillPlanHalfYearReport.CompileDate = drillPlanHalfYearReport.CompileDate;
                newDrillPlanHalfYearReport.YearId = drillPlanHalfYearReport.YearId;
                newDrillPlanHalfYearReport.HalfYearId = drillPlanHalfYearReport.HalfYearId;
                newDrillPlanHalfYearReport.Telephone = drillPlanHalfYearReport.Telephone;
                newDrillPlanHalfYearReport.UpState = drillPlanHalfYearReport.UpState;
                newDrillPlanHalfYearReport.HandleState = drillPlanHalfYearReport.HandleState;
                newDrillPlanHalfYearReport.HandleMan = drillPlanHalfYearReport.HandleMan;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急演练工作计划半年报
        /// </summary>
        /// <param name="drillPlanHalfYearReportId"></param>
        public static void DeleteDrillPlanHalfYearReportById(string drillPlanHalfYearReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_DrillPlanHalfYearReport drillPlanHalfYearReport = db.Information_DrillPlanHalfYearReport.FirstOrDefault(e => e.DrillPlanHalfYearReportId == drillPlanHalfYearReportId);
            if (drillPlanHalfYearReport != null)
            {
                db.Information_DrillPlanHalfYearReport.DeleteOnSubmit(drillPlanHalfYearReport);
                db.SubmitChanges();
            }
        }
    }
}