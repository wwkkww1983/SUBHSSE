using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目HSE绩效管理报告
    /// </summary>
    public static class PerformanceManagementReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目HSE绩效管理报告
        /// </summary>
        /// <param name="performanceManagementReportId"></param>
        /// <returns></returns>
        public static Model.Manager_PerformanceManagementReport GetPerformanceManagementReportById(string performanceManagementReportId)
        {
            return Funs.DB.Manager_PerformanceManagementReport.FirstOrDefault(e => e.PerformanceManagementReportId == performanceManagementReportId);
        }

        /// <summary>
        /// 获取项目费用小计
        /// </summary>
        /// <param name="subPaydate"></param>
        /// <returns></returns>
        public static List<Model.Manager_PerformanceManagementReport> GetPerformanceManagementReportTotal(string projectId)
        {
            return (from x in db.Manager_PerformanceManagementReport where x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 添加项目HSE绩效管理报告
        /// </summary>
        /// <param name="performanceManagementReport"></param>
        public static void AddPerformanceManagementReport(Model.Manager_PerformanceManagementReport performanceManagementReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_PerformanceManagementReport newPerformanceManagementReport = new Model.Manager_PerformanceManagementReport();
            newPerformanceManagementReport.PerformanceManagementReportId = performanceManagementReport.PerformanceManagementReportId;
            newPerformanceManagementReport.ProjectId = performanceManagementReport.ProjectId;
            newPerformanceManagementReport.ReportDate = performanceManagementReport.ReportDate;
            newPerformanceManagementReport.MonthPerformance1 = performanceManagementReport.MonthPerformance1;
            newPerformanceManagementReport.MonthPerformance2 = performanceManagementReport.MonthPerformance2;
            newPerformanceManagementReport.MonthPerformance3 = performanceManagementReport.MonthPerformance3;
            newPerformanceManagementReport.MonthPerformance4 = performanceManagementReport.MonthPerformance4;
            newPerformanceManagementReport.MonthPerformance5 = performanceManagementReport.MonthPerformance5;
            newPerformanceManagementReport.MonthPerformance6 = performanceManagementReport.MonthPerformance6;
            newPerformanceManagementReport.MonthPerformance7 = performanceManagementReport.MonthPerformance7;
            newPerformanceManagementReport.MonthPerformance8 = performanceManagementReport.MonthPerformance8;
            newPerformanceManagementReport.MonthPerformance9 = performanceManagementReport.MonthPerformance9;
            newPerformanceManagementReport.MonthPerformance10 = performanceManagementReport.MonthPerformance10;
            newPerformanceManagementReport.MonthPerformance11 = performanceManagementReport.MonthPerformance11;
            newPerformanceManagementReport.MonthPerformance12 = performanceManagementReport.MonthPerformance12;
            newPerformanceManagementReport.MonthPerformance13 = performanceManagementReport.MonthPerformance13;
            newPerformanceManagementReport.MonthPerformance14 = performanceManagementReport.MonthPerformance14;
            newPerformanceManagementReport.MonthPerformance15 = performanceManagementReport.MonthPerformance15;
            newPerformanceManagementReport.MonthPerformance16 = performanceManagementReport.MonthPerformance16;
            newPerformanceManagementReport.MonthPerformance17 = performanceManagementReport.MonthPerformance17;
            newPerformanceManagementReport.MonthPerformance18 = performanceManagementReport.MonthPerformance18;
            newPerformanceManagementReport.MonthPerformance19 = performanceManagementReport.MonthPerformance19;
            newPerformanceManagementReport.PerformanceIndex1 = performanceManagementReport.PerformanceIndex1;
            newPerformanceManagementReport.PerformanceIndex2 = performanceManagementReport.PerformanceIndex2;
            newPerformanceManagementReport.PerformanceIndex3 = performanceManagementReport.PerformanceIndex3;
            newPerformanceManagementReport.PerformanceIndex4 = performanceManagementReport.PerformanceIndex4;
            newPerformanceManagementReport.PerformanceIndex5 = performanceManagementReport.PerformanceIndex5;
            newPerformanceManagementReport.PerformanceIndex6 = performanceManagementReport.PerformanceIndex6;
            newPerformanceManagementReport.PerformanceIndex7 = performanceManagementReport.PerformanceIndex7;
            newPerformanceManagementReport.PerformanceIndex8 = performanceManagementReport.PerformanceIndex8;
            newPerformanceManagementReport.PerformanceIndex9 = performanceManagementReport.PerformanceIndex9;
            newPerformanceManagementReport.PerformanceIndex10 = performanceManagementReport.PerformanceIndex10;
            newPerformanceManagementReport.PerformanceIndex11 = performanceManagementReport.PerformanceIndex11;
            newPerformanceManagementReport.CompileMan = performanceManagementReport.CompileMan;
            newPerformanceManagementReport.CompileDate = performanceManagementReport.CompileDate;
            db.Manager_PerformanceManagementReport.InsertOnSubmit(newPerformanceManagementReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目HSE绩效管理报告
        /// </summary>
        /// <param name="performanceManagementReport"></param>
        public static void UpdatePerformanceManagementReport(Model.Manager_PerformanceManagementReport performanceManagementReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_PerformanceManagementReport newPerformanceManagementReport = db.Manager_PerformanceManagementReport.FirstOrDefault(e => e.PerformanceManagementReportId == performanceManagementReport.PerformanceManagementReportId);
            if (newPerformanceManagementReport != null)
            {
                newPerformanceManagementReport.ReportDate = performanceManagementReport.ReportDate;
                newPerformanceManagementReport.MonthPerformance1 = performanceManagementReport.MonthPerformance1;
                newPerformanceManagementReport.MonthPerformance2 = performanceManagementReport.MonthPerformance2;
                newPerformanceManagementReport.MonthPerformance3 = performanceManagementReport.MonthPerformance3;
                newPerformanceManagementReport.MonthPerformance4 = performanceManagementReport.MonthPerformance4;
                newPerformanceManagementReport.MonthPerformance5 = performanceManagementReport.MonthPerformance5;
                newPerformanceManagementReport.MonthPerformance6 = performanceManagementReport.MonthPerformance6;
                newPerformanceManagementReport.MonthPerformance7 = performanceManagementReport.MonthPerformance7;
                newPerformanceManagementReport.MonthPerformance8 = performanceManagementReport.MonthPerformance8;
                newPerformanceManagementReport.MonthPerformance9 = performanceManagementReport.MonthPerformance9;
                newPerformanceManagementReport.MonthPerformance10 = performanceManagementReport.MonthPerformance10;
                newPerformanceManagementReport.MonthPerformance11 = performanceManagementReport.MonthPerformance11;
                newPerformanceManagementReport.MonthPerformance12 = performanceManagementReport.MonthPerformance12;
                newPerformanceManagementReport.MonthPerformance13 = performanceManagementReport.MonthPerformance13;
                newPerformanceManagementReport.MonthPerformance14 = performanceManagementReport.MonthPerformance14;
                newPerformanceManagementReport.MonthPerformance15 = performanceManagementReport.MonthPerformance15;
                newPerformanceManagementReport.MonthPerformance16 = performanceManagementReport.MonthPerformance16;
                newPerformanceManagementReport.MonthPerformance17 = performanceManagementReport.MonthPerformance17;
                newPerformanceManagementReport.MonthPerformance18 = performanceManagementReport.MonthPerformance18;
                newPerformanceManagementReport.MonthPerformance19 = performanceManagementReport.MonthPerformance19;
                newPerformanceManagementReport.PerformanceIndex1 = performanceManagementReport.PerformanceIndex1;
                newPerformanceManagementReport.PerformanceIndex2 = performanceManagementReport.PerformanceIndex2;
                newPerformanceManagementReport.PerformanceIndex3 = performanceManagementReport.PerformanceIndex3;
                newPerformanceManagementReport.PerformanceIndex4 = performanceManagementReport.PerformanceIndex4;
                newPerformanceManagementReport.PerformanceIndex5 = performanceManagementReport.PerformanceIndex5;
                newPerformanceManagementReport.PerformanceIndex6 = performanceManagementReport.PerformanceIndex6;
                newPerformanceManagementReport.PerformanceIndex7 = performanceManagementReport.PerformanceIndex7;
                newPerformanceManagementReport.PerformanceIndex8 = performanceManagementReport.PerformanceIndex8;
                newPerformanceManagementReport.PerformanceIndex9 = performanceManagementReport.PerformanceIndex9;
                newPerformanceManagementReport.PerformanceIndex10 = performanceManagementReport.PerformanceIndex10;
                newPerformanceManagementReport.PerformanceIndex11 = performanceManagementReport.PerformanceIndex11;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目HSE绩效管理报告
        /// </summary>
        /// <param name="performanceManagementReportId"></param>
        public static void DeletePerformanceManagementReportById(string performanceManagementReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_PerformanceManagementReport performanceManagementReport = db.Manager_PerformanceManagementReport.FirstOrDefault(e => e.PerformanceManagementReportId == performanceManagementReportId);
            if (performanceManagementReport != null)
            {
                CommonService.DeleteFlowOperateByID(performanceManagementReportId);
                db.Manager_PerformanceManagementReport.DeleteOnSubmit(performanceManagementReport);
                db.SubmitChanges();
            }
        }
    }
}
