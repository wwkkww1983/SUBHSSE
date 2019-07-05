using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class MonthReportBService
    {
        /// <summary>
        /// 根据时间获取最近时间的月报
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetMonthReportByDate(DateTime date, string projectId)
        {
            var a = from x in Funs.DB.Manager_MonthReportB where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) select x;
            return a.Count() > 0;
        }

        /// <summary>
        /// 获取项目时间段内的月报
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns></returns>
        public static List<Model.Manager_MonthReportB> GetMonthReportsByStartAndEndTimeAndProjectId(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Manager_MonthReportB where x.Months >= startTime && x.Months < endTime && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 获取所有项目时间段内的月报
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static List<Model.Manager_MonthReportB> GetMonthReportsByStartAndEndTime(DateTime startTime, DateTime endTime)
        {
            return (from x in Funs.DB.Manager_MonthReportB where x.Months >= startTime && x.Months < endTime select x).ToList();
        }

        /// <summary>
        /// 获取所有项目当月的月报
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public static List<Model.Manager_MonthReportB> GetMonthReportsByMonths(DateTime months)
        {
            return (from x in Funs.DB.Manager_MonthReportB where x.Months == months select x).ToList();
        }

        /// <summary>
        /// 根据时间获取最近时间的月报
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetFreezeMonthReportByDate(DateTime date, string projectId, int day)
        {
            var a = from x in Funs.DB.Manager_MonthReportB where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.AddMonths(1).Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) && date.Day < (day + 1) select x;
            return a.Count() > 0;
        }

        /// <summary>
        /// 根据日期获得月报告信息
        /// </summary>
        /// <param name="month">日期</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportB GetMonthReportByMonth(DateTime month, string projectId)
        {
            return (from x in Funs.DB.Manager_MonthReportB where x.Months == month && x.ProjectId == projectId select x).FirstOrDefault();
        }

        ///// <summary>
        ///// 根据日期获得最近的一条月报告信息
        ///// </summary>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public static Model.Manager_MonthReportB GetLastMonthReportByDate(DateTime date, int freezeDay, string projectId)
        //{
        //    Model.Manager_MonthReportB LastMonth = null;
        //    if (date.Day < freezeDay)
        //    {
        //        LastMonth = (from x in Funs.DB.Manager_MonthReportB where x.Months <= date.AddMonths(-2) && x.ProjectId == projectId orderby x.MonthReportDate descending select x).FirstOrDefault();
        //    }
        //    else
        //    {
        //        LastMonth = (from x in Funs.DB.Manager_MonthReportB where x.Months < date.AddMonths(-1) && x.ProjectId == projectId orderby x.MonthReportDate descending select x).FirstOrDefault();
        //    }
        //    return LastMonth;
        //}

        /// <summary>
        /// 根据日期获得最近的一条月报告信息
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportB GetLastMonthReportByDate(DateTime date, int freezeDay, string projectId)
        {
            Model.Manager_MonthReportB LastMonth = null;
            DateTime? monthDate = null;

            var q = from x in Funs.DB.Manager_MonthReportB where x.ProjectId == projectId && x.Months < date orderby x.Months descending select x;
            if (q.Count() > 0)
            {
                if (date.Day <= freezeDay)
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
        public static Model.Manager_MonthReportB GetMonthReportByMonthReportId(string monthReportId)
        {
            return Funs.DB.Manager_MonthReportB.FirstOrDefault(x => x.MonthReportId == monthReportId);
        }

        /// <summary>
        /// 根据月报告编号获取月报告信息
        /// </summary>
        /// <param name="monthReportCode">月报告编号</param>
        /// <returns>月报告信息</returns>
        public static Model.Manager_MonthReportB GetMonthReportByMonthReportCode(string monthReportCode)
        {
            return Funs.DB.Manager_MonthReportB.FirstOrDefault(x => x.MonthReportCode == monthReportCode);
        }

        /// <summary>
        /// 增加月报告信息
        /// </summary>
        /// <param name="monthReport">月报告实体</param>
        public static void AddMonthReport(Model.Manager_MonthReportB monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MonthReportB));
            Model.Manager_MonthReportB newMonthReport = new Model.Manager_MonthReportB
            {
                MonthReportId = monthReport.MonthReportId,
                MonthReportCode = monthReport.MonthReportCode,
                ProjectId = monthReport.ProjectId,
                Months = monthReport.Months,
                MonthReportDate = monthReport.MonthReportDate,
                ReportMan = monthReport.ReportMan,
                Manhours = monthReport.Manhours,
                TotalManhours = monthReport.TotalManhours,
                HseManhours = monthReport.HseManhours,
                TotalHseManhours = monthReport.TotalHseManhours,
                NoStartDate = monthReport.NoStartDate,
                NoEndDate = monthReport.NoEndDate,
                SafetyManhours = monthReport.SafetyManhours,
                AccidentReview = monthReport.AccidentReview,
                AccidentNum = monthReport.AccidentNum,
                AccidentRateA = monthReport.AccidentRateA,
                AccidentRateB = monthReport.AccidentRateB,
                AccidentRateC = monthReport.AccidentRateC,
                AccidentRateD = monthReport.AccidentRateD,
                AccidentRateE = monthReport.AccidentRateE,
                LargerHazardNun = monthReport.LargerHazardNun,
                TotalLargerHazardNun = monthReport.TotalLargerHazardNun,
                IsArgumentLargerHazardNun = monthReport.IsArgumentLargerHazardNun,
                TotalIsArgumentLargerHazardNun = monthReport.TotalIsArgumentLargerHazardNun,
                HseActiveReview = monthReport.HseActiveReview,
                HseActiveKey = monthReport.HseActiveKey,
                TotalManNum = monthReport.TotalManNum
            };

            db.Manager_MonthReportB.InsertOnSubmit(newMonthReport);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerMonthBMenuId, monthReport.ProjectId, null, monthReport.MonthReportId, monthReport.MonthReportDate);
        }

        /// <summary>
        /// 修改月报告信息
        /// </summary>
        /// <param name="monthReport">月报告实体</param>
        public static void UpdateMonthReport(Model.Manager_MonthReportB monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportB newMonthReport = db.Manager_MonthReportB.First(e => e.MonthReportId == monthReport.MonthReportId);
            newMonthReport.MonthReportCode = monthReport.MonthReportCode;
            newMonthReport.ProjectId = monthReport.ProjectId;
            newMonthReport.Months = monthReport.Months;
            newMonthReport.MonthReportDate = monthReport.MonthReportDate;
            newMonthReport.ReportMan = monthReport.ReportMan;
            newMonthReport.Manhours = monthReport.Manhours;
            newMonthReport.TotalManhours = monthReport.TotalManhours;
            newMonthReport.HseManhours = monthReport.HseManhours;
            newMonthReport.TotalHseManhours = monthReport.TotalHseManhours;
            newMonthReport.NoStartDate = monthReport.NoStartDate;
            newMonthReport.NoEndDate = monthReport.NoEndDate;
            newMonthReport.SafetyManhours = monthReport.SafetyManhours;
            newMonthReport.AccidentReview = monthReport.AccidentReview;
            newMonthReport.AccidentNum = monthReport.AccidentNum;
            newMonthReport.AccidentRateA = monthReport.AccidentRateA;
            newMonthReport.AccidentRateB = monthReport.AccidentRateB;
            newMonthReport.AccidentRateC = monthReport.AccidentRateC;
            newMonthReport.AccidentRateD = monthReport.AccidentRateD;
            newMonthReport.AccidentRateE = monthReport.AccidentRateE;
            newMonthReport.LargerHazardNun = monthReport.LargerHazardNun;
            newMonthReport.TotalLargerHazardNun = monthReport.TotalLargerHazardNun;
            newMonthReport.IsArgumentLargerHazardNun = monthReport.IsArgumentLargerHazardNun;
            newMonthReport.TotalIsArgumentLargerHazardNun = monthReport.TotalIsArgumentLargerHazardNun;
            newMonthReport.HseActiveReview = monthReport.HseActiveReview;
            newMonthReport.HseActiveKey = monthReport.HseActiveKey;
            newMonthReport.TotalManNum = monthReport.TotalManNum;
            newMonthReport.PlanCost = monthReport.PlanCost;
            newMonthReport.RealCost = monthReport.RealCost;
            newMonthReport.TotalRealCost = monthReport.TotalRealCost;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除一个月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteMonthReportByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportB monthReport = db.Manager_MonthReportB.First(e => e.MonthReportId == monthReportId);
            ///删除编码表记录
            BLL.CodeRecordsService.DeleteCodeRecordsByDataId(monthReportId);
            BLL.CommonService.DeleteAttachFileById(monthReportId);//删除附件
            db.Manager_MonthReportB.DeleteOnSubmit(monthReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 获取项目当月的月报
        /// </summary>
        /// <param name="months"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.Manager_MonthReportB GetMonthReportsByMonthsAndProjectId(DateTime months, string projectId)
        {
            return (from x in Funs.DB.Manager_MonthReportB where x.Months == months && x.ProjectId == projectId select x).FirstOrDefault();
        }

        /// <summary>
        /// 获取项目最近的的月报
        /// </summary>
        /// <param name="months"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.Manager_MonthReportB GetLateMonthReportByMonths(DateTime months, string projectId)
        {
            var q = from x in Funs.DB.Manager_MonthReportB where x.Months < months && x.ProjectId == projectId orderby x.Months descending select x;
            if (q.Count() > 0)
            {
                return q.First();
            }
            else
            {
                return null;
            }
        }
    }
}
