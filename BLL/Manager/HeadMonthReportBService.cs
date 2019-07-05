using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class HeadMonthReportBService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据安全统计主键获取安全统计信息
        /// </summary>
        /// <param name="headMonthReportId">安全统计主键</param>
        /// <returns>安全统计信息</returns>
        public static Model.Manager_HeadMonthReportB GetHeadMonthReportByHeadMonthReportId(string headMonthReportId)
        {
            return Funs.DB.Manager_HeadMonthReportB.FirstOrDefault(x => x.HeadMonthReportId == headMonthReportId);
        }

        /// <summary>
        /// 根据安全统计编号获取安全统计信息
        /// </summary>
        /// <param name="monthReportCode">安全统计编号</param>
        /// <returns>安全统计信息</returns>
        public static Model.Manager_HeadMonthReportB GetHeadMonthReportByMonthReportCode(string monthReportCode)
        {
            return Funs.DB.Manager_HeadMonthReportB.FirstOrDefault(x => x.MonthReportCode == monthReportCode);
        }

        public static int GetHeadMonthReportCount()
        {
            return (from x in Funs.DB.Manager_HeadMonthReportB orderby x.MonthReportCode select x).Count();
        }

        /// <summary>
        /// 根据日期获取安全统计信息
        /// </summary>
        /// <param name="months">日期</param>
        /// <returns></returns>
        public static Model.Manager_HeadMonthReportB GetHeadMonthReportByMonths(DateTime months)
        {
            return Funs.DB.Manager_HeadMonthReportB.FirstOrDefault(x => x.Months == months);
        }

        /// <summary>
        /// 获取最近时间的一条安全统计信息
        /// </summary>
        /// <returns></returns>
        public static Model.Manager_HeadMonthReportB GetLastHeadMonthReport()
        {
            return (from x in Funs.DB.Manager_HeadMonthReportB orderby x.Months descending select x).FirstOrDefault();
        }

        /// <summary>
        /// 获取早于当前时间的最近时间的一条安全统计信息
        /// </summary>
        /// <returns></returns>
        public static Model.Manager_HeadMonthReportB GetLastHeadMonthReportByMonths(DateTime months)
        {
            return (from x in Funs.DB.Manager_HeadMonthReportB where x.Months < months orderby x.Months descending select x).FirstOrDefault();
        }

        /// <summary>
        /// 增加安全统计信息
        /// </summary>
        /// <param name="headMonthReport">安全统计实体</param>
        public static void AddHeadMonthReport(Model.Manager_HeadMonthReportB headMonthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_HeadMonthReportB));
            Model.Manager_HeadMonthReportB newHeadMonthReport = new Model.Manager_HeadMonthReportB
            {
                HeadMonthReportId = headMonthReport.HeadMonthReportId,
                MonthReportCode = headMonthReport.MonthReportCode,
                Months = headMonthReport.Months,
                ReportUnitName = headMonthReport.ReportUnitName,
                ReportDate = headMonthReport.ReportDate,
                ReportMan = headMonthReport.ReportMan,
                CheckMan = headMonthReport.CheckMan,
                AllSumHseManhours = headMonthReport.AllSumHseManhours,
                AllSumTotalHseManhours = headMonthReport.AllSumTotalHseManhours
            };

            db.Manager_HeadMonthReportB.InsertOnSubmit(newHeadMonthReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全统计信息
        /// </summary>
        /// <param name="headMonthReport">安全统计实体</param>
        public static void UpdateHeadMonthReport(Model.Manager_HeadMonthReportB headMonthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_HeadMonthReportB newHeadMonthReport = db.Manager_HeadMonthReportB.First(e => e.HeadMonthReportId == headMonthReport.HeadMonthReportId);
            newHeadMonthReport.MonthReportCode = headMonthReport.MonthReportCode;
            newHeadMonthReport.Months = headMonthReport.Months;
            newHeadMonthReport.ReportUnitName = headMonthReport.ReportUnitName;
            newHeadMonthReport.ReportDate = headMonthReport.ReportDate;
            newHeadMonthReport.ReportMan = headMonthReport.ReportMan;
            newHeadMonthReport.CheckMan = headMonthReport.CheckMan;
            newHeadMonthReport.AllSumHseManhours = headMonthReport.AllSumHseManhours;
            newHeadMonthReport.AllSumTotalHseManhours = headMonthReport.AllSumTotalHseManhours;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据安全统计主键删除一个安全统计信息
        /// </summary>
        /// <param name="headMonthReportId">安全统计主键</param>
        public static void DeleteHeadMonthReportByHeadMonthReportId(string headMonthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_HeadMonthReportB headMonthReport = db.Manager_HeadMonthReportB.First(e => e.HeadMonthReportId == headMonthReportId);
            db.Manager_HeadMonthReportB.DeleteOnSubmit(headMonthReport);
            db.SubmitChanges();
        }
    }
}
