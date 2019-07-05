using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 管理月报
    /// </summary>
    public class MonthReportService
    {
        ///// <summary>
        ///// 根据时间获取最近时间的月报
        ///// </summary>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public static bool GetMonthReportByDate(DateTime date, string projectId)
        //{
        //    var a = from x in Funs.DB.Manager_MonthReport where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) select x;
        //    return a.Count() > 0;
        //}

        ///// <summary>
        ///// 根据时间获取最近时间的月报
        ///// </summary>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public static bool GetFreezeMonthReportByDate(DateTime date, string projectId, int day)
        //{
        //    var a = from x in Funs.DB.Manager_MonthReport where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.AddMonths(1).Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) && date.Day < (day + 1) select x;
        //    return a.Count() > 0;
        //}

        /// <summary>
        /// 根据时间获取最近时间的月报
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetMonthReportByDate(DateTime date, string projectId)
        {
            var a = Funs.DB.Manager_MonthReport.FirstOrDefault(x => x.ProjectId == projectId && x.ReportMonths.Value.Year == date.Year && x.ReportMonths.Value.Month == date.Month);            
            return (a != null);
        }

        /// <summary>
        /// 根据时间获取最近时间的月报
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetFreezeMonthReportByDate(DateTime date, string projectId)
        {
            int setDay = 6;
            var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
            if (sysSet != null)
            {
                int? sysSetDay = Funs.GetNewInt(sysSet.ConstValue);
                if (sysSetDay.HasValue)
                {
                    setDay = sysSetDay.Value;
                }
            }

            var a = Funs.DB.Manager_MonthReport.FirstOrDefault(x => x.ProjectId == projectId && x.ReportMonths.Value.AddMonths(1).AddDays(setDay) > date);
            return (a != null);
        }

        /// <summary>
        /// 根据日期获得月报告信息集合
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static List<string> GetMonthReportIdsByDate(DateTime date)
        {
            return (from x in Funs.DB.Manager_MonthReport where x.MonthReportDate < date select x.MonthReportId).ToList();
        }

        /// <summary>
        /// 根据日期获得最近的一条月报告信息
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        //public static Model.Manager_MonthReport GetLastMonthReportByDate(DateTime date, string projectId)
        //{
        //    Model.Manager_MonthReport LastMonth = null;
        //    if (date.Day < 5)
        //    {
        //        LastMonth = (from x in Funs.DB.Manager_MonthReport where x.Months <= date.AddMonths(-2) && x.ProjectId == projectId orderby x.MonthReportDate descending select x).FirstOrDefault();
        //    }
        //    else
        //    {
        //        LastMonth = (from x in Funs.DB.Manager_MonthReport where x.Months < date.AddMonths(-1) && x.ProjectId == projectId orderby x.MonthReportDate descending select x).FirstOrDefault();
        //    }
        //    return LastMonth;
        //}

        /// <summary>
        /// 根据月报告主键获取月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns>月报告信息</returns>
        public static Model.Manager_MonthReport GetMonthReportByMonthReportId(string monthReportId)
        {
            return Funs.DB.Manager_MonthReport.FirstOrDefault(x => x.MonthReportId == monthReportId);
        }

        /// <summary>
        /// 根据月报告主键获取月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns>月报告信息</returns>
        public static bool GetMonthReportIsCloseDByMonthReportId(string monthReportId)
        {
            int setDay = 6;
            var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
            if (sysSet != null)
            {
                int? sysSetDay = Funs.GetNewInt(sysSet.ConstValue);
                if (sysSetDay.HasValue)
                {
                    setDay = sysSetDay.Value;
                }
            }
            var monthReport =   Funs.DB.Manager_MonthReport.FirstOrDefault(x => x.MonthReportId == monthReportId && x.ReportMonths.Value.AddMonths(1).AddDays(setDay) > System.DateTime.Now);
            return (monthReport != null);
        }

        /// <summary>
        /// 根据月份获取月报告信息集合
        /// </summary>
        /// <param name="monthReportId">月份</param>
        /// <returns>月报告信息</returns>
        public static List<Model.Manager_MonthReport> GetMonthReportsByReportMonths(DateTime? reportMonths)
        {
            return (from x in Funs.DB.Manager_MonthReport where x.ReportMonths == reportMonths select x).ToList();
        }


        /// <summary>
        /// 根据月份获取月报告信息集合
        /// </summary>
        /// <param name="monthReportId">月份</param>
        /// <returns>月报告信息</returns>
        public static Model.Manager_MonthReport GetMonthReportsByReportMonthsIDProejctID(DateTime reportMonths, string monthReportId,string projectId)
        {
            return Funs.DB.Manager_MonthReport.FirstOrDefault(x => x.ReportMonths.Value.Year == reportMonths.Year && x.ReportMonths.Value.Month == reportMonths.Month && x.ProjectId == projectId && (x.MonthReportId != monthReportId || monthReportId == null));
        }

        /// <summary>
        /// 根据月报告编号获取月报告信息
        /// </summary>
        /// <param name="monthReportCode">月报告编号</param>
        /// <returns>月报告信息</returns>
        public static Model.Manager_MonthReport GetMonthReportByMonthReportCode(string monthReportCode)
        {
            return Funs.DB.Manager_MonthReport.FirstOrDefault(x => x.MonthReportCode == monthReportCode);
        }

        /// <summary>
        /// 增加月报告信息
        /// </summary>
        /// <param name="monthReport">月报告实体</param>
        public static void AddMonthReport(Model.Manager_MonthReport monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReport newMonthReport = new Model.Manager_MonthReport
            {
                MonthReportId = monthReport.MonthReportId,
                MonthReportCode = monthReport.MonthReportCode,
                ProjectId = monthReport.ProjectId,
                Months = monthReport.Months,
                ReportMonths = monthReport.ReportMonths,
                MonthReportDate = monthReport.MonthReportDate,
                MonthReportStartDate = monthReport.MonthReportStartDate,
                ReportMan = monthReport.ReportMan,
                AllProjectData = monthReport.AllProjectData,
                ThisMonthKeyPoints = monthReport.ThisMonthKeyPoints,
                ThisMonthSafetyCost = monthReport.ThisMonthSafetyCost,
                TotalSafetyCost = monthReport.TotalSafetyCost,
                ThisMonthSafetyActivity = monthReport.ThisMonthSafetyActivity,
                NextMonthWorkFocus = monthReport.NextMonthWorkFocus,
                AllManhoursData = monthReport.AllManhoursData,
                EquipmentQualityData = monthReport.EquipmentQualityData,
                FileAttachUrl = monthReport.FileAttachUrl,
                AttachUrl = monthReport.AttachUrl,
                Flag = monthReport.Flag
            };
            db.Manager_MonthReport.InsertOnSubmit(newMonthReport);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerMonthMenuId, monthReport.ProjectId, null, monthReport.MonthReportId, monthReport.MonthReportDate);
        }

        /// <summary>
        /// 修改月报告信息
        /// </summary>
        /// <param name="monthReport">月报告实体</param>
        public static void UpdateMonthReport(Model.Manager_MonthReport monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReport newMonthReport = db.Manager_MonthReport.FirstOrDefault(e => e.MonthReportId == monthReport.MonthReportId);
            if (newMonthReport != null)
            {
                newMonthReport.MonthReportCode = monthReport.MonthReportCode;
                newMonthReport.Months = monthReport.Months;
                newMonthReport.MonthReportDate = monthReport.MonthReportDate;
                newMonthReport.MonthReportStartDate = monthReport.MonthReportStartDate;
                newMonthReport.ReportMan = monthReport.ReportMan;
                newMonthReport.ReportMonths = monthReport.ReportMonths;
                newMonthReport.AllProjectData = monthReport.AllProjectData;
                newMonthReport.ThisMonthKeyPoints = monthReport.ThisMonthKeyPoints;
                newMonthReport.ThisMonthSafetyCost = monthReport.ThisMonthSafetyCost;
                newMonthReport.TotalSafetyCost = monthReport.TotalSafetyCost;
                newMonthReport.ThisMonthSafetyActivity = monthReport.ThisMonthSafetyActivity;
                newMonthReport.NextMonthWorkFocus = monthReport.NextMonthWorkFocus;
                newMonthReport.AllManhoursData = monthReport.AllManhoursData;
                newMonthReport.EquipmentQualityData = monthReport.EquipmentQualityData;
                newMonthReport.FileAttachUrl = monthReport.FileAttachUrl;
                newMonthReport.AttachUrl = monthReport.AttachUrl;
                newMonthReport.Flag = monthReport.Flag;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月报告主键删除一个月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteMonthReportByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReport monthReport = db.Manager_MonthReport.FirstOrDefault(e => e.MonthReportId == monthReportId);
            if (monthReport != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(monthReportId);
                BLL.CommonService.DeleteAttachFileById(monthReportId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(monthReportId);//删除审核流程
                db.Manager_MonthReport.DeleteOnSubmit(monthReport);
                db.SubmitChanges();
            }
        }
    }
}
