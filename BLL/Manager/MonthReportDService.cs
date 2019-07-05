using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MonthReportDService
    {
        /// <summary>
        /// 根据时间获取最近时间的月报
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetMonthReportByDate(DateTime date, string projectId)
        {
            var a = from x in Funs.DB.Manager_MonthReportD where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) select x;
            return a.Count() > 0;
        }

        /// <summary>
        /// 根据月份获取当月月报
        /// </summary>
        /// <param name="months">月份</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportD GetMonthReportByMonths(DateTime months, string projectId)
        {
            return (from x in Funs.DB.Manager_MonthReportD where x.ProjectId == projectId && x.Months == months select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据时间获取最近时间的月报
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetFreezeMonthReportByDate(DateTime date, string projectId, int day)
        {
            var a = from x in Funs.DB.Manager_MonthReportD where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.AddMonths(1).Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) && date.Day < (day + 1) select x;
            return a.Count() > 0;
        }

        /// <summary>
        /// 根据日期获得最近的一条月报告信息
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportD GetLastMonthReportByDate(DateTime date, string projectId)
        {
            Model.Manager_MonthReportD LastMonth = null;

            var q = from x in Funs.DB.Manager_MonthReportD where x.ProjectId == projectId && x.Months <= date orderby x.Months descending select x;
            if (q.Count() > 0)
            {
                var month = from x in q where x.Months == date select x;
                if (month.Count() > 0)  // 表示存在当月记录
                {
                    if (q.Count() > 1)
                    {
                        LastMonth = q.ToList()[1];
                    }
                }
                else    // 表示不存在当月记录
                {
                    LastMonth = q.ToList()[0];
                }
            }
            return LastMonth;
        }

        /// <summary>
        /// 根据日期获得月报告信息集合
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static List<string> GetMonthReportIdsByDate(DateTime date)
        {
            return (from x in Funs.DB.Manager_MonthReportD where x.MonthReportDate < date select x.MonthReportId).ToList();
        }

        /// <summary>
        /// 根据年份获得月报告信息集合
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static List<Model.Manager_MonthReportD> GetMonthReportsByYear(DateTime date)
        {
            return (from x in Funs.DB.Manager_MonthReportD where x.MonthReportDate < date select x).ToList();
        }

        /// <summary>
        /// 根据日期获得最近的一条月报告信息
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportD GetLastMonthReportByDate(DateTime date, int freezeDay, string projectId)
        {
            Model.Manager_MonthReportD LastMonth = null;
            DateTime? monthDate = null;

            var q = from x in Funs.DB.Manager_MonthReportD where x.ProjectId == projectId && x.Months < date orderby x.Months descending select x;
            if (q.Count() > 0)
            {
                if (date.Day < freezeDay)
                {
                    DateTime c = date.AddMonths(-1).Date;
                    monthDate = Convert.ToDateTime(c.Year + "-" + c.Month + "-01");  //当月
                }
                else
                {
                    monthDate = Convert.ToDateTime(date.Year + "-" + date.Month + "-01");  //当月
                }

                var month = from x in q where x.Months == monthDate select x;

                if (month.Count() > 0)  // 表示存在当月记录
                {
                    if (q.Count() > 1)
                    {
                        LastMonth = q.ToList()[1];
                    }
                }
                else    // 表示不存在当月记录
                {
                    LastMonth = q.ToList()[0];
                }
            }
            return LastMonth;
        }

        /// <summary>
        /// 根据月报告主键获取月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns>月报告信息</returns>
        public static Model.Manager_MonthReportD GetMonthReportByMonthReportId(string monthReportId)
        {
            return Funs.DB.Manager_MonthReportD.FirstOrDefault(x => x.MonthReportId == monthReportId);
        }

        /// <summary>
        /// 根据月报告编号获取月报告信息
        /// </summary>
        /// <param name="monthReportCode">月报告编号</param>
        /// <returns>月报告信息</returns>
        public static Model.Manager_MonthReportD GetMonthReportByMonthReportCode(string monthReportCode)
        {
            return Funs.DB.Manager_MonthReportD.FirstOrDefault(x => x.MonthReportCode == monthReportCode);
        }

        /// <summary>
        /// 增加月报告信息
        /// </summary>
        /// <param name="monthReport">月报告实体</param>
        public static void AddMonthReport(Model.Manager_MonthReportD monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportD newMonthReport = new Model.Manager_MonthReportD
            {
                MonthReportId = monthReport.MonthReportId,
                MonthReportCode = monthReport.MonthReportCode,
                ProjectId = monthReport.ProjectId,
                Months = monthReport.Months,
                MonthReportDate = monthReport.MonthReportDate,
                ReportMan = monthReport.ReportMan,
                States = monthReport.States
            };

            db.Manager_MonthReportD.InsertOnSubmit(newMonthReport);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerMonthCMenuId, monthReport.ProjectId, null, monthReport.MonthReportId, monthReport.MonthReportDate);
        }

        /// <summary>
        /// 修改月报告信息
        /// </summary>
        /// <param name="monthReport">月报告实体</param>
        public static void UpdateMonthReport(Model.Manager_MonthReportD monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportD newMonthReport = db.Manager_MonthReportD.First(e => e.MonthReportId == monthReport.MonthReportId);
            newMonthReport.MonthReportCode = monthReport.MonthReportCode;
            newMonthReport.ProjectId = monthReport.ProjectId;
            newMonthReport.Months = monthReport.Months;
            newMonthReport.MonthReportDate = monthReport.MonthReportDate;
            newMonthReport.ReportMan = monthReport.ReportMan;
            newMonthReport.States = monthReport.States;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除一个月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteMonthReportByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportD monthReport = db.Manager_MonthReportD.FirstOrDefault(e => e.MonthReportId == monthReportId);
            if (monthReport != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(monthReportId);
                BLL.CommonService.DeleteAttachFileById(monthReportId);//删除附件
                //BLL.MonthSafetyRecordDService.DeleteMonthSafetyRecordDByMonthReportId(monthReportId);
                //BLL.MonthSummaryDService.DeleteMonthSummaryDByMonthReportId(monthReportId);   
                //BLL.HSSEMeetingDService.DeleteHSSEMeetingDByMonthReportId(monthReportId);
                BLL.SafetyDataDService.DeleteSafetyDataDByMonthReportId(monthReportId);
                //BLL.EnvironmentalHealthMonthlyDService.DeleteEnvironmentalHealthMonthlyDByMonthReportId(monthReportId);
                //BLL.CheckDayRecordDService.DeleteCheckDayRecordDByMonthReportId(monthReportId);
                db.Manager_MonthReportD.DeleteOnSubmit(monthReport);
                db.SubmitChanges();
            }
        }
    }
}
