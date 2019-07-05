using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急演练开展情况季报主表
    /// </summary>
    public static class DrillConductedQuarterlyReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应急演练开展情况季报表
        /// </summary>
        /// <param name="drillConductedQuarterlyReportId"></param>
        /// <returns></returns>
        public static Model.Information_DrillConductedQuarterlyReport GetDrillConductedQuarterlyReportById(string drillConductedQuarterlyReportId)
        {
            return Funs.DB.Information_DrillConductedQuarterlyReport.FirstOrDefault(e => e.DrillConductedQuarterlyReportId == drillConductedQuarterlyReportId);
        }

        /// <summary>
        /// 应急演练开展情况季报表
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <param name="year">年度</param>
        /// <param name="quarters">季度</param>
        /// <returns>应急演练开展情况季报表</returns>
        public static Model.Information_DrillConductedQuarterlyReport GetDrillConductedQuarterlyReportByUnitIdAndYearAndQuarters(string unitId, int year, int quarters)
        {
            return Funs.DB.Information_DrillConductedQuarterlyReport.FirstOrDefault(e => e.UnitId == unitId && e.Quarter == quarters && e.YearId == year);
        }

        /// <summary>
        /// 根据单位Id获取安全生产数据季报集合
        /// </summary>
        /// <param name="UnitId">单位Id</param>
        /// <returns>安全生产数据季报集合</returns>
        public static List<Model.View_Information_DrillConductedQuarterlyReport> GetDrillConductedQuarterlyReportsByUnitId(string UnitId)
        {
            return (from x in Funs.DB.View_Information_DrillConductedQuarterlyReport where x.UnitId == UnitId orderby x.ReportDate descending select x).ToList();
        }

        /// <summary>
        /// 添加应急演练开展情况季报表
        /// </summary>
        /// <param name="drillConductedQuarterlyReport"></param>
        public static void AddDrillConductedQuarterlyReport(Model.Information_DrillConductedQuarterlyReport drillConductedQuarterlyReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_DrillConductedQuarterlyReport newDrillConductedQuarterlyReport = new Model.Information_DrillConductedQuarterlyReport
            {
                DrillConductedQuarterlyReportId = drillConductedQuarterlyReport.DrillConductedQuarterlyReportId,
                UnitId = drillConductedQuarterlyReport.UnitId,
                ReportDate = drillConductedQuarterlyReport.ReportDate,
                YearId = drillConductedQuarterlyReport.YearId,
                Quarter = drillConductedQuarterlyReport.Quarter,
                CompileMan = drillConductedQuarterlyReport.CompileMan,
                UpState = drillConductedQuarterlyReport.UpState,
                HandleState = drillConductedQuarterlyReport.HandleState,
                HandleMan = drillConductedQuarterlyReport.HandleMan
            };
            db.Information_DrillConductedQuarterlyReport.InsertOnSubmit(newDrillConductedQuarterlyReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改应急演练开展情况季报表
        /// </summary>
        /// <param name="drillConductedQuarterlyReport"></param>
        public static void UpdateDrillConductedQuarterlyReport(Model.Information_DrillConductedQuarterlyReport drillConductedQuarterlyReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_DrillConductedQuarterlyReport newDrillConductedQuarterlyReport = db.Information_DrillConductedQuarterlyReport.FirstOrDefault(e => e.DrillConductedQuarterlyReportId == drillConductedQuarterlyReport.DrillConductedQuarterlyReportId);
            if (newDrillConductedQuarterlyReport != null)
            {
                newDrillConductedQuarterlyReport.UnitId = drillConductedQuarterlyReport.UnitId;
                newDrillConductedQuarterlyReport.ReportDate = drillConductedQuarterlyReport.ReportDate;
                newDrillConductedQuarterlyReport.YearId = drillConductedQuarterlyReport.YearId;
                newDrillConductedQuarterlyReport.Quarter = drillConductedQuarterlyReport.Quarter;
                newDrillConductedQuarterlyReport.UpState = drillConductedQuarterlyReport.UpState;
                newDrillConductedQuarterlyReport.HandleState = drillConductedQuarterlyReport.HandleState;
                newDrillConductedQuarterlyReport.HandleMan = drillConductedQuarterlyReport.HandleMan;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急演练开展情况季报表
        /// </summary>
        /// <param name="drillConductedQuarterlyReportId"></param>
        public static void DeleteDrillConductedQuarterlyReportById(string drillConductedQuarterlyReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_DrillConductedQuarterlyReport drillConductedQuarterlyReport = db.Information_DrillConductedQuarterlyReport.FirstOrDefault(e => e.DrillConductedQuarterlyReportId == drillConductedQuarterlyReportId);
            if (drillConductedQuarterlyReport != null)
            {
                db.Information_DrillConductedQuarterlyReport.DeleteOnSubmit(drillConductedQuarterlyReport);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据单位、季度获取应急演练开展情况季报表
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="quarter"></param>
        /// <returns></returns>
        public static Model.Information_DrillConductedQuarterlyReport GetDrillConductedQuarterlyReportByUnitIdDate(string unitId, int yearId, int quarter,string drillConductedQuarterlyReportId)
        {
            return Funs.DB.Information_DrillConductedQuarterlyReport.FirstOrDefault(e => e.UnitId == unitId && e.YearId == yearId && e.Quarter == quarter && ((drillConductedQuarterlyReportId== null && e.DrillConductedQuarterlyReportId != null)|| e.DrillConductedQuarterlyReportId != drillConductedQuarterlyReportId));
        }
    }
}