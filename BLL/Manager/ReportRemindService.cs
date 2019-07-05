using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ReportRemindService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月份获取报告信息
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static Model.ManagementReport_ReportRemind GetReportRemindByReportRemindId(string reportRemindId)
        {
            return (from x in Funs.DB.ManagementReport_ReportRemind where x.ReportRemindId == reportRemindId select x).FirstOrDefault();
        }

        /// <summary>
        /// 添加报表上报情况
        /// </summary>
        /// <param name="reportRemind"></param>
        public static void AddReportRemind(Model.ManagementReport_ReportRemind reportRemind)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ManagementReport_ReportRemind newReportRemind = new Model.ManagementReport_ReportRemind
            {
                ReportRemindId = SQLHelper.GetNewID(typeof(Model.ManagementReport_ReportRemind)),
                ProjectId = reportRemind.ProjectId,
                Months = reportRemind.Months,
                Year = reportRemind.Year,
                Month = reportRemind.Month,
                Quarterly = reportRemind.Quarterly,
                HalfYear = reportRemind.HalfYear,
                ReportName = reportRemind.ReportName,
                CompileDate = reportRemind.CompileDate
            };
            db.ManagementReport_ReportRemind.InsertOnSubmit(newReportRemind);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除报表上报情况
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteReportRemindByReportRemind(Model.ManagementReport_ReportRemind reportRemind)
        {
            Model.SUBHSSEDB db = Funs.DB;
            if (reportRemind != null)
            {
                db.ManagementReport_ReportRemind.DeleteOnSubmit(reportRemind);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目Id删除报表上报情况
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteReportRemindByProjectId(string projectId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var report = from x in db.ManagementReport_ReportRemind where x.ProjectId == projectId select x;
            if (report.Count() >0)
            {
                db.ManagementReport_ReportRemind.DeleteAllOnSubmit(report);
                db.SubmitChanges();
            }
        }
    }
}
